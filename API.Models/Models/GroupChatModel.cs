using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models.Models
{
    public class GroupChatModel
    {
        public int UserID { get; set; }
        public string? ChatConnectionID { get; set; }
        public string? EmailID { get; set; }
        public string? UserName { get; set; }
        public string? Message { get; set; }
    }
}
