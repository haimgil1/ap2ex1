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
	/// Start maze command.
	/// </summary>
	class StartMazeCommand : ICommand
	{
		private IModel model;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Server.StartMazeCommand"/> class.
		/// </summary>
		/// <param name="model">Model.</param>
		public StartMazeCommand(IModel model)
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
			model.GetmodelData().mutexGamePlaying.WaitOne();
			model.GetmodelData().mutexGameWating.WaitOne();
			string name = args[0];
			int rows = int.Parse(args[1]);
			int cols = int.Parse(args[2]);
			// get the game from the model.
			GameMultiPlayer game = model.GenerateGame(name, rows, cols, client);
			model.GetmodelData().mutexGamePlaying.ReleaseMutex();
			model.GetmodelData().mutexGameWating.ReleaseMutex();
			if (game != null)
			{
				return "multiPlayer";
			}
			else
			{
				Controller.NestedErrors nested = new Controller.NestedErrors("Error exist game", client);
				return "singlePlayer";
			}
		}

		/// <summary>
		/// Checks the valid.
		/// </summary>
		/// <returns><c>true</c>, if valid was checked, <c>false</c> otherwise.</returns>
		/// <param name="args">Arguments.</param>
		/// <param name="client">Client.</param>
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
