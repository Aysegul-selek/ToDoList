using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Entities.Entities;

namespace ToDoAPI.Entities.Auth
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        // İlişkiler
        public ICollection<Todo> Todos { get; set; }
    }

}
