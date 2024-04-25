using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgBotPixelArt.Building;

namespace TgBotPixelArt.SaveFile
{
    public class SaveFileFactory
    {
        public enum FileType
        {
            json,
            bitmap,
            xml,
            txt
        }

        public static ISaveFile<T> CreateSaveFile<T>(FileType fileType) where T : Block
        {
            switch (fileType)
            {
                case FileType.json:
                    return new SaveJson<T>();
                case FileType.bitmap:
                    return new SaveBitmap<T>();
                default:
                    throw new ArgumentException("Неподдерживаемый формат файла", nameof(fileType));
            }
        }
    }
}
