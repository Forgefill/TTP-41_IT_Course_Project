using DAL.DatabaseEntities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace DAL.DBFileManagers
{
    public abstract class DatabaseFileManager
    {
        public DatabaseFileManager() { }

        public abstract void SaveDatabase(Database _database);

        public abstract Database LoadDatabase(string dbName);

        public abstract void DeleteDatabase(string dbName);

        public abstract List<string> GetDatabasesName();
    }
}
