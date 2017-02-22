using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace BingoClient
{
    public partial class BingoClient : Form
    {
        private BingoConfiguration CurrentConfiguration;

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
            }
        }
    }
}
