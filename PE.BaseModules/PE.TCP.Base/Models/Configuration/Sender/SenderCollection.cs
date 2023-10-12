using System;
using System.Configuration;

namespace PE.TCP.Base.Models.Configuration.Sender
{
  [ConfigurationCollection(typeof(SenderElement))]
  public class SenderCollection : ConfigurationElementCollection
  {
    protected override string ElementName => "Sender";

    public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;

    public SenderElement this[int index] => BaseGet(index) as SenderElement;

    public new SenderElement this[string key] => BaseGet(key) as SenderElement;

    protected override ConfigurationElement CreateNewElement()
    {
      return new SenderElement();
    }

    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((SenderElement)element).Ip + ((SenderElement)element).Port;
    }

    protected override bool IsElementName(string elementName)
    {
      return !String.IsNullOrEmpty(elementName) && elementName == "Sender";
    }
  }
}
