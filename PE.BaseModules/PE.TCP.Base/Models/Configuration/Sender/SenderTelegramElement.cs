using System.Configuration;

namespace PE.TCP.Base.Models.Configuration.Sender
{
  public class SenderTelegramElement : ConfigurationElement
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

    [ConfigurationProperty("hasAck", IsKey = true, IsRequired = true, DefaultValue = false)]
    public bool HasAcknowledge
    {
      get => (bool)this["hasAck"];
      set => this["hasAck"] = value;
    }

    [ConfigurationProperty("ackTelegramType", IsKey = true, IsRequired = false)]
    public string AcknowledgeTelegramType
    {
      get => this["ackTelegramType"].ToString();
      set => this["ackTelegramType"] = value;
    }

    [ConfigurationProperty("ackLength", IsKey = true, IsRequired = false)]
    public int AcknowledgeLength
    {
      get => (int)this["ackLength"];
      set => this["ackLength"] = value;
    }

    [ConfigurationProperty("isASCIIMessage", IsKey = true, IsRequired = true)]
    public bool IsAsciiMessage
    {
      get => (bool)this["isASCIIMessage"];
      set => this["isASCIIMessage"] = value;
    }
  }
}
