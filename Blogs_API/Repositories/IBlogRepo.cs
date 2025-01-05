using Blogs_API.DTO;
using Blogs_API.Models;
using Blogs_API.Util;

namespace Blogs_API.Repositories
{
    public interface IBlogRepo
    {
        Task<List<Blog>> GetBlogsAsync();
        Task<Blog?> GetByIdAsync(int id);
        Task<Blog> CreateBlogAsync(Blog blogModel);
    }
}
