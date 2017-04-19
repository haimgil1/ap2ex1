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
	/// List games command.
	/// </summary>
	public class ListGamesCommand : ICommand
	{
		private IModel model;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Server.ListGamesCommand"/> class.
		/// </summary>
		/// <param name="model">Model.</param>
		public ListGamesCommand(IModel model)
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

			Controller.SendToClient(model.ListGamesWating(), client);
			return "multiPlayer";
		}
	}
}
