using KDACore.Helpers;
using KDACore.Models;
using KDASharedLibrary.DataAccess;
using KDASharedLibrary.Enums;
using KDASharedLibrary.Models;
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
        private bool isInitilized = false;
        public string filePath;
        private List<AppSession> sessions { get; set; } = new List<AppSession>();
        private bool isBusy = false;
        public bool IsBusy { 
            get 
            {
                return isBusy; 
            } 
        }

        public List<AppSession> GetAllSessions()
        {
            if (isInitilized)
            {
                if (sessions == null)
                {
                    try
                    {
                        sessions = BinaryConnector.StaticLoad<List<AppSession>>(filePath);
                        return sessions;
                    }
                    catch (Exception)
                    {
                        sessions = new List<AppSession>();
                        return sessions;
                    }
                }
                else
                {
                    return sessions;
                }
            }
            else
            {
                throw new Exception("Data cache file path was not provided. Call AppManager.Initialize(string path) to pass the path");
            }
        }

        public void ClearSessions()
        {
            sessions = new List<AppSession>();
        }
        public void Initialize(string path)
        {
            isInitilized = true;
            filePath = path;
        }
        private AppManager()
        {            
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

        private void CreateSession(AppSession session)
        {
            sessions.Add(session);
            BinaryConnector.StaticSave(sessions, filePath);
            //if(session.HeaderText != "")
            //{                                
            //    AddSession(session);
            //    //Console.Write($"{session.ProcessName} is displaying {session.HeaderText} at {session.ExcutableName}");
            //}

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
        public async Task<bool> /*bool*/ CheckIfSessionChanged()
        {
            isBusy = true;        
            bool isChanged;
            WindowInfo winInfo = GetWindowInfo();            
            if (sessions.Count > 0)
            {
                AppSession lastSession = GetLastSession();
                if (winInfo.Title == lastSession.App.HeaderText || winInfo.Title == "")
                {
                    isChanged = false;
                }
                else
                {
                    AppSession newSession = /*await Task.Run(() => */GetNewSessionInfo(winInfo)/*)*/;
                    if (newSession == null)
                    {
                        isChanged = false;
                    }
                    else
                    {
                        lastSession.EndTime = DateTime.Now;
                        SaveSessionData(lastSession);
                        if (lastSession.KeyboardData == null || lastSession.MouseData == null)
                        {
                            isChanged = false;
                        }
                        else
                        {
                            Console.WriteLine(newSession.App.ProcessName);
                            CreateSession(newSession);
                            isChanged = true;
                        }                        
                    }                    
                }
            }
            else
            {
                AppSession newSession = GetNewSessionInfo(winInfo);
                if (newSession == null)
                {
                    isChanged = false;
                }
                else
                {
                    sessions.Add(newSession);
                    isChanged = false;
                }                
            }             
            isBusy = false;
            return isChanged;            
        }

        public void SaveSessionData(AppSession session)
        {
            if (session.KeyboardData == null && session.MouseData == null)
            {
                session.KeyboardData = KeystrokesManager.GetKeyStrokesManager().GetKeyboardData();
                session.MouseData = MouseManager.GetMouseManager().GetMouseData();
                KeystrokesManager.GetKeyStrokesManager().SessionChanged();
                MouseManager.GetMouseManager().SessionChanged();
            }
        }

        private AppSession GetNewSessionInfo(WindowInfo winInfo)
        {
            AppSession newSession = new AppSession();
            newSession.StartTime = DateTime.Now;
            newSession.App.HeaderText = winInfo.Title;
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
                    newSession.App.ProcessName = foregroundProcess.ProcessName;
                }
                catch (Exception)
                {
                    return null;
                }                
            }
            else
            {
                newSession.App.ProcessName = foregroundProcess.ProcessName;
                if (foregroundProcess.ProcessName == "chrome")
                {
                    newSession.App.Content =  GetUrl(winInfo.WindowHandler);
                    newSession.App.Type = AppType.Browser;
                }
                    
            }
            try
            {
                newSession.App.ExcutableName = foregroundProcess.MainModule.FileName.ToString();
            }
            catch (Exception)
            {
                newSession.App.ExcutableName = "Access Denied";
            }
            return newSession;
        }

        private /*async Task<string>*/string GetUrl(IntPtr hWindow)
        {            
            AutomationElement elm = AutomationElement.FromHandle(hWindow);            
            AutomationElement elmUrlBar;
            try
            {
                //var t = elm.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane));
                var pane1 = elm.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane))[1];
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
                    var uri = new Uri(ret);                                        
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
