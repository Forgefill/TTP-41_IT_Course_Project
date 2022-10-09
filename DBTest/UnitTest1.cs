using DAL;
using DAL.DataBaseEntities;
using DAL.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DBTest
{
    [TestClass]
    public class DBUnitTest
    {
        [TestMethod]
        public void TestAddDelete()
        {
            string dbName = "dbName";
            string tableName = "tableName";
            string tableToDelete = "randomName";

            DBManager dbManager = new DBManager(dbName);
            dbManager.AddNewDB(dbName, "anyPath");
            dbManager.AddTable(tableName);
            dbManager.AddTable(tableToDelete);
            dbManager.AddColumn(tableName, new EmailType());
            dbManager.AddColumn(tableName, new IntegerType());
            dbManager.AddRow(tableName);
            dbManager.AddRow(tableName);

            Assert.AreEqual(dbManager.GetDbName(), dbName);
            Assert.AreEqual(dbManager.GetTablesName().Count, 2);
            Assert.AreEqual(dbManager.GetTable(tableName).columnTypes.Count, 2);

            dbManager.DeleteTable(tableToDelete);
            dbManager.DeleteColumn(tableName, 1);
            dbManager.DeleteRow(tableName, 1);

            Assert.AreEqual(dbManager.GetTablesName().Count, 1);
            Assert.AreEqual(dbManager.GetTable(tableName).columnTypes.Count, 1);
            Assert.AreEqual(dbManager.GetTable(tableName).Rows.Count, 1);

            string newTableName = "newTestTableName";

            dbManager.EditTableName(tableName, newTableName);

            Assert.AreEqual(dbManager.GetTablesName()[0], newTableName);
        }

        [TestMethod]
        public void TestRemoveEqualMethod()
        {
            string addedTable = "testTable";

            DBManager dbManager = new DBManager();
            dbManager.AddNewDB(addedTable, "anyPath");
            dbManager.AddTable(addedTable);
            dbManager.AddColumn(addedTable, new EmailType());
            dbManager.AddColumn(addedTable, new IntegerType());
            dbManager.AddColumn(addedTable, new CharType());
            dbManager.AddRow(addedTable);
            dbManager.AddRow(addedTable);
            dbManager.AddRow(addedTable);

            int exEqRowCount = 3;
            Assert.AreEqual(dbManager.GetTable(addedTable).Rows.Count, exEqRowCount);

            dbManager.DeleteEqualRows(addedTable);

            int exNEqRowCount = 1;
            Assert.AreEqual(dbManager.GetTable(addedTable).Rows.Count, exNEqRowCount);
        }

        [TestMethod]
        public void TestSaveLoadDB()
        {
            string dbName = "TestDB";
            string addedTable = "testTable";
            string savePath = "C:\\Users\\bubka\\source\\repos\\LocalDB\\DAL\\Src\\postTestDB.xml";

            DBManager saveDbManager = new DBManager();
            saveDbManager.AddNewDB(dbName, savePath);
            saveDbManager.AddTable(addedTable);
            saveDbManager.AddColumn(addedTable, new IntegerType());
            saveDbManager.AddColumn(addedTable, new CharType());
            saveDbManager.AddRow(addedTable);
            saveDbManager.EditRowItem(addedTable, 0, 0, "100");
            saveDbManager.EditRowItem(addedTable, 1, 0, "a");
            saveDbManager.SaveDb(savePath);

            DBManager loadDbManager = new DBManager();
            loadDbManager.LoadDb(savePath);

            Assert.AreEqual(loadDbManager.GetDbName(), saveDbManager.GetDbName());
            CollectionAssert.AreEqual(loadDbManager.GetTablesName(), saveDbManager.GetTablesName());
            Assert.AreEqual(loadDbManager.GetRowItem(addedTable, 0, 0), loadDbManager.GetRowItem(addedTable, 0, 0));
        }

        
    }
}