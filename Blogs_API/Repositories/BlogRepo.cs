using Blogs_API.DTO;
using Blogs_API.Models;
using Blogs_API.Util;
using Dapper;
using Microsoft.Data.Sqlite;


namespace Blogs_API.Repositories
{
    public class BlogRepo : IBlogRepo
    {
        private readonly string _connectionString;
        public BlogRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Blog>> GetBlogsAsync()
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = "SELECT Blogs.*, LocalUsers.Id AS UserKey, LocalUsers.UserName FROM Blogs JOIN LocalUsers ON Blogs.UserId = LocalUsers.Id ORDER BY Blogs.Id DESC";
            
            var blogs = await connection.QueryAsync<Blog, LocalUser, Blog>(sql, (blog, localUser) =>
            {
                blog.UserName = localUser.UserName;
                return blog;
            },
            splitOn: "UserKey"
            );
            return blogs.ToList();
        }
        public async Task<Blog?> GetByIdAsync(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = "SELECT Blogs.*, LocalUsers.Id AS UserKey, LocalUsers.UserName FROM Blogs JOIN LocalUsers ON Blogs.UserId = LocalUsers.Id WHERE Blogs.Id = @BlogId";
            var blogs = await connection.QueryAsync<Blog, LocalUser, Blog>(sql, (blog, localUser) =>
            {
                blog.UserName = localUser.UserName;
                return blog;
            },
            new { BlogId = id },
            splitOn: "UserKey"
            );
            return blogs.SingleOrDefault();
        }

        public async Task<Blog> CreateBlogAsync(Blog blogModel)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = "INSERT INTO Blogs (Title, Content, UserId) VALUES (@title, @content, @userId)";
            await connection.ExecuteAsync(sql, new { title = blogModel.Title, content = blogModel.Content, userId = blogModel.UserId});
            return blogModel;
        }
    }
}
