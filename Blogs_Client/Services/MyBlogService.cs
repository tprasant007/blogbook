using Blogs_Client.Model;
using System.Net.Http.Json;

namespace Blogs_Client.Services
{
    public class MyBlogService : IMyBlogService
    {
        private readonly HttpClient _http;
        public MyBlogService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<MyBlog>> GetMyBlogs()
        {
            var blogs = await _http.GetFromJsonAsync<List<MyBlog>>("myblog") ?? [];
            return blogs;
        }
        public async Task DeleteBlog(int id)
        {
            await _http.DeleteAsync($"myblog/{id}");
        }
        public async Task UpdateBlog(int id, CreateBlog blog)
        {
            var response = await _http.PutAsJsonAsync($"myblog/{id}", blog);
            if(!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception(errorMessage);
            }
        }
    }
}
