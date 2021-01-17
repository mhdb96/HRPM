using Caliburn.Micro;
using HRPMCore;
using HRPMCore.Enums;
using HRPMCore.Managers;
using HRPMonitor.ICallers;
using HRPMonitor.Models;
using HRPMonitor.Views;
using HRPMSharedLibrary.Enums;
using HRPMSharedLibrary.Models;
using HRPMUILibrary;
using HRPMUILibrary.Helpers;
using HRPMUILibrary.Logic.Files;
using HRPMUILibrary.Logic.Logs;
using MaterialDesignThemes.Wpf;
using Squirrel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using UIStrings = HRPMonitor.Properties.Resources;

namespace HRPMonitor.ViewModels
{
    public class MainControlViewModel : Screen, IHandle<PauseOptionModel>, ITasksWindowCaller
    {
        //private string _username = GlobalConfig.CurruntUser;
        private bool _isLogging = false;
        private string _startTimeLabel = UIStrings.StartTimeLabel;
        private string _stopTimeLabel = UIStrings.StopTimeLabel;
        private string _startTime;
        private string _stopTime;
        private LoggingStatus _loggingStatus = LoggingStatus.Stopped;
        private bool _isControlable = false;
        
        private bool _shouldSkip = false;
        private Timer appTimer;
        private DateTime startTime = new DateTime(2019, 10, 11, 0, 0, 0);
        private DateTime endTime = new DateTime(2019, 10, 11, 23, 50, 0);
        private double selectedPausePeriodTime = 0;
        private int pauseCountDown = 0;
        private bool isPaused = false;
        private bool isConnected = false;
        private readonly IEventAggregator _eventAggregator;
        private StateControllersManager stateMngr;
        private LogsManager logMngr;
        private FilesManager filelMngr;
        private User _user;
        private WorkTask curruntTask;
        private List<WorkTask> workTasks = new List<WorkTask>();

