using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_MaterialCatalogueType : VM_Base
  {
    public VM_MaterialCatalogueType()
    {
    }

    public VM_MaterialCatalogueType(PRMMaterialCatalogueType s)
    {
      Id = s.MaterialCatalogueTypeId;
      Name = s.MaterialCatalogueTypeName;
      Description = s.MaterialCatalogueTypeDescription;
      Code = s.MaterialCatalogueTypeCode;
      IsDefault = s.IsDefault;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long Id { get; set; }

    [SmfDisplay(typeof(VM_Shape), "KeyName", "NAME_Name")]
    public string Name { get; set; }

    [SmfDisplay(typeof(VM_Shape), "KeyDescription", "NAME_Description")]
    public string Description { get; set; }

    [SmfDisplay(typeof(VM_Shape), "KeySymbol", "NAME_Symbol")]
    public string Code { get; set; }

    [SmfDisplay(typeof(VM_Shape), "KeyName", "NAME_IsDefault")]
    public bool IsDefault { get; set; }
  }
}
