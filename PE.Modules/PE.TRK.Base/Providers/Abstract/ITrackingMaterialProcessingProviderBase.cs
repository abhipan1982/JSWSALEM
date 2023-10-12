using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.DC;

namespace PE.TRK.Base.Providers.Abstract
{
  public interface ITrackingMaterialProcessingProviderBase
  {
    Task<DataContractBase> ProcessScrapMessage(DCL1ScrapData message);
  }
}
