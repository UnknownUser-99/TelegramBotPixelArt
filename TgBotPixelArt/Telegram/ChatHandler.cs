using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using TgBotPixelArt.Telegram;
using TgBotPixelArt.Database;

namespace TgBotPixelArt
{
    public class ChatHandler
    {
        private readonly ITelegramBotClient botClient;

        private DataHandler dataHandler;

        public ChatHandler()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            string botToken = config["BotToken"];
            botClient = new TelegramBotClient(botToken);

            botClient.OnMessage += Bot_OnMessage;

            botClient.StartReceiving();

            dataHandler = new DataHandler();
            Console.WriteLine("Работает");
        }

        private async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                if (e.Message != null)
                {
                    bool result;
                    string format;
                    MessageHandlerContext context = new MessageHandlerContext(null);

                    switch (e.Message.Type)
                    {
                        case MessageType.Photo:

                            format = "jpg";

                            context.SetStrategy(new PhotoHandler(botClient));

                            break;

                        case MessageType.Document:

                            format = Path.GetExtension(e.Message.Document.FileName).TrimStart('.');

                            if (format != "json")
                            {
                                context.SetStrategy(new OtherHandler(botClient));
                            }
                            else
                            {
                                context.SetStrategy(new DocumentHandler(botClient));
                            }

                            break;

                        default:

                            format = null;

                            context.SetStrategy(new OtherHandler(botClient));

                            break;
                    }

                    result = await context.HandleMessage(e);

                    if (result == true)
                    {
                        Type strategyType = context.GetStrategy();

                        if (strategyType != typeof(OtherHandler))
                        {
                            result = await dataHandler.AddData(format, e);

                            if (result == true)
                            {
                                Console.WriteLine($"Запрос от пользователя {e.Message.From.Id} успешно обработан и занесён в базу данных");
                            }
                            else
                            {
                                Console.WriteLine($"Запрос от пользователя {e.Message.From.Id} успешно обработан, но не занесён в базу данных");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Cообщение от пользователя {e.Message.From.Id} имеет неправильный тип");
                        }
                    }
                    else
                    {
                        context.SetStrategy(new ErrorHandler(botClient));
                        await context.HandleMessage(e);

                        Console.WriteLine($"Cообщение от пользователя {e.Message.From.Id} не обработано");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка обработки сообщения: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception: " + ex.InnerException.Message);
                }
            }
        }
    }
}
