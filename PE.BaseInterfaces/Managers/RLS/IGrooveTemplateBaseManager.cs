using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.RLS;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.RLS
{
  public interface IGrooveTemplateBaseManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> InsertGrooveTemplateAsync(DCGrooveTemplateData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateGrooveTemplateAsync(DCGrooveTemplateData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteGrooveTemplateAsync(DCGrooveTemplateData dc);
  }
}
