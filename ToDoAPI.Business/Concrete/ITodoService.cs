using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Entities.DTOs;
using ToDoAPI.Entities.Entities;


    public interface ITodoService
    {
    Task<List<TodoDto>> GetAllTodosAsync();
    Task<TodoDto> GetTodoByIdAsync(int todoId);
    Task<TodoDto> CreateTodoAsync(TodoDto todoDto);
    Task<TodoDto> UpdateTodoAsync(TodoDto todoDto);
    Task<bool> DeleteTodoAsync(int todoId);
}

