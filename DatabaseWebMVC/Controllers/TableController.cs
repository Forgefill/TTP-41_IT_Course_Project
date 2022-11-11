using DAL;
using DAL.DatabaseEntities;
using DatabaseWebMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseWebMVC.Controllers
{
    public class TableController : Controller
    {
        private DBManager context;

        public TableController(DBManager _context)
        {
            context = _context;
        }

        public ActionResult Index(string dbName)
        {
            Database database =  context.GetDB(dbName);
            return View(new DatabaseViewModel(database));
        }

        public ActionResult Details(string dbName, string tableName)
        {
            return RedirectToAction("Index", "Content", new {dbName = dbName, tableName = tableName});
        }

        public ActionResult Create(string dbName)
        {
            return View(new TableViewModel(dbName, null));
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateConfirm(string dbName, string tableName)
        {
            try
            {
                context.AddTable(dbName, tableName);
                context.SaveDB();
                return RedirectToAction(nameof(Index),  new { dbName = dbName});
            }
            catch
            {
                return View(nameof(Create), new {dbName = dbName});
            }
        }

        public ActionResult Edit(string dbName, string tableName)
        {
            return View(new EditTableViewModel(dbName, tableName, null));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string dbName, string tableName, string newTableName)
        {
            try
            {
                context.EditTableName(dbName, tableName, newTableName);
                context.SaveDB();
                return RedirectToAction(nameof(Index), new {dbName=dbName});
            }
            catch
            {
                return View(nameof(Edit), new TableViewModel(dbName, tableName));
            }
        }


        public ActionResult Delete(string dbName, string tableName)
        {
            return View(new TableViewModel(dbName, tableName));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(string dbName, string tableName)
        {
            try
            {
                context.DeleteTable(dbName, tableName);
                context.SaveDB();
                return RedirectToAction(nameof(Index), new {dbName = dbName});
            }
            catch
            {
                return View(nameof(Delete), new TableViewModel(dbName, tableName));
            }
        }
    }
}
