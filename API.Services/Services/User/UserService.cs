using API.Common.Helpers;
using API.Models.Config;
using API.Models.Models;
using Dapper;
using Microsoft.Extensions.Options;
using System.Data;

namespace API.Services.Services.User
{
    public class UserService : BaseRepository, IUserService
    {
        public UserService(IOptions<DataConfig> dataConfig) : base(dataConfig)
        {
        }

        public async Task<LoginUserModel> Login(LoginModel loginModel)
        {
            var param = new DynamicParameters();
            param.Add("@EmailID", loginModel.EmailID);
            param.Add("@Password", loginModel.Password);
            var data = await QueryFirstOrDefaultAsync<LoginUserModel>(StoredProcedure.UserLogin, param, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<long> UpdateLoginToken(string Token, long UserId)
        {
            var param = new DynamicParameters();
            param.Add("@Token", Token);
            param.Add("@UserId", UserId);
            return await QueryFirstOrDefaultAsync<long>(StoredProcedure.UpdateLoginToken, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<LoginUserModel>> GetUserList()
        {
            var data = await QueryAsync<LoginUserModel>(StoredProcedure.GetUserList, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<ResponseModel> SavePeer(string peerId, long userId)
        {
            var param = new DynamicParameters();
            param.Add("@PeerId", peerId);
            param.Add("@UserId", userId);
            ResponseModel result = await QueryFirstOrDefaultAsync<ResponseModel>(StoredProcedure.SavePeer, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<LoginUserModel> GetUserByID(long id)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", id);
            LoginUserModel result = await QueryFirstOrDefaultAsync<LoginUserModel>(StoredProcedure.GetUserById, param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
