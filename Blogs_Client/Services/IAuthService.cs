using Blogs_Client.Model;

namespace Blogs_Client.Services
{
    public interface IAuthService
    {
        User? User { get; }
        event Action OnUserChanged;
        Task Initialize();
        Task Login(string username, string password);
        Task Logout();
        Task Register(string username, string email, string password);
    }
}
