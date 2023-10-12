using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.TcpProxy
{
	public class Program
	{

		#region members
		#endregion

		#region setup

		private static void Main(string[] args)
		{
			ModuleController.StartModule(ModuleInit, TcpProxyApp.Init, "TcpProxy");
		}
		private static void ModuleInit()
		{
			ModuleController.ExternalAdapter = new ExternalAdapter(ModuleController.ModuleName);

			// events
			ModuleController.ModuleStarted += ModuleController_ModuleStarted;
			ModuleController.ModuleInitialized += ModuleController_ModuleInitialized;
			ModuleController.ModuleClosed += ModuleController_ModuleClosed;
			ModuleController.ParametersChanged += ModuleController_ParametersChanged;
		}
		//private static void ModuleImplementationInit()
		//{
		//		ModuleImplementation.Init();
		//}

		#endregion setup

		#region module events

		private static void ModuleController_ModuleClosed(object sender, ModuleCloseEventArgs e)
		{
			ModuleController.Logger.Info("Module Closed");
		}
		private static void ModuleController_ModuleInitialized(object sender, ModuleInitEventArgs e)
		{
			ModuleController.Logger.Info("Module Initialized");
		}
		private static void ModuleController_ModuleStarted(object sender, ModuleStartEventArgs e)
		{
			ModuleController.Logger.Info("Module Started");
		}
		private static void ModuleController_ParametersChanged(object sender, SMF.Module.Parameter.ParametersChangedEventArgs e)
		{
			ModuleController.Logger.Info("EVENT - test only - parameters updated");
		}


		#endregion
	}
}
