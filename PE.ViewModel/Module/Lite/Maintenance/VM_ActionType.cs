using System;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_ActionType : VM_Base
  {
    public VM_ActionType()
    {
      UnitConverterHelper.ConvertToLocal(this);
    }

    //public VM_ActionType(MNTActionType p)
    //{
    //  ActionTypeId = p.ActionTypeId;
    //  CreatedTs = p.AUDCreatedTs;
    //  LastUpdateTs = p.AUDLastUpdatedTs;
    //  ActionTypeCode = p.ActionTypeCode;
    //  ActionTypeDescription = p.ActionTypeDescription;
    //  ActionTypeName = p.ActionTypeName;
    //  UnitConverterHelper.ConvertToLocal(this);
    //}

    public long? ActionTypeId { get; set; }

    [SmfDisplay(typeof(VM_ActionType), "CreatedTs", "NAME_CreatedTs")]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_ActionType), "LastUpdateTs", "NAME_LastUpdateTs")]
    public DateTime? LastUpdateTs { get; set; }

    [SmfDisplay(typeof(VM_ActionType), "ActionTypeName", "NAME_ActionTypeName")]
    public string ActionTypeName { get; set; }

    [SmfDisplay(typeof(VM_ActionType), "ActionTypeCode", "NAME_ActionTypeCode")]
    public string ActionTypeCode { get; set; }

    [SmfDisplay(typeof(VM_ActionType), "ActionTypeDescription", "NAME_ActionTypeDescription")]
    public string ActionTypeDescription { get; set; }
  }
}
