using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.EnumClasses;
using PE.DbEntity;
using PE.DbEntity.TransferModels;
using PE.BaseDbEntity.TransferModels;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_L2L3WorkOrderReport : VM_Base
  {
    #region properties

    [Editable(false)]
    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "CounterId", "NAME_CounterId")]
    public virtual long CounterId { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "CreatedTs", "NAME_CreatedTs")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public virtual DateTime CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "UpdatedTs", "NAME_UpdatedTs")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public virtual DateTime UpdatedTs { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "WorkOrderName", "NAME_WorkOrderName")]
    [SmfStringLength(50)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "ProductCatalogueName", "NAME_ProductName")]
    [SmfStringLength(50)]
    public string ProductCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "ProductsNumber", "NAME_ProductsNumber")]
    public short ProductsNumber { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "NumberOfProducts", "NAME_ProductsNumber")]
    public string NumberOfProducts { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "ProductsNumber", "NAME_ProductsWeight")]
    public double TotalProductsWeight { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "TotalWeightOfProducts", "NAME_ProductsWeight")]
    public string TotalWeightOfProducts { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "TotalRawMaterialWeight", "NAME_RawMaterialsWeight")]
    public double TotalRawMaterialWeight { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "TotalWeightOfMaterials", "NAME_RawMaterialsWeight")]
    public string TotalWeightOfMaterials { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "RawMaterialsPlanned", "NAME_MaterialsPlanned")]
    public short RawMaterialsPlanned { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "NumberOfPlannedMaterials", "NAME_MaterialsPlanned")]
    public string NumberOfPlannedMaterials { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "RawMaterialsDischarged", "NAME_DischargedCount")]
    public short RawMaterialsDischarged { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "RawMaterialsRolled", "NAME_RolledCount")]
    public short RawMaterialsRolled { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "NumberOfRolledMaterials", "NAME_RolledCount")]
    public string NumberOfRolledMaterials { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "RawMaterialsScrapped", "NAME_NumberOfScrappedBillets")]
    public short RawMaterialsScrapped { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "NumberOfScrappedMaterials", "NAME_NumberOfScrappedBillets")]
    public string NumberOfScrappedMaterials { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "NumberOfRejectedMaterials", "NAME_NumberOfRejectedBillets")]
    public string NumberOfRejectedMaterials { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "WorkOrderStart", "NAME_ProductionStarted")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderStart { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "WorkOrderEnd", "NAME_ProductionFinished")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderEnd { get; set; }

    [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "WorkOrderNameStatus", "NAME_WorkOrderNameStatus")]
    public virtual string CommStatusName { get; set; }

    public virtual bool CommStatusError { get; set; }

    public virtual short? CommStatus { get; set; }

    #endregion

    #region ctor


    // #region properties

    // [Editable(false)]
    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "BATCH_NO", "NAME_BATCH_NO")]
    // public virtual String BATCH_NO { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "PO_NO", "NAME_PO_NO")]
    // //[SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    // public virtual String PO_NO { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "BAR_CODE_SCAN_TIME", "NAME_BAR_CODE_SCAN_TIME")]
    // //[SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    // public virtual String BAR_CODE_SCAN_TIME { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "FUR_CHARGE_TIME", "NAME_FUR_CHARGE_TIME")]
    // [SmfStringLength(50)]
    // public string FUR_CHARGE_TIME { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "FUR_DISCHARGE_TIME", "NAME_FUR_DISCHARGE_TIME")]
    // [SmfStringLength(50)]
    // public string FUR_DISCHARGE_TIME { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "ROLLING_TIME", "NAME_ROLLING_TIME")]
    // public string ROLLING_TIME { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "NO_ROLLED_BARS", "NAME_NO_ROLLED_BARS")]
    // public int NO_ROLLED_BARS { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "CP1_OP_NAME", "NAME_CP1_OP_NAME")]
    // public string CP1_OP_NAME { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "CP1_SUP_NAME", "NAME_CP1_SUP_NAME")]
    // public string CP1_SUP_NAME { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "CP2_OP_NAME", "NAME_CP2_OP_NAME")]
    // public string CP2_OP_NAME { get; set; }


    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "CP2_HELP_NAME", "NAME_CP2_HELP_NAME")]
    // public string CP2_HELP_NAME { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "CP3_OP_NAME", "NAME_CP3_OP_NAME")]
    // public string CP3_OP_NAME { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "CP4_OP_NAME", "NAME_CP4_OP_NAME")]
    // public string CP4_OP_NAME { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "CP5_OP_NAME", "NAME_CP5_OP_NAME")]
    // public string CP5_OP_NAME { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "CP6_OP_NAME", "NAME_CP6_OP_NAME")]
    // public string CP6_OP_NAME { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "SHIFT_IN_CHARGE_NAME", "NAME_SHIFT_IN_CHARGE_NAME")]
    // public string SHIFT_IN_CHARGE_NAME { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "SUPERVISOR_NAME", "NAME_SUPERVISOR_NAME")]
    // public string SUPERVISOR_NAME { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "BLOOMS_IN_BUNDLE", "NAME_BLOOMS_IN_BUNDLE")]
    // public short BLOOMS_IN_BUNDLE { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "CHARGING_REMARKS", "NAME_CHARGING_REMARKS")]
    // public string CHARGING_REMARKS { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "DISCHARGING_REMARKS", "NAME_DISCHARGING_REMARKS")]
    // public string DISCHARGING_REMARKS { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "ROLLING_REMARKS", "NAME_ROLLING_REMARKS")]
    // public string ROLLING_REMARKS { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "COLLING_REMARKS", "NAME_COLLING_REMARKS")]
    // public string COLLING_REMARKS { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "COBBLE_AREA", "NAME_COBBLE_AREA")]
    // public string COBBLE_AREA { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "COBBLE_REASON", "NAME_COBBLE_REASON")]
    // public string COBBLE_REASON { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "BUNDLE_WGT", "NAME_BUNDLE_WGT")]
    // public float BUNDLE_WGT { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "MISROLL_PERC", "NAME_MISROLL_PERC")]
    //// [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    // public float MISROLL_PERC { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "COBBLE_PERC", "NAME_COBBLE_PERC")]
    // //[DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    // public float COBBLE_PERC { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "INDIRECT_PERC", "NAME_INDIRECT_PERC")]
    // public float INDIRECT_PERC { get; set; }

    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "CHARGING_SIDE", "NAME_CHARGING_SIDE")]
    // public string CHARGING_SIDE { get; set; }


    // [SmfDisplay(typeof(VM_L2L3WorkOrderReport), "CommMessage", "NAME_CommMessage")]
    // public string CommMessage { get; set; }


    // public virtual string ValidationCheck { get; set; }

    // public virtual short? CommStatus { get; set; }

    // #endregion

    // #region ctor










    public VM_L2L3WorkOrderReport() { }

    public VM_L2L3WorkOrderReport(DbEntity.TransferModels.L2L3WorkOrderReport data)
    {
      CounterId = data.Counter;
      CreatedTs = data.CreatedTs;
      UpdatedTs = data.UpdatedTs;
      CommStatus = data.CommStatus;
      CommStatusName = ResxHelper.GetResxByKey((CommStatus)data.CommStatus);
      CommStatusError = data.CommStatus == BaseDbEntity.EnumClasses.CommStatus.ProcessingError.Value ||
                        data.CommStatus == BaseDbEntity.EnumClasses.CommStatus.ValidationError.Value;

      WorkOrderName = data.WorkOrderName;
      ProductCatalogueName = data.ProductCatalogueName;
      NumberOfProducts = data.NumberOfProducts;
      TotalWeightOfProducts = data.TotalWeightOfProducts;
      TotalWeightOfMaterials = data.TotalWeightOfMaterials;
      NumberOfPlannedMaterials = data.NumberOfPlannedMaterials;
      NumberOfRolledMaterials = data.NumberOfRolledMaterials;
      NumberOfScrappedMaterials = data.NumberOfScrappedMaterials;
      NumberOfRejectedMaterials = data.NumberOfRejectedMaterials;
      WorkOrderStart = data.WorkOrderStart;
      WorkOrderEnd = data.WorkOrderEnd;
    }
    //public VM_L2L3WorkOrderReport(L2L3BatchReport data)
    //{
    //  BATCH_NO = data.BATCH_NO;
    //  PO_NO = data.PO_NO;
    //  BAR_CODE_SCAN_TIME = data.BAR_CODE_SCAN_TIME;
    //  FUR_CHARGE_TIME = data.FUR_CHARGE_TIME;
    //  FUR_DISCHARGE_TIME = data.FUR_DISCHARGE_TIME;
    //  ROLLING_TIME = data.ROLLING_TIME;
    //  NO_ROLLED_BARS = (short) data.NO_ROLLED_BARS;
    //  CP1_OP_NAME = data.CP1_OP_NAME;
    //  CP1_SUP_NAME = data.CP1_SUP_NAME;
    //  CP2_OP_NAME = data.CP2_OP_NAME;
    //  CP2_HELP_NAME= data.CP2_HELP_NAME;
    //  CP3_OP_NAME = data.CP3_OP_NAME;
    //  CP4_OP_NAME = data.CP4_OP_NAME;
    //  CP5_OP_NAME = data.CP5_OP_NAME;
    //  CP6_OP_NAME = data.CP6_OP_NAME;
    //  SHIFT_IN_CHARGE_NAME = data.SHIFT_IN_CHARGE_NAME;
    //  SUPERVISOR_NAME = data.SUPERVISOR_NAME;
    //  BLOOMS_IN_BUNDLE = (short) data.BLOOMS_IN_BUNDLE;
    //  CHARGING_REMARKS = data.CHARGING_REMARKS;
    //  DISCHARGING_REMARKS = data.DISCHARGING_REMARKS;
    //  ROLLING_REMARKS = data.ROLLING_REMARKS;
    //  COLLING_REMARKS= data.COLLING_REMARKS;
    //  COBBLE_AREA= data.COBBLE_AREA;
    //  COBBLE_REASON= data.COBBLE_REASON;
    //  BUNDLE_WGT= (float) data.BUNDLE_WGT;
    //  MISROLL_PERC= (float)data.MISROLL_PERC;
    //  COBBLE_PERC=  (float)data.COBBLE_PERC;
    //  INDIRECT_PERC=(float)data.INDIRECT_PERC;
    //  CHARGING_SIDE= data.CHARGING_SIDE;
    //  CommStatus = data.CommStatus;
    //  CommMessage= data.CommMessage;
    //  ValidationCheck= data.ValidationCheck;
    //}

    #endregion
  }
}
