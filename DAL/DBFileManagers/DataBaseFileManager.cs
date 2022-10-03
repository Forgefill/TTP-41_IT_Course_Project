using DAL.DataBaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DAL.DBFileManagers
{
    public abstract class DataBaseFileManager
    {
        public DataBaseFileManager(){}

        public abstract void SaveDatabase(string path, DataBase _database);

        public abstract DataBase LoadDatabase(string path);
    }
}
