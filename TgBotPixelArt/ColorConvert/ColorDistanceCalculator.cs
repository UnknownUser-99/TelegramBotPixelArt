using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TgBotPixelArt.ColorConvert
{
    public class ColorDistanceCalculator
    {
        public double CalculateDistance(Color color1, Color color2)
        {
            double deltaR = color1.R - color2.R;
            double deltaG = color1.G - color2.G;
            double deltaB = color1.B - color2.B;

            double distance = Math.Sqrt(deltaR * deltaR + deltaG * deltaG + deltaB * deltaB);

            return distance;
        }
    }
}
