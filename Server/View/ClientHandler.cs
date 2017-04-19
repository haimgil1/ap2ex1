using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
	/// <summary>
	/// Client handler.
	/// </summary>
	public class ClientHandler : IClientHandler
	{
		/// <summary>
		/// The controller.
		/// </summary>
		private Controller controller;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Server.ClientHandler"/> class.
		/// </summary>
		public ClientHandler()
		{
			this.controller = new Controller();
		}

		/// <summary>
		/// Handles the client.
		/// </summary>
		/// <param name="client">Client.</param>
		public void HandleClient(TcpClient client)
		{
			new Task(() =>
			{
				using (NetworkStream stream = client.GetStream())
				using (StreamReader reader = new StreamReader(stream))
				using (StreamWriter writer = new StreamWriter(stream))
				{
					while (true)
					{
						Console.WriteLine("watting for message");
						string commandLine = reader.ReadLine();
						if (commandLine != null)
						{
							Console.WriteLine("Got command: {0}", commandLine);
							string result = controller.ExecuteCommand(commandLine, client);
							Thread.Sleep(200);
							if (result == "singlePlayer")
							{

								writer.WriteLine(result);
								writer.Flush();
								break;
							}
							if (result == "multiPlayer")
							{

								writer.WriteLine(result);
								writer.Flush();
								continue;
							}
						}
					}
				}
				client.Close();
			}).Start();
		}

	}
}
