using PE.DbEntity.DWModels;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.DataAnalysis
{
  public class VM_DataAnalysisWorkOrder : VM_Base
  {
    public VM_DataAnalysisWorkOrder()
    {
    }

    public VM_DataAnalysisWorkOrder(V_AS_WorkOrder data)
    {
      WorkOrderId = data.DimWorkOrderId;
      WorkOrderName = data.DimWorkOrderName;
      TargetWeight = data.TargetWeight;
      MaterialsCount = data.MaterialsNumber;
      MaterialsWeight = data.MaterialsWeight ?? 0;
      ProductsCount = data.ProductsNumber;
      ProductsWeight = data.ProductsWeight;
      //TODOMN - add a column for ScrapWeight
      ScrapsWeight = data.MaterialsScrappedNumber;
      MonthNr = data.DimMonth;
      Month = data.DimMonth;
      Week = data.DimWeek;
      Year = data.DimYear;
      Date = data.DimDate;
      //Day = Date.Value.Day;
      Day = data.DimDate;
      HeatName = data.DimHeatName;
      MaterialCatalogueName = data.DimMaterialCatalogueName;
      ProductCatalogue = data.DimProductCatalogueName;
      ShiftCode = data.DimShiftCode;
      Crew = data.DimCrewName;
      Steelgrade = data.DimSteelgradeName;
      ProductThickness = data.DimProductThickness;
      RawsCount = data.RawMaterialsNumber;
      RawsWeight = data.RawMaterialsWeight;
      MetallicYield = data.MetallicYield;
      QualityYield = data.QualityYield;
      ProductionTime = data.ProductionTime;
      RollingTime = data.RollingTime;

      //MonthNumber = MonthNr ?? -1;

      //string strMonthName = mfi.GetMonthName(8).ToString();

      //if (MonthNumber != -1)
      //{
      //  DateTimeFormatInfo mfi = new DateTimeFormatInfo();
      //  Month = mfi.GetAbbreviatedMonthName(MonthNumber);
      //}


      UnitConverterHelper.ConvertToLocalNotFormatted(this);
      //UnitConverterHelper.ConvertToLocal(this);
      //DynamicUnitConverterHelper.ConvertToLocalNotFormatted(this);
      //DynamicUnitConverterHelper.ConvertToLocal(this);
    }

    public VM_DataAnalysisWorkOrder(FactWorkOrder data)
    {
      WorkOrderId = data.FactWorkOrderKey;
      WorkOrderName = data.WorkOrderName;
      TargetWeight = data.WorkOrderTargetWeight;
      MaterialsCount = data.WorkOrderMaterialNumber;
      MaterialsWeight = data.WorkOrderMaterialWeight ?? 0;
      ProductsCount = data.WorkOrderProductNumber;
      ProductsWeight = data.WorkOrderProductWeight;
      ScrapsNumber = data.WorkOrderScrappedNumber;
      ScrapsWeight = data.WorkOrderScrappedWeight;
      MonthNr = data.DimMonth;
      Month = data.DimMonth;
      Week = data.DimWeek;
      Year = data.DimYear;
      Date = data.DimDate;
      Day = data.DimDate;
      HeatName = data.DimHeatName;
      MaterialCatalogueName = data.DimMaterialCatalogueName;
      ProductCatalogue = data.DimProductCatalogueName;
      ShiftCode = data.DimShiftCode;
      Crew = data.DimCrewName;
      Steelgrade = data.DimSteelgradeName;
      ProductThickness = data.DimProductThickness;
      RawsCount = data.WorkOrderRawMaterialNumber;
      RawsWeight = data.WorkOrderRawMaterialWeight;
      MetallicYield = data.WorkOrderMetallicYield;
      QualityYield = data.WorkOrderQualityYield;
      ProductionTime = data.WorkOrderDuration;
      RollingTime = data.WorkOrderRollingDuration;

      UnitConverterHelper.ConvertToLocalNotFormatted(this);
    }

    public long WorkOrderId { get; set; }

    [SmfDisplay(typeof(VM_DataAnalysisWorkOrder), "Year", "NAME_Year")]
    public string Year { get; set; }

    public string MonthNr { get; set; }
    public int MonthNumber { get; set; }

    [SmfDisplay(typeof(VM_DataAnalysisWorkOrder), "Month", "NAME_Month")]
    public string Month { get; set; }

    [SmfDisplay(typeof(VM_DataAnalysisWorkOrder), "Week", "NAME_Week")]
    public string Week { get; set; }

    public string Date { get; set; }
    public string Day { get; set; }

    [SmfDisplay(typeof(VM_DataAnalysisWorkOrder), "ShiftCode", "NAME_ShiftCode")]
    public string ShiftCode { get; set; }

    [SmfDisplay(typeof(VM_DataAnalysisWorkOrder), "ShiftKey", "NAME_ShiftKey")]
    public string ShiftKey { get; set; }

    [SmfDisplay(typeof(VM_DataAnalysisWorkOrder), "Crew", "NAME_Crew")]
    public string Crew { get; set; }

    public string MaterialCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_DataAnalysisWorkOrder), "ProductCatalogue", "NAME_ProductCatalogue")]
    public string ProductCatalogue { get; set; }

    [SmfDisplay(typeof(VM_DataAnalysisWorkOrder), "WorkOrderName", "NAME_WorkOrderName")]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_DataAnalysisWorkOrder), "Steelgrade", "NAME_Steelgrade")]
    public string Steelgrade { get; set; }

    [SmfDisplay(typeof(VM_DataAnalysisWorkOrder), "HeatName", "NAME_HeatName")]
    public string HeatName { get; set; }

    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double TargetWeight { get; set; }

    public int? MaterialsCount { get; set; }

    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double? MaterialsWeight { get; set; }

    public int? RawsCount { get; set; }

    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double? RawsWeight { get; set; }

    public int? ProductsCount { get; set; }


    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double? ProductsWeight { get; set; }

    public string ProductThickness { get; set; }
    public double? MetallicYield { get; set; }
    public double? QualityYield { get; set; }

    public long? ProductionTime { get; set; }
    public long? RollingTime { get; set; }
    public int? ScrapsNumber { get; set; }

    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double? ScrapsWeight { get; set; }

    public string UnitSymbol { get; set; }
    public long? UnitOfMeasureId { get; }
  }
}
