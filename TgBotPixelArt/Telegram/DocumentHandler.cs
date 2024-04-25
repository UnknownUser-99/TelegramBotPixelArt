using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using TgBotPixelArt.Building;
using TgBotPixelArt.ConvertToBuilding;
using TgBotPixelArt.OpenFile;
using TgBotPixelArt.SaveFile;
using TgBotPixelArt.Voxels;

namespace TgBotPixelArt.Telegram
{
    public class DocumentHandler : IMessageHandler
    {
        private readonly ITelegramBotClient botClient;

        public DocumentHandler(ITelegramBotClient botClient)
        {
            this.botClient = botClient;
        }

        public async Task<bool> HandleMessage(MessageEventArgs e)
        {
            var document = e.Message.Document;
            var fileId = document.FileId;

            var fileInfo = await botClient.GetFileAsync(fileId);
            var fileStream = new MemoryStream();
            await botClient.DownloadFileAsync(fileInfo.FilePath, fileStream);

            var format = Path.GetExtension(fileInfo.FilePath);

            bool result = false;
            VoxelModel voxelModel = new VoxelModel();
            Building<DiggerBlock> building = new Building<DiggerBlock>();
            string fileName = "Building.json";
            dynamic data;
            byte[] byteArray;
            Message sentMessage;

            OpenFileFactory openFactory = null;

            switch (format)
            {
                case ".json":
                    openFactory = new OpenJsonFactory();
                    break;
                default:
                    return false;
            }

            IOpenFile openFile = openFactory.CreateOpenFile();

            (voxelModel, result) = openFile.OpenFile(fileStream);

            if (result == true)
            {
                IConvertToBuilding<DiggerBlock> converter = ConvertToBuildingFactory.CreateConverter<DiggerBlock>(ConvertToBuildingFactory.GameType.Digger);
                (building, result) = converter.ConvertToBuilding(voxelModel);
            }
            else
            {
                return false;
            }

            if (result == true)
            {
                ISaveFile<DiggerBlock> jsonSave = SaveFileFactory.CreateSaveFile<DiggerBlock>(SaveFileFactory.FileType.json);
                (data, result) = jsonSave.SaveFile(building);
                byteArray = Encoding.UTF8.GetBytes(data);
            }
            else
            {
                return false;
            }

            if (result == true)
            {
                using (MemoryStream memoryStream = new MemoryStream(byteArray))
                {
                    sentMessage = await botClient.SendDocumentAsync(
                        chatId: e.Message.From.Id,
                        document: new InputOnlineFile(memoryStream, fileName),
                        caption: $"Размер постройки: {building.GetSize().x} X {building.GetSize().y} X {building.GetSize().z}\nВсего блоков: {building.GetTotalBlocks()}");
                }
            }
            else
            {
                return false;
            }

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
