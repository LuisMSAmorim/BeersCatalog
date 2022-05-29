using BeersCatalog.BLL.Models;
using BeersCatalog.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BeersCatalog.Presentation.Controllers;

public class LoginController : Controller
{
    // GET: Login/Index
    public IActionResult Index()
    {
        return View();
    }

    // POST: Login/Index
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(LoginCredentials credentials)
    {
        var httpClient = new HttpClient();

        StringContent content = new(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("https://localhost:7205/api/Auth/Login", content);

        string apiResponse = await response.Content.ReadAsStringAsync();

        LoginResponse receivedToken = JsonConvert.DeserializeObject<LoginResponse>(apiResponse);

        if (receivedToken.Token != null)
        {
            CookieOptions option = new();
            option.Expires = DateTime.Now.AddMinutes(60);
            Response.Cookies.Append("token", receivedToken.Token, option);
            ViewBag.Message = null;
            return View();
        }
        else
        {
            ViewBag.Message = "Login ou senha inválidos...";
            return View();
        }
    }
}
