using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.QEX;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.QEX
{
  public interface IQualityExpertBaseManager
  {
    Task Init();
    Task<DataContractBase> UpdateSignalDefinitionsAsync(DataContractBase message);
    Task<DataContractBase> ProcessMaterialAreaExitEventAsync(DCAreaRawMaterial message);
    Task CheckForNewVersion();
    Task<DataContractBase> ForceRatingValueAsync(DCRatingForce message);
    Task<DataContractBase> ToggleCompensationAsync(DCCompensationTrigger message);
    Task<DataContractBase> CalculateMaterialGrading(DCRawMaterial message);
  }
}
