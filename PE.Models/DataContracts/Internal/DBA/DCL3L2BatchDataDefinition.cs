using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.EnumClasses;
using System.Reflection;
using PE.DbEntity.TransferModels;
using PE.BaseDbEntity.EnumClasses;
//using PE.DbEntity.TransferModels;

namespace PE.Models.DataContracts.Internal.DBA
{
  public class DCL3L2BatchDataDefinition : DataContractBase
  {

    /// <summary>
    ///   Primary Key in transfer table
    /// </summary>
    [DataMember]
    public long Counter { get; set; }

    /// <summary>
    ///   0 - new record, 1 - processed and OK, 2 - processed and Error, 3 - processed and rejected
    /// </summary>
    [DataMember]
    public CommStatus CommStatus { get; set; }

    /// <summary>
    ///   Unique Batch No.
    /// </summary>
    [DataMember]
    public string BatchNo { get; set; }

    /// <summary>
    ///   Production Order No.
    /// </summary>
    [DataMember]
    public string PONo { get; set; }

    /// <summary>
    ///   Heat name
    /// </summary>
    [DataMember]
    public string HeatName { get; set; }

    /// <summary>
    ///   Customer name
    /// </summary>
    [DataMember]
    public string CustomerName { get; set; }       

    [DataMember] public DateTime CreatedTs { get; set; }

    [DataMember] public float  InputThickness { get; set; }
    [DataMember] public float  InputWidth { get; set; }
    [DataMember] public string InputShapeSymbol { get; set; }
    [DataMember] public float  BloomWeight { get; set; }
    [DataMember] public float  BloomLength { get; set; }
    [DataMember] public float  OutputThickness { get; set; }
    [DataMember] public float  OutputWidth { get; set; }
    [DataMember] public string OutputShapeSymbol { get; set; }
    [DataMember] public float  S_SIDE_TOL_MM_NEG { get; set; }
    [DataMember] public float  S_SIDE_TOL_MM_POS { get; set; }
    [DataMember] public float  S_OUT_OF_SQUARNESS_MM_MIN { get; set; }
    [DataMember] public float  S_OUT_OF_SQUARNESS_MM_MAX { get; set; }
    [DataMember] public float  S_DIA_TOL_MM_LOWER { get; set; }
    [DataMember] public float  S_DIA_TOL_MM_UPPER { get; set; }
    [DataMember] public float  S_OVALITY_MM_MIN { get; set; }
    [DataMember] public float  S_OVALITY_MM_MAX { get; set; }
    [DataMember] public float  S_LENGTH_MM_MIN { get; set; }
    [DataMember] public float  S_LENGTH_MM_MAX { get; set; }
    [DataMember] public float  S_MULTIPLE_LENGTH_MM { get; set; }
    [DataMember] public float  S_LENGTH { get; set; }
    [DataMember] public int    CHARGE_TYPE { get; set; }
    [DataMember] public int    COOL_TYPE { get; set; }
    [DataMember] public int    LIFT_TYPE { get; set; }
  }
}
