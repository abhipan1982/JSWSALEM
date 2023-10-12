using System.Data;
using PE.BaseInterfaces.Managers.HMI;
using PE.BaseModels.DataContracts.Internal.HMI;
using PE.HMI.Base.Module.Communication;

namespace PE.HMI.Communication
{
  public class ExternalAdapterHandler : ModuleBaseExternalAdapterHandler
  {
    public ExternalAdapterHandler(IHmiBaseManager hmiManager) : base(hmiManager)
    {      
    }
  }
}
