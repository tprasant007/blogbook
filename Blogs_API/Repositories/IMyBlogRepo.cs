using Blogs_API.DTO;
using Blogs_API.Models;

namespace Blogs_API.Repositories
{
    public interface IMyBlogRepo
    {
        Task<List<MyBlogResponseDTO>> GetMyBlogsAsync(int userId);
        Task<Blog?> UpdateBlogAsync(int id, UpdateBlogDTO blogDto);
        Task<Blog?> DeleteBlogAsync(int id);
    }
}
