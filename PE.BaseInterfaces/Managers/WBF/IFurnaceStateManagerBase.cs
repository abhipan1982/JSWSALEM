using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.WBF;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.WBF
{
  public interface IFurnaceStateManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DCFurnaceRawMaterials> GetFurnanceRawMaterials();
  }
}
