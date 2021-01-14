using KDACore.Enums;
using KDACore.Interfaces;
using KDACore.Helpers;
using KDACore.Managers;
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

namespace KDACore.StateControllers
{    
    public class KeystrokeStateController : StateController
    {        
        private static readonly KeystrokeStateController _instance = new KeystrokeStateController();
        NativeMethods.HookProc callback = KeystrokesManager.CallbackFunction;

        private KeystrokeStateController()
        {
        }

        public static KeystrokeStateController GetStateController()
        {
            return _instance;
        }

        protected override void TerminateTask()
        {
            cts.Cancel();
            cts = new CancellationTokenSource();
            KeystrokesManager.GetKeyStrokesManager().GetKeyboardData();
        }
        protected override void RunTask(CancellationToken cancellationToken)
        {
            var module = Process.GetCurrentProcess().MainModule.ModuleName;
            var moduleHandle = NativeMethods.GetModuleHandle(module);
            hHook = NativeMethods.SetWindowsHookEx(HookType.WH_KEYBOARD_LL, callback, moduleHandle, 0);
            try
            {
                isRunning = true;
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
                isRunning = false;
                return;
            }  
        }
    }
}
