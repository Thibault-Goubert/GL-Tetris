using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    public class Tetromino : Grid
    {
        readonly Piece[] orientations;
        readonly int currentOrientation;

        public Tetromino(params string[] pieces)
        {
            currentOrientation = 0;
            orientations = new Piece[pieces.Length];
            for (int i = 0; i < pieces.Length; i++)
            {
                orientations[i] = new Piece(pieces[i]);
            }
        }

        private Tetromino(int current, Piece[] orientations)
        {
            this.currentOrientation = (current + orientations.Length) % orientations.Length;
            this.orientations = orientations;
        }

        public override string ToString()
        {
            return Current().ToString();
        }
        private Piece Current()
        {
            return this.orientations[this.currentOrientation];
        }

        public Tetromino RotateRight()
        {
            return new Tetromino(this.currentOrientation + 1, this.orientations);
        }

        public Tetromino RotateLeft()
        {
            return new Tetromino(this.currentOrientation - 1, this.orientations);
        }

        public int Rows()
        {
            return Current().Rows();
        }

        public int Columns()
        {
            return Current().Columns();
        }

        public char CellAt(int row, int col)
        {
            return Current().CellAt(row, col);
        }

        public static readonly Tetromino T_SHAPE
            = new Tetromino(
                "....\n" +
                "TTT.\n" +
                ".T..\n"
            ,
                ".T..\n" +
                "TT..\n" +
                ".T..\n"
            ,
                "....\n" +
                ".T..\n" +
                "TTT.\n"
            ,
                ".T..\n" +
                ".TT.\n" +
                ".T..\n"
        );

        public static readonly Tetromino L_SHAPE
            = new Tetromino(
                "....\n" +
                "LLL.\n" +
                "L...\n"
            ,
                "LL..\n" +
                ".L..\n" +
                ".L..\n"
            ,
                "....\n" +
                "..L.\n" +
                "LLL.\n"
            ,
                ".L..\n" +
                ".L..\n" +
                ".LL.\n"
        );

        public static readonly Tetromino J_SHAPE
            = new Tetromino(
                "....\n" +
                "JJJ.\n" +
                "..J.\n"
            ,
                ".J..\n" +
                ".J..\n" +
                "JJ..\n"
            ,
                "....\n" +
                "J...\n" +
                "JJJ.\n"
            ,
                ".JJ.\n" +
                ".J..\n" +
                ".J..\n"
        );

        public static readonly Tetromino I_SHAPE
            = new Tetromino(
                "....\n" +
                "IIII\n" +
                "....\n" +
                "....\n"
            ,
                "..I.\n" +
                "..I.\n" +
                "..I.\n" +
                "..I.\n"
        );

        public static readonly Tetromino S_SHAPE
            = new Tetromino(
                "....\n" +
                ".SS.\n" +
                "SS..\n"
            ,
                "S...\n" +
                "SS..\n" +
                ".S..\n"
        );

        public static readonly Tetromino Z_SHAPE
            = new Tetromino(
                "....\n" +
                "ZZ..\n" +
                ".ZZ.\n"
            ,
                "..Z.\n" +
                ".ZZ.\n" +
                ".Z..\n"
        );

        public static readonly Tetromino O_SHAPE = new Tetromino(
            ".00.\n"+
            ".00.\n"
        );
    }
}
