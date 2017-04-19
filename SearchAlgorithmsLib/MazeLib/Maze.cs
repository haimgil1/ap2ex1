using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLib
{
    public enum CellType { Free, Wall };
    public enum Direction { Left, Right, Up, Down, Unknown = -1 };

    public class Maze
    {
        public string Name { get; set; }
        public int Rows { get; private set; }
        public int Cols { get; private set; }

        private CellType[,] cells;
        public Position InitialPos { get; set; }
        public Position GoalPos { get; set; }
                
        public Maze() { }
        public Maze(int rows, int cols)
        {            
            Rows = rows;
            Cols = cols;
            cells = new CellType[Rows, Cols];            
        }

        public CellType this[int row, int col]
        {
            get
            {
                return cells[row, col];
            }
            set
            {
                cells[row, col] = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (i == InitialPos.Row && j == InitialPos.Col)
                        sb.Append("*");
                    else if (i == GoalPos.Row && j == GoalPos.Col)
                        sb.Append("#");
                    else
                        sb.Append((int)this[i, j]);
                }
                sb.Append(Environment.NewLine);         
            }
            return sb.ToString();
        }

        private string ToStringWithoutSpace()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {                    
                    sb.Append((int)this[i, j]);
                }
            }
            return sb.ToString();
        }

        public string ToJSON()
        {
            JObject mazeObj = new JObject();
            mazeObj["Name"] = Name;            
            mazeObj["Maze"] = this.ToStringWithoutSpace();
            mazeObj["Rows"] = Rows;
            mazeObj["Cols"] = Cols;

            JObject startObj = new JObject();
            startObj["Row"] = InitialPos.Row;
            startObj["Col"] = InitialPos.Col;
            mazeObj["Start"] = startObj;

            JObject endObj = new JObject();
            endObj["Row"] = GoalPos.Row;
            endObj["Col"] = GoalPos.Col;
            mazeObj["End"] = endObj;

            return mazeObj.ToString();
        }

        public static Maze FromJSON(string str)
        {
            Maze maze = new Maze();

            JObject mazeObj = JObject.Parse(str);
            maze.Name = (string)mazeObj["Name"];
            string mazeData = (string)mazeObj["Maze"];
            maze.Rows = (int)mazeObj["Rows"];
            maze.Cols = (int)mazeObj["Cols"];

            maze.cells = new CellType[maze.Rows, maze.Cols];
            
            int k = 0;
            for (int i = 0; i < maze.Rows; i++)
            {
                for (int j = 0; j < maze.Cols; j++)
                {
                    if (mazeData[k] == '0')
                        maze[i, j] = CellType.Free;
                    else if (mazeData[k] == '1')
                        maze[i, j] = CellType.Wall;
                    else
                        throw new Exception("Unknown cell type");
                    k++;
                }                
            }

            maze.InitialPos = new Position((int)mazeObj["Start"]["Row"], (int)mazeObj["Start"]["Col"]);
            maze.GoalPos = new Position((int)mazeObj["End"]["Row"], (int)mazeObj["End"]["Col"]);

            return maze;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Maze maze = (Maze)obj;
            return this.ToString().Equals(maze.ToString());
        }
    }
}
