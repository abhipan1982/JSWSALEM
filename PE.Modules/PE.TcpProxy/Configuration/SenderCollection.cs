using System.Configuration;

namespace PE.TcpProxy
{
	[ConfigurationCollection(typeof(SenderElement))]
	public class SenderCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new SenderElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SenderElement)element).TelegramID;
		}

		public SenderElement this[int index]
		{
			get
			{
				return BaseGet(index) as SenderElement;
			}
		}
	}
}
