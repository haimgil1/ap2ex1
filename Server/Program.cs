using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Server
{
    public class Program
    {
        /// <summary>
        /// Mains the specified arguments. The main of the Program.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            // Get the port from the App.config.
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
