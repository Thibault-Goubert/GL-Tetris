using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    public class Piece : Grid
    {
        int rows, columns;
        char[,] blocks;

        public Piece(string shape)
        {
            StringToMatrix s2m = new StringToMatrix(shape);
            blocks = s2m.blocks;
            rows = s2m.rows;
            columns = s2m.columns;
        }

        public char CellAt(int row, int col)
        {
            return blocks[row, col];
        }

        public int Columns()
        {
            return columns;
        }

        public int Rows()
        {
            return rows;
        }

        public override string ToString()
        {
            return StringToMatrix.Inverse(blocks, rows, columns);
        }
    }
}
