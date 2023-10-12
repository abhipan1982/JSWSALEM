using System.Threading.Tasks;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.TRK.Base.Models.Configuration.Concrete;
using PE.TRK.Base.Models.TrackingEntities.Abstract;

namespace PE.TRK.Base.Providers.Abstract
{
  public interface ITrackingMaterialCutProviderBase
  {
    Task<ITrackingInstructionDataContractBase> ProcessMaterialCutAsync(DcTrackingPointSignal signal, PEContext ctx, TrackingInstruction instruction, ITrackingInstructionDataContractBase result, TypeOfCut typeOfCut);
  }
}
