using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TgBotPixelArt.Building;
using TgBotPixelArt.Voxels;
using TgBotPixelArt.ColorConvert;

namespace TgBotPixelArt.ConvertToBuilding
{
    public class ConvertToDigger : IConvertToBuilding<DiggerBlock>
    {
        public (Building<DiggerBlock> building, bool result) ConvertToBuilding(VoxelModel voxelModel)
        {
            Building<DiggerBlock> building = new Building<DiggerBlock>();
            bool result;

            try
            {
                DiggerBlocks blocks = new DiggerBlocks();
                ClosestColorFinder colorFinder = new ClosestColorFinder();

                foreach (var voxel in voxelModel.GetVoxels())
                {
                    Color voxelColor = voxel.color;

                    int closestBlock = colorFinder.GetClosestColor(voxelColor, blocks.Blocks);

                    building.AddBlock(new DiggerBlock
                    {
                        x = voxel.x,
                        y = voxel.y,
                        z = voxel.z,
                        blockType = closestBlock,
                        blockKind = 0
                    });
                }

                building.AddSize(voxelModel.GetSize().x, voxelModel.GetSize().y, voxelModel.GetSize().z);

                Console.WriteLine("Модель преобразована в постройку");

                return (building, result = true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при преобразовании в постройку: {ex.Message}");

                return (building, result = false);
            }
        }
    }
}
