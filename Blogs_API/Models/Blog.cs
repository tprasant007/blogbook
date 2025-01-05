using System.ComponentModel.DataAnnotations;

namespace Blogs_API.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content {  get; set; } = string.Empty;
        public int UserId { get; set; }
        //Navigation
        public string UserName { get; set; } = string.Empty;

    }
}
