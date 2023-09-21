﻿using System.ComponentModel.DataAnnotations;

namespace eStoreAPI.DTOs
{
    public class LoginDto
    {
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}
