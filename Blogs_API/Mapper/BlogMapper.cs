using Blogs_API.DTO;
using Blogs_API.Models;

namespace Blogs_API.Mapper
{
    public static class BlogMapper
    {
        public static BlogDTO ToBlogDto(this Blog blogModel)
        {
            return new BlogDTO
            {
                Id = blogModel.Id,
                Title = blogModel.Title,
                Content = blogModel.Content,
                BlogCreator = blogModel.UserName
            };
        }
        public static Blog ToBlogFromCreate(this CreateBlogDto blogDTO)
        {
            return new Blog
            {
                Title = blogDTO.Title,
                Content = blogDTO.Content,
            };
        }
    }
}
