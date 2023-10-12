using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.RollSetToCassette
{
  public class VM_CassetteOverviewWithPositions : VM_Base
  {
    #region ctor

    public VM_CassetteOverviewWithPositions() { }

    public VM_CassetteOverviewWithPositions(V_CassettesOverview model, IList<V_RollSetOverview> rollsets)
    {
      Id = model.CassetteId;
      CassetteName = model.CassetteName;
      EnumCassetteStatus = model.EnumCassetteStatus;
      CassetteTypeId = model.CassetteTypeId;
      CassetteTypeName = model.CassetteTypeName;
      NumberOfPositions = model.NumberOfPositions;
      RollSetss = new List<VM_RollSetShort>();
      for (int i = 1; i <= NumberOfPositions; i++)
      {
        bool rollsetFound = false;
        foreach (V_RollSetOverview item in rollsets)
        {
          if (item.PositionInCassette == i)
          {
            rollsetFound = true;
            RollSetss.Add(new VM_RollSetShort(item));
            break;
          }
        }
        if (!rollsetFound)
        {
          //add empty row
          VM_RollSetShort rollSetShort = new VM_RollSetShort((short)i);
          RollSetss.Add(rollSetShort);
        }
      }
      StandStatus = 0;
      Arrangement = 0;
      StandId = model.StandId;
      NumberOfRolls = model.NumberOfRolls;

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region properties

    public virtual long? Id { get; set; }

    [SmfDisplay(typeof(VM_CassetteOverviewWithPositions), "CassetteName", "NAME_CassetteName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string CassetteName { get; set; }

    [SmfDisplay(typeof(VM_CassetteOverviewWithPositions), "EnumCassetteStatus", "NAME_Status")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? EnumCassetteStatus { get; set; }

    [SmfDisplay(typeof(VM_CassetteOverviewWithPositions), "CassetteTypeId", "NAME_Type")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual long? CassetteTypeId { get; set; }

    [SmfDisplay(typeof(VM_CassetteOverviewWithPositions), "CassetteTypeName", "NAME_Type")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual string CassetteTypeName { get; set; }

    [SmfDisplay(typeof(VM_CassetteOverviewWithPositions), "NumberOfPositions", "NAME_NumberOfPosition")]
    [SmfFormat("FORMAT_CassettePositions")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short NumberOfPositions { get; set; }

    public virtual List<VM_RollSetShort> RollSetss { get; set; }

    //for cassette mounting purposes
    public virtual long? StandId { get; set; }

    [SmfDisplay(typeof(VM_CassetteOverviewWithPositions), "Arrangement", "NAME_Arrangement")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? Arrangement { get; set; }

    [SmfDisplay(typeof(VM_CassetteOverviewWithPositions), "StandStatus", "NAME_StandStatus")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? StandStatus { get; set; }

    [SmfDisplay(typeof(VM_CassetteOverviewWithPositions), "NumberOfRolls", "NAME_NumberOfRolls")]
    public virtual short? NumberOfRolls { get; set; }

    #endregion
  }
}
