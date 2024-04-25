using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TgBotPixelArt.Building;

namespace TgBotPixelArt.SaveFile
{
    public class SaveJson<T> : ISaveFile<T> where T : Block
    {
        public (dynamic file, bool result) SaveFile(Building<T> building)
        {
            string json = null;
            bool result;

            try
            {
                json = JsonConvert.SerializeObject(building.GetBlocks(), Formatting.Indented);

                Console.WriteLine("Файл JSON сохранён");

                return (json, result = true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при сохранении файла JSON: {ex.Message}");

                return (json, result = false);
            }
        }
    }
}
