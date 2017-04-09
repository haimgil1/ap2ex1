using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLib
{
    public struct Position 
    {
        public int Row { get; set; } // row
        public int Col { get; set; } // column
        
        public Position(int row, int col)
        {
            Row = row;
            Col = col;            
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", Row, Col);
        }
        
        // in struct no need to override Equals()

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + Row;
            hash = hash * 23 + Col;                   
            return hash;
        }
    }
}
