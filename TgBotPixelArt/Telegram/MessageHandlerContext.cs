using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace TgBotPixelArt.Telegram
{
    public class MessageHandlerContext
    {
        private IMessageHandler strategy;

        public MessageHandlerContext(IMessageHandler strategy)
        {
            this.strategy = strategy;
        }

        public void SetStrategy(IMessageHandler strategy)
        {
            this.strategy = strategy;
        }

        public Type GetStrategy()
        {
            return strategy.GetType();
        }

        public async Task<bool> HandleMessage(MessageEventArgs e)
        {
            return await strategy.HandleMessage(e);
        }
    }
}
