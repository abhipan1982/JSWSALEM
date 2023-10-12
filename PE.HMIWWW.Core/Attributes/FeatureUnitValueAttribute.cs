using System;

namespace SMF.HMIWWW.Attributes
{
  [AttributeUsage(AttributeTargets.Property)]
  public class FeatureUnitValueAttribute : Attribute
  {
    public FeatureUnitValueAttribute(string unitIdPropertyName, string featureIdPropertyName)
    {
      UnitIdPropertyName = unitIdPropertyName;
      FeatureIdPropertyName = featureIdPropertyName;
    }

    public string UnitIdPropertyName { get; set; }
    public string FeatureIdPropertyName { get; set; }
  }
}
