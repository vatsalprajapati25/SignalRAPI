using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models.Models
{
    public class MessageModel
    {
        public string? From { get; set; }
        public string? To { get; set; }
        [Required]
        public string? Content { get; set; }
    }

    public class ResponseModel
    {
        public int IsSuccess { get; set; }
        public string? Result { get; set; }
    }
}
