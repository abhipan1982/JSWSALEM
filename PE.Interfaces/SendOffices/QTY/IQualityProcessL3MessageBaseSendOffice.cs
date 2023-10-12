using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.Communication;
using SMF.Core.DC;

namespace PE.BaseInterfaces.SendOffices.QTY
{
  public interface IQualityProcessL3MessageBaseSendOffice
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<SendOfficeResult<DataContractBase>> SendProductReportAsync(DCRawMaterial message);
  }
}
