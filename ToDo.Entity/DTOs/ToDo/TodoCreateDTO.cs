using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Entities.Enums;

namespace ToDoAPI.Entities.DTOs.ToDo
{
    public class TodoCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public StatusEnum Status { get; set; } // Kullanıcıdan gelen enum değeri
    }
}
