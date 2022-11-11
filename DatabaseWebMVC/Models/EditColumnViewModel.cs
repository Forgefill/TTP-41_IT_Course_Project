namespace DatabaseWebMVC.Models
{
    public class EditColumnViewModel
    {
        public int Id { get; set; }

        public string dbName { get; set; }

        public string tableName { get; set; }

        public string newColumnName { get; set; }

        public EditColumnViewModel(string dbName, string tableName, string newName, int id)
        {
            this.dbName = dbName;
            this.tableName = tableName;
            this.newColumnName = newName;
            Id = id;
        }
    }
}
