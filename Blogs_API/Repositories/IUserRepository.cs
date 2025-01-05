using Blogs_API.DTO;
using Blogs_API.Models;

namespace Blogs_API.Repositories
{
    public interface IUserRepository
    {
        Task<bool> IsUniqueUserAsync(string username, string email);
        Task<LoginResponseDTO?> Login(LoginRequestDTO loginRequestDto);
        Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO);
    }
}
