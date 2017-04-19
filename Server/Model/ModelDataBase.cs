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
    public class ModelDataBase
    {
        private Dictionary<string, Maze> mazes;
        private Dictionary<string, Solution<Position>> bfsSolutions;
        private Dictionary<string, Solution<Position>> dfsSolutions;
        private Dictionary<string, GameMultiPlayer> gameWating;
        private Dictionary<string, GameMultiPlayer> gamesPlaying;

        public Mutex mutexMazes = new Mutex();
        public Mutex mutexBfs = new Mutex();
        public Mutex mutexDfs = new Mutex();
        public Mutex mutexGameWating = new Mutex();
        public Mutex mutexGamePlaying = new Mutex();

        public ModelDataBase()
        {
            mazes = new Dictionary<string, Maze>();
            bfsSolutions = new Dictionary<string, Solution<Position>>();
            dfsSolutions = new Dictionary<string, Solution<Position>>();
            gameWating = new Dictionary<string, GameMultiPlayer>();
            gamesPlaying = new Dictionary<string, GameMultiPlayer>();
        }

        public Dictionary<string, Maze> Mazes
        {
            get { return mazes; }
        }
        public Dictionary<string, Solution<Position>> BfsSolutions
        {
            get { return bfsSolutions; }
        }
        public Dictionary<string, Solution<Position>> DfsSolutions
        {
            get { return dfsSolutions; }
        }
        public Dictionary<string, GameMultiPlayer> GameWating
        {
            get { return gameWating; }
        }
        public Dictionary<string, GameMultiPlayer> GamesPlaying
        {
            get { return gamesPlaying; }
        }

    }
}
