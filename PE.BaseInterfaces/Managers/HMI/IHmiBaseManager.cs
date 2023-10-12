using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.HMI;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.HMI
{
  public interface IHmiBaseManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessL1MaterialPositionAsync(DCMaterialPosition dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessLastMaterialPositionRequestAsync();

    Task<DataContractBase> SendCurrentLabelPrintingAsync(DCPrintingLabel dc);
  }
}
