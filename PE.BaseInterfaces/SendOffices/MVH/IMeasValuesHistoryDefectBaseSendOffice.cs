using PE.BaseModels.DataContracts.Internal.L1A;
using SMF.Core.Communication;
using System.Threading.Tasks;

namespace PE.BaseInterfaces.SendOffices.MVH
{
  public interface IConsumptionMeasurementsSendOfficeBase
  {
    Task<SendOfficeResult<DcMeasurementResponse>> GetMeasurementsAsync(DcMeasurementRequest data);
  }
}
