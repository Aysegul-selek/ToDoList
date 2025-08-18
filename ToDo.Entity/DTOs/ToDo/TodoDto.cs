using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Entities.Entities;
using ToDoAPI.Entities.Enums;

namespace ToDoAPI.Entities.DTOs.ToDo
{
    public class TodoDto
    {
        [Key]
        public int TodoId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }

    }
}
