using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BingoClient
{
    public partial class CardConfigurator : Form
    {
        public BingoDataTable BingoDataTable { get; private set; }

        internal CardConfigurator(CardConfiguration cardConfiguration)
        {
            InitializeComponent();

            if (cardConfiguration.Numbers == null)
            {
                this.BingoDataTable = new BingoDataTable();
                this.StartScreenRecognition(cardConfiguration);
            }
            else
            {
                this.BingoDataTable = cardConfiguration.Numbers;
            }
            this.dataGridView1.DataSource = this.BingoDataTable;
        }

        private void StartScreenRecognition(CardConfiguration cardConfiguration)
        {
            Color BColor = Color.FromArgb(69, 121, 40);
            Color IColor = Color.FromArgb(175, 138, 0);
            Color NColor = Color.FromArgb(147, 59, 13);
            Color GColor = Color.FromArgb(105, 50, 105);
            Color OColor = Color.FromArgb(15, 100, 121);
            int? number;
            for (int i = 0; i < 5; i++)
            {
                if (ScanPoint(cardConfiguration.BPoints[i], BColor, out number))
                {
                    this.BingoDataTable.Rows[i]["B"] = number.Value;
                }
                if (ScanPoint(cardConfiguration.IPoints[i], IColor, out number))
                {
                    this.BingoDataTable.Rows[i]["I"] = number.Value;
                }
                if (ScanPoint(cardConfiguration.NPoints[i], NColor, out number))
                {
                    this.BingoDataTable.Rows[i]["N"] = number.Value;
                }
                if (ScanPoint(cardConfiguration.GPoints[i], GColor, out number))
                {
                    this.BingoDataTable.Rows[i]["G"] = number.Value;
                }
                if (ScanPoint(cardConfiguration.OPoints[i], OColor, out number))
                {
                    this.BingoDataTable.Rows[i]["O"] = number.Value;
                }
            }
        }

        private bool ScanPoint(Point p, Color color, out int? number)
        {
            Bitmap pointBitmap = scanAroundPoint(p, 20);
            Bitmap dryBitmap = getDryBitmap(pointBitmap, color);
            if (dryBitmap != null) dryBitmap.Save(DateTime.Now.Ticks.ToString() + ".png");
            number = detectNumber(dryBitmap);
            return number.Value > -1;
        }

        private Bitmap getDryBitmap(Bitmap pointBitmap, Color color)
        {
            int minX = Int32.MaxValue, minY = Int32.MaxValue, maxX = 0, maxY = 0;
            Color grey = Color.FromArgb(67, 57, 38);
            for (int x = 0; x < pointBitmap.Width; x++)
            {
                for (int y = 0; y < pointBitmap.Height; y++)
                {
                    Color pixel = pointBitmap.GetPixel(x, y);
                    if (pixel == color || pixel == grey)
                    {
                        pointBitmap.SetPixel(x, y, Color.Black);
                        minX = x < minX ? x : minX;
                        maxX = x > maxX ? x : maxX;
                        minY = y < minY ? y : minY;
                        maxY = y > maxY ? y : maxY;
                    }
                    else
                    {
                        pointBitmap.SetPixel(x, y, Color.White);
                    }
                }
            }
            if (minX < Int32.MaxValue && minY < Int32.MaxValue && maxX > 0 && maxY > 0)
            {
                Bitmap reducedPointBitmap = copyRectangle(pointBitmap, new Rectangle(-minX, -minY, maxX - minX + 1, maxY - minY + 1));
                return reducedPointBitmap;
            }
            return null;
        }

        private Bitmap scanAroundPoint(Point p, int distance)
        {
            Rectangle r = new Rectangle(new Point(p.X - distance, p.Y - distance), new Size(distance * 2, distance * 2));
            Bitmap pointBitmap = new Bitmap(distance * 2, distance * 2, PixelFormat.Format32bppArgb);
            Graphics pointGraphics = Graphics.FromImage(pointBitmap);
            pointGraphics.CopyFromScreen(r.X, r.Y, 0, 0, r.Size, CopyPixelOperation.SourceCopy);
            return pointBitmap;
        }

        private int detectNumber(Bitmap reducedPointBitmap)
        {
            for (int i = 0; i <= 75; i++)
            {
                if (CompareBitmaps(BingoClient.NumberBitmaps[i], reducedPointBitmap))
                {
                    return i;
                }
            }
            return -1;
        }

        private bool CompareBitmaps(Bitmap a, Bitmap b)
        {
            if (a == null || b == null)
            {
                return false;
            }

            if (a.Size.Width != b.Size.Width || a.Size.Height != b.Size.Height)
            {
                return false;
            }

            for (int x = 0; x < a.Size.Width; x++)
            {
                for (int y = 0; y < a.Size.Height; y++)
                {
                    if (a.GetPixel(x, y) != b.GetPixel(x, y))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private Bitmap copyRectangle(Bitmap source, Rectangle r)
        {
            Bitmap dest = new Bitmap(r.Width, r.Height);
            Graphics destGraphics = Graphics.FromImage(dest);
            destGraphics.DrawImageUnscaled(source, r);
            return dest;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            BingoDataTable bdt = (BingoDataTable)this.dataGridView1.DataSource;

            foreach (DataRow dr in bdt.Rows)
            {
                if (!ValidateColumn(dr, "B", 1, 15, false))
                {
                    return;
                }

                if (!ValidateColumn(dr, "I", 16, 30, false))
                {
                    return;
                }

                if (!ValidateColumn(dr, "N", 31, 45, bdt.Rows.IndexOf(dr) == 2))
                {
                    return;
                }

                if (!ValidateColumn(dr, "G", 46, 60, false))
                {
                    return;
                }

                if (!ValidateColumn(dr, "O", 60, 75, false))
                {
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        private bool ValidateColumn(DataRow dr, string columnName, int minValue, int maxValue, bool allowNull)
        {
            return allowNull || (dr[columnName] != DBNull.Value && minValue <= dr.Field<int>(columnName) && dr.Field<int>(columnName) <= maxValue);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.BingoDataTable = null;
            Close();
        }
    }
}
