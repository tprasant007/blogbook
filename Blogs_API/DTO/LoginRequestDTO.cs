using Blogs_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Blogs_API.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password {get; set; } = string.Empty;
    }
}
