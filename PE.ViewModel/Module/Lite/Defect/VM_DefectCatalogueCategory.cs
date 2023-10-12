using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Defect
{
  public class VM_DefectCatalogueCategory : VM_Base
  {
    public VM_DefectCatalogueCategory()
    {
    }

    public VM_DefectCatalogueCategory(QTYDefectCatalogueCategory d)
    {
      Id = d.DefectCatalogueCategoryId;
      DefectCatalogueCategoryName = d.DefectCatalogueCategoryName;
      DefectCatalogueCategoryCode = d.DefectCatalogueCategoryCode;
      DefectCatalogueCategoryDescription = d.DefectCatalogueCategoryDescription;
      IsDefault = d.IsDefault;
      DefectCategoryGroupId = d.FKDefectCategoryGroup?.DefectCategoryGroupId;
      DefectCategoryGroupCode = d.FKDefectCategoryGroup?.DefectCategoryGroupCode;
      EnumAssignmentTypeId = d.EnumAssignmentType;
      EnumAssignmentTypeName = ResxHelper.GetResxByKey((AssignmentType)d.EnumAssignmentType);

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long Id { get; set; }

    [SmfDisplay(typeof(VM_DefectCatalogueCategory), "DefectCatalogueCategoryName", "NAME_DefectCategoryName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DefectCatalogueCategoryName { get; set; }

    [SmfDisplay(typeof(VM_DefectCatalogueCategory), "DefectCatalogueCategoryCode", "NAME_DefectCategoryCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DefectCatalogueCategoryCode { get; set; }

    [SmfDisplay(typeof(VM_DefectCatalogueCategory), "DefectCatalogueCategoryDescription",
      "NAME_DefectCategoryDescription")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DefectCatalogueCategoryDescription { get; set; }

    [SmfDisplay(typeof(VM_DefectCatalogueCategory), "DefectCategoryGroupId", "NAME_DefectCategoryGroupId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? DefectCategoryGroupId { get; set; }

    [SmfDisplay(typeof(VM_DefectCatalogueCategory), "DefectCategoryGroupCode", "NAME_DefectCategoryGroupCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string DefectCategoryGroupCode { get; set; }

    [SmfDisplay(typeof(VM_DefectCatalogueCategory), "IsDefault", "NAME_IsDefault")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsDefault { get; set; }

    [SmfDisplay(typeof(VM_DefectGroupsCatalogue), "EnumAssignmentType", "NAME_EnumAssignmentType")]
    public short EnumAssignmentTypeId { get; set; }

    [SmfDisplay(typeof(VM_DefectGroupsCatalogue), "EnumAssignmentType", "NAME_EnumAssignmentType")]
    public string EnumAssignmentTypeName { get; set; }
  }
}
