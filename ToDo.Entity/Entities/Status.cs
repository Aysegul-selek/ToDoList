using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAPI.Entities.Entities
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public string Name { get; set; }
        public ICollection<Todo> Todos { get; set; }
    }

}
