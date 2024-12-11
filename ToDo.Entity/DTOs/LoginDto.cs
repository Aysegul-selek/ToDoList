using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Entities.DTOs
{
    public class LoginDto
    {
      
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
