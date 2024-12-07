using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Entities.Auth;

namespace ToDoAPI.Entities.Entities
{
    public class Todo
    {
        [Key]
        public int TodoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }

        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Oluşturulma tarihi
        public DateTime DueDate { get; set; }

        // İlişkiler
        public int UserId { get; set; }
        public User User { get; set; }
    }

}
