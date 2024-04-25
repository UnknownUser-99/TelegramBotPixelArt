using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
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
using System.Drawing.Imaging;

namespace TgBotPixelArt.Telegram
{
    public class PhotoHandler : IMessageHandler
    {
        private readonly ITelegramBotClient botClient;

        private bool isSendingResponse = false;
        private readonly object lockObject = new object();

        public PhotoHandler(ITelegramBotClient botClient)
        {
            this.botClient = botClient;
        }
        public async Task<bool> HandleMessage(MessageEventArgs e)
        {
            var photo = e.Message.Photo[^1];
            var fileId = photo.FileId;

            var fileInfo = await botClient.GetFileAsync(fileId);
            var fileStream = new MemoryStream();
            await botClient.DownloadFileAsync(fileInfo.FilePath, fileStream);

            SizeHandler sizeHandler = new SizeHandler();
            int size = sizeHandler.SizeInput(e.Message.Caption);

            bool result = false;
            VoxelModel voxelModel = new VoxelModel();
            Building<DiggerBlock> building = new Building<DiggerBlock>();
            string fileName = "Art.json";
            dynamic data;
            dynamic image;
            Bitmap bitmap;
            byte[] byteArray;
            Message sentImage;
            Message sentDocument;

            OpenFileFactory openFactory = null;
            openFactory = new OpenImageFactory();
            IOpenFile openFile = openFactory.CreateOpenFile();

            if (size != 0)
            {
                (voxelModel, result) = openFile.OpenFile(fileStream, size);
            }
            else
            {
                (voxelModel, result) = openFile.OpenFile(fileStream);
            }

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
                ISaveFile<DiggerBlock> bitmapSave = SaveFileFactory.CreateSaveFile<DiggerBlock>(SaveFileFactory.FileType.bitmap);
                (image, result) = bitmapSave.SaveFile(building);
                bitmap = new Bitmap(image);
            }
            else
            {
                return false;
            }

            if (result == true)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    bitmap.Save(memoryStream, ImageFormat.Jpeg);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    sentImage = await botClient.SendPhotoAsync(
                        chatId: e.Message.From.Id,
                        photo: new InputOnlineFile(memoryStream, "image.png"));
                }
            }
            else
            {
                return false;
            }

            if (sentImage != null)
            {
                using (MemoryStream memoryStream = new MemoryStream(byteArray))
                {
                    sentDocument = await botClient.SendDocumentAsync(
                        chatId: e.Message.From.Id,
                        document: new InputOnlineFile(memoryStream, fileName),
                        caption: $"Размер арта: {building.GetSize().x} X {building.GetSize().y}\nВсего блоков: {building.GetTotalBlocks()}");
                }
            }
            else
            {
                return false;
            }

            if (sentDocument != null)
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
