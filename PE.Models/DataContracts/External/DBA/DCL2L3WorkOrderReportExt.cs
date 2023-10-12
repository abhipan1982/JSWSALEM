using System;
using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseModels.DataContracts.Internal.DBA;
using SMF.Core.DC;

namespace PE.Models.DataContracts.External.DBA
{
  public class DCL2L3WorkOrderReportExt : BaseExternalTelegram
  {
    /// <summary>
    ///   Primary Key in transfer table
    /// </summary>
    [DataMember]
    public long Counter { get; set; }

    /// <summary>
    ///   Time stamp when record has been inserted to transfer table
    /// </summary>
    [DataMember]
    public DateTime CreatedTs { get; set; }

    /// <summary>
    ///   Time stamp when record has been updated in transfer table (initially null)
    /// </summary>
    [DataMember]
    public DateTime? UpdatedTs { get; set; }

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
    ///   Unique work order name
    /// </summary>
    [DataMember]
    public string WorkOrderName { get; set; }

    /// <summary>
    ///   unique product name, reference to Product Catalogue
    /// </summary>
    [DataMember]
    public string ProductName { get; set; }

    /// <summary>
    ///   total number of products (coils or bundles)
    /// </summary>
    [DataMember]
    public short ProductsNumber { get; set; }

    /// <summary>
    ///   total weight of all products
    /// </summary>
    [DataMember]
    public int TotalProductsWeight { get; set; }

    /// <summary>
    ///   total weight of all raw materials
    /// </summary>
    [DataMember]
    public int TotalRawMaterialWeight { get; set; }

    /// <summary>
    ///   number of all planned raw materials
    /// </summary>
    [DataMember]
    public short RawMaterialsPlanned { get; set; }

    /// <summary>
    ///   number of all discharged raw materials
    /// </summary>
    [DataMember]
    public short RawMaterialsDischarged { get; set; }

    /// <summary>
    ///   number of all rolled raw materials
    /// </summary>
    [DataMember]
    public short RawMaterialsRolled { get; set; }

    /// <summary>
    ///   number of all scrapped raw materials
    /// </summary>
    [DataMember]
    public short RawMaterialsScrapped { get; set; }

    /// <summary>
    ///   number of all rejected raw materials
    /// </summary>
    [DataMember]
    public short RawMaterialsRejected { get; set; }

    /// <summary>
    ///   Timestamp when first raw material has been charged to furnace
    /// </summary>
    [DataMember]
    public DateTime? WorkOrderStart { get; set; }

    /// <summary>
    ///   Timestamp when last product of the work order has been created
    /// </summary>
    [DataMember]
    public DateTime? WorkOrderEnd { get; set; }

    /// <summary>
    ///   Work order finished flag
    /// </summary>
    [DataMember]
    public bool IsWorkOrderFinished { get; set; }

    /// <summary>
    ///   unique product name, reference to Material Catalogue
    /// </summary>
    [DataMember]
    public string MaterialName { get; set; }

    /// <summary>
    ///   material width from Material Catalogue
    ///   [mm]
    /// </summary>
    [DataMember]
    public double InputWidth { get; set; }

    /// <summary>
    ///   material thickness from Material Catalogue
    ///   [mm]
    /// </summary>
    [DataMember]
    public double InputThickness { get; set; }

    /// <summary>
    ///   product width from Product Catalogue
    ///   [mm]
    /// </summary>
    [DataMember]
    public double OutputWidth { get; set; }

    /// <summary>
    ///   product thickness from Product Catalogue
    ///   [mm]
    /// </summary>
    [DataMember]
    public double OutputThickness { get; set; }

    /// <summary>
    ///   heat name
    /// </summary>
    [DataMember]
    public string HeatName { get; set; }

    /// <summary>
    ///   steelgrade code
    /// </summary>
    [DataMember]
    public string SteelgradeCode { get; set; }

    /// <summary>
    ///   weight of order to be delivered to customer (from L3)
    ///   [kg]
    /// </summary>
    [DataMember]
    public double TargetWorkOrderWeight { get; set; }

