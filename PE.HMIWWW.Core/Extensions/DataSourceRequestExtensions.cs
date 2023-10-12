using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.Resources;
using SMF.HMI.Resources;
using SMF.HMIWWW.Attributes;
using SMF.Module.UnitConverter;
using Kendo.Mvc.Extensions;

namespace PE.HMIWWW.Core.Extensions
{
  public static class DataSourceRequestExtensions
  {
    public static DataSourceResult ToDataSourceLocalResult<TModel, TResult>(
      this IEnumerable<TModel> enumerable,
      DataSourceRequest request,
      ModelStateDictionary modelState, 
      Func<TModel, TResult> selector)
    {
      ConvertFiltersToSi<TResult>(request.Filters);

      ConvertFiltersToEnumClasses<TModel>(request.Filters);

      return enumerable.ToDataSourceResult<TModel, TResult>(request, modelState, selector);
    }

    public static DataSourceResult ToDataSourceLocalResult<TModel, TResult>(
      this IEnumerable<TModel> enumerable,
      DataSourceRequest request,
      ModelStateDictionary modelState)
    {
      ConvertFiltersToSi<TResult>(request.Filters);

      ConvertFiltersToEnumClasses<TModel>(request.Filters);

      return enumerable.ToDataSourceResult(request, modelState);
    }

    public static DataSourceResult ToDataSourceLocalResult<TModel, TResult>(
      this IQueryable<TModel> enumerable, DataSourceRequest request,
      ModelStateDictionary modelState, Func<TModel, TResult> selector) where TResult : class
    {
      ConvertFiltersToSi<TResult>(request.Filters);

      ConvertFiltersToEnumClasses<TModel>(request.Filters);

      return enumerable.ToDataSourceResult<TModel, TResult>(request, modelState, selector);
    }

    public static DataSourceResult ToDataSourceLocalResult<TModel, TResult>(
      this IEnumerable<TModel> enumerable, DataSourceRequest request,
      Func<TModel, TResult> selector) where TResult : class
    {
      ConvertFiltersToSi<TResult>(request.Filters);

      ConvertFiltersToEnumClasses<TModel>(request.Filters);

      return enumerable.ToDataSourceResult<TModel, TResult>(request, selector);
    }

    private static void ConvertFiltersToEnumClasses<TModel>(IList<IFilterDescriptor> filters)
    {
      if (filters == null) return;

      foreach (IFilterDescriptor filter in filters)
      {
        if (filter is FilterDescriptor descriptor)
        {
          try
          {
            PropertyInfo pi = typeof(TModel).GetProperty(descriptor.Member);

            if (!pi.PropertyType.FullName.Contains("EnumClasses") || !pi.PropertyType.IsEnumType(out string genericTypeArgumentName)) continue;

            descriptor.Value = pi.PropertyType
              .InvokeMember("GetValue",
                BindingFlags.InvokeMethod,
                null,
                null,
                new object[] {
                  typeof(Convert)
                  .InvokeMember($"To{genericTypeArgumentName}",
                    BindingFlags.InvokeMethod,
                    null,
                    null,
                    new object[] {
                      descriptor.Value
                    }) });
          }
          catch
          {
          }
        }
        else if (filter is CompositeFilterDescriptor composite)
        {
          ConvertFiltersToEnumClasses<TModel>(composite.FilterDescriptors);
        }
      }
    }

    public static bool IsEnumType(this Type type, out string genericTypeArgumentName)
    {
      while(type.BaseType != null)
      {
        if(type.BaseType.Name == typeof(SMF.DbEntity.EnumClasses.GenericEnumType<>).Name)
        {
          genericTypeArgumentName = type.BaseType.GenericTypeArguments[0].Name;

          return true;
        } 
      }

      genericTypeArgumentName = null;

      return false;
    }

    /// <summary>
    ///   Finds a Member with the "memberName" name and renames it for "newMemberName".
    /// </summary>
    /// <param name="request">The DataSourceRequest instance. <see cref="DataSourceRequest" /></param>
    /// <param name="memberName">The Name of the Filter to be renamed.</param>
    /// <param name="newMemberName">The New Name of the Filter.</param>
    public static void RenameRequestMember(this DataSourceRequest request, string memberName,
      string newMemberName)
    {
      RenameRequestFilterMember(request.Filters, memberName, newMemberName);

      foreach (SortDescriptor descriptor in request.Sorts)
      {
        if (descriptor.Member.Equals(memberName))
        {
          descriptor.Member = newMemberName;
        }
      }

      foreach (GroupDescriptor descriptor in request.Groups)
      {
        if (descriptor.Member.Equals(memberName))
        {
          object previousDisplayContent = descriptor.DisplayContent;
          descriptor.Member = newMemberName;
          descriptor.DisplayContent = previousDisplayContent;
        }
      }
    }

