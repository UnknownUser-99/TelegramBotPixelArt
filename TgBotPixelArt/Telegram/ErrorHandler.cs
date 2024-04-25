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
    public class ErrorHandler : IMessageHandler
    {
        private readonly ITelegramBotClient botClient;

        public ErrorHandler(ITelegramBotClient botClient)
        {
            this.botClient = botClient;
        }

        public async Task<bool> HandleMessage(MessageEventArgs e)
        {
            Message sentMessage;

            sentMessage = await botClient.SendTextMessageAsync(
                e.Message.Chat.Id,
                "Возникла ошибка при обработке сообщения\n\nОбратитесь к разработчику бота для её устранения");

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
