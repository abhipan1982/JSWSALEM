using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.Core;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_RawMaterialGenealogyElement : VM_Base
  {
    public VM_RawMaterialGenealogyElement() { }

    public VM_RawMaterialGenealogyElement(DbEntity.SPModels.SPRawMaterialGenealogy data)
    {
      RawMaterialId = data.RawMaterialId;
      RawMaterialName = data.RawMaterialName;
      Length = data.LastLength;
      MaterialId = data.MaterialId;
      ProductId = data.ProductId;
      EnumRejectLocation = data.EnumRejectLocation;
      EnumTypeOfScrap = data.EnumTypeOfScrap;
      //TODO Remove !data.ParentRawMaterialId.HasValue condition when multilevel genealogy will be implemented
      Dividable =
        (!data.ProductCatalogueTypeId.HasValue
          || data.ProductCatalogueTypeCode.ToUpper().Equals(Constants.Bar.ToUpper())
          || data.ProductCatalogueTypeCode.ToUpper().Equals(Constants.WireRod.ToUpper())
          || data.ProductCatalogueTypeCode.ToUpper().Equals(Constants.Garret.ToUpper()))
        && !ProductId.HasValue && data.EnumTypeOfScrap != TypeOfScrap.Scrap
        && data.EnumRejectLocation == RejectLocation.None
        && !data.ParentRawMaterialId.HasValue;

      UnitConverterHelper.ConvertToLocal(this);
    }

    [SmfDisplay(typeof(VM_RawMaterialGenealogyElement), "RawMaterialId", "NAME_MaterialId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? RawMaterialId { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialGenealogyElement), "RawMaterialName", "NAME_MaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string RawMaterialName { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialGenealogyElement), "Length", "NAME_Length")]
    [SmfFormat("FORMAT_Length", NullDisplayText = "-")]
    [SmfUnit("UNIT_Length")]
    public double? Length { get; set; }

    public bool Dividable { get; set; }

    public long? MaterialId { get; set; }

    public long? ProductId { get; set; }

    public short EnumTypeOfScrap { get; set; }

    public short EnumRejectLocation { get; set; }
  }
}
