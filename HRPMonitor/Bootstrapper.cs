using Caliburn.Micro;
using HRPMCore;
using HRPMonitor.ViewModels;
using HRPMonitor.Views;
using HRPMSharedLibrary.DataAccess;
using HRPMSharedLibrary.Models;
using HRPMUILibrary;
using Squirrel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HRPMonitor
{
    public class Bootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container = new SimpleContainer();
        public User User { get; set; }
        public Bootstrapper()
        {
            CheckForMultipleProgramInstences();
            StateControllersManager.GetStateController().Initilize(GlobalConfig.LiveDataFilePath);
            CheckForUser();

#if !DEBUG
InitialSatrt();
#endif
            //CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("tr-TR");
            //CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("tr-TR");
            Initialize();
        }

        private void CheckForUser()
        {
            try
            {
                User = BinaryConnector.StaticLoad<User>(GlobalConfig.UserDataFile);
            }
            catch
            {
                User = null;
            }
        }
        private void CheckForMultipleProgramInstences()
        {
            var processesWithTheSameName = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location));
            if (processesWithTheSameName.Length > 1)
            {                
                if (processesWithTheSameName.Length == 2)
                {
                    if (processesWithTheSameName[0].MainModule.FileName == processesWithTheSameName[1].MainModule.FileName)
                    {
                        File.AppendAllText(Path.Combine(Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory), "error.txt"), $"{DateTime.Now} - error == 2");
                        Application.Current.Shutdown();
                    }
                }
                else
                {
                    File.AppendAllText(Path.Combine(Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory), "error.txt"), $"{DateTime.Now} - error > 2");
                    Application.Current.Shutdown();
                }
            }
        }
        private void InitialSatrt()
        {
            using (var mgr = new UpdateManager("https://github.com/mhdb96/KDAnalyzer"))
            {
                SquirrelAwareApp.HandleEvents(
                  onInitialInstall: v => 
                  {
                      mgr.CreateShortcutForThisExe();
                      mgr.CreateRunAtWindowsStartupRegistry();
                  },
                  onAppUpdate: v => { 
                      mgr.CreateShortcutForThisExe();
                      mgr.CreateRunAtWindowsStartupRegistry();
                  },
                  onAppUninstall: v => 
                  {
                      mgr.RemoveShortcutForThisExe();
                      mgr.RemoveRunAtWindowsStartupRegistry();
                  });

            }
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            base.OnStartup(sender, e);
            DisplayRootViewFor<ShellViewModel>();
        }
        protected override void Configure()
        {
            _container.RegisterInstance(typeof(User), null, User);
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.RegisterPerRequest(typeof(ShellViewModel), null, typeof(ShellViewModel));
            _container.RegisterPerRequest(typeof(ShellView), null, typeof(ShellView));            
        }
        protected override object GetInstance(Type serviceType, string key)
        {
            return _container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
