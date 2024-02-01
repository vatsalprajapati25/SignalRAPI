using API.Common.Helpers;
using API.Models;
using API.Models.Config;
using API.Models.Models;
using API.Services.JWTAuthentication;
using API.Services.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly IConfiguration _config;
        private readonly IJWTAuthenticationService _jwtAuthenticationService;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserService userService,
            IConfiguration config,
            IJWTAuthenticationService jwtAuthenticationService,
            IOptions<AppSettings> appSettings,
            IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _config = config;
            _jwtAuthenticationService = jwtAuthenticationService;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ApiPostResponse<LoginUserModel>> Login(LoginModel loginModel)
        {
            ApiPostResponse<LoginUserModel> response = new ApiPostResponse<LoginUserModel>() { Data = new LoginUserModel() };
            var user = await _userService.Login(loginModel);
            if(user == null)
            {
                response.Success = false;
                response.Message = "Invalid Email ID or Password";
                return response;
            }
            if(user != null && user.UserID > 0)
            {
                TokenModel objTokenData = new TokenModel();
                objTokenData.EmailID = loginModel.EmailID;
                objTokenData.UserId = user.UserID;
                AccessTokenModel objAccessTokenData = _jwtAuthenticationService.GenerateToken(objTokenData, !string.IsNullOrEmpty(_appSettings.JWT_Secret) ? _appSettings.JWT_Secret : "", _appSettings.JWT_Validity_Mins);
                user.JWTToken = objAccessTokenData.Token;

                await _userService.UpdateLoginToken(!string.IsNullOrEmpty(objAccessTokenData.Token) ? objAccessTokenData.Token : "", objAccessTokenData.UserId);
                response.Message = "Login successfully";
                response.Success = true;
                response.Data.JWTToken = !string.IsNullOrEmpty(user.JWTToken) ? user.JWTToken : "";
                response.Data.UserID = user.UserID;
                response.Data.EmailID = user.EmailID;
            }
            return response;
        }

        [HttpGet("list")]
        public async Task<ApiResponse<LoginUserModel>> GetUserList()
        {
            ApiResponse<LoginUserModel> response = new ApiResponse<LoginUserModel>() { Data = new List<LoginUserModel>() };

            var result = await _userService.GetUserList();
            if (result != null)
            {
                response.Data = result;
            }
            response.Success = true;
            return response;
        }

        [HttpPost("save-peer")]
        public async Task<BaseApiResponse> SavePeer(LoginUserModel user)
        {
            BaseApiResponse response = new BaseApiResponse();   
            var result = await _userService.SavePeer(user.PeerUserId, user.UserID);
            if (result != null)
            {
                response.Success = true;
                response.Message = "Peer saved successfuylly";
            }
            return response;
        }

        [HttpGet("get-user-by-id/{id}")]
        public async Task<ApiPostResponse<LoginUserModel>> GetUserById(int id)
        {
            ApiPostResponse<LoginUserModel> response = new ApiPostResponse<LoginUserModel>() { Data = new LoginUserModel() };

            var result = await _userService.GetUserByID(id);
            if (result != null)
            {
                response.Data = result;
            }
            response.Success = true;
            return response;
        }
    }
}
