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
    /// <summary>
    /// Class : GenerateMazeCommand. The class responsible to generate maze.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class GenerateMazeCommand : ICommand
    {
        private IModel model;
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public GenerateMazeCommand(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Create a maze. If the maze exist the client gets a message.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>
        /// string: "singlePlayer" or "multiPlayer".
        /// </returns>
        public string Execute(string[] args, TcpClient client)
        {
            if (!this.CheckValid(args, client))
            {
                // check if the client play on other game to keep the connection.
                if (model.ClientOnGame(client))
                {
                    return "multiPlayer";
                }
                return "singlePlayer";
            }
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            // Check if the maze exist.
            if (!model.ContainMaze(name))
            { 
                // Create a new maze.
                Maze maze = model.GenerateMaze(name, rows, cols);
                maze.Name = name;
                // Send the maze to the client.
                Controller.SendToClient(maze.ToJSON(), client); 
            }
            else
            {
                // Send to client that the game exist.
                Controller.NestedErrors nested = new Controller.NestedErrors("Maze exist", client);
            }
            
            if (model.ClientOnGame(client))
            {
                return "multiPlayer";
            }
            return "singlePlayer";
        }

        /// <summary>
        /// Checks the valid.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public bool CheckValid(string[] args, TcpClient client)
        {
            // number of arguements big than 3.
            if (args.Length > 3)
            {
                Controller.NestedErrors nested = new Controller.NestedErrors("Bad arguement", client);
                return false;
            }
            try {
                string name = args[0];
                int rows = int.Parse(args[1]);
                int cols = int.Parse(args[2]);
                return true;
            } catch (Exception)
            {
                Controller.NestedErrors nested = new Controller.NestedErrors("Bad arguement", client);
                return false;
            }
            
        }
    }
}
