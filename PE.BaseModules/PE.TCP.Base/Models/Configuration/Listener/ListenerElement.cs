using System.Configuration;

namespace PE.TCP.Base.Models.Configuration.Listener
{
  public class ListenerElement : ConfigurationElement
  {
    [ConfigurationProperty("ip", IsKey = true, IsRequired = true)]
    public string Ip
    {
      get => this["ip"].ToString();
      set => this["ip"] = value;
    }

    [ConfigurationProperty("port", IsKey = true, IsRequired = true)]
    public int Port
    {
      get => (int)this["port"];
      set => this["port"] = value;
    }

    [ConfigurationProperty("telegramIdLen", IsKey = true, IsRequired = true)]
    public int TelegramIdLength
    {
      get => (int)this["telegramIdLen"];
      set => this["telegramIdLen"] = value;
    }

    [ConfigurationProperty("telegramIdOffset", IsKey = true, IsRequired = true)]
    public int TelegramIdOffset
    {
      get => (int)this["telegramIdOffset"];
      set => this["telegramIdOffset"] = value;
    }

    [ConfigurationProperty("alive", IsKey = true, IsRequired = true)]
    public int Alive
    {
      get => (int)this["alive"];
      set => this["alive"] = value;
    }

    [ConfigurationProperty("alivecycle", IsKey = true, IsRequired = true)]
    public int AliveCycle
    {
      get => (int)this["alivecycle"];
      set => this["alivecycle"] = value;
    }

    [ConfigurationProperty("aliveId", IsKey = true, IsRequired = true)]
    public int AliveId
    {
      get => (int)this["aliveId"];
      set => this["aliveId"] = value;
    }

    [ConfigurationProperty("aliveOffset", IsKey = true, IsRequired = true)]
    public int AliveOffset
    {
      get => (int)this["aliveOffset"];
      set => this["aliveOffset"] = value;
    }

    [ConfigurationProperty("aliveLen", IsKey = true, IsRequired = true)]
    public int AliveLength
    {
      get => (int)this["aliveLen"];
      set => this["aliveLen"] = value;
    }

    [ConfigurationProperty("Telegrams", IsDefaultCollection = true)]
    public ListenerTelegramCollection Telegrams
    {
      get => (ListenerTelegramCollection)this["Telegrams"];
      set => this["Telegrams"] = value;
    }
  }
}
