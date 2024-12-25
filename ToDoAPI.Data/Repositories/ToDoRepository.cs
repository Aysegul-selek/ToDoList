using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using ToDoAPI.Entities.Entities;
using ToDoAPI.Data.IRepositories;
using ToDoAPI.Entities.DTOs;
using ToDoAPI.Entities.DTOs.ToDo;
using ToDoAPI.Entities.Enums;

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
            try
            {
                return await _context.Todos.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception here
                throw new Exception("An error occurred while fetching the todos.", ex);
            }
        }



        public async Task<Todo> GetTodoByIdAsync(int todoId)
        {
            return await _context.Todos.Include(t => t.User).FirstOrDefaultAsync(t => t.TodoId == todoId);
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

        public async Task<List<TodoDto>> GetAllWithStatusAndUserAsync()
        {
            return await _context.Todos
                .Include(t => t.User) 
                .Select(t => new TodoDto
                {
                    TodoId = t.TodoId,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status.ToString(),
                    UserName = t.User.Username  
                })
                .ToListAsync();
        }

        public async Task UpdateStatusAsync(int todoId, string status)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(t => t.TodoId == todoId);
            if (todo != null)
            {
                // String değeri StatusEnum'a dönüştürme
                if (Enum.TryParse<StatusEnum>(status, out var parsedStatus))
                {
                    todo.Status = parsedStatus;  // Status'u güncelle
                    _context.Todos.Update(todo);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Geçersiz durum değeri.");  // Geçersiz string durum için hata mesajı
                }
            }
        }

        public async Task UpdateTodoDescriptionAsync(int id, string description)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo != null)
            {
                todo.Description = description;
                await _context.SaveChangesAsync();
            }
        }
    }
}
