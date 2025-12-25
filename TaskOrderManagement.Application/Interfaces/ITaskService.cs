using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOrderManagement.Application.DTOs;
using TaskOrderManagement.Domain.Entities;

namespace TaskOrderManagement.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskItem> CreateTaskAsync(TaskItem task);
        Task<List<TaskItem>> GetTasksByUserAsync(int userId);
        Task<PagedResponseDto<TaskItem>> GetPagedTasksAsync(PagedRequestDto request);
    }
}
