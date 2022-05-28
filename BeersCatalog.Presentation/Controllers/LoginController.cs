using BeersCatalog.BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BeersCatalog.Presentation.Controllers;

public class LoginController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    // POST: Login/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(LoginCredentials credentials)
    {
        var httpClient = new HttpClient();

        StringContent content = new(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("https://localhost:7205/api/Auth/Login", content);

        string apiResponse = await response.Content.ReadAsStringAsync();

        AuthToken receivedToken = JsonConvert.DeserializeObject<AuthToken>(apiResponse);

        if (receivedToken.Token != null)
        {
            Response.Cookies.Append("token", receivedToken.Token);
            return View();
        }
        else
        {
            return View();
        }
    }
}
