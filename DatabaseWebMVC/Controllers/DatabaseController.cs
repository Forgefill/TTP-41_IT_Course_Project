using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DatabaseWebMVC.Models;
using DAL.DatabaseEntities;

namespace DatabaseWebMVC.Controllers
{
    public class DatabaseController : Controller
    {
        private DBManager context;

        public DatabaseController(DBManager _context)
        {
            context = _context;
        }

        public ActionResult Index()
        {
            return View(context.GetDbsName());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Name)
        {
            try
            {
                context.AddDB(Name);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string Name)
        {
            return View(new DatabaseViewModel(Name));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string Name)
        {
            try
            {
                context.DeleteDB(Name);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(nameof(Delete), new DatabaseViewModel(Name));
            }
        }
    }
}
