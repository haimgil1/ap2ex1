using MazeLib;
using SearchAlgorithmsLib;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Server
{
	/// <summary>
	/// Model.
	/// </summary>
	public interface IModel
	{
		Maze GenerateMaze(string name, int rows, int cols);
		Solution<Position> solveMazeBFS(string name);
		Solution<Position> solveMazeDFS(string name);
		GameMultiPlayer GenerateGame(string name, int rows, int cols, TcpClient client1);

		bool ContainMaze(string name);
		void RemoveGameWating(string name);
		void RemoveGamePlaying(string name);
		void AddGamePlaying(string name, GameMultiPlayer game);
		ModelDataBase GetmodelData();

		string ListGamesWating();
		GameMultiPlayer FindGameWating(string name);
		GameMultiPlayer FindGamePlaying(string name);
		GameMultiPlayer FindGameByClient(TcpClient client);

		bool ClientOnGame(TcpClient client);

	}
}