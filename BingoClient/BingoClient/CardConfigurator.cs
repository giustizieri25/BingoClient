using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            }
            else
            {
                this.BingoDataTable = cardConfiguration.Numbers;
            }
            this.dataGridView1.DataSource = this.BingoDataTable;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            BingoDataTable bdt = (BingoDataTable)this.dataGridView1.DataSource;

            foreach (DataRow dr in bdt.Rows)
            {
                if (!ValidateColumn(dr, "B", 1, 15))
                {
                    return;
                }

                if (!ValidateColumn(dr, "I", 16, 30))
                {
                    return;
                }

                if (!ValidateColumn(dr, "N", 31, 45))
                {
                    return;
                }

                if (!ValidateColumn(dr, "G", 46, 60))
                {
                    return;
                }

                if (!ValidateColumn(dr, "O", 60, 75))
                {
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        private bool ValidateColumn(DataRow dr, string columnName, int minValue, int maxValue)
        {
            return dr[columnName] != DBNull.Value && minValue <= dr.Field<int>(columnName) && dr.Field<int>(columnName) <= maxValue;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.BingoDataTable = null;
            Close();
        }
    }
}
