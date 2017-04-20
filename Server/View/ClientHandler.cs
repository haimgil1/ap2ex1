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
    /// Class : ClientHandler.
    /// </summary>
    /// <seealso cref="Server.IClientHandler" />
    public class ClientHandler : IClientHandler
    {
        private Controller controller;
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientHandler"/> class.
        /// </summary>
        public ClientHandler() {
            this.controller = new Controller();
        }
        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="client">The client.</param>
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
                        try
                        {
                            // Wating for message.
                            string commandLine = reader.ReadLine();
                            // Check the command from the client.
                            if (commandLine != null)
                            {
                                Console.WriteLine("Got command: {0}", commandLine);
                                /* If the result from the controller is "singlePlayer" closing the connection,
                                   If the result is "multiPlayer" keep the connection. */
                                string result = controller.ExecuteCommand(commandLine, client);
                                Thread.Sleep(200);
                                // Closing the connection.
                                if (result == "singlePlayer")
                                {
                                    writer.WriteLine(result);
                                    writer.Flush();
                                    break;
                                }
                                // Keep turn on the connection.
                                if (result == "multiPlayer")
                                {
                                    writer.WriteLine(result);
                                    writer.Flush();
                                    continue;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                }
                client.Close();
            }).Start();
        }

    }
}
