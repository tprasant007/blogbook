using Blogs_Client.Model;

namespace Blogs_Client.Services
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAllBlogs();
        Task<Blog?> GetBlogById(int id);
        Task<Blog?> CreateBlog(CreateBlog blog);
    }
}
