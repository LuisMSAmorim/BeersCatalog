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

        LoginResponseViewModel receivedToken = JsonConvert.DeserializeObject<LoginResponseViewModel>(apiResponse);

        if (receivedToken.Token != null)
        {
            CookieOptions option = new();
            option.Expires = DateTime.Now.AddMinutes(60);
            Response.Cookies.Append("token", receivedToken.Token, option);

            return RedirectToAction("Index", "Home");
        }
        else
        {
            ViewBag.Message = "Login ou senha inválidos...";
            return View();
        }
    }

    // POST Login/Logout
    public IActionResult Logout()
    {
        Response.Cookies.Delete("token");

        return RedirectToAction("Index", "Home");
    }
}
