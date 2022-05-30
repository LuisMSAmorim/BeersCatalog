using Microsoft.AspNetCore.Mvc;
using BeersCatalog.BLL.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using BeersCatalog.BLL.DTOs;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeersCatalog.Presentation.Controllers;

public class BeersController : Controller
{
    // GET: BeersController
    public async Task<ActionResult> Index()
    {
        string token = Request.Cookies["token"];

        if (token == null)
            return RedirectToAction("Index", "Login");

        HttpClient httpClient = new();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7205/api/Beers");

        string apiResponse = await response.Content.ReadAsStringAsync();

        var beers = JsonConvert.DeserializeObject<List<Beer>>(apiResponse);

        if (beers == null || beers.Count == 0)
        {
            ViewBag.Message = "Não há cervejas registradas";
            return View();
        }

        return View(beers);
    }

    // GET: BeersController/Details/5
    public async Task<ActionResult> Details(int id)
    {
        string token = Request.Cookies["token"];

        if (token == null)
            return RedirectToAction("Index", "Login");

        HttpClient httpClient = new();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7205/api/Beers/{id}");

        string apiResponse = await response.Content.ReadAsStringAsync();

        var beer = JsonConvert.DeserializeObject<Beer>(apiResponse);

        if (beer == null)
        {
            return NotFound();
        }

        return View(beer);
    }

    // GET: BeersController/Create
    public async Task<ActionResult> Create()
    {
        string token = Request.Cookies["token"];

        if (token == null)
            return RedirectToAction("Index", "Login");

        HttpClient httpClient = new();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7205/api/styles");

        string apiResponse = await response.Content.ReadAsStringAsync();

        var styles = JsonConvert.DeserializeObject<List<Style>>(apiResponse);

        if (styles == null || styles.Count == 0)
        {
            ViewBag.Message = "Não há estilos registrados";
            return View();
        }

        ViewData["StyleId"] = new SelectList(styles, "StyleId", "Name");

        return View();
    }

    // POST: BeersController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(IFormCollection collection)
    {
        string token = Request.Cookies["token"];

        if (token == null)
            return RedirectToAction("Index", "Login");

        BeerDTO beer = CreateBeerDTOWithFormProps(collection);

        StringContent content = new(JsonConvert.SerializeObject(beer), Encoding.UTF8, "application/json");

        HttpClient httpClient = new();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await httpClient.PostAsync($"https://localhost:7205/api/Beers/", content);

        string apiResponse = await response.Content.ReadAsStringAsync();

        JsonConvert.DeserializeObject<CreatedAtActionResult>(apiResponse);

        return RedirectToAction("Index");
    }

    // GET: BeersController/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
        string token = Request.Cookies["token"];

        if (token == null)
            return RedirectToAction("Index", "Login");

        HttpClient httpClient = new();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await Task.WhenAll(
            httpClient.GetAsync($"https://localhost:7205/api/Beers/{id}"),
            httpClient.GetAsync($"https://localhost:7205/api/Styles")
        );

        var beerResponse = response[0];
        var stylesResponse = response[1];

        string apiBeerResponse = await beerResponse.Content.ReadAsStringAsync();
        string apiStylesResponse = await stylesResponse.Content.ReadAsStringAsync();

        var beer = JsonConvert.DeserializeObject<Beer>(apiBeerResponse);
        var styles = JsonConvert.DeserializeObject<List<Style>>(apiStylesResponse);

        if (beer == null)
        {
            return NotFound();
        }

        ViewData["StyleId"] = new SelectList(styles, "StyleId", "Name");

        return View(beer);
    }

    // POST: BeersController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, IFormCollection collection)
    {
        string token = Request.Cookies["token"];

        if (token == null)
            return RedirectToAction("Index", "Login");

        BeerDTO beer = CreateBeerDTOWithFormProps(collection);

        StringContent content = new(JsonConvert.SerializeObject(beer), Encoding.UTF8, "application/json");

        HttpClient httpClient = new();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.PutAsync($"https://localhost:7205/api/Beers/{id}", content);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        return View(beer);
    }

    // GET: BeersController/Delete/5
    public async Task<ActionResult> Delete(int id)
    {
        string token = Request.Cookies["token"];

        if (token == null)
            return RedirectToAction("Index", "Login");

        HttpClient httpClient = new();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7205/api/Beers/{id}");

        string apiResponse = await response.Content.ReadAsStringAsync();

        var beer = JsonConvert.DeserializeObject<Beer>(apiResponse);

        if (beer == null)
        {
            return NotFound();
        }

        return View(beer);
    }

    // POST: BeersController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id, IFormCollection collection)
    {
        string token = Request.Cookies["token"];

        if (token == null)
            return RedirectToAction("Index", "Login");

        HttpClient httpClient = new();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await httpClient.DeleteAsync($"https://localhost:7205/api/Beers/{id}");

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        BeerDTO beer = CreateBeerDTOWithFormProps(collection);

        return View(beer);
    }

    private static BeerDTO CreateBeerDTOWithFormProps(IFormCollection collection)
    {
        BeerDTO beer = new();
        beer.Name = collection["name"];
        beer.Price = int.Parse(collection["price"]);
        beer.ABV = float.Parse(collection["ABV"]);
        beer.IBU = int.Parse(collection["IBU"]);
        beer.StyleId = int.Parse(collection["StyleId"]);

        return beer;
    }
}
