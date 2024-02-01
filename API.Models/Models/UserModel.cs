using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models.Models
{
    public class UserModel
    {
        public long UserID { get; set; }
        public string? UserName { get; set; }
        public string? EmailID { get; set; }
        public string? Password { get; set; }
        public string? ChatUserId { get; set; }
        public string? ChatConnectionID { get; set; }
        public string? PeerUserId { get; set; }
        public string? PeerConnectionId { get; set; }
        public bool IsDeleted { get; set; }


    }
}
