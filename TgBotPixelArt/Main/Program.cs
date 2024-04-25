using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace TgBotPixelArt
{
    class Program
    {
        static void Main(string[] args)
        {
            ChatHandler chatHandler = new ChatHandler();

            Console.ReadLine();
        }
    }
}
