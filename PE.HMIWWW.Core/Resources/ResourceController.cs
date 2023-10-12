using System;
using System.Globalization;

namespace PE.HMIWWW.Core.Resources
{
  public static class ResourceController
  {
    public static string GetMenuDisplayName(string nameFromDb)
    {
      string displayName = VM_Resources.ResourceManager.GetString(String.Format("HMI_MENU_{0}", nameFromDb));
      if (displayName == null)
      {
        displayName = String.Format("HMI_MENU_{0} - [Translation needed in PE.HMIWWW.Core.Resources]", nameFromDb);
      }

      return displayName;
    }

    public static string GetResourceTextByResourceKey(string resourceKey)
    {
      string resourceString = VM_Resources.ResourceManager.GetString(String.Format("{0}", resourceKey));
      if (resourceString == null)
      {
        resourceString = String.Format("{0} - [Translation needed in PE.HMIWWW.Core.Resources]", resourceKey);
      }

      return resourceString;
    }

    public static string GetGlobalResourceTextByResourceKey(string resourceKey)
    {
      string resourceString = VM_Resources.ResourceManager.GetString(String.Format("GLOB_{0}", resourceKey));
      if (resourceString == null)
      {
        resourceString = String.Format("GLOB_{0} - [Translation needed in PE.HMIWWW.Core.Resources]", resourceKey);
      }

      return resourceString;
    }

    public static string GetErrorText(string resourceKey)
    {
      string resourceString = VM_Resources.ResourceManager.GetString(String.Format("ERROR_{0}", resourceKey));
      if (resourceString == null)
      {
        resourceString = String.Format("ERROR_{0} - [Translation needed in PE.HMIWWW.Core.Resources]", resourceKey);
      }

      return resourceString;
    }

    public static string GetPageTitleValue(string controller, string action)
    {
      string resourceString =
        VM_Resources.ResourceManager.GetString(string.Format("PAGE_TITLE_{0}_{1}", controller, action));
      if (resourceString == null)
      {
        resourceString = string.Format("Resource PAGE_TITLE_{0}_{1} not defined in PE.HMIWWW.Core.Resources",
          controller, action);
      }

      return resourceString;
    }

    public static string GetBilletMillDirectionValue(string value)
    {
      string resourceString =
        VM_Resources.ResourceManager.GetString(string.Format("GLOB_BilletMillDirection_{0}", value));
      if (resourceString == null)
      {
        resourceString = string.Format("Resource GLOB_BilletMillDirection_{0}", value);
      }

      return resourceString;
    }

    public static string GetMeasurementValueName(string value)
    {
      string resourceString = VM_Resources.ResourceManager.GetString(string.Format("GLOB_MeasuredValueIds_{0}", value));
      if (resourceString == null)
      {
        resourceString = string.Format("Resource GLOB_MeasuredValueIds_{0}", value);
      }

      return resourceString;
    }
  }
}
