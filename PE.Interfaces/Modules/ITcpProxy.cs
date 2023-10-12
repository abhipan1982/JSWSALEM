using SMF.Core.DC;
using SMF.Core.Interfaces;
using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseInterfaces.Modules;

namespace PE.Interfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface ITcpProxy : ITcpProxyBase
  {
    
  }
}
