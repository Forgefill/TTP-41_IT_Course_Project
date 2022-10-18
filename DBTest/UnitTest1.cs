using DAL;
using DAL.DatabaseEntities;
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

            DBManager dbManager = new DBManager();
            dbManager.AddDB(dbName);
            dbManager.AddTable(dbName, tableName);
            dbManager.AddTable(dbName, tableToDelete);
            dbManager.AddColumn(dbName, tableName, new EmailType());
            dbManager.AddColumn(dbName, tableName, new IntegerType());
            dbManager.AddRow(dbName, tableName);
            dbManager.AddRow(dbName, tableName);

            Assert.AreEqual(dbManager.GetDbsName().Where(x => x == dbName), dbName);
            Assert.AreEqual(dbManager.GetAllTableName(dbName).Count, 2);
            Assert.AreEqual(dbManager.GetTable(dbName, tableName).columnTypes.Count, 2);

            dbManager.DeleteTable(dbName, tableToDelete);
            dbManager.DeleteColumn(dbName, tableName, 1);
            dbManager.DeleteRow(dbName, tableName, 1);

            Assert.AreEqual(dbManager.GetAllTableName(dbName).Count, 1);
            Assert.AreEqual(dbManager.GetTable(dbName, tableName).columnTypes.Count, 1);
            Assert.AreEqual(dbManager.GetTable(dbName, tableName).Rows.Count, 1);

            string newTableName = "newTestTableName";

            dbManager.EditTableName(dbName, tableName, newTableName);

            Assert.AreEqual(dbManager.GetAllTableName(dbName)[0], newTableName);
        }

        [TestMethod]
        public void TestRemoveEqualMethod()
        {
            string dbName = "dbName";
            string addedTable = "testTable";

            DBManager dbManager = new DBManager();
            dbManager.AddDB(dbName);
            dbManager.AddTable(dbName, addedTable);
            dbManager.AddColumn(dbName, addedTable, new EmailType());
            dbManager.AddColumn(dbName, addedTable, new IntegerType());
            dbManager.AddColumn(dbName, addedTable, new CharType());
            dbManager.AddRow(dbName, addedTable);
            dbManager.AddRow(dbName, addedTable);
            dbManager.AddRow(dbName, addedTable);

            int exEqRowCount = 3;
            Assert.AreEqual(dbManager.GetTable(dbName, addedTable).Rows.Count, exEqRowCount);

            dbManager.DeleteEqualRows(dbName, addedTable);

            int exNEqRowCount = 1;
            Assert.AreEqual(dbManager.GetTable(dbName, addedTable).Rows.Count, exNEqRowCount);
        }

        [TestMethod]
        public void TestSaveLoadDB()
        {
            string dbName = "TestDB";
            string addedTable = "testTable";
            string savePath = "C:\\Users\\bubka\\source\\repos\\LocalDB\\DAL\\Src\\postTestDB.xml";

            DBManager saveDbManager = new DBManager();
            saveDbManager.AddDB(dbName);
            saveDbManager.AddTable(dbName, addedTable);
            saveDbManager.AddColumn(dbName, addedTable, new IntegerType());
            saveDbManager.AddColumn(dbName, addedTable, new CharType());
            saveDbManager.AddRow(dbName, addedTable);
            saveDbManager.EditRowItem(dbName, addedTable, 0, 0, "100");
            saveDbManager.EditRowItem(dbName, addedTable, 1, 0, "a");
            saveDbManager.SaveDB();

            DBManager loadDbManager = new DBManager();
            loadDbManager.GetDB(dbName);

            Assert.AreEqual(loadDbManager.GetDB(dbName).Name, saveDbManager.GetDB(dbName).Name);
            CollectionAssert.AreEqual(loadDbManager.GetAllTableName(dbName), saveDbManager.GetAllTableName(dbName));
            Assert.AreEqual(loadDbManager.GetRowItem(dbName, addedTable, 0, 0), loadDbManager.GetRowItem(dbName, addedTable, 0, 0));
        }

        
    }
}