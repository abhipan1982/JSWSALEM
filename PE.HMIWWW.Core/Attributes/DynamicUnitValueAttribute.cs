using System;

namespace SMF.HMIWWW.Attributes
{
  [AttributeUsage(AttributeTargets.Property)]
  public class DynamicUnitValueAttribute : Attribute
  {
    public DynamicUnitValueAttribute(string unitIdPropertyName)
    {
      UnitIdPropertyName = unitIdPropertyName;
    }

    public string UnitIdPropertyName { get; set; }
  }
}
