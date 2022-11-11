using DAL.DatabaseEntities;

namespace DatabaseWebMVC.Models
{
    public class DatabaseViewModel
    {
        public string Name { get; set; }

        public List<string> TablesName = new List<string>();

        public DatabaseViewModel(Database database)
        {
            Name = database.Name;

            TablesName = database.Tables.Select(t => t.Name).ToList();
        }

        public DatabaseViewModel(string name)
        {
            Name = name;
        }
    }
}
