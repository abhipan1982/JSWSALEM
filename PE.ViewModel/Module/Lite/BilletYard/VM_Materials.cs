using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.BilletYard
{
  public class VM_Materials : VM_Base
  {
    [SmfDisplay(typeof(VM_Materials), "FKHeatId", "NAME_HeatName")]
    [DisplayFormat(HtmlEncode = false, ConvertEmptyStringToNull = true)]
    [SmfRequired]
    public long FKHeatId { get; set; }

    [SmfDisplay(typeof(VM_Materials), "MaterialNumber", "NAME_MaterialsNumber")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public int MaterialsNumber { get; set; }

    [SmfDisplay(typeof(VM_Materials), "MaterialCatalogueId", "NAME_MaterialCatalogue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public long MaterialCatalogueId { get; set; }
  }
}
