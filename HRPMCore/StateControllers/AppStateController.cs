using HRPMCore.Helpers;
using HRPMCore.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRPMCore.StateControllers
{
    //public class AppStateController : StateController
    //{
    //    private static readonly AppStateController _instance = new AppStateController();        
    //    private AppStateController()
    //    {
    //    }

    //    public static AppStateController GetStateController()
    //    {
    //        return _instance;
    //    }
    //    protected override void TerminateTask()
    //    {
    //        cts.Cancel();
    //        cts = new CancellationTokenSource();
    //    }
    //    protected override void RunTask(CancellationToken cancellationToken)
    //    {
    //        var module = Process.GetCurrentProcess().MainModule.ModuleName;
    //        var moduleHandle = NativeMethods.GetModuleHandle(module);
    //        hHook = NativeMethods.SetWindowsHookEx(HookType.WH_CBT,callback,moduleHandle,0);                        
    //        try
    //        {
    //            isRunning = true;
    //            while (true)
    //            {
    //                NativeMethods.PeekMessage(IntPtr.Zero, IntPtr.Zero, (uint)WM.MOUSEFIRST, (uint)WM.MOUSELAST, 0);
    //                Thread.Sleep(5);
    //                cancellationToken.ThrowIfCancellationRequested();
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            NativeMethods.UnhookWinEvent(hHook);
    //            isRunning = false;
    //            return;
    //        }
    //    }
    //}
}
