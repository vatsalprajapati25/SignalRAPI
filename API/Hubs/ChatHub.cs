using API.Models.Models;
using API.Services;
using API.Services.Services.Chat;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;
        private readonly IChatService _chatRepoService;
        public ChatHub(ChatService chatService, IChatService chatRepoService)
        {
            _chatService = chatService;
            _chatRepoService = chatRepoService;
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "WebChat");
            await Clients.Caller.SendAsync("UserConnected");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "WebChat");
            var user = _chatService.GetUserByConnectionId(Context.ConnectionId);
            _chatService.RemoveUserFromList(user);
            await DisplyOnlineUsers();
            await base.OnDisconnectedAsync(exception);
        }

        public async Task AddUserConnectionId(string username)
        {
            //_chatService.AddUserConnectionId(username, Context.ConnectionId);
            UserModel user = new UserModel();
            user.EmailID = username;
            user.ChatConnectionID = Context.ConnectionId;
            await _chatRepoService.UpdateConnection(user);
            var onlineUsers = await _chatRepoService.GetOnlineUsers();
            await Clients.Groups("WebChat").SendAsync("OnlineUsers", onlineUsers);
        }

        public async Task DisplyOnlineUsers()
        {
            //var onlineUsers = _chatService.GetOnlineUsers();
            var onlineUsers = await _chatRepoService.GetOnlineUsers();
            await Clients.Groups("WebChat").SendAsync("OnlineUsers", onlineUsers);
        }

        public async Task ReceiveMessage(MessageModel message)
        {
            var oldChats = await _chatRepoService.SaveGroupChat(message);
            await Clients.Group("WebChat").SendAsync("NewMessage", oldChats);
        }

        public async Task GetOldChats()
        {
            var oldChats = await _chatRepoService.GetGroupChats();
            await Clients.Group("WebChat").SendAsync("GetOldChats", oldChats);
        }

        public async Task Chat(Models.MessageModel message)
        {
            string privateGroupName = GetPrivateGroupName(message.From, message.To);
            await Groups.AddToGroupAsync(Context.ConnectionId, privateGroupName);
            var toConnection =  _chatService.GetConnectionIdByUser(message.To);
            await Groups.AddToGroupAsync(toConnection, privateGroupName);
            await Clients.Client(toConnection).SendAsync("OpenPrivateChat", message);
        }

        public async Task ReceivePrivateMessages(Models.MessageModel messages)
        {
            string privateGroupName = GetPrivateGroupName(messages.From, messages.To);
            await Clients.Group(privateGroupName).SendAsync("NewPrivateMessage", messages);
        }

        public async Task RemovePrivateChat(string from, string to)
        {
            string privateGroupName = GetPrivateGroupName(from, to);
            await Clients.Group(privateGroupName).SendAsync("ClosePrivateChat");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, privateGroupName);
            var toConnId = _chatService.GetConnectionIdByUser(Context.ConnectionId);
            await Groups.RemoveFromGroupAsync(toConnId, privateGroupName);
        }


        public static string GetPrivateGroupName(string from, string to)
        {
            var stringCompare = string.CompareOrdinal(from, to) < 0;
            return stringCompare ? $"{from}-{to}" : $"{to}-{from}";
        }
    }
}
