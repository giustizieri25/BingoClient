using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoClient
{
    static class BitmapUtilities
    {
        public static Bitmap CopyRectangle(Bitmap source, Rectangle r)
        {
            Bitmap dest = new Bitmap(r.Width, r.Height);
            Graphics destGraphics = Graphics.FromImage(dest);
            destGraphics.DrawImageUnscaled(source, r);
            return dest;
        }

        public static bool CompareBitmaps(Bitmap a, Bitmap b)
        {
            if (a == null || b == null)
            {
                return false;
            }

            if (a.Size.Width != b.Size.Width || a.Size.Height != b.Size.Height)
            {
                return false;
            }

            for (int x = 0; x < a.Size.Width; x++)
            {
                for (int y = 0; y < a.Size.Height; y++)
                {
                    if (a.GetPixel(x, y) != b.GetPixel(x, y))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static int DetectNumber(IEnumerable<Bitmap> source, Bitmap item)
        {
            for (int i = 0; i <= 75; i++)
            {
                if (BitmapUtilities.CompareBitmaps(source.ElementAt(i), item))
                {
                    return i;
                }
            }
            return -1;
        }

        public static Bitmap DryBitmap(Bitmap pointBitmap, IEnumerable<Color> colors)
        {
            int minX = Int32.MaxValue, minY = Int32.MaxValue, maxX = 0, maxY = 0;
            for (int x = 0; x < pointBitmap.Width; x++)
            {
                for (int y = 0; y < pointBitmap.Height; y++)
                {
                    Color pixel = pointBitmap.GetPixel(x, y);
                    if ( colors.Contains(pixel))
                    {
                        pointBitmap.SetPixel(x, y, Color.Black);
                        minX = x < minX ? x : minX;
                        maxX = x > maxX ? x : maxX;
                        minY = y < minY ? y : minY;
                        maxY = y > maxY ? y : maxY;
                    }
                    else
                    {
                        pointBitmap.SetPixel(x, y, Color.White);
                    }
                }
            }
            if (minX < Int32.MaxValue && minY < Int32.MaxValue && maxX > 0 && maxY > 0)
            {
                Bitmap reducedPointBitmap = BitmapUtilities.CopyRectangle(pointBitmap, new Rectangle(-minX, -minY, maxX - minX + 1, maxY - minY + 1));
                return reducedPointBitmap;
            }
            return null;
        }
    }
}
