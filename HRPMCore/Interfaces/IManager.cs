using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPMCore.Interfaces
{
    public interface IManager
    {
        IManager GetManagerInstance();
        void SaveData();
    }
}
