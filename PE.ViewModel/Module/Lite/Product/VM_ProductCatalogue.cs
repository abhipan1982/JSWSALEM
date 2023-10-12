using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using PE.DbEntity.Models;

namespace PE.HMIWWW.ViewModel.Module.Lite.Product
{
  public class VM_ProductCatalogue : VM_Base
  {    
    public VM_ProductCatalogue() { }

    public VM_ProductCatalogue(PRMProductCatalogue p)
    {
      Id = p.ProductCatalogueId;
      Weight = p.Weight;
      WeightMax = p.WeightMax;
      WeightMin = p.WeightMin;
      ProductCatalogueName = p.ProductCatalogueName;
      ProductExternalCatalogueName = p.ExternalProductCatalogueName;
      Length = p.Length;
      LengthMax = p.LengthMax;
      LengthMin = p.LengthMin;
      Width = p.Width;
      WidthMax = p.WidthMax;
      WidthMin = p.WidthMin;
      Thickness = p.Thickness;
      ThicknessMax = p.ThicknessMax;
      ThicknessMin = p.ThicknessMin;
      StdProductivity = p.StdProductivity;
      OvalityMax = p.MaxOvality;
      ProductCatalogueDescription = p.ProductCatalogueDescription;
      Shape = p.FKShape?.ShapeName;
      ShapeId = p.FKShapeId;
      TypeId = p.FKProductCatalogueTypeId;
      Type = p.FKProductCatalogueType?.ProductCatalogueTypeCode;
      

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long Id { get; set; }

    [SmfFormat("GLOB_ShortDateTime_FORMAT")]
    public DateTime LastUpdateTs { get; set; }

    [SmfRequired]
    [SmfStringLength(50)]
    [SmfDisplay(typeof(VM_ProductCatalogue), "ProductCatalogueName", "NAME_Name")]
    public string ProductCatalogueName { get; set; }

    [SmfStringLength(50)]
    [SmfDisplay(typeof(VM_ProductCatalogue), "ProductExternalCatalogueName", "NAME_ExternalName")]
    public string ProductExternalCatalogueName { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "Length", "NAME_Length")]
    [SmfFormat("FORMAT_Length", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_ProductCatalogue), "Length", "ProductLength")]
    [SmfUnit("UNIT_Length")]
    public double? Length { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "LengthMax", "NAME_LengthMax")]
    [SmfFormat("FORMAT_Length", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_ProductCatalogue), "LengthMax", "ProductLength")]
    [SmfUnit("UNIT_Length")]
    public double? LengthMax { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "LengthMin", "NAME_LengthMin")]
    [SmfFormat("FORMAT_Length", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_ProductCatalogue), "LengthMin", "ProductLength")]
    [SmfUnit("UNIT_Length")]
    public double? LengthMin { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "Width", "NAME_Width")]
    [SmfFormat("FORMAT_Width", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_ProductCatalogue), "Width", "ProductWidth")]
    [SmfUnit("UNIT_Width")]
    public double? Width { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "WidthMax", "NAME_WidthMax")]
    [SmfFormat("FORMAT_Width", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_ProductCatalogue), "WidthMax", "ProductWidth")]
    [SmfUnit("UNIT_Width")]
    public double? WidthMax { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "WidthMin", "NAME_WidthMin")]
    [SmfFormat("FORMAT_Width", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_ProductCatalogue), "WidthMin", "ProductWidth")]
    [SmfUnit("UNIT_Width")]
    public double? WidthMin { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_ProductCatalogue), "Thickness", "NAME_Thickness")]
    [SmfFormat("FORMAT_Thickness", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_ProductCatalogue), "Thickness", "ProductThickness")]
    [SmfUnit("UNIT_Diameter")]
    public double Thickness { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_ProductCatalogue), "ThicknessMax", "NAME_ThicknessMax")]
    [SmfFormat("FORMAT_Thickness", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_ProductCatalogue), "ThicknessMax", "ProductThickness")]
    [SmfUnit("UNIT_Diameter")]
    public double ThicknessMax { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_ProductCatalogue), "ThicknessMin", "NAME_ThicknessMin")]
    [SmfFormat("FORMAT_Thickness", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_ProductCatalogue), "ThicknessMin", "ProductThickness")]
    [SmfUnit("UNIT_Diameter")]
    public double ThicknessMin { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "Weight", "NAME_Weight")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_ProductCatalogue), "Weight", "ProductWeight")]
    [SmfUnit("UNIT_Weight")]
    public double? Weight { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "WeightMax", "NAME_WeightMax")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_ProductCatalogue), "WeightMax", "ProductWeight")]
    [SmfUnit("UNIT_Weight")]
    public double? WeightMax { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "WeightMin", "NAME_WeightMin")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_ProductCatalogue), "WeightMin", "ProductWeight")]
    [SmfUnit("UNIT_Weight")]
    public double? WeightMin { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "OvalityMax", "NAME_MaxOvality")]
    [SmfFormat("FORMAT_Ovality", NullDisplayText = "-", HtmlEncode = false)]
    public double? OvalityMax { get; set; }

    [SmfStringLength(100)]
    [SmfDisplay(typeof(VM_ProductCatalogue), "Description", "NAME_Description")]
    public string ProductCatalogueDescription { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "Shape", "NAME_Shape")]
    public string Shape { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "Prod_Shape", "PROD_Shape")]
    public string Prod_Shape { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "Prod_Dim", "PROD_Dim")]
    public string Prod_Dim { get; set; }

    [SmfDisplay(typeof(VM_ProductCatalogue), "MinOvality", "PROD_Dim")]
    public double MinOval { get; set; }    

    [SmfDisplay(typeof(VM_ProductCatalogue), "Type", "NAME_Type")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Type { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_ProductCatalogue), "StdProductivity", "NAME_StdTph")]
    [SmfFormat("FORMAT_Plain3", NullDisplayText = "-", HtmlEncode = false)]
    public double StdProductivity { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_ProductCatalogue), "StdMetallicYield", "NAME_StdMetalicYield")]
    [SmfFormat("FORMAT_Plain3", NullDisplayText = "-", HtmlEncode = false)]
    public double StdMetallicYield { get; set; }

    [SmfRequired]
    public long ShapeId { get; set; }

    [SmfRequired]
    public long TypeId { get; set; }
  }
}
