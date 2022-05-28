using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeersCatalog.Presentation.Controllers
{
    public class assadasdsa : Controller
    {
        // GET: assadasdsa
        public ActionResult Index()
        {
            return View();
        }

        // GET: assadasdsa/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: assadasdsa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: assadasdsa/Create
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

        // GET: assadasdsa/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: assadasdsa/Edit/5
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

        // GET: assadasdsa/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: assadasdsa/Delete/5
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
}
