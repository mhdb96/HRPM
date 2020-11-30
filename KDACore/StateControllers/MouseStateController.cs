using KDACore.Helpers;
using KDACore.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KDACore.StateControllers
{
    public class MouseStateController : StateController
    {
        private static readonly MouseStateController _instance = new MouseStateController();
        NativeMethods.HookProc callback = MouseManager.CallbackFunction;
        private MouseStateController()
        {
        }

        public static MouseStateController GetStateController()
        {
            return _instance;
        }
        protected override void TerminateTask()
        {
            cts.Cancel();
            cts = new CancellationTokenSource();
            KeystrokesManager.GetKeyStrokesManager().SaveKeystrokeData();
        }
        protected override void RunTask(CancellationToken cancellationToken)
        {
            var module = Process.GetCurrentProcess().MainModule.ModuleName;
            var moduleHandle = NativeMethods.GetModuleHandle(module);
            hHook = NativeMethods.SetWindowsHookEx(HookType.WH_MOUSE_LL, callback, moduleHandle, 0);
            try
            {
                isRunning = true;
                while (true)
                {
                    NativeMethods.PeekMessage(IntPtr.Zero, IntPtr.Zero, (uint)WM.MOUSEFIRST, (uint)WM.MOUSELAST, 0);
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
