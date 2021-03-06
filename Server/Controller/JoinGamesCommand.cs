﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
	/// <summary>
	/// Join games command.
	/// </summary>
	public class JoinGamesCommand : ICommand
	{
		private IModel model;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Server.Commands.JoinGamesCommand"/> class.
		/// </summary>
		/// <param name="model">Model.</param>
		public JoinGamesCommand(IModel model)
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
			// the name of the game to join.
			string name = args[0];
			GameMultiPlayer game = model.FindGameWating(name);
			if (model.ClientOnGame(client))
			{
				Controller.NestedErrors nested = new Controller.NestedErrors("You already on the game", client);
				model.GetmodelData().mutexGamePlaying.ReleaseMutex();
				model.GetmodelData().mutexGameWating.ReleaseMutex();
				return "multiPlayer";
			}
			// check if the game is in the list of games to play.
			else if (game != null)
			{
				game.Join(client);
				model.AddGamePlaying(name, game);
				model.RemoveGameWating(name);
				model.GetmodelData().mutexGamePlaying.ReleaseMutex();
				model.GetmodelData().mutexGameWating.ReleaseMutex();
				return "multiPlayer";
			}
			else
			{
				Controller.SendToClient("Error exist game", game.OtherClient(client));
				model.GetmodelData().mutexGamePlaying.ReleaseMutex();
				model.GetmodelData().mutexGameWating.ReleaseMutex();
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
			if (args.Length > 1)
			{
				Controller.NestedErrors nested = new Controller.NestedErrors("Bad arguement", client);
				return false;
			}
			try
			{
				string name = args[0];
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
