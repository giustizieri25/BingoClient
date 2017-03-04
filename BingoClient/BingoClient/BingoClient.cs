using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        public static Bitmap[] NumberBitmaps = new Bitmap[76];
        private int lastCallMatches, totalMatches, totalBingos;

        public BingoClient()
        {
            InitializeComponent();
        }

        private void form1_Load(object sender, EventArgs e)
        {
            if (CurrentConfiguration == null)
            {
                pickConfiguration();
            }

            createCardConfigurationButtons();
            preLoadNumberMasks();
        }

        private void preLoadNumberMasks()
        {
            string fileName;
            for (int i = 0; i <= 75; i++)
            {
                fileName = string.Format(@".\masks\{0}.png", i.ToString().PadLeft(2, '0'));
                if (File.Exists(fileName))
                {
                    BingoClient.NumberBitmaps[i] = (Bitmap)Bitmap.FromFile(fileName);
                }
            }
        }

        private void createCardConfigurationButtons()
        {
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
                    b.Click += cardConfigurator_Click;
                    b.Dock = DockStyle.Fill;
                    b.TextAlign = ContentAlignment.MiddleCenter;
                    this.tableLayoutPanel1.Controls.Add(b, c, r);
                }
            }
        }

        private void cardConfigurator_Click(object sender, EventArgs e)
        {
            TableLayoutPanelCellPosition cell = this.tableLayoutPanel1.GetCellPosition((Control)sender);
            if (this.createCardConfiguration(cell.Row, cell.Column, false))
            {
                Button senderButton = sender as Button;
                senderButton.Text = "Edit";
                //this.updateCardConfigurationThumbnail(senderControl.Parent, this.CurrentConfiguration.CardConfigurations[cell.Row * this.CurrentConfiguration.Columns + cell.Column]);
            }
        }

        private bool createCardConfiguration(int row, int column, bool silent)
        {
            int index = row * this.CurrentConfiguration.Columns + column;
            CardConfigurator cc = new CardConfigurator(this.CurrentConfiguration.CardConfigurations[index]);

            if (silent)
            {
                cc.StartScreenRecognition(this.CurrentConfiguration.CardConfigurations[index]);
                if (cc.ValidateDataSource())
                {
                    this.CurrentConfiguration.CardConfigurations[index].Numbers = cc.BingoDataTable;
                    return true;
                }
            }
            else
            {
                if (cc.ShowDialog(this) == DialogResult.OK)
                {
                    this.CurrentConfiguration.CardConfigurations[index].Numbers = cc.BingoDataTable;
                    return true;
                }
            }
            return false;
        }

        private void updateCardConfigurationThumbnail(Control parent, CardConfiguration cc)
        {
            this.SuspendLayout();
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.ColumnCount = 5;
            tlp.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            for (int i = 0; i < tlp.ColumnCount; i++)
            {
                tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100 / tlp.ColumnCount));
            }
            tlp.RowCount = 5;
            for (int i = 0; i < tlp.RowCount; i++)
            {
                tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100 / tlp.RowCount));
            }
            parent.Controls.Add(tlp);

            for (int i = 0; i < 5; i++)
            {
                tlp.Controls.Add(new Label() { Text = cc.Numbers.Rows[i]["B"].ToString(), Font = new System.Drawing.Font("Microsoft Sans Serif", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))) });
                tlp.Controls.Add(new Label() { Text = cc.Numbers.Rows[i]["I"].ToString(), Font = new System.Drawing.Font("Microsoft Sans Serif", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))) });
                tlp.Controls.Add(new Label() { Text = cc.Numbers.Rows[i]["N"].ToString(), Font = new System.Drawing.Font("Microsoft Sans Serif", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))) });
                tlp.Controls.Add(new Label() { Text = cc.Numbers.Rows[i]["G"].ToString(), Font = new System.Drawing.Font("Microsoft Sans Serif", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))) });
                tlp.Controls.Add(new Label() { Text = cc.Numbers.Rows[i]["O"].ToString(), Font = new System.Drawing.Font("Microsoft Sans Serif", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))) });
            }
            this.ResumeLayout(true);
        }

        private void pickConfiguration()
        {
            ConfigPicker configPicker = new ConfigPicker();

            if (configPicker.ShowDialog(this) == DialogResult.OK)
            {
                this.CurrentConfiguration = configPicker.SelectedCofiguration;
                this.labelConfiguration.Text = this.CurrentConfiguration.Name;
            }
        }

        private void clickPointsAndRestore(IEnumerable<Point> points, int waitMs, bool doubleClick)
        {
            Point cursorPoint = Cursor.Position;
            foreach (Point p in points)
            {
                clickAtPoint(p, waitMs, doubleClick);
            }
            this.Activate();
            Cursor.Position = cursorPoint;
        }

        private void clickAtPoint(Point p, int waitMs, bool doubleClick)
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

        private IEnumerable<Point> searchNumberOrColumns(string column, int number, Func<CardConfiguration, IEnumerable<Point>> allPoints)
        {
            List<Point> points = new List<Point>();
            lastCallMatches = 0;
            foreach (CardConfiguration cardConfiguration in this.CurrentConfiguration.CardConfigurations)
            {
                if (cardConfiguration.Numbers != null)
                {
                    DataRow dr = cardConfiguration.Numbers.Select(string.Format("{0}={1}", column, number)).FirstOrDefault();
                    int index = cardConfiguration.Numbers.Rows.IndexOf(dr);
                    if (index > -1)
                    {
                        lastCallMatches++;
                        switch (column)
                        {
                            case "b":
                                points.Add(cardConfiguration.BPoints[index]);
                                cardConfiguration.SelectedNumbers.Add(new Point(0, index));
                                break;
                            case "i":
                                points.Add(cardConfiguration.IPoints[index]);
                                cardConfiguration.SelectedNumbers.Add(new Point(1, index));
                                break;
                            case "n":
                                points.Add(cardConfiguration.NPoints[index]);
                                cardConfiguration.SelectedNumbers.Add(new Point(2, index));
                                break;
                            case "g":
                                points.Add(cardConfiguration.GPoints[index]);
                                cardConfiguration.SelectedNumbers.Add(new Point(3, index));
                                break;
                            case "o":
                                points.Add(cardConfiguration.OPoints[index]);
                                cardConfiguration.SelectedNumbers.Add(new Point(4, index));
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
            int interval;
            if (e.Number.HasValue)
            {
                points = searchNumberOrColumns(e.Column, e.Number.Value, e.AllPoints);
                toolStripStatusLabelLastCallMatches.Text = lastCallMatches.ToString();
                toolStripStatusLabelTotalMatches.Text = (totalMatches += lastCallMatches).ToString();
                toolStripStatusLabelBingos.Text = countBingos(this.CurrentConfiguration.CardConfigurations).ToString();
                interval = 100;
            }
            else
            {
                points = this.CurrentConfiguration.CardConfigurations.SelectMany(e.AllPoints).ToList();
                interval = 10;
            }
            clickPointsAndRestore(points, interval, true);
        }

        private int countBingos(IEnumerable<CardConfiguration> cardConfigurations)
        {
            int totalBingos = 0;

            foreach (CardConfiguration cc in cardConfigurations)
            {
                List<BingoTypesEnum> bingos = new List<BingoTypesEnum>() { BingoTypesEnum.R0, BingoTypesEnum.R1, BingoTypesEnum.R2, BingoTypesEnum.R3, BingoTypesEnum.R4,
                    BingoTypesEnum.C0, BingoTypesEnum.C1, BingoTypesEnum.C2, BingoTypesEnum.C3, BingoTypesEnum.C4, BingoTypesEnum.D1, BingoTypesEnum.D2, BingoTypesEnum.SQ};

                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        if (!cc.SelectedNumbers.Contains(new Point(x, y)))
                        {
                            BingoTypesEnum row = (BingoTypesEnum)Enum.Parse(typeof(BingoTypesEnum), "R" + y);
                            BingoTypesEnum column = (BingoTypesEnum)Enum.Parse(typeof(BingoTypesEnum), "C" + x);

                            bingos.Remove(row);
                            bingos.Remove(column);

                            if (x == y)
                            {
                                bingos.Remove(BingoTypesEnum.D1);
                            }
                            if (x + y == 4)
                            {
                                bingos.Remove(BingoTypesEnum.D2);

                                if (x == 0 || y == 0)
                                {
                                    bingos.Remove(BingoTypesEnum.SQ);
                                }
                            }
                            if (x + y == 8)
                            {
                                bingos.Remove(BingoTypesEnum.SQ);
                            }
                        }
                    }
                }
                totalBingos += bingos.Count;
            }

            return totalBingos;
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

            clickPointsAndRestore(allpoints, 10, false);
        }

        private void buttonPower_Click(object sender, EventArgs e)
        {
            clickPointsAndRestore(new Point[] { this.CurrentConfiguration.PowerButton }, 10, false);
        }

        private void buttonBingo_Click(object sender, EventArgs e)
        {
            IEnumerable<Point> bingos = this.CurrentConfiguration.CardConfigurations.Select(cc => cc.BingoButton);
            clickPointsAndRestore(bingos, 350, false);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.createCardConfigurationButtons();
            foreach (CardConfiguration cc in this.CurrentConfiguration.CardConfigurations)
            {
                cc.ResetForNewMatch();
            }
            this.lastCallMatches = 0;
            this.totalMatches = 0;
            this.toolStripStatusLabelBingos.Text = "0";
        }

        private void readNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int r = 0; r < this.CurrentConfiguration.Rows; r++)
            {
                for (int c = 0; c < this.CurrentConfiguration.Columns; c++)
                {
                    int index = r * this.CurrentConfiguration.Columns + c;
                    if (this.CurrentConfiguration.CardConfigurations[index].Numbers != null)
                    {
                        continue;
                    }

                    if (this.createCardConfiguration(r, c, true))
                    {
                        Button button = this.tableLayoutPanel1.GetControlFromPosition(c, r) as Button;
                        button.Text = "Edit";
                        //this.updateCardConfigurationThumbnail(control.Parent, this.CurrentConfiguration.CardConfigurations[index]);
                    }
                }
            }
        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            if (textBoxInput.Text.Length == 1)
            {
                switch (textBoxInput.Text)
                {
                    case "b":
                        buttonB_Click(null, new BingoSelectedEventArgs());
                        toolStripStatusLabelLastCall.Text = textBoxInput.Text.ToUpper();
                        textBoxInput.Clear();
                        break;
                    case "i":
                        buttonI_Click(null, new BingoSelectedEventArgs());
                        toolStripStatusLabelLastCall.Text = textBoxInput.Text.ToUpper();
                        textBoxInput.Clear();
                        break;
                    case "n":
                        buttonN_Click(null, new BingoSelectedEventArgs());
                        toolStripStatusLabelLastCall.Text = textBoxInput.Text.ToUpper();
                        textBoxInput.Clear();
                        break;
                    case "g":
                        buttonG_Click(null, new BingoSelectedEventArgs());
                        toolStripStatusLabelLastCall.Text = textBoxInput.Text.ToUpper();
                        textBoxInput.Clear();
                        break;
                    case "o":
                        buttonO_Click(null, new BingoSelectedEventArgs());
                        toolStripStatusLabelLastCall.Text = textBoxInput.Text.ToUpper();
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
                        toolStripStatusLabelLastCall.Text = i.ToString();
                        textBoxInput.Clear();
                    }
                    else if (16 <= i && i <= 30)
                    {
                        buttonI_Click(null, new BingoSelectedEventArgs("i", i));
                        toolStripStatusLabelLastCall.Text = i.ToString();
                        textBoxInput.Clear();
                    }
                    else if (31 <= i && i <= 45)
                    {
                        buttonN_Click(null, new BingoSelectedEventArgs("n", i));
                        toolStripStatusLabelLastCall.Text = i.ToString();
                        textBoxInput.Clear();
                    }
                    else if (46 <= i && i <= 60)
                    {
                        buttonG_Click(null, new BingoSelectedEventArgs("g", i));
                        toolStripStatusLabelLastCall.Text = i.ToString();
                        textBoxInput.Clear();
                    }
                    else if (61 <= i && i <= 75)
                    {
                        buttonO_Click(null, new BingoSelectedEventArgs("o", i));
                        toolStripStatusLabelLastCall.Text = i.ToString();
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
    }

    internal enum BingoTypesEnum
    {
        R0 = 1,
        R1 = 2,
        R2 = 4,
        R3 = 8,
        R4 = 16,
        C0 = 32,
        C1 = 64,
        C2 = 128,
        C3 = 256,
        C4 = 512,
        D1 = 1024,
        D2 = 2048,
        SQ = 4096
    }
}
