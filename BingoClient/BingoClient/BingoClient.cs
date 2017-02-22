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


        private void ClickPointsAndRestore(IEnumerable<Point> points, int waitMs)
        {
            Point cursorPoint = Cursor.Position;
            foreach (Point p in points)
            {
                ClickAtPoint(p, waitMs);
            }
            this.Activate();
            Cursor.Position = cursorPoint;
        }

        private void ClickAtPoint(Point p, int waitMs)
        {
            Cursor.Position = new Point(p.X, p.Y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, p.X, p.Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, p.X, p.Y, 0, 0);

            if (waitMs > 0)
            {
                Thread.Sleep(waitMs);
            }
            mouse_event(MOUSEEVENTF_LEFTDOWN, p.X, p.Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, p.X, p.Y, 0, 0);

            if (waitMs > 0)
            {
                Thread.Sleep(waitMs);
            }
        }

        private void buttonB_Click(object sender, EventArgs e)
        {
            IEnumerable<Point> bpoints = this.CurrentConfiguration.CardConfigurations.SelectMany(cc => cc.BPoints).ToList();
            ClickPointsAndRestore(bpoints, 10);
        }

        private void buttonI_Click(object sender, EventArgs e)
        {
            IEnumerable<Point> ipoints = this.CurrentConfiguration.CardConfigurations.SelectMany(cc => cc.IPoints).ToList();
            ClickPointsAndRestore(ipoints, 10);
        }

        private void buttonN_Click(object sender, EventArgs e)
        {
            IEnumerable<Point> npoints = this.CurrentConfiguration.CardConfigurations.SelectMany(cc => cc.NPoints).ToList();
            ClickPointsAndRestore(npoints, 10);
        }

        private void buttonG_Click(object sender, EventArgs e)
        {
            IEnumerable<Point> gpoints = this.CurrentConfiguration.CardConfigurations.SelectMany(cc => cc.GPoints).ToList();
            ClickPointsAndRestore(gpoints, 10);
        }

        private void buttonO_Click(object sender, EventArgs e)
        {
            IEnumerable<Point> opoints = this.CurrentConfiguration.CardConfigurations.SelectMany(cc => cc.OPoints).ToList();
            ClickPointsAndRestore(opoints, 10);
        }

        private void buttonALL_Click(object sender, EventArgs e)
        {
            IEnumerable<Point> allpoints = this.CurrentConfiguration.CardConfigurations.SelectMany(cc => cc.BPoints
                .Concat(cc.IPoints)
                .Concat(cc.NPoints)
                .Concat(cc.GPoints)
                .Concat(cc.OPoints)).ToList();

            if (checkBoxCallBingos.Checked)
            {
                allpoints = allpoints.Concat(this.CurrentConfiguration.CardConfigurations.Select(cc => cc.BingoButton));
            }

            ClickPointsAndRestore(allpoints, 10);
        }

        private void textBoxInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBoxInput.Text.Length == 1)
            {
                switch (textBoxInput.Text)
                {
                    case "b":
                        buttonB_Click(null, null);
                        textBoxInput.Clear();
                        break;
                    case "i":
                        buttonI_Click(null, null);
                        textBoxInput.Clear();
                        break;
                    case "n":
                        buttonN_Click(null, null);
                        textBoxInput.Clear();
                        break;
                    case "g":
                        buttonG_Click(null, null);
                        textBoxInput.Clear();
                        break;
                    case "o":
                        buttonO_Click(null, null);
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
                        buttonB_Click(null, null);
                        textBoxInput.Clear();
                    }
                    else if (16 <= i && i <= 30)
                    {
                        buttonI_Click(null, null);
                        textBoxInput.Clear();
                    }
                    else if (31 <= i && i <= 45)
                    {
                        buttonN_Click(null, null);
                        textBoxInput.Clear();
                    }
                    else if (46 <= i && i <= 60)
                    {
                        buttonG_Click(null, null);
                        textBoxInput.Clear();
                    }
                    else if (61 <= i && i <= 75)
                    {
                        buttonO_Click(null, null);
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
            ClickPointsAndRestore(new Point[] { this.CurrentConfiguration.PowerButton }, 10);
        }

        private void buttonBingo_Click(object sender, EventArgs e)
        {
            IEnumerable<Point> bingos = this.CurrentConfiguration.CardConfigurations.Select(cc => cc.BingoButton);
            ClickPointsAndRestore(bingos, 10);
        }
    }
}
