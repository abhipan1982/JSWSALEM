using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
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

    public VM_L2L3WorkOrderReport() { }

    public VM_L2L3WorkOrderReport(L2L3WorkOrderReport data)
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

    #endregion
  }
}
