using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseModels.DataContracts.Internal._Base;

namespace PE.BaseModels.DataContracts.Internal.ZPC
{
  [DataContract]
  public class DCZebraRequest : IdBase<long>
  {
    [DataMember]
    public string ZebraTemplateCode { get; set; }

    [DataMember]
    public int TrackingArea { get; set; }
  }
}
