using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Server
{
	/// <summary>
	/// Program.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		static void Main(string[] args)
		{

			string port = ConfigurationManager.AppSettings["Port"].ToString();
			int portInt = Int32.Parse(port);

			IClientHandler ch = new ClientHandler();
			ServerConnect server = new ServerConnect(portInt, ch);
			//start the server
			server.Start();
			Console.ReadKey();
		}
	}
}
