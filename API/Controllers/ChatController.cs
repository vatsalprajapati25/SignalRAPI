using API.Common.Helpers;
using API.Models.Models;
using API.Services.Services.Chat;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService) 
        {
            _chatService = chatService;   
        }

        //[HttpPost("register-user")]
        //public IActionResult RegisterUser(UserModel user)
        //{
        //    if (_chatService.AddUserToList(user.EmailID))
        //    {
        //        return NoContent();
        //    }

        //    return BadRequest("User already exists");
        //}

        [HttpPost("update-user-connection")]
        public async Task<ApiPostResponse<UserModel>> UpdateUserConnection(UserModel user)
        {
            ApiPostResponse<UserModel> response = new ApiPostResponse<UserModel>();

            //var result = await _chatService.UpdateConnection(user);

            //response.Data = result;
            response.Success = true;
            response.Message = "Connection updated successfully";
            return response;
        }
    }
}
