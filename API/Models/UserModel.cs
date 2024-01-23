using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class UserModel
    {
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Invslid name.")]
        public string Name { get; set; }
    }
}
