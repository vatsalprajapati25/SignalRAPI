using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models.Models
{
    public class TokenModel
    {
        public long UserId { get; set; }
        public string? EmailID { get; set; }
        public string? FullName { get; set; }
        public DateTime TokenValidTo { get; set; }
        public string? UserName { get; set; }

    }
}
