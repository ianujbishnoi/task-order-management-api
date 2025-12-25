using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOrderManagement.Application.DTOs;
using TaskOrderManagement.Application.Interfaces;
using TaskOrderManagement.Domain.Entities;
using TaskOrderManagement.Infrastructure.Data;

namespace TaskOrderManagement.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<List<TaskItem>> GetTasksByUserAsync(int userId)
        {
            return await _context.Tasks
                .AsNoTracking()
                .Where(t => t.AssignedUserId == userId)
                .ToListAsync();
        }

        public async Task<PagedResponseDto<TaskItem>> GetPagedTasksAsync(PagedRequestDto request)
        {
            var query = _context.Tasks
                .AsNoTracking()
                .AsQueryable();

            // 🔍 Filtering
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(t => t.Title.Contains(request.Search));
            }

            // ↕ Sorting
            query = request.SortBy?.ToLower() switch
            {
                "title" => request.IsDescending
                    ? query.OrderByDescending(t => t.Title)
                    : query.OrderBy(t => t.Title),

                _ => query.OrderByDescending(t => t.CreatedAt)
            };

            var totalRecords = await query.CountAsync();

            // 📄 Pagination
            var data = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResponseDto<TaskItem>
            {
                TotalRecords = totalRecords,
                Data = data
            };
        }

    }
}
