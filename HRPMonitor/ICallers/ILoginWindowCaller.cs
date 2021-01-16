using HRPMSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPMonitor.ICallers
{
    public interface ILoginWindowCaller
    {
        void OnLogin(User user);
    }
}
