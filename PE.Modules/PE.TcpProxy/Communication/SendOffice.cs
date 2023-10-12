using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMF.Core.Log;
using SMF.Module;
using SMF.Module.Core;
using PE.Interfaces;
using PE.Interfaces.DC;
using System.ServiceModel;

namespace PE.TcpProxy
{
	public static class SendOffice
	{
    #region External calls

    public static async Task ForwardBytesToAdapter(DCExtCommonMessage message)
		{
      //prepare target module name and interface
      string targetModuleName = PE.Interfaces.Module.Modules.adapter.Name;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);
      
      //call method on remote module
      await SendOfficeBase.HandleSendMethod((IClientChannel)client, targetModuleName, () => client.ProcessCommonMessage(message));
		}

		#endregion
	}
}
