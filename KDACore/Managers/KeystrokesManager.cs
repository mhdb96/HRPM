using KDACore.Enums;
using KDACore.Helpers;
using KDACore.Models;
using KDACore.StateControllers;
using KDASharedLibrary.DataAccess;
using KDASharedLibrary.Enums;
using KDASharedLibrary.Helpers;
using KDASharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KDACore.Managers
{
    public class KeystrokesManager
    {
        private static readonly KeystrokesManager _instance = new KeystrokesManager();
        private List<Keystroke> keystrokes = new List<Keystroke>();
        private List<KeystrokeEvent> keystrokeEventsBuffer;
        private KeystrokeStateController controller;
        private short[] uniqueKeyCount = new short[FileHelper.GetEnumCount<KeysList>()];
        KeyboardData keyboardData = new KeyboardData();


        public static string lastTitle = "";


        private KeystrokesManager()
        {
            controller = KeystrokeStateController.GetStateController();
            keystrokeEventsBuffer = new List<KeystrokeEvent>();
        }

        public static KeystrokesManager GetKeyStrokesManager()
        {
            return _instance;
        }

        public static IntPtr CallbackFunction(Int32 code, IntPtr wParam, IntPtr lParam)
        {
            WM eventType = (WM)wParam;
            if (code >= 0 && (eventType == WM.KEYDOWN || eventType == WM.KEYUP || eventType == WM.SYSKEYDOWN || eventType == WM.SYSKEYUP))
            {
                //Task.Run(async() => { await Task.Run(() => { AppManager.GetAppManager().CheckIfSessionChanged(); }); });
                AppManager.GetAppManager().CheckIfSessionChanged();

                //var logMngr = GetKeyStrokesManager();
                //IntPtr hWindow = NativeMethods.GetForegroundWindow();
                //StringBuilder title = new StringBuilder(256);
                //NativeMethods.GetWindowText(hWindow, title, title.Capacity);
                //if (title.ToString() != lastTitle)
                //{
                //    lastTitle = title.ToString();
                //    logMngr.WindowChanged();
                //}
                //KBDLLHOOKSTRUCT keyData = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                //var vKey = Marshal.ReadInt32(lParam);
                //string btnStatus = "";
                //KeystrokeEvent keystrokeEvent = new KeystrokeEvent();
                //keystrokeEvent.EventTime = keyData.time;
                //Keys defaultKeysEnum = (Keys)vKey;
                //var key = KeyMapper.GetKeyEnum(defaultKeysEnum);
                //if (key == KeysList.NoKey)
                //{
                //    return NativeMethods.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
                //}
                //keystrokeEvent.Key.Data = key;                
                //if (eventType == WM.KEYDOWN || eventType == WM.SYSKEYDOWN)
                //{
                //    keystrokeEvent.Type = KeystrokeType.KeyDown;
                //    btnStatus = "Down";
                //}
                //else if (eventType == WM.KEYUP || eventType == WM.SYSKEYUP)
                //{
                //    keystrokeEvent.Type = KeystrokeType.KeyUp;
                //    btnStatus = "Up  ";
                //}
                //logMngr.InsertKeystrokeEvent(keystrokeEvent);
                //if (btnStatus == "Up  " && logMngr.keystrokeEventsBuffer.Count > 30)
                //{
                //    logMngr.KeystrokeMaker();
                //}
                //Console.WriteLine($"{btnStatus},{keyData.time},{defaultKeysEnum.GetDescription()}, {key.GetDescription()}");
                //Trace.WriteLine($"{btnStatus},{DateTime.Now.Ticks},{key.GetDescription()}");
            }
            return NativeMethods.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        private void InsertKeystrokeEvent(KeystrokeEvent key)
        {
            keystrokeEventsBuffer.Add(key);
        }

        private void WindowChanged()
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
            keyboardData.StrokesCount = keystrokes.Count;
            keyboardData.BackspaceStrokesCount = uniqueKeyCount[(int)KeysList.Back];
            for (int i = 0; i < uniqueKeyCount.Length; i++)
            {
                if(uniqueKeyCount[i] > 0)
                {
                    keyboardData.UniqueKeysCount++;
                }
            }
            BinaryConnector.StaticSave(controller.GetKeyStrokesData(), controller.filePath);
            uniqueKeyCount = new short[FileHelper.GetEnumCount<KeysList>()];
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
                        uniqueKeyCount[keystroke.Key.KeyIndex]++;                        
                        for (int j = i + 1; j < keystrokeEventsBuffer.Count; j++)
                        {
                            if (keystrokeEventsBuffer[j] != null)
                            {
                                if (keystrokeEventsBuffer[j].Key.KeyIndex == keystroke.Key.KeyIndex)
                                {
                                    if (keystrokeEventsBuffer[j].Type == KeystrokeType.KeyUp)
                                    {
                                        keystroke.KeyUp = keystrokeEventsBuffer[j].EventTime;
                                        keyboardData.StrokeHoldTimes += keystroke.HoldTime;
                                        keystrokes.Add(keystroke);
                                        break;
                                    }
                                    else
                                    {
                                        keystrokeEventsBuffer[j] = null;
                                    }
                                }
                            }
                        }                        
                    }
                }
            }
            keystrokeEventsBuffer.Clear();
            Console.WriteLine(keystrokes.Count);
        }
    }
}
