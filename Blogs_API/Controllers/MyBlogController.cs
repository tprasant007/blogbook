using Blogs_API.DTO;
using Blogs_API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blogs_API.Controllers
{
    [Route("api/myblog")]
    [ApiController]
    [Authorize]
    public class MyBlogController : ControllerBase
    {
        private readonly IMyBlogRepo _repo;
        private readonly IBlogRepo _blogRepo;
        public MyBlogController(IMyBlogRepo repo, IBlogRepo blogRepo)
        {
            _repo = repo;
            _blogRepo = blogRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyBlogs()
        {
            var userId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (userId == null)
            {
                return Unauthorized("User ID not found. Please login to create a blog.");
            }
            var blogs = await _repo.GetMyBlogsAsync(Int32.Parse(userId));
            return Ok(blogs);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog([FromRoute] int id)
        {
            var blog = await _blogRepo.GetByIdAsync(id);
            if (blog == null) return NotFound("No such blog found");
            var userId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (userId == null || blog.UserId != Int32.Parse(userId))
            {
                return Unauthorized("Access denied");
            }
            await _repo.DeleteBlogAsync(id);
            return NoContent();

        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBlog([FromRoute] int id, [FromBody] UpdateBlogDTO blogDto)
        {
            var blog = await _blogRepo.GetByIdAsync(id);
            if (blog == null) return NotFound("No such blog found");
            var userId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (userId == null || blog.UserId != Int32.Parse(userId))
            {
                return Unauthorized("Access denied");
            }
            var blogModel = await _repo.UpdateBlogAsync(id, blogDto);
            if (blogModel == null) return NotFound();
            return Ok(blogModel);
        }
    }
}
