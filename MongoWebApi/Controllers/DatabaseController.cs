using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.DBFileManagers;
using MongoWebApi.Services;

namespace DatabaseWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseController : ControllerBase
    {

        private MongoDBManager dbManager { get; set; }

        public DatabaseController(MongoDBManager dBManager)
        {
            dbManager = dBManager;
        }

        [HttpGet]
        public ActionResult<List<string>> GetAllDBName()
        {
            return Ok(dbManager.GetDbsName());
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
        }

    }
}
