namespace DatabaseWebMVC.Models
{
    public class EditTableViewModel
    {
        public string dbName { get; set; }

        public string tableName { get; set; }

        public string newTableName { get; set; }

        public EditTableViewModel(string dbName, string tableName, string newTableName)
        {
            this.dbName = dbName;
            this.tableName = tableName;
            this.newTableName = newTableName;
        }
    }
}
