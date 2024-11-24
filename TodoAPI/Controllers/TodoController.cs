using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _service;

    public TodoController(ITodoService service)
    {
        _service = service;
    }

    [HttpGet]
    public Task<List<Todo>> GetTodos() => _service.GetTodosAsync();

    [HttpGet("{id}")]
    public Task<Todo> GetTodoById(int id) => _service.GetTodoByIdAsync(id);

    [HttpPost]
    public Task AddTodo(Todo todo) => _service.AddTodoAsync(todo);

    [HttpPut]
    public Task UpdateTodo(Todo todo) => _service.UpdateTodoAsync(todo);

    [HttpDelete("{id}")]
    public Task DeleteTodoAsync(int id) => _service.DeleteTodoAsync(id);
}
