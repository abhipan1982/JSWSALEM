using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PE.DbEntity.HmiModels;
using PE.DbEntity.PEContext;
using PE.HMIWWW.Core.Resources;
using SMF.DbEntity.Models;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using SMF.Module.UnitConverter;

namespace PE.HMIWWW.Core.UnitConverter
{
  /// <summary>
  /// Converts unit symbols and measurement values collected during material tracking.
  /// </summary>
  public class FeatureUnitConverterHelper
  {
    private static readonly string _emptyUnitSymbol = "-";
    private static List<V_FeatureCustom> _customUnits = new List<V_FeatureCustom>();
    private static List<Language> _languages = new List<Language>();

    /// <summary>
    /// Converts measurements from SI units to local values in actual language version or custom values defined for specified feature.
    /// </summary>
    /// <remarks>
    /// To add custom unit of measure for selected feature update MVHFeatureCustom database table.
    /// </remarks>
    /// <param name="siModel"></param>
    public static void ConvertToLocal<T>(T siModel)
    {
      PropertyInfo[] allClassProperties = typeof(T).GetProperties();

      if (_customUnits.Count == 0)
        _customUnits = GetCustomUnits();

      foreach (PropertyInfo pi in allClassProperties)
      {
        FeatureUnitSymbolAttribute symbolAttribute = pi.GetCustomAttribute<FeatureUnitSymbolAttribute>();
        FeatureUnitValueAttribute valueAttribute = pi.GetCustomAttribute<FeatureUnitValueAttribute>();
        string localUnitCode = null;
        string localFormatCode = null;
        string unitName = null;

        if (symbolAttribute == null && valueAttribute == null) continue;

        PropertyInfo dynamicPropInfo = typeof(T).GetProperty(symbolAttribute?.UnitIdPropertyName ?? valueAttribute?.UnitIdPropertyName);
        PropertyInfo customPropInfo = typeof(T).GetProperty(symbolAttribute?.FeatureIdPropertyName ?? valueAttribute?.FeatureIdPropertyName);

        V_FeatureCustom customUnit = _customUnits
          .Where(x => x.LanguageId == GetLanguage())
          .FirstOrDefault(x => x.FeatureId == Convert.ToInt64(customPropInfo.GetValue(siModel)));

        if (customUnit == null)
        {
          double siUnitId = Convert.ToDouble(dynamicPropInfo.GetValue(siModel));

          UnitOfMeasure uom = UOMHelper.UOMCatalogue.Find(x => x.UnitId == siUnitId);
          unitName = uom.UnitCategory.CategoryName;
          unitName = unitName.Substring(0, 1).ToUpper() +
                         unitName.Substring(1, unitName.Length - 1).ToLower();
          localUnitCode = VM_Resources.ResourceManager.GetString($"UNIT_{unitName}");
          localFormatCode = VM_Resources.ResourceManager.GetString($"FORMAT_{unitName}");
        }
        else
        {
          unitName = customPropInfo.GetValue(siModel).ToString();
          localUnitCode = customUnit.CustomUnitSymbol ?? customUnit.UnitSymbol;
          localFormatCode = customUnit.CustomUnitFormat;
        }

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
          else
          {
            // TODO: throw ex ???
          }
        }
      }
    }

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
    /// Convert the value from the SI unit into the specified unit
    /// </summary>
    /// <param name="value"></param>
    /// <param name="unitId">Unit id from which the value will be converted</param>
    /// <returns></returns>
    public static double? SI2Local(double? value, long unitId)
    {
      //var watch = global::System.Diagnostics.Stopwatch.StartNew();

      if (value == null)
        return null;

      UnitOfMeasure uom = UOMHelper.UOMCatalogue.Find(x => x.UnitId == unitId);
      if (uom == null)
        return null;

      return (value * uom.Factor - uom.Shift);
    }

    /// <summary>
    /// Clear custom units list after the request.
    /// </summary>
    /// <remarks>
    /// TODO: ??? List should be stored in memory but the refresh on change method is not implemented ???
    /// TODOMN: For all of units not only custom - refresh should be on application start - on production once new unit has come - there should be anyway a restart of application
    /// once client would like to have such option - we will prepare an API GET call, which can be called from database - and it will refresh the cache.
    /// </remarks>    
    public static void ClearCustomUnitsList()
    {
      _customUnits.Clear();
    }

    private static double DoFormat(double? valueInLocal, string localFormatCode)
    {
      if (localFormatCode == null)
        return valueInLocal.GetValueOrDefault();
      string s = string.Format(localFormatCode, valueInLocal);

      return Convert.ToDouble(s);
    }

    private static List<V_FeatureCustom> GetCustomUnits()
    {
      List<V_FeatureCustom> result = new List<V_FeatureCustom>();

      using (HmiContext ctx = new HmiContext())
      {
        result = ctx.V_FeatureCustoms
          .ToList();
      }

      return result;
    }

    private static long GetLanguage()
    {
      long result;

      if (_languages.Count == 0)
      {
        using (SMFContext ctx = new SMFContext())
        {
          _languages = ctx.Languages.ToList();
        }

        _languages.ForEach(x => x.LanguageCode = x.LanguageCode.ToUpper());
      }

      result = _languages
        .First(x => x.LanguageCode.Equals(System.Globalization.CultureInfo.CurrentCulture.Name.ToUpper()))
        .LanguageId;

      return result;
    }
  }
}
