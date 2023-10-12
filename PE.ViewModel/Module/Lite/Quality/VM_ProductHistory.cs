using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Quality
{
  internal class UnitAttribute : Attribute
  {
    public UnitAttribute(string value)
    {
      Value = value;
    }

    public string Value { get; set; }
  }

  public class VM_ProductHistory : VM_Base
  {
    public VM_ProductHistory() { }

    //public VM_ProductHistory(V_ProductionHistory r)
    //{
    //  FKShapeId = r.FKShapeId;
    //  HeatId = r.HeatId;
    //  HeatName = r.HeatName;
    //  NumDefects = r.NumDefects ?? 0;
    //  ProductCatalogueId = r.ProductCatalogueId;
    //  ProductCatalogueName = r.ProductCatalogueName;
    //  ProductCatalogueTypeId = r.ProductCatalogueTypeId;
    //  ProductCatalogueTypeName = r.ProductCatalogueTypeName;
    //  ProductCatalogueTypeCode = r.ProductCatalogueTypeCode;
    //  ProductCreated = r.ProductCreated;
    //  ProductId = r.ProductId;
    //  ProductName = r.ProductName;
    //  EnumQuality = r.EnumQuality;
    //  ShapeName = r.ShapeName;
    //  ShapeCode = r.ShapeCode;
    //  SteelgradeCode = r.SteelgradeCode;
    //  SteelgradeId = r.SteelgradeId;
    //  SteelgradeName = r.SteelgradeName;
    //  Thickness = r.Thickness;
    //  ProductWeight = r.ProductWeight;
    //  Width = r.Width;
    //  WorkOrderId = r.WorkOrderId;
    //  WorkOrderName = r.WorkOrderName;
    //  WorkOrderStatus = r.WorkOrderStatus;

    //  //UnitConverterHelper.ConvertToLocal(v);
    //  UnitConverterHelper.ConvertToLocal(this);
    //  Profile = GenerateProfileCode(Thickness, Width);
    //}

    [SmfDisplay(typeof(VM_ProductHistory), "ProductId", "NAME_ProductId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long ProductId { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "ProductCreated", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime ProductCreated { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "ProductName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-;", HtmlEncode = false)]
    public string ProductName { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "Weight", "NAME_Weight")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Weight")]
    [SmfFormat("FORMAT_ProductWeightKg")]
    public double? ProductWeight { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "QualityEnum", "NAME_Quality")]
    public short? EnumQuality { get; set; }

    public long WorkOrderId { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "WorkOrderName", "NAME_WorkOrderName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "WorkOrderStatus", "NAME_OrderStatus")]
    public short? WorkOrderStatus { get; set; }

    public long ProductCatalogueId { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "ProductCatalogueName", "NAME_ProductCatalogue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductCatalogueName { get; set; }

    public long FKShapeId { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "ShapeSymbol", "NAME_ShapeCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ShapeCode { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "ShapeName", "NAME_Shape")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ShapeName { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "Profile", "NAME_Profile")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string
      Profile { get; set; } //artificially constructed out of thickness and width reference from Product Catalogue

    [SmfDisplay(typeof(VM_ProductHistory), "Thickness", "NAME_Thickness")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_ProductThickness")]
    [SmfFormat("FORMAT_Thickness")]

    public double? Thickness { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "Width", "NAME_Width")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Width")]
    [SmfFormat("FORMAT_Width")]
    public double? Width { get; set; }

    public long SteelgradeId { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "SteelgradeCode", "NAME_SteelgradeCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeCode { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "SteelgradeName", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeName { get; set; }

    public long HeatId { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "NumDefects", "NAME_NumberOfDefectsShort")]
    public long NumDefects { get; set; }

    public long ProductCatalogueTypeId { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "ProductCatalogueTypeSymbol", "NAME_ProductCatalogueType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductCatalogueTypeCode { get; set; }

    [SmfDisplay(typeof(VM_ProductHistory), "ProductCatalogueTypeName", "NAME_ProductCatalogueType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ProductCatalogueTypeName { get; set; }

    private string GenerateProfileCode(double? thickness, double? width)
    {
      StringBuilder sb = new StringBuilder("?");
      if (thickness != null)
      {
        sb.Clear();
        sb.Append(String.Format("{0:N1}", thickness));
        if (width != null)
        {
          sb.Append("x").Append(String.Format("{0:N1}", width));
        }
      }
      else
      {
        if (width != null)
        {
          sb.Append(String.Format("{0:N1}", width));
        }
      }

      return sb.ToString();
    }
  }
}
