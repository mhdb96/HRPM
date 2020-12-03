using KDACore.Helpers;
using KDACore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KDACore.Managers
{
    public class AppManager
    {
        private static readonly AppManager _instance = new AppManager();
        private List<AppSession> sessions { get; set; } = new List<AppSession>();

        private AppManager()
        {
            //keystrokeEventsBuffer = new List<KeystrokeEvent>();
        }

        public static AppManager GetAppManager()
        {
            return _instance;
        }

        public static IntPtr CallbackFunction(Int32 code, IntPtr wParam, IntPtr lParam)
        {
            AppManager.GetAppManager().CheckIfSessionChanged();
            //if(evenT == 0x0003 || evenT == 0x800C)
            //{

            //    StringBuilder title = new StringBuilder(256);
            //    StringBuilder name = new StringBuilder(256);
            //    NativeMethods.GetWindowText(hwnd, title, title.Capacity);                
            //    uint t;
            //    NativeMethods.GetWindowThreadProcessId(hwnd, out t);
            //    if(Process.GetProcessById((int)t).ProcessName == "chrome")
            //    {
            //        Console.WriteLine(title.ToString());
            //    }
            //    //Console.WriteLine(Process.GetProcessById((int)t).ProcessName);                

            //    //Console.WriteLine(title.ToString());
            //}


            //Int32 msgType = wParam.ToInt32();
            //if (code >= 0 && (msgType == 0x100 || msgType == 0x104 || msgType == 0x101))
            //{
            //    var logMngr = GetKeyStrokesManager();
            //    //var t = logMngr.GetCurrentKeyboardLayout().Name;
            //    IntPtr hWindow = NativeMethods.GetForegroundWindow();
            //    StringBuilder title = new StringBuilder(256);
            //    NativeMethods.GetWindowText(hWindow, title, title.Capacity);
            //    if (title.ToString() != lastTitle)
            //    {
            //        lastTitle = title.ToString();
            //        logMngr.WindowChanged();
            //    }
            //    var vKey = Marshal.ReadInt32(lParam);
            //    string btnStatus = "";
            //    KeystrokeEvent keystrokeEvent = new KeystrokeEvent();
            //    keystrokeEvent.EventTime = DateTime.Now;
            //    Keys defaultKeysEnum = (Keys)vKey;
            //    var key = KeyMapper.GetKeyEnum(defaultKeysEnum);
            //    if (key == KeysList.NoKey)
            //    {
            //        return NativeMethods.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            //    }
            //    keystrokeEvent.Key.Data = key;
            //    if (wParam.ToInt32() == 256)
            //    {
            //        keystrokeEvent.Type = KeystrokeType.KeyDown;
            //        btnStatus = "Down";
            //    }
            //    else if (wParam.ToInt32() == 257)
            //    {
            //        keystrokeEvent.Type = KeystrokeType.KeyUp;
            //        btnStatus = "Up  ";
            //    }
            //    logMngr.InsertKeystrokeEvent(keystrokeEvent);
            //    Console.WriteLine($"{btnStatus},{DateTime.Now.Ticks},{defaultKeysEnum.GetDescription()}, {key.GetDescription()}");
            //    //Trace.WriteLine($"{btnStatus},{DateTime.Now.Ticks},{key.GetDescription()}");
            //}          
            return NativeMethods.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }
        public AppSession GetLastSession()
        {
            if(sessions.Count > 0)
            {
                return sessions.Last();
            }
            else
            {
                return null;
            }
            
        }
        public void AddSession(AppSession session)
        {
            sessions.Add(session);
        }

        private void CreateSession(AppSession session)
        {
            if(session.HeaderText != "")
            {                
                session.StartTime = DateTime.Now;
                AddSession(session);
                Console.Write($"{session.ProcessName} is displaying {session.HeaderText} at {session.ExcutablePath}");
            }
            
        }
        public /*async Task<bool>*/ bool CheckIfSessionChanged()
        {

            IntPtr hWindow = NativeMethods.GetForegroundWindow();
            StringBuilder title = new StringBuilder(256);
            string name="";
            NativeMethods.GetWindowText(hWindow, title, title.Capacity);
            uint t;            
            NativeMethods.GetWindowThreadProcessId(hWindow, out t);
            uint pid = 0;            
            AppSession lastSession = GetLastSession();
            AppSession currentSession = new AppSession(); 
            if (lastSession != null)
            {
                if (title.ToString() == GetLastSession().HeaderText || title.ToString() == "")
                {
                    return false;
                }
                else
                {                    
                    currentSession.HeaderText = title.ToString();
                    Process foregroundProcess = Process.GetProcessById(NativeMethods.GetWindowProcessId(hWindow));
                    if (foregroundProcess.ProcessName == "ApplicationFrameHost")
                    {                        
                        if (foregroundProcess == null)
                        {
                            return false;
                        }
                        foregroundProcess = GetRealProcess(foregroundProcess);                        
                        currentSession.ProcessName = foregroundProcess.ProcessName;
                    }
                    else
                    {
                        currentSession.ProcessName = foregroundProcess.ProcessName;
                    }
                    try
                    {
                        currentSession.ExcutablePath = foregroundProcess.MainModule.FileName.ToString();                                                
                    }
                    catch (Exception)
                    {
                        currentSession.ExcutablePath = "Access Denied";
                    }
                    CreateSession(currentSession);
                    return true;
                }
            }
            else
            {
                CreateSession(currentSession);
                return false;
            }
            
        }
        private Process _realProcess;        

        private void TimerCallback(object state)
        {
            
        }

        private Process GetRealProcess(Process foregroundProcess)
        {
            NativeMethods.EnumChildWindows(foregroundProcess.MainWindowHandle, ChildWindowCallback, IntPtr.Zero);
            return _realProcess;
        }

        private bool ChildWindowCallback(IntPtr hwnd, IntPtr lparam)
        {
            var process = Process.GetProcessById(NativeMethods.GetWindowProcessId(hwnd));
            if (process.ProcessName != "ApplicationFrameHost")
            {
                _realProcess = process;
            }
            return true;
        }

    }
}
