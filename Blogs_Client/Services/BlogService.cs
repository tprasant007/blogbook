using Blazored.LocalStorage;
using Blogs_Client.Model;
using System.Linq.Expressions;
using System.Net.Http.Json;

namespace Blogs_Client.Services
{
    public class BlogService(HttpClient http) : IBlogService
    {
        public async Task<Blog?> CreateBlog(CreateBlog blogModel)
        {

            var response = await http.PostAsJsonAsync("blogs", new { blogModel.Title, blogModel.Content });
            {
                if (response.IsSuccessStatusCode)
                {
                    var blog = await response.Content.ReadFromJsonAsync<Blog>() ?? throw new Exception("Failed to deserialize");
                    return blog;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorMessage);
                }
            }
        }

        public async Task<List<Blog>> GetAllBlogs()
        {
            var blogs = await http.GetFromJsonAsync<List<Blog>>("blogs") ?? [];
            return blogs;
        }



        public async Task<Blog?> GetBlogById(int id)
        {
            var response = await http.GetAsync($"blogs/{id}");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                return null;
            }
            else
            {
                return await response.Content.ReadFromJsonAsync<Blog>();
            }
        }
    }
}
