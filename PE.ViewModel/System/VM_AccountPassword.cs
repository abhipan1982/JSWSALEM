using System;
using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Models;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_AccountPassword : VM_Base
  {
    #region properties

    public virtual String UserName { get; set; }

    [SmfDisplay(typeof(VM_Account), "OldPassword", "NAME_OldPassword")]
    [Required(ErrorMessageResourceName = "FORM_ATTRIBUTE_FieldIsRequired", ErrorMessageResourceType = typeof(VM_Resources))]
    [StringLength(100, ErrorMessageResourceName = "FORM_ATTRIBUTE_WrongStringLength", ErrorMessageResourceType = typeof(VM_Resources), MinimumLength = 6)]
    [DataType(DataType.Password)]
    public virtual string OldPassword { get; set; }

    [SmfDisplay(typeof(VM_Account), "Password", "NAME_Password")]
    [Required(ErrorMessageResourceName = "FORM_ATTRIBUTE_FieldIsRequired", ErrorMessageResourceType = typeof(VM_Resources))]
    [StringLength(100, ErrorMessageResourceName = "FORM_ATTRIBUTE_WrongStringLength", ErrorMessageResourceType = typeof(VM_Resources), MinimumLength = 6)]
    [DataType(DataType.Password)]
    public virtual string Password { get; set; }

    [SmfDisplay(typeof(VM_Account), "ConfirmPassword", "NAME_PasswordConfirm")]
    [Required(ErrorMessageResourceName = "FORM_ATTRIBUTE_FieldIsRequired", ErrorMessageResourceType = typeof(VM_Resources))]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessageResourceName = "FORM_ATTRIBUTE_PasswordsDoNotMatch", ErrorMessageResourceType = typeof(VM_Resources))]
    public virtual string ConfirmPassword { get; set; }

    #endregion

    #region ctor

    public VM_AccountPassword(string userName, string oldPassword, string password, string confirmPassword)
    {
      UserName = userName;
      OldPassword = oldPassword;
      Password = password;
      ConfirmPassword = confirmPassword;
    }

    public VM_AccountPassword(User user)
    {
      UserName = user.UserName;
      OldPassword = "";
      Password = user.PasswordHash;
      ConfirmPassword = "";
    }

    public VM_AccountPassword()
    {
      UserName = "";
      OldPassword = "";
      Password = "";
      ConfirmPassword = "";
    }

    #endregion
  }
}
