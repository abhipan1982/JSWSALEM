using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.EnumClasses;
using PE.Core;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_RawMaterialGenealogy : VM_Base
  {
    public VM_RawMaterialGenealogy() { }

    public VM_RawMaterialGenealogy(DbEntity.SPModels.SPRawMaterialGenealogy data)
    {
      Id = data.RawMaterialId;
      CuttingSeqNo = data.CuttingSeqNo;
      ChildsNo = data.ChildsNo;
      ProductCatalogueTypeId = data.ProductCatalogueTypeId;
      EnumRawMaterialStatus = data.EnumRawMaterialStatus;
      //TODO Remove !data.ParentRawMaterialId.HasValue condition when multilevel genealogy will be implemented
      Dividable =
        (!data.ProductCatalogueTypeId.HasValue
          || data.ProductCatalogueTypeCode.ToUpper().Equals(Constants.Bar.ToUpper())
          || data.ProductCatalogueTypeCode.ToUpper().Equals(Constants.WireRod.ToUpper())
          || data.ProductCatalogueTypeCode.ToUpper().Equals(Constants.Garret.ToUpper()))
        && !ProductId.HasValue && data.EnumTypeOfScrap != TypeOfScrap.Scrap 
        && data.EnumRejectLocation == RejectLocation.None 
        && !data.ParentRawMaterialId.HasValue;
      KeepInTracking = true;
      MaterialId = data.MaterialId;
      ProductId = data.ProductId;
      EnumRejectLocation = data.EnumRejectLocation;
      EnumTypeOfScrap = data.EnumTypeOfScrap;
    }

    public long? Id { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialGenealogy), "Name", "NAME_MaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Name { get; set; }

    public VM_RawMaterialGenealogyElement Parent { get; set; }

    public List<VM_RawMaterialGenealogyElement> Children { get; set; }

    public bool Dividable { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialGenealogy), "ProductCatalogueTypeId", "NAME_Type")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public long? ProductCatalogueTypeId { get; set; }

    public string ProductCatalogueTypeName { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialGenealogy), "CuttingSeqNo", "NAME_CutNo")]
    public short CuttingSeqNo { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialGenealogy), "ChildsNo", "NAME_ChildsNo")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = "-", HtmlEncode = false)]
    public short ChildsNo { get; set; }

    public short EnumRawMaterialStatus { get; set; }

    [SmfDisplay(typeof(VM_RawMaterialGenealogy), "KeepInTracking", "NAME_KeepInTracking")]
    public bool KeepInTracking { get; set; }

    public long? MaterialId { get; set; }

    public long? ProductId { get; set; }

    public short EnumTypeOfScrap { get; set; }

    public short EnumRejectLocation { get; set; }
  }
}
