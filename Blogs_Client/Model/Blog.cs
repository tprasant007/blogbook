﻿namespace Blogs_Client.Model
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string BlogCreator { get; set; } = string.Empty;
    }
}
