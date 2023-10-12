using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Furnace;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Asset
{
  public class VM_Asset : VM_Base
  {
    public VM_Asset(V_Asset table)
    {
      OrderSeq = table.OrderSeq;
      ParentAssetId = table.ParentAssetId;
      AssetCode = table.AssetCode;
      AssetName = table.AssetName;
      AssetDescription = table.AssetDescription;
      AreaCode = table.AreaCode;
      AreaName = table.AreaName;
      ZoneCode = table.ZoneCode;
      ZoneName = table.ZoneName;
      IsDelayCheckpoint = table.IsDelayCheckpoint;
      AssetOrderSeq = table.AssetOrderSeq;
      Levels = table.Levels;
      Path = table.Path;
      AssetId = table.AssetId;
    }

    public VM_Asset(V_Asset table, List<VM_RawMaterialOverview> materialsInArea)
    {
      OrderSeq = table.OrderSeq;
      ParentAssetId = table.ParentAssetId;
      AssetCode = table.AssetCode;
      AssetName = table.AssetName;
      AssetDescription = table.AssetDescription;
      AreaCode = table.AreaCode;
      AreaName = table.AreaName;
      ZoneCode = table.ZoneCode;
      ZoneName = table.ZoneName;
      IsDelayCheckpoint = table.IsDelayCheckpoint;
      AssetOrderSeq = table.AssetOrderSeq;
      Levels = table.Levels;
      Path = table.Path;
      AssetId = table.AssetId;
      MaterialsInArea = materialsInArea;
    }

    public VM_Asset(V_Asset table, List<VM_Furnace> materialsInFurnace)
    {
      OrderSeq = table.OrderSeq;
      ParentAssetId = table.ParentAssetId;
      AssetCode = table.AssetCode;
      AssetName = table.AssetName;
      AssetDescription = table.AssetDescription;
      AreaCode = table.AreaCode;
      AreaName = table.AreaName;
      ZoneCode = table.ZoneCode;
      ZoneName = table.ZoneName;
      IsDelayCheckpoint = table.IsDelayCheckpoint;
      AssetOrderSeq = table.AssetOrderSeq;
      Levels = table.Levels;
      Path = table.Path;
      AssetId = table.AssetId;
      MaterialsInFurnace = materialsInFurnace;
    }

    public VM_Asset()
    {
    }

    [SmfDisplay(typeof(VM_Asset), "AssetCode", "NAME_AssetCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int AssetCode { get; set; }

    [SmfDisplay(typeof(VM_Asset), "AssetName", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetName { get; set; }

    [SmfDisplay(typeof(VM_Asset), "AssetDescription", "NAME_AssetDescription")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetDescription { get; set; }

    [SmfDisplay(typeof(VM_Asset), "AreaName", "NAME_AreaName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AreaName { get; set; }

    [SmfDisplay(typeof(VM_Asset), "AreaCode", "NAME_AreaCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? AreaCode { get; set; }


    [SmfDisplay(typeof(VM_Asset), "ZoneName", "NAME_ZoneName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ZoneName { get; set; }

    [SmfDisplay(typeof(VM_Asset), "ZoneCode", "NAME_ZoneCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? ZoneCode { get; set; }

    [SmfDisplay(typeof(VM_Asset), "IsCheckpoint", "NAME_IsCheckpoint")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsDelayCheckpoint { get; set; }

    [SmfDisplay(typeof(VM_Asset), "AssetOrderSeq", "NAME_AssetOrderSeq")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int AssetOrderSeq { get; set; }

    public long? ParentAssetId { get; set; }

    [SmfDisplay(typeof(VM_Asset), "Levels", "NAME_Levels")]
    //[DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Levels { get; set; }


    [SmfDisplay(typeof(VM_Asset), "Path", "NAME_Path")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string Path { get; set; }


    [SmfDisplay(typeof(VM_Asset), "Seq", "Seq")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? OrderSeq { get; set; }

    [SmfDisplay(typeof(VM_Asset), "AssetId", "NAME_AssetId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long AssetId { get; set; }

    public List<VM_RawMaterialOverview> MaterialsInArea { get; set; }

    public List<VM_Furnace> MaterialsInFurnace { get; set; }

    public bool AreaSelected { get; set; }
  }
}
