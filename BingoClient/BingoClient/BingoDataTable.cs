using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoClient
{
    public class BingoDataTable : DataTable
    {
        public BingoDataTable()
        {
            this.Columns.Add("B", typeof(int));
            this.Columns.Add("I", typeof(int));
            this.Columns.Add("N", typeof(int));
            this.Columns.Add("G", typeof(int));
            this.Columns.Add("O", typeof(int));

            for (int i = 0; i < 5; i++)
            {
                this.Rows.Add();
            }
        }
    }
}
