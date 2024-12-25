using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Business.Abstract;
using ToDoAPI.Entities.DTOs.ToDo;


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
    public async Task<ActionResult<TodoDto>> CreateTodo([FromBody] TodoCreateDTO todoCreateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdTodo = await _todoService.CreateTodoAsync(todoCreateDto);
        return CreatedAtAction(nameof(GetTodoById), new { id = createdTodo.TodoId }, createdTodo);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<TodoDto>> UpdateTodo(int id, [FromBody] TodoCreateDTO todoCreateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedTodo = await _todoService.UpdateTodoAsync(id, todoCreateDto);
        if (updatedTodo == null)
            return NotFound("Todo not found.");
        return Ok(updatedTodo);
    }
    [HttpPut("{todoId}/status")]
    public async Task<IActionResult> UpdateStatus(int todoId, string status)
    {
       
            var result = await _todoService.UpdateStatusAsync(todoId, status);
            if (result)
            {
                return Ok(new { message = "Status updated successfully!" });
            }
            return BadRequest(new { message = "Invalid status or Todo not found!" });
        
      
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

    [HttpPut("{id}/description")]
    public async Task<IActionResult> UpdateTodoDescription(int id, [FromBody] string description)
    {
        var todo = await _todoService.GetTodoByIdAsync(id);

        if (todo == null)
        {
            return NotFound();
        }

        await _todoService.UpdateTodoDescriptionAsync(id, description);
        return NoContent(); // Güncelleme başarılı
    }
}