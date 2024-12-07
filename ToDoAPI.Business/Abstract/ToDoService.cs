using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Data.IRepositories;
using ToDoAPI.Entities.DTOs;
using ToDoAPI.Entities.Entities;

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

        public async Task<TodoDto> CreateTodoAsync(TodoDto todoDto)
        {
            var todo = _mapper.Map<Todo>(todoDto);
            var createdTodo = await _todoRepository.CreateTodoAsync(todo);
            return _mapper.Map<TodoDto>(createdTodo);
        }

        public async Task<bool> DeleteTodoAsync(int todoId)
        {
            return await _todoRepository.DeleteTodoAsync(todoId);
        }

        public async Task<List<TodoDto>> GetAllTodosAsync()
        {
            var todos = await _todoRepository.GetAllWithStatusAndUserAsync();
            return _mapper.Map<List<TodoDto>>(todos);
        }

        public async Task<TodoDto> GetTodoByIdAsync(int todoId)
        {
            var todo = await _todoRepository.GetTodoByIdAsync(todoId);
            return _mapper.Map<TodoDto>(todo);
        }

        public async Task<TodoDto> UpdateTodoAsync(TodoDto todoDto)
        {
            var todo = _mapper.Map<Todo>(todoDto);
            var updatedTodo = await _todoRepository.UpdateTodoAsync(todo);
            return _mapper.Map<TodoDto>(updatedTodo);
        }
    }
}
