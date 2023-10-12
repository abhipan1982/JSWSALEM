using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.DBA
{
  public class DCL2L3ProductReport : DataContractBase
  {
    /// <summary>
    ///   Primary Key in transfer table
    /// </summary>
    [DataMember]
    public long Counter { get; set; }

    /// <summary>
    ///   Communication status, final result of message processing
    ///   0 - New entry, no validation errors
    ///   1 - Entry currently processed by system
    ///   2 - Entry processed by system, no errors
    ///   -1 - Validation errors(DB validation)
    ///   -2 - Entry processed by system, but processing errors occurred
    /// </summary>
    [DataMember]
    public CommStatus CommStatus { get; set; }

    /// <summary>
    ///   Name of the shift that was active when first material reached “Delay Point”
    /// </summary>
    [DataMember]
    public string ShiftName { get; set; }

    /// <summary>
    ///   Unique work order name
    /// </summary>
    [DataMember]
    public string WorkOrderName { get; set; }

    /// <summary>
    ///   Steelgrade code
    /// </summary>
    [DataMember]
    public string SteelgradeCode { get; set; }

    /// <summary>
    ///   Heat name
    /// </summary>
    [DataMember]
    public string HeatName { get; set; }

    /// <summary>
    ///   Product sequence number in Work Order
    /// </summary>
    [DataMember]
    public short SequenceInWorkOrder { get; set; }

    /// <summary>
    ///   L2 product name
    /// </summary>
    [DataMember]
    public string ProductName { get; set; }

    /// <summary>
    ///   Type of product
    ///   B - Bundle (Bar)
    ///   C - Coil (Wire rod)
    ///   G - Bar in Coil (Garret)
    /// </summary>
    [DataMember]
    public string ProductType { get; set; }

    /// <summary>
    ///   Product weight form weighing machine
    ///   [mm]
    /// </summary>
    [DataMember]
    public double OutputWeight { get; set; }

    /// <summary>
    ///   Product width from product catalogue
    ///   [mm]
    /// </summary>
    [DataMember]
    public double OutputWidth { get; set; }

    /// <summary>
    ///   Product thickness from Product Catalogue
    ///   [mm]
    /// </summary>
    [DataMember]
    public double OutputThickness { get; set; }

    /// <summary>
    ///   Number of pieces in product
    /// </summary>
    [DataMember]
    public short OutputPieces { get; set; }

    /// <summary>
    ///   Result of quality inspection
    ///   0 - Undefined
    ///   1 - Good
    ///   2 - Bad
    ///   2 - Doubtful
    /// </summary>
    [DataMember]
    public short InspectionResult { get; set; }
  }
}
