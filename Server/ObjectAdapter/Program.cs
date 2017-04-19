//using System;
//using SearchAlgorithmsLib;
//using MazeLib;
//using MazeGeneratorLib;
//namespace ObjectAdapter
//{
//	class Program
//	{
//		static void Main(string[] args)
//		{
//			CompareSolvers();
//		}
//		public static void CompareSolvers()
//		{
//			DFSMazeGenerator generator = new DFSMazeGenerator();
//			Maze maze = generator.Generate(155, 155);
//			Console.WriteLine(maze);
//			ISearchable<Position> mazeAdapter = new MazeAdapter(maze);
//			ISearcher<Position> bfs = new Bfs<Position>();
//			ISearcher<Position> dfs = new Dfs<Position>();
//			Solution<Position> solution = bfs.Search(mazeAdapter);
//			Console.WriteLine("bfs sol:" + solution.SolutionSize());
//			Console.WriteLine(mazeAdapter.ToString(solution));
//			solution = dfs.Search(mazeAdapter);
//			Console.WriteLine("dfs sol:" + solution.SolutionSize());
//			Console.WriteLine(mazeAdapter.ToString(solution));
//		}
//	}
//}