using System.ServiceModel;
using System.Threading.Tasks;
using PE.Models.DataContracts.Internal.MDA;
using PE.Models.DataContracts.Internal.MDB;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.Interfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IModuleABase : IBaseModule
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCAckMessage> Hello(DCHelloMessage message);
  }
}
