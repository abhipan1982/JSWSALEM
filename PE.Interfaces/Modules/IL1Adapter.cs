using System.ServiceModel;
using PE.BaseInterfaces.Modules;

namespace PE.Interfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IL1Adapter : IL1AdapterBase
  {
  }
}
