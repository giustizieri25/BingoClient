using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoClient
{
    class BingoConfiguration
    {
        public List<CardConfiguration> CardConfigurations { get; set; }
        public Point PowerButton { get; set; }
    }
}
