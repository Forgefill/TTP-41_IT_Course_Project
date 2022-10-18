using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.DatabaseEntities;
using DAL.Types;

namespace DatabaseWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private DBManager dbManager { get; set; }

        public TableController(DBManager dBManager)
        {
            dbManager = dBManager;
        }

        [HttpGet("{dbName}/{tableName}")]
        public ActionResult<Table> GetTable(string dbName, string tableName)
        {
            try
            {
                Table table = dbManager.GetTable(dbName, tableName);
                List<string> columnTypes = new List<string>();
                List<List<string>> rows = new List<List<string>>();

                foreach(BaseType a in table.columnTypes)
                {
                    columnTypes.Add(a.typeName);
                }

                foreach(Row a in table.Rows)
                {
                    rows.Add(a.elements);
                }

                var res =new { Name = table.Name, columnTypes = columnTypes, rows = rows };

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpGet("{dbName}/Tables")]
        public ActionResult<List<string>> GetAllTableName(string dbName)
        {
            try
            {
                return Ok(dbManager.GetAllTableName(dbName));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpPost("{dbName}/{tableName}")]
        public ActionResult CreateTable(string dbName, string tableName)
        {
            try
            {
                dbManager.AddTable(dbName, tableName);
                dbManager.SaveDB();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{dbName}/{tableName}")]
        public ActionResult DeleteTable(string dbName, string tableName)
        {
            try
            {
                dbManager.DeleteTable(dbName, tableName);
                dbManager.SaveDB();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpPatch("{dbName}/{tableName}")]
        public ActionResult EditTableName(string dbName, string tableName, string newName)
        {
            try
            {
                dbManager.EditTableName(dbName, tableName, newName);
                dbManager.SaveDB();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

    }
}
