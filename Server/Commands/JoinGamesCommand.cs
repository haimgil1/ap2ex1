using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    /// <summary>
    /// Class : JoinGamesCommand. The class responsible to join exist game.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class JoinGamesCommand : ICommand
    {
        private IModel model;
        /// <summary>
        /// Initializes a new instance of the <see cref="JoinGamesCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public JoinGamesCommand(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Join the client to the game. If the game not exist the client gets a message.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>
        /// string: "singlePlayer" or "multiPlayer"
        /// </returns>
        public string Execute(string[] args, TcpClient client)
        {
            if (!this.CheckValid(args, client))
            {
                return "multiPlayer";
            }
            // the name of the game to join.
            string name = args[0];
            GameMultiPlayer game = model.FindGameWating(name);
            if (model.ClientOnGameByName(client, name))
            {
                Controller.NestedErrors nested = new Controller.NestedErrors("You already on the game", client);
            }
            // check if the game is in the list of games to play.
            else if (game != null)
            {
                game.Join(client);
                // Add to Game play list and remove from wating list.
                model.AddGamePlaying(name, game);
                model.RemoveGameWating(name);
            }
            else
            {
                Controller.NestedErrors nested = new Controller.NestedErrors("Error exist game", client);
            }
            return "multiPlayer";
        }

        /// <summary>
        /// Checks the valid.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public bool CheckValid(string[] args, TcpClient client)
        {
            if (args.Length > 1)
            {
                Controller.NestedErrors nested = new Controller.NestedErrors("Bad arguement", client);
                return false;
            }
            try
            {
                string name = args[0];
                return true;
            }
            catch (Exception)
            {
                Controller.NestedErrors nested = new Controller.NestedErrors("Bad arguement", client);
                return false;
            }
        }
    }
}
