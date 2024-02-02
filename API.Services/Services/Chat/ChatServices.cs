using API.Common.Helpers;
using API.Models.Config;
using API.Models.Models;
using Dapper;
using Microsoft.Extensions.Options;
using System.Data;

namespace API.Services.Services.Chat
{
    public class ChatServices : BaseRepository, IChatService
    {
        public ChatServices(IOptions<DataConfig> dataConfig) : base(dataConfig)
        {
        }

        public async Task<UserModel> UpdateConnection(UserModel user)
        {
            var param = new DynamicParameters();
            param.Add("@UserEmail", user.EmailID);
            param.Add("@ConnectionId", user.ChatConnectionID);
            var data = await QueryFirstOrDefaultAsync<UserModel>(StoredProcedure.UpdateSignalRConnection, param, commandType: CommandType.StoredProcedure);
            return data;
        }

        public bool AddUserToList(string userToAdd)
        {
            return true;
        }

        public async Task<List<UserModel>> GetOnlineUsers()
        {
            var data = await QueryAsync<UserModel>(StoredProcedure.GetOnlineUsers, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<List<GroupChatModel>> GetGroupChats()
        {
            var data = await QueryAsync<GroupChatModel>(StoredProcedure.GetGroupChats, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<List<GroupChatModel>> SaveGroupChat(MessageModel message)
        {
            var param = new DynamicParameters();
            param.Add("@FromUserId", int.Parse(message.From));
            param.Add("@Message", message.Content);
            var data = await QueryAsync<GroupChatModel>(StoredProcedure.SaveGroupChat, param, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }
    }
}
