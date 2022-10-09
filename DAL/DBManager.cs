using DAL.DataBaseEntities;
using DAL.DBFileManagers;
using DAL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    public class DBManager
    {
        private DataBase database;
        private string dbPath = "C:\\Users\\bubka\\source\\repos\\LocalDB\\DAL\\Src";
        private DataBaseFileManager FileManager;
        

        public DBManager(string dbName)
        {
            database = new DataBase(dbName);
            FileManager = new DataBaseXmlFileManager();
            dbPath = dbPath + "\\" + dbName + ".xml";
        }

        public DBManager()
        {
            dbPath = "C:\\Users\\bubka\\source\\repos\\LocalDB\\DAL\\Src\\Cache.xml";
            FileManager = new DataBaseXmlFileManager();
        }

        public Table GetTable(string tableName)
        {
            return database.GetTable(tableName);
        }
        public BaseType GetColumnTypeById(string tableName, int columnId)
        {
            return database.GetTable(tableName).columnTypes[columnId];
        }
        public string? GetAnyTableNameOrNull()
        {
            if (database.Tables.Count > 0)
            {
                return database.Tables[0].Name;
            }
            else return null;
        }
        public void AddTable(string tableName)
        {
            if (!database.Tables.Any(x => x.Name == tableName))
            {
                database.Tables.Add(new Table(tableName));
            }
        }
        public void DeleteTable(string tableName)
        {
            database.Tables.Remove(database.GetTable(tableName));
        }
        public void EditTableName(string tableName, string value)
        {
            database.GetTable(tableName).Name = value;
        }


        public void DeleteRow(string tableName, int id)
        {
            database.GetTable(tableName).Rows.RemoveAt(id);
        }
        public void DeleteEqualRows(string tableName)
        {
            database.RemoveEqualRows(tableName);
        }
        public void AddRow(string tableName)
        {
            Table table = database.GetTable(tableName);
            List<string> elements = new List<string>();

            foreach(BaseType a in table.columnTypes)
            {
                elements.Add(a.defValue);
            }

            table.Rows.Add(new Row(elements));
        }
        public void EditRowItem(string tableName, int ColumnId, int RowId, string value)
        {
            Table table = database.GetTable(tableName);
            if (table.columnTypes[ColumnId].isCorrect(value))
            {
                database.GetTable(tableName).Rows[RowId][ColumnId] = value;
            }
            
        }
        public string GetRowItem(string tableName, int ColumnId, int RowId)
        {
            return database.GetTable(tableName).Rows[RowId][ColumnId];
        }


        public void AddColumn(string tableName, BaseType type)
        {
            Table t = database.GetTable(tableName);
            t.columnTypes.Add(type);

            foreach (Row s in t.Rows)
            {
                s.elements.Add(type.defValue);
            }
        }
        public void DeleteColumn(string tableName, int ColumnId)
        {
            Table table = database.GetTable(tableName);

            table.columnTypes.RemoveAt(ColumnId);
            foreach(var a in table.Rows)
            {
                a.elements.RemoveAt(ColumnId);
            }
        }
        public void EditColumnName(string tableName, int ColumnId, string value)
        {
            Table table = database.GetTable(tableName);
            table.columnTypes[ColumnId].name = value;
        }

        public void SaveDb(string path)
        {
            FileManager.SaveDatabase(path, database);
        }
        public void LoadDb(string filePath)
        {
            dbPath = filePath;
            database = FileManager.LoadDatabase(filePath);
        }
        public void AddNewDB(string name, string path)
        { 
            dbPath = path + "\\" + name + ".xml";
            database = new DataBase(name);
        }
        public string GetDbPath()
        {
            return dbPath;
        }
        public void SetDbPath(string newPath)
        {
            dbPath = newPath;
        }
        public string GetDbName()
        {
            return database.Name;
        }


        public List<string> GetTablesName()
        {
            return database.Tables.Select(x => x.Name).ToList();    
        }
    }
}
