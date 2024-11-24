using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Core.Entities;

namespace ToDoAPI.Core.Concrete
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetAllTodosAsync();
        Task<Todo> GetTodoByIdAsync(int id);
        Task AddTodoAsync(Todo todo);
        Task UpdateTodoAsync(Todo todo);
        Task DeleteTodoAsync(int id);
    }
}
