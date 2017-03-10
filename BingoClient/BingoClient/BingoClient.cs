using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
        public static Bitmap[] CardsNumbersBitmaps = new Bitmap[76];
        public static Bitmap[] ListNumbersBitmaps = new Bitmap[76];
        private int lastCallMatches;
        private Queue<BingoSelectedEventArgs> Queue = new Queue<BingoSelectedEventArgs>();
        private Dictionary<int, int> selectionHistory = new Dictionary<int, int>();
        private int plus5 = -1;

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
            string cardFileName, listFileName;
            for (int i = 0; i <= 75; i++)
            {
                cardFileName = string.Format(@".\masks\cards\{0}.png", i.ToString().PadLeft(2, '0'));
                listFileName = string.Format(@".\masks\list\{0}.png", i.ToString().PadLeft(2, '0'));
                if (File.Exists(cardFileName))
                {
                    BingoClient.CardsNumbersBitmaps[i] = (Bitmap)Bitmap.FromFile(cardFileName);
                }
                if (File.Exists(listFileName))
                {
                    BingoClient.ListNumbersBitmaps[i] = (Bitmap)Bitmap.FromFile(listFileName);
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
                    b.Text = "+";
                    b.Click += cardConfigurator_Click;
                    b.Dock = DockStyle.Fill;
                    b.TextAlign = ContentAlignment.MiddleCenter;
                    b.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
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
                senderButton.Text = this.getCardConfigurationButtonText(this.CurrentConfiguration.CardConfigurations[cell.Row * this.CurrentConfiguration.Columns + cell.Column]);
                //this.updateCardConfigurationThumbnail(senderControl.Parent, this.CurrentConfiguration.CardConfigurations[cell.Row * this.CurrentConfiguration.Columns + cell.Column]);
            }
        }

        private string getCardConfigurationButtonText(CardConfiguration cardConfiguration)
        {
            return string.Format("{0}:{1}", cardConfiguration.SelectedNumbers.Count - 1, cardConfiguration.Bingos);
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

        private void updateCardConfigurationThumbnail()
        {
            for (int r = 0; r < this.CurrentConfiguration.Rows; r++)
            {
                for (int c = 0; c < this.CurrentConfiguration.Columns; c++)
                {
                    int index = r * this.CurrentConfiguration.Columns + c;
                    tableLayoutPanel1.Controls[index].Text = getCardConfigurationButtonText(this.CurrentConfiguration.CardConfigurations[index]);
                }
            }
        }

        private void pickConfiguration()
        {
            ConfigPicker configPicker = new ConfigPicker();

            if (configPicker.ShowDialog(this) == DialogResult.OK)
            {
                this.CurrentConfiguration = configPicker.SelectedCofiguration;
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

        private IEnumerable<Point> searchNumberOrColumns(BingoColumns column, int number, Func<CardConfiguration, IEnumerable<Point>> allPoints)
        {
            List<Point> pointsToClick = new List<Point>();
            lastCallMatches = 0;
            foreach (CardConfiguration cardConfiguration in this.CurrentConfiguration.CardConfigurations)
            {
                if (cardConfiguration.Numbers != null)
                {
                    DataRow dr = cardConfiguration.Numbers.Select(string.Format("{0}={1}", column, Math.Abs(number))).FirstOrDefault();
                    int index = cardConfiguration.Numbers.Rows.IndexOf(dr);
                    if (index > -1)
                    {
                        lastCallMatches++;
                        if (number > 0)
                        {
                            pointsToClick.Add(cardConfiguration.Column[column][index]);
                            cardConfiguration.SelectedNumbers.Add(new Point((int)column - 1, index));
                        }
                        else
                        {
                            cardConfiguration.SelectedNumbers.Remove(new Point((int)column - 1, index));
                        }
                    }
                }
                else
                {
                    pointsToClick.AddRange(allPoints(cardConfiguration));
                }
            }
            return pointsToClick;
        }

        private void buttonColumn_Clicked(BingoSelectedEventArgs e)
        {
            IEnumerable<Point> points;
            int interval;
            if (e.Number.HasValue)
            {
                points = searchNumberOrColumns(e.Column, e.Number.Value, e.AllPoints);
                updateBingoCount(this.CurrentConfiguration.CardConfigurations);
                updateCardConfigurationThumbnail();

                labelTotalMatches.Text = this.CurrentConfiguration.CardConfigurations.Sum(cc => cc.SelectedNumbers.Count - 1).ToString();
                labelTotalBingos.Text = this.CurrentConfiguration.CardConfigurations.Sum(cc => cc.Bingos).ToString();
                this.updateSelectionHistory(e.Number.Value, lastCallMatches);

                interval = 100;
            }
            else
            {
                points = this.CurrentConfiguration.CardConfigurations.SelectMany(e.AllPoints).ToList();
                interval = 10;
            }
            clickPointsAndRestore(points, interval, true);
        }

        private void updateSelectionHistory(int value, int lastCallMatches)
        {
            if (value > 0)
            {
                this.selectionHistory[value] = lastCallMatches;
                this.dataGridViewHistory.Rows.Insert(0, value, lastCallMatches);
            }
            else
            {
                if (this.selectionHistory.ContainsKey(Math.Abs(value)))
                {
                    this.selectionHistory.Remove(Math.Abs(value));
                    foreach (DataGridViewRow row in this.dataGridViewHistory.Rows)
                    {
                        if (row.Cells[0].Value.Equals(Math.Abs(value)))
                        {
                            this.dataGridViewHistory.Rows.Remove(row);
                            break;
                        }
                    }
                }
            }
        }

        private void updateBingoCount(IEnumerable<CardConfiguration> cardConfigurations)
        {
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

                                if (x == 0 || x == 4)
                                {
                                    bingos.Remove(BingoTypesEnum.SQ);
                                }
                            }
                            if (x + y == 4)
                            {
                                bingos.Remove(BingoTypesEnum.D2);

                                if (x == 0 || y == 0)
                                {
                                    bingos.Remove(BingoTypesEnum.SQ);
                                }
                            }
                        }
                    }
                }
                cc.Bingos = bingos.Count;
            }
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
            this.checkBoxAutoPilot.Checked = false;
            IEnumerable<Point> bingos = this.CurrentConfiguration.CardConfigurations.Select(cc => cc.BingoButton);
            clickPointsAndRestore(bingos, 350, false);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.selectionHistory = new Dictionary<int, int>();
            this.dataGridViewHistory.Rows.Clear();
            this.createCardConfigurationButtons();
            foreach (CardConfiguration cc in this.CurrentConfiguration.CardConfigurations)
            {
                cc.ResetForNewMatch();
            }
            this.plus5 = -1;
            this.updatePlus5Counter(0);
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
                        button.Text = this.getCardConfigurationButtonText(this.CurrentConfiguration.CardConfigurations[index]);
                        //this.updateCardConfigurationThumbnail(control.Parent, this.CurrentConfiguration.CardConfigurations[index]);
                    }
                }
            }
            this.textBoxInput.Focus();
        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxInput.Text))
            {
                return;
            }

            // Name of column or command
            if (textBoxInput.Text.Length == 1)
            {
                BingoSelectedEventArgs args = new BingoSelectedEventArgs();

                // If it exist as a column ... 
                if (!Char.IsDigit(textBoxInput.Text[0]) && Enum.TryParse<BingoColumns>(textBoxInput.Text.ToUpper(), out BingoColumns parsed))
                {
                    var (column, allPoints) = this.GetColumnFromNumber(null, parsed);
                    args.AllPoints = allPoints;
                    this.EnqueueSelection(args);
                    textBoxInput.Clear();
                }
                // Otherwise threat it as a command
                else
                {
                    switch (textBoxInput.Text)
                    {
                        case "p":
                            buttonPower_Click(null, null);
                            textBoxInput.Clear();
                            break;
                        case "a":
                            buttonALL_Click(null, null);
                            textBoxInput.Clear();
                            break;
                        case "-":
                            break;
                        default:
                            if (!Char.IsDigit(textBoxInput.Text[0]))
                            {
                                textBoxInput.Clear();
                            }
                            break;
                    }
                }
            }
            else if (textBoxInput.Text.Length == 2)
            {
                // If it's just 2 digits, like 08, 34, etc ...
                if (Int32.TryParse(textBoxInput.Text, out int i))
                {
                    if (i > 0)
                    {
                        // Show previous match ... 
                        if (this.selectionHistory.ContainsKey(i))
                        {
                            showNumberInHistory(i);
                            this.textBoxInput.Clear();
                        }
                        // Otherwise it's a new match
                        else
                        {
                            if (1 <= i && i <= 75)
                            {
                                var (column, allPoints) = this.GetColumnFromNumber(i, BingoColumns.None);
                                this.EnqueueSelection(new BingoSelectedEventArgs(column, i, allPoints));
                                textBoxInput.Clear();
                            }
                            else
                            {
                                textBoxInput.Clear();
                            }
                        }
                    }
                }
                else
                {
                    textBoxInput.Clear();
                }
            }
            else if (textBoxInput.Text.Length == 3)
            {
                // To remove typoed entries, just follow the regular flow, but with a negative number.
                if (Int32.TryParse(textBoxInput.Text, out int i) && i < 0)
                {
                    this.EnqueueSelection(new BingoSelectedEventArgs(this.GetColumnFromNumber(Math.Abs(i), BingoColumns.None).column, i, null));
                }
                textBoxInput.Clear();
            }
        }

        (BingoColumns column, Func<CardConfiguration, IEnumerable<Point>> allPoints) GetColumnFromNumber(int? i, BingoColumns column)
        {
            if (i.HasValue)
            {
                if (1 <= i.Value && i.Value <= 15)
                {
                    return (BingoColumns.B, cc => cc.BPoints);
                }
                else if (16 <= i.Value && i.Value <= 30)
                {
                    return (BingoColumns.I, cc => cc.IPoints);
                }
                else if (31 <= i.Value && i.Value <= 45)
                {
                    return (BingoColumns.N, cc => cc.NPoints);
                }
                else if (46 <= i.Value && i.Value <= 60)
                {
                    return (BingoColumns.G, cc => cc.GPoints);
                }
                else if (61 <= i.Value && i.Value <= 75)
                {
                    return (BingoColumns.O, cc => cc.OPoints);
                }
                else
                {
                    throw new InvalidOperationException(i + " is not valid number");
                }
            }
            else if (Enum.IsDefined(typeof(BingoColumns), column) && column != BingoColumns.None)
            {
                switch (column)
                {
                    case BingoColumns.B:
                        return (BingoColumns.B, cc => cc.BPoints);
                    case BingoColumns.I:
                        return (BingoColumns.I, cc => cc.IPoints);
                    case BingoColumns.N:
                        return (BingoColumns.N, cc => cc.NPoints);
                    case BingoColumns.G:
                        return (BingoColumns.G, cc => cc.GPoints);
                    case BingoColumns.O:
                        return (BingoColumns.O, cc => cc.OPoints);
                    default:
                        throw new ArgumentException();
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private void showNumberInHistory(int i)
        {
            this.dataGridViewHistory.ClearSelection();
            foreach (DataGridViewRow row in this.dataGridViewHistory.Rows)
            {
                if (row.Cells[0].Value.Equals(i))
                {
                    row.Selected = true;
                    this.dataGridViewHistory.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }

        private void EnqueueSelection(BingoSelectedEventArgs args)
        {
            this.Queue.Enqueue(args);
            processQueueItem();
        }

        private BingoSelectedEventArgs DequeueSelection()
        {
            return this.Queue.Count > 0 ? this.Queue.Dequeue() : null;
        }

        private void timerAutoPilot_Tick(object sender, EventArgs e)
        {
            this.processQueue();

            int distance = 25;
            Point origin = this.CurrentConfiguration.NumbersList;
            Rectangle r = new Rectangle(new Point(origin.X - distance, origin.Y - distance), new Size(distance * 2, Screen.GetBounds(origin).Height - origin.Y + distance));
            Bitmap listBitmap = new Bitmap(distance * 2, Screen.GetBounds(origin).Height - origin.Y + distance, PixelFormat.Format32bppArgb);
            Graphics pointGraphics = Graphics.FromImage(listBitmap);
            pointGraphics.CopyFromScreen(r.X, r.Y, 0, 0, r.Size, CopyPixelOperation.SourceCopy);

            int minX = 0, maxX = 0, minY = 0, maxY = 0;
            bool rowHasPixels = false, previousRowHasPixels = false;
            for (int y = 0; y < listBitmap.Height; y++)
            {
                if (previousRowHasPixels && !rowHasPixels && minX + minY + maxX + maxY > 0)
                {
                    Bitmap reducedPointBitmap = BitmapUtilities.CopyRectangle(listBitmap, new Rectangle(-minX, -minY, maxX - minX + 1, maxY - minY + 1));
                    int i = BitmapUtilities.DetectNumber(BingoClient.ListNumbersBitmaps, reducedPointBitmap);
                    if (i != -1 && !this.selectionHistory.ContainsKey(i))
                    {
                        textBoxInput.Text = i.ToString().PadLeft(2, '0');
                    }
                }
                previousRowHasPixels = rowHasPixels;
                rowHasPixels = false;

                minX = !rowHasPixels && !previousRowHasPixels ? 0 : minX;
                maxX = !rowHasPixels && !previousRowHasPixels ? 0 : maxX;
                minY = !rowHasPixels && !previousRowHasPixels ? 0 : minY;
                maxY = !rowHasPixels && !previousRowHasPixels ? 0 : maxY;

                for (int x = 0; x < listBitmap.Width; x++)
                {
                    if (BingoColors.Numbers.Contains(listBitmap.GetPixel(x, y)))
                    {
                        listBitmap.SetPixel(x, y, Color.Black);

                        minX = minX == 0 || x < minX ? x : minX;
                        maxX = maxX == 0 || x > maxX ? x : maxX;
                        minY = !previousRowHasPixels && (minY == 0 || y < minY) ? y : minY;
                        maxY = maxY == 0 || y > maxY ? y : maxY;

                        rowHasPixels = true;
                    }
                    else
                    {
                        listBitmap.SetPixel(x, y, Color.White);
                    }
                }
            }
        }

        private void processQueue()
        {
            while (this.Queue.Count > 0)
            {
                this.processQueueItem();
            }
        }

        private void processQueueItem()
        {
            BingoSelectedEventArgs args = this.DequeueSelection();

            buttonColumn_Clicked(args);

            if (args.Number.HasValue)
            {
                this.updatePlus5Counter(args.Number.Value);
            }
        }

        private void updatePlus5Counter(int number)
        {
            if (this.plus5 > -1)
            {
                if (number > 0)
                {
                    this.plus5++;
                }
                else if (this.selectionHistory.ContainsKey(Math.Abs(number)))
                {
                    this.plus5--;
                }
                this.buttonPlus5.Text = string.Format("+5 ({0})", this.plus5);
            }
            else
            {
                this.buttonPlus5.Text = "+5";
            }

            if (this.plus5 == 5)
            {
                this.buttonBingo_Click(null, null);
                this.plus5 = -1;
            }
        }

        private void checkBoxAutoPilot_CheckedChanged(object sender, EventArgs e)
        {
            timerAutoPilot.Enabled = checkBoxAutoPilot.Checked;
            this.textBoxInput.Focus();
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            this.newToolStripMenuItem_Click(null, null);
            this.readNumbersToolStripMenuItem_Click(null, null);
        }

        private void buttonPlus5_Click(object sender, EventArgs e)
        {
            this.plus5 = 0;
        }
    }

    internal enum BingoTypesEnum
    {
        R0, R1, R2, R3, R4,
        C0, C1, C2, C3, C4,
        D1, D2, SQ
    }
}
