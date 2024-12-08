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

        public int UserId { get; set; }  // Kullanıcıyı tutar
        public User User { get; set; }   // User ile ilişki kurar (Navigasyon özelliği)
       
        public StatusEnum Status { get; set; } // Status enum değerini taşıyoruz
       
    }

}
