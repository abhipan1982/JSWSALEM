using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.EVT;
using PE.BaseModels.DataContracts.Internal.PRF;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.EVT
{
  public interface IShiftManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateShiftCalendarElement(DCShiftCalendarElement dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DCShiftCalendarId> InsertShiftCalendar(DCShiftCalendarElement dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteShiftCalendarElement(DCShiftCalendarId dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> GenerateShiftCalendarForNextWeek(DataContractBase dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<(bool ShiftEnded, long? EndedShiftId)> CheckCurrentShiftIsEnded();

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculcateShiftEndKPIs(DCCalculateKPI dc);
  }
}
