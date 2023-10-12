using System;
using System.Collections.Generic;
using System.Text;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.WorkOrder
{
  public class VM_BilletCatalogueDetails : VM_Base
  {

    public VM_BilletCatalogueDetails()
    {
    }

    public VM_BilletCatalogueDetails(PRMMaterialCatalogue p)
    {
      Id = p.MaterialCatalogueId;
      WeightMin = p.WeightMin;
      WeightMax = p.WeightMax;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long Id { get; set; }

    [SmfDisplay(typeof(VM_BilletCatalogueDetails), "WeightMin", "NAME_WeightMin")]
    [SmfFormat("FORMAT_WeightMin", NullDisplayText = "-", DataFormatString = "{0:0.###}")]
    [SmfUnit("UNIT_Weight")]
    public double? WeightMin { get; set; }

    [SmfDisplay(typeof(VM_BilletCatalogueDetails), "WeightMax", "NAME_WeightMax")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-", DataFormatString = "{0:0.###}")]
    [SmfUnit("UNIT_Weight")]
    public double? WeightMax { get; set; }
  }
}
