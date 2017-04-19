using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorLib
{
    public interface IMazeGenerator
    {
        Maze Generate(int rows, int cols);
    }
}
