using PE.DTO.External;
using PE.DTO.External.DBAdapter;
using PE.DTO.External.MVHistory;
using PE.DTO.External.Setup;
using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.HMI;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.Setup;
using PE.DTO.Internal.Tracking;
using PE.Interfaces;
using PE.Interfaces.Interfaces.Custom;
using PE.Interfaces.Interfaces.Lite;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Lite;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace PE.ADP.Adapter.Communication
{
  public class SendOffice : ModuleSendOfficeBase, IAdapterL3SendOffice, IAdapterL1SendOffice, IAdapterTcpTelegramCommunicationSendOffice
  {
    #region L3 communication

    public Task<SendOfficeResult<DCWorkOrderStatus>> SendWorkOrderDataAsync(DCL3L2WorkOrderDefinition dataToSend)
    {
      string targetModuleName =  PE.Core.Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.ProcessWorkOrderDataAsync(dataToSend));
    }


    #endregion
    #region L1 communication
    public Task<SendOfficeResult<DCPEBasId>> SendL1DivisionMessageAsync(DCL1MaterialDivision dataToSend)
    {
      string targetModuleName =  PE.Core.Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleModuleSendMethod(targetModuleName, () => client.ProcessDivisionMaterialMessageAsync(dataToSend));
    }
    #endregion
    #region TCP telegram sending
    public Task<SendOfficeResult<DataContractBase>> SendTcpTelegramAsync(DataContractBase dataToSend)
    {
      string targetModuleName =  PE.Core.Modules.TcpProxy.Name;
      ITcpProxy client = InterfaceHelper.GetFactoryChannel<ITcpProxy>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.SendTelegramAsync(dataToSend));
    }
    #endregion
    #region L1 Send

    public Task<SendOfficeResult<DCDefaultExt>> SendSetupDataToL1TCPAsync(DCTCPSetpointTelegramEnvelopeExt dataToSend)
    {
      string targetModuleName =  PE.Core.Modules.TcpProxy.Name;
      ITcpProxy client = InterfaceHelper.GetFactoryChannel<ITcpProxy>(targetModuleName);


      return HandleModuleSendMethod(targetModuleName, () => client.SendTelegramSetupDataAsync(dataToSend));
    }

    #endregion
  }
}
