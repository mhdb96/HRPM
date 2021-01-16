using Caliburn.Micro;
using Hardcodet.Wpf.TaskbarNotification;
using HRPMonitor.Models;
using HRPMonitor.ViewModels;
using HRPMSharedLibrary.Enums;
using HRPMSharedLibrary.Models;
using HRPMUILibrary.Logic.Logs;
using Squirrel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HRPMonitor.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window, IHandle<BalloonMessageModel>
    {
        private readonly IEventAggregator _eventAggregator;
        public ShellView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            var resizer = new WindowResizer(this);
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _eventAggregator.Unsubscribe(this);
            Log log = new Log();
            log.Text = "Program forceblly closed";
            log.Type = LogType.ProgramClosed;
            LogsManager.GetLogsManager().AddLog(log);            
        }

        private void MyNotifyIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            this.Show();
        }

        public void Handle(BalloonMessageModel model)
        {
            MyNotifyIcon.ShowBalloonTip(model.Title, model.Message, BalloonIcon.Info);
        }
    }
}
