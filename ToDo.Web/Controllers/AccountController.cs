using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDo.Web.Models;
using ToDoAPI.Business.Concrete;
using ToDoAPI.Entities.DTOs;
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

                    // Token'ı cookie'ye kaydet
                    Response.Cookies.Append("AuthToken", token.AccessToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = false,  // Geliştirme ortamında false, gerçek ortamda true
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTime.Now.AddHours(1)
                    });

                    // Bearer token'ı header'a ekle
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

                    // Kullanıcı adını almak için istek yap
                    var userNameResponse = await client.GetAsync("https://localhost:44305/api/Auth/user");
                    if (userNameResponse.IsSuccessStatusCode)
                    {
                        var userInfo = await userNameResponse.Content.ReadAsStringAsync();
                        var user = JsonConvert.DeserializeObject<UserDto>(userInfo);

                        TempData["UserName"] = user.UserName; // Kullanıcı adını TempData'ya kaydediyoruz
                    }

                    return RedirectToAction("ToDoList", "ToDo");
                }

                ModelState.AddModelError("", "Giriş başarısız.");
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
