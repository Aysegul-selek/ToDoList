using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using ToDoAPI.Entities.DTOs;
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

            // Cookie'den token'ı al
            var token = Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                // Token yoksa misafir olarak göster
                ViewBag.UserName = "vvvv";
            }
            else
            {
                // Token varsa, header'a ekleyip API'den kullanıcı bilgisini çek
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var userInfoResponse = await client.GetAsync("https://localhost:44305/api/Auth/user");


                if (userInfoResponse.IsSuccessStatusCode)
                {
                    var userJson = await userInfoResponse.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<UserDto>(userJson);
                    ViewBag.UserName = user.UserName; // Kullanıcı adını ViewBag'e kaydediyoruz
                }
                else
                {
                    // Token geçerli değilse misafir olarak göster
                    ViewBag.UserName = "Misafir";
                }
            }

            // Todo listesi çekiliyor
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

            // Eğer Todo listesi alınamazsa boş bir liste döneceğiz
            return View(new List<TodoDto>());
        }

        [HttpGet]
        public IActionResult Create()
        {
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
                return RedirectToAction("ToDoList");
            }
            return View();
        }
        public async Task<IActionResult> Detail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44305/api/Todo");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var todos = JsonConvert.DeserializeObject<List<TodoDto>>(jsonData); 
                return View(todos);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();

            // API tarafındaki DELETE endpointi
            var responseMessage = await client.DeleteAsync($"https://localhost:44305/api/Todo/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                // Silme başarılı olursa listeye dön
                return RedirectToAction("ToDoList");
            }

            // Hata durumunda da listeye dön ama istersen hata mesajı ekleyebilirsin
            return RedirectToAction("ToDoList");
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] TodoCreateDTO model)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await client.PutAsync($"https://localhost:44305/api/Todo/{id}", content);
            if (resp.IsSuccessStatusCode) return Ok();
            return BadRequest("Güncelleme başarısız!");
        }


        // SADECE STATUS → PUT /api/Todo/{id}/status?status=...
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var client = _httpClientFactory.CreateClient();
            var resp = await client.PutAsync($"https://localhost:44305/api/Todo/{id}/status?status={Uri.EscapeDataString(status)}", null);
            if (resp.IsSuccessStatusCode) return Ok();
            return BadRequest();
        }

        // SADECE DESCRIPTION → PUT /api/Todo/{id}/description  (body: string)
        [HttpPost]
        public async Task<IActionResult> UpdateDescription(int id, [FromBody] string description)
        {
            var client = _httpClientFactory.CreateClient();
            // API [FromBody] string beklediği için JSON string literal gönderiyoruz: "metin"
            var content = new StringContent(JsonConvert.SerializeObject(description), Encoding.UTF8, "application/json");

            var resp = await client.PutAsync($"https://localhost:44305/api/Todo/{id}/description", content);
            if (resp.IsSuccessStatusCode) return Ok();
            return BadRequest();
        }

    }
}
