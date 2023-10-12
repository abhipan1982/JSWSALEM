using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Visualization
{
  public class VM_Measurement
  {
    public VM_Measurement(V_RawMaterialMeasurement model)
    {
      MeasurementId = model.MeasurementId;
      ValueAvg = model.MeasurementValueAvg;
      ValueMin = model.MeasurementValueMin;
      ValueMax = model.MeasurementValueMax;
    }

    [SmfDisplay(typeof(VM_Measurement), "MeasurementId", "NAME_MeasurementId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long MeasurementId { get; set; }

    [SmfDisplay(typeof(VM_Measurement), "ValueAvg", "NAME_ValueAvg")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double ValueAvg { get; set; }

    [SmfDisplay(typeof(VM_Measurement), "ValueMin", "NAME_ValueMin")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? ValueMin { get; set; }

    [SmfDisplay(typeof(VM_Measurement), "ValueMax", "NAME_ValueMax")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? ValueMax { get; set; }
  }
}
