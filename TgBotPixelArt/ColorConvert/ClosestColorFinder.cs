using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TgBotPixelArt.ColorConvert
{
    public class ClosestColorFinder
    {
        public int GetClosestColor(Color color, Dictionary<int, Color> blockColors)
        {
            ColorDistanceCalculator calculator = new ColorDistanceCalculator();

            int closestType = -1;
            double closestDistance = double.MaxValue;

            foreach (var kvp in blockColors)
            {
                double distance = calculator.CalculateDistance(color, kvp.Value);

                if (distance < closestDistance)
                {
                    closestType = kvp.Key;
                    closestDistance = distance;
                }
            }

            return closestType;
        }
    }
}
