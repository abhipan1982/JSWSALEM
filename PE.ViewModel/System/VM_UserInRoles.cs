using System;
using System.Linq;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Models;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_UserInRoles : VM_Base
  {
    public VM_UserInRoles()
    {
    }

    public VM_UserInRoles(User user, Role role)
    {
      UserId = user.Id;
      UserName = user.UserName;
      RoleId = role.Id;
      Role = role.Name;
      IsAssignedToUser = user.Roles.Any(z => z.Id == role.Id);
    }

    public virtual string UserId { get; set; }

    [SmfDisplay(typeof(VM_UserInRoles), "User", "NAME_UserName")]
    public virtual string UserName { get; set; }

    [SmfDisplay(typeof(VM_UserInRoles), "Role", "NAME_Role")]
    public virtual string Role { get; set; }

    public virtual string RoleId { get; set; }

    [SmfDisplay(typeof(VM_UserInRoles), "IsAssigned", "NAME_Assigned")]
    public virtual Boolean IsAssignedToUser { get; set; }
  }
}
