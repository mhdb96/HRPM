﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDACore.StateControllers
{
    public interface IStateController
    {        
        void Run();
        void Stop();
        bool IsRunning();
    }
}