    /// <summary>
    ///   Finds a Sort Descriptor Member with the "memberName" name and renames it for "newMemberName".
    /// </summary>
    /// <param name="request">The DataSourceRequest instance. <see cref="DataSourceRequest" /></param>
    /// <param name="memberName">The Name of the Sort Descriptor Member to be renamed.</param>
    /// <param name="newMemberName">The New Name of the Sort Descriptor Member.</param>
    public static void RenameRequestSortDescriptor(this DataSourceRequest request, string memberName,
      string newMemberName)
    {
      foreach (SortDescriptor descriptor in request.Sorts)
      {
        if (descriptor.Member.Equals(memberName))
        {
          descriptor.Member = newMemberName;
        }
      }
    }

    /// <summary>
    ///   Finds a Filter Descriptor Member with the "memberName" name and renames it for "newMemberName" Then converts bool value to number.
    /// </summary>
    /// <param name="request">The DataSourceRequest instance. <see cref="DataSourceRequest" /></param>
    /// <param name="memberName">The Name of the Sort Descriptor Member to be renamed.</param>
    /// <param name="newMemberName">The New Name of the Sort Descriptor Member.</param>
    /// <param name="limitValue">Indicates when result should be considered as "true" or "false".</param>
    public static void FilterShortByBooleanValue(this DataSourceRequest request, string memberName,
      string newMemberName, short limitValue)
    {
      foreach (IFilterDescriptor filter in request.Filters)
      {
        if (filter is FilterDescriptor descriptor)
        {
          if (descriptor.Member.Equals(memberName))
          {
            bool filterValue = Convert.ToBoolean(descriptor.ConvertedValue);
            descriptor.Value = limitValue;
            descriptor.Member = newMemberName;
            descriptor.Operator = filterValue ? FilterOperator.IsGreaterThanOrEqualTo : descriptor.Operator = FilterOperator.IsLessThan;
          }
        }
        else if (filter is CompositeFilterDescriptor composite)
        {
          RenameRequestFilterMember(composite.FilterDescriptors, memberName, newMemberName);
        }
      }
    }

    /// <summary>
    ///   Finds a Filter Descriptor Member with the "memberName" name and renames it for "newMemberName" Then converts duration represented by string value to number of seconds.
    /// </summary>
    /// <param name="request">The DataSourceRequest instance. <see cref="DataSourceRequest" /></param>
    /// <param name="memberName">The Name of the Sort Descriptor Member to be renamed.</param>
    /// <param name="newMemberName">The New Name of the Sort Descriptor Member.</param>
    public static void FilterDurationByStringValue(this DataSourceRequest request, string memberName,
      string newMemberName)
    {
      foreach (IFilterDescriptor filter in request.Filters)
      {
        if (filter is FilterDescriptor descriptor)
        {
          if (descriptor.Member.Equals(memberName))
          {
            string durationText = Convert.ToString(descriptor.Value);
            int seconds = Convert.ToInt32(durationText[^2..]);
            durationText = durationText[0..^3];
            int minutes = Convert.ToInt32(durationText[^2..]);
            durationText = durationText[0..^3];
            int hours = Convert.ToInt32(durationText);

            descriptor.Value = seconds + (minutes * 60) + (hours * 3600);
            descriptor.Member = newMemberName;
          }
        }
        else if (filter is CompositeFilterDescriptor composite)
        {
          RenameRequestFilterMember(composite.FilterDescriptors, memberName, newMemberName);
        }
      }
    }

    private static void RenameRequestFilterMember(IList<IFilterDescriptor> filters, string memberName,
      string newMemberName)
    {
      foreach (IFilterDescriptor filter in filters)
      {
        if (filter is FilterDescriptor descriptor)
        {
          if (descriptor.Member.Equals(memberName))
          {
            descriptor.Member = newMemberName;
          }
        }
        else if (filter is CompositeFilterDescriptor composite)
        {
          RenameRequestFilterMember(composite.FilterDescriptors, memberName, newMemberName);
        }
      }
    }

    private static void ConvertFiltersToSi<TResult>(IList<IFilterDescriptor> filters)
    {
      if (filters == null) return;

      foreach (IFilterDescriptor filter in filters)
      {
        if (filter is FilterDescriptor descriptor)
        {
          try
          {
            PropertyInfo pi = typeof(TResult).GetProperty(descriptor.Member);
            SmfUnitAttribute smfUnit = pi.GetCustomAttribute<SmfUnitAttribute>();

            if (smfUnit == null || (pi.PropertyType != typeof(double?) && pi.PropertyType != typeof(double))) continue;

            string localUnitCode = VM_Resources.ResourceManager.GetString(smfUnit.UnitResourceKey);
            if (localUnitCode == null)
            {
              localUnitCode = Global.ResourceManager.GetString(smfUnit.UnitResourceKey);
            }

            double valueInLocal = Convert.ToDouble(descriptor.Value);
            double? valueInSi = UOMHelper.Local2SI(valueInLocal, localUnitCode);

            descriptor.Value = valueInSi;
          }
          catch
          {
          }
        }
        else if (filter is CompositeFilterDescriptor composite)
        {
          ConvertFiltersToSi<TResult>(composite.FilterDescriptors);
        }
      }
    }
  }
}
