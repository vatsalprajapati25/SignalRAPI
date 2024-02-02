namespace API.Common.Helpers
{
    public class StoredProcedure
    {
        public const string UserLogin = "sp_VerifyLogin";
        public const string UpdateLoginToken = "SP_UpdateLoginToken";
        public const string GetUserList = "sp_GetUserList";


        #region WebRTC
        public const string SavePeer = "sp_SavePeer";
        public const string GetUserById = "sp_GetUserById";
        #endregion

        #region SignalR
        public const string SaveSignalRConnection = "sp_SaveSignalRConnection";
        public const string UpdateSignalRConnection = "sp_UpdateConnection";
        public const string GetUserByConnectionId = "sp_GetUserByConnectionId";
        public const string GetConnectionIdByUser = "sp_GetConnectionIdByUser";
        public const string GetOnlineUsers = "sp_GetOnlineUserList";
        public const string GetGroupChats = "sp_GetGroupChat";
        public const string SaveGroupChat = "sp_SaveGroupChat";
        #endregion
    }
}
