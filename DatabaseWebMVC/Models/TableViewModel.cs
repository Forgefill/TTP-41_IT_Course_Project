using DAL.Types;
using DAL.DatabaseEntities;

namespace DatabaseWebMVC.Models
{
    public class TableViewModel
    {
        public string dbName { get; set; }

        public string tableName { get; set; }

        public List<BaseType>? columnTypes;

        public List<List<string>>? elements;

        public TableViewModel(string dbName, string tableName)
        {
            this.dbName = dbName;
            this.tableName = tableName;
        }

        public TableViewModel(string dbName, string tableName, List<BaseType> baseTypes, List<Row> rows)
        {
            this.dbName = dbName;
            this.tableName = tableName;
            columnTypes = baseTypes;

            elements = new List<List<string>>();
            for(int i = 0; i < rows.Count; i++)
            {
                elements.Add(rows.ElementAt(i).elements);
            }
        }
    }
}
