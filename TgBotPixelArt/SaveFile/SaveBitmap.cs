using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TgBotPixelArt.Building;

namespace TgBotPixelArt.SaveFile
{
    public class SaveBitmap<T> : ISaveFile<T> where T : Block
    {
        public (dynamic file, bool result) SaveFile(Building<T> building)
        {
            Bitmap bitmap = null;
            bool result;

            try
            {
                (int width, int height, int depth) = building.GetSize();

                bitmap = new Bitmap(width, height);

                List<DiggerBlock> blocks = building.GetBlocks().Cast<DiggerBlock>().ToList();

                DiggerBlocks diggerBlocks = new DiggerBlocks();

                foreach (var block in blocks)
                {
                    Color pixelColor = diggerBlocks.Blocks[block.blockType];

                    bitmap.SetPixel(block.x, block.y, pixelColor);
                }

                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

                    g.DrawImage(bitmap, 0, 0);
                }

                return (bitmap, result = true);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при сохранении файла Bitmap: {ex.Message}");

                return (bitmap, result = false);
            }
        }
    }
}
