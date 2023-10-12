using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Visualization
{
  public class VM_AreaRawMaterialMeasurements
  {
    public VM_AreaRawMaterialMeasurements(string areaCode)
    {
      AreaCode = areaCode;
      UnitConverterHelper.ConvertToLocal(this);
    }

    [SmfDisplay(typeof(VM_AreaRawMaterialMeasurements), "AreaCode", "NAME_AreaCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AreaCode { get; set; }

    public List<VM_AreaRawMaterialMeasurement> AreaMeasurements { get; set; }
  }
}
