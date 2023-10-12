using System;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.DataAnalysis
{
  public class VM_DataAnalysisDelay : VM_Base
  {
    public VM_DataAnalysisDelay()
    {
    }

    public VM_DataAnalysisDelay(V_AS_Delay data)
    {
      DelayId = data.DimDelayId;

      Crew = data.DimCrewName;

      Category = data.DimDelayCatalogueCategoryName;
      ShiftCode = data.DimShiftCode;

      Year = data.DimYear;
      MonthNr = data.DimMonth;
      Week = data.DimWeek;
      DelayDuration = data.DelayDuration ?? 0;
      //Day = Date.Value.Day;
      Day = data.DimDate;

      //MonthNumber = MonthNr ?? -1;


      //string strMonthName = mfi.GetMonthName(8).ToString();

      //if (MonthNumber != -1)
      //{
      //  DateTimeFormatInfo mfi = new DateTimeFormatInfo();
      //  Month = mfi.GetAbbreviatedMonthName(MonthNumber);
      //}


      //if (data.DelayStart != null && data.DelayEnd != null)
      //{
      //  Duration = data.DelayEnd - data.DelayStart;
      //  DurationInSeconds = (int)Math.Round(Duration.Value.TotalSeconds);
      //}

      UnitConverterHelper.ConvertToLocalNotFormatted(this);
    }

    public long DelayId { get; set; }
    public string Year { get; set; }
    public string Day { get; set; }
    public string MonthNr { get; set; }
    public string Week { get; set; }
    public string Date { get; set; }
    public string ShiftCode { get; set; }
    public string ShiftKey { get; set; }
    public string Crew { get; set; }

    [SmfDisplay(typeof(VM_DataAnalysisDelay), "Category", "NAME_DelayCategory")]
    public string Category { get; set; }

    //[SmfDisplay(typeof(VM_DataAnalysisDelay), "CatalogueCategoryName", "NAME_DelayCategory")]
    public string DelayCatalogueName { get; set; }
    public string DelayCatalogueCategoryCode { get; set; }
    public bool DimIsPlanned { get; set; }
    public DateTime DelayStart { get; set; }
    public DateTime? DelayEnd { get; set; }
    public double? DelayDuration { get; set; }
    public int IsOpen { get; set; }

    [SmfDisplay(typeof(VM_DataAnalysisDelay), "Duration", "NAME_Duration")]
    public TimeSpan? Duration { get; set; }

    [SmfDisplay(typeof(VM_DataAnalysisDelay), "DurationInSeconds", "NAME_Duration")]
    //[SmfUnit("UNIT_Second")]
    public int DurationInSeconds { get; set; }

    public string Month { get; set; }

    public int MonthNumber { get; set; }
  }
}
