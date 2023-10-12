using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.Core;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_L3L1MaterialAssignment : VM_Base
  {
    #region ctor

    public VM_L3L1MaterialAssignment() { }

    public VM_L3L1MaterialAssignment(DbEntity.SPModels.SPL3L1MaterialAssignment data)
    {
      MaterialId = data.MaterialId;
      MaterialName = data.MaterialName;
      HeatId = data.HeatId;
      WorkOrderId = data.WorkOrderId;
      IsDummy = data.IsDummy;
      RawMaterialId = data.RawMaterialId;
      MaterialName = data.RawMaterialName ?? data.MaterialName;
      DisplayedMaterialName = data.DisplayedMaterialName;
      EnumRawMaterialStatus = data.EnumRawMaterialStatus ?? 0;
      ParentRawMaterialId = data.ParentRawMaterialId;
      ChildsNo = data.ChildsNo;
      EnumRejectLocation = data.EnumRejectLocation ?? 0;
      EnumTypeOfScrap = data.EnumTypeOfScrap ?? 0;
      OutputPieces = data.OutputPieces ?? 0;
      ScrapPercent = data.ScrapPercent;
      ProductId = data.ProductId;
      RawMaterialStatus = data.RawMaterialStatus;
      DefectsNumber = data.DefectsNumber;
      HasDefects = data.DefectsNumber > 0;
      HasDefectsText = HasDefects ? ResxHelper.GetResxByKey("NAME_Yes") : ResxHelper.GetResxByKey("NAME_No");
      HasDefectImageSRC = HasDefects ? "result_false" : "result_true";
      ProductId = data.ProductId;
      ProductCatalogueTypeCode = data.ProductCatalogueTypeCode;

      if (RawMaterialId.HasValue)
      {
        ProductUndoActionAvailable = ProductId.HasValue;
        RejectActionAvailable = !ProductId.HasValue && EnumTypeOfScrap != TypeOfScrap.Scrap;
        ScrapActionAvailable = !ProductId.HasValue && EnumRejectLocation == RejectLocation.None;
        //TODO Remove !data.ParentRawMaterialId.HasValue condition when multilevel genealogy will be implemented
        MaterialReadyActionAvailable =
          (string.IsNullOrEmpty(data.ProductCatalogueTypeCode)
            || data.ProductCatalogueTypeCode.ToUpper().Equals(Constants.Bar.ToUpper())
            || data.ProductCatalogueTypeCode.ToUpper().Equals(Constants.WireRod.ToUpper())
            || data.ProductCatalogueTypeCode.ToUpper().Equals(Constants.Garret.ToUpper()))
          && !ProductId.HasValue && data.EnumTypeOfScrap != TypeOfScrap.Scrap
          && data.EnumRejectLocation == RejectLocation.None
          && !data.ParentRawMaterialId.HasValue;
      }

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region props

    [SmfDisplay(typeof(VM_L3L1MaterialAssignment), "MaterialId", "NAME_MaterialId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long MaterialId { get; set; }

    public long? RawMaterialId { get; set; }
    public long? ParentRawMaterialId { get; set; }

    public long? ProductId { get; set; }

    [SmfDisplay(typeof(VM_L3L1MaterialAssignment), "IsDummy", "NAME_IsDummyMaterial")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsDummy { get; set; }

    [SmfDisplay(typeof(VM_L3L1MaterialAssignment), "MaterialName", "NAME_MaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialName { get; set; }

    [SmfDisplay(typeof(VM_L3L1MaterialAssignment), "DisplayedMaterialName", "NAME_MaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DisplayedMaterialName { get; set; }

    [SmfDisplay(typeof(VM_L3L1MaterialAssignment), "FKHeatId", "NAME_FKHeatId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long HeatId { get; set; }

    [SmfDisplay(typeof(VM_L3L1MaterialAssignment), "FKWorkOrderId", "NAME_FKWorkOrderId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? WorkOrderId { get; set; }

    [SmfDisplay(typeof(VM_L3L1MaterialAssignment), "StatusName", "NAME_Status")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string RawMaterialStatus { get; set; }

    [SmfDisplay(typeof(VM_L3L1MaterialAssignment), "EnumRawMaterialStatus", "NAME_Status")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short EnumRawMaterialStatus { get; set; }

    [SmfDisplay(typeof(VM_L3L1MaterialAssignment), "HasDefects", "NAME_Defects")]
    public bool HasDefects { get; set; }

    public string HasDefectsText { get; set; }

    public string HasDefectImageSRC { get; set; }

    [SmfDisplay(typeof(VM_L3L1MaterialAssignment), "EnumTypeOfScrap", "NAME_TypeOfScrap")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short EnumTypeOfScrap { get; set; }

    [SmfDisplay(typeof(VM_L3L1MaterialAssignment), "EnumRejectLocation", "NAME_RejectLocation")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short EnumRejectLocation { get; set; }

    [SmfDisplay(typeof(VM_L3L1MaterialAssignment), "OutputPieces", "NAME_NumberOfOutputPieces")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short OutputPieces { get; set; }

    [SmfDisplay(typeof(VM_L3L1MaterialAssignment), "ScrapPercent", "NAME_ScrapPercent")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Percent")]
    public double? ScrapPercent { get; set; }

    [SmfDisplay(typeof(VM_L3L1MaterialAssignment), "DefectsNumber", "NAME_Defects")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int DefectsNumber { get; set; }

    public bool ScrapActionAvailable { get; set; }

    public bool RejectActionAvailable { get; set; }

    public bool ProductUndoActionAvailable { get; set; }

    public bool MaterialReadyActionAvailable { get; set; }

    public short? ChildsNo { get; set; }

    [SmfDisplay(typeof(VM_L3L1MaterialAssignment), "ProductCatalogueTypeCode", "NAME_ProductCatalogueType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductCatalogueTypeCode { get; set; }

    #endregion
  }
}
