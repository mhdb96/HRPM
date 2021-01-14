using KDACore.Interfaces;
using KDACore.Managers;
using KDACore.StateControllers;
using KDASharedLibrary.Models;
using KDAUILibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KDACore
{
    public class StateControllersManager
    {
        private static readonly StateControllersManager _instance = new StateControllersManager();
        private AppManager mngr = AppManager.GetAppManager();
        List<IStateController> controllers = new List<IStateController>();
        private StateControllersManager()
        {
            controllers.Add(KeystrokeStateController.GetStateController());
            controllers.Add(MouseStateController.GetStateController());            
        }

        public static StateControllersManager GetStateController()
        {
            return _instance;
        }
        public void Run()
        {
            foreach (var controller in controllers)
            {
                controller.Run();
            }
        }
        public void Stop()
        {
            foreach (var controller in controllers)
            {
                controller.Stop();
            }
            mngr.SaveSessionData(mngr.GetLastSession());
        }

        public List<AppSession> GetSessions()
        {            
            mngr.SaveSessionData(mngr.GetLastSession());
            return mngr.GetAllSessions();
        }

        public void Initilize(string path)
        {
            mngr.Initialize(path);
        }

        public void ClearData()
        {
            mngr.ClearSessions();
        }


    }
}
