﻿using Caliburn.Micro;
using System.Reflection;
using System.Windows;
using UIStrings = HRPMonitor.Properties.Resources;
using HRPMUILibrary;
using HRPMCore.Enums;
using HRPMonitor.Models;
using System.Runtime.CompilerServices;

using System.Net;
using Squirrel;
using HRPMSharedLibrary.Models;
using System;
using HRPMSharedLibrary.Enums;
using HRPMUILibrary.Logic.Logs;
using System.Threading.Tasks;
using System.Timers;
using HRPMonitor.Views;
using HRPMonitor.ICallers;
using HRPMSharedLibrary.DataAccess;

namespace HRPMonitor.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>, IHandle<LoggingInfoModel>, ILoginWindowCaller
    {
        private int _resizeBorder = 6;
        private int _outerMarginSize = 10;
        private int _windowRadius = 6;
        private int _titleHeight = 30;
        private WindowState _windowState = WindowState.Normal;
        private double _windowMinimumWidth = 400;
        private double _windowMinimumHeight = 400;
        private Visibility _windowVisibility = Visibility.Visible;
        private string _version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        private string _windowTitle = UIStrings.Title;
        private readonly IEventAggregator _eventAggregator;
        private LoggingStatus _loggingStatus = LoggingStatus.Stopped;
        private string _loggingStatusText;
        private User _user;
        Timer updateTime; 

        /// <summary>
        ///     The Constructor
        /// </summary>
        public ShellViewModel(IEventAggregator eventAggregator, User user)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            if(user == null)
            {
                _user = user;
                WindowVisibility = Visibility.Collapsed;
                LoginWindow loginWin = new LoginWindow(this);
                loginWin.Show();
            }
            else
            {
                ActivateItem(new MainControlViewModel(_eventAggregator, user));
            }                        
        }

        public string Version
        {
            get { return "Version " + _version; }
            set { _version = value; }
        }
        public double WindowMinimumWidth
        {
            get { return _windowMinimumWidth; }
            set { _windowMinimumWidth = value; }
        }
        public double WindowMinimumHeight
        {
            get { return _windowMinimumHeight; }
            set { _windowMinimumHeight = value; }
        }
        public int TitleHeight
        {
            get { return _titleHeight; }
            set { _titleHeight = value; }
        }
        public string WindowTitle
        {
            get { return _windowTitle; }
            set { _windowTitle = value; }
        }
        public Visibility WindowVisibility
        {
            get { return _windowVisibility; }
            set
            {
                _windowVisibility = value;
                NotifyOfPropertyChange(() => WindowVisibility);
            }
        }
        public WindowState WindowState
        {
            get { return _windowState; }
            set
            {
                _windowState = value;
                NotifyOfPropertyChange(() => WindowState);
                NotifyOfPropertyChange(() => ResizeBorderThickness);
                NotifyOfPropertyChange(() => OuterMarginSize);
                NotifyOfPropertyChange(() => OuterMarginSizeThickness);
                NotifyOfPropertyChange(() => WindowRadius);
                NotifyOfPropertyChange(() => WindowCornerRadius);
                NotifyOfPropertyChange(() => WindowCornerRadius);
            }
        }
        public int WindowRadius
        {
            get
            {
                return WindowState == WindowState.Maximized ? 0 : _windowRadius;
            }
            set
            {
                _windowRadius = value;
            }
        }
        public int OuterMarginSize
        {
            get
            {
                return WindowState == WindowState.Maximized ? 0 : _outerMarginSize;
            }
            set
            {
                _outerMarginSize = value;
            }
        }
        public int ResizeBorder
        {
            get
            {
                return _resizeBorder;
            }
            set
            {
                _resizeBorder = value;
            }
        }
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }
        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }
        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }
        public GridLength TitleHightGridLength { get { return new GridLength(TitleHeight + ResizeBorder); } }

        public LoggingStatus LoggingStatus
        {
            get { return _loggingStatus; }
            set
            {
                _loggingStatus = value;
                NotifyOfPropertyChange(() => LoggingStatus);
            }
        }
        public string LoggingStatusText
        {
            get { return _loggingStatusText; }
            set 
            { 
                _loggingStatusText = value;
                NotifyOfPropertyChange(() => LoggingStatusText);
            }
        }

        public void Minimize()
        {
            WindowState = WindowState.Minimized;
        }
        public void Close()
        {
            WindowVisibility = Visibility.Hidden;
        }
        public void Show()
        {
            WindowVisibility = Visibility.Visible;
        }
        public void Restore()
        {
            WindowState = WindowState.Normal;
            WindowVisibility = Visibility.Visible;
        }

        public void Handle(LoggingInfoModel message)
        {
            LoggingStatusText = message.LoggingSatusText;
            LoggingStatus = message.loggingStatus;
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            _eventAggregator.Unsubscribe(this);
        }

        public void OnLogin(User user)
        {
            _user = user;
            BinaryConnector.StaticSave(_user, GlobalConfig.UserDataFile);
            WindowVisibility = Visibility.Visible;
            ActivateItem(new MainControlViewModel(_eventAggregator, _user));
        }
    }
}
