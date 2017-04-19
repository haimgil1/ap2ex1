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
        /// <returns></returns>
        public Maze GenerateMaze(string name, int rows, int cols)
        {
            modelData.mutexMazes.WaitOne();
            IMazeGenerator newMaze = new DFSMazeGenerator();
            Maze maze = null;
            if (!modelData.Mazes.ContainsKey(name))
            {
                maze = newMaze.Generate(rows, cols);
                modelData.Mazes.Add(name, maze); 
            }
            else
            {
                maze = modelData.Mazes[name];
            }
            modelData.mutexMazes.ReleaseMutex();
            return maze;
        }
        /// <summary>
        /// Solves the maze BFS.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Solution<Position> solveMazeBFS(string name)
        {
            modelData.mutexBfs.WaitOne();
            Solution<Position> solution = null;
            if (modelData.Mazes.ContainsKey(name))
            {
                ISearchable<Position> mazeObjectAdapter = new MazeAdapter(modelData.Mazes[name]);
                ISearcher<Position> BFS = new Bfs<Position>();

                if (modelData.BfsSolutions.ContainsKey(name))
                {
                    solution = modelData.BfsSolutions[name];
                }
                else
                {
                    solution = BFS.Search(mazeObjectAdapter);
                    modelData.BfsSolutions.Add(name, solution);
                }
            }
            modelData.mutexBfs.ReleaseMutex();
            return solution;

        }
        /// <summary>
        /// Solves the maze DFS.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Solution<Position> solveMazeDFS(string name)
        {
            modelData.mutexDfs.WaitOne();
            Solution<Position> solution = null;
            if (modelData.Mazes.ContainsKey(name))
            {
                ISearchable<Position> mazeObjectAdapter = new MazeAdapter(modelData.Mazes[name]);
                ISearcher<Position> DFS = new Dfs<Position>();

                if (modelData.DfsSolutions.ContainsKey(name))
                {
                    solution = modelData.DfsSolutions[name];
                }
                else
                {
                    solution = DFS.Search(mazeObjectAdapter);
                    modelData.DfsSolutions.Add(name, solution);
                }
            }
            modelData.mutexBfs.ReleaseMutex();
            return solution;
        }

        public GameMultiPlayer GenerateGame(string name, int rows, int cols, TcpClient client1)
        {
            IMazeGenerator newMaze = new DFSMazeGenerator();
            Maze maze;
            GameMultiPlayer game = null;
            if (!modelData.GameWating.ContainsKey(name) &&
                !modelData.GamesPlaying.ContainsKey(name))
            {
                maze = newMaze.Generate(rows, cols);
                maze.Name = name;
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

        public void RemoveGameWating(string name)
        {
            modelData.GameWating.Remove(name);
        }
        public void RemoveGamePlaying(string name)
        {
            modelData.GamesPlaying.Remove(name);
        }
        public bool ContainMaze(string name)
        {
            return modelData.Mazes.ContainsKey(name);
        }

        public string ListGamesWating()
        {
            return JsonConvert.SerializeObject(modelData.GameWating.Keys);
        }
        public GameMultiPlayer FindGameWating(string name)
        {
            if (modelData.GameWating.ContainsKey(name))
            {
                return modelData.GameWating[name];
            }
            return null;
        }
        public GameMultiPlayer FindGamePlaying(string name)
        {
            if (modelData.GamesPlaying.ContainsKey(name))
            {
                return modelData.GamesPlaying[name];
            }
            return null;
        }

        public GameMultiPlayer FindGameByClient(TcpClient client)
        {
            GameMultiPlayer game = null;
            foreach (GameMultiPlayer value in modelData.GameWating.Values)
            {
                if (value.GetClient1() == client || value.GetClient2() == client)
                {
                    game = value;
                }
            }
            foreach (GameMultiPlayer value in modelData.GamesPlaying.Values)
            {
                if (value.GetClient1() == client || value.GetClient2() == client)
                {
                    game = value;
                }
            }
            return game;
        }

        public bool ClientOnGame(TcpClient client)
        {
            GameMultiPlayer game = this.FindGameByClient(client);
            if (game == null)
            {
                return false;
            }
            return true;
        }

            
                
        
    }
}