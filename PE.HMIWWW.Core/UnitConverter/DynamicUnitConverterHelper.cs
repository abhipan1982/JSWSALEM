using System;
using System.Reflection;
using PE.HMIWWW.Core.Resources;
using SMF.DbEntity.Models;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using SMF.Module.UnitConverter;

namespace PE.HMIWWW.Core.UnitConverter
{
  public static class DynamicUnitConverterHelper
  {
    private static readonly string _emptyUnitSymbol = "-";

    [Obsolete("No longer in use. Please try FeatureUnitConverterHelper.ConvertToLocal instead")]
    public static void ConvertToLocal<T>(T siModel)
    {
      PropertyInfo[] allClassProperties = typeof(T).GetProperties();

      foreach (PropertyInfo pi in allClassProperties)
      {
        DynamicUnitSymbolAttribute symbolAttribute = pi.GetCustomAttribute<DynamicUnitSymbolAttribute>();
        DynamicUnitValueAttribute valueAttribute = pi.GetCustomAttribute<DynamicUnitValueAttribute>();

        if (symbolAttribute == null && valueAttribute == null)
        {
          continue;
        }


        PropertyInfo siPropInfo =
          typeof(T).GetProperty(symbolAttribute?.UnitIdPropertyName ?? valueAttribute?.UnitIdPropertyName);
        double siUnitId = Convert.ToDouble(siPropInfo.GetValue(siModel));

        UnitOfMeasure uom = UOMHelper.UOMCatalogue.Find(x => x.UnitId == siUnitId);
        string categoryName = uom.UnitCategory.CategoryName;
        categoryName = categoryName.Substring(0, 1).ToUpper() +
                       categoryName.Substring(1, categoryName.Length - 1).ToLower();

        string localUnitCode = VM_Resources.ResourceManager.GetString($"UNIT_{categoryName}");
        string localFormatCode = VM_Resources.ResourceManager.GetString($"FORMAT_{categoryName}");


        if (symbolAttribute != null)
        {
          if (!string.IsNullOrEmpty(localUnitCode))
          {
            pi.SetValue(siModel, localUnitCode);
          }
          else if (symbolAttribute.ClearIfNotFound)
          {
            pi.SetValue(siModel, _emptyUnitSymbol);
          }
        }

        if (valueAttribute != null)
        {
          double? valueInLocal = null;
          try
          {
            if (pi.GetValue(siModel) != null)
            {
              double valueInSi = Convert.ToDouble(pi.GetValue(siModel));
              valueInLocal = UOMHelper.SI2Local(valueInSi, localUnitCode);
            }
          }
          catch
          {
            // TODO
          }

          //ustawienie nowej wartosci
          if (valueInLocal != null)
          {
            valueInLocal = DoFormat(valueInLocal, localFormatCode);
            pi.SetValue(siModel, valueInLocal);
          }
        }
      }
    }

    [Obsolete("No longer in use. Please try FeatureUnitConverterHelper.ConvertToLocalNotFormatted instead")]
    public static void ConvertToLocalNotFormatted<T>(T siModel)
    {
      PropertyInfo[] allClassProperties = typeof(T).GetProperties();

      foreach (PropertyInfo pi in allClassProperties)
      {
        DynamicUnitSymbolAttribute symbolAttribute = pi.GetCustomAttribute<DynamicUnitSymbolAttribute>();
        DynamicUnitValueAttribute valueAttribute = pi.GetCustomAttribute<DynamicUnitValueAttribute>();

        if (symbolAttribute == null && valueAttribute == null)
        {
          continue;
        }

        PropertyInfo siPropInfo =
          typeof(T).GetProperty(symbolAttribute?.UnitIdPropertyName ?? valueAttribute?.UnitIdPropertyName);
        double siUnitId = Convert.ToDouble(siPropInfo.GetValue(siModel));

        UnitOfMeasure uom = UOMHelper.UOMCatalogue.Find(x => x.UnitId == siUnitId);
        string categoryName = uom.UnitCategory.CategoryName;
        categoryName = categoryName.Substring(0, 1).ToUpper() +
                       categoryName.Substring(1, categoryName.Length - 1).ToLower();

        string localUnitCode = VM_Resources.ResourceManager.GetString($"UNIT_{categoryName}");

        if (symbolAttribute != null)
        {
          if (!string.IsNullOrEmpty(localUnitCode))
          {
            pi.SetValue(siModel, localUnitCode);
          }
          else if (symbolAttribute.ClearIfNotFound)
          {
            pi.SetValue(siModel, _emptyUnitSymbol);
          }
        }

        if (valueAttribute != null)
        {
          double? valueInLocal = null;
          try
          {
            if (pi.GetValue(siModel) != null)
            {
              double valueInSi = Convert.ToDouble(pi.GetValue(siModel));
              valueInLocal = UOMHelper.SI2Local(valueInSi, localUnitCode);
            }
          }
          catch
          {
            // TODO
          }

          //ustawienie nowej wartosci
          if (valueInLocal != null)
          {
            pi.SetValue(siModel, valueInLocal);
          }
        }
      }
    }

    /// <summary>
    ///   Convert the value from the SI unit into the specified unit
    /// </summary>
    /// <param name="value"></param>
    /// <param name="unitId">Unit id from which the value will be converted</param>
    /// <returns></returns>
    [Obsolete("No longer in use. Please try FeatureUnitConverterHelper.SI2Local instead")]
    public static double? SI2Local(double? value, long unitId)
    {
      //var watch = global::System.Diagnostics.Stopwatch.StartNew();

      if (value == null)
      {
        return null;
      }

      UnitOfMeasure uom = UOMHelper.UOMCatalogue.Find(x => x.UnitId == unitId);
      if (uom == null)
      {
        return null;
      }

      return (value * uom.Factor) - uom.Shift;
    }

    private static double DoFormat(double? valueInLocal, string localFormatCode)
    {
      if (localFormatCode == null)
      {
        return valueInLocal.GetValueOrDefault();
      }

      string s = string.Format(localFormatCode, valueInLocal);

      return Convert.ToDouble(s);
    }
  }
}
