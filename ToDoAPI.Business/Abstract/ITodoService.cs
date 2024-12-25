using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Entities.DTOs.ToDo;
using ToDoAPI.Entities.Entities;


public interface ITodoService
    {
    Task<List<TodoDto>> GetAllTodosAsync();
    Task<TodoDto> GetTodoByIdAsync(int todoId);
    Task<TodoDto> CreateTodoAsync(TodoCreateDTO todoDto);
    Task<TodoDto> UpdateTodoAsync(int todoId, TodoCreateDTO todoCreateDto);
    Task UpdateTodoDescriptionAsync(int id, string description);
Task<bool> DeleteTodoAsync(int todoId);

    Task<bool> UpdateStatusAsync(int todoId, string status);
}

