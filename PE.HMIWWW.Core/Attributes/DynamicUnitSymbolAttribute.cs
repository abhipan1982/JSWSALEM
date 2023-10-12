using System;

namespace SMF.HMIWWW.Attributes
{
  [AttributeUsage(AttributeTargets.Property)]
  public class DynamicUnitSymbolAttribute : Attribute
  {
    public DynamicUnitSymbolAttribute(string unitIdPropertyName, bool clearIfNotFound = true)
    {
      UnitIdPropertyName = unitIdPropertyName;
      ClearIfNotFound = clearIfNotFound;
    }

    public string UnitIdPropertyName { get; }
    public bool ClearIfNotFound { get; }
  }
}
