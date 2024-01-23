using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;
        public ChatController(ChatService chatService) 
        {
            _chatService = chatService;   
        }

        [HttpPost("register-user")]
        public IActionResult RegisterUser(UserModel user)
        {
            if (_chatService.AddUserToList(user.Name))
            {
                return NoContent();
            }

            return BadRequest("User already exists");
        }
    }
}
