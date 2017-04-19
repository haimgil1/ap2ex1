using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace Client
{
	/// <summary>
	/// Program.
	/// </summary>
	class Program
	{
		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		static void Main(string[] args)
		{
			string port = ConfigurationManager.AppSettings["Port"].ToString();
			int portInt = Int32.Parse(port);
			ViewClientConnect client = new ViewClientConnect();
			// connect the client
			client.Connect(portInt);
		}
	}
}
