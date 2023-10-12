using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.BilletYard
{
  public class VM_HeatsOnYards : VM_Base
  {
    public VM_HeatsOnYards()
    {
    }

    public VM_HeatsOnYards(V_HeatsOnYard data)
    {
      YardId = data.AreaId;
      AreaDescription = data.AreaDescription;
      HeatId = data.HeatId;
      HeatName = data.HeatName;
      SteelgradeId = data.SteelgradeId;
      SteelgradeName = data.SteelgradeName;
      NumberOfMaterials = data.MaterialsOnArea;
      MaterialsOnArea = data.MaterialsOnArea;
    }

    public VM_HeatsOnYards(V_HeatsByGroupOnYard data)
    {
      YardId = data.AreaId;
      AreaDescription = data.AreaDescription;
      AssetId = data.AssetId;
      AssetDescription = data.AssetDescription;
      HeatId = data.HeatId;
      HeatName = data.HeatName;
      SteelgradeId = data.SteelgradeId;
      SteelgradeName = data.SteelgradeName;
      NumberOfMaterials = data.MaterialsByGroupOnArea;
      IsFirstInQueue = data.IsFirstInQueue;
      IsFirstInQueueShort = IsFirstInQueue ?? true ? (short)1 : (short)0;
      HeatWeight = data.HeatWeight;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long HeatId { get; set; }

    [SmfDisplay(typeof(VM_HeatsOnYards), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }

    public long? SteelgradeId { get; set; }

    [SmfDisplay(typeof(VM_HeatsOnYards), "SteelgradeName", "NAME_Steelgrade")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SteelgradeName { get; set; }

    public long? YardId { get; set; }

    [SmfDisplay(typeof(VM_HeatsOnYards), "AreaDescription", "NAME_Yard")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AreaDescription { get; set; }

    public string AreaName { get; set; }

    public long? AssetId { get; set; }

    [SmfDisplay(typeof(VM_HeatsOnYards), "AssetDescription", "NAME_Location")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetDescription { get; set; }

    public bool? IsFirstInQueue { get; set; }

    public short IsFirstInQueueShort { get; set; }


    [SmfDisplay(typeof(VM_HeatsOnYards), "NumberOfMaterials", "NAME_MaterialsShort")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? NumberOfMaterials { get; set; }

    [SmfDisplay(typeof(VM_HeatsOnYards), "NumberOfMaterials", "NAME_MaterialsShort")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int? MaterialsOnArea { get; set; }

    [SmfDisplay(typeof(VM_HeatsOnYards), "HeatWeight", "NAME_Weight")]
    [SmfFormat("FORMAT_Weight", NullDisplayText = "-")]
    [SmfUnit("UNIT_Weight")]
    public double? HeatWeight { get; set; }
  }
}
