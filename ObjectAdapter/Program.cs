using System;
using SearchAlgorithmsLib;
using MazeLib;
using MazeGeneratorLib;
namespace ObjectAdapter
{
	class Program
	{
		static void Main(string[] args)
		{
			CompareSolvers();
		}
		public static void CompareSolvers()
		{
			DFSMazeGenerator generator = new DFSMazeGenerator();
			Maze maze = generator.Generate(50, 50);
			Console.WriteLine(maze);
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