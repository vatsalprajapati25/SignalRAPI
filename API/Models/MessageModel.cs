using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class MessageModel
    {
        [Required]
        public string From { get; set; }
        public string To { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
