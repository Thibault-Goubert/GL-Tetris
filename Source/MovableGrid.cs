using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    class MovableGrid : Grid
    {
        int row, col;
        Tetromino inner;

        public MovableGrid(Tetromino inner) : this(0,0,inner) { }

        private MovableGrid(int outer_row, int outer_col, Tetromino inner)
        {
            this.row = outer_row;
            this.col = outer_col;
            this.inner = inner;
        }
        int ToInnerRow(int outer_row)
        {
            return outer_row - row;
        }
        int ToOuterRow(int inner_row)
        {
            return inner_row + row;
        }
        int ToInnerCol(int outer_col)
        {
            return outer_col - col;
        }
        int ToOuterCol(int inner_col)
        {
            return inner_col + col;
        }
        public MovableGrid MoveTo(int outer_row, int outer_col)
        {
            return new MovableGrid(outer_row, outer_col, inner);
        }
        public MovableGrid MoveDown()
        {
            return new MovableGrid(row + 1, col, inner);
        }
        public MovableGrid MoveLeft()
        {
            return new MovableGrid(row, col-1, inner);
        }
        public MovableGrid MoveRight()
        {
            return new MovableGrid(row, col+1, inner);
        }
        public MovableGrid RotateLeft()
        {
            return new MovableGrid(row, col, inner.RotateLeft());
        }
        public MovableGrid RotateRight()
        {
            return new MovableGrid(row, col, inner.RotateRight());
        }

        public bool OutsideBoard(Board board)
        {
            for (int r = 0; r < Rows(); r++)
            {
                for (int c = 0; c < Columns(); c++)
                {
                    if (inner.CellAt(r, c) != Board.EMPTY)
                    {
                        int outer_row = ToOuterRow(r);
                        int outer_col = ToOuterCol(c);
                        if (outer_col < 0 ||
                            outer_col >= board.Columns ||
                            outer_row < 0 ||
                            outer_row >= board.Rows) return true;
                    }
                }
            }
            return false;
        }

        public bool HitsAnotherBlock(Board board)
        {
            for (int r = 0; r < Rows(); r++) {
                for (int c = 0; c < Columns(); c++) {
                    if (inner.CellAt(r, c) != Board.EMPTY)
                    {
                        int outer_row = ToOuterRow(r);
                        int outer_col = ToOuterCol(c);
                        if (board.CellAt(outer_row, outer_col) != Board.EMPTY) 
                            return true;
                    }
                }
            }
            return false;
        }

        public int Rows()
        {
            return inner.Rows();
        }

        public int Columns()
        {
            return inner.Columns();
        }

        public char CellAt(int outer_row, int outer_col)
        {
            int inner_row = ToInnerRow(outer_row);
            int inner_col = ToInnerCol(outer_col);
            return inner.CellAt(inner_row, inner_col);
        }

        public bool IsAt(int outer_row, int outer_col)
        {
            int inner_row = ToInnerRow(outer_row);
            int inner_col = ToInnerCol(outer_col);
            return inner_row >= 0
                && inner_row < inner.Rows()
                && inner_col >= 0
                && inner_col < inner.Columns()
                && inner.CellAt(inner_row, inner_col) != Board.EMPTY;
        }
    }
}
