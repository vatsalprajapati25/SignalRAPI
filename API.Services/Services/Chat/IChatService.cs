using API.Models.Models;

namespace API.Services.Services.Chat
{
    public interface IChatService
    {
        Task<UserModel> UpdateConnection(UserModel user);
        Task<List<UserModel>> GetOnlineUsers();
        Task<List<GroupChatModel>> GetGroupChats();
        Task<List<GroupChatModel>> SaveGroupChat(MessageModel message);
    }
}
