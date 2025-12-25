using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOrderManagement.Domain.Enum;

namespace TaskOrderManagement.Application.DTOs
{
    public class CreateUserRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = null!;

        [Required]
        public UserRole Role { get; set; }
    }
}
