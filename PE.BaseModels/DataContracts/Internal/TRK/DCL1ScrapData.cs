using System;
using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseModels.DataContracts.Internal._Base;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  [DataContract]
  public class DCL1ScrapData : IdBase<long>
  {
    private DCL1ScrapData() { }

    public DCL1ScrapData(long id):base(id)
    {

    }
    /// <summary>
    /// TypeOfScrap
    /// </summary>
    [DataMember] public TypeOfScrap TypeOfScrap { get; set; }

    /// <summary>
    ///   percent of scrap
    /// </summary>
    [DataMember] public double? ScrapPercent { get; set; }

    /// <summary>
    ///   Scrap remark
    /// </summary>
    [DataMember] public string ScrapRemark { get; set; }

    [DataMember] public long? AssetId { get; set; }
  }
}
