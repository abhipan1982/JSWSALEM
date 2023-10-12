using System;

namespace SMF.HMIWWW.Attributes
{
  [AttributeUsage(AttributeTargets.Property)]
  public class FeatureUnitSymbolAttribute : Attribute
  {
    public FeatureUnitSymbolAttribute(string unitIdPropertyName, string featureIdPropertyName, bool clearIfNotFound = true)
    {
      UnitIdPropertyName = unitIdPropertyName;
      FeatureIdPropertyName = featureIdPropertyName;
      ClearIfNotFound = clearIfNotFound;
    }

    public string UnitIdPropertyName { get; }
    public string FeatureIdPropertyName { get; }
    public bool ClearIfNotFound { get; }
  }
}
