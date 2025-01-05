using Blogs_API.DTO;
using Blogs_API.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Blogs_API.Repositories
{
    public class MyBlogRepo : IMyBlogRepo
    {
        private readonly string _connectionString;
        public MyBlogRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<List<MyBlogResponseDTO>> GetMyBlogsAsync(int userId)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = "SELECT Id, Title, Content FROM Blogs WHERE Blogs.UserId = @userId ORDER BY Id DESC";
            var blogs = await connection.QueryAsync<MyBlogResponseDTO>(sql, new { userId });
            return blogs.ToList();
        }
        public async Task<Blog?> DeleteBlogAsync(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            var findSql = "SELECT * FROM Blogs WHERE Id = @id";
            var blog = await connection.QueryFirstOrDefaultAsync<Blog>(findSql, new { id });
            if (blog == null) return null;
            var sql = "DELETE FROM Blogs WHERE Id = @id";
            await connection.ExecuteAsync(sql, new { id });
            return blog;
        }

        public async Task<Blog?> UpdateBlogAsync(int id, UpdateBlogDTO blogDto)
        {
            using var connection = new SqliteConnection(_connectionString);
            var findSql = "SELECT * FROM Blogs WHERE Id = @id";
            var blog = await connection.QuerySingleOrDefaultAsync<Blog>(findSql, new { id });
            if (blog == null) return null;
            var sql = "UPDATE Blogs SET Title = @Title, Content = @Content WHERE Id = @id";
            await connection.ExecuteAsync(sql, new { blogDto.Title, blogDto.Content, id });
            return blog;
        }
    }
}
