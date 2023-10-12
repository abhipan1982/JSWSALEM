using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.ConsumptionMonitoring
{
  public class VM_Measurement : VM_Base
  {
    public VM_Measurement()
    {
    }

    public VM_Measurement(V_Measurement measurement)
    {
      MeasurementValueAvg = measurement.MeasurementValueAvg;
      MeasurementTime = measurement.MeasurementTime;

      ConvertToLocal(this);
    }

    [SmfDisplay(typeof(VM_Measurement), "MeasurementTime", "NAME_MeasurementTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime MeasurementTime { get; set; }

    [SmfDisplay(typeof(VM_Measurement), "MeasurementValueAvg", "NAME_MeasurementValueAvg")]
    [DisplayFormat(NullDisplayText = "-;", HtmlEncode = false)]
    public double MeasurementValueAvg { get; set; }
  }
}
