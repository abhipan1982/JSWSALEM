using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  [DataContract]
  public class DCMaterialsCountResult : DataContractBase
  {
    [DataMember] public LayerStatus LayerStatus { get; set; }

    [DataMember] public int MaterialsCount { get; set; }
  }
}