        #region UI Properties
        public string Username
        {
            get { return _user.UserName; }
            set { _user.UserName = value; }
        }
        public bool IsLogging
        {
            get { return _isLogging; }
            set
            {
                _isLogging = value;
                NotifyOfPropertyChange(() => IsLogging);
            }
        }
        public string StartTimeLabel
        {
            get { return _startTimeLabel; }
            set
            {
                _startTimeLabel = value;
                NotifyOfPropertyChange(() => StartTimeLabel);
            }
        }
        public string StopTimeLabel
        {
            get { return _stopTimeLabel; }
            set
            {
                _stopTimeLabel = value;
                NotifyOfPropertyChange(() => StopTimeLabel);
            }
        }
        public string StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                NotifyOfPropertyChange(() => StartTime);
            }
        }
        public string StopTime
        {
            get { return _stopTime; }
            set
            {
                _stopTime = value;
                NotifyOfPropertyChange(() => StopTime);
            }
        }
        public string ActionToolTip
        {
            get
            {
                if (LoggingStatus == LoggingStatus.Stopped || LoggingStatus == LoggingStatus.Paused)
                {
                    return UIStrings.RunActionToolTip;
                }
                else
                {
                    return UIStrings.PauseActionToolTip;
                }
            }
        }
        public string StatusDescription
        {
            get
            {
                if (LoggingStatus == LoggingStatus.Running)
                {
                    return UIStrings.RunningStatusDescription;
                }
                else if (LoggingStatus == LoggingStatus.Paused)
                {
                    return string.Format(UIStrings.PauesdStatusDescription, selectedPausePeriodTime);
                }
                else
                {
                    return UIStrings.StoppedStatusDescription;
                }
            }
        }
        public bool IsControlable
        {
            get { return _isControlable; }
            set
            {
                _isControlable = value;
                NotifyOfPropertyChange(() => IsControlable);
            }
        }
        public bool IsRunning
        {
            get
            {
                if (LoggingStatus == LoggingStatus.Running)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        public bool IsStopped
        {
            get
            {
                if (LoggingStatus == LoggingStatus.Stopped)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        public bool IsPaused
        {
            get
            {
                if (LoggingStatus == LoggingStatus.Paused)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public string LoggingStatusText
        {
            get
            {
                if (LoggingStatus == LoggingStatus.Running)
                {
                    return UIStrings.RunningLoggingStatusText.ToUpper();
                }
                else if (LoggingStatus == LoggingStatus.Paused)
                {
                    return UIStrings.PausedLoggingStatusText.ToUpper();
                }
                else
                {
                    return UIStrings.StoppedLoggingStatusText.ToUpper();
                }
            }
        }
        public LoggingStatus LoggingStatus
        {
            get { return _loggingStatus; }
            set
            {
                _loggingStatus = value;
                NotifyOfPropertyChange(() => LoggingStatus);
                NotifyOfPropertyChange(() => IsRunning);
                NotifyOfPropertyChange(() => IsStopped);
                NotifyOfPropertyChange(() => IsPaused);
                NotifyOfPropertyChange(() => LoggingStatusText);
                NotifyOfPropertyChange(() => ActionToolTip);
                NotifyOfPropertyChange(() => StatusDescription);
                ShowBalloon();
                SetLoggerStatus();
                PublishStatusCahngedEvent();
            }
        }

        public string TaskText 
        { 
            get 
            {
                return "Tasks";
            } 
        }

        #endregion

        /// <summary>
        ///     The Constructor
        /// </summary>
        public MainControlViewModel(IEventAggregator eventAggregator, User user)
        {
            _user = user;
            stateMngr = StateControllersManager.GetStateController();
            logMngr = LogsManager.GetLogsManager();
            filelMngr = FilesManager.GetFilesManager();
            StartTime = startTime.ToString("HH:mm");
            StopTime = endTime.ToString("HH:mm");
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            appTimer = new Timer(1000);
            appTimer.Elapsed += AppTimer_Elapsed;
            appTimer.Start();
        }        
        public async Task Toggle()
        {
            if (LoggingStatus == LoggingStatus.Paused)
            {
                isPaused = false;
                StartTime = startTime.ToString("HH:mm");
                StartTimeLabel = UIStrings.StartTimeLabel;
                LoggingStatus = LoggingStatus.Running;
                logMngr.AddLog(new Log
                {
                    Severity = LogSeverity.Medium,
                    Type = LogType.LoggerStarted,
                    Text = "Logger Started Manually."
                });
            }
            else if (LoggingStatus == LoggingStatus.Running)
            {
                await DialogHost.Show(new PauseDialogView(_eventAggregator), "RootDialog");
            }
        }

        public void OpenTasks()
        {
            TasksWindow win = new TasksWindow(curruntTask, workTasks, this);
            win.ShowDialog();
        }
        public void Handle(PauseOptionModel message)
        {
            selectedPausePeriodTime = message.SelectedPausePeriodTime;
            StartPauseTimer((int)message.SelectedPausePeriodTime);
        }
        private void StartPauseTimer(int periodInMinute)
        {
            StartTimeLabel = UIStrings.PauseTimeLabel;
            pauseCountDown = periodInMinute * 60;
            isPaused = true;
            LoggingStatus = LoggingStatus.Paused;
            logMngr.AddLog(new Log
            {
                Severity = LogSeverity.High,
                Type = LogType.LoggerPaused,
                Text = $"Logger paused for {periodInMinute} mins."
            });
        }

        private void AppTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (TimeManager.GetTimeManager().Usage != null)
            {
                //Console.WriteLine(TimeManager.GetTimeManager().Usage.IdleMinutes);
                var api = ApiHelper.GetApiHelper();
                api.PostUsageTime(TimeManager.GetTimeManager().Usage, _user);
                TimeManager.GetTimeManager().Usage = null;
            }
            TimeSpan nowtime = DateTime.Now.TimeOfDay;
            if (isConnected)
            {
                if (DateTime.Now.Second % 10 == 0)
                {
                    if (logMngr.IsSync == false)
                    {
                        logMngr.SyncLogs(_user);
                    }
                }
                if (DateTime.Now.Minute % 30  == 0)
                {                    
                    if (!_shouldSkip)
                    {
                        _shouldSkip = true;
                        filelMngr.CreateLocalFile(stateMngr.GetSessions());
                        filelMngr.DeleteLiveDataFile();
                        stateMngr.ClearData();                                                
                        if (filelMngr.IsSync == false)
                        {
                            filelMngr.SyncFiles(_user);
                        }
                    }    
                }
                else
                {
                    _shouldSkip = false;
                }
            }
            if (isPaused)
            {
                pauseCountDown--;
                if (pauseCountDown == 0)
                {
                    isPaused = false;
                    //IsControlable = true;
                    StartTime = UIStrings.StartTime;
                    StartTimeLabel = UIStrings.StartTimeLabel;
                    LoggingStatus = LoggingStatus.Running;
                    logMngr.AddLog(new Log
                    {
                        Severity = LogSeverity.Low,
                        Type = LogType.LoggerStarted,
                        Text = "Logger Started automatically after being paused."
                    });
                }
                else
                {
                    TimeSpan time = TimeSpan.FromSeconds(pauseCountDown);
                    StartTime = time.ToString(@"hh\:mm\:ss");
                }

            }
            if (LoggingStatus == LoggingStatus.Stopped &&
                nowtime > startTime.TimeOfDay && nowtime < endTime.TimeOfDay /*&& DateTime.Now.DayOfWeek != DayOfWeek.Saturday*/ /*&& DateTime.Now.DayOfWeek != DayOfWeek.Sunday*/)
            {
                isConnected = true;
                LoggingStatus = LoggingStatus.Running;
                IsControlable = true;
                logMngr.AddLog(new Log
                {
                    Severity = LogSeverity.Low,
                    Type = LogType.LoggerStarted,
                    Text = "Logger Started automatically."
                });
                return;
            }
            if(
                (LoggingStatus == LoggingStatus.Running || LoggingStatus == LoggingStatus.Paused) 
                && 
                (nowtime > endTime.TimeOfDay || nowtime < startTime.TimeOfDay))
            {
                LoggingStatus = LoggingStatus.Stopped;
                filelMngr.CreateLocalFile(stateMngr.GetSessions());
                filelMngr.DeleteLiveDataFile();
                stateMngr.ClearData();
                isConnected = false;
                IsControlable = false;
                logMngr.AddLog(new Log
                {
                    Severity = LogSeverity.Low,
                    Type = LogType.LoggerStopped,
                    Text = "Logger Stopped automatically."
                });
                return;
            }
        }
        private void ShowBalloon()
        {
            string title;
            string message;
            if(LoggingStatus == LoggingStatus.Running)
            {
                title = UIStrings.RunningSystemNotificationTitle;
                message = UIStrings.RunningSystemNotificationMessage;
                
            } 
            else if(LoggingStatus == LoggingStatus.Paused)
            {
                title = UIStrings.PausedSystemNotificationTitle;
                message = UIStrings.PausedSystemNotificationMessage;
            } else
            {
                title = UIStrings.StoppedSystemNotificationTitle;
                message = UIStrings.StoppedSystemNotificationMessage;
            }
            BalloonMessageModel msg = new BalloonMessageModel { Message = message, Title = title };
            _eventAggregator.PublishOnUIThread(msg);
        }

        private void SetLoggerStatus()
        {
            if(LoggingStatus == LoggingStatus.Running)
            {
                stateMngr.Run();
            } 
            else
            {
                stateMngr.Stop();
            }
        }
        private void PublishStatusCahngedEvent()
        {
            _eventAggregator.BeginPublishOnUIThread(new LoggingInfoModel
            {
                LoggingSatusText = LoggingStatusText,
                loggingStatus = LoggingStatus
            });
        }

        public void TaskAdded(WorkTask workTask)
        {
            
        }

        public void TaskCreated(WorkTask workTask)
        {
            curruntTask = workTask;
        }

        public async Task TaskSaved(WorkTask workTask)
        {
            var api = ApiHelper.GetApiHelper();
            await api.PostTask(workTask, _user);
        }
    }
}
