using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using PE.BaseDbEntity.Models;
using PE.DbEntity.Models;
using PE.DbEntity.PEContext;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using PE.DbEntity.HmiModels;

namespace PE.HMIWWW.ViewModel.Module.Lite.Product
{
  public class VM_ProductCatalogue : VM_Base
  {
    private readonly PECustomContext _peCustomContext;//@av
    public VM_ProductCatalogue()
    {
      //_peCustomContext = new PECustomContext();//@av
    }
    public VM_ProductCatalogue(V_ProductCatalogue p)
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
      Shape = p.ShapeName;
      ShapeId = p.shapeId;
      TypeId = p.ProductCatalogueTypeID;
      Type = p.ProductCatalogueTypeCode;
      MinOvality = p.MinOvality;
      MinDiameter = p.MinDiameter;
      MaxDiameter = p.MaxDiameter;
      Diameter = p.Diameter;
      NegRcsSide = p.NegRcsSide;
      PosRcsSide = p.PosRcsSide;
      MinSquareness = p.MinSquareness;
      MaxSquareness = p.MaxSquareness;
      UnitConverterHelper.ConvertToLocal(this);
    }
    public VM_ProductCatalogue(PRMProductCatalogue p)
    {
      _peCustomContext = new PECustomContext();//@av
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

      ////avstart260723

      //_peCustomContext = new PECustomContext();
      //PRMProductCatalogueEXT p1 = _peCustomContext.PRMProductCatalogueEXTs
      //  .SingleOrDefault(x => x.FKProductCatalogueId == Id);


      //MinOvality = p1.MinOvality;
      //MinDiameter = p1.MinDiameter;
      //MaxDiameter = p1.MaxDiameter;
      //Diameter = p1.Diameter;
      //NegRcsSide = p1.NegRcsSide;
      //PosRcsSide = p1.PosRcsSide;
      //MinSquareness = p1.MinSquareness;
      //MaxSquareness = p1.MaxSquareness;

      ////avend260723
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









    //avstart26072023

    [SmfDisplay(typeof(VM_ProductCatalogue), "MinOvality", "NAME_MinOvality")]
    [SmfFormat("FORMAT_Thickness", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_ProductCatalogue), "MinOvality", "ProductOvality")]
    [SmfUnit("UNIT_Ovality")]
    public double? MinOvality { get; set; }



    [SmfDisplay(typeof(VM_ProductCatalogue), "MinDiameter", "NAME_MinDiameter")]
    [SmfFormat("FORMAT_Thickness", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_ProductCatalogue), "MinDiameter", "ProductDiameter")]
    [SmfUnit("UNIT_Diameter")]
    public double? MinDiameter { get; set; }



    [SmfDisplay(typeof(VM_ProductCatalogue), "MaxDiameter", "NAME_MaxDiameter")]
    [SmfFormat("FORMAT_Thickness", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_ProductCatalogue), "MaxDiameter", "ProductDiameter")]
    [SmfUnit("UNIT_Diameter")]
    public double? MaxDiameter { get; set; }



    [SmfDisplay(typeof(VM_ProductCatalogue), "Diameter", "NAME_Diameter")]
    [SmfFormat("FORMAT_Thickness", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_ProductCatalogue), "Diameter", "ProductDiameter")]
    [SmfUnit("UNIT_Diameter")]
    public double? Diameter { get; set; }



    [SmfDisplay(typeof(VM_ProductCatalogue), "MinSquareness", "NAME_MinSquareness")]
    [SmfFormat("FORMAT_Thickness", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_ProductCatalogue), "MinSquareness", "ProductSquareness")]
    [SmfUnit("UNIT_SQUARNESS")]
    public double? MinSquareness { get; set; }



    [SmfDisplay(typeof(VM_ProductCatalogue), "MaxSquareness", "NAME_MaxSquareness")]
    [SmfFormat("FORMAT_Thickness", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_ProductCatalogue), "MaxSquareness", "ProductSquareness")]
    [SmfUnit("UNIT_SQUARNESS")]
    public double? MaxSquareness { get; set; }



    [SmfDisplay(typeof(VM_ProductCatalogue), "NegRcsSide", "NAME_NegRcsSide")]
    [SmfFormat("FORMAT_Thickness", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_ProductCatalogue), "NegRcsSide", "ProductNegRcsSide")]
    [SmfUnit("UNIT_Width")]
    public double? NegRcsSide { get; set; }



    [SmfDisplay(typeof(VM_ProductCatalogue), "PosRcsSide", "NAME_PosRcsSide")]
    [SmfFormat("FORMAT_Thickness", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRange(typeof(VM_ProductCatalogue), "PosRcsSide", "ProductPosRcsSide")]
    [SmfUnit("UNIT_Width")]
    public double? PosRcsSide { get; set; }





    //avend26072023





  }

}

