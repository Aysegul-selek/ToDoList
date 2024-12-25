using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Data.IRepositories;
using ToDoAPI.Entities.DTOs.ToDo;
using ToDoAPI.Entities.Entities;
using ToDoAPI.Entities.Enums;
using TodoApp.Data.Repositories;

namespace ToDoAPI.Business.Abstract
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;
       
        public TodoService(IMapper mapper, ITodoRepository todoRepository)
        {
            _mapper = mapper;
            _todoRepository = todoRepository;
        
        }


        public async Task<TodoDto> CreateTodoAsync(TodoCreateDTO todoCreateDto)
        {
            var todo = _mapper.Map<Todo>(todoCreateDto);
            var createdTodo = await _todoRepository.CreateTodoAsync(todo);
            return _mapper.Map<TodoDto>(createdTodo);
        }

        public async Task<bool> DeleteTodoAsync(int todoId)
        {
            return await _todoRepository.DeleteTodoAsync(todoId);
        }

        public async Task<List<TodoDto>> GetAllTodosAsync()
        {
            var todos = await _todoRepository.GetAllTodosAsync();
            return _mapper.Map<List<TodoDto>>(todos);
        }

        public async Task<TodoDto> GetTodoByIdAsync(int todoId)
        {
            var todo = await _todoRepository.GetTodoByIdAsync(todoId);
            return _mapper.Map<TodoDto>(todo);
        }

        public async Task<bool> UpdateStatusAsync(int todoId, string status)
        {
            // StatusEnum'a dönüştürme işlemi
            if (Enum.TryParse<StatusEnum>(status, out var parsedStatus))
            {
                var todo = await _todoRepository.GetTodoByIdAsync(todoId);
                if (todo != null)
                {
                    todo.Status = parsedStatus;
                    await _todoRepository.UpdateTodoAsync(todo);
                    return true;
                }
            }
            return false; // Status geçersiz veya Todo bulunamadı
        }
        public async Task<TodoDto> UpdateTodoAsync(int todoId, TodoCreateDTO todoCreateDto)
        {
            var todo = await _todoRepository.GetTodoByIdAsync(todoId);
            if (todo == null)
                throw new Exception("Todo not found.");

            _mapper.Map(todoCreateDto, todo);
            var updatedTodo = await _todoRepository.UpdateTodoAsync(todo);
            return _mapper.Map<TodoDto>(updatedTodo);
        }

        public async Task UpdateTodoDescriptionAsync(int id, string description)
        {
            await _todoRepository.UpdateTodoDescriptionAsync(id, description);
        }
    }
}
