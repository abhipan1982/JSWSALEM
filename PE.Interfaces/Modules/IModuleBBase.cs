using System.ServiceModel;
using SMF.Core.Interfaces;

namespace PE.Interfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IModuleBBase : IBaseModule
  {
  }
}
