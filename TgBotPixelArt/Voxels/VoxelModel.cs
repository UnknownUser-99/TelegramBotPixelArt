using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgBotPixelArt.Voxels
{
    public class VoxelModel
    {
        private List<Voxel> voxels;

        private int sizeX;
        private int sizeY;
        private int sizeZ;
        private int totalVoxels;

        public VoxelModel()
        {
            voxels = new List<Voxel>();
        }

        public void AddVoxel(Voxel voxel)
        {
            voxels.Add(voxel);
            totalVoxels++;
        }

        public void AddSize(int x, int y, int z)
        {
            sizeX = x;
            sizeY = y;
            sizeZ = z;
        }

        public List<Voxel> GetVoxels()
        {
            return voxels;
        }

        public (int x, int y, int z) GetSize()
        {
            return (sizeX, sizeY, sizeZ);
        }

        public int GetTotalVoxels()
        {
            return totalVoxels;
        }
    }
}
