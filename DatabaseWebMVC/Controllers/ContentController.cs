using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.DatabaseEntities;
using DatabaseWebMVC.Models;
using DAL.Types;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DatabaseWebMVC.Controllers
{
    public class ContentController : Controller
    {
        private DBManager context;

        public ContentController(DBManager context)
        {
            this.context = context;
        }

        public ActionResult Index(string dbName, string tableName)
        {
            try
            {
                Table table = context.GetTable(dbName, tableName);
                TableViewModel tableViewModel = new TableViewModel(dbName, tableName, table.columnTypes, table.Rows);
                return View(tableViewModel);
            }
            catch
            {
                return RedirectToAction("Index", "Table", new { dbName = dbName });
            }
        }

        public ActionResult EditRowItem(string dbName, string tableName, int rowId, int columnId)
        {
            ViewBag.type = context.GetColumnTypeById(dbName, tableName, columnId).typeName;
            if (ViewBag.type == "Enum")
            {
                EnumType type = (EnumType)context.GetColumnTypeById(dbName, tableName, columnId);
                ViewBag.enumTypes = type.enumValues;
                EditRowItemViewModel viewModel = new EditRowItemViewModel(dbName, tableName, rowId, columnId);
                return View(viewModel);
            }
            else
            {
                EditRowItemViewModel viewModel = new EditRowItemViewModel(dbName, tableName, rowId, columnId);
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRowItem(string dbName, string tableName, int rowId, int columnId, string newValue)
        {
            BaseType type = context.GetColumnTypeById(dbName, tableName, columnId);
            if (!type.isCorrect(newValue))
            {
                ModelState.AddModelError("", "Invalid input for column type, input rules: " + type.TypeRule());
                return View(new EditRowItemViewModel(dbName, tableName, rowId, columnId));
            }
            try
            {
                context.EditRowItem(dbName, tableName, columnId, rowId, newValue);
                context.SaveDB();
                return RedirectToAction(nameof(Index), new { dbName = dbName, tableName = tableName });
            }
            catch
            {
                return RedirectToAction(nameof(Index), new { dbName = dbName, tableName = tableName });
            }
        }

        public ActionResult EditColumn(string dbName, string tableName, int Id)
        {
            EditColumnViewModel columnViewModel = new EditColumnViewModel(dbName, tableName, "",Id);
            return View(columnViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditColumn(string dbName, string tableName, int Id, string newColumnName)
        {
            try
            {
                context.EditColumnName(dbName, tableName, Id, newColumnName);
                context.SaveDB();
                return RedirectToAction(nameof(Index), new { dbName = dbName, tableName = tableName });
            }
            catch
            {
                return RedirectToAction(nameof(Index), new { dbName = dbName, tableName = tableName });
            }
        }


        public ActionResult DeleteColumn(string dbName, string tableName, int columnId)
        {
            try
            {
                Table table = context.GetTable(dbName, tableName);
                BaseType type = table.columnTypes[columnId];
                ColumnViewModel columnViewModel = new ColumnViewModel(dbName, tableName, type.typeName, type.name, columnId);
                return View(columnViewModel);
            }
            catch
            {
                return RedirectToAction(nameof(Index), new { dbName = dbName, tableName = tableName });
            }
        }

        [HttpPost, ActionName("DeleteColumn")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteColumnConfirm(string dbName, string tableName, int Id)
        {
            try
            {
                context.DeleteColumn(dbName, tableName, Id);
                context.SaveDB();
                return RedirectToAction(nameof(Index), new { dbName = dbName, tableName = tableName });
            }
            catch
            {
                return RedirectToAction(nameof(Index), new { dbName = dbName, tableName = tableName });
            }
        }


        public ActionResult CreateColumn(string dbName, string tableName)
        {
            return View(new ColumnViewModel(dbName, tableName));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateColumn(string dbName, string tableName, string ColumnType, string Name, string enumTypes)
        {
            BaseType toAdd = new IntegerType(Name);
            switch(ColumnType)
            {
                case "Int":
                    toAdd = new IntegerType(Name);
                    break;
                case "Real":
                    toAdd = new RealType(Name);
                    break;
                case "String":
                    toAdd = new StringType(Name);
                    break;
                case "Char":
                    toAdd = new CharType(Name);
                    break;
                case "Email":
                    toAdd = new EmailType(Name);
                    break;
                case "Enum":
                    List<string> enums = enumTypes.Split(";").ToList();
                    enums.Remove("");
                    toAdd = new EnumType(Name, enums);
                    break;
            }

            try
            {
                context.AddColumn(dbName, tableName, toAdd);
                context.SaveDB();
                return RedirectToAction(nameof(Index), new { dbName = dbName, tableName = tableName });
            }
            catch
            {
                return RedirectToAction(nameof(Index), new { dbName = dbName, tableName = tableName });
            }
        }


        public ActionResult CreateRow(string dbName, string tableName)
        {
            try
            {
                context.AddRow(dbName, tableName);
                context.SaveDB();
                return RedirectToAction(nameof(Index), new {dbName = dbName, tableName = tableName});
            }
            catch
            {
                return RedirectToAction(nameof(Index), new { dbName = dbName, tableName = tableName });
            }
        }

        public ActionResult DeleteEqRows(string dbName, string tableName)
        {
            try
            {
                context.DeleteEqualRows(dbName, tableName);
                context.SaveDB();
                return RedirectToAction(nameof(Index), new { dbName = dbName, tableName = tableName });
            }
            catch
            {
                return RedirectToAction(nameof(Index), new { dbName = dbName, tableName = tableName });
            }
        }

        public ActionResult DeleteRow(string dbName, string tableName, int rowId)
        {
            try
            {
                context.DeleteRow(dbName, tableName, rowId);
                context.SaveDB();
                return RedirectToAction(nameof(Index), new { dbName = dbName, tableName = tableName });
            }
            catch
            {
                return RedirectToAction(nameof(Index), new { dbName = dbName, tableName = tableName });
            }
        }
    }
}
