using System.Collections.Generic;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.RollSetToCassette;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.ActualStandConfiguration
{
  public class VM_SwappingCassettes : VM_Base
  {
    #region ctor

    public VM_SwappingCassettes() { }

    public VM_SwappingCassettes(RLSCassette asc, IList<V_RollSetOverview> rollsets)
    {
      Id = asc.FKStand.StandId;
      StandNo = asc.FKStand.StandNo;
      CassetteId = asc.CassetteId;
      CassetteName = asc.CassetteName;
      CassetteType = asc.FKCassetteTypeId;
      StandStatus = asc.FKStand.EnumStandStatus;
      if (StandStatus == null)
        StandStatus = 0;
      Arrangement = asc.Arrangement;
      if (Arrangement == null)
        Arrangement = 0;
      StandStatusNew = 0;
      ArrangementNew = 0;
      NumberOfPositions = asc.NumberOfPositions;


      bool rollsetFound = false;
      RollSetss = new List<VM_RollSetShort>();
      for (int i = 1; i <= NumberOfPositions; i++)
      {
        rollsetFound = false;
        foreach (V_RollSetOverview v in rollsets)
        {
          if (v.PositionInCassette == i)
          {
            rollsetFound = true;
            RollSetss.Add(new VM_RollSetShort(v));
            break;
          }
        }
        if (!rollsetFound)
        {
          //add empty row
          VM_RollSetShort rov = new VM_RollSetShort((short)i);
          RollSetss.Add(rov);
        }
      }
    }

    #endregion

    #region properties

    public virtual long? Id { get; set; }

    [SmfDisplay(typeof(VM_SwappingCassettes), "StandNo", "NAME_StandNo")]
    public virtual short StandNo { get; set; }

    [SmfDisplay(typeof(VM_SwappingCassettes), "CassetteId", "NAME_CassetteName")]
    public virtual long? CassetteId { get; set; }

    [SmfDisplay(typeof(VM_SwappingCassettes), "CassetteName", "NAME_CassetteName")]
    public virtual string CassetteName { get; set; }

    [SmfDisplay(typeof(VM_SwappingCassettes), "CassetteType", "NAME_Name")]
    public virtual long? CassetteType { get; set; }

    [SmfDisplay(typeof(VM_SwappingCassettes), "StandStatus", "NAME_Status")]
    public virtual short? StandStatus { get; set; }

    [SmfDisplay(typeof(VM_SwappingCassettes), "Arrangement", "NAME_Arrangement")]
    public virtual short? Arrangement { get; set; }


    [SmfDisplay(typeof(VM_SwappingCassettes), "NumberOfPositions", "NAME_NumberOfPosition")]
    [SmfFormat("FORMAT_CassettePositions")]
    public virtual short NumberOfPositions { get; set; }

    public virtual List<VM_RollSetShort> RollSetss { get; set; }

    [SmfDisplay(typeof(VM_SwappingCassettes), "StandStatusNew", "NAME_StatusNew")]
    public virtual short? StandStatusNew { get; set; }

    [SmfDisplay(typeof(VM_SwappingCassettes), "ArrangementNew", "NAME_ArrangementNew")]
    public virtual short? ArrangementNew { get; set; }

    [SmfDisplay(typeof(VM_SwappingCassettes), "NewCassetteId", "NAME_CassetteName")]
    public virtual long? NewCassetteId { get; set; }

    [SmfDisplay(typeof(VM_SwappingCassettes), "NewCassetteType", "NAME_Name")]
    public virtual long? NewCassetteType { get; set; }

    [SmfDisplay(typeof(VM_SwappingCassettes), "RollSetName", "NAME_Name")]
    public virtual string RollSetName { get; set; }

    #endregion
  }
}
