using System;
using Microsoft.AspNetCore.Html;
using PE.BaseDbEntity.EnumClasses;
using PE.HMIWWW.Core.Resources;
using SMF.DbEntity.EnumClasses;

namespace PE.HMIWWW.Core.HtmlHelpers
{
  public static class ResxHelper
  {
    public static string GetResxByKey(string resxKey)
    {
      string resxValue = VM_Resources.ResourceManager.GetString(resxKey);
      if (resxValue == null || resxValue == string.Empty)
      {
        resxValue = VM_Resources.ResourceManager.GetString(resxKey);

        if (resxValue == null || resxValue == string.Empty)
        {
          resxValue = String.Format("({0}) - {1}", resxKey, VM_Resources.ResourceManager.GetString("GLOB_SNDIR"));
        }
      }

      return resxValue;
    }

    public static string GetResxByKey<T>(T name) where T : IEnumName
    {
      return GetResxByKey($"ENUM_{typeof(T).Name}_{name.Name}");
    }

    public static string GetResxButtonText(string functionName)
    {
      string resxKey = string.Format("BUTTON_{0}", functionName);
      return GetResxByKey(resxKey);
    }

    public static string GetResxAreaText(int areaCode)
    {
      string resxKey = string.Format("NAME_AREA_{0}", areaCode);
      return GetResxByKey(resxKey);
    }

    public static HtmlString GetResourceByKey(string resourceKey)
    {
      return new HtmlString(GetResxByKey(resourceKey));
    }
  }
}
