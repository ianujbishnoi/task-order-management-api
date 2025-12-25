using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOrderManagement.Domain.Common;

namespace TaskOrderManagement.Domain.Entities
{
    public class TaskItem : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }

        public int AssignedUserId { get; set; }
        public User AssignedUser { get; set; } = null!;
    }
}
