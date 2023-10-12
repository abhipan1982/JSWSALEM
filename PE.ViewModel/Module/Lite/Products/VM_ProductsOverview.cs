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
  public class VM_ProductsOverview : VM_Base
  {
    public VM_ProductsOverview() { }

    public VM_ProductsOverview(V_ProductOverview data)
    {
      ProductId = data.ProductId;
      ProductName = data.ProductName;
      ProductCreatedTs = data.ProductCreatedTs;
      IsAssigned = data.IsAssigned;
      ProductWeight = data.ProductWeight;
      WorkOrderName = data.WorkOrderName;
      WorkOrderId = data.WorkOrderId;
      HasHeatChemicalAnalysis = data.HasHeatChemicalAnalysis;
      InspectionResultEnum = InspectionResult.GetValue(data.EnumInspectionResult);
      EnumInspectionResult = data.EnumInspectionResult;
      InspectionResultText = ResxHelper.GetResxByKey("NAME_InspectionResult_" + InspectionResultEnum.Name);

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

    [SmfDisplay(typeof(VM_ProductsOverview), "ProductId", "NAME_ProductId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? ProductId { get; set; }

    [SmfDisplay(typeof(VM_ProductsOverview), "ProductCreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? ProductCreatedTs { get; set; }

    [SmfDisplay(typeof(VM_ProductsOverview), "ProductName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-;", HtmlEncode = false)]
    public string ProductName { get; set; }

    [SmfDisplay(typeof(VM_ProductsOverview), "IsAssigned", "NAME_IsAssigned")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsAssigned { get; set; }

    [SmfDisplay(typeof(VM_ProductsOverview), "Weight", "NAME_Weight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_Weight")]
    public double ProductWeight { get; set; }

    [SmfDisplay(typeof(VM_ProductsOverview), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    public InspectionResult InspectionResultEnum { get; set; }

    [SmfDisplay(typeof(VM_ProductsOverview), "EnumInspectionResult", "NAME_QualityResult")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int EnumInspectionResult { get; set; }

    [SmfDisplay(typeof(VM_ProductsOverview), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string InspectionResultText { get; set; }

    public long? WorkOrderId { get; set; }

    public bool HasHeatChemicalAnalysis { get; set; }

    public string InspectionResultImageSRC { get; set; }
  }
}
