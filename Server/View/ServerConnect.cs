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
    /// Class: ServerConnect.
    /// </summary>
    public class ServerConnect
    {
        private int port;
        private TcpListener listener;
        private IClientHandler ch;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerConnect"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="ch">The ch.</param>
        public ServerConnect(int port, IClientHandler ch)
        {
            this.port = port;
            this.ch = ch;
        }
        /// <summary>
        /// Starts this instance. The main function.
        /// </summary>
        public void Start()
        {
            // Define the end point.
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);

            listener.Start();
            Console.WriteLine("Waiting for connections...");
            Task task = new Task(() => {
                // Wait always to clients.
                while (true)
                {
                    try
                    {
                        // Listen to Clients and get the connection.
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        ch.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
        }
        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            listener.Stop();
        }

    }
}
