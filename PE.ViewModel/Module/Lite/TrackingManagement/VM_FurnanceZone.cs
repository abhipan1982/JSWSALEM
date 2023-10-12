using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.TrackingManagement
{
  public class VM_FurnanceZone : VM_Base
  {
    #region ctor

    public VM_FurnanceZone(IGrouping<short, V_RawMaterialLocation> groupedRawMaterialLocations)
    {
      V_RawMaterialLocation location = groupedRawMaterialLocations.First();
      AssetId = location.AssetId;
      AssetCode = location.AssetCode;
      AssetDescription = location.AssetDescription;
      AssetName = location.AssetName;
      IsArea = location.IsArea;
      PositionSlots = groupedRawMaterialLocations.OrderBy(x => x.OrderSeq).Select(x => new VM_MaterialPosition(x));
      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region prop

    [SmfDisplay(typeof(VM_FurnanceZone), "AssetId", "NAME_AssetId")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long AssetId { get; set; }

    [SmfDisplay(typeof(VM_FurnanceZone), "AssetCode", "NAME_AssetCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int AssetCode { get; set; }

    [SmfDisplay(typeof(VM_FurnanceZone), "AssetDescription", "NAME_AssetDescription")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetDescription { get; set; }

    [SmfDisplay(typeof(VM_FurnanceZone), "AssetName", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetName { get; set; }

    [SmfDisplay(typeof(VM_FurnanceZone), "IsArea", "NAME_IsArea")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public bool IsArea { get; set; }

    public IEnumerable<VM_MaterialPosition> PositionSlots { get; set; }

    #endregion
  }
}
