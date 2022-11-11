# Web Api with mongoDB

Для роботи з MongoDB розроблено тип даних [Column](https://github.com/Forgefill/TTP-41_IT_Course_Project/tree/master/MongoWebApi/Models), що має інформацію про тип даних в колонці, ім'я колонки, стандартне значення і елементи в колонці. Від нього наслідуються класи **EnumColumn**, **EmailColumn**, **StringColumn**, **CharColumn**, **IntegerColumn**, **RealColumn**, що є представленням відповідних типів даних, що зберігаються в базу даних MongoDB. До проекту завантажимо офіційний driver mongo для .NET, **MongoDB.Driver**. 
  
Встановлено фреймворк для роботи з JSON в .NET - **Newtonsoft**. Створено класи ColumnConverter i ColumnSpecifiedConcreteClassConverter, що серіалізують модель даних з наслідуванням в JSON, переглянити їх код можна за [посиланням.](https://github.com/Forgefill/TTP-41_IT_Course_Project/tree/master/MongoWebApi/JsonHelpers)

Створено сервіс MongoDBManager:

```C#
using MongoDB.Driver;
using MongoWebApi.Models;
namespace MongoWebApi.Services
{
    public class MongoDBManager
    {
        MongoClient _client;

        public MongoDBManager()
        {
            _client = new MongoClient("mongodb://localhost:27017");
        }

        public void AddRow(string dbName, string tableName);

        public void DeleteRow(string dbName, string tableName, int rowId);

        public void DeleteEqualRows(string dbName, string tableName);

        public List<string> GetRow(string dbName, string tableName, int rowId);
        
        public string GetRowItem(string dbName, string tableName, string columnName, int rowId);

        public void EditRowItem(string dbName, string tableName, string columnName, int rowId, string value);

        public List<Column> GetAllColumns(string dbName, string tableName);

        public Column GetColumn(string dbName, string tableName, string columnName);

        public void AddColumn(string dbName, string tableName, string columnName, string typeName, List<string> enumValues);

        public void DeleteColumn(string dbName, string tableName, string columnName);

        public List<string> GetAllTableName(string dbName);

        public void DeleteTable(string dbName, string tableName);

        public void EditTableName(string dbName, string tableName, string newName);

        public string? GetAnyTableName(string dbName);
        
        public void DeleteDB(string dbName);

        public List<string> GetDbsName();
        
        private Column CreateColumn(string ColumnName, string Type, List<string> enumsValue = null);

        private int getRowsNum(string dbName, string tableName);

        private bool anyColumnExist(string dbName, string tableName);

        private bool equalRows(List<Column> columns, int k, int s);
    }
}
```
