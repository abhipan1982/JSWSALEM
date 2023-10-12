using System;
using System.Collections.Generic;
using System.Reflection;

namespace PE.HMIWWW.Core.HtmlHelpers
{
  public static class ReflectionHelper
  {
    public static void IterateProps(Object obj, string baseProperty, ref Dictionary<string, object> properties)
    {
      if (obj != null)
      {
        Type baseType = obj.GetType();
        PropertyInfo[] props = baseType.GetProperties();
        foreach (PropertyInfo property in props)
        {
          string name = property.Name;
          Type propType = property.PropertyType;
          if (propType.IsClass && propType.Name != "String")
          {
            IterateProps(property.GetValue(obj, null), baseProperty + "." + property.Name, ref properties);
          }
          else
          {
            properties.Add(baseProperty + "." + name, property.GetValue(obj, null));
          }
        }
      }
    }
  }
}
