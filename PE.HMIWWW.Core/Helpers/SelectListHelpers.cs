using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;
using PE.Common;
using PE.HMIWWW.Core.HtmlHelpers;
using SMF.Core.Notification;
using SMF.DbEntity.EnumClasses;

namespace PE.HMIWWW.Core.Helpers
{
  public static class SelectListHelpers
  {
    
    public static SelectList GetSelectList<T>(Type enumClassType, string stringFormat, bool exclude = true,
      params T[] excludeItems)
    {
      Dictionary<T, string> resultDictionary = new Dictionary<T, string>();

      SelectList tmpSelectList = null;
      try
      {
        foreach (var propertyItem in enumClassType.GetFields(BindingFlags.Public | BindingFlags.Static))
        {
          dynamic item = propertyItem.GetValue(null);

          T value = (T)item.Value;

          if (exclude && !excludeItems.Any(x => value.Equals(x)))
          {
            resultDictionary.Add((T)item.Value, ResxHelper.GetResxByKey(string.Format(stringFormat, item.Value)));
          }

          if (!exclude && excludeItems.Any(x => value.Equals(x)))
          {
            resultDictionary.Add((T)item.Value, ResxHelper.GetResxByKey(string.Format(stringFormat, item.Value)));
          }
        }

        tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(CommonAlarmDefs.AlarmCode_ExceptionInViewBagMethod,
          String.Format("Exception in view bag method: {0}. Exception: {1}", MethodBase.GetCurrentMethod().Name,
            ex.Message), MethodBase.GetCurrentMethod().Name, ex.Message);
        NotificationController.LogException(ex,
          String.Format("Exception in view bag method: {0}. Exception: {1}", MethodBase.GetCurrentMethod().Name,
            ex.Message));
      }

      return tmpSelectList;
    }
    
    public enum SelectListMode { Exclude, Include }
    
    public static SelectList GetSelectList<V, T>(SelectListMode mode = SelectListMode.Exclude, params T[] items) where V : IEnumName
    {
      Dictionary<T, string> resultDictionary = new Dictionary<T, string>();

      SelectList tmpSelectList = null;
      try
      {
        var fieldInfos = new List<FieldInfo>(); 

        if (typeof(V).BaseType.Name != typeof(GenericEnumType<int>).Name)
          fieldInfos.AddRange(typeof(V).BaseType.GetFields(BindingFlags.Public | BindingFlags.Static).ToList());

        fieldInfos.AddRange(typeof(V).GetFields(BindingFlags.Public | BindingFlags.Static).ToList());

        foreach (var propertyItem in fieldInfos)
        {
          dynamic item = propertyItem.GetValue(null);

          T value = (T)item.Value;

          if (mode == SelectListMode.Exclude && !items.Any(x => value.Equals(x)))
          {
            resultDictionary.Add((T)item.Value, ResxHelper.GetResxByKey(item));
          }

          if (mode == SelectListMode.Include && items.Any(x => value.Equals(x)))
          {
            resultDictionary.Add((T)item.Value, ResxHelper.GetResxByKey(item));
          }
        }

        tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(CommonAlarmDefs.AlarmCode_ExceptionInViewBagMethod,
          String.Format("Exception in view bag method: {0}. Exception: {1}", MethodBase.GetCurrentMethod().Name,
            ex.Message), MethodBase.GetCurrentMethod().Name, ex.Message);
        NotificationController.LogException(ex,
          String.Format("Exception in view bag method: {0}. Exception: {1}", MethodBase.GetCurrentMethod().Name,
            ex.Message));
      }

      return tmpSelectList;
    }
  }
}
