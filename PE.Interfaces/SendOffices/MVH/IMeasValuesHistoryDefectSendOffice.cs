using PE.BaseModels.DataContracts.Internal.L1A;
using SMF.Core.Communication;
using System.Threading.Tasks;

namespace PE.Interfaces.SendOffices.MVH
{
  public interface IConsumptionMeasurementsSendOffice
  {
    Task<SendOfficeResult<DcMeasurementResponse>> GetMeasurementsAsync(DcMeasurementRequest data);
  }
}
