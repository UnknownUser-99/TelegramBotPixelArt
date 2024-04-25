using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TgBotPixelArt.ColorConvert
{
    public class BitmapResizer
    {
        public Bitmap Resize(Bitmap bitmap, int newSize)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            ProportionalSizeCalculator calculator = new ProportionalSizeCalculator();
            (width, height) = calculator.ResizeProportionally(width, height, newSize);

            Bitmap newBitmap = new Bitmap(width, height);

            using (Graphics graphics = Graphics.FromImage(newBitmap))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;

                graphics.DrawImage(bitmap, 0, 0, width, height);
            }

            return newBitmap;
        }
    }
}
