using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	/// <summary>
	/// Server connect.
	/// </summary>
	public class ServerConnect
	{
		/// <summary>
		/// The port.
		/// </summary>
		private int port;
		/// <summary>
		/// The listener.
		/// </summary>
		private TcpListener listener;
		/// <summary>
		/// The ch.
		/// </summary>
		private IClientHandler ch;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Server.ServerConnect"/> class.
		/// </summary>
		/// <param name="port">Port.</param>
		/// <param name="ch">Ch.</param>
		public ServerConnect(int port, IClientHandler ch)
		{
			this.port = port;
			this.ch = ch;
		}

		/// <summary>
		/// Start this instance.
		/// </summary>
		public void Start()
		{
			IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
			listener = new TcpListener(ep);

			listener.Start();
			Console.WriteLine("Waiting for connections...");
			Task task = new Task(() =>
			{

				while (true)
				{
					try
					{
						TcpClient client = listener.AcceptTcpClient();
						Console.WriteLine("Got new connection");
						ch.HandleClient(client);
					}
					catch (SocketException)
					{
						//Console.WriteLine("fail");
						continue;
					}
				}
				Console.WriteLine("Server stopped");
			});
			task.Start();
		}

		/// <summary>
		/// Stop this instance.
		/// </summary>
		public void Stop()
		{
			listener.Stop();
		}

	}
}
