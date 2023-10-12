using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Cassette
{
  public class VM_CassetteOverview : VM_Base
  {
    #region ctor

    public VM_CassetteOverview() { }

    public VM_CassetteOverview(V_CassettesOverview model)
    {
      CassetteId = model.CassetteId;
      CassetteName = model.CassetteName;
      EnumCassetteStatus = model.EnumCassetteStatus;
      CassetteTypeId = model.CassetteTypeId;
      CassetteTypeName = model.CassetteTypeName;
      NumberOfPositions = model.NumberOfPositions;
      StandNo = model.StandNo;
      StandName = model.StandName;
      Arrangement = model.Arrangement;
      NewStatus = model.EnumCassetteStatus;
      NumberOfRolls = model.NumberOfRolls;
      StatusTxt = ResxHelper.GetResxByKey(CassetteStatus.GetValue(model.EnumCassetteStatus));
      Editable = model.EnumCassetteStatus == CassetteStatus.Undefined.Value
        || model.EnumCassetteStatus == CassetteStatus.New.Value
        || model.EnumCassetteStatus == CassetteStatus.Empty.Value
        || model.EnumCassetteStatus == CassetteStatus.InRegeneration.Value
        || model.EnumCassetteStatus == CassetteStatus.NotAvailable.Value;
      Dismountable = model.EnumCassetteStatus == CassetteStatus.RollSetInside.Value;

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region properties

    public virtual long? CassetteId { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_CassetteOverview), "CassetteName", "NAME_CassetteName")]
    public virtual string CassetteName { get; set; }

    [SmfDisplay(typeof(VM_CassetteOverview), "EnumCassetteStatus", "NAME_Status")]
    public virtual short EnumCassetteStatus { get; set; }

    [SmfDisplay(typeof(VM_CassetteOverview), "StatusTxt", "NAME_Status")]
    public virtual string StatusTxt { get; set; }

    [SmfDisplay(typeof(VM_CassetteOverview), "CassetteTypeId", "NAME_Type")]
    public virtual long? CassetteTypeId { get; set; }

    [SmfDisplay(typeof(VM_CassetteOverview), "CassetteTypeName", "NAME_Type")]
    public virtual string CassetteTypeName { get; set; }

    [SmfDisplay(typeof(VM_CassetteOverview), "NumberOfPositions", "NAME_NumberOfPosition")]
    [SmfFormat("FORMAT_CassettePositions")]
    public virtual short? NumberOfPositions { get; set; }

    [SmfDisplay(typeof(VM_CassetteOverview), "StandNo", "NAME_StandNo")]
    public virtual short? StandNo { get; set; }

    [SmfDisplay(typeof(VM_CassetteOverview), "StandName", "NAME_StandName")]
    public virtual string StandName { get; set; }

    [SmfDisplay(typeof(VM_CassetteOverview), "Arrangement", "NAME_Arrangement")]
    public virtual short Arrangement { get; set; }

    [SmfDisplay(typeof(VM_CassetteOverview), "NewStatus", "NAME_StatusNew")]
    public virtual short? NewStatus { get; set; }

    [SmfDisplay(typeof(VM_CassetteOverview), "NumberOfRolls", "NAME_NumberOfRolls")]
    public virtual short? NumberOfRolls { get; set; }

    public bool Editable { get; set; }

    public bool Dismountable { get; set; }

    #endregion
  }
}
