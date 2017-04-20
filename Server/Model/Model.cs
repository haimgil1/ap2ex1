using MazeGeneratorLib;
using MazeLib;
using Newtonsoft.Json;
using ObjectAdapter;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    /// <summary>
    /// Class: Model. The Model implement IModel.
    /// </summary>
    /// <seealso cref="Server.IModel" />
    public class Model : IModel
    {
        private ModelDataBase modelData;
        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        public Model()
        {
            modelData = new ModelDataBase();
        }
        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns>Maze</returns>
        public Maze GenerateMaze(string name, int rows, int cols)
        {
            modelData.mutexMazes.WaitOne();
            IMazeGenerator newMaze = new DFSMazeGenerator();
            Maze maze = null;
            // Generate the maze.
            maze = newMaze.Generate(rows, cols);
            modelData.Mazes.Add(name, maze);
            // Unlock.
            modelData.mutexMazes.ReleaseMutex();
            return maze;
        }
        /// <summary>
        /// Solves the maze BFS.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Solution</returns>
        public Solution<Position> solveMazeBFS(string name)
        {
            Solution<Position> solution = null;
            // Check if the maze exist.
            if (modelData.Mazes.ContainsKey(name))
            {
                ISearchable<Position> mazeObjectAdapter = new MazeAdapter(modelData.Mazes[name]);
                ISearcher<Position> BFS = new Bfs<Position>();
                // Check if the solution exist.
                if (modelData.BfsSolutions.ContainsKey(name))
                {
                    solution = modelData.BfsSolutions[name];
                }
                else
                {
                    // Calculate the solution.
                    solution = BFS.Search(mazeObjectAdapter);
                    modelData.BfsSolutions.Add(name, solution);
                }
            }
            return solution;

        }
        /// <summary>
        /// Solves the maze DFS.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Solution</returns>
        public Solution<Position> solveMazeDFS(string name)
        {
            Solution<Position> solution = null;
            // Check if the maze exist.
            if (modelData.Mazes.ContainsKey(name))
            {
                ISearchable<Position> mazeObjectAdapter = new MazeAdapter(modelData.Mazes[name]);
                ISearcher<Position> DFS = new Dfs<Position>();
                // Check if the solution exist.
                if (modelData.DfsSolutions.ContainsKey(name))
                {
                    solution = modelData.DfsSolutions[name];
                }
                else
                {
                    // Calculate the solution.
                    solution = DFS.Search(mazeObjectAdapter);
                    modelData.DfsSolutions.Add(name, solution);
                }
            }
            return solution;
        }
        /// <summary>
        /// Generates the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="client1">The client1.</param>
        /// <returns>GameMultiPlayer</returns>
        public GameMultiPlayer GenerateGame(string name, int rows, int cols, TcpClient client1)
        {
            IMazeGenerator newMaze = new DFSMazeGenerator();
            Maze maze;
            GameMultiPlayer game = null;
            // Check if the game is on the list of wating or playing games.
            if (!modelData.GameWating.ContainsKey(name) &&
                !modelData.GamesPlaying.ContainsKey(name))
            {
                // Generate maze.
                maze = newMaze.Generate(rows, cols);
                maze.Name = name;
                // Generate the game.
                game = new GameMultiPlayer(maze, client1);
                modelData.GameWating.Add(name, game);
            }
            return game;

        }
        /// <summary>
        /// Adds the game playing.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="game">The game.</param>
        public void AddGamePlaying(string name, GameMultiPlayer game)
        {
            modelData.GamesPlaying.Add(name, game);
        }

        /// <summary>
        /// Removes the game wating.
        /// </summary>
        /// <param name="name">The name.</param>
        public void RemoveGameWating(string name)
        {
            modelData.GameWating.Remove(name);
        }
        /// <summary>
        /// Removes the game playing.
        /// </summary>
        /// <param name="name">The name.</param>
        public void RemoveGamePlaying(string name)
        {
            modelData.GamesPlaying.Remove(name);
        }
        /// <summary>
        /// Check if contains the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>bool</returns>
        public bool ContainMaze(string name)
        {
            return modelData.Mazes.ContainsKey(name);
        }
        /// <summary>
        /// Lists the games wating.
        /// </summary>
        /// <returns>string</returns>
        public string ListGamesWating()
        {
            return JsonConvert.SerializeObject(modelData.GameWating.Keys);
        }
        /// <summary>
        /// Finds the game wating.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>GameMultiPlayer</returns>
        public GameMultiPlayer FindGameWating(string name)
        {
            if (modelData.GameWating.ContainsKey(name))
            {
                return modelData.GameWating[name];
            }
            return null;
        }
        /// <summary>
        /// Finds the game playing.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>GameMultiPlayer</returns>
        public GameMultiPlayer FindGamePlaying(string name)
        {
            if (modelData.GamesPlaying.ContainsKey(name))
            {
                return modelData.GamesPlaying[name];
            }
            return null;
        }
        /// <summary>
        /// Finds the game by client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>GameMultiPlayer</returns>
        public GameMultiPlayer FindGameByClient(TcpClient client)
        {
            GameMultiPlayer game = null;
            // Over on GameWating dictionary.
            foreach (GameMultiPlayer value in modelData.GameWating.Values)
            {
                // Check if the client exist.
                if (value.GetClient1() == client || value.GetClient2() == client)
                {
                    game = value;
                }
            }
            // Over on GamesPlaying dictionary.
            foreach (GameMultiPlayer value in modelData.GamesPlaying.Values)
            {
                // Check if the client exist.
                if (value.GetClient1() == client || value.GetClient2() == client)
                {
                    game = value;
                }
            }
            return game;
        }
        /// <summary>
        /// Clients the on game.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>bool</returns>
        public bool ClientOnGame(TcpClient client)
        {
            if (this.FindGameByClient(client) != null)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Find if the client exist in game.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public bool ClientOnGameByName(TcpClient client, string name)
        {
            if (modelData.GameWating.ContainsKey(name))
            {
                if(modelData.GameWating[name].GetClient1() == client ||
                    modelData.GameWating[name].GetClient2() == client)
                {
                    return true;
                }
            } 
            else if (modelData.GamesPlaying.ContainsKey(name)) {
                if (modelData.GamesPlaying[name].GetClient1() == client ||
                    modelData.GamesPlaying[name].GetClient2() == client)
                {
                    return true;
                }
            }
            return false;
        }

    }
}