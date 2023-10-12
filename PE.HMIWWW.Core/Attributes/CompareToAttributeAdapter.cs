using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PE.HMIWWW.Core.Resources;

namespace PE.HMIWWW.Core.Attributes
{
  /// <summary>
  /// Contains possible comparison types for <see cref="CompareToAttribute"/>.
  /// </summary>
  public enum ComparisonType
  {
    /// <summary>
    /// Use to check equality.
    /// </summary>
    Equal,

    /// <summary>
    /// Use to check not equality.
    /// </summary>
    NotEqual,

    /// <summary>
    /// Use to check greater than.
    /// </summary>
    GreaterThan,

    /// <summary>
    /// Use to check greater than or equality.
    /// </summary>
    GreaterThanOrEqual,

    /// <summary>
    /// Use to check smaller than.
    /// </summary>
    SmallerThan,

    /// <summary>
    /// Use to check smaller than or equal.
    /// </summary>
    SmallerThanOrEqual
  }

  /// <summary>
  /// This <see cref="ValidationAttribute"/> is used to compare the decorated property value against the another property value of the same object.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
  public sealed class CompareToAttribute : ValidationAttribute
  { 
    /// <summary>
    /// Initializes a new instance of the <see cref="CompareToAttribute"/> class.
    /// </summary>
    /// <param name="comparePropertyName">Name of the property which against the comparison will be done.</param>
    /// <param name="comparisonType">The <see cref="ComparisonType"/>.</param>
    public CompareToAttribute(string comparePropertyName, ComparisonType comparisonType)
    {
      ComparePropertyName = comparePropertyName;
      ComparisonType = comparisonType;

      if (ErrorMessage == null)
      {
        SetErrorMessage(comparisonType);
      }
    }

    /// <summary>
    /// Name of the property which against the comparison will be done.
    /// </summary>
    public string ComparePropertyName { get; }

    /// <summary>
    /// The comparison type. For example: GreaterThan, GreaterThanOrEqual etc.
    /// </summary>
    public ComparisonType ComparisonType { get; }

    /// <summary>
    /// Perform the compare to validation check and returns the <see cref="ValidationResult"/>.
    /// </summary>
    /// <param name="propertyValue">Value of the input field.</param>
    /// <param name="validationContext">The validation context.</param>
    /// <returns>Retuns <see cref="ValidationResult"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="validationContext"/> is null.</exception>
    protected override ValidationResult IsValid(object propertyValue, ValidationContext validationContext)
    {
      if (validationContext == null)
      {
        throw new ArgumentNullException(nameof(validationContext));
      }

      if (propertyValue == null)
      {
        return ValidationResult.Success;
      }

      PropertyInfo comparePropertyInfo = validationContext.ObjectType.GetProperty(ComparePropertyName);

      if (comparePropertyInfo == null)
      {
        throw new ArgumentException($"The object does not contain any property with name '{ComparePropertyName}'");
      }

      Type comparePropertyType = comparePropertyInfo.PropertyType;

      if (comparePropertyType == null)
      {
        throw new ArgumentNullException($"The type of {ComparePropertyName} is null");
      }

      object comparePropertyValue = comparePropertyInfo.GetValue(validationContext.ObjectInstance, null);

      if (comparePropertyValue == null)
      {
        return ValidationResult.Success;
      }

      PropertyInfo propertyInfo = validationContext.ObjectType.GetProperty(validationContext.MemberName);

      if (propertyInfo == null)
      {
        throw new ArgumentException($"The object does not contain any property with name '{validationContext.MemberName}'");
      }

      Type propertyType = propertyInfo.PropertyType;

      if (propertyType == null)
      {
        throw new ArgumentException($"The type of {validationContext.MemberName} is null.");
      }

      propertyType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
      comparePropertyType = Nullable.GetUnderlyingType(comparePropertyType) ?? comparePropertyType;

      if (propertyType == comparePropertyType || propertyType.IsNumericType() == comparePropertyType.IsNumericType())
      {
        ValidationResult validationResult = TriggerValueComparison();
        return validationResult;
      }
      else
      {
        throw new ArgumentException($"The type of {validationContext.MemberName} is not comparable to type of {ComparePropertyName}");
      }

      ValidationResult TriggerValueComparison()
      {
        string propertyDisplayName = validationContext.DisplayName;
        DisplayNameAttribute comparePropertyDisplayAttribute = comparePropertyInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute;
        string comparePropertyDisplayName = comparePropertyDisplayAttribute?.DisplayName ?? ComparePropertyName;

        string errorMessage = GetFormattedErrorMessage(ErrorMessage, propertyDisplayName, comparePropertyDisplayName);

        // Cast value to the appropriate dynamic type.
        dynamic propertyValueDynamic;
        dynamic comparePropertyValueDynamic;

        if (propertyType.IsDateTimeType())
        {
          propertyValueDynamic = propertyValue.ToDateTime();
          comparePropertyValueDynamic = comparePropertyValue.ToDateTime();
        }
        else if (propertyType.IsNumericType())
        {
          propertyValueDynamic = Convert.ToDecimal(propertyValue, CultureInfo.InvariantCulture);
          comparePropertyValueDynamic = Convert.ToDecimal(comparePropertyValue, CultureInfo.InvariantCulture);
        }
        else if (propertyType == typeof(string))
        {
          propertyValueDynamic = propertyValue?.ToString().Length ?? 0;
          comparePropertyValueDynamic = comparePropertyValue?.ToString().Length ?? 0;
        }
        else if (propertyType.IsTimeSpanType())
        {
          propertyValueDynamic = propertyValue.ToTimeSpan();
          comparePropertyValueDynamic = comparePropertyValue.ToTimeSpan();
        }
        else
        {
          throw new Exception($"The type is not supported in {nameof(CompareToAttribute)}.");
        }

        // Do comaprison and do the required validation.
        if (ComparisonType == ComparisonType.Equal)
        {
          if (propertyValueDynamic != comparePropertyValueDynamic)
          {
            return new ValidationResult(errorMessage);
          }
        }
        else if (ComparisonType == ComparisonType.NotEqual)
        {
          if (propertyValueDynamic == comparePropertyValueDynamic)
          {
            return new ValidationResult(errorMessage);
          }
        }
        else if (ComparisonType == ComparisonType.GreaterThan)
        {
          if (propertyValueDynamic <= comparePropertyValueDynamic)
          {
            return new ValidationResult(errorMessage);
          }
        }
        else if (ComparisonType == ComparisonType.GreaterThanOrEqual)
        {
          if (propertyValueDynamic < comparePropertyValueDynamic)
          {
            return new ValidationResult(errorMessage);
          }
        }
        else if (ComparisonType == ComparisonType.SmallerThan)
        {
          if (propertyValueDynamic >= comparePropertyValueDynamic)
          {
            return new ValidationResult(errorMessage);
          }
        }
        else if (ComparisonType == ComparisonType.SmallerThanOrEqual)
        {
          if (propertyValueDynamic > comparePropertyValueDynamic)
          {
            return new ValidationResult(errorMessage);
          }
        }

        return ValidationResult.Success;
      }
    }

