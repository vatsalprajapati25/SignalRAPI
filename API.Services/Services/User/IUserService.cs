using API.Common.Helpers;
using API.Models.Models;

namespace API.Services.Services.User
{
    public interface IUserService
    {
        Task<LoginUserModel> Login(LoginModel loginModel);
        Task<long> UpdateLoginToken(string Token, long UserId);
        Task<List<LoginUserModel>> GetUserList();
        Task<ResponseModel> SavePeer(string peerId, long userId);

        Task<LoginUserModel> GetUserByID(long id);
    }
}
