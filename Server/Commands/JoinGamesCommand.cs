using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    public class JoinGamesCommand : ICommand
    {
        private IModel model;
        public JoinGamesCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client)
        {
            if (!this.CheckValid(args, client))
            {
                return "singlePlayer";
            }
            // the name of the game to join.
            string name = args[0];
            GameMultiPlayer game = model.FindGameWating(name);
            if (model.ClientOnGame(client))
            {
                Controller.NestedErrors nested = new Controller.NestedErrors("You already on the game", client);
                return "multiPlayer";
            }
            // check if the game is in the list of games to play.
            else if (game != null)
            {
                game.Join(client);
                model.AddGamePlaying(name, game);
                model.RemoveGameWating(name);
                return "multiPlayer";
            }
            else
            {
                Controller.SendToClient("Error exist game", game.OtherClient(client));
                return "singlePlayer";
            }
        }

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
