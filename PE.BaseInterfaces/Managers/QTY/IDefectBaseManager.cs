using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.QTY;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.QTY
{
  public interface IDefectBaseManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteDefectAsync(DCDefect defect);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> EditDefectAsync(DCDefect dc);
  }
}
