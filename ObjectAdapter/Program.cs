using System;
using SearchAlgorithmsLib;
using MazeLib;
using MazeGeneratorLib;
namespace ObjectAdapter
{
	/// <summary>
	/// Program.
	/// </summary>
	class Program
	{
		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		static void Main(string[] args)
		{
			CompareSolvers();
		}

		/// <summary>
		/// Compares the solvers.
		/// </summary>
		public static void CompareSolvers()
		{
			DFSMazeGenerator generator = new DFSMazeGenerator();
			Maze maze = generator.Generate(10, 10);
			//Console.WriteLine(maze);
			ISearchable<Position> mazeAdapter = new MazeAdapter(maze);
			ISearcher<Position> bfs = new BestFirstSearch<Position>();
			ISearcher<Position> dfs = new Dfs<Position>();
			Solution<Position> solution = bfs.Search(mazeAdapter);
			Console.WriteLine("bfs sol:" + solution.EvaluatedNodes);
			Console.WriteLine(mazeAdapter.ToString(solution));
			solution = dfs.Search(mazeAdapter);
			Console.WriteLine("dfs sol:" + solution.EvaluatedNodes);
			Console.WriteLine(mazeAdapter.ToString(solution));
		}
	}
}