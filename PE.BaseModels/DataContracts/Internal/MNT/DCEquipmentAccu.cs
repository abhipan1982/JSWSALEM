using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.MNT
{
  public class DCEquipmentAccu : DataContractBase
  {
    [DataMember] public double MaterialWeight { get; set; }
  }
}
