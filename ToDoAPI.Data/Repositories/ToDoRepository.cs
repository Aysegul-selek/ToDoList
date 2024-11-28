using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using ToDoAPI.Entities.ToDo;

namespace TodoApp.Data.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoRepository(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Todo>> GetAllTodosAsync()
        {
            return await _context.Todos.Include(t => t.User).Include(t => t.Category).ToListAsync();
        }

        public async Task<Todo> GetTodoByIdAsync(int todoId)
        {
            return await _context.Todos.Include(t => t.User).Include(t => t.Category).FirstOrDefaultAsync(t => t.TodoId == todoId);
        }

        public async Task<Todo> CreateTodoAsync(Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<Todo> UpdateTodoAsync(Todo todo)
        {
            _context.Todos.Update(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<bool> DeleteTodoAsync(int todoId)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(t => t.TodoId == todoId);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
