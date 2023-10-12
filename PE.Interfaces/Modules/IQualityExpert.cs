using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseInterfaces.Modules;
using PE.BaseModels.DataContracts.Internal.QEX;
using SMF.Core.DC;

namespace PE.Interfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IQualityExpert : IQualityExpertBase
  {
  }
}
