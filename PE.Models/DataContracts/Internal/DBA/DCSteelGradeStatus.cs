using SMF.Core.DC;
using System.Runtime.Serialization;

namespace PE.DTO.Internal.Adapter
{
  public class DCSteelGradeStatus : DataContractBase
  {
    [DataMember]
    public long Counter { get; set; }

    //[DataMember]
    //public PE.DbEntity.Enums.CommStatus Status { get; set; }

    [DataMember]
    public string BackMessage { get; set; }
  }
}
