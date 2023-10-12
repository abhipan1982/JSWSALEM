using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Material
{
  public class VM_MaterialCatalogue : VM_Base
  {
    #region ctor

    public VM_MaterialCatalogue() { }

    public VM_MaterialCatalogue(PRMMaterialCatalogue p)
    {
      Id = p.MaterialCatalogueId;
      MaterialCatalogueName = p.MaterialCatalogueName;
      ExternalMaterialCatalogueName = p.ExternalMaterialCatalogueName;
      Description = p.MaterialCatalogueDescription;
      ShapeCode = p.FKShape?.ShapeCode;
      ShapeId = p.FKShapeId;
      LengthMin = p.LengthMin;
      LengthMax = p.LengthMax;
      WidthMin = p.WidthMin;
      WidthMax = p.WidthMax;
      ThicknessMin = p.ThicknessMin;
      ThicknessMax = p.ThicknessMax;
      WeightMin = p.WeightMin;
      WeightMax = p.WeightMax;
      MaterialCatalogueTypeSymbol = p.FKMaterialCatalogueType?.MaterialCatalogueTypeCode;
      TypeId = p.FKMaterialCatalogueTypeId;


      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region props

    public long Id { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_MaterialCatalogue), "MaterialCatalogueName", "NAME_Name")]
    [SmfStringLength(50, MinimumLength = 1)]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_MaterialCatalogue), "ExternalMaterialCatalogueName", "NAME_ExternalName")]
    [SmfStringLength(50)]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ExternalMaterialCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_MaterialCatalogue), "Description", "NAME_Description")]
    [SmfStringLength(200)]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Description { get; set; }

    [SmfDisplay(typeof(VM_MaterialCatalogue), "ShapeCode", "NAME_Shape")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ShapeCode { get; set; }

    [SmfDisplay(typeof(VM_MaterialCatalogue), "LengthMin", "NAME_LengthMin")]
    [SmfFormat("FORMAT_BilletLength", NullDisplayText = "-")]
    [SmfRange(typeof(VM_MaterialCatalogue), "LengthMin", "BilletLength")]
    [SmfUnit("UNIT_BilletLength")]
    public double? LengthMin { get; set; }

    [SmfDisplay(typeof(VM_MaterialCatalogue), "LengthMax", "NAME_LengthMax")]
    [SmfFormat("FORMAT_BilletLength", NullDisplayText = "-")]
    [SmfRange(typeof(VM_MaterialCatalogue), "LengthMax", "BilletLength")]
    [SmfUnit("UNIT_BilletLength")]
    public double? LengthMax { get; set; }

    [SmfDisplay(typeof(VM_MaterialCatalogue), "WidthMin", "NAME_WidthMin")]
    [SmfFormat("FORMAT_BilletWidth", NullDisplayText = "-")]
    [SmfRange(typeof(VM_MaterialCatalogue), "WidthMin", "BilletWidth")]
    [SmfUnit("UNIT_BilletWidth")]
    public double? WidthMin { get; set; }

    [SmfDisplay(typeof(VM_MaterialCatalogue), "WidthMax", "NAME_WidthMax")]
    [SmfFormat("FORMAT_BilletWidth", NullDisplayText = "-")]
    [SmfRange(typeof(VM_MaterialCatalogue), "WidthMax", "BilletWidth")]
    [SmfUnit("UNIT_BilletWidth")]
    public double? WidthMax { get; set; }

    [SmfDisplay(typeof(VM_MaterialCatalogue), "ThicknessMin", "NAME_ThicknessMin")]
    [SmfFormat("FORMAT_BilletThickness", NullDisplayText = "-")]
    [SmfRange(typeof(VM_MaterialCatalogue), "ThicknessMin", "BilletThickness")]
    [SmfUnit("UNIT_BilletThickness")]
    [SmfRequired]
    public double ThicknessMin { get; set; }

    [SmfDisplay(typeof(VM_MaterialCatalogue), "ThicknessMax", "NAME_ThicknessMax")]
    [SmfFormat("FORMAT_BilletThickness", NullDisplayText = "-")]
    [SmfRange(typeof(VM_MaterialCatalogue), "ThicknessMax", "BilletThickness")]
    [SmfUnit("UNIT_BilletThickness")]
    [SmfRequired]
    public double ThicknessMax { get; set; }

    [SmfDisplay(typeof(VM_MaterialCatalogue), "MaterialCatalogueTypeSymbol", "NAME_Type")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string MaterialCatalogueTypeSymbol { get; set; }

    [SmfDisplay(typeof(VM_MaterialCatalogue), "WeightMin", "NAME_WeightMin")]
    [SmfFormat("FORMAT_BilletWeight", NullDisplayText = "-")]
    [SmfRange(typeof(VM_MaterialCatalogue), "WeightMin", "BilletWeight")]
    [SmfUnit("UNIT_BilletWeight")]
    public double? WeightMin { get; set; }

    [SmfDisplay(typeof(VM_MaterialCatalogue), "WeightMax", "NAME_WeightMax")]
    [SmfFormat("FORMAT_BilletWeight", NullDisplayText = "-")]
    [SmfRange(typeof(VM_MaterialCatalogue), "WeightMax", "BilletWeight")]
    [SmfUnit("UNIT_BilletWeight")]
    public double? WeightMax { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_MaterialCatalogue), "ShapeId", "NAME_Shape")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long ShapeId { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_MaterialCatalogue), "TypeId", "NAME_Type")]
    public long TypeId { get; set; }

    #endregion
  }
}
