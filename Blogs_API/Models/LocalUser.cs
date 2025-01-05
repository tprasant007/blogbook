using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Blogs_API.Models
{
    public class LocalUser
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string HashPassword { get; set; } = string.Empty;


    }
}
