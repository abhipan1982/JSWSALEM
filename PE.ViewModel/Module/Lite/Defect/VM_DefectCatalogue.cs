using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Defect
{
  public class VM_DefectCatalogue : VM_Base
  {
    public VM_DefectCatalogue()
    {
    }

    public VM_DefectCatalogue(QTYDefectCatalogue c)
    {
      DefectCatalogueId = c.DefectCatalogueId;
      DefectCatalogueName = c.DefectCatalogueName;
      DefectCatalogueCode = c.DefectCatalogueCode;
      if (c.FKDefectCatalogueCategory != null)
      {
        DefectCatalogueCategoryId = c.FKDefectCatalogueCategory.DefectCatalogueCategoryId;
      }

      DefectCatalogueCategoryCode = c.FKDefectCatalogueCategory?.DefectCatalogueCategoryCode;
      DefectCatalogueDescription = c.DefectCatalogueDescription;
      IsDefault = c.IsDefault;
      ParentDefectCatalogueId = c.FKParentDefectCatalogue?.DefectCatalogueId;
      ParentDefectCatalogueCode = c.FKParentDefectCatalogue?.DefectCatalogueCode;
      IsActive = c.IsActive;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long DefectCatalogueId { get; set; }

    [SmfDisplay(typeof(VM_DefectCatalogue), "DefectCatalogueName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    [SmfRequired]
    public string DefectCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_DefectCatalogue), "DefectCatalogueCode", "NAME_Code")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [StringLength(4, MinimumLength = 3, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    [SmfRequired]
    public string DefectCatalogueCode { get; set; }

    [SmfDisplay(typeof(VM_DefectCatalogue), "DefectCatalogueCategoryId", "NAME_DefectCatalogueCategoryId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long DefectCatalogueCategoryId { get; set; }

    [SmfDisplay(typeof(VM_DefectCatalogue), "DefectCatalogueCategoryCode", "NAME_DefectCatalogueCategoryCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DefectCatalogueCategoryCode { get; set; }

    [SmfDisplay(typeof(VM_DefectCatalogue), "DefectCatalogueDescription", "NAME_DefectCategoryDescription")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public string DefectCatalogueDescription { get; set; }

    [SmfDisplay(typeof(VM_DefectCatalogue), "IsDefault", "NAME_IsDefault")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsDefault { get; set; }

    [SmfDisplay(typeof(VM_DefectCatalogue), "ParentDefectCatalogueId", "NAME_ParentDefectCatalogueId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? ParentDefectCatalogueId { get; set; }

    [SmfDisplay(typeof(VM_DefectCatalogue), "ParentDefectCatalogueCode", "NAME_ParentDefectCatalogueCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ParentDefectCatalogueCode { get; set; }

    [SmfDisplay(typeof(VM_DefectCatalogue), "IsActive", "NAME_IsActive")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool? IsActive { get; set; }
  }
}
