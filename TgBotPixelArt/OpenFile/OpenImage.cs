using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using TgBotPixelArt.ColorConvert;
using TgBotPixelArt.Voxels;

namespace TgBotPixelArt.OpenFile
{
    public class OpenImage : IOpenFile
    {
        public (VoxelModel voxelModel, bool result) OpenFile(Stream fileStream, int size = 256)
        {
            VoxelModel voxelModel = new VoxelModel();
            bool result;

            try
            {
                using (var bitmap = new Bitmap(fileStream))
                {
                    BitmapResizer resizer = new BitmapResizer();
                    Bitmap newBitmap = resizer.Resize(bitmap, size);

                    int width = newBitmap.Width;
                    int height = newBitmap.Height;

                    using (Graphics g = Graphics.FromImage(newBitmap))
                    {
                        newBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

                        g.DrawImage(newBitmap, 0, 0);
                    }

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            Color pixelColor = newBitmap.GetPixel(x, y);

                            voxelModel.AddVoxel(new Voxel { x = x, y = y, z = 0, color = pixelColor });
                        }
                    }

                    voxelModel.AddSize(width, height, 1);
                }

                Console.WriteLine("Данные из файла с изображением получены");

                return (voxelModel, result = true);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при получении данных: {ex.Message}");

                return (voxelModel, result = false);
            }
        }
    }
}
