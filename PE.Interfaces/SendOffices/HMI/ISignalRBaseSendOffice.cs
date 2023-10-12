using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.HMI;
using SMF.Core.Communication;
using SMF.Core.DC;

namespace PE.BaseInterfaces.SendOffices.HMI
{
  public interface ISignalRBaseSendOffice
  {
    Task<SendOfficeResult<DataContractBase>> SendMaterialPositionMessageAsync(DCMaterialPosition data);
    Task<SendOfficeResult<DataContractBase>> SendCurrentLabelPrintingAsync(DCPrintingLabel data);
  }
}
