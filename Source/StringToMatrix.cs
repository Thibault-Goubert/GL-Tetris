using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    public class StringToMatrix
    {
        public char[,] blocks;
        public int rows, columns;

        public StringToMatrix(string shape)
        {
            //Un tableau de char de 1 char ?? bon, on peut pas utiliser l'option sinon mais quand même =O
            string[] lines = shape.Split(new char[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);

            rows = lines.Length;
            columns = lines[0].Length;

            blocks = new char[rows, columns];

            for (int row = 0; row < rows; row++)
            {
                char[] oneLine = lines[row].ToCharArray();
                if (oneLine.Length != columns) throw new Exception("Rows must be the same size");
                for (int col = 0; col < columns; col++) blocks[row, col] = oneLine[col];
            }
        }

        public static string Inverse(char[,] matrix, int rows, int cols)
        {
            string matrixToString = "";
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    matrixToString += matrix[r, c];
                }
                matrixToString += "\n";
            }
            return matrixToString;
        }
    }
}
