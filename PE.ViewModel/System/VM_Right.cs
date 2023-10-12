using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Models;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_Right : VM_Base
  {
    #region ctor

    public VM_Right() { }

    public VM_Right(RoleRight data)
    {
      if (data != null)
      {
        Id = data.Id;
        RoleId = data.RoleId;
        AccessUnitId = data.AccessUnitId;
        Assigned = false;
        if (data.AccessUnit != null)
        {
          Name = data.AccessUnit.AccessUnitName;
          PermissionType = data.PermissionType;
        }
      }
    }

    public VM_Right(AccessUnit data, string roleId)
    {
      Id = 0;
      Name = data.AccessUnitName;
      AccessUnitId = data.AccessUnitId;
      RoleId = roleId;
      PermissionType = SMF.DbEntity.EnumClasses.PermissionType.View.Value;
    }

    #endregion

    #region props

    [SmfDisplay(typeof(VM_Right), "Assigned", "NAME_Assigned")]
    public virtual bool Assigned { get; set; }

    [SmfDisplay(typeof(VM_Right), "Id", "NAME_Id")]
    public virtual int Id { get; set; }

    [SmfDisplay(typeof(VM_Right), "RoleId", "NAME_RoleId")]
    public virtual string RoleId { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_Right), "AccessUnitId", "NAME_RoleAccessUnit")]
    public virtual long AccessUnitId { get; set; }

    [SmfDisplay(typeof(VM_Right), "PermissionType", "NAME_Permission")]
    public virtual short PermissionType { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_Right), "Name", "NAME_Name")]
    public virtual string Name { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_Right), "Description", "NAME_Description")]
    public virtual string Description { get; set; }

    #endregion

    #region methods

    public void CopyToEntity(ref RoleRight right, bool copyIndex)
    {
      if (copyIndex)
      {
        right.Id = Id;
      }

      right.AccessUnitId = AccessUnitId;
      right.RoleId = RoleId;
      right.PermissionType = PermissionType;
    }

    public void CopyToEntity(ref RoleRight right, string roleId, bool copyIndex)
    {
      if (copyIndex)
      {
        right.Id = Id;
      }

      right.AccessUnitId = AccessUnitId;
      right.RoleId = roleId;
      right.PermissionType = PermissionType;
    }

    #endregion
  }

  public class VM_RightList : List<VM_Right>
  {
    #region ctor

    public VM_RightList() { }

    public VM_RightList(IList<RoleRight> dbClass)
    {
      foreach (RoleRight item in dbClass)
      {
        Add(new VM_Right(item));
      }
    }

    public VM_RightList(IList<AccessUnit> dbClass, string roleId)
    {
      foreach (AccessUnit item in dbClass)
      {
        Add(new VM_Right(item, roleId));
      }
    }

    #endregion
  }
}
