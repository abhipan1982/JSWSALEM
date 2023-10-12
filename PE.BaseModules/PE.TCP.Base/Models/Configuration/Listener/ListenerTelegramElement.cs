using System.Configuration;

namespace PE.TCP.Base.Models.Configuration.Listener
{
  public class ListenerTelegramElement : ConfigurationElement
  {
    [ConfigurationProperty("telegramId", IsKey = true, IsRequired = true)]
    public int TelegramId
    {
      get => (int)this["telegramId"];
      set => this["telegramId"] = value;
    }

    [ConfigurationProperty("telegramLength", IsRequired = false, DefaultValue = -1)]
    public int TelegramLength
    {
      get => (int)this["telegramLength"];
      set => this["telegramLength"] = value;
    }

    [ConfigurationProperty("descr", IsRequired = true, DefaultValue = "dummy description")]
    public string Description
    {
      get => (string)this["descr"];
      set => this["descr"] = value;
    }

    [ConfigurationProperty("assembly", IsKey = true, IsRequired = true)]
    public string Assembly
    {
      get => this["assembly"].ToString();
      set => this["assembly"] = value;
    }

    [ConfigurationProperty("telegramType", IsKey = true, IsRequired = true)]
    public string TelegramType
    {
      get => this["telegramType"].ToString();
      set => this["telegramType"] = value;
    }
  }
}
