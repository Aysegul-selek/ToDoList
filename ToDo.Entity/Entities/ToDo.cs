using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Entities.Auth;
using ToDoAPI.Entities.Enums;

namespace ToDoAPI.Entities.Entities
{
    public class Todo
    {
        [Key]
        public int TodoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; } 
        public User User { get; set; }      
        public StatusEnum Status { get; set; }
        public DateTime CreatedAtNew { get; set; }
        public DateTime? DueDate { get; set; }
    }

}