    /// <summary>
    ///   number of allcharged raw materials
    /// </summary>
    [DataMember]
    public short RawMaterialsCharged { get; set; }

    /// <summary>
    ///   sum of delays during Work Order
    ///   [s]
    /// </summary>
    [DataMember]
    public int DelayDuration { get; set; }

    /// <summary>
    ///   Name of the shift that was active when first material reached “Delay Point”
    /// </summary>
    [DataMember]
    public string ShiftName { get; set; }

    /// <summary>
    ///   Name of the crew that was active when first material reached “Delay Point”
    /// </summary>
    [DataMember]
    public string CrewName { get; set; }

    /// <summary>
    ///   Number of pieces created from rejected pieces on location 1
    /// </summary>
    [DataMember]
    public short NumberOfPiecesRejectedInLocation1 { get; set; }

    /// <summary>
    ///   Weight of pieces created from rejected pieces on location 1
    ///   [kg]
    /// </summary>
    [DataMember]
    public double WeightOfPiecesRejectedInLocation1 { get; set; }


    public override DataContractBase ToInternal(int? telId = null)
    {
      return new DCL2L3WorkOrderReport
      {
        CommStatus = CommStatus,
        Counter = Counter,
        CreatedTs = CreatedTs,
        WorkOrderEndTs = WorkOrderEnd,
        WorkOrderStartTs = WorkOrderStart,
        ProductName = ProductName,
        ProductsNumber = ProductsNumber,
        RawMaterialsDischarged = RawMaterialsDischarged,
        RawMaterialsPlanned = RawMaterialsPlanned,
        RawMaterialsRejected = RawMaterialsRejected,
        RawMaterialsRolled = RawMaterialsRolled,
        RawMaterialsScrapped = RawMaterialsScrapped,
        TotalProductsWeight = TotalProductsWeight,
        TotalRawMaterialWeight = TotalRawMaterialWeight,
        UpdatedTs = UpdatedTs,
        WorkOrderName = WorkOrderName
      };
    }

    public override int ToExternal(DataContractBase dc)
    {
      DCL2L3WorkOrderReport internalDc = (DCL2L3WorkOrderReport)dc;

      CommStatus = internalDc.CommStatus;
      CreatedTs = internalDc.CreatedTs;
      UpdatedTs = internalDc.UpdatedTs;
      WorkOrderName = internalDc.WorkOrderName;
      IsWorkOrderFinished = internalDc.IsWorkOrderFinished;
      MaterialName = internalDc.MaterialName;
      InputWidth = internalDc.InputWidth;
      InputThickness = internalDc.InputThickness;
      ProductName = internalDc.ProductName;
      OutputWidth = internalDc.OutputWidth;
      OutputThickness = internalDc.OutputThickness;
      HeatName = internalDc.HeatName;
      SteelgradeCode = internalDc.SteelgradeCode;
      TargetWorkOrderWeight = internalDc.TargetWorkOrderWeight;
      ProductsNumber = internalDc.ProductsNumber;
      TotalProductsWeight = internalDc.TotalProductsWeight;
      TotalRawMaterialWeight = internalDc.TotalRawMaterialWeight;
      NumberOfPiecesRejectedInLocation1 = internalDc.NumberOfPiecesRejectedInLocation1;
      WeightOfPiecesRejectedInLocation1 = internalDc.WeightOfPiecesRejectedInLocation1;
      RawMaterialsPlanned = internalDc.RawMaterialsPlanned;
      RawMaterialsCharged = internalDc.RawMaterialsCharged;
      RawMaterialsDischarged = internalDc.RawMaterialsDischarged;
      RawMaterialsRolled = internalDc.RawMaterialsRolled;
      RawMaterialsScrapped = internalDc.RawMaterialsScrapped;
      RawMaterialsRejected = internalDc.RawMaterialsRejected;
      WorkOrderStart = internalDc.WorkOrderStartTs;
      WorkOrderEnd = internalDc.WorkOrderEndTs;
      DelayDuration = internalDc.DelayDuration;
      ShiftName = internalDc.ShiftName;
      CrewName = internalDc.CrewName;

      return 0;
    }
  }
}
