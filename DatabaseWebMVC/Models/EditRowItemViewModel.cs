namespace DatabaseWebMVC.Models
{
    public class EditRowItemViewModel
    {
        public string dbName { get; set; }
        public string tableName { get; set; }
        public int rowId { get; set; }
        public int columnId { get; set; }
        public string newValue { get; set; }

        public EditRowItemViewModel(string dbName, string tableName, int rowId, int columnId)
        {
            this.dbName = dbName;
            this.tableName = tableName;
            this.rowId = rowId;
            this.columnId = columnId;
        }
    }
}
