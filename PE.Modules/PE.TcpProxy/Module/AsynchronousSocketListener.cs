using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using SMF.Module.Core;
using SMF.Core.Log;
using NLog;
using PE.Interfaces.DC;

namespace PE.TcpProxy
{
	// State object for reading client data asynchronously
	public class StateObject
	{
		// Client  socket.
		public Socket workSocket = null;
		// Size of receive buffer.
		public const int BufferSize = 1000;
		public int actualSize = 0;
		// Receive buffer.
		public byte[] buffer = new byte[BufferSize];
	}

	public class AsynchronousSocketListener
	{
		// Thread signal.
		public static ManualResetEvent allDone = new ManualResetEvent(false);

		[Serializable]
		[ComVisible(true)]
		public delegate void ListenerAsyncCallback(IAsyncResult ar, int bytesSent);

		private int telegramid;
		private int telLength;
		private int socket;
		private string descr;
		private int alive;
		private int alivecycle;
		private Socket listener;
		private int aliveCounter;
		private int aliveId;
		private int aliveLength;
		private int aliveOffset;
		private const int sleepPeriod = 10;

		private bool isConnected = false;

		public AsynchronousSocketListener(int telegramid, int telLength, int socket, string descr, int alive, int alivecycle, int aliveId, int aliveLength, int aliveOffset)
		{
			this.telegramid = telegramid;
			this.telLength = telLength;
			this.socket = socket;
			this.descr = descr;
			this.alive = alive;
			this.alivecycle = alivecycle;
			this.aliveOffset = aliveOffset;
			this.aliveId = aliveId;
			this.aliveLength = aliveLength;
		}

		public void StartListening()
		{
			ModuleController.Logger.Info("Starting listener for id: {0}, socket: {1}, descr: {2}", telegramid, socket, descr);
			IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
			IPAddress ipAddress = ipHostInfo.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
			if (ipAddress == null)
			{
				ModuleController.Logger.Error("Can not find right address type, socket {0} is not intialized", socket);
				return;
			}
			IPEndPoint localEndPoint = new IPEndPoint(ipAddress, socket);

			// Create a TCP/IP socket.
			this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			// Bind the socket to the local endpoint and listen for incoming connections.
			try
			{
				listener.Bind(localEndPoint);
				listener.Listen(100);
				ModuleController.Logger.Info("Telegram {0}: Waiting  for a connection...", telegramid);
				while (true)
				{
					// Set the event to nonsignaled state.
					allDone.Reset();
					listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
					Thread.Sleep(sleepPeriod);
					aliveCounter += sleepPeriod;

					if (isConnected && aliveCounter > alivecycle)
					{
						//disconnect channel
						ModuleController.Logger.Info("Telegram {0} timed out! Disconnecting client...", telegramid);
						isConnected = false;
						try
						{
							this.listener.Shutdown(SocketShutdown.Both);
						}
						catch
						{
							this.listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
						}
					}
				}

			}
			catch (Exception ex)
			{
				LogHelper.LogException(ModuleController.Logger, ex, String.Format("Commincation error: unable initialize listener"));
			}
		}

		public void AcceptCallback(IAsyncResult ar)
		{
			// Signal the main thread to continue.
			allDone.Set();
			Socket handler;
			Socket listener;

			// Get the socket that handles the client request.
			try
			{
				listener = (Socket)ar.AsyncState;
				handler = listener.EndAccept(ar);

				// Create the state object.
				StateObject state = new StateObject();
				state.workSocket = handler;

				handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
				this.isConnected = true;
				this.aliveCounter = 0;
			}
			catch (Exception ex)
			{
				LogHelper.LogException(ModuleController.Logger, ex, String.Format("Commincation error: AcceptCallback failed"));
				return;
			}
			ModuleController.Logger.Info("Client connected on port {0}", ((IPEndPoint)handler.LocalEndPoint).Port.ToString());
		}

