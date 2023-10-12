using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.WBF;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.WBF
{
  public interface IFurnaceProcessManagerBase
  {
    Task Init();

    Task<DataContractBase> ProcessMaterialChangeInFurnaceEventAsync(DCFurnaceRawMaterials dc);

    Task<DataContractBase> ProcessMaterialDischargeEventAsync(DCFurnaceMaterialDischarge dc);

    Task UpdateMaterialsInFurnaceAsync();

    Task SendDischargedMeasurements();
  }
}
