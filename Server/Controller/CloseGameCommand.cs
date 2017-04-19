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
	/// Close game command.
	/// </summary>
	public class CloseGameCommand : ICommand
	{
		private IModel model;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Server.CloseGameCommand"/> class.
		/// </summary>
		/// <param name="model">Model.</param>
		public CloseGameCommand(IModel model)
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
			GameMultiPlayer game = model.FindGamePlaying(name);
			// check if the game is in the list of games to play.
			if (game != null)
			{
				Controller.SendToClient("close client do close", client);
				Controller.SendToClient("close", game.OtherClient(client));
				return "singlePlayer";
			}
			else
			{
				Controller.NestedErrors nested = new Controller.NestedErrors("Error exist game", client);
				return "multiPlayer";
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
