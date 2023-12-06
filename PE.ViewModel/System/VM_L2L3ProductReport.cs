using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
//using PE.BaseDbEntity.TransferModels;
using PE.DbEntity.TransferModels;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_L2L3ProductReport : VM_Base
  {
    #region ctor

    public VM_L2L3ProductReport() { }

    public VM_L2L3ProductReport(L2L3ProductReport data)
    {
      Counter = data.Counter;
      CreatedTs = data.CreatedTs;
      UpdatedTs = data.UpdatedTs;
      CommStatus = data.CommStatus;
      CommStatusName = ResxHelper.GetResxByKey((CommStatus)data.CommStatus);
      CommStatusError = data.CommStatus == BaseDbEntity.EnumClasses.CommStatus.ProcessingError.Value ||
                        data.CommStatus == BaseDbEntity.EnumClasses.CommStatus.ValidationError.Value;
      ShiftName = data.ShiftName;
      WorkOrderName = data.WorkOrderName;
      SteelgradeCode = data.SteelgradeCode;
      HeatName = data.HeatName;
      SequenceInWorkOrder = data.SequenceInWorkOrder;
      ProductName = data.ProductName;
      ProductType = data.ProductType;
      OutputWeight = data.OutputWeight;
      OutputWidth = data.OutputWidth;
      OutputThickness = data.OutputThickness;
      OutputPieces = data.OutputPieces;
      InspectionResult = data.InspectionResult;
    }

    #endregion

    #region properties

    [Editable(false)]
    [SmfDisplay(typeof(VM_L2L3ProductReport), "Counter", "NAME_CounterId")]
    public long Counter { get; set; }

    [SmfDisplay(typeof(VM_L2L3ProductReport), "CreatedTs", "NAME_CreatedTs")]
    public virtual DateTime CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_L2L3ProductReport), "UpdatedTs", "NAME_UpdatedTs")]
    public virtual DateTime UpdatedTs { get; set; }

    [SmfDisplay(typeof(VM_L2L3ProductReport), "CommStatusName", "NAME_WorkOrderNameStatus")]
    public string CommStatusName { get; set; }

    public bool CommStatusError { get; set; }

    public short? CommStatus { get; set; }

    [SmfDisplay(typeof(VM_L2L3ProductReport), "ShiftName", "NAME_Shift")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ShiftName { get; set; }

    [SmfDisplay(typeof(VM_L2L3ProductReport), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_L2L3ProductReport), "SteelgradeCode", "NAME_SteelgradeCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeCode { get; set; }

    [SmfDisplay(typeof(VM_L2L3ProductReport), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_L2L3ProductReport), "SequenceInWorkOrder", "NAME_ProductSequenceInWorkOrder")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SequenceInWorkOrder { get; set; }

    [SmfDisplay(typeof(VM_L2L3ProductReport), "ProductName", "NAME_ProductName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductName { get; set; }

    [SmfDisplay(typeof(VM_L2L3ProductReport), "ProductType", "NAME_ProductType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductType { get; set; }

    [SmfDisplay(typeof(VM_L2L3ProductReport), "OutputWeight", "NAME_OutputWeight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string OutputWeight { get; set; }

    [SmfDisplay(typeof(VM_L2L3ProductReport), "OutputWidth", "NAME_OutputWidth")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string OutputWidth { get; set; }

    [SmfDisplay(typeof(VM_L2L3ProductReport), "OutputThickness", "NAME_OutputThickness")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string OutputThickness { get; set; }

    [SmfDisplay(typeof(VM_L2L3ProductReport), "OutputPieces", "NAME_OutputPieces")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string OutputPieces { get; set; }

    [SmfDisplay(typeof(VM_L2L3ProductReport), "InspectionResult", "NAME_InspectionResult")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string InspectionResult { get; set; }

    #endregion
  }
}
