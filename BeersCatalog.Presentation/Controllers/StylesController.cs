using BeersCatalog.BLL.DTOs;
using BeersCatalog.BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace BeersCatalog.Presentation.Controllers;

public class StylesController : Controller
{
    private readonly string baseUrl = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Urls")["ApiUrl"];

    // GET: StylesController
    public async Task<ActionResult> Index()
    {
        string token = Request.Cookies["token"];
        
        if(token == null)
            return RedirectToAction("Index", "Login");

        HttpClient httpClient = new();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await httpClient.GetAsync($"{baseUrl}/styles");

        string apiResponse = await response.Content.ReadAsStringAsync();

        var styles = JsonConvert.DeserializeObject<List<Style>>(apiResponse);

        if(styles == null || styles.Count == 0)
        {
            ViewBag.Message = "Não há estilos registrados";
            return View();
        }

        return View(styles);
    }

    // GET: StylesController/Details/5
    public async Task<ActionResult> Details(int id)
    {
        string token = Request.Cookies["token"];

        if (token == null)
            return RedirectToAction("Index", "Login");

        HttpClient httpClient = new();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await httpClient.GetAsync($"{baseUrl}/styles/{id}");

        string apiResponse = await response.Content.ReadAsStringAsync();

        var style = JsonConvert.DeserializeObject<Style>(apiResponse);

        if (style == null)
        {
            return NotFound();
        }

        return View(style);
    }

    // GET: StylesController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: StylesController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(IFormCollection collection)
    {
        string token = Request.Cookies["token"];

        if (token == null)
            return RedirectToAction("Index", "Login");

        Style style = new();
        style.Name = collection["name"];

        StringContent content = new(JsonConvert.SerializeObject(style), Encoding.UTF8, "application/json");

        HttpClient httpClient = new();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await httpClient.PostAsync($"{baseUrl}/styles/", content);

        string apiResponse = await response.Content.ReadAsStringAsync();

        JsonConvert.DeserializeObject<CreatedAtActionResult>(apiResponse);

        return RedirectToAction("Index");
    }

    // GET: StylesController/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
        string token = Request.Cookies["token"];

        if (token == null)
            return RedirectToAction("Index", "Login");

        HttpClient httpClient = new();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await httpClient.GetAsync($"{baseUrl}/styles/{id}");

        string apiResponse = await response.Content.ReadAsStringAsync();

        var style = JsonConvert.DeserializeObject<Style>(apiResponse);

        if (style == null)
        {
            return NotFound();
        }

        return View(style);
    }

    // POST: StylesController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, IFormCollection collection)
    {
        string token = Request.Cookies["token"];

        if (token == null)
            return RedirectToAction("Index", "Login");

        StyleDTO style = new();
        style.Name = collection["name"];

        StringContent content = new(JsonConvert.SerializeObject(style), Encoding.UTF8, "application/json");

        HttpClient httpClient = new();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.PutAsync($"{baseUrl}/styles/{id}", content);

        if(response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        return View(style);
    }

    // GET: StylesController/Delete/5
    public async Task<ActionResult> Delete(int id)
    {
        string token = Request.Cookies["token"];

        if (token == null)
            return RedirectToAction("Index", "Login");

        HttpClient httpClient = new();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await httpClient.GetAsync($"{baseUrl}/styles/{id}");

        string apiResponse = await response.Content.ReadAsStringAsync();

        var style = JsonConvert.DeserializeObject<Style>(apiResponse);

        if (style == null)
        {
            return NotFound();
        }

        return View(style);
    }

    // POST: StylesController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id, IFormCollection collection)
    {
        string token = Request.Cookies["token"];

        if (token == null)
            return RedirectToAction("Index", "Login");

        HttpClient httpClient = new();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await httpClient.DeleteAsync($"{baseUrl}/styles/{id}");

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        StyleDTO style = new();
        style.Name = collection["name"];

        return View(style);
    }
}
