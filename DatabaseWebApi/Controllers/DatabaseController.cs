using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.DBFileManagers;
using DAL;
using DAL.DatabaseEntities;

namespace DatabaseWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseController : ControllerBase
    {

        private DBManager dbManager { get; set; }

        public DatabaseController(DBManager dBManager)
        {
            dbManager = dBManager;
        }

        [HttpGet]
        public ActionResult<List<string>> GetAllDBName()
        {
            return Ok(dbManager.GetDbsName());
        }

        [HttpPost("{dbName}")]
        public ActionResult PostDatabase(string dbName)
        {
            try
            {
                dbManager.AddDB(dbName);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{dbName}")]
        public ActionResult DeleteDatabase(string dbName)
        {
            try
            {
                dbManager.DeleteDB(dbName);
                return Ok(dbName);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

    }
}
