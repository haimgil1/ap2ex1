using MazeLib;
using SearchAlgorithmsLib;
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
    /// Class : SolveMazeCommand. The class responsible to sovle the maze by DFS or BFS.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class SolveMazeCommand : ICommand
    {
        private IModel model;
        /// <summary>
        /// Initializes a new instance of the <see cref="SolveMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SolveMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            if (!this.CheckValid(args, client))
            {
                // Check if the client is on other game to keep the connection.
                if (model.ClientOnGame(client))
                {
                    return "multiPlayer";
                }
                return "singlePlayer";
            }
            string name = args[0];
            string typeAlgorithem = args[1];
            AdapterSolution adpterSolution = null;
            Solution<Position> s = null;

            // Solve the maze by BFS - 0, DFS - 1.
            switch (typeAlgorithem)
            {
                case "0":
                    s = model.solveMazeBFS(name); // BFS solution.
                    break;
                case "1":
                    s = model.solveMazeDFS(name); // DFS solution.
                    break;
            }
            // If the solution is not null send the solution to the client. Else send a message.
            if (s != null)
            {
                adpterSolution = new AdapterSolution(s, name, s.SolutionSize());
                Controller.SendToClient(adpterSolution.ToJson(), client);
            } else
            {
                Controller.NestedErrors nested = new Controller.NestedErrors("maze exist", client);
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
            if (args.Length > 2)
            {
                Controller.NestedErrors nested = new Controller.NestedErrors("Bad arguement", client);
                return false;
            }
            try
            {
                string name = args[0];
                int rows = int.Parse(args[1]);
                if (rows > 1 || rows < 0)
                {
                    Controller.NestedErrors nested = new Controller.NestedErrors("Bad arguement", client);
                    return false;
                }
                else {
                    return true;
                }
            }
            catch (Exception)
            {
                Controller.NestedErrors nested = new Controller.NestedErrors("Bad arguement", client);
                return false;
            }
        }
    }
}
