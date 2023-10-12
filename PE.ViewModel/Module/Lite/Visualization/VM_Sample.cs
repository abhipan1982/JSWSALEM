using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Visualization
{
  public class VM_Sample
  {
    [SmfDisplay(typeof(V_RawMaterialMeasurement), "Value", "NAME_Value")]
    [SmfUnit("UNIT_Length")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double Value { get; set; }

    [SmfDisplay(typeof(V_RawMaterialMeasurement), "Time", "NAME_Time")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfFormat("FORMAT_Minutes")]
    [SmfUnit("UNIT_Second")]
    public double Time { get; set; }

    public double[] ValueArray => new[] {Time, Math.Round(Value, 8)};
  }
}
