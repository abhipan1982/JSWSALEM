using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_RecommendedAction : VM_Base
  {
    public VM_RecommendedAction()
    {
      UnitConverterHelper.ConvertToLocal(this);
    }

    //public VM_RecommendedAction(MNTRecomendedAction p)
    //{
    //  RecommendedActionId = p.RecomenedActionId;
    //  FkIncidentTypeId = p.FKIncidentTypeId;
    //  FkActionTypeId = p.FKActionTypeId;
    //  ActionDescription = p.ActionDescription;
    //  if (p.FKActionType != null)
    //  {
    //    ActionTypeName = p.FKActionType.ActionTypeName;
    //  }
    //  else
    //  {
    //    ActionTypeName = "-";
    //  }

    //  if (p.FKIncidentType != null)
    //  {
    //    IncidentTypeName = p.FKIncidentType.IncidentTypeName;
    //  }
    //  else
    //  {
    //    IncidentTypeName = "-";
    //  }

    //  UnitConverterHelper.ConvertToLocal(this);
    //}

    public long? RecommendedActionId { get; set; }

    [SmfDisplay(typeof(VM_RecommendedAction), "FkIncidentTypeId", "NAME_IncidentType")]
    public long? FkIncidentTypeId { get; set; }

    [SmfDisplay(typeof(VM_RecommendedAction), "FkActionTypeId", "NAME_ActionType")]
    public long? FkActionTypeId { get; set; }

    [SmfDisplay(typeof(VM_RecommendedAction), "IncidentTypeName", "NAME_IncidentType")]
    public string IncidentTypeName { get; set; }

    [SmfDisplay(typeof(VM_RecommendedAction), "ActionTypeName", "NAME_ActionType")]
    public string ActionTypeName { get; set; }

    [SmfDisplay(typeof(VM_RecommendedAction), "ActionDescription", "NAME_ActionDescription")]
    public string ActionDescription { get; set; }
  }
}
