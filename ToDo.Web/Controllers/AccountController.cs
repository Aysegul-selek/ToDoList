using IdentityModel.Client;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ToDo.Web.Models;
using ToDoAPI.Entities.DTOs.Auth;  // Kullanıcı kayıt ve giriş modelini burada tanımlayabilirsiniz

namespace ToDo.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Kayıt Olma Sayfası
        public IActionResult Register()
        {
            return View();
        }

        // Kayıt Olma POST isteği
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var responseMessage = await client.PostAsync("https://localhost:44305/api/Auth/register", content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login", "Account");  // Kayıt başarılıysa giriş sayfasına yönlendir
                }

                // Hata durumunda mesaj göster
                ModelState.AddModelError("", "Kayıt işlemi başarısız.");
            }
            return View(model);
        }

        // Giriş Yapma Sayfası
        public IActionResult Login()
        {
            return View();
        }

        // Giriş Yapma POST isteği
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var responseMessage = await client.PostAsync("https://localhost:44305/api/Auth/login", content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                    var token = JsonConvert.DeserializeObject<TokenResponse>(jsonResponse);

                    // Token'ı local storage veya cookie'ye kaydedebilirsiniz
                    // Burada token'ı kullanıcının kimliğini doğrulamak için saklayabilirsiniz
                    Response.Cookies.Append("AuthToken", token.AccessToken, new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict
                    });

                    return RedirectToAction("Index", "ToDo");  // Giriş başarılıysa ToDo ana sayfasına yönlendir
                }

                // Hata durumunda mesaj göster
                ModelState.AddModelError("", "Giriş işlemi başarısız.");
            }
            return View(model);
        }

        // Çıkış Yapma
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");
            return RedirectToAction("Login");
        }
    }
}
