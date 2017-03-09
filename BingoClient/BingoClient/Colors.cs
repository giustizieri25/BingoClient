using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoClient
{
    public static class BingoColors
    {
        public static class Cards
        {
            public static Color BColor = Color.FromArgb(69, 121, 40);
            public static Color IColor = Color.FromArgb(175, 138, 0);
            public static Color NColor = Color.FromArgb(147, 59, 13);
            public static Color GColor = Color.FromArgb(105, 50, 105);
            public static Color OColor = Color.FromArgb(15, 100, 121);
            public static Color Grey = Color.FromArgb(67, 57, 38);
        }

        public static class List
        {
            public static Color BColor = Color.FromArgb(66, 166, 47);
            public static Color IColor = Color.FromArgb(255, 182, 0);
            public static Color NColor = Color.FromArgb(236, 95, 21);
            public static Color GColor = Color.FromArgb(191, 47, 189);
            public static Color OColor = Color.FromArgb(22, 160, 222);
        }

        public static Color[] Numbers = new Color[] { BingoColors.List.BColor, BingoColors.List.IColor, BingoColors.List.NColor, BingoColors.List.GColor, BingoColors.List.OColor };
    }
}
