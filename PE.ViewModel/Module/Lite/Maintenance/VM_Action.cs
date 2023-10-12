using System;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_Action : VM_Base
  {
    public VM_Action()
    {
      UnitConverterHelper.ConvertToLocal(this);
    }

    //public VM_Action(MNTAction p)
    //{
    //  ActionId = p.ActionId;
    //  //TODOMN - exclude this
    //  //this.CreatedTs = p.CreatedTs;
    //  //this.LastUpdateTs = p.LastUpdateTs;
    //  FkActionTypeId = p.FKActionTypeId;
    //  FkIncidentId = p.FKIncidentId;

    //  FkMemberId = p.FKMemberId;
    //  ActionDescription = p.ActionDescription;
    //  EnumActionStatus = p.EnumActionStatus;
    //  ActionTs = p.ActionTs;
    //  UnitConverterHelper.ConvertToLocal(this);
    //}

    public long? ActionId { get; set; }

    [SmfDisplay(typeof(VM_RecommendedAction), "FkIncidentTypeId", "NAME_IncidentType")]
    public long? FkIncidentTypeId { get; set; }

    [SmfDisplay(typeof(VM_RecommendedAction), "FkActionTypeId", "NAME_ActionType")]
    public long? FkActionTypeId { get; set; }

    [SmfDisplay(typeof(VM_RecommendedAction), "ActionDescription", "NAME_ActionDescription")]
    public string ActionDescription { get; set; }

    [SmfDisplay(typeof(VM_RecommendedAction), "CreatedTs", "NAME_CreatedTs")]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_RecommendedAction), "LastUpdateTs", "NAME_LastUpdateTs")]
    public DateTime? LastUpdateTs { get; set; }

    [SmfDisplay(typeof(VM_RecommendedAction), "ActionTs", "NAME_ActionTs")]
    public DateTime? ActionTs { get; set; }

    [SmfDisplay(typeof(VM_RecommendedAction), "EnumActionStatus", "NAME_EnumActionStatus")]
    public short? EnumActionStatus { get; set; }

    [SmfDisplay(typeof(VM_RecommendedAction), "FkIncidentId", "NAME_Incident")]
    public long? FkIncidentId { get; set; }

    [SmfDisplay(typeof(VM_RecommendedAction), "FkMemberId", "NAME_Member")]
    public long? FkMemberId { get; set; }
  }
}
