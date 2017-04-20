using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Class : ListGamesCommand. The class responsible to present the list of games to join them.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class ListGamesCommand : ICommand
    {
        private IModel model;
        /// <summary>
        /// Initializes a new instance of the <see cref="ListGamesCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public ListGamesCommand(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Send to client the list of wating games.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>
        /// string: "singlePlayer" or "multiPlayer"
        /// </returns>
        public string Execute(string[] args, TcpClient client)
        {
            // send the list of games to the client.
            Controller.SendToClient(model.ListGamesWating(), client);
            return "multiPlayer";
        }
    }
}
