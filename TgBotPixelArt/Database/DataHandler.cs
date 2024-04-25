using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Args;

namespace TgBotPixelArt.Database
{
    public class DataHandler
    {
        private readonly BotDbContext dbContext;

        private HashSet<long> Users;
        private List<File> Files;

        private readonly object usersLock = new object();

        public DataHandler()
        {
            dbContext = new BotDbContext();

            GetAllData().Wait();
        }
        
        public async Task<bool> AddData(string format, MessageEventArgs e)
        {
            bool result;
            int fileID = 0;

            foreach (var file in Files)
            {
                if (file.FileFormat.Equals(format, StringComparison.OrdinalIgnoreCase))
                {
                    fileID = file.FileID;
                    break;
                }
            }

            using (var transaction = dbContext.Database.BeginTransaction())
            {
                result = await AddUser(e);

                if (result == true)
                {
                    result = await AddOperation(fileID, e.Message.From.Id);

                    if (result == true)
                    {
                        transaction.Commit();

                        return true;
                    }
                    else
                    {
                        transaction.Rollback();

                        return false;
                    }
                }
                else
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        private async Task GetAllData()
        {
            Users = await GetAllUsers();
            Files = await GetAllFiles();
        }

        private async Task<HashSet<long>> GetAllUsers()
        {
            var userIDs = await dbContext.Users.Select(u => u.UserID).ToListAsync();
            return new HashSet<long>(userIDs);
        }

        private async Task<List<File>> GetAllFiles()
        {
            return await dbContext.Files.ToListAsync();
        }

        private async Task<bool> AddUser(MessageEventArgs e)
        {
            if (Users.Contains(e.Message.From.Id))
            {
                return true;
            }
            else
            {
                var newUser = new User
                {
                    UserID = e.Message.From.Id,
                    UserName = e.Message.From.Username,
                    UserFirstName = e.Message.From.FirstName,
                    UserLanguage = e.Message.From.LanguageCode
                };

                dbContext.Users.Add(newUser);
                int result = dbContext.SaveChanges();

                if (result == 1)
                {
                    Users.Add(newUser.UserID);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            /*
            lock (usersLock)
            {
                if (Users.Contains(e.Message.From.Id))
                {
                    return true;
                }
                else
                {
                    var newUser = new User
                    {
                        UserID = e.Message.From.Id,
                        UserName = e.Message.From.Username,
                        UserFirstName = e.Message.From.FirstName,
                        UserLanguage = e.Message.From.LanguageCode
                    };

                    dbContext.Users.Add(newUser);
                    int result = dbContext.SaveChanges();

                    if (result == 1)
                    {
                        Users.Add(newUser.UserID);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            */
        }

        private async Task<bool> AddOperation(int fileID, long userID)
        {
            var newOperation = new Operation
            {
                UserID = userID,
                FileID = fileID,
                OperationDate = DateTime.Now
            };

            dbContext.Operations.Add(newOperation);
            int result = await dbContext.SaveChangesAsync();

            if (result > 0)
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
