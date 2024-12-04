using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Entities.DTOs;


[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    // GET: api/Todo
    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var todos = await _todoService.GetAllTodosAsync();
        return Ok(todos); 
    }

    // GET: api/Todo/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodoById(int id)
    {
        var todo = await _todoService.GetTodoByIdAsync(id);
        if (todo == null)
        {
            return NotFound();
        }
        return Ok(todo); 
    }

    // POST: api/Todo
    [HttpPost]
    public async Task<IActionResult> CreateTodo([FromBody] TodoDto todoDto)
    {
        if (todoDto == null)
        {
            return BadRequest("Todo verisi geçersiz.");
        }

        var createdTodo = await _todoService.CreateTodoAsync(todoDto);
        return CreatedAtAction(nameof(GetTodoById), new { id = createdTodo.TodoId }, createdTodo); 
    }

    // PUT: api/Todo/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo(int id, [FromBody] TodoDto todoDto)
    {
        if (id != todoDto.TodoId)
        {
            return BadRequest("Todo ID'leri eşleşmiyor.");
        }

        var updatedTodo = await _todoService.UpdateTodoAsync(todoDto);
        if (updatedTodo == null)
        {
            return NotFound(); 
        }

        return NoContent(); 
    }

    // DELETE: api/Todo/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var deleted = await _todoService.DeleteTodoAsync(id);
        if (!deleted)
        {
            return NotFound(); 
        }

        return NoContent(); 
    }
}