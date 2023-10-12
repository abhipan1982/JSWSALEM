using SMF.Module.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.TcpProxy
{
	public class SenderInfo
	{
		#region constructor

		public SenderInfo(AsynchronousSocketClient sender, int telIDcustomer)
		{
			_sender = sender;
			_telIDcus = telIDcustomer;
		}
		#endregion

		#region members

		private AsynchronousSocketClient _sender;
		private int _telIDcus;

		#endregion

		#region properties

		public AsynchronousSocketClient Sender
		{
			get { return _sender; }
			set { _sender = value; }
		}

		public int TelIdCustomer
		{
			get { return _telIDcus; }
			set { _telIDcus = value; }
		}
		#endregion

	}
	public class SendersTable
	{
		#region constructor

		public SendersTable(int senderCount)
		{
			_htSenders = new Hashtable(senderCount);
		}

		#endregion

		#region members

		private Hashtable _htSenders;
		#endregion

		#region methods

		public int AddSender(AsynchronousSocketClient sender, int telIdExternal, int telIDCustomer)
		{
			SenderInfo si = new SenderInfo(sender, telIDCustomer);
			_htSenders.Add(telIdExternal, si);
			return 0;
		}

		public AsynchronousSocketClient GetSenderByTelId(int telIdExternal)
		{
			if (_htSenders.ContainsKey(telIdExternal))
			{
				SenderInfo si = (SenderInfo)_htSenders[telIdExternal];
				return si.Sender;
			}
			else
			{
				ModuleController.Logger.Error("Sender with telegram Id {0} not found", telIdExternal);
				return null;
			}
		}

		#endregion

	}
}
