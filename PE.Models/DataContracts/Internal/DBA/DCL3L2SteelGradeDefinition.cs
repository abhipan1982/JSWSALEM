using SMF.Core.DC;
using System;
using System.Runtime.Serialization;

namespace PE.DTO.Internal.Adapter
{
  public class DCL3L2SteelGradeDefinition : DataContractBase
  {
    /// <summary>
    /// Primary Key in transfer table
    /// </summary>
    [DataMember]
    public long Counter { get; set; }

    /// <summary>
    /// Time stamp when record has been inserted to transfer table
    /// </summary>
    [DataMember]
    public DateTime CreatedTs { get; set; }

    /// <summary>
    /// Time stamp when record has been updated in transfer table (initially null)
    /// </summary>
    [DataMember]
    public DateTime? UpdatedTs { get; set; }

    /// <summary>
    /// 0 - new record. 1 - processing, 2 - processed and OK, -2 - processed and Error
    /// </summary>
    //[DataMember]
    //public PE.DbEntity.Enums.CommStatus CommStatus { get; set; }

    /// <summary>
    /// Unique steel grade code
    /// </summary>
    [DataMember]
    public string SteelgradeCode { get; set; }

    /// <summary>
    /// Steel grade name
    /// </summary>
    [DataMember]
    public string SteelgradeName { get; set; }

    /// <summary>
    /// Scrap group code
    /// </summary>
    [DataMember]
    public string ScrapGroupCode { get; set; }
  }
}
