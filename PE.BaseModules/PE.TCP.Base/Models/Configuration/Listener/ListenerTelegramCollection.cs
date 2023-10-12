using System.Configuration;

namespace PE.TCP.Base.Models.Configuration.Listener
{
  [ConfigurationCollection(typeof(ListenerTelegramElement))]
  public class ListenerTelegramCollection : ConfigurationElementCollection
  {
    public ListenerTelegramElement this[int index] => BaseGet(index) as ListenerTelegramElement;

    protected override ConfigurationElement CreateNewElement()
    {
      return new ListenerTelegramElement();
    }

    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((ListenerTelegramElement)element).TelegramId;
    }
  }
}
