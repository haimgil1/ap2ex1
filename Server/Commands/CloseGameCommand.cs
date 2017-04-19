using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class CloseGameCommand : ICommand
    {
        private IModel model;
        public CloseGameCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client)
        {
            if (!this.CheckValid(args, client))
            {
                return "singlePlayer";
            }
            string name = args[0];
            GameMultiPlayer game = model.FindGamePlaying(name);
            // check if the game is in the list of games to play.
            if (game != null)
            {
                Controller.SendToClient("close client do close", client);
                Controller.SendToClient("close", game.OtherClient(client));
                return "singlePlayer";
            }
            else
            {
                Controller.NestedErrors nested = new Controller.NestedErrors("Error exist game", client);
                return "multiPlayer";
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
