using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace TgBotPixelArt.Telegram
{
    public interface IMessageHandler
    {
        public Task<bool> HandleMessage(MessageEventArgs e);
    }
}
