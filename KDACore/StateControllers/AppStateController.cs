﻿using KDACore.Helpers;
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
    public class AppStateController : StateController
    {
        private static readonly AppStateController _instance = new AppStateController();
        NativeMethods.WinEventProc callback = AppManager.CallbackFunction;
        private AppStateController()
        {
        }

        public static AppStateController GetStateController()
        {
            return _instance;
        }
        protected override void TerminateTask()
        {
            cts.Cancel();
            cts = new CancellationTokenSource();
        }
        protected override void RunTask(CancellationToken cancellationToken)
        {
            var module = Process.GetCurrentProcess().MainModule.ModuleName;
            var moduleHandle = NativeMethods.GetModuleHandle(module);
            hHook = NativeMethods.SetWinEventHook(0x00000001, 0x7FFFFFFF, IntPtr.Zero, callback, 0, 0, 0 | 2);                        
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
                NativeMethods.UnhookWinEvent(hHook);
                isRunning = false;
                return;
            }
        }
    }
}