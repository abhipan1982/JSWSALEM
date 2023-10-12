using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseModels.DataContracts.Internal._Base;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  [DataContract]
  public class DCRemoveMaterial : IdBase<long>
  {
    private DCRemoveMaterial() { }
    public DCRemoveMaterial(long id) : base(id)
    {

    }

    [DataMember] public TrackingArea AreaCode { get; set; }


  }
}
