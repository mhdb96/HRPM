using KDACore.Enums;
using KDACore.Models;
using KDASharedLibrary.DataAccess;
using KDASharedLibrary.Enums;
using KDASharedLibrary.Helpers;
using KDASharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace KDACore.Logic
{
    public class KeystrokesManager
    {
        private static readonly KeystrokesManager _instance = new KeystrokesManager();
        private List<Keystroke> keystrokes = new List<Keystroke>();
        private List<KeystrokeEvent> keystrokeEventsBuffer;
        private StateController controller;
        int t = 0;


        public static string lastTitle = "";


        private KeystrokesManager()
        {
            controller = StateController.GetStateController();
            keystrokeEventsBuffer = new List<KeystrokeEvent>();
        }

        public static KeystrokesManager GetKeyStrokesManager()
        {
            return _instance;
        }

        public static IntPtr CallbackFunction(Int32 code, IntPtr wParam, IntPtr lParam)
        {
            WM t = (WM)wParam;
            if(t == WM.MOUSEWHEEL || t == WM.RBUTTONDOWN || t == WM.RBUTTONUP || t == WM.LBUTTONDOWN || t == WM.LBUTTONUP || t == WM.XBUTTONUP)
            {
                Thread.Sleep(1000);
                MSLLHOOKSTRUCT ver = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                Console.WriteLine(t.ToString() + " " + ver.mouseData +" " + ver.pt.x + " " + ver.pt.y);
            }
            
            Int32 msgType = wParam.ToInt32();
            if (code >= 0 && (msgType == 0x100 || msgType == 0x104 || msgType == 0x101))
            {
                var logMngr = GetKeyStrokesManager();
                //var t = logMngr.GetCurrentKeyboardLayout().Name;
                IntPtr hWindow = NativeMethods.GetForegroundWindow();
                StringBuilder title = new StringBuilder(256);
                NativeMethods.GetWindowText(hWindow, title, title.Capacity);
                if (title.ToString() != lastTitle)
                {
                    lastTitle = title.ToString();
                    logMngr.WindowChanged();
                }
                var vKey = Marshal.ReadInt32(lParam);
                string btnStatus = "";
                KeystrokeEvent keystrokeEvent = new KeystrokeEvent();
                keystrokeEvent.EventTime = DateTime.Now;
                Keys defaultKeysEnum = (Keys)vKey;
                var key = KeyMapper.GetKeyEnum(defaultKeysEnum);
                if (key == KeysList.NoKey)
                {
                    return NativeMethods.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
                }
                keystrokeEvent.Key.Data = key;
                if (wParam.ToInt32() == 256)
                {
                    keystrokeEvent.Type = KeystrokeType.KeyDown;
                    btnStatus = "Down";
                }
                else if (wParam.ToInt32() == 257) 
                {
                    keystrokeEvent.Type = KeystrokeType.KeyUp;
                    btnStatus = "Up  ";
                }
                logMngr.InsertKeystrokeEvent(keystrokeEvent);
                Console.WriteLine($"{btnStatus},{DateTime.Now.Ticks},{defaultKeysEnum.GetDescription()}, {key.GetDescription()}");
                //Trace.WriteLine($"{btnStatus},{DateTime.Now.Ticks},{key.GetDescription()}");
            }
            return NativeMethods.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        public void InsertKeystrokeEvent(KeystrokeEvent key)
        {
            keystrokeEventsBuffer.Add(key);
        }

        public void WindowChanged()
        {
            if (keystrokeEventsBuffer.Count == 0)
            {
                return;
            }
            else
            {
                SaveKeystrokeData();
            }
        }

        public void SaveKeystrokeData()
        {
            KeystrokeMaker();
            GetSeekTime();
            BinaryConnector.StaticSave(controller.GetKeyStrokesData(), controller.filePath);
            keystrokes.Clear();
        }

        private void KeystrokeMaker()
        {
            for (int i = 0; i < keystrokeEventsBuffer.Count; i++)
            {
                if (keystrokeEventsBuffer[i] != null)
                {
                    if (keystrokeEventsBuffer[i].Type == KeystrokeType.KeyDown)
                    {
                        Keystroke keystroke = new Keystroke();
                        keystroke.Key = keystrokeEventsBuffer[i].Key;
                        keystroke.KeyDown = keystrokeEventsBuffer[i].EventTime;
                        for (int j = i + 1; j < keystrokeEventsBuffer.Count; j++)
                        {
                            if (keystrokeEventsBuffer[j] != null)
                            {
                                if (keystrokeEventsBuffer[j].Key.KeyIndex == keystroke.Key.KeyIndex)
                                {
                                    if (keystrokeEventsBuffer[j].Type == KeystrokeType.KeyUp)
                                    {
                                        keystroke.KeyUp = keystrokeEventsBuffer[j].EventTime;
                                        break;
                                    }
                                    else
                                    {
                                        keystrokeEventsBuffer[j] = null;
                                    }
                                }
                            }
                        }
                        keystrokes.Add(keystroke);
                    }
                }
            }
            keystrokeEventsBuffer.Clear();
        }

        private void GetSeekTime()
        {
            var KeystrokesData = controller.GetKeyStrokesData();
            for (int i = 0; i < keystrokes.Count; i++)
            {
                // the pressed key 
                int to = keystrokes[i].Key.KeyIndex;

                if (KeystrokesData[to] == null)
                {
                    KeystrokesData[to] = new KeystrokeData();
                    KeystrokesData[to].Key = keystrokes[i].Key;
                }
                KeystrokesData[to].HoldTimes.Add(keystrokes[i].HoldTime);

                //skip first element for seektime
                if (i == 0)
                {
                    continue;
                }
                else
                {
                    // the key pressed before
                    int from = keystrokes[i - 1].Key.KeyIndex;

                    // -1 NoKey key index
                    if (to == -1 || from == -1)
                    {
                        continue;
                    }
                    if (KeystrokesData[to].SeekTimes[from] == null)
                    {
                        KeystrokesData[to].SeekTimes[from] = new List<ushort>();
                    }
                    ushort seekTime = (ushort)new TimeSpan(keystrokes[i].KeyDown.Ticks - keystrokes[i - 1].KeyDown.Ticks).TotalMilliseconds;
                    if (seekTime > 5000)
                    {
                        seekTime = 0;
                    }
                    KeystrokesData[to].SeekTimes[from].Add(seekTime);
                }
            }
            controller.KeystrokeData = KeystrokesData;
        }
        private CultureInfo GetCurrentKeyboardLayout()
        {
            try
            {
                IntPtr foregroundWindow = NativeMethods.GetForegroundWindow();
                uint foregroundProcess = NativeMethods.GetWindowThreadProcessId(foregroundWindow, IntPtr.Zero);
                int keyboardLayout = NativeMethods.GetKeyboardLayout(foregroundProcess).ToInt32() & 0xFFFF;
                return new CultureInfo(keyboardLayout);
            }
            catch (Exception _)
            {
                return new CultureInfo(1055); // Assume Turkish if something went wrong.
            }
        }
    }
}
