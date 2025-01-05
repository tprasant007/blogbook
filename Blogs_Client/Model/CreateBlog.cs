using System.ComponentModel.DataAnnotations;

namespace Blogs_Client.Model
{
    public class CreateBlog
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
    }
}