		//public static void ReadCallback(IAsyncResult ar)
		public void ReadCallback(IAsyncResult ar)
		{
			// Retrieve the state object and the handler socket
			// from the asynchronous state object.
			StateObject state = (StateObject)ar.AsyncState;
			Socket handler = state.workSocket;
			bool connected = handler.Available != 0 || !handler.Poll(1, SelectMode.SelectRead);
			if (!connected)
			{
				ModuleController.Logger.Info("Client disconnected on port: {0} ", ((IPEndPoint)handler.LocalEndPoint).Port.ToString());
				this.aliveCounter = 0;
				state.actualSize = 0;
				handler.Close();
				return;
			}

			// Read data from the client socket. 
			int bytesRead = handler.EndReceive(ar);
			state.actualSize += bytesRead;
			ModuleController.Logger.Info(" Read {0} bytes from socket. Actual size {1}", bytesRead, state.actualSize);
			int telId;
			int j;
			int i;
			byte[] singleTelbuff = new byte[this.telLength];

			while (state.actualSize >= this.telLength)
			{
				telId = ExtractTelegramId(state.buffer, this.aliveId, this.aliveOffset, this.aliveLength);
				//get data for one telegram
				for (i = 0; i < this.telLength; i++)
					singleTelbuff[i] = state.buffer[i];

				if (telId == this.telegramid)
				{
					ModuleController.Logger.Info("Dispatching telegram {0}", telId);
					this.TCPStatusUpdate(true);
					SendByteData(handler, singleTelbuff, this.telLength);
					this.aliveCounter = 0;
				}
				else if (telId == this.aliveId)
				{
					ModuleController.Logger.Info("Discarding alive telegram.");
					this.aliveCounter = 0;
				}
				else
				{
					ModuleController.Logger.Error("ERROR: Unknown telegram ID: {0} ", telId);
				}

				//shift data in buffer
				j = 0;
				for (i = telLength; i < state.buffer.Length; i++)
				{
					state.buffer[j++] = state.buffer[i];
				}
				//state.buffer[state.actualSize] = 0;
				state.actualSize -= this.telLength;
				ModuleController.Logger.Info("Actual buffer size {0}", state.actualSize);

			}
			// Not all data received. Get more.
			handler.BeginReceive(state.buffer, state.actualSize, StateObject.BufferSize - state.actualSize, 0, new AsyncCallback(ReadCallback), state);
		}

		private int ExtractTelegramId(byte[] buffer, int aliveId, int aliveOffset, int aliveLength)
		{
			int telId = -999; //wrong telId
			byte[] telIdChar = new byte[aliveLength];
			for (int i = 0; i < aliveLength; i++)
			{
				telIdChar[i] = buffer[i + aliveOffset];
			}
			try
			{
				switch (aliveLength)
				{
					case 2: { telId = BitConverter.ToInt16(telIdChar, 0); break; }
					case 4: { telId = BitConverter.ToInt32(telIdChar, 0); break; }
					default: { telId = BitConverter.ToInt32(telIdChar, 0); break; }
				}
			}
			catch (Exception ex)
			{
				LogHelper.LogException(ModuleController.Logger, ex, String.Format("Convert error"));
			}
			return telId;
		}

		//private static void Send(Socket handler, String data)
		/*
		private void Send(Socket handler, String data)
		{
			// Convert the string data to byte data using ASCII encoding.
			byte[] byteData = Encoding.ASCII.GetBytes(data);
			TelegramTest1 o = new TelegramTest1();
			GCHandle handle = GCHandle.Alloc(byteData, GCHandleType.Pinned);
			Marshal.StructureToPtr(o, handle.AddrOfPinnedObject(), false);
			handle.Free();
			// Begin sending the data to the remote device.
			handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
		}
		 */

		private void SendByteData(Socket handler, byte[] byteData, int bytesRead)
		{
      DCExtCommonMessage dc = new DCExtCommonMessage();
			ModuleController.InitDataContract(dc, telegramid);
      dc._buffer = byteData;
      dc.IsValid = true;

      Task sendTask = TcpProxy.SendOffice.ForwardBytesToAdapter(dc);
		}

		//private static void SendCallback(IAsyncResult ar)
		private void SendCallback(IAsyncResult ar)
		{
			try
			{
				// Retrieve the socket from the state object.
				Socket handler = (Socket)ar.AsyncState;

				// Complete sending the data to the remote device.
				int bytesSent = handler.EndSend(ar);
				ModuleController.Logger.Info("Sent {0} bytes to client.", bytesSent);

			}
			catch (Exception ex)
			{
				LogHelper.LogException(ModuleController.Logger, ex, String.Format("Commincation error: unable to send data"));
			}
		}

		private void TCPStatusUpdate(bool tcpState)
		{
			if (tcpState == true)
				ModuleController.ElementStatusUpdate(SMF.Core.Constants.KeyL1TcpStatus, SMF.Core.Constants.SystemSts.Ok);
			else
				ModuleController.ElementStatusUpdate(SMF.Core.Constants.KeyL1TcpStatus, SMF.Core.Constants.SystemSts.Error);
		}
	}
}
