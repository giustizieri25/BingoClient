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
    public partial class ConfigPicker : Form
    {
        internal BingoConfiguration SelectedCofiguration { get; set; }

        public ConfigPicker()
        {
            InitializeComponent();

            XmlDocument doc = new XmlDocument();
            doc.Load(@".\bingo-configuration.xml");
            XmlNodeList xmlNodeList = doc.SelectNodes(@"/Configurations/Configuration");

            foreach (XmlNode item in xmlNodeList)
            {
                this.listBoxConfigurations.Items.Add(new ConfigPickerItem(item));
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (this.listBoxConfigurations.SelectedItem != null)
            {
                this.DialogResult = DialogResult.OK;
                this.SelectedCofiguration = ((ConfigPickerItem)this.listBoxConfigurations.SelectedItem).Configuration;
            }
        }
    }

    class ConfigPickerItem
    {
        public string Name { get; set; }
        public BingoConfiguration Configuration { get; private set; }

        public ConfigPickerItem(XmlNode xmlNode)
        {
            this.Name = string.Format("Resolution: {0}\tCards: {1}", xmlNode.Attributes["Resolution"].Value, xmlNode.Attributes["Cards"].Value);
            this.Configuration = CreateConfiguration(this.Name, xmlNode);
        }

        private BingoConfiguration CreateConfiguration(string name, XmlNode xmlNode)
        {
            BingoConfiguration bingoConfiguration = new BingoConfiguration(name);

            XmlNodeList cardsRows = xmlNode.SelectNodes(@"Cards/RowsPoints");
            XmlNodeList cardsColumns = xmlNode.SelectNodes(@"Cards/ColumnsPoints");

            int createdCards = 0;
            int configurationCards = Convert.ToInt32(xmlNode.Attributes["Cards"].Value);
            for (int r = 0; r < cardsRows.Count; r++)
            {
                for (int c = 0; c < cardsColumns.Count && createdCards++ < configurationCards; c++)
                {
                    CardConfiguration cardConfiguration = new CardConfiguration();

                    int[] columnsValues = cardsColumns[c].InnerText.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToArray();
                    int[] rowsValues = cardsRows[r].InnerText.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToArray();

                    if (columnsValues.Length != 5 || rowsValues.Length != 6)
                    {
                        throw new Exception("Invalid Configuration: " + this.Name);
                    }

                    for (int n = 0; n < 5; n++)
                    {
                        cardConfiguration.BPoints.Add(new Point(columnsValues[0], rowsValues[n]));
                        cardConfiguration.IPoints.Add(new Point(columnsValues[1], rowsValues[n]));
                        cardConfiguration.NPoints.Add(new Point(columnsValues[2], rowsValues[n]));
                        cardConfiguration.GPoints.Add(new Point(columnsValues[3], rowsValues[n]));
                        cardConfiguration.OPoints.Add(new Point(columnsValues[4], rowsValues[n]));
                    }

                    cardConfiguration.BingoButton = new Point(columnsValues[2], rowsValues[5]);

                    bingoConfiguration.CardConfigurations.Add(cardConfiguration);
                }
            }

            int[] powerPointValues = xmlNode["PowerPoint"].InnerText.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToArray();
            if (powerPointValues.Length != 2)
            {
                throw new Exception("Invalid Configuration: " + this.Name);
            }

            bingoConfiguration.Columns = cardsColumns.Count;
            bingoConfiguration.Rows = cardsRows.Count;
            bingoConfiguration.PowerButton = new Point(powerPointValues[0], powerPointValues[1]);

            int[] numbersList = xmlNode["NumbersList"].InnerText.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToArray();
            if (numbersList.Length != 2)
            {
                throw new Exception("Invalid Configuration: " + this.Name);
            }

            bingoConfiguration.NumbersList = new Point(numbersList[0], numbersList[1]);

            return bingoConfiguration;
        }
    }
}
