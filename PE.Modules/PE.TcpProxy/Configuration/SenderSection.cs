using System.Configuration;

namespace PE.TcpProxy
{
	public class SenderSection : ConfigurationSection
	{
		[ConfigurationProperty("Senders", IsDefaultCollection = true)]
		public SenderCollection Senders
		{
			get { return (SenderCollection)this["Senders"]; }
			set { this["Senders"] = value; }
		}
	}
}
