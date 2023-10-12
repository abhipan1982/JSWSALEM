using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.STP
{
  public class DCSetupCompare : DataContractBase
  {
    [DataMember] public long WorkOrder1Id { get; set; }

    [DataMember] public long WorkOrder2Id { get; set; }
  }

  public class DCSetupCompareResult : DataContractBase
  {
    [DataMember] public SetupCompareResult Result { get; set; }
  }

  public enum SetupCompareResult : short
  {
    Different = 0,
    OptionalDifferent = 1,
    Matching = 2
  }
}
