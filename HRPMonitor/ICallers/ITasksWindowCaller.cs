using HRPMSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPMonitor.ICallers
{
    public interface ITasksWindowCaller
    {
        void TaskCreated(WorkTask workTask);
        Task TaskSaved(WorkTask workTask);
    }
}
