using Blogs_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Blogs_API.DTO
{
    public class BlogDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string BlogCreator { get; set; } = string.Empty;
    }
}
