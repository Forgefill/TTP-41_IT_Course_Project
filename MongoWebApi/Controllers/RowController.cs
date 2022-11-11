using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.DatabaseEntities;
using DAL;
using MongoWebApi.Services;

namespace DatabaseWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RowController : ControllerBase
    {
        private MongoDBManager dbManager { get; set; }

        public RowController(MongoDBManager dBManager)
        {
            dbManager = dBManager;
        }


        [HttpGet("{dbName}/{tableName}/{rowId}")]
        public ActionResult<List<string>> GetRow(string dbName, string tableName, int rowId)
        {
            try
            {
                return Ok(dbManager.GetRow(dbName, tableName, rowId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{dbName}/{tableName}/{rowId}/{columnName}")]
        public ActionResult<string> GetRowItem(string dbName, string tableName,string columnName ,int rowId)
        {
            try
            {
                return Ok(dbManager.GetRowItem(dbName, tableName, columnName, rowId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{dbName}/{tableName}")]
        public ActionResult AddRow(string dbName, string tableName)
        {
            try
            {
                dbManager.AddRow(dbName, tableName);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{dbName}/{tableName}/{rowId}")]
        public ActionResult DeleteRow(string dbName, string tableName, int rowId)
        {
            try
            {
                dbManager.DeleteRow(dbName, tableName, rowId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{dbName}/{tableName}/deleteEquals")]
        public ActionResult DeleteEqualRows(string dbName, string tableName)
        {
            try
            {
                dbManager.DeleteEqualRows(dbName, tableName);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{dbName}/{tableName}/{rowId}/{columnName}")]
        public ActionResult EditRowItem(string dbName, string tableName,string columnName ,int rowId, string value)
        {
            try
            {
                dbManager.EditRowItem(dbName, tableName, columnName, rowId, value);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
