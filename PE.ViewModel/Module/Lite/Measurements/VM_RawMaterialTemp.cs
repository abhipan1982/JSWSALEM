using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Measurements
{
  public class VM_RawMaterialTemp : VM_Base
  {
    public long? RawMaterialId { get; set; }

    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Temperature")]
    public double? Temperature { get; set; }

    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short? OrderSeq { get; set; }

    public DateTime CreatedTs { get; set; }

    public VM_RawMaterialTemp(V_RawMaterialTemperature data)
    {
      RawMaterialId = data.RawMaterialId;
      OrderSeq = data.OrderSeq;
      Temperature = data.Temperature;
      //CreatedTs = data.CreatedTs;

      UnitConverterHelper.ConvertToLocal(this);
    }
  }
}
