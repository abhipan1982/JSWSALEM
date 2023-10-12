using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.Module.Lite.DataAnalysis
{
  public class VM_DataAnalysisDefect : VM_Base
  {
    public VM_DataAnalysisDefect(V_AS_Defect data)
    {
      ProductThickness = data.DimProductThickness;
      HeatName = data.DimHeatName;
      SteelgradeName = data.DimSteelgradeName;
      DefectCategoryName = data.DimDefectCatalogueCategoryName;
      WorkOrderName = data.DimWorkOrderName;
      ShiftCode = data.DimShiftCode;
      CrewName = data.DimCrewName;
      Date = data.DimDate;
      //Day = Date.Value.Day;
      Week = data.DimWeek;
      Month = data.DimMonth;
      Year = data.DimYear;
      DefectsNumber = data.DefectsNumber;
    }

    public long DefectId { get; set; }
    public string Year { get; set; }
    public string Month { get; set; }
    public string Week { get; set; }
    public int? Day { get; set; }
    public string Date { get; set; }
    public string ShiftCode { get; set; }
    public string DimShiftKey { get; set; }
    public string CrewName { get; set; }
    public string DimDefectCatalogueCode { get; set; }
    public string DimDefectCatalogueName { get; set; }
    public string DimDefectCatalogueCategoryCode { get; set; }
    public string DefectCategoryName { get; set; }
    public string DimMaterialCatalogueName { get; set; }
    public string DimMaterialThickness { get; set; }
    public string DimProductCatalogueName { get; set; }
    public string ProductThickness { get; set; }
    public string WorkOrderName { get; set; }
    public string SteelgradeName { get; set; }
    public string HeatName { get; set; }
    public string DimAssetName { get; set; }
    public int DefectsNumber { get; set; }
  }
}
