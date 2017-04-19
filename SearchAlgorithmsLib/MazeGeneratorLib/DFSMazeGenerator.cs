using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorLib
{
    public class DFSMazeGenerator : IMazeGenerator
    {
        private Random rand = new Random();

        /// <summary>
        /// This method generates a new maze by running a DFS algorithm
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public Maze Generate(int rows, int cols)
        {
            Maze maze = new Maze(rows, cols);

            // Initialize maze with walls
            InitMaze(maze);

            Stack<Position> stack = new Stack<Position>();

            // Choose a random starting position and mark it as free
            maze.InitialPos = GetRandomStartPosition(maze);
            maze[maze.InitialPos.Row, maze.InitialPos.Col] = CellType.Free;
            stack.Push(maze.InitialPos);

            while (stack.Any())
            {
                Position c = stack.Peek();
                List<Position> neighbors = GetUnvisitedNeighbors(maze, c);

                if (neighbors.Count > 0)
                {
                    // Pick one of the neighbors randomly
                    int idx = rand.Next(0, neighbors.Count);
                    Position neighbor = neighbors[idx];
                    CreatePassageBetweenCells(maze, c, neighbor);
                    stack.Push(neighbor);
                }
                else
                {
                    stack.Pop();
                }
            }

            maze.GoalPos = GetRandomGoalPosition(maze);
            return maze;
        }

        /// <summary>
        /// Start with a maze that contains only walls
        /// </summary>
        /// <param name="maze"></param>
        private void InitMaze(Maze maze)
        {
            for (int i = 0; i < maze.Rows; i++)
            {
                for (int j = 0; j < maze.Cols; j++)
                {
                    maze[i, j] = CellType.Wall;
                }
            }
        }

        /// <summary>
        /// Choose a start position randomly. Start position must reside on an even row and col.
        /// </summary>
        /// <param name="maze"></param>
        /// <returns></returns>
        private Position GetRandomStartPosition(Maze maze)
        {
            int row = rand.Next(0, maze.Rows);
            while (row % 2 == 1)
            {
                row = rand.Next(0, maze.Rows);
            }

            int col = rand.Next(0, maze.Cols);
            while (col % 2 == 1)
            {
                col = rand.Next(0, maze.Cols);
            }

            return new Position(row, col);
        }

        /// <summary>
        /// Find all the unvisited neighbors of a given cell
        /// </summary>
        /// <param name="maze"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        private List<Position> GetUnvisitedNeighbors(Maze maze, Position c)
        {
            List<Position> neighbors = new List<Position>();
                       
            if (c.Col - 2 >= 0)
            {
                if (maze[c.Row, c.Col - 2] == CellType.Wall)
                {
                    neighbors.Add(new Position(c.Row, c.Col - 2));
                }
            }

            if (c.Col + 2 < maze.Cols)
            {
                if (maze[c.Row, c.Col + 2] == CellType.Wall)
                {
                    neighbors.Add(new Position(c.Row, c.Col + 2));
                }
            }

            if (c.Row - 2 >= 0)
            {
                if (maze[c.Row - 2, c.Col] == CellType.Wall)
                {
                    neighbors.Add(new Position(c.Row - 2, c.Col));
                }
            }

            if (c.Row + 2 < maze.Rows)
            {
                if (maze[c.Row + 2, c.Col] == CellType.Wall)
                {
                    neighbors.Add(new Position(c.Row + 2, c.Col));
                }
            }
            return neighbors;
        }

        private void CreatePassageBetweenCells(Maze maze, Position c, Position neighbor)
        {
            if (neighbor.Col == c.Col - 2)
            {
                maze[c.Row, c.Col - 1] = CellType.Free;
                maze[c.Row, c.Col - 2] = CellType.Free;
            }
            else if (neighbor.Col == c.Col + 2)
            {
                maze[c.Row, c.Col + 1] = CellType.Free;
                maze[c.Row, c.Col + 2] = CellType.Free;
            }
            else if (neighbor.Row == c.Row - 2)
            {
                maze[c.Row - 1, c.Col] = CellType.Free;
                maze[c.Row - 2, c.Col] = CellType.Free;
            }
            else
            {
                maze[c.Row + 1, c.Col] = CellType.Free;
                maze[c.Row + 2, c.Col] = CellType.Free;
            }
        }

        private Position GetRandomGoalPosition(Maze maze)
        {
            int row = rand.Next(0, maze.Rows);
            int col = rand.Next(0, maze.Cols);

            while (maze[row, col] == CellType.Wall)
            {
                row = rand.Next(0, maze.Rows);
                col = rand.Next(0, maze.Cols);
            }

            return new Position(row, col);
        }
    }
}
