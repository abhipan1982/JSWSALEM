using System.Threading.Tasks;
using SMF.Core.Communication;
using SMF.Core.DC;

namespace PE.BaseInterfaces.SendOffices.QEX
{
  public interface IQualityExpertBaseSendOffice
  {
    Task<SendOfficeResult<DataContractBase>> UpdateRuleEngineStatus(DCIntElementStatusUpdate elementStatus);

    Task<SendOfficeResult<DataContractBase>> LastMaterialPositionRequestMessageAsync(DataContractBase message);
  }
}
