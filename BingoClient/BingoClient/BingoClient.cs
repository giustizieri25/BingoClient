using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace BingoClient
{
    public partial class BingoClient : Form
    {
        private BingoConfiguration CurrentConfiguration;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        public BingoClient()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (CurrentConfiguration == null)
            {
                PickConfiguration();
            }

            this.tableLayoutPanel1.Controls.Clear();
            this.tableLayoutPanel1.ColumnStyles.Clear();
            this.tableLayoutPanel1.RowStyles.Clear();
            this.tableLayoutPanel1.ColumnCount = this.CurrentConfiguration.Columns;
            for (int i = 0; i < this.tableLayoutPanel1.ColumnCount; i++)
            {
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100 / this.tableLayoutPanel1.ColumnCount));
            }
            this.tableLayoutPanel1.RowCount = this.CurrentConfiguration.Rows;
            for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
            {
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100 / this.tableLayoutPanel1.RowCount));
            }

            int buttonsCreated = 0;
            for (int r = 0; r < this.CurrentConfiguration.Rows; r++)
            {
                for (int c = 0; c < this.CurrentConfiguration.Columns && buttonsCreated++ < this.CurrentConfiguration.CardConfigurations.Count; c++)
                {
                    Button b = new Button();
                    b.Text = "Add";
                    b.Click += CardConfigurator_Click;
                    b.Dock = DockStyle.Fill;
                    b.TextAlign = ContentAlignment.MiddleCenter;
                    this.tableLayoutPanel1.Controls.Add(b, c, r);
                }
            }
        }

        private void CardConfigurator_Click(object sender, EventArgs e)
        {
            TableLayoutPanelCellPosition cell = this.tableLayoutPanel1.GetCellPosition((Control)sender);
            int index = cell.Row * this.CurrentConfiguration.Columns + cell.Column;
            CardConfigurator cc = new CardConfigurator(this.CurrentConfiguration.CardConfigurations[index]);
            if (cc.ShowDialog(this) == DialogResult.OK)
            {
                this.CurrentConfiguration.CardConfigurations[index].Numbers = cc.BingoDataTable;
            }
        }

        private void PickConfiguration()
        {
            ConfigPicker configPicker = new ConfigPicker();

            if (configPicker.ShowDialog(this) == DialogResult.OK)
            {
                this.CurrentConfiguration = configPicker.SelectedCofiguration;
                this.labelConfiguration.Text = this.CurrentConfiguration.Name;
            }
        }

        private void ClickPointsAndRestore(IEnumerable<Point> points, int waitMs, bool doubleClick)
        {
            Point cursorPoint = Cursor.Position;
            foreach (Point p in points)
            {
                ClickAtPoint(p, waitMs, doubleClick);
            }
            this.Activate();
            Cursor.Position = cursorPoint;
        }

        private void ClickAtPoint(Point p, int waitMs, bool doubleClick)
        {
            Cursor.Position = new Point(p.X, p.Y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, p.X, p.Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, p.X, p.Y, 0, 0);

            if (waitMs > 0)
            {
                Thread.Sleep(waitMs);
            }
            if (doubleClick)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN, p.X, p.Y, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, p.X, p.Y, 0, 0);

                if (waitMs > 0)
                {
                    Thread.Sleep(waitMs);
                }
            }
        }

        private void buttonB_Click(object sender, EventArgs e)
        {
            BingoSelectedEventArgs args = e is BingoSelectedEventArgs ? e as BingoSelectedEventArgs : new BingoSelectedEventArgs();
            args.AllPoints = cc => cc.BPoints;
            buttonColumn_Clicked(args);
        }

        private void buttonI_Click(object sender, EventArgs e)
        {
            BingoSelectedEventArgs args = e is BingoSelectedEventArgs ? e as BingoSelectedEventArgs : new BingoSelectedEventArgs();
            args.AllPoints = cc => cc.IPoints;
            buttonColumn_Clicked(args);
        }

        private void buttonN_Click(object sender, EventArgs e)
        {
            BingoSelectedEventArgs args = e is BingoSelectedEventArgs ? e as BingoSelectedEventArgs : new BingoSelectedEventArgs();
            args.AllPoints = cc => cc.NPoints;
            buttonColumn_Clicked(args);
        }

        private void buttonG_Click(object sender, EventArgs e)
        {
            BingoSelectedEventArgs args = e is BingoSelectedEventArgs ? e as BingoSelectedEventArgs : new BingoSelectedEventArgs();
            args.AllPoints = cc => cc.GPoints;
            buttonColumn_Clicked(args);
        }

        private void buttonO_Click(object sender, EventArgs e)
        {
            BingoSelectedEventArgs args = e is BingoSelectedEventArgs ? e as BingoSelectedEventArgs : new BingoSelectedEventArgs();
            args.AllPoints = cc => cc.OPoints;
            buttonColumn_Clicked(args);
        }

        private IEnumerable<Point> SearchNumberOrColumns(string column, int number, Func<CardConfiguration, IEnumerable<Point>> allPoints)
        {
            List<Point> points = new List<Point>();
            foreach (CardConfiguration cardConfiguration in this.CurrentConfiguration.CardConfigurations)
            {
                if (cardConfiguration.Numbers != null)
                {
                    DataRow dr = cardConfiguration.Numbers.Select(string.Format("{0}={1}", column, number)).FirstOrDefault();
                    int index = cardConfiguration.Numbers.Rows.IndexOf(dr);
                    if (index > -1)
                    {
                        switch (column)
                        {
                            case "b":
                                points.Add(cardConfiguration.BPoints[index]);
                                break;
                            case "i":
                                points.Add(cardConfiguration.IPoints[index]);
                                break;
                            case "n":
                                points.Add(cardConfiguration.NPoints[index]);
                                break;
                            case "g":
                                points.Add(cardConfiguration.GPoints[index]);
                                break;
                            case "o":
                                points.Add(cardConfiguration.OPoints[index]);
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    points.AddRange(allPoints(cardConfiguration));
                }
            }
            return points;
        }

        private void buttonColumn_Clicked(BingoSelectedEventArgs e)
        {
            IEnumerable<Point> points;
            if (e.Number.HasValue)
            {
                points = SearchNumberOrColumns(e.Column, e.Number.Value, e.AllPoints);
            }
            else
            {
                points = this.CurrentConfiguration.CardConfigurations.SelectMany(e.AllPoints).ToList();
            }
            ClickPointsAndRestore(points, 10, true);
        }

        private void buttonALL_Click(object sender, EventArgs e)
        {
            IEnumerable<Point> allpoints = this.CurrentConfiguration.CardConfigurations.SelectMany(cc =>
                (cc.BPoints)
                .Concat(cc.IPoints)
                .Concat(cc.NPoints)
                .Concat(cc.GPoints)
                .Concat(cc.OPoints)
                .Concat(checkBoxCallBingos.Checked ? new Point[] { cc.BingoButton } : new Point[] { })).ToList();

            ClickPointsAndRestore(allpoints, 10, false);
        }

        private void textBoxInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBoxInput.Text.Length == 1)
            {
                switch (textBoxInput.Text)
                {
                    case "b":
                        buttonB_Click(null, new BingoSelectedEventArgs());
                        textBoxInput.Clear();
                        break;
                    case "i":
                        buttonI_Click(null, new BingoSelectedEventArgs());
                        textBoxInput.Clear();
                        break;
                    case "n":
                        buttonN_Click(null, new BingoSelectedEventArgs());
                        textBoxInput.Clear();
                        break;
                    case "g":
                        buttonG_Click(null, new BingoSelectedEventArgs());
                        textBoxInput.Clear();
                        break;
                    case "o":
                        buttonO_Click(null, new BingoSelectedEventArgs());
                        textBoxInput.Clear();
                        break;
                    case "p":
                        buttonPower_Click(null, null);
                        textBoxInput.Clear();
                        break;
                    case "a":
                        buttonALL_Click(null, null);
                        textBoxInput.Clear();
                        break;
                    default:
                        if (!Char.IsDigit(textBoxInput.Text[0]))
                        {
                            textBoxInput.Clear();
                        }
                        break;
                }
            }
            else if (textBoxInput.Text.Length == 2)
            {
                int i;
                if (Int32.TryParse(textBoxInput.Text, out i))
                {
                    if (1 <= i && i <= 15)
                    {
                        buttonB_Click(null, new BingoSelectedEventArgs("b", i));
                        textBoxInput.Clear();
                    }
                    else if (16 <= i && i <= 30)
                    {
                        buttonI_Click(null, new BingoSelectedEventArgs("i", i));
                        textBoxInput.Clear();
                    }
                    else if (31 <= i && i <= 45)
                    {
                        buttonN_Click(null, new BingoSelectedEventArgs("n", i));
                        textBoxInput.Clear();
                    }
                    else if (46 <= i && i <= 60)
                    {
                        buttonG_Click(null, new BingoSelectedEventArgs("g", i));
                        textBoxInput.Clear();
                    }
                    else if (61 <= i && i <= 75)
                    {
                        buttonO_Click(null, new BingoSelectedEventArgs("o", i));
                        textBoxInput.Clear();
                    }
                    else
                    {
                        textBoxInput.Clear();
                    }
                }
                else
                {
                    textBoxInput.Clear();
                }
            }
        }

        private void buttonPower_Click(object sender, EventArgs e)
        {
            ClickPointsAndRestore(new Point[] { this.CurrentConfiguration.PowerButton }, 10, false);
        }

        private void buttonBingo_Click(object sender, EventArgs e)
        {
            IEnumerable<Point> bingos = this.CurrentConfiguration.CardConfigurations.Select(cc => cc.BingoButton);
            ClickPointsAndRestore(bingos, 350, false);
        }
    }
}
