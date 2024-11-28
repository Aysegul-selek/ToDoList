using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Entities.ToDo;

namespace ToDoAPI.Entities.Categories
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        // İlişkiler
        public ICollection<Todo> Todos { get; set; }
    }

}
