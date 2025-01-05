﻿using System.ComponentModel.DataAnnotations;

namespace Blogs_API.DTO
{
    public class RegistrationRequestDTO
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
