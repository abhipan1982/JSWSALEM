using System.Configuration;

namespace PE.TcpProxy
{
	public class ListenerSection : ConfigurationSection
	{
		[ConfigurationProperty("Listeners", IsDefaultCollection = true)]
		public ListenerCollection Listeners
		{
			get { return (ListenerCollection)this["Listeners"]; }
			set { this["Listeners"] = value; }
		}
	}
}
