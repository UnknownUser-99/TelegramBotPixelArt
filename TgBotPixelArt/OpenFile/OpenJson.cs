using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json.Linq;
using TgBotPixelArt.Voxels;

namespace TgBotPixelArt.OpenFile
{
    public class OpenJson : IOpenFile
    {
        public (VoxelModel voxelModel, bool result) OpenFile(Stream fileStream, int size = 256)
        {
            VoxelModel voxelModel = new VoxelModel();
            bool result;

            try
            {
                fileStream.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(fileStream))
                {
                    string jsonContent = reader.ReadToEnd();
                    JObject jsonObj = JObject.Parse(jsonContent);

                    JArray dimensionArray = (JArray)jsonObj["dimension"];
                    JObject dimensionObj = (JObject)dimensionArray[0];

                    int sizeX = (int)dimensionObj["width"];
                    int sizeY = (int)dimensionObj["height"];
                    int sizeZ = (int)dimensionObj["depth"];

                    voxelModel.AddSize(sizeX, sizeY, sizeZ);

                    JArray voxelsArray = (JArray)jsonObj["voxels"];

                    foreach (JObject voxelObj in voxelsArray)
                    {
                        int x = (int)voxelObj["x"];
                        int y = (int)voxelObj["y"];
                        int z = (int)voxelObj["z"];

                        int red = (int)voxelObj["red"];
                        int green = (int)voxelObj["green"];
                        int blue = (int)voxelObj["blue"];

                        Color color = Color.FromArgb(red, green, blue);

                        voxelModel.AddVoxel(new Voxel { x = x, y = y, z = z, color = color });
                    }
                }

                Console.WriteLine("Данные из файла JSON получены");

                return (voxelModel, result = true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при получении данных: {ex.Message}");

                return (voxelModel, result = false);
            }
        }
    }
}
