using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public interface ITodoService
    {
        Task<List<Todo>> GetTodosAsync();
        Task<Todo> GetTodoByIdAsync(int id);
        Task AddTodoAsync(Todo todo);
        Task UpdateTodoAsync(Todo todo);
        Task DeleteTodoAsync(int id);
    }

