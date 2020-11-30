using KDACore.Helpers;
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

        private AppManager()
        {
            //keystrokeEventsBuffer = new List<KeystrokeEvent>();
        }

        public static AppManager GetMouseManager()
        {
            return _instance;
        }

        public static void CallbackFunction(IntPtr hWinEventHook, uint evenT, IntPtr hwnd, long idObject, long idChild, uint idEventThread, uint dwmsEventTime)
        {
            if(evenT == 0x0003 || evenT == 0x800C)
            {

                StringBuilder title = new StringBuilder(256);
                NativeMethods.GetWindowText(hwnd, title, title.Capacity);
                //Console.WriteLine((Process.GetProcessById((int)NativeMethods.GetWindowThreadProcessId(hwnd, new IntPtr(1)))).ProcessName);
                Console.WriteLine(title.ToString());
            }
            

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
        }

    }
}
