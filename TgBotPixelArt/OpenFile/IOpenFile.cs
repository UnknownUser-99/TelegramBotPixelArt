using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgBotPixelArt.Voxels;

namespace TgBotPixelArt.OpenFile
{
    public interface IOpenFile
    {
        public (VoxelModel voxelModel, bool result) OpenFile(Stream fileStream, int size = 256);
    }
}
