using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAPI.Business.Abstract
{
    public class ToDoService : ITodoService
    {
        private readonly ITodoRepository _repository;

        public ToDoService(ITodoRepository repository)
        {
            _repository = repository;
        }
        public Task<List<Todo>> GetTodosAsync() => _repository.GetAllTodosAsync();

        public Task<Todo> GetTodoByIdAsync(int id) => _repository.GetTodoByIdAsync(id);

        public Task AddTodoAsync(Todo todo) => _repository.AddTodoAsync(todo);

        public Task UpdateTodoAsync(Todo todo) => _repository.UpdateTodoAsync(todo);

        public Task DeleteTodoAsync(int id) => _repository.DeleteTodoAsync(id);
    }
}
