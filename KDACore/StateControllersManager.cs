using KDACore.Interfaces;
using KDACore.StateControllers;
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
        List<IStateController> controllers = new List<IStateController>();
        private StateControllersManager()
        {
            controllers.Add(KeystrokeStateController.GetStateController());
            controllers.Add(MouseStateController.GetStateController());
            //KeystrokeStateController.GetStateController().Initialize(GlobalConfig.LiveDataFilePath);
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
        }
    }
}
