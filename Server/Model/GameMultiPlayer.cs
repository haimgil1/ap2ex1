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
    /// Class : GameMultiPlayer.
    /// </summary>
    public class GameMultiPlayer
    {
        private Maze maze;
        private TcpClient client1;
        private TcpClient client2;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameMultiPlayer"/> class.
        /// </summary>
        /// <param name="maze">The maze.</param>
        /// <param name="client">The client.</param>
        public GameMultiPlayer(Maze maze, TcpClient client)
        {
            this.maze = maze;
            this.client1 = client;
            this.client2 = null;
        }
        /// <summary>
        /// Joins the specified client and send the maze to the clients.
        /// </summary>
        /// <param name="client">The client.</param>
        public void Join(TcpClient client)
        {
            this.client2 = client;
            SendMaze();
        }
        /// <summary>
        /// Determines whether this instance is join.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is join; otherwise, <c>false</c>.
        /// </returns>
        public bool IsJoin()
        {
            if (client2 != null)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Gets the maze.
        /// </summary>
        /// <returns>Maze</returns>
        public Maze GetMaze()
        {
            return this.maze;
        }
        /// <summary>
        /// Gets the client1.
        /// </summary>
        /// <returns>TcpClient</returns>
        public TcpClient GetClient1()
        {
            return this.client1;
        }
        /// <summary>
        /// Gets the client2.
        /// </summary>
        /// <returns>TcpClient</returns>
        public TcpClient GetClient2()
        {
            return this.client2;
        }
        /// <summary>
        /// Sends the maze to the clients.
        /// </summary>
        public void SendMaze()
        {
            Controller.SendToClient(maze.ToJSON(), client1);
            Controller.SendToClient(maze.ToJSON(), client2);
        }
        /// <summary>
        /// Find the other the client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>TcpClient</returns>
        public TcpClient OtherClient(TcpClient client)
        {
            if (client == client1)
            {
                return client2;
            }
            else return client1;
        }

    }
}
