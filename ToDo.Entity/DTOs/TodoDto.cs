using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAPI.Entities.DTOs
{
    public class TodoDto
    {
        [Key]
        public int TodoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
