namespace DatabaseWebMVC.Models
{
    public class ColumnViewModel
    {
        public int Id { get; set; }

        public string dbName { get; set; }

        public string tableName { get; set; }

        public string ColumnType { get; set; }

        public string Name { get; set; }

        public List<string> availableTypes = new List<string>() { "Int", "String", "Char", "Email", "Real", "Enum"};

        public string enumTypes { get; set; } 

        public ColumnViewModel(string dbName, string tableName, string columnType, string name)
        {
            this.dbName = dbName;
            this.tableName = tableName;
            ColumnType = columnType;
            Name = name;
        }

        public ColumnViewModel(string dbName, string tableName)
        {
            this.dbName = dbName;
            this.tableName = tableName;
        }

        public ColumnViewModel(string dbName, string tableName, string columnType, string name, int columnId)
        {
            this.dbName = dbName;
            this.tableName = tableName;
            ColumnType = columnType;
            Name = name;
            this.Id = columnId;
        }
    }
}
