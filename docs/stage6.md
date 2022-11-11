# Web Api with mongoDB

Для роботи з MongoDB розроблено тип даних [Column](https://github.com/Forgefill/TTP-41_IT_Course_Project/tree/master/MongoWebApi/Models), що має інформацію про тип даних в колонці, ім'я колонки, стандартне значення і елементи в колонці. Від нього наслідуються класи **EnumColumn**, **EmailColumn**, **StringColumn**, **CharColumn**, **IntegerColumn**, **RealColumn**, що є представленням відповідних типів даних, що зберігаються в базу даних MongoDB. До проекту завантажимо офіційний driver mongo для .NET, **MongoDB.Driver**. 
  
Встановлено фреймворк для роботи з JSON в .NET - **Newtonsoft**. Створено класи ColumnConverter i ColumnSpecifiedConcreteClassConverter, що серіалізують модель даних з наслідуванням в JSON, переглянити їх код можна за [посиланням.](https://github.com/Forgefill/TTP-41_IT_Course_Project/tree/master/MongoWebApi/JsonHelpers)

Створено сервіс MongoDBManager:

```C#
using DAL.DatabaseEntities;
using DAL.Types;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
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

        #region row

        public void AddRow(string dbName, string tableName)
        {
            IMongoCollection<Column> collection = _client.GetDatabase(dbName).GetCollection<Column>(tableName);

            List<Column> toUpdate = collection.AsQueryable().ToList();
            foreach (Column item in toUpdate)
            {
                item.elements.Add(item.DefValue);
                collection.ReplaceOneAsync(x => x.ColumnName == item.ColumnName, item);
            }
        }

        public void DeleteRow(string dbName, string tableName, int rowId)
        {
            IMongoCollection<Column> collection = _client.GetDatabase(dbName).GetCollection<Column>(tableName);

            List<Column> toUpdate = collection.AsQueryable().ToList();


            if (!anyColumnExist(dbName, tableName) || rowId + 1 > getRowsNum(dbName, tableName))
            {
                return;
            }

            foreach (Column item in toUpdate)
            {
                item.elements.RemoveAt(rowId);
                collection.ReplaceOneAsync(x => x.ColumnName == item.ColumnName, item);
            }
        }

        public void DeleteEqualRows(string dbName, string tableName)
        {
            IMongoCollection<Column> collection = _client.GetDatabase(dbName).GetCollection<Column>(tableName);

            List<Column> Columns = collection.AsQueryable().ToList();

            if (!anyColumnExist(dbName, tableName) ||  getRowsNum(dbName, tableName) < 2)
            {
                return;
            }

            int rowsCount = getRowsNum(dbName, tableName);

            HashSet<int> toDelete = new HashSet<int>();

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = i + 1; j < rowsCount; j++)
                {
                    if (equalRows(Columns, i, j))
                    {
                        toDelete.Add(j);
                    }
                }
            }

            List<int> res = toDelete.ToList().OrderByDescending(i => i).ToList();

            foreach(int i in res)
            {
                DeleteRow(dbName, tableName, i);
            }

            return;
        }

        public List<string> GetRow(string dbName, string tableName, int rowId)
        {
            IMongoCollection<Column> collection = _client.GetDatabase(dbName).GetCollection<Column>(tableName);

            List<Column> Columns = collection.AsQueryable().ToList();

            if (!anyColumnExist(dbName, tableName) || rowId + 1 > getRowsNum(dbName, tableName))
            {
                return null;
            }

            List<string> res = new List<string>();

            foreach(Column col in Columns)
            {
                res.Add(col.elements.ElementAt(rowId));
            }

            return res;
        }

        public string GetRowItem(string dbName, string tableName, string columnName, int rowId)
        {
            IMongoCollection<Column> collection = _client.GetDatabase(dbName).GetCollection<Column>(tableName);

            if (!anyColumnExist(dbName, tableName) || rowId + 1 > getRowsNum(dbName, tableName))
            {
                return null;
            }

            Column column = collection.Find(x => x.ColumnName == columnName).First();

            return column.elements.ElementAt(rowId);
        }

        public void EditRowItem(string dbName, string tableName, string columnName, int rowId, string value)
        {
            IMongoCollection<Column> collection = _client.GetDatabase(dbName).GetCollection<Column>(tableName);

            if (!anyColumnExist(dbName, tableName) || rowId + 1 > getRowsNum(dbName, tableName))
            {
                return;
            }

            Column column = collection.Find(x => x.ColumnName == columnName).First();

            if(!column.isCorrect(value))
            {
                throw new Exception("incorrect value.\nPls use next rules: " + column.TypeRule());
            }

            column.elements[rowId] = value;

            collection.ReplaceOne(x => x.ColumnName == columnName, column);
        }

        #endregion


        #region column

        public List<Column> GetAllColumns(string dbName, string tableName)
        {
            return _client.GetDatabase(dbName).GetCollection<Column>(tableName).AsQueryable().ToList();
        }

        public Column GetColumn(string dbName, string tableName, string columnName)
        {
            return _client.GetDatabase(dbName).GetCollection<Column>(tableName).Find(x => x.ColumnName == columnName).First();
        }

        public void AddColumn(string dbName, string tableName, string columnName, string typeName, List<string> enumValues)
        {
            IMongoCollection<Column> collection = _client.GetDatabase(dbName).GetCollection<Column>(tableName);

            if(collection.Find(x=>x.ColumnName == columnName).Any())
            {
                return;
            }

            int n = getRowsNum(dbName, tableName);

            Column toAdd = CreateColumn(columnName, typeName, enumValues);
            
            for(int i = 0; i < n; i++)
            {
                toAdd.elements.Add(toAdd.DefValue);
            }

            _client.GetDatabase(dbName).GetCollection<Column>(tableName).InsertOne(toAdd);
        }

        public void DeleteColumn(string dbName, string tableName, string columnName)
        {
            IMongoCollection<Column> collection = _client.GetDatabase(dbName).GetCollection<Column>(tableName);

            collection.DeleteOne(x => x.ColumnName == columnName);
            return;
        }

        #endregion

        #region Table

        public List<string> GetAllTableName(string dbName)
        {
            return _client.GetDatabase(dbName).ListCollectionNames().ToList();
        }

        public void DeleteTable(string dbName, string tableName)
        {
            _client.GetDatabase(dbName).DropCollection(tableName);
        }

        public void EditTableName(string dbName, string tableName, string newName)
        {
            _client.GetDatabase(dbName).RenameCollection(tableName, newName);
        }

        public string? GetAnyTableName(string dbName)
        {
            List<string> tables = _client.GetDatabase(dbName).ListCollectionNames().ToList();
            if(tables.Count > 0)
            {
                return tables[0];
            }
            else return null;
        }

        #endregion

        #region DB

        public void DeleteDB(string dbName)
        {
            _client.DropDatabase(dbName);
        }

        public List<string> GetDbsName()
        {
            List<string> res = _client.ListDatabaseNames().ToList();
            res = res.Where(x => x != "admin" && x != "config" && x != "local").ToList();
            return res;
        }

        #endregion

        private Column CreateColumn(string ColumnName, string Type, List<string> enumsValue = null)
        {
            Column res = new IntegerColumn();
            switch (Type)
            {
                case "Int":
                    res = new IntegerColumn(ColumnName);
                    break;
                case "Real":
                    res = new RealColumn(ColumnName);
                    break;
                case "Char":
                    res = new CharColumn(ColumnName);
                    break;
                case "Email":
                    res = new EmailColumn(ColumnName);
                    break;
                case "String":
                    res = new StringColumn(ColumnName);
                    break;
                case "Enum":
                    res = new EnumColumn(ColumnName, enumsValue);
                    break;
                default:
                    return null;
            }
            return res;
        }

        private int getRowsNum(string dbName, string tableName)
        {
            int res = 0;
            try
            {
                res = _client.GetDatabase(dbName).GetCollection<Column>(tableName).AsQueryable().First().elements.Count;
            }
            catch { }

            return res;
        }

        private bool anyColumnExist(string dbName, string tableName)
        {
            IMongoCollection<Column> collection = _client.GetDatabase(dbName).GetCollection<Column>(tableName);

            List<Column> Columns = collection.AsQueryable().ToList();

            if (Columns.Count < 1) return false;
            else return true;
        }

        private bool equalRows(List<Column> columns, int k, int s)
        {
            bool result = true;
            for (int i = 0; i < columns.Count; i++)
            {
                result = result && (columns[i].elements[k] == columns[i].elements[s]);
            }
            return result;
        }
    }
}
```
