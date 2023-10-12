using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.RollChange
{
  public class VM_StandGrid : VM_Base
  {
    #region properties

    public long? CassetteId { get; set; }
    public long? StandId { get; set; }
    public short? Position { get; set; }

    [SmfDisplay(typeof(VM_StandGrid), "StandStatus", "NAME_StandStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short StandStatus { get; set; }

    [SmfDisplay(typeof(VM_StandGrid), "CassetteName", "NAME_CassetteName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string CassetteName { get; set; }

    [SmfDisplay(typeof(VM_StandGrid), "CassetteTypeName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual String CassetteTypeName { get; set; }

    [SmfDisplay(typeof(VM_StandGrid), "StandNo", "NAME_StandNo")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short? StandNo { get; set; }

    [SmfDisplay(typeof(VM_StandGrid), "StandZoneName", "NAME_StandZoneName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public string StandZoneName { get; set; }

    [SmfDisplay(typeof(VM_StandGrid), "ArrangementString", "NAME_Arrangement")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public short? Arrangement { get; set; }

    #endregion

    #region ctor

    public VM_StandGrid()
    {
    }

    public VM_StandGrid(V_CassettesInStand entity)
    {
      CassetteId = entity.CassetteId;
      StandId = entity.StandId;
      CassetteName = entity.CassetteName;
      CassetteTypeName = entity.CassetteTypeName;
      StandNo = entity.StandNo;
      StandZoneName = entity.StandZoneName;
      Position = entity.Position;
      StandStatus = entity.EnumStandStatus;
      Arrangement = entity.Arrangement;

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion
  }
}
