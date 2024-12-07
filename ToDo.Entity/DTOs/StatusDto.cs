using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAPI.Entities.DTOs
{
    public class StatusDto
    {
        [Key]
        public int StatusId { get; set; } // Veritabanındaki StatusId ile uyumlu
        public string Name { get; set; }
    }

}
