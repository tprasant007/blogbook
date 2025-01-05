using Blogs_API.DTO;
using Blogs_API.Mapper;
using Blogs_API.Repositories;
using Blogs_API.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blogs_API.Controllers
{
    [Route("api/blogs")]
    [ApiController]
    [Authorize]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepo _blogRepo;
        public BlogController(IBlogRepo blogRepo)
        {
            _blogRepo = blogRepo;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var blogs = await _blogRepo.GetBlogsAsync();
            var blogDto = blogs.Select(s => s.ToBlogDto());
            return Ok(blogDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBlogById([FromRoute] int id) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var blog = await _blogRepo.GetByIdAsync(id);
            if (blog == null) return NotFound("No such blog found");
            return Ok(blog.ToBlogDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] CreateBlogDto blogDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (userId == null)
            {
                return Unauthorized("User ID not found. Please login to create a blog.");
            }
            var blogModel = blogDto.ToBlogFromCreate();
            blogModel.UserId = Int32.Parse(userId);
            await _blogRepo.CreateBlogAsync(blogModel);
            return Ok(blogDto);
        }

        
    }
}
