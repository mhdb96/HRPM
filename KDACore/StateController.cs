using KDACore.Enums;
using KDACore.Logic;
using KDASharedLibrary.DataAccess;
using KDASharedLibrary.Enums;
using KDASharedLibrary.Helpers;
using KDASharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KDACore
{    
    public class StateController
    {        
        private readonly int _charCount = FileHelper.GetEnumCount<KeysList>();
        public string filePath;
        private static readonly StateController _instance = new StateController();
        private LoggingStatus programStatus = LoggingStatus.Stopped;
        CancellationTokenSource cts = new CancellationTokenSource();
        NativeMethods.HookProc callback = KeystrokesManager.CallbackFunction;
        private Task logger;
        private bool isInitilized = false;
        IntPtr hHook;
        public KeystrokeData[] KeystrokeData;

        
        public KeystrokeData[] GetKeyStrokesData()
        {
            if (isInitilized)
            {
                if (KeystrokeData == null)
                {
                    try
                    {
                        KeystrokeData = BinaryConnector.StaticLoad<KeystrokeData[]>(filePath);
                        return KeystrokeData;
                    }
                    catch (Exception)
                    {
                        KeystrokeData = new KeystrokeData[_charCount];
                        return KeystrokeData;
                    }
                }
                else
                {
                    return KeystrokeData;
                }
            }
            else
            {
                throw new Exception("Data cache file path was not provided. Call StateController.Initialize(string path) to pass the path");
            } 
        }

        private StateController()
        {
        }

        public static StateController GetStateController()
        {
            return _instance;
        }

        public void Initialize(string path)
        {
            isInitilized = true;
            filePath = path;
        }

        public void Run(LoggingStatus status)
        {
            programStatus = status;
            if(logger == null)
            {
                logger = Task.Run(() => RunTask(cts.Token)); 
            }
            else
            {
                if (logger.Status == TaskStatus.Running)
                {
                    return;
                }
                else if (logger.Status == TaskStatus.Canceled || logger.Status == TaskStatus.Faulted || logger.Status == TaskStatus.RanToCompletion)
                {
                    logger = Task.Run(() => RunTask(cts.Token));
                }
            }
            
        }
        public void Stop(LoggingStatus status)
        {
            programStatus = status;
            if (logger == null)
            {
                return;
            }
            else
            {
                if (logger.Status == TaskStatus.Running)
                {
                    TerminateTask();
                }
                else if (logger.Status == TaskStatus.Canceled || logger.Status == TaskStatus.Faulted)
                {
                    return;
                }
            }
        }

        private void TerminateTask()
        {
            cts.Cancel();
            cts = new CancellationTokenSource();
            KeystrokesManager.GetKeyStrokesManager().SaveKeystrokeData();
            KeystrokeData = null;
        }
        public void CleanKeystrokeData()
        {
            KeystrokeData = null;
        }
        private void RunTask(CancellationToken cancellationToken)
        {
            //Trace.Listeners.Clear();
            //TextWriterTraceListener twtl = new TextWriterTraceListener(UserProperties.logName);
            //twtl.Name = "TextLogger";
            //twtl.TraceOutputOptions = TraceOptions.ThreadId | TraceOptions.DateTime;
            //ConsoleTraceListener ctl = new ConsoleTraceListener(false);
            //ctl.TraceOutputOptions = TraceOptions.DateTime;
            //Trace.Listeners.Add(twtl);
            //Trace.Listeners.Add(ctl);
            //Trace.AutoFlush = true;
            
            var module = Process.GetCurrentProcess().MainModule.ModuleName;
            var moduleHandle = NativeMethods.GetModuleHandle(module);
            hHook = NativeMethods.SetWindowsHookEx(HookType.WH_MOUSE_LL, callback, moduleHandle, 0);
            try
            {
                while (true)
                {
                    NativeMethods.PeekMessage(IntPtr.Zero, IntPtr.Zero, 0x100, 0x109, 0);
                    Thread.Sleep(5);
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
            catch (Exception)
            {
                NativeMethods.UnhookWindowsHookEx(hHook);
                //Trace.Listeners.Clear();
                //twtl.Close();
                //ctl.Close();
                return;
            }  
        }
    }
}
