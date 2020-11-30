using KDAUILibrary.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using KDAUILibrary.Helpers;
using KDACore.Helpers;
using KDASharedLibrary.Enums;
using KDASharedLibrary.Helpers;
using Microsoft.Win32;
using KDASharedLibrary.DataAccess;
using KDASharedLibrary.Models;
using KDACore;
using KDACore.Enums;
using KDAUILibrary;
using KDACore.Managers;

namespace KeyLoggerUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        NativeMethods.HookProc callback = KeystrokesManager.CallbackFunction;
        StateControllersManager mngr;
        public MainWindow()
        {
            InitializeComponent();
            //SetTime();
            //var mngr = KeystrokesManager.GetKeyStrokesManager();
            //runBtn.IsEnabled = true;
            //stopBtn.IsEnabled = false;
            mngr = StateControllersManager.GetStateController();            
        }
        //void SetTime()
        //{
        //    DateTime Start = DateTime.Now;
        //    dayStart = new DateTime(Start.Year,Start.Month,Start.Day,8,0,0);
        //    dayEnd = new DateTime(Start.Year, Start.Month, Start.Day, 17, 30, 0);
        //}
        


        //private void CheckForLogin()
        //{
        //    isRunning = true;
        //    while(isRunning == true)
        //    {
        //        this.Dispatcher.Invoke(() =>
        //        {
        //            if (Helpers.ConvertNanosecondToSecond(DateTime.Now.Ticks - LastLogTime.Ticks) > 1)
        //            {
        //                logstatus.IsChecked = false;
        //            }
        //            else
        //            {
        //                logstatus.IsChecked = true;
        //            }
        //        });
        //    }
        //}
        private async void runBtn_Click(object sender, RoutedEventArgs e)
        {
            mngr.Run();
            //try
            //{
            //    runBtn.IsEnabled = false;
            //    stopBtn.IsEnabled = true;
            //    //Task.Run(() => CheckForLogin());
            //    LoggingStatus l = LoggingStatus.Running;
            //    StateController.GetStateController().Run(l);
            //}
            //catch (Exception)
            //{

            //    return;
            //}
            
        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {
            mngr.Stop();
            //cts.Cancel();
            //cts = new CancellationTokenSource();
            //Trace.Listeners.Clear();
            //runBtn.IsEnabled = true;
            //stopBtn.IsEnabled = false;
        }

        void RunAsync(CancellationToken cancellationToken)
        {
            //try
            //{
            //    Trace.Listeners.Clear();
            //    TextWriterTraceListener twtl = new TextWriterTraceListener(Glo.logName);
            //    twtl.Name = "TextLogger";
            //    twtl.TraceOutputOptions = TraceOptions.ThreadId | TraceOptions.DateTime;
            //    ConsoleTraceListener ctl = new ConsoleTraceListener(false);
            //    ctl.TraceOutputOptions = TraceOptions.DateTime;
            //    Trace.Listeners.Add(twtl);
            //    Trace.Listeners.Add(ctl);
            //    Trace.AutoFlush = true;
            //    // Start the clipboard
            //    //ThreadStart clipboardThreadStart = new ThreadStart(Log.BootClipboard);
            //    //Thread clipboardThread = new Thread(clipboardThreadStart);
            //    //clipboardThread.Start();
            //    //Application.Run(new ClipboardNotification.NotificationForm());
            //    //NativeMethods.HookProc callback = Log.CallbackFunction;
            //    var module = Process.GetCurrentProcess().MainModule.ModuleName;
            //    var moduleHandle = NativeMethods.GetModuleHandle(module);
            //    var hook = NativeMethods.SetWindowsHookEx(HookType.WH_KEYBOARD_LL, callback, moduleHandle, 0);
            //    while (true)
            //    {
            //        NativeMethods.PeekMessage(IntPtr.Zero, IntPtr.Zero, 0x100, 0x109, 0);
            //        System.Threading.Thread.Sleep(5);
            //        cancellationToken.ThrowIfCancellationRequested();
            //    }
            //}
            //catch
            //{
            //    var mngr = KeystrokesManager.GetKeyStrokesManager();
            //    mngr.SaveKeystrokeData();
            //}
        }

        private void analyse_Click(object sender, RoutedEventArgs e)
        {
            //var data = UserProperties.GetKeyStrokesData();
            //StringBuilder b = new StringBuilder();
            //for (int i = -1; i < FileHelper.GetEnumCount<KeysList>(); i++)
            //{
            //    if (i != -1)
            //    {
            //        b.Append(((KeysList)i).GetDescription() + ",");
            //    }
            //    for (int j = -1; j < FileHelper.GetEnumCount<KeysList>(); j++)
            //    {
            //        if (j == -1 && i == -1)
            //        {
            //            b.Append("*,");
            //            continue;
            //        }
            //        if (i == -1)
            //        {
            //            b.Append(((KeysList)j).GetDescription() + ","); ;
            //        }
            //        else if (j == -1)
            //        {
            //            continue;
            //        }
            //        else
            //        {
            //            if (data[i] != null)
            //            {
            //                if (data[i].SeekTimes[j] != null)
            //                {
            //                    b.Append((data[i].SeekTimes[j].Sum(item => item)  / data[i].SeekTimes[j].Count) + ",");
            //                }
            //                else
            //                {
            //                    b.Append(",");
            //                }
            //            }

            //        }
            //    }
            //    b.AppendLine();
            //}
            //File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + "mat" + ".txt", b.ToString());

            //StringBuilder bu = new StringBuilder();
            //for (int i = -1; i < FileHelper.GetEnumCount<KeysList>(); i++)
            //{
            //    if (i != -1)
            //    {
            //        bu.Append(((KeysList)i).GetDescription() + ",");
            //    }
            //    for (int j = -1; j < FileHelper.GetEnumCount<KeysList>(); j++)
            //    {
            //        if (j == -1 && i == -1)
            //        {
            //            bu.Append("*,");
            //            continue;
            //        }
            //        if (i == -1)
            //        {
            //            bu.Append(((KeysList)j).GetDescription() + ",");
            //        }
            //        else if (j == -1)
            //        {
            //            continue;
            //        }
            //        else
            //        {
            //            if (data[i] != null)
            //            {
            //                if (data[i].SeekTimes[j] != null)
            //                {
            //                    bu.Append(data[i].SeekTimes[j].Count + ",");
            //                }
            //                else
            //                {
            //                    bu.Append(",");
            //                }
            //            }
            //        }
            //    }
            //    bu.AppendLine();
            //}
            //File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + "matcount" + ".txt", bu.ToString());


            //bu = new StringBuilder();
            //for (int i = 0; i < FileHelper.GetEnumCount<KeysList>(); i++)
            //{
            //    bu.Append(((KeysList)i).GetDescription() + ",");
            //    if (data[i] != null)
            //    {
            //        bu.Append(data[i].HoldTimes.Count + ",");
            //        bu.Append((data[i].HoldTimes.Sum(item => item) / data[i].HoldTimes.Count) + ",");
            //    }
            //    bu.AppendLine();
            //}
            //File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + "keystats" + ".txt", bu.ToString());
            ////Process.Start(@"C:\Users\mhdb9\Documents\Github\KeystrokeDynamics\bin\Debug\Keylogger.exe");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".kdf"; // Default file extension
            dlg.Filter = "KDA Data File (.kdf)|*.kdf"; // Filter files by extension
            dlg.InitialDirectory = @"C:\Users\mhdb9\Desktop\API\Data\MHD-MSI-mhdb9";
            dlg.Title = "Open KDA Data File";

            // Show open file dialog box
            var result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                var d = BinaryConnector.StaticLoad<KeystrokeData[]>(filename);
            }

        }
    }
}
