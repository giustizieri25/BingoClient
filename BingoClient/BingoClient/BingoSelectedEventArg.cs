using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoClient
{
    public class BingoSelectedEventArgs : EventArgs
    {
        public string Column { get; set; }
        public int? Number { get; set; }
        public Func<CardConfiguration, IEnumerable<Point>> AllPoints { get; set; }

        public BingoSelectedEventArgs()
        {

        }

        public BingoSelectedEventArgs(string column, int number)
        {
            this.Column = column;
            this.Number = number;
        }
    }
}
