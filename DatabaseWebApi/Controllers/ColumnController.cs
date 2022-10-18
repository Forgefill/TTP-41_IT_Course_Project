using DAL;
using DAL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace DatabaseWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {

        private DBManager dbManager { get; set; }

        public ColumnController(DBManager dBManager)
        {
            dbManager = dBManager;
        }

        [HttpGet("{dbName}/{tableName}")]
        public ActionResult<List<BaseType>> GetAllColumnType(string dbName, string tableName)
        {
            try
            {
                return Ok(dbManager.GetAllColumnTypes(dbName, tableName));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpGet("{dbName}/{tableName}/{columnId}")]
        public ActionResult<BaseType> GetColumnTypeById(string dbName, string tableName, int columnId)
        {
            try
            {
                return Ok(dbManager.GetColumnTypeById(dbName, tableName, columnId));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        [HttpPost("{dbName}/{tableName}/{typeName}")]
        public ActionResult AddColumn(string dbName, string tableName, string typeName)
        {
            BaseType addValue = new IntegerType();

            switch(typeName)
            {
                case "Integer":
                    addValue = new IntegerType();
                    break;
                case "Char":
                    addValue = new CharType();
                    break;
                case "Real":
                    addValue = new RealType();
                    break;
                case "String":
                    addValue = new StringType();
                    break;
                case "Email":
                    addValue = new EmailType();
                    break;
            }

            try
            {
                dbManager.AddColumn(dbName, tableName, addValue);
                dbManager.SaveDB();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpPost("{dbName}/{tableName}/AddEnum")]
        public ActionResult AddEnumColumn(string dbName, string tableName, string[] Enums)
        {
            List<string> enums = new List<string>();

            for(int i = 0; i < Enums.Length; i++)
            {
                enums.Add(Enums[i]);
            }

            try
            {
                dbManager.AddColumn(dbName, tableName, new EnumType("Enum", enums));
                dbManager.SaveDB();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete(("{dbName}/{tableName}/{columnId}"))]
        public ActionResult DeleteColumn(string dbName, string tableName, int columnId)
        {
            try
            {
                dbManager.DeleteColumn(dbName, tableName, columnId);
                dbManager.SaveDB();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        [HttpPatch(("{dbName}/{tableName}/{columnId}"))]
        public ActionResult UpdateColumnName(string dbName, string tableName, int columnId, string newName)
        {
            try
            {
                dbManager.EditColumnName(dbName, tableName, columnId, newName);
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
