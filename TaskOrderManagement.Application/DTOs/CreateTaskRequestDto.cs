using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskOrderManagement.Application.DTOs
{
    public class CreateTaskRequestDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        public int AssignedUserId { get; set; }
    }
}
