using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Product
{
  public class VM_ProductCatalogueType : VM_Base
  {
    public VM_ProductCatalogueType()
    {
    }

    public VM_ProductCatalogueType(PRMProductCatalogueType s)
    {
      Id = s.ProductCatalogueTypeId;
      Name = s.ProductCatalogueTypeName;
      Description = s.ProductCatalogueTypeDescription;
      Symbol = s.ProductCatalogueTypeCode;
      IsDefault = s.IsDefault;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long Id { get; set; }

    [SmfDisplay(typeof(VM_Shape), "KeyName", "NAME_Name")]
    public string Name { get; set; }

    [SmfDisplay(typeof(VM_Shape), "KeyDescription", "NAME_Description")]
    public string Description { get; set; }

    [SmfDisplay(typeof(VM_Shape), "KeySymbol", "NAME_Symbol")]
    public string Symbol { get; set; }

    [SmfDisplay(typeof(VM_Shape), "KeyName", "NAME_IsDefault")]
    public bool IsDefault { get; set; }
  }
}
