using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    public interface Grid
    {
        int Rows();
        int Columns();
        char CellAt(int row, int col);
    }
}
