using HRPMCore.Enums;
using HRPMCore.Helpers;
using HRPMCore.Models;
using HRPMCore.StateControllers;
using HRPMSharedLibrary.DataAccess;
using HRPMSharedLibrary.Enums;
using HRPMSharedLibrary.Helpers;
using HRPMSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HRPMCore.Managers
{
    public class KeystrokesManager
    {
        private static readonly KeystrokesManager _instance = new KeystrokesManager();
        private List<Keystroke> keystrokes = new List<Keystroke>();
        private List<KeystrokeEvent> keystrokeEventsBuffer;
        private KeystrokeStateController controller;
        private short[] uniqueKeyCount = new short[FileHelper.GetEnumCount<KeysList>()];
        KeyboardData keyboardData = new KeyboardData();


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
                var logMngr = GetKeyStrokesManager();
                if (AppManager.GetAppManager().IsBusy == true)
                {                    
                    return NativeMethods.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
                }
                AppManager.GetAppManager().CheckIfSessionChanged();                
                KBDLLHOOKSTRUCT keyData = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                var vKey = keyData.vkCode;
                KeystrokeEvent keystrokeEvent = new KeystrokeEvent();
                keystrokeEvent.EventTime = keyData.time;
                Keys defaultKeysEnum = (Keys)vKey;
                var key = KeyMapper.GetKeyEnum(defaultKeysEnum);
                if (key == KeysList.NoKey)
                {
                    return NativeMethods.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
                }
                keystrokeEvent.Key.Data = key;
                if (eventType == WM.KEYDOWN || eventType == WM.SYSKEYDOWN)
                {
                    keystrokeEvent.Type = KeystrokeType.KeyDown;
                    //Console.WriteLine("Down");
                }
                else if (eventType == WM.KEYUP || eventType == WM.SYSKEYUP)
                {
                    TimeManager.GetTimeManager().CreateNewAction();
                    keystrokeEvent.Type = KeystrokeType.KeyUp;
                    //Console.WriteLine("Up");
                }
                logMngr.InsertKeystrokeEvent(keystrokeEvent);
                //Console.WriteLine($"{btnStatus},{keyData.time},{defaultKeysEnum.GetDescription()}, {key.GetDescription()}");
            }
            return NativeMethods.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        private void InsertKeystrokeEvent(KeystrokeEvent key)
        {
            keystrokeEventsBuffer.Add(key);
        }

        public void SessionChanged()
        {
            uniqueKeyCount = new short[FileHelper.GetEnumCount<KeysList>()];
            keystrokes.Clear();
            keyboardData = new KeyboardData();
        }

        public KeyboardData GetKeyboardData()
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
            return keyboardData;
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
            //Console.WriteLine(keystrokes.Count);
        }
    }
}
