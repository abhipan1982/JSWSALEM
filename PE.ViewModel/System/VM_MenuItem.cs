using System.Collections.Generic;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Models;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_Menu : VM_Base
  {
    #region ctor

    public VM_Menu() { }

    #endregion

    #region props

    public List<VM_LanguageItem> Languages { get; set; }

    public VM_MenuItemList Menuitems { get; set; }

    #endregion
  }

  public class VM_LanguageItem : VM_Base
  {
    #region ctor

    public VM_LanguageItem() { }

    public VM_LanguageItem(Language data)
    {
      Code = data.LanguageCode;
      DisplayName = data.LanguageName;
      IconName = data.IconName;
      Order = data.Order;
    }

    #endregion

    #region props

    public virtual string Code { get; set; }

    public virtual string DisplayName { get; set; }

    public virtual string IconName { get; set; }

    public virtual int Order { get; set; }

    #endregion
  }

  public class VM_LanguageItemList : List<VM_LanguageItem>
  {
    #region ctor

    public VM_LanguageItemList(IEnumerable<Language> dbClass)
    {
      foreach (Language item in dbClass)
      {
        Add(new VM_LanguageItem(item));
      }
    }

    #endregion
  }

  public class VM_MenuItem : VM_Base
  {
    #region ctor

    public VM_MenuItem() { }

    public VM_MenuItem(DbEntity.SPModels.SPUserMenu data)
    {
      Id = data.HmiClientMenuId;
      Name = data.HmiClientMenuName;
      DisplayName = ResourceController.GetMenuDisplayName(data.HmiClientMenuName);
      Area = data.Area;
      Controller = data.ControllerName;
      Method = data.Method;
      ParentId = data.ParentHmiClientMenuId;
      DisplayOrder = data.DisplayOrder;
      MethodParameter = data.MethodParameter;
      IconName = data.IconName;
    }

    #endregion

    #region props
    public virtual long Id { get; set; }

    public virtual string Name { get; set; }

    public virtual string DisplayName { get; set; }

    public virtual string Area { get; set; }
    
    public virtual string Controller { get; set; }

    public virtual string Method { get; set; }

    public virtual long? ParentId { get; set; }

    public virtual long? DisplayOrder { get; set; }

    public virtual string MethodParameter { get; set; }

    public virtual VM_MenuItemList Children { get; set; }

    public virtual string IconName { get; set; }

    #endregion
  }

  public class VM_MenuItemList : List<VM_MenuItem>
  {
    #region ctor

    public VM_MenuItemList() { }

    public VM_MenuItemList(IList<DbEntity.SPModels.SPUserMenu> data)
    {
      foreach (DbEntity.SPModels.SPUserMenu item in data)
      {
        Add(new VM_MenuItem(item));
      }
    }

    #endregion
  }
}
