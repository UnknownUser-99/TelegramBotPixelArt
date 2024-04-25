using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace TgBotPixelArt.Telegram
{
    public class OtherHandler : IMessageHandler
    {
        private readonly ITelegramBotClient botClient;

        public OtherHandler(ITelegramBotClient botClient)
        {
            this.botClient = botClient;
        }

        public async Task<bool> HandleMessage(MessageEventArgs e)
        {
            Message sentMessage;

            sentMessage = await botClient.SendTextMessageAsync(
                e.Message.Chat.Id,
                "Неправильный формат сообщения\n\nОтправьте изображение и, если нужно, укажите необходимый размер в подписи к нему");

            if (sentMessage != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
