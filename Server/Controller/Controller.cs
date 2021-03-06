﻿using Newtonsoft.Json;
using Server.Commands;
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
	/// Controller.
	/// </summary>
	class Controller
	{
		private Dictionary<string, ICommand> commands;
		private IModel model;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Server.Controller"/> class.
		/// </summary>
		public Controller()
		{
			model = new Model();
			commands = new Dictionary<string, ICommand>();
			commands.Add("generate", new GenerateMazeCommand(model));
			commands.Add("solve", new SolveMazeCommand(model));
			commands.Add("start", new StartMazeCommand(model));
			commands.Add("list", new ListGamesCommand(model));
			commands.Add("join", new JoinGamesCommand(model));
			commands.Add("play", new PlayGameCommand(model));
			commands.Add("close", new CloseGameCommand(model));
		}

		/// <summary>
		/// Executes the command.
		/// </summary>
		/// <returns>The command.</returns>
		/// <param name="commandLine">Command line.</param>
		/// <param name="client">Client.</param>
		public string ExecuteCommand(string commandLine, TcpClient client)
		{
			string[] arr = commandLine.Split(' ');
			string commandKey = arr[0];
			if (!commands.ContainsKey(commandKey))
				return "Command not found";
			string[] args = arr.Skip(1).ToArray();
			ICommand command = commands[commandKey];
			return command.Execute(args, client);
		}

		/// <summary>
		/// Nested errors.
		/// </summary>
		public class NestedErrors
		{
			public string Error;
			public NestedErrors(string error, TcpClient client)
			{
				this.Error = error;
				NetworkStream stream = client.GetStream();
				StreamWriter writer = new StreamWriter(stream);
				writer.WriteLine(JsonConvert.SerializeObject(this));
				writer.Flush();
			}

		}

		/// <summary>
		/// Sends to client.
		/// </summary>
		/// <param name="str">String.</param>
		/// <param name="client">Client.</param>
		public static void SendToClient(string str, TcpClient client)
		{
			NetworkStream stream = client.GetStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.WriteLine(str);
			writer.Flush();
		}
	}
}
