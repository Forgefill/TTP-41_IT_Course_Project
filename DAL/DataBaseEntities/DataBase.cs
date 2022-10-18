

using System.Xml.Linq;
using System.Linq;
using System.Xml.Serialization;

namespace DAL.DatabaseEntities
{
    public class Database
    {
        [XmlElement(ElementName ="DatabaseName")]
        public string Name { get; set; }
        
        [XmlElement()]
        public List<Table> Tables;

        public Database(string name)
        {
            Name = name;
            Tables = new List<Table>();
        }

        public Database() 
        {
            Tables = new List<Table>();
        }

        public Table GetTable(string name)
        {
            if (!Tables.Any(x => x.Name == name)) throw new Exception("Table " + name + " does not exist");
            return Tables.First(x=>x.Name == name);
        }

        public void RemoveEqualRows(string tableName)
        {
            Table table = GetTable(tableName);

            List<Row> Rows = table.Rows;
            List<Row> newRows = new List<Row>();
            
            for(int i = 0; i < Rows.Count; i++)
            {
                for(int j = 0;j < Rows.Count; j++)
                {
                    if (Rows[i] == Rows[j] && !Contain(Rows[i], newRows))
                    {
                        newRows.Add(Rows[i]);
                    }
                }
            }
            
            table.Rows = newRows;
        }

        private bool Contain(Row row, List<Row> rows)
        {
            for(int i = 0; i < rows.Count; i++)
            {
                if (row == rows[i]) return true;
            }

            return false;
        }
    }
}