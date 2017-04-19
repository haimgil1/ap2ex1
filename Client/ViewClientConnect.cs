using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Client
{
	/// <summary>
	/// View client connect.
	/// </summary>
	public class ViewClientConnect
	{
		/// <summary>
		/// The sender thread.
		/// </summary>
		private Thread senderThread;
		/// <summary>
		/// The recieve thread.
		/// </summary>
		private Task recieveThread;
		/// <summary>
		/// The is connect.
		/// </summary>
		private static bool isConnect = false; // indicate of connection between client - server.
		/// <summary>
		/// The get messege.
		/// </summary>
		private static bool getMessege = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Client.ViewClientConnect"/> class.
		/// </summary>
		public ViewClientConnect()
		{

		}

		/// <summary>
		/// Connect the specified port.
		/// </summary>
		/// <returns>The connect.</returns>
		/// <param name="port">Port.</param>
		public void Connect(int port)
		{
			IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
			TcpClient client = null;
			NetworkStream stream = null;
			StreamReader reader = null;
			StreamWriter writer = null;

			// delegate function that returns always void.
			Action recieveData = new Action(() =>
			{
				while (true)
				{
					try
					{
					   //Thread.Sleep(200);
					   // get data from the server.
					   // getMessege = false;
					   string result = reader.ReadLine();

					   // close the connect with the server.
					   if (result.Contains("singlePlayer"))
						{

						   // update the boolean status that is connectionless.
						   isConnect = false;
							getMessege = true;
							client.Close();
							break;
						}
						if (result.Contains("multiPlayer"))
						{
							getMessege = true;
						   //Console.WriteLine("multiPlayer");
						   continue;
						}
					   // print the result from the server.

					   if (result == "close")
						{
							isConnect = false;
							Console.WriteLine("close the game by other client");
							break;
						}
						if (result != "")
						{
							Console.WriteLine(result);
						}
					}
				   // The server close the connect, and we close the client.
				   catch (Exception)
					{
						isConnect = false;
						client.Close();
						break;
					}
				}
			});
			// the thread that always running.
			senderThread = new Thread(() =>
			{
				while (true)
				{
					try
					{

						//while (!getMessege)
						//{
						//}

						//Console.WriteLine("wait for command");
						string dataInput = Console.ReadLine();
						getMessege = false;
						if (dataInput == "exit")
						{
							break;
						}
						// there is no connection and we start a new connection.
						if (!isConnect)
						{
							client = new TcpClient();
							client.Connect(ep);
							//Console.WriteLine("You are connected");
							stream = client.GetStream();
							reader = new StreamReader(stream);
							writer = new StreamWriter(stream);
							isConnect = true;
							recieveThread = new Task(recieveData);
							recieveThread.Start();
						}
						// write to the server.
						writer.WriteLine(dataInput);
						writer.Flush();
						Thread.Sleep(200);
					}
					// error connection.
					catch (Exception)
					{
						isConnect = false;
						client.Close();
						// break;
					}
				}
			});
			senderThread.Start();

		}
	}
}
