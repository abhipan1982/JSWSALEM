using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Quality
{
  public class VM_ProductDefect : VM_Base
  {
    public VM_ProductDefect(V_DefectsSummary defect)
    {
      ProductId = defect.ProductId;
      DefectId = defect.DefectId;
      DefectCatalogueName = defect.DefectCatalogueName;
      DefectCatalogueCode = defect.DefectCatalogueCode;
      DefectDescription = defect.DefectDescription;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long DefectId { get; set; }

    public long? ProductId { get; set; }

    [SmfDisplay(typeof(VM_ProductDefect), "DefectName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DefectName { get; set; }

    [SmfDisplay(typeof(VM_ProductDefect), "DefectDescription", "NAME_Description")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DefectDescription { get; set; }

    [SmfDisplay(typeof(VM_ProductDefect), "DefectCatalogueName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DefectCatalogueName { get; set; }

    public string DefectCatalogueCategoryName { get; set; }

    public bool? IsProductRelated { get; set; }

    public string ProductName { get; set; }

    [SmfDisplay(typeof(VM_ProductDefect), "DefectCatalogueCode", "NAME_Code")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DefectCatalogueCode { get; set; }
  }
}
