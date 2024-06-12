using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinefieldConsole
{
    public class Square
    {
        public Square(string column, int row)
        {
            this.Column = column;
            this.Row = row;
        }

        public string Column { get; set; }
        public int Row { get; set; }
        public bool IsMined { get; set; }
    }
}
