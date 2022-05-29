using BeersCatalog.BLL.Models;
using BeersCatalog.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BeersCatalog.Presentation.Controllers;

public class RegistrationController : Controller
{
    // GET: Registration/Index
    public IActionResult Index()
    {
        return View();
    }

    // POST: Registration/Index
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(UserDetails userDetails)
    {
        HttpClient httpClient = new();

        StringContent content = new(JsonConvert.SerializeObject(userDetails), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PostAsync("https://localhost:7205/api/Auth/Register", content);

        string apiResponse = await response.Content.ReadAsStringAsync();

        var deserializedResponse = JsonConvert.DeserializeObject<RegistrationResponseViewModel>(apiResponse);

        if (deserializedResponse.Errors != null)
        {
            ViewBag.Message = "Oops, parece que sua tentativa de registro falhou... \r\n" +
                "\n Verifique se sua senha contém ao menos uma letra maiúscula, um número e um caracter especial." +
                "\n Caso sua senha esteja dentro do padrão requerido, tente novamente com um nome de usuário diferente...";
            return View();
        }

        TempData["registration"] = $"Usuário {userDetails.UserName} registrado com sucesso";
        return RedirectToAction("Index", "Login");
    }
}
