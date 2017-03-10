using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoClient
{
    public class CardConfiguration
    {
        public List<Point> BPoints { get; set; }
        public List<Point> IPoints { get; set; }
        public List<Point> NPoints { get; set; }
        public List<Point> GPoints { get; set; }
        public List<Point> OPoints { get; set; }
        public Dictionary<BingoColumns, List<Point>> Column { get; set; }
        public Point BingoButton { get; set; }
        public BingoDataTable Numbers { get; set; }
        public List<Point> SelectedNumbers { get; set; }
        public int Bingos { get; set; }

        public CardConfiguration()
        {
            this.BPoints = new List<Point>();
            this.IPoints = new List<Point>();
            this.NPoints = new List<Point>();
            this.GPoints = new List<Point>();
            this.OPoints = new List<Point>();
            this.Column = new Dictionary<BingoColumns, List<Point>>();
            this.Column[BingoColumns.B] = this.BPoints;
            this.Column[BingoColumns.I] = this.IPoints;
            this.Column[BingoColumns.N] = this.NPoints;
            this.Column[BingoColumns.G] = this.GPoints;
            this.Column[BingoColumns.O] = this.OPoints;
            this.BingoButton = new Point();

            this.ResetForNewMatch();
        }

        internal void ResetForNewMatch()
        {
            this.Numbers = null;
            this.SelectedNumbers = new List<Point>() { new Point(2, 2) };
            this.Bingos = 0;
        }
    }
}
