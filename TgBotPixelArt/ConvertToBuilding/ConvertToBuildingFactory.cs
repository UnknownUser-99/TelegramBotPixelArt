using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgBotPixelArt.Building;

namespace TgBotPixelArt.ConvertToBuilding
{
    public class ConvertToBuildingFactory
    {
        public enum GameType
        {
            Digger
        }

        public static IConvertToBuilding<T> CreateConverter<T>(GameType gameType) where T : Block
        {
            switch (gameType)
            {
                case GameType.Digger:
                    return new ConvertToDigger() as IConvertToBuilding<T>;
                default:
                    throw new ArgumentException("Неподдерживаемый тип игры", nameof(gameType));
            }
        }
    }
}
