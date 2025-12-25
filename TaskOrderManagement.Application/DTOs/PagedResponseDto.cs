using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskOrderManagement.Application.DTOs
{
    public class PagedResponseDto<T>
    {
        public int TotalRecords { get; set; }
        public List<T> Data { get; set; } = new();
    }
}
