using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models.Models
{
    public class MessageModel
    {
    }

    public class ResponseModel
    {
        public int IsSuccess { get; set; }
        public string? Result { get; set; }
    }
}
