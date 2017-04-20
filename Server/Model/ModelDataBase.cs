using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Class: ModelDataBase. Contians the Data of the Model.
    /// </summary>
    public class ModelDataBase
    {
        private Dictionary<string, Maze> mazes;
        private Dictionary<string, Solution<Position>> bfsSolutions;
        private Dictionary<string, Solution<Position>> dfsSolutions;
        private Dictionary<string, GameMultiPlayer> gameWating;
        private Dictionary<string, GameMultiPlayer> gamesPlaying;

        /// <summary>
        /// The define the Mutex to the DataBase.
        /// </summary>
        public Mutex mutexMazes = new Mutex();
        public Mutex mutexBfs = new Mutex();
        public Mutex mutexDfs = new Mutex();
        public Mutex mutexGameWating = new Mutex();
        public Mutex mutexGamePlaying = new Mutex();

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelDataBase"/> class.
        /// </summary>
        public ModelDataBase()
        {
            mazes = new Dictionary<string, Maze>();
            bfsSolutions = new Dictionary<string, Solution<Position>>();
            dfsSolutions = new Dictionary<string, Solution<Position>>();
            gameWating = new Dictionary<string, GameMultiPlayer>();
            gamesPlaying = new Dictionary<string, GameMultiPlayer>();
        }
        /// <summary>
        /// Gets the mazes.
        /// </summary>
        /// <value>
        /// The mazes.
        /// </value>
        public Dictionary<string, Maze> Mazes
        {
            get { return mazes; }
        }
        /// <summary>
        /// Gets the BFS solutions.
        /// </summary>
        /// <value>
        /// The BFS solutions.
        /// </value>
        public Dictionary<string, Solution<Position>> BfsSolutions
        {
            get { return bfsSolutions; }
        }
        /// <summary>
        /// Gets the DFS solutions.
        /// </summary>
        /// <value>
        /// The DFS solutions.
        /// </value>
        public Dictionary<string, Solution<Position>> DfsSolutions
        {
            get { return dfsSolutions; }
        }
        /// <summary>
        /// Gets the game wating.
        /// </summary>
        /// <value>
        /// The game wating.
        /// </value>
        public Dictionary<string, GameMultiPlayer> GameWating
        {
            get { return gameWating; }
        }
        /// <summary>
        /// Gets the games playing.
        /// </summary>
        /// <value>
        /// The games playing.
        /// </value>
        public Dictionary<string, GameMultiPlayer> GamesPlaying
        {
            get { return gamesPlaying; }
        }

    }
}
