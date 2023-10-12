using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.MNT;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.MNT
{
  public interface IEquipmentAccumulationBaseManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AccumulateEquipmentUsageAsync(DCEquipmentAccu message);
  }
}
