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
	/// Game multi player.
	/// </summary>
	public class GameMultiPlayer
	{
		private Maze maze;
		private TcpClient client1;
		private TcpClient client2;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Server.GameMultiPlayer"/> class.
		/// </summary>
		/// <param name="maze">Maze.</param>
		/// <param name="client">Client.</param>
		public GameMultiPlayer(Maze maze, TcpClient client)
		{
			this.maze = maze;
			this.client1 = client;
			this.client2 = null;
		}

		/// <summary>
		/// Join the specified client.
		/// </summary>
		/// <returns>The join.</returns>
		/// <param name="client">Client.</param>
		public void Join(TcpClient client)
		{
			this.client2 = client;
			SendMaze();
		}

		/// <summary>
		/// Ises the join.
		/// </summary>
		/// <returns><c>true</c>, if join was ised, <c>false</c> otherwise.</returns>
		public bool IsJoin()
		{
			return (client2 != null);
		}

		/// <summary>
		/// Gets the maze.
		/// </summary>
		/// <returns>The maze.</returns>
		public Maze GetMaze()
		{
			return this.maze;
		}

		/// <summary>
		/// Gets the client1.
		/// </summary>
		/// <returns>The client1.</returns>
		public TcpClient GetClient1()
		{
			return this.client1;
		}

		/// <summary>
		/// Gets the client2.
		/// </summary>
		/// <returns>The client2.</returns>
		public TcpClient GetClient2()
		{
			return this.client2;
		}

		/// <summary>
		/// Sends the maze.
		/// </summary>
		public void SendMaze()
		{
			Controller.SendToClient(maze.ToJSON(), client1);
			Controller.SendToClient(maze.ToJSON(), client2);
		}

		/// <summary>
		/// Others the client.
		/// </summary>
		/// <returns>The client.</returns>
		/// <param name="client">Client.</param>
		public TcpClient OtherClient(TcpClient client)
		{
			if (client == client1)
			{
				return client2;
			}
			return client1; //client==client1
		}

	}
}
