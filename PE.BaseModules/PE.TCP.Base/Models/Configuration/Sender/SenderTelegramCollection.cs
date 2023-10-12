using System.Configuration;

namespace PE.TCP.Base.Models.Configuration.Sender
{
  [ConfigurationCollection(typeof(SenderTelegramElement))]
  public class SenderTelegramCollection : ConfigurationElementCollection
  {
    public SenderTelegramElement this[int index] => BaseGet(index) as SenderTelegramElement;

    protected override ConfigurationElement CreateNewElement()
    {
      return new SenderTelegramElement();
    }

    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((SenderTelegramElement)element).TelegramId;
    }
  }
}
