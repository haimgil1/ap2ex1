using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class PlayGameCommand : ICommand
    {
        private IModel model;
        private List<String> directions;

        public PlayGameCommand(IModel model)
        {
            directions = new List<string>();
            this.model = model;
            directions.Add("up");
            directions.Add("down");
            directions.Add("left");
            directions.Add("right");
        }

        public string Execute(string[] args, TcpClient client)
        {
            string direction = args[0];
            GameMultiPlayer game = model.FindGameByClient(client);

            if (!directions.Contains(direction))
            {
                Controller.NestedErrors error = new Controller.NestedErrors("The dirction is incorrect}", client);
                return "multiPlayer";
            }
            if (game != null)
            {
                NestedPlay nested = new NestedPlay(game.GetMaze().Name, direction);
                Controller.SendToClient(JsonConvert.SerializeObject(nested), game.OtherClient(client));
                return "multiPlayer";
            }
            else
            {
                Controller.SendToClient("{Erorr: you need to start a game}", client);
                return "singlePlayer";
            }
        }

        public class NestedPlay
        {
            public string Name;
            public string Direction;

            public NestedPlay(string name1, string direction1)
            {
                this.Name = name1;
                this.Direction = direction1;
            }
        }



    }
}
