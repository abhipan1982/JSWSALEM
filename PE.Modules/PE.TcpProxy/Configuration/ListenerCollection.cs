using System.Configuration;

namespace PE.TcpProxy
{
	[ConfigurationCollection(typeof(ListenerElement))]
	public class ListenerCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new ListenerElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ListenerElement)element).TelegramID;
		}

		public ListenerElement this[int index]
		{
			get
			{
				return BaseGet(index) as ListenerElement;
			}
		}
	}
}
