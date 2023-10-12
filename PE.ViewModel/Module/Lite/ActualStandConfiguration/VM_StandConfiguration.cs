using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.ActualStandConfiguration
{
  public class VM_StandConfiguration : VM_Base
  {
    #region ctor

    public VM_StandConfiguration() { }

    public VM_StandConfiguration(V_ActualStandConfiguration model)
    {
      StandId = model.StandId;
      StandNo = model.StandNo;
      CassetteId = model.CassetteId;
      CassetteName = model.CassetteName;
      CassetteTypeId = model.CassetteTypeId;
      EnumStandStatus = model.EnumStandStatus;
      Arrangement = model.Arrangement ?? 0;
      StandStatusNew = model.EnumStandStatus;
      ArrangementNew = model.Arrangement;
      StandZoneName = model.StandZoneName;
      StandStatusAct = null;
      ArrangementAct = null;
      NumberOfRolls = model.NumberOfRolls;
      IsOnLine = model.IsOnLine;
      IsCalibrated = model.IsCalibrated;
      Position = model.Position;
      RollsetsNumber = model.RollsetsNumber;
      StandName = model.StandName;
      RollSetId = model.RollSetId;

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region properties

    public virtual long? StandId { get; set; }

    [SmfDisplay(typeof(VM_StandConfiguration), "StandNo", "NAME_StandNo")]
    public virtual short StandNo { get; set; }

    [SmfDisplay(typeof(VM_StandConfiguration), "CassetteId", "NAME_CassetteName")]
    public virtual long? CassetteId { get; set; }

    [SmfDisplay(typeof(VM_StandConfiguration), "CassetteName", "NAME_CassetteName")]
    public virtual string CassetteName { get; set; }

    [SmfDisplay(typeof(VM_StandConfiguration), "StandName", "NAME_StandName")]
    public virtual string StandName { get; set; }

    [SmfDisplay(typeof(VM_StandConfiguration), "CassetteTypeId", "NAME_Name")]
    public virtual long? CassetteTypeId { get; set; }

    [SmfDisplay(typeof(VM_StandConfiguration), "EnumStandStatus", "NAME_Status")]
    public virtual short EnumStandStatus { get; set; }

    [SmfDisplay(typeof(VM_StandConfiguration), "Arrangement", "NAME_Arrangement")]
    public virtual short? Arrangement { get; set; }

    [SmfDisplay(typeof(VM_StandConfiguration), "StandStatusNew", "NAME_StatusNew")]
    public virtual short? StandStatusNew { get; set; }

    [SmfDisplay(typeof(VM_StandConfiguration), "ArrangementNew", "NAME_ArrangementNew")]
    public virtual short? ArrangementNew { get; set; }

    [SmfDisplay(typeof(VM_StandConfiguration), "StandStatusAct", "NAME_StatusAct")]
    public virtual short? StandStatusAct { get; set; }

    [SmfDisplay(typeof(VM_StandConfiguration), "ArrangementAct", "NAME_ArrangementAct")]
    public virtual short? ArrangementAct { get; set; }

    [SmfDisplay(typeof(VM_StandConfiguration), "NumberofRolls", "NAME_NumberOfRolls")]
    public virtual short? NumberOfRolls { get; set; }

    [SmfDisplay(typeof(VM_StandConfiguration), "StandZoneName", "NAME_StandBlockName")]
    public virtual string StandZoneName { get; set; }

    [SmfDisplay(typeof(VM_StandConfiguration), "IsOnLine", "NAME_StandActivity")]
    public virtual bool IsOnLine { get; set; }

    public virtual bool IsCalibrated { get; set; }

    [SmfDisplay(typeof(VM_StandConfiguration), "Position", "NAME_Position")]
    public virtual short? Position { get; set; }

    public virtual int? RollsetsNumber { get; set; }

    public virtual long? RollSetId { get; set; }

    #endregion
  }
}
