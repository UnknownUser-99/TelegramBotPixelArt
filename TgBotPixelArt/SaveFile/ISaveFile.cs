using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgBotPixelArt.Building;

namespace TgBotPixelArt.SaveFile
{
    public interface ISaveFile<T> where T : Block
    {
        public (dynamic file, bool result) SaveFile(Building<T> building);
    }
}
