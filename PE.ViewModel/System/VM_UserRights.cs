using System;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Models;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_UserRights : VM_Base
  {
    #region ctor

    public VM_UserRights() { }

    public VM_UserRights(VM_UserRights data)
    {
      if (data != null)
      {
        Name = data.Name;
        Method = data.Method;
        AccessName = data.AccessName;
      }
    }

    public VM_UserRights(AccessUnit data)
    {
      if (data != null)
      {
        Name = data.AccessUnitName;
      }
    }

    #endregion

    #region props

    [SmfDisplay(typeof(VM_UserRights), "Controller", "NAME_Controller")]
    public virtual string Name { get; set; }

    [SmfDisplay(typeof(VM_UserRights), "Method", "NAME_Method")]
    public virtual string Method { get; set; }

    [SmfDisplay(typeof(VM_UserRights), "AccessName", "NAME_AccessName")]
    public virtual string AccessName { get; set; }

    public virtual SMF.DbEntity.Models.Module Module { get; set; }

    #endregion
  }
}
