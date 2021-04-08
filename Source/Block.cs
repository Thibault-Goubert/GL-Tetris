using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    public class Block
    {
        public char Shape { get; private set; } 
        public int Row { get; private set; }
        public int Col { get; private set; }

        public Block(char s) : this(0,0,s) { }

        private Block(int r, int c, char s)
        {
            this.Row = r;
            this.Col = c;
            this.Shape = s;
        }

        public Block MoveTo(int r, int c)
        {
            return new Block(r, c, Shape);
        }

        public bool Trouver(int r, int c)
        {
            return r == this.Row && c == this.Col;
        }

        public Block MoveDown()
        {
            return new Block(this.Row + 1, this.Col, Shape);
        }
    }
}
