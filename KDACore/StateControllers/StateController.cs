﻿using KDACore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KDACore.StateControllers
{
    public abstract class StateController : IStateController
    {
        public string filePath;
        protected CancellationTokenSource cts = new CancellationTokenSource();
        protected Task loggerTask;
        protected bool isInitilized = false;
        protected bool isRunning = false;
        protected IntPtr hHook;

        protected StateController()
        {

        }
        public void Initialize(string path)
        {
            isInitilized = true;
            filePath = path;
        }

        public bool IsRunning()
        {
            return isRunning;
        }
        public void Run()
        {
            if (loggerTask == null)
            {
                loggerTask = Task.Run(() => RunTask(cts.Token));
            }
            else
            {
                if (loggerTask.Status == TaskStatus.Running)
                {
                    return;
                }
                else if (loggerTask.Status == TaskStatus.Canceled || loggerTask.Status == TaskStatus.Faulted || loggerTask.Status == TaskStatus.RanToCompletion)
                {
                    loggerTask = Task.Run(() => RunTask(cts.Token));
                }
            }
        }
        public void Stop()
        {
            if (loggerTask == null)
            {
                return;
            }
            else
            {
                if (loggerTask.Status == TaskStatus.Running)
                {
                    TerminateTask();
                }
                else if (loggerTask.Status == TaskStatus.Canceled || loggerTask.Status == TaskStatus.Faulted)
                {
                    return;
                }
            }
        }

        protected abstract void TerminateTask();
        protected abstract void RunTask(CancellationToken cancellationToken);
    }
}