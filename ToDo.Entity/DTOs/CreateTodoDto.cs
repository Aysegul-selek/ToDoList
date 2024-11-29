using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAPI.Entities.DTOs
{
    public class CategoryDto
    {
        [Key]
        public int CategoryId { get; set; } // Veritabanındaki CategoryId ile uyumlu
        public string Name { get; set; }
    }

}
