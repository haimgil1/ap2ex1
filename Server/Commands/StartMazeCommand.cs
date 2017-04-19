using MazeLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class StartMazeCommand : ICommand
    {
        private IModel model;
        public StartMazeCommand(IModel model)
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
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            // get the game from the model.
            GameMultiPlayer game = model.GenerateGame(name, rows, cols, client);

            if (game != null)
            {
                return "multiPlayer";
            } else
            {
                Controller.NestedErrors nested = new Controller.NestedErrors("Error exist game", client);
                return "singlePlayer";
            }
        }

        public bool CheckValid(string[] args, TcpClient client)
        {
            if (args.Length > 3)
            {
                Controller.NestedErrors nested = new Controller.NestedErrors("Bad arguement", client);
                return false;
            }
            try
            {
                string name = args[0];
                int rows = int.Parse(args[1]);
                int cols = int.Parse(args[2]);
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
