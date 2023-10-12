using System;
using System.Configuration;

namespace PE.TCP.Base.Models.Configuration.Listener
{
  [ConfigurationCollection(typeof(ListenerElement))]
  public class ListenerCollection : ConfigurationElementCollection
  {
    protected override string ElementName => "Listener";

    public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;

    public ListenerElement this[int index] => BaseGet(index) as ListenerElement;

    public new ListenerElement this[string key] => BaseGet(key) as ListenerElement;

    protected override ConfigurationElement CreateNewElement()
    {
      return new ListenerElement();
    }

    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((ListenerElement)element).Ip + ((ListenerElement)element).Port;
    }

    protected override bool IsElementName(string elementName)
    {
      return !String.IsNullOrEmpty(elementName) && elementName == "Listener";
    }
  }
}
