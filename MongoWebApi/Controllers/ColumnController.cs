using DAL;
using DAL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoWebApi.Services;
using System.Security.Cryptography.X509Certificates;
using MongoWebApi.Models;
namespace DatabaseWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {

        private MongoDBManager dbManager { get; set; }

        public ColumnController(MongoDBManager dBManager)
        {
            dbManager = dBManager;
        }

        [HttpGet("{dbName}/{tableName}")]
        public ActionResult<List<Column>> GetAllColumns(string dbName, string tableName)
        {
            try
            {
                List<Column> columns = dbManager.GetAllColumns(dbName, tableName);
                return Ok(columns);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{dbName}/{tableName}/{columnName}")]
        public ActionResult<BaseType> GetColumnTypeById(string dbName, string tableName, string columnName)
        {
            try
            {
                return Ok(dbManager.GetColumn(dbName, tableName, columnName));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{dbName}/{tableName}/{typeName}")]
        public ActionResult AddColumn(string dbName, string tableName, string columnName , string typeName)
        {
            try
            {
                dbManager.AddColumn(dbName, tableName, columnName, typeName, null);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{dbName}/{tableName}/AddEnum")]
        public ActionResult AddEnumColumn(string dbName, string tableName, string columnName, string[] Enums)
        {
            try
            {
                dbManager.AddColumn(dbName, tableName, columnName, "Enum", new List<string>(Enums));
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete(("{dbName}/{tableName}/{columnName}"))]
        public ActionResult DeleteColumn(string dbName, string tableName, string columnName)
        {
            try
            {
                dbManager.DeleteColumn(dbName, tableName, columnName);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPatch(("{dbName}/{tableName}/{columnName}"))]
        //public ActionResult UpdateColumnName(string dbName, string tableName, string columnName, string newName)
        //{
        //    try
        //    {
        //        dbManager.EditColumnName(dbName, tableName, columnName, newName);
        //        return Ok();
        //    }
        //    catch(Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

    }
}
