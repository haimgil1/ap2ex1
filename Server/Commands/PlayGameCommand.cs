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
    /// <summary>
    /// Class : PlayGameCommand. The class responsible to make play = {up, dowm, left, right} 
    /// on the game, and update the other client.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class PlayGameCommand : ICommand
    {
        private IModel model;
        private List<String> directions;
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayGameCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public PlayGameCommand(IModel model)
        {
            directions = new List<string>();
            this.model = model;
            // Add the parameters to the list.
            directions.Add("up");
            directions.Add("down");
            directions.Add("left");
            directions.Add("right");
        }

        public string Execute(string[] args, TcpClient client)
        {
            string direction = args[0];
            // Get the game of the client who press play.
            GameMultiPlayer game = model.FindGameByClient(client);
            // Check the direction.
            if (!directions.Contains(direction))
            {
                Controller.NestedErrors error = new Controller.NestedErrors("The dirction is incorrect", client);
                return "multiPlayer";
            }
            // Check if the game exist.
            if (game != null)
            {
                NestedPlay nested = new NestedPlay(game.GetMaze().Name, direction);
                Controller.SendToClient(JsonConvert.SerializeObject(nested), game.OtherClient(client));
            }
            else
            {
                // The game dosnt exits. Send message to the client.
                Controller.NestedErrors error = new Controller.NestedErrors("you need to start a game", client);
            }
            return "multiPlayer";
        }
        /// <summary>
        /// class: NestedPlay.
        /// </summary>
        public class NestedPlay
        {
            public string Name;
            public string Direction;
            /// <summary>
            /// Initializes a new instance of the <see cref="NestedPlay"/> class.
            /// </summary>
            /// <param name="name1">The name1.</param>
            /// <param name="direction1">The direction1.</param>
            public NestedPlay(string name1, string direction1)
            {
                this.Name = name1;
                this.Direction = direction1;
            }
        }



    }
}
