using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Entities.Entities;

namespace ToDoAPI.Data.IRepositories
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetAllTodosAsync();
        Task<List<Todo>> GetAllWithCategoryAndUserAsync();
        Task<Todo> GetTodoByIdAsync(int todoId);
        Task<Todo> CreateTodoAsync(Todo todo);
        Task<Todo> UpdateTodoAsync(Todo todo);
        Task<bool> DeleteTodoAsync(int todoId);
    }
}
