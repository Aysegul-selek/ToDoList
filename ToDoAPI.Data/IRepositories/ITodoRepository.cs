using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Entities.ToDo;

namespace ToDoAPI.Data.IRepositories
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetAllTodosAsync();
        Task<Todo> GetTodoByIdAsync(int todoId);
        Task<Todo> CreateTodoAsync(Todo todo);
        Task<Todo> UpdateTodoAsync(Todo todo);
        Task<bool> DeleteTodoAsync(int todoId);
    }
}
