using Blogs_API.Models;

namespace Blogs_API.DTO
{
    public class LoginResponseDTO
    {
        public string? Username { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
