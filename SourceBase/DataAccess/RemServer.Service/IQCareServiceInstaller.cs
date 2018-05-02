using System;
using System.Collections;
using System.Configuration.Install;
using System.ServiceProcess;
using System.ComponentModel;
using RemServer.Service;


namespace RemServer.Service
{
    [RunInstaller(true)]
    public class IQCareServiceInstaller : Installer 
    {
        private ServiceInstaller serviceInstaller;
		private ServiceProcessInstaller processInstaller;

		public IQCareServiceInstaller() 
		{
			processInstaller = new ServiceProcessInstaller();
			serviceInstaller = new ServiceInstaller();

			processInstaller.Account = ServiceAccount.LocalSystem;
			serviceInstaller.StartType = ServiceStartMode.Automatic;
			serviceInstaller.ServiceName = "IQCare Service";
            serviceInstaller.Description = "This service controls the Server Operations of Futures Group PMM System.";

			Installers.Add(serviceInstaller);
			Installers.Add(processInstaller);
		}
    }
}