    private void SetErrorMessage(ComparisonType comparisonType)
    {
      switch (comparisonType)
      {
        case ComparisonType.Equal:
          ErrorMessage = VM_Resources.FORM_ATTRIBUTE_CompareEqual;
          break;
        case ComparisonType.NotEqual:
          ErrorMessage = VM_Resources.FORM_ATTRIBUTE_CompareNotEqual;
          break;
        case ComparisonType.GreaterThan:
          ErrorMessage = VM_Resources.FORM_ATTRIBUTE_CompareGreaterThan;
          break;
        case ComparisonType.GreaterThanOrEqual:
          ErrorMessage = VM_Resources.FORM_ATTRIBUTE_CompareGreaterThanOrEqual;
          break;
        case ComparisonType.SmallerThan:
          ErrorMessage = VM_Resources.FORM_ATTRIBUTE_CompareSmallerThan;
          break;
        case ComparisonType.SmallerThanOrEqual:
          ErrorMessage = VM_Resources.FORM_ATTRIBUTE_CompareSmallerThanOrEqual;
          break;
        default:
          throw new ArgumentNullException(nameof(comparisonType));
      }
    }

    private static string GetFormattedErrorMessage(string errorMessage, string propertyName, string comparePropertyName)
    {
      return string.Format(CultureInfo.InvariantCulture, errorMessage, propertyName, comparePropertyName);
    }
  }

  public static class TypeExtensions
  {
    private static readonly HashSet<Type> _numericTypes = new HashSet<Type>
        {
            typeof(sbyte),
            typeof(byte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal)
        };

    /// <summary>
    /// To check if the target type is numeric value type.
    /// </summary>
    /// <param name="type">The type to be checked.</param>
    /// <returns>Returns <see langword="true"/> or <see langword="false"/>.</returns>
    public static bool IsNumericType(this Type type)
    {
      return _numericTypes.Contains(type) || _numericTypes.Contains(Nullable.GetUnderlyingType(type));
    }

    /// <summary>
    /// To check if the target type is a <see cref="DateTime"/> value type.
    /// </summary>
    /// <param name="type">The type to be checked.</param>
    /// <returns>Returns <see langword="true"/> or <see langword="false"/>.</returns>
    public static bool IsDateTimeType(this Type type)
    {
      return type == typeof(DateTime) || type == typeof(DateTime?);
    }

    /// <summary>
    /// To check if the target type is <see cref="TimeSpan"/> value type.
    /// </summary>
    /// <param name="type">The type to be checked.</param>
    /// <returns>Returns <see langword="true"/> or <see langword="false"/>.</returns>
    public static bool IsTimeSpanType(this Type type)
    {
      return type == typeof(TimeSpan) || type == typeof(TimeSpan?);
    }
    internal static TimeSpan? ToTimeSpan(this object value)
    {
      TimeSpan? timeSpan = null;

      if (value != null)
      {
        if (TimeSpan.TryParse(value.ToString(), out TimeSpan outTimeSpan))
        {
          timeSpan = outTimeSpan;
        }
        else
        {
          throw new FormatException("The provided string is not a valid timespan.");
        }
      }

      return timeSpan;
    }

    internal static DateTime? ToDateTime(this object value)
    {
      DateTime? dateTime = null;

      if (value != null)
      {
        if (DateTime.TryParse(value.ToString(), out DateTime outDateTime))
        {
          dateTime = outDateTime;
        }
        else
        {
          throw new FormatException("The string was not recognized as a valid DateTime. String format should be: 01-Jan-2020 or 01-Jan-2020 10:00:00 AM");
        }
      }

      return dateTime;
    }
  }
}
