using DAL.DatabaseEntities;
using DAL.DBFileManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DatabaseEntities;
using DAL.DBFileManagers;
using DAL.Types;

namespace DAL
{
    public class DBManager
    {

        private Database currentDatabase;
        private DatabaseFileManager fileManager;

        public DBManager()
        {
            fileManager = new DatabaseXmlFileManager();
            currentDatabase = new Database();
        }


        #region row

        public void AddRow(string dbName, string tableName)
        {
            setDB(dbName);
            Table table = currentDatabase.GetTable(tableName);
            List<string> res = new List<string>();

            foreach (BaseType a in table.columnTypes)
            {
                res.Add(a.defValue);
            }

            table.Rows.Add(new Row(res));
        }

        public void DeleteRow(string dbName, string tableName, int rowId)
        {
            setDB(dbName);
            currentDatabase.GetTable(tableName).Rows.RemoveAt(rowId);
        }

        public void DeleteEqualRows(string dbName, string tableName)
        {
            setDB(dbName);
            currentDatabase.RemoveEqualRows(tableName);
        }

        public Row GetRow(string dbName, string tableName, int rowId)
        {
            setDB(dbName);
            return currentDatabase.GetTable(tableName).Rows[rowId];
        }

        public string GetRowItem(string dbName, string tableName, int ColumnId, int RowId)
        {
            setDB(dbName);
            return currentDatabase.GetTable(tableName).Rows[RowId][ColumnId];
        }

        public void EditRowItem(string dbName, string tableName, int ColumnId, int RowId, string value)
        {
            setDB(dbName);
            Table table = currentDatabase.GetTable(tableName);
            if (table.columnTypes[ColumnId].isCorrect(value))
            {
                currentDatabase.GetTable(tableName).Rows[RowId][ColumnId] = value;
            }
        }

        #endregion


        #region column

        public List<BaseType> GetAllColumnTypes(string dbName, string tableName)
        {
            setDB(dbName);
            return currentDatabase.GetTable(tableName).columnTypes;
        }

        public BaseType GetColumnTypeById(string dbName, string tableName, int columnId)
        {
            setDB(dbName);
            return currentDatabase.GetTable(tableName).columnTypes[columnId];
        }

        public void AddColumn(string dbName, string tableName, BaseType type)
        {
            setDB(dbName);
            Table t = currentDatabase.GetTable(tableName);
            t.columnTypes.Add(type);

            foreach (Row s in t.Rows)
            {
                s.elements.Add(type.defValue);
            }
        }

        public void DeleteColumn(string dbName, string tableName, int columnId)
        {
            setDB(dbName);
            Table table = currentDatabase.GetTable(tableName);

            table.columnTypes.RemoveAt(columnId);
            foreach (var a in table.Rows)
            {
                a.elements.RemoveAt(columnId);
            }
        }

        public void EditColumnName(string dbName, string tableName, int columnId, string value)
        {
            setDB(dbName);
            Table table = currentDatabase.GetTable(tableName);

            table.columnTypes[columnId].name = value;
        }

        #endregion

        #region Table

        public List<string> GetAllTableName(string dbName)
        {
            setDB(dbName);
            return currentDatabase.Tables.Select(x => x.Name).ToList();
        }

        public Table GetTable(string dbName, string tableName)
        {
            setDB(dbName);
            return currentDatabase.GetTable(tableName);
        }

        public void AddTable(string dbName, string tableName)
        {
            setDB(dbName);
            if (!tableExist(tableName))
            {
                currentDatabase.Tables.Add(new Table(tableName));
            }
        }

        public void DeleteTable(string dbName, string tableName)
        {
            setDB(dbName);
            currentDatabase.Tables.Remove(currentDatabase.GetTable(tableName));
        }

        public void EditTableName(string dbName, string tableName, string newName)
        {
            setDB(dbName);
            if (!tableExist(newName))
            {
                currentDatabase.GetTable(tableName).Name = newName;
            }
            else
            {
                throw new Exception("Table with name " + newName + " already exist");
            }
        }

        public string? GetAnyTableName(string dbName)
        {
            setDB(dbName);
            if (currentDatabase.Tables.Count > 0)
            {
                return currentDatabase.Tables[0].Name;
            }
            else return null;
        }

        #endregion

        #region DB

        public Database GetDB(string dbName)
        {
            setDB(dbName);
            return currentDatabase;
        }

        public void DeleteDB(string dbName)
        {
            fileManager.DeleteDatabase(dbName);
        }

        public void AddDB(string dbName)
        {
            if (!dbExist(dbName))
            {
                fileManager.SaveDatabase(new Database(dbName));
            }
        }

        public void SaveDB()
        {
            fileManager.SaveDatabase(currentDatabase);
        }

        public List<string> GetDbsName()
        {
            return fileManager.GetDatabasesName();
        }

        #endregion

        private bool tableExist(string tableName)
        {
            if (currentDatabase.Tables.Any(x => x.Name == tableName)) return true;
            else return false;
        }

        private bool dbExist(string dbName)
        {
            if (fileManager.GetDatabasesName().Any(x => x == dbName)) return true;
            else return false;
        }

        private void setDB(string dbName)
        {
            if (!dbExist(dbName))
            {
                throw new Exception("Database " + dbName + " does not exist");
            }
            else if (currentDatabase.Name == dbName)
            {
                return;
            }
            else
            {
                if (currentDatabase.Name != null) fileManager.SaveDatabase(currentDatabase);
                currentDatabase = fileManager.LoadDatabase(dbName);
            }
        }

        ~DBManager()
        {
            SaveDB();
        }
    }
}