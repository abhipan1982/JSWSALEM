using System;
using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Models;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_Role : VM_Base
  {
    #region properties

    [Editable(false)]
    [SmfDisplay(typeof(VM_Role), "Id", "NAME_Id")]
    public virtual String Id { get; set; }

    [Required(ErrorMessageResourceName = "FORM_ATTRIBUTE_FieldIsRequired",
      ErrorMessageResourceType = typeof(VM_Resources))]
    [SmfDisplay(typeof(VM_Role), "Name", "NAME_Name")]
    public virtual String Name { get; set; }

    [SmfDisplay(typeof(VM_Role), "Description", "NAME_Description")]
    public virtual String Description { get; set; }

    [SmfDisplay(typeof(VM_Role), "NumberOfUsers", "NAME_NumberOfUsers")]
    public virtual Int32 NumberOfUsers { get; set; }

    [SmfDisplay(typeof(VM_Role), "NumberOfPermissions", "NAME_NumberOfPermissions")]
    public virtual Int32 NumberOfPermissions { get; set; }

    #endregion

    #region ctor

    public VM_Role()
    {
    }

    public VM_Role(Role role)
    {
      Id = role.Id;
      Name = role.Name;
      Description = role.Description;
      NumberOfUsers = role.Users.Count;
      NumberOfPermissions = role.RoleRights.Count;
    }

    #endregion
  }
}
