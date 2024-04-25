using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgBotPixelArt.Building;
using TgBotPixelArt.Voxels;

namespace TgBotPixelArt.ConvertToBuilding
{
    public interface IConvertToBuilding<T> where T : Block
    {
        public (Building<T> building, bool result) ConvertToBuilding(VoxelModel voxelModel);
    }
}
