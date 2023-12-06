using System;
using PE.DbEntity.TransferModels;
using PE.Models.DataContracts.Internal.DBA;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_L3L2WorkOrderDefinition : VM_Base
  {
    #region properties
    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "CounterId", "NAME_CounterId")]
    public long CounterId { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "CreatedTs", "NAME_CreatedTs")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public virtual DateTime CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "UpdatedTs", "NAME_UpdatedTs")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public virtual DateTime UpdatedTs { get; set; }


    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "WorkOrderName", "NAME_WorkOrderName")]
    [SmfStringLength(50)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "ExternalWorkOrderName", "NAME_ExternalWorkOrderName")]
    [SmfStringLength(50)]
    public string ExternalWorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "PreviousWorkOrderName", "NAME_PreviousWorkOrderName")]
    [SmfStringLength(50)]
    public string PreviousWorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "OrderDeadline", "NAME_OrderDeadline")]
    [SmfStringLength(8)]
    public string OrderDeadline { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "HeatName", "NAME_HeatName")]
    [SmfStringLength(50)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "NumberOfBillets", "NAME_NumberOfBillets")]
    [SmfStringLength(3)]
    public string NumberOfBillets { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "CustomerName", "NAME_CustomerName")]
    [SmfStringLength(50)]
    public string CustomerName { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "BundleWeightMin", "NAME_BundleWeightMin")]
    [SmfStringLength(10)]
    public string BundleWeightMin { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "BundleWeightMax", "NAME_BundleWeightMax")]
    [SmfStringLength(10)]
    public string BundleWeightMax { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "TargetWorkOrderWeight", "NAME_TargetWorkOrderWeight")]
    [SmfStringLength(10)]
    public string TargetWorkOrderWeight { get; set; }


    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "TargetWorkOrderWeightMin", "NAME_TargetWorkOrderWeightMin")]
    [SmfStringLength(10)]
    public string TargetWorkOrderWeightMin { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "TargetWorkOrderWeightMax", "NAME_TargetWorkOrderWeightMax")]
    [SmfStringLength(10)]
    public string TargetWorkOrderWeightMax { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "MaterialCatalogueName", "NAME_MaterialCatalogue")]
    [SmfStringLength(50)]
    public string MaterialCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "ProductCatalogueName", "NAME_ProductCatalogue")]
    [SmfStringLength(50)]
    public string ProductCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "SteelgradeCode", "NAME_SteelgradeCode")]
    [SmfStringLength(50)]
    public string SteelgradeCode { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "InputThickness", "NAME_InputThickness")]
    [SmfStringLength(10)]
    public string InputThickness { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "InputWidth", "NAME_InputWidth")]
    [SmfStringLength(10)]
    public string InputWidth { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "InputShapeSymbol", "NAME_InputShapeSymbol")]
    [SmfStringLength(10)]
    public string InputShapeSymbol { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "BilletWeight", "NAME_RawMaterialWeight")]
    [SmfStringLength(10)]
    public string BilletWeight { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "BilletLength", "NAME_InputLength")]
    [SmfStringLength(10)]
    public string BilletLength { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "OutputThickness", "NAME_OutputThickness")]
    [SmfStringLength(10)]
    public string OutputThickness { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "OutputWidth", "NAME_OutputWidth")]
    [SmfStringLength(10)]
    public string OutputWidth { get; set; }

    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "OutputShapeSymbol", "NAME_OutputShapeSymbol")]
    [SmfStringLength(10)]
    public string OutputShapeSymbol { get; set; }


    [SmfDisplay(typeof(VM_L3L2WorkOrderDefinition), "CommStatusName", "NAME_WorkOrderNameStatus")]
    public virtual String CommStatusName { get; set; }

    public virtual String ValidationCheck { get; set; }
    public virtual String CommMessage { get; set; }

    public virtual bool CommStatusError { get; set; }

    public short? CommStatus
    {
      get;
      set;
    }
    public BaseDbEntity.EnumClasses.CommStatus? CommStatusEnum
    {
      get { return CommStatus != null ? BaseDbEntity.EnumClasses.CommStatus.GetValue(CommStatus.Value) : null; }
      set { CommStatus = value?.Value; }
    }

    #endregion

    #region ctor
    public VM_L3L2WorkOrderDefinition() { }
    //public VM_L3L2WorkOrderDefinition(DbEntity.TransferModels.L3L2BatchDataDefinition x) { }

    public VM_L3L2WorkOrderDefinition(L3L2WorkOrderDefinition l3L2WorkOrderDefinition)
    {
      CounterId = l3L2WorkOrderDefinition.CounterId;
      CreatedTs = l3L2WorkOrderDefinition.CreatedTs;
      UpdatedTs = l3L2WorkOrderDefinition.UpdatedTs;
      CommStatus = l3L2WorkOrderDefinition.CommStatus;
      CommStatusName = ResxHelper.GetResxByKey(CommStatusEnum);
      CommStatusError =
        l3L2WorkOrderDefinition.CommStatus == BaseDbEntity.EnumClasses.CommStatus.ProcessingError.Value ||
        l3L2WorkOrderDefinition.CommStatus == BaseDbEntity.EnumClasses.CommStatus.ValidationError.Value;
      ValidationCheck = l3L2WorkOrderDefinition.ValidationCheck;
      CommMessage = l3L2WorkOrderDefinition.CommMessage;

      WorkOrderName = l3L2WorkOrderDefinition.WorkOrderName;
      ExternalWorkOrderName = l3L2WorkOrderDefinition.ExternalWorkOrderName;
      PreviousWorkOrderName = l3L2WorkOrderDefinition.PreviousWorkOrderName;
      OrderDeadline = l3L2WorkOrderDefinition.OrderDeadline;
      HeatName = l3L2WorkOrderDefinition.HeatName;
      BilletWeight = l3L2WorkOrderDefinition.BilletWeight;
      NumberOfBillets = l3L2WorkOrderDefinition.NumberOfBillets;
      CustomerName = l3L2WorkOrderDefinition.CustomerName;
      BundleWeightMin = l3L2WorkOrderDefinition.BundleWeightMin;
      BundleWeightMax = l3L2WorkOrderDefinition.BundleWeightMax;
      TargetWorkOrderWeight = l3L2WorkOrderDefinition.TargetWorkOrderWeight;
      TargetWorkOrderWeightMin = l3L2WorkOrderDefinition.TargetWorkOrderWeightMin;
      TargetWorkOrderWeightMax = l3L2WorkOrderDefinition.TargetWorkOrderWeightMax;
      MaterialCatalogueName = l3L2WorkOrderDefinition.MaterialCatalogueName;
      ProductCatalogueName = l3L2WorkOrderDefinition.ProductCatalogueName;
      SteelgradeCode = l3L2WorkOrderDefinition.SteelgradeCode;
      InputThickness = l3L2WorkOrderDefinition.InputThickness;
      InputWidth = l3L2WorkOrderDefinition.InputWidth;
      BilletLength = l3L2WorkOrderDefinition.BilletLength;
      InputShapeSymbol = l3L2WorkOrderDefinition.InputShapeSymbol;
      OutputThickness = l3L2WorkOrderDefinition.OutputThickness;
      OutputWidth = l3L2WorkOrderDefinition.OutputWidth;
      OutputShapeSymbol = l3L2WorkOrderDefinition.OutputShapeSymbol;

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    //modify by AV

    public DCL3L2WorkOrderDefinitionMOD GetL3L2WorkOrderDefinitionDataContract()
    {
      return new DCL3L2WorkOrderDefinitionMOD()
      {
        Counter = this.CounterId,
        CommStatus = this.CommStatusEnum,
        WorkOrderName = this.WorkOrderName,
        ExternalWorkOrderName = this.ExternalWorkOrderName,
        PreviousWorkOrderName = this.PreviousWorkOrderName,
        OrderDeadline = this.OrderDeadline,
        HeatName = this.HeatName,
        BilletWeight = this.BilletWeight,
        NumberOfBillets = this.NumberOfBillets,
        CustomerName = this.CustomerName,
        BundleWeightMin = this.BundleWeightMin,
        BundleWeightMax = this.BundleWeightMax,
        TargetWorkOrderWeight = this.TargetWorkOrderWeight,
        TargetWorkOrderWeightMin = this.TargetWorkOrderWeightMin,
        TargetWorkOrderWeightMax = this.TargetWorkOrderWeightMax,
        MaterialCatalogueName = this.MaterialCatalogueName,
        ProductCatalogueName = this.ProductCatalogueName,
        SteelgradeCode = this.SteelgradeCode,
        InputThickness = this.InputThickness,
        InputWidth = this.InputWidth,
        BilletLength = this.BilletLength,
        InputShapeSymbol = this.InputShapeSymbol,
        OutputThickness = this.OutputThickness,
        OutputWidth = this.OutputWidth,
        OutputShapeSymbol = this.OutputShapeSymbol


      };
    }
  }
}
