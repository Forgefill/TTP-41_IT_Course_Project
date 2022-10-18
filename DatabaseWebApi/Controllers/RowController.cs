using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.DatabaseEntities;
using DAL;

namespace DatabaseWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RowController : ControllerBase
    {
        private DBManager dbManager { get; set; }

        public RowController(DBManager dBManager)
        {
            dbManager = dBManager;
        }


        [HttpGet("{dbName}/{tableName}/{rowId}")]
        public ActionResult<Row> GetRow(string dbName, string tableName, int rowId)
        {
            try
            {
                return Ok(dbManager.GetRow(dbName, tableName, rowId).elements);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpGet("{dbName}/{tableName}/{rowId}/{columnId}")]
        public ActionResult<string> GetRowItem(string dbName, string tableName, int rowId, int columnId)
        {
            try
            {
                return Ok(dbManager.GetRowItem(dbName, tableName, columnId, rowId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpPost("{dbName}/{tableName}")]
        public ActionResult AddRow(string dbName, string tableName)
        {
            try
            {
                dbManager.AddRow(dbName, tableName);
                dbManager.SaveDB();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{dbName}/{tableName}/{rowId}")]
        public ActionResult DeleteRow(string dbName, string tableName, int rowId)
        {
            try
            {
                dbManager.DeleteRow(dbName, tableName, rowId);
                dbManager.SaveDB();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{dbName}/{tableName}/deleteEquals")]
        public ActionResult DeleteEqualRows(string dbName, string tableName)
        {
            try
            {
                dbManager.DeleteEqualRows(dbName, tableName);
                dbManager.SaveDB();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpPatch("{dbName}/{tableName}/{rowId}/{columnId}")]
        public ActionResult EditRowItem(string dbName, string tableName, int rowId, int columnId, string value)
        {
            try
            {
                dbManager.EditRowItem(dbName, tableName, columnId, rowId, value);
                dbManager.SaveDB();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

    }
}
