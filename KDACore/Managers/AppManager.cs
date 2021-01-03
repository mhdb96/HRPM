using KDACore.Helpers;
using KDACore.Models;
using KDASharedLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Automation;

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
                AddSession(session);
                //Console.Write($"{session.ProcessName} is displaying {session.HeaderText} at {session.ExcutableName}");
            }
            
        }
        private WindowInfo GetWindowInfo()
        {
            WindowInfo win = new WindowInfo();
            win.WindowHandler = NativeMethods.GetForegroundWindow();
            StringBuilder title = new StringBuilder(256);
            NativeMethods.GetWindowText(win.WindowHandler, title, title.Capacity);
            win.Title = title.ToString();
            return win;
        }
        public /*async Task<bool>*/ bool CheckIfSessionChanged()
        {
            WindowInfo winInfo = GetWindowInfo();
            //uint t;            
            //NativeMethods.GetWindowThreadProcessId(hWindow, out t);         
            AppSession lastSession = GetLastSession();            
            if (lastSession != null)
            {
                if (winInfo.Title == GetLastSession().HeaderText || winInfo.Title == "")
                {
                    return false;
                }
                else
                {
                    lastSession.EndTime = DateTime.Now;
                    AppSession newSession = GetNewSessionInfo(winInfo);
                    if (newSession == null)
                    {
                        return false;
                    }
                    CreateSession(newSession);
                    return true;
                }
            }
            else
            {
                AppSession newSession = GetNewSessionInfo(winInfo);
                if (newSession == null)
                {
                    return false;
                }
                CreateSession(newSession);
                return false;
            }
        }

        private AppSession GetNewSessionInfo(WindowInfo winInfo)
        {
            AppSession newSession = new AppSession();
            newSession.StartTime = DateTime.Now;
            newSession.HeaderText = winInfo.Title;
            Process foregroundProcess = Process.GetProcessById(NativeMethods.GetWindowProcessId(winInfo.WindowHandler));
            if (foregroundProcess.ProcessName == "ApplicationFrameHost")
            {
                try
                {
                    if (foregroundProcess == null)
                    {
                        return null;
                    }
                    foregroundProcess = GetRealProcess(foregroundProcess);
                    newSession.ProcessName = foregroundProcess.ProcessName;
                }
                catch (Exception)
                {
                    return null;
                }                
            }
            else
            {
                newSession.ProcessName = foregroundProcess.ProcessName;
                if (foregroundProcess.ProcessName == "chrome")
                {
                    newSession.Content =  GetUrl(winInfo.WindowHandler);
                    newSession.AppType = AppType.Browser;
                }
                    
            }
            try
            {
                newSession.ExcutableName = foregroundProcess.MainModule.FileName.ToString();
            }
            catch (Exception)
            {
                newSession.ExcutableName = "Access Denied";
            }
            return newSession;
        }

        private /*async Task<string>*/string GetUrl(IntPtr hWindow)
        {            
            AutomationElement elm = AutomationElement.FromHandle(hWindow);            
            AutomationElement elmUrlBar;
            try
            {                                      
                var pane1 = elm.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "Google Chrome"));            
                var pane2 = pane1.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane))[1];
                var pane3 = pane2.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane));
                var pane4 = pane3.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane));
                var cus = pane4.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Custom));
                elmUrlBar = cus.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));                               
            }
            catch
            {                
                return "";
            }
            // make sure it's valid
            if (elmUrlBar == null)
            {
                // it's not..
                return "";
            }
            // elmUrlBar is now the URL bar element. we have to make sure that it's out of keyboard focus if we want to get a valid URL
            if ((bool)elmUrlBar.GetCurrentPropertyValue(AutomationElement.HasKeyboardFocusProperty))
            {
                return "";
            }
            // there might not be a valid pattern to use, so we have to make sure we have one
            AutomationPattern[] patterns = elmUrlBar.GetSupportedPatterns();
            if (patterns.Length == 1)
            {
                string ret = "";
                try
                {
                    ret = ((ValuePattern)elmUrlBar.GetCurrentPattern(patterns[0])).Current.Value;
                    //Console.WriteLine("Open Chrome URL found: '" + ret + "'");
                    return ret;
                }
                catch { }
                if (ret != "")
                {
                    // must match a domain name (and possibly "https://" in front)
                    if (Regex.IsMatch(ret, @"^(https:\/\/)?[a-zA-Z0-9\-\.]+(\.[a-zA-Z]{2,4}).*$"))
                    {
                        // prepend http:// to the url, because Chrome hides it if it's not SSL
                        if (!ret.StartsWith("http"))
                        {
                            ret = "http://" + ret;
                        }
                        //Console.WriteLine("Open Chrome URL found: '" + ret + "'");
                        return ret;
                    }
                }
                return "";
            }
            return "";
        }

        private Process _realProcess;        


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
