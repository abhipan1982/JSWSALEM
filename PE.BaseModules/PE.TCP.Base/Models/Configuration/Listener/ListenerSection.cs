using System.Configuration;

namespace PE.TCP.Base.Models.Configuration.Listener
{
  public class ListenerSection : ConfigurationSection
  {
    [ConfigurationProperty("Listeners", IsDefaultCollection = true)]
    public ListenerCollection Listeners
    {
      get => (ListenerCollection)this["Listeners"];
      set => this["Listeners"] = value;
    }
  }
}
