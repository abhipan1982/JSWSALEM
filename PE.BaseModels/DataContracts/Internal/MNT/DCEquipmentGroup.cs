using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.MNT
{
  public class DCEquipmentGroup : DataContractBase
  {
    [DataMember] public long EquipmentGroupId { get; set; }

    [DataMember] public string EquipmentGroupCode { get; set; }

    [DataMember] public string EquipmentGroupName { get; set; }

    [DataMember] public string Description { get; set; }
  }
}
