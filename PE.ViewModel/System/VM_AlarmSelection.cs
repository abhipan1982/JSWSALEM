using System;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Models;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_AlarmSelection : VM_Base
  {
    public VM_AlarmSelection() { }

    public VM_AlarmSelection(string alarmOwner, bool? toConfirm)
    {
      AlarmOwner = alarmOwner;
      ToConfirm = toConfirm;
    }

    [SmfDisplay(typeof(VM_AlarmItem), "ToConfirm", "NAME_ToConfirm")]
    public virtual bool? ToConfirm { get; set; }

    [SmfDisplay(typeof(VM_AlarmItem), "AlarmOwner", "NAME_AlarmOwner")]
    public virtual String AlarmOwner { get; set; }
  }
}
