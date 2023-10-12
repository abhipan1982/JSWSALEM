using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Products
{
  public class VM_ProductsList : VM_Base
  {
    public VM_ProductsList() { }


    public VM_ProductsList(V_Product data)
    {
      ProductId = data.ProductId;
      //ShiftKey = data.ShiftKey;
      ProductName = data.ProductName;
      WorkOrderName = data.WorkOrderName;
      ProductWeight = data.ProductWeight;
      IsDummy = data.IsDummy;
      IsAssigned = data.IsAssigned;
      SteelgradeCode = data.SteelgradeCode;
      SteelgradeName = data.SteelgradeName;
      HeatName = data.HeatName;
      ProductRollingDate = data.ProductRollingDate;
      CreatedTs = data.ProductCreatedTs;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public VM_ProductsList(V_ProductSearchGrid data)
    {
      ProductId = data.ProductId;
      ProductName = data.ProductName;
      WorkOrderName = data.WorkOrderName;
      ProductWeight = data.ProductWeight;
      IsAssigned = data.IsAssigned;
      ProductCatalogueName = data.ProductCatalogueName;
      SteelgradeCode = data.SteelgradeCode;
      SteelgradeName = data.SteelgradeName;
      HeatName = data.HeatName;
      ProductRollingDate = data.ProductRollingDate;
      HasDefects = !(data?.DefectsNumber is null) && (data.DefectsNumber > 0);
      HasDefectsText = HasDefects ? ResxHelper.GetResxByKey("NAME_Yes") : ResxHelper.GetResxByKey("NAME_No");
      HasDefectImageSRC = HasDefects ? "result_false" : "result_true";
      RawMaterialId = data.RawMaterialId;
      EnumInspectionResult = data.EnumInspectionResult;
      InspectionResultEnum = InspectionResult.GetValue(data.EnumInspectionResult);
      InspectionResultText = ResxHelper.GetResxByKey("NAME_InspectionResult_" + InspectionResultEnum.Name);
      CreatedTs = data.ProductCreatedTs;

      if (InspectionResultEnum == InspectionResult.Undefined)
        InspectionResultImageSRC = "result_warn_blue";
      else if (InspectionResultEnum == InspectionResult.Good)
        InspectionResultImageSRC = "result_true";
      else if (InspectionResultEnum == InspectionResult.Bad)
        InspectionResultImageSRC = "result_false";
      else if (InspectionResultEnum == InspectionResult.Doubtful)
        InspectionResultImageSRC = "result_warn";

      UnitConverterHelper.ConvertToLocal(this);
    }

    [SmfDisplay(typeof(VM_ProductsList), "ProductId", "NAME_ProductId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? ProductId { get; set; }


    [SmfDisplay(typeof(VM_ProductsList), "ProductName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-;", HtmlEncode = false)]
    public string ProductName { get; set; }

    [SmfDisplay(typeof(VM_ProductsList), "WorkOrderName", "NAME_WorkOrder")]
    [DisplayFormat(NullDisplayText = "-;", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_ProductsList), "RawMaterialWeight", "NAME_RawMaterialWeight")]
    [DisplayFormat(NullDisplayText = "-;", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double ProductWeight { get; set; }

    [SmfDisplay(typeof(VM_ProductsList), "IsDummy", "NAME_IsDummyMaterial")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsDummy { get; set; }

    [SmfDisplay(typeof(VM_ProductsList), "IsAssigned", "NAME_IsAssigned")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsAssigned { get; set; }

    [SmfDisplay(typeof(VM_ProductsList), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_ProductsList), "ProductCatalogueName", "NAME_ProductCatalogue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_ProductsList), "SteelgradeCode", "NAME_SteelgradeCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeCode { get; set; }

    [SmfDisplay(typeof(VM_ProductsList), "SteelgradeName", "NAME_SteelgradeName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeName { get; set; }

    [SmfDisplay(typeof(VM_ProductsList), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_ProductsList), "ProductRollingDate", "NAME_RollingDate")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public DateTime? ProductRollingDate { get; set; }

    [SmfDisplay(typeof(VM_ProductsList), "DefectsNumber", "NAME_Defects")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int DefectsNumber { get; set; }

    public bool HasDefects { get; set; }

    public string HasDefectImageSRC { get; set; }

    public string HasDefectsText { get; set; }

    public long? RawMaterialId { get; set; }

    public string InspectionResultText { get; set; }

    public string InspectionResultImageSRC { get; set; }

    public short EnumInspectionResult { get; set; }

    public InspectionResult InspectionResultEnum { get; set; }
  }
}
