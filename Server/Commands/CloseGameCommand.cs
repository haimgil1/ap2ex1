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
    /// Class: CloseGameCommand. The class responsible to close game multi players.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class CloseGameCommand : ICommand
    {
        private IModel model;
        /// <summary>
        /// Initializes a new instance of the <see cref="CloseGameCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public CloseGameCommand(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Close the multi players game.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>
        /// string: "singlePlayer" or "multiPlayer".
        /// </returns>
        public string Execute(string[] args, TcpClient client)
        {
            // Check the valid if the arguements.
            if (!this.CheckValid(args, client))
            {
                return "multiPlayer";
            }
            string name = args[0];
            GameMultiPlayer game = model.FindGamePlaying(name);
            // Check if the game is in the list of games to play.
            if (game != null)
            {
                model.RemoveGamePlaying(name);
                Controller.SendToClient("close client do close", client);
                Controller.SendToClient("close the game by other client", game.OtherClient(client));
                if (!model.ClientOnGame(game.OtherClient(client)))
                {
                    Controller.SendToClient("singlePlayer", game.OtherClient(client));
                }
                if (!model.ClientOnGame(client))
                {
                    return "singlePlayer";
                }
                return "multiPlayer";

            }
            else
            {
                Controller.NestedErrors nested = new Controller.NestedErrors("Error exist game", client);
                return "multiPlayer";
            }
 
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
