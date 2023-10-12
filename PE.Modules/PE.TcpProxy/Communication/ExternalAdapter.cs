using PE.Interfaces;
using PE.Interfaces.DC;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using SMF.Core.DC;
using PE.TcpProxy.Communication;

namespace PE.TcpProxy
{
	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
	public class ExternalAdapter : ExternalAdapterBase, ITcpProxy
	{
		#region ctor

		public ExternalAdapter(string moduleName) : base(moduleName, typeof(ITcpProxy)) { }

    #endregion

    #region Interfaces

    public async Task<bool> SendTelegram(DCExtCommonMessage message)
    {
      return await HandleExternalMethod(message, () => ExternalAdapterHandler.SwithForMessages(message));
    }   
    #endregion Interfaces




  }
}
