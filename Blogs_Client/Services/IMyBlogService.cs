using Blogs_Client.Model;

namespace Blogs_Client.Services
{
    public interface IMyBlogService
    {
        Task<List<MyBlog>> GetMyBlogs();
        Task DeleteBlog(int Id);
        Task UpdateBlog (int Id, CreateBlog blog);
    }
}
