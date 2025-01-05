using System.ComponentModel.DataAnnotations;

namespace Blogs_API.DTO
{
    public class CreateBlogDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
