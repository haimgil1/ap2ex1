﻿using MazeLib;
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
	/// Generate maze command.
	/// </summary>
	public class GenerateMazeCommand : ICommand
	{
		private IModel model;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Server.GenerateMazeCommand"/> class.
		/// </summary>
		/// <param name="model">Model.</param>
		public GenerateMazeCommand(IModel model)
		{
			this.model = model;
		}

		/// <summary>
		/// Execute the specified args and client.
		/// </summary>
		/// <returns>The execute.</returns>
		/// <param name="args">Arguments.</param>
		/// <param name="client">Client.</param>
		public string Execute(string[] args, TcpClient client)
		{
			if (!this.CheckValid(args, client))
			{
				return "singlePlayer";
			}
			string name = args[0];
			int rows = int.Parse(args[1]);
			int cols = int.Parse(args[2]);

			if (!model.ContainMaze(name))
			{
				Maze maze = model.GenerateMaze(name, rows, cols);
				maze.Name = name;
				Controller.SendToClient(maze.ToJSON(), client);
			}
			else
			{
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
