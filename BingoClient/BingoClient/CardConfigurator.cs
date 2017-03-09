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
        private readonly CardConfiguration cardConfiguration;

        internal CardConfigurator(CardConfiguration cardConfiguration)
        {
            InitializeComponent();

            if (cardConfiguration.Numbers == null)
            {
                this.BingoDataTable = new BingoDataTable();
            }
            else
            {
                this.BingoDataTable = cardConfiguration.Numbers;
            }
            this.cardConfiguration = cardConfiguration;
            this.dataGridView1.DataSource = this.BingoDataTable;
        }

        internal void StartScreenRecognition(CardConfiguration cardConfiguration)
        {
            int? number;
            for (int i = 0; i < 5; i++)
            {
                if (ScanPoint(cardConfiguration.BPoints[i], BingoColors.Cards.BColor, out number))
                {
                    this.BingoDataTable.Rows[i]["B"] = number.Value;
                }
                if (ScanPoint(cardConfiguration.IPoints[i], BingoColors.Cards.IColor, out number))
                {
                    this.BingoDataTable.Rows[i]["I"] = number.Value;
                }
                if (ScanPoint(cardConfiguration.NPoints[i], BingoColors.Cards.NColor, out number))
                {
                    this.BingoDataTable.Rows[i]["N"] = number.Value;
                }
                if (ScanPoint(cardConfiguration.GPoints[i], BingoColors.Cards.GColor, out number))
                {
                    this.BingoDataTable.Rows[i]["G"] = number.Value;
                }
                if (ScanPoint(cardConfiguration.OPoints[i], BingoColors.Cards.OColor, out number))
                {
                    this.BingoDataTable.Rows[i]["O"] = number.Value;
                }
            }
        }

        private bool ScanPoint(Point p, Color color, out int? number)
        {
            Bitmap pointBitmap = scanAroundPoint(p, 20);
            Bitmap dryBitmap = BitmapUtilities.DryBitmap(pointBitmap, new Color[] { color, BingoColors.Cards.Grey });
            //if (dryBitmap != null) dryBitmap.Save(DateTime.Now.Ticks.ToString() + ".png");
            number = BitmapUtilities.DetectNumber(BingoClient.CardsNumbersBitmaps, dryBitmap);
            return number.Value > -1;
        }

        private Bitmap scanAroundPoint(Point p, int distance)
        {
            Rectangle r = new Rectangle(new Point(p.X - distance, p.Y - distance), new Size(distance * 2, distance * 2));
            Bitmap pointBitmap = new Bitmap(distance * 2, distance * 2, PixelFormat.Format32bppArgb);
            Graphics pointGraphics = Graphics.FromImage(pointBitmap);
            pointGraphics.CopyFromScreen(r.X, r.Y, 0, 0, r.Size, CopyPixelOperation.SourceCopy);
            return pointBitmap;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!this.ValidateDataSource())
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        internal bool ValidateDataSource()
        {
            BingoDataTable bdt = (BingoDataTable)this.dataGridView1.DataSource;

            foreach (DataRow dr in bdt.Rows)
            {
                if (!ValidateColumn(dr, "B", 1, 15, false))
                {
                    return false;
                }

                if (!ValidateColumn(dr, "I", 16, 30, false))
                {
                    return false;
                }

                if (!ValidateColumn(dr, "N", 31, 45, bdt.Rows.IndexOf(dr) == 2))
                {
                    return false;
                }

                if (!ValidateColumn(dr, "G", 46, 60, false))
                {
                    return false;
                }

                if (!ValidateColumn(dr, "O", 60, 75, false))
                {
                    return false;
                }
            }
            return true;
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

        private void CardConfigurator_Load(object sender, EventArgs e)
        {
            if (this.cardConfiguration.Numbers == null)
            {
                this.StartScreenRecognition(this.cardConfiguration);
            }
        }
    }
}
