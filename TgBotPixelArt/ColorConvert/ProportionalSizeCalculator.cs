using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgBotPixelArt.ColorConvert
{
    public class ProportionalSizeCalculator
    {
        public (int x, int y, int z) ResizeProportionally(int x, int y, int z, int newSize)
        {
            int maxDimension = Math.Max(Math.Max(x, y), z);

            double scaleFactor = (double)newSize / maxDimension;

            int newX = (int)Math.Round(x * scaleFactor);
            int newY = (int)Math.Round(y * scaleFactor);
            int newZ = (int)Math.Round(z * scaleFactor);

            return (newX, newY, newZ);
        }

        public (int x, int y) ResizeProportionally(int x, int y, int newSize)
        {
            int maxDimension = Math.Max(x, y);

            double scaleFactor = (double)newSize / maxDimension;

            int newX = (int)Math.Round(x * scaleFactor);
            int newY = (int)Math.Round(y * scaleFactor);

            return (newX, newY);
        }
    }
}
