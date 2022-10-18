using DAL.DatabaseEntities;
using DAL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace DAL.DBFileManagers
{
    public class DatabaseXmlFileManager : DatabaseFileManager
    {
        public DatabaseXmlFileManager() : base()
        {
        }

        private string dirPath = "C:\\Users\\bubka\\source\\repos\\LocalDB\\DAL\\Src";

        public override void SaveDatabase(Database _database)
        {

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Database),
                new[] { typeof(CharType), typeof(IntegerType), typeof(EmailType), typeof(EnumType), typeof(RealType), typeof(StringType) });

            FileStream fcreate = File.Open(getDbPath(_database.Name), FileMode.Create);

            using (StreamWriter writer = new StreamWriter(fcreate))
            {
                xmlSerializer.Serialize(writer, _database);
            }
        }

        public override Database LoadDatabase(string dbName)
        {
            Database result;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Database),
                new[] { typeof(Table), typeof(Row), typeof(CharType), typeof(IntegerType), typeof(EmailType), typeof(EnumType), typeof(RealType), typeof(StringType) });


            using (StreamReader reader = new StreamReader(getDbPath(dbName)))
            {
                result = (Database)xmlSerializer.Deserialize(reader);
            }
            
            return result;
        }

        public override void DeleteDatabase(string dbName)
        {
            string filePath = getDbPath(dbName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }


        public override List<string> GetDatabasesName()
        {
            List<string> res = new List<string>();

            DirectoryInfo d = new DirectoryInfo(dirPath);


            FileInfo[] Files = d.GetFiles("*.xml");

            foreach (FileInfo file in Files)
            {
                res.Add(Path.GetFileNameWithoutExtension(file.Name));
            }

            return res;
        }

        private string getDbPath(string dbName)
        {
            return dirPath + "\\" + dbName + ".xml";
        }
    }
}
