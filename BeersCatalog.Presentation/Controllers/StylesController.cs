using BeersCatalog.BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BeersCatalog.Presentation.Controllers;

public class StylesController : Controller
{
    // GET: StylesController
    public async Task<ActionResult> Index()
    {
        string token = Request.Cookies["token"];

        HttpClient httpClient = new();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7205/api/styles");

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
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: StylesController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: StylesController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: StylesController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: StylesController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: StylesController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: StylesController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
