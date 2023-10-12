using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.MVH;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.Communication;
using SMF.Core.DC;

namespace PE.BaseInterfaces.SendOffices.SIM
{
  public interface ISimulationSendOffice
  {
    Task<SendOfficeResult<WorkOrderId>> SendL1MaterialIdRequestAsync(WorkOrderId dataToSend);

    Task<SendOfficeResult<DataContractBase>> SendL1CutDataAsync(DCL1CutData dataToSend);

    Task<SendOfficeResult<WorkOrderId>> SendL1DivisionAsync(DCL1MaterialDivision dataToSend);

    Task<SendOfficeResult<DataContractBase>> SendL1ScrapInfoToAdapterAsync(DCL1ScrapData dataToSend);

    Task<SendOfficeResult<DataContractBase>> SendL1AggregatedMeasDataToAdapterAsync(DcMeasData dataToSend);

    Task<SendOfficeResult<DataContractBase>> SendL1SampleMeasDataToAdapterAsync(DcMeasDataSample dataToSend);
  }
}
