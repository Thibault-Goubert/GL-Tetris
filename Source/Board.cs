using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    public class Board
    {
        public static readonly char EMPTY = '.';
        public int Columns, Rows;

        char[,] board;
        MovableGrid fallingBlock;

        public Board(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;
            board = new char[rows, columns];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    board[row, col] = EMPTY;
                }
            }
        }

        public override String ToString()
        {
            String s = "";
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    if (IsFallingBlock())
                    {
                        s += fallingBlock.IsAt(row, col) ? 
                            fallingBlock.CellAt(row, col) : board[row, col];
                    }
                    else s += board[row, col];
                }
                s += "\n";
            }
            return s;
        }

        public void FromString(String blocks)
        {
            StringToMatrix converter = new StringToMatrix(blocks);
            board = converter.blocks;
            Rows = converter.rows;
            Columns = converter.columns;
        }

        public void Drop(Tetromino shape)
        {
            CheckIsFalling();
            int r = StartingRowOffset(shape);
            fallingBlock = new MovableGrid(shape).MoveTo(r, Columns / 2 - shape.Columns() / 2);
        }

        static int StartingRowOffset(Grid shape)
        {
            for (int r = 0; r < shape.Rows(); r++)
                for (int c = 0; c < shape.Columns(); c++)
                    if (shape.CellAt(r, c) != EMPTY) return -r;
            return 0;
        }

        void CheckIsFalling()
        {
            if (IsFallingBlock())
                throw new ArgumentException("Another block is already falling!");
        }

        public bool IsFallingBlock()
        {
            return fallingBlock != null;
        }

        public void Tick()
        {
            MovableGrid test = fallingBlock.MoveDown();
            if (ConflictWithBoard(test)) StopFallingBlock();            
            else fallingBlock = test;            
        }

        public void MoveDown()
        {
            if (!IsFallingBlock()) return;

            MovableGrid test = fallingBlock.MoveDown();
            if (ConflictWithBoard(test))
            {
                StopFallingBlock();
                RemoveFullRows();
            }
            else fallingBlock = test;
        }
        public void MoveLeft()
        {
            if (!IsFallingBlock()) return;

            TryMove(fallingBlock.MoveLeft());
        }
        public void MoveRight()
        {
            if (!IsFallingBlock()) return;

            TryMove(fallingBlock.MoveRight());
        }

        private void TryMove(MovableGrid test)
        {
            if (!ConflictWithBoard(test)) 
                fallingBlock = test;
        }
        public void RotateLeft()
        {
            if (!IsFallingBlock()) return;
            TryRotate(fallingBlock.RotateLeft());
        }
        public void RotateRight()
        {
            if (!IsFallingBlock()) return;
            TryRotate(fallingBlock.RotateRight());
        }
        private void TryRotate(MovableGrid test)
        {
            MovableGrid[] moves =
            {
                test,
                test.MoveLeft(),
                test.MoveRight(),
                test.MoveLeft().MoveLeft(),
                test.MoveRight().MoveRight()
            };
            foreach(MovableGrid move in moves)
            {
                if (!ConflictWithBoard(move))
                {
                    fallingBlock = move;
                    return;
                }
            }
        }
        private void RemoveFullRows()
        {
            RemoveRows(FindFullRows());
        }
        private void RemoveRows(List<int> rowsToRemove)
        {
            foreach (int rowIndex in rowsToRemove)
            {
                SqueezeRow(rowIndex);
            }
        }

        private void SqueezeRow(int rowIndex)
        {
            for (int row = rowIndex-1; row >= 0; row--)
            {
                for (int col = 0; col < Columns; col++)
                {
                    board[row + 1, col] = board[row, col];
                }
            }
        }

        private List<int> FindFullRows()
        {
            List<int> fullRows = new List<int>();
            for (int row = 0; row < Rows; row++)
            {
                if (RowIsFull(row))
                {
                    fullRows.Add(row);
                }
            }
            return fullRows;
        }

        private bool RowIsFull(int row)
        {
            for (int col = 0; col < Columns; col++)
            {
                if (board[row, col] == EMPTY)
                {
                    return false;
                }
            }
            return true;
        }

        private void StopFallingBlock()
        {
            CopyToBoard(fallingBlock);
            fallingBlock = null;
        }

        private void CopyToBoard(MovableGrid block)
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    if (block.IsAt(row, col)){
                        board[row, col] = block.CellAt(row, col);
                    }
                }
            }
        }

        private bool ConflictWithBoard(MovableGrid block)
        {
            return block.OutsideBoard(this) || block.HitsAnotherBlock(this);
        }

        public char CellAt(int row, int col)
        {
            return board[row, col];
        }

        private bool HitsAnotherBlock(Block block)
        {
            return board[block.Row, block.Col] != EMPTY;
        }

        private bool OutsideBoard(Block block)
        {
            return block.Row >= Rows;
        }


    }
}
