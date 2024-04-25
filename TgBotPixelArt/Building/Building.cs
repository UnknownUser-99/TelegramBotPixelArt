using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgBotPixelArt.Building
{
    public class Building<T> where T : Block
    {
        private List<T> blocks;

        private int sizeX;
        private int sizeY;
        private int sizeZ;
        private int totalBlocks;

        public Building()
        {
            blocks = new List<T>();
        }

        public void AddBlock(T block)
        {
            blocks.Add(block);
            totalBlocks++;
        }

        public void AddSize(int x, int y, int z)
        {
            sizeX = x;
            sizeY = y;
            sizeZ = z;
        }

        public List<T> GetBlocks()
        {
            return blocks;
        }

        public (int x, int y, int z) GetSize()
        {
            return (sizeX, sizeY, sizeZ);
        }

        public int GetTotalBlocks()
        {
            return totalBlocks;
        }
    }
}
