using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Entities.DTOs.ToDo;

namespace ToDo.Web.Controllers
{
    public class ToDoController : Controller
    {private readonly ITodoService _todoService;

        public ToDoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public async Task<IActionResult> Index()
        {
            var todos = await _todoService.GetAllTodosAsync();
            return View(todos);
        }
        // ToDo detaylarını görüntülemek için
        public async Task<IActionResult> Details(int id)
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoCreateDTO todoCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(todoCreateDto);
            }

            await _todoService.CreateTodoAsync(todoCreateDto);
            return RedirectToAction(nameof(Index));
        }
    }
}
