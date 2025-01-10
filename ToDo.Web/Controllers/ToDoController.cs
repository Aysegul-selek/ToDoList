using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using ToDoAPI.Entities.DTOs.ToDo;
using ToDoAPI.Entities.Enums;

namespace ToDo.Web.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ToDoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ToDoList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44305/api/Todo");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<TodoDto>>(jsonData);

                foreach (var todo in values)
                {

                    if (Enum.TryParse(typeof(StatusEnum), todo.Status, out var statusEnumValue))
                    {
                        todo.Status = Enum.GetName(typeof(StatusEnum), statusEnumValue);
                    }
                }

                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoCreateDTO model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:44305/api/Todo", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Detail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44305/api/Todo/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var todo = JsonConvert.DeserializeObject<TodoDto>(jsonData);
                return View(todo);
            }
            return RedirectToAction("Index");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateStatus(int todoId, string status)
        {
            var client = _httpClientFactory.CreateClient();


            var jsonData = JsonConvert.SerializeObject(new { Status = status });
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

           
            var responseMessage = await client.PutAsync($"https://localhost:44305/api/Todo/{todoId}/status", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return Ok(new { message = "Status updated successfully!" });
            }

            return StatusCode(500, "Failed to update status.");
        }
        [HttpPut("{todoId}/description")]
        public async Task<IActionResult> UpdateDescription(int todoId, [FromBody] string description)
        {
            var client = _httpClientFactory.CreateClient();

         
            var updateDescriptionDTO = new TodoDto
            {
                TodoId = todoId,
                Description = description
            };

            var jsonData = JsonConvert.SerializeObject(updateDescriptionDTO);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PutAsync($"https://localhost:44305/api/Todo/{todoId}/description", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return Ok(new { message = "Description updated successfully!" });
            }

            return StatusCode(500, "Failed to update description.");
        }


    }
}
