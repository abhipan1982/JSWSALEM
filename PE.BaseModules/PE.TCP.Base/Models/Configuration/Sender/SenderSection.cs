using System.Configuration;

namespace PE.TCP.Base.Models.Configuration.Sender
{
  public class SenderSection : ConfigurationSection
  {
    [ConfigurationProperty("Senders", IsDefaultCollection = true)]
    public SenderCollection Senders
    {
      get => (SenderCollection)this["Senders"];
      set => this["Senders"] = value;
    }
  }
}
