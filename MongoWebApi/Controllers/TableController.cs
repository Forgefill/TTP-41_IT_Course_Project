using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.DatabaseEntities;
using DAL.Types;
using MongoWebApi.Services;
using MongoWebApi.Models;

namespace DatabaseWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private MongoDBManager dbManager { get; set; }

        public TableController(MongoDBManager dBManager)
        {
            dbManager = dBManager;
        }

        [HttpGet("{dbName}/{tableName}")]
        public ActionResult<List<Column>> GetTable(string dbName, string tableName)
        {
            try
            {
                return Ok(dbManager.GetAllColumns(dbName, tableName));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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


        [HttpDelete("{dbName}/{tableName}")]
        public ActionResult DeleteTable(string dbName, string tableName)
        {
            try
            {
                dbManager.DeleteTable(dbName, tableName);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{dbName}/{tableName}")]
        public ActionResult EditTableName(string dbName, string tableName, string newName)
        {
            try
            {
                dbManager.EditTableName(dbName, tableName, newName);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
