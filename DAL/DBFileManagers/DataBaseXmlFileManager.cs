using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DAL.DataBaseEntities;
using DAL.Types;

namespace DAL.DBFileManagers
{
    public class DataBaseXmlFileManager: DataBaseFileManager
    {
        public DataBaseXmlFileManager():base()
        {

        }

        public override void SaveDatabase(string path, DataBase _database)
        {
            
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DataBase), 
                new[] { typeof(CharType), typeof(IntegerType), typeof(EmailType), typeof(EnumType), typeof(RealType), typeof(StringType) });

            FileStream fcreate = File.Open(path, FileMode.Create);

            using (StreamWriter writer = new StreamWriter(fcreate))
            {
                xmlSerializer.Serialize(writer, _database);
            }
        }

        public override DataBase LoadDatabase(string FilePath)
        {

            DataBase result;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DataBase),
                new[] { typeof(Table), typeof(Row),typeof(CharType), typeof(IntegerType), typeof(EmailType), typeof(EnumType), typeof(RealType), typeof(StringType) });

            
            using (StreamReader reader = new StreamReader(FilePath))
            {
                result = (DataBase)xmlSerializer.Deserialize(reader);
            }

            return result;
        }
    }
}
