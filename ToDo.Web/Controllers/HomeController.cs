using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDo.Web.Models;
using ToDoAPI.Entities.DTOs.ToDo;

namespace ToDo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ITodoService _todoService;

        public HomeController(HttpClient httpClient, ITodoService todoService)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri("https://localhost:44305/api/")
            };
            _todoService = todoService;
        }

        public async Task<IActionResult> Index()
        {
            var todos = await _todoService.GetAllTodosAsync();
            return View(todos);
        }
        // ToDo detaylarýný görüntülemek için
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
    

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
