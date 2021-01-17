using HRPMCore.Enums;
using HRPMCore.Helpers;
using HRPMCore.Models;
using HRPMCore.StateControllers;
using HRPMSharedLibrary.Enums;
using HRPMSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HRPMCore.Managers
{
    public class MouseManager
    {
        private static readonly MouseManager _instance = new MouseManager();
        private List<MouseClick> mouseClicks = new List<MouseClick>();
        private List<MouseClickEvent> mouseClickEventsBuffer;
        private MouseStateController controller;
        MouseData mouseData = new MouseData();

        private MouseManager()
        {
            controller = MouseStateController.GetStateController();
            mouseClickEventsBuffer = new List<MouseClickEvent>();
        }

        public static MouseManager GetMouseManager()
        {
            return _instance;
        }

        public static IntPtr CallbackFunction(Int32 code, IntPtr wParam, IntPtr lParam)
        {
            WM t = (WM)wParam;
            if (t == WM.MOUSEWHEEL || t == WM.RBUTTONDOWN || t == WM.RBUTTONUP || t == WM.LBUTTONDOWN || t == WM.LBUTTONUP || t == WM.XBUTTONUP || t == WM.XBUTTONDOWN || t == WM.MOUSEMOVE)
            {
                //Task.Run(async () => { await Task.Run(() => { AppManager.GetAppManager().CheckIfSessionChanged(); }); });
                var mngr = GetMouseManager();
                if (AppManager.GetAppManager().IsBusy == true)
                {                    
                    return NativeMethods.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
                }
                AppManager.GetAppManager().CheckIfSessionChanged();
                MSLLHOOKSTRUCT butonData = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                MouseClickEvent mouseClickEvent = new MouseClickEvent();
                mouseClickEvent.EventTime = butonData.time;
                if(t == WM.RBUTTONDOWN || t == WM.RBUTTONUP)
                {
                    mouseClickEvent.MouseButton.Data = MouseButtonList.RightButton;
                    if(t == WM.RBUTTONDOWN)
                    {
                        mouseClickEvent.Type = KeystrokeType.KeyDown;
                        //Console.WriteLine("Down");

                    }
                    else
                    {
                        TimeManager.GetTimeManager().CreateNewAction();
                        mouseClickEvent.Type = KeystrokeType.KeyUp;
                        //Console.WriteLine("Up");
                    }
                }
                else if (t == WM.LBUTTONDOWN || t == WM.LBUTTONUP) 
                {
                    mouseClickEvent.MouseButton.Data = MouseButtonList.LeftButton;
                    if (t == WM.LBUTTONDOWN)
                    {
                        mouseClickEvent.Type = KeystrokeType.KeyDown;
                        //Console.WriteLine("Down");
                    }
                    else
                    {
                        TimeManager.GetTimeManager().CreateNewAction();
                        mouseClickEvent.Type = KeystrokeType.KeyUp;
                        //Console.WriteLine("Up");
                    }
                }
                else
                {
                    return NativeMethods.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
                }

                mngr.InsertMouseClickEvent(mouseClickEvent);             
            }
            return NativeMethods.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        private void InsertMouseClickEvent(MouseClickEvent mouseClickEvent)
        {
            mouseClickEventsBuffer.Add(mouseClickEvent);
        }

        public void SessionChanged()
        {            
            mouseClicks.Clear();
            mouseData = new MouseData();
        }

        public MouseData GetMouseData()
        {
            MouseClickMaker();
            mouseData.MouseClickCount = (uint)mouseClicks.Count;                        
            return mouseData;
        }

        private void MouseClickMaker()
        {
            for (int i = 0; i < mouseClickEventsBuffer.Count; i++)
            {
                if (mouseClickEventsBuffer[i] != null)
                {
                    if (mouseClickEventsBuffer[i].Type == KeystrokeType.KeyDown)
                    {
                        MouseClick mouseClick = new MouseClick();
                        mouseClick.MouseButton = mouseClickEventsBuffer[i].MouseButton;
                        mouseClick.ButtonDown = mouseClickEventsBuffer[i].EventTime;                        
                        for (int j = i + 1; j < mouseClickEventsBuffer.Count; j++)
                        {
                            if (mouseClickEventsBuffer[j] != null)
                            {
                                if (mouseClickEventsBuffer[j].MouseButton.KeyIndex == mouseClick.MouseButton.KeyIndex)
                                {
                                    if (mouseClickEventsBuffer[j].Type == KeystrokeType.KeyUp)
                                    {
                                        mouseClick.ButtonUp = mouseClickEventsBuffer[j].EventTime;
                                        mouseData.MouseClickTotalTime += mouseClick.HoldTime;
                                        mouseClicks.Add(mouseClick);
                                        break;
                                    }
                                    else
                                    {
                                        mouseClickEventsBuffer[j] = null;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            mouseClickEventsBuffer.Clear();            
        }
    }
}
