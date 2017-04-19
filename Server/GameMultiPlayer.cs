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
    public class GameMultiPlayer
    {
        private Maze maze;
        private TcpClient client1;
        private TcpClient client2; 

    public GameMultiPlayer(Maze maze, TcpClient client)
        {
            this.maze = maze;
            this.client1 = client;
            this.client2 = null;
        }

        public void Join (TcpClient client)
        {
            this.client2 = client;
            SendMaze();
        }
        public bool IsJoin()
        {
            if (client2 != null)
            {
                return true;
            }
            return false;
        }

        public Maze GetMaze()
        {
            return this.maze; 
        }
        public TcpClient GetClient1()
        {
            return this.client1;
        }
        public TcpClient GetClient2()
        {
            return this.client2;
        }

        public void SendMaze()
        {
            Controller.SendToClient(maze.ToJSON(), client1);
            Controller.SendToClient(maze.ToJSON(), client2);
        }

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
