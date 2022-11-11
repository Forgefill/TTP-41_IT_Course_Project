using DAL;
using DAL.Types;
using DAL.DatabaseEntities;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using DAL.DBFileManagers;
using System.Security.Cryptography;


namespace LocalDB
{
    public partial class Form1 : Form
    {
        DBManager dbManager;
        string currentDB = null;
        string currentTable = null;

        public Form1()
        {
            InitializeComponent();
            databaseGridView.AllowUserToAddRows = false;
            dbManager = new DBManager();
            currentDB = "test";
            dbManager.GetDB(currentDB);
            DbName.Text = currentDB;
            currentTable = dbManager.GetAnyTableName(currentDB);
            RefreshTableList();
            RefreshGrid(currentTable);
        }


        #region DB

        private void createDbBtn_Click(object sender, EventArgs e)
        {
            string input = "Type here";
            if(ShowInputDialog("DB", ref input, "Add new db name") != DialogResult.OK) return;

            
            dbManager.SaveDB();
            dbManager.AddDB(input);
            DbName.Text = input;
            currentDB = input;
            currentTable = dbManager.GetAnyTableName(input);
            RefreshTableList();
            RefreshGrid(currentTable);
        }

        private void saveDbBtn_Click(object sender, EventArgs e)
        {
            dbManager.SaveDB();
        }


        private void downloadDbBtn_Click(object sender, EventArgs e)
        {
            string dbName = "Cache";
            openFileDialog1.Multiselect = false;
            openFileDialog1.InitialDirectory = @"C:\Users\bubka\source\repos\LocalDB\DAL\Src";
            openFileDialog1.Filter = "XML Files (*.xml)|*.xml";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.DefaultExt = "xml";
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                dbName = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
            }

            try
            {
                dbManager.GetDB(dbName);
            }
            catch(Exception a)
            {
                MessageBox.Show("Can`t download db from file\n" + "file path: " + dbName +"\n Error msg: " + a.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DbName.Text = dbName;
            currentDB = dbName;
            currentTable = dbManager.GetAnyTableName(dbName);
            RefreshTableList();
            RefreshGrid(currentTable);
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            dbManager.SaveDB();
        }

        #endregion


        #region column
        private void addColumnBtn_Click(object sender, EventArgs e)
        {
            if (currentTable == null) return;
            

            string type = typeComboBox.Text;
            string columnName = typeComboBox.Text;
            DialogResult dialogResult = ShowInputDialog("Add column",ref columnName, "Input new column name");
            
            
            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            switch (type)
            {
                case "Int":
                    IntegerType newInt = new IntegerType(columnName);
                    dbManager.AddColumn(currentDB, currentTable, newInt);
                    break;
                case "Char":
                    CharType newChar = new CharType(columnName);
                    dbManager.AddColumn(currentDB, currentTable, newChar);
                    break;
                case "String":
                    dbManager.AddColumn(currentDB, currentTable, new StringType(columnName));
                    break;
                case "Real":
                    RealType newReal = new RealType(columnName);
                    dbManager.AddColumn(currentDB, currentTable, newReal);
                    break;
                case "Email":
                    EmailType newEmail = new EmailType(columnName);
                    dbManager.AddColumn(currentDB, currentTable, newEmail);
                    break;
                case "Enum":
                    List<string> Enum = new List<string>();
                    ShowInputEnum(ref Enum);
                    if (Enum.Count != 0)
                    {
                        EnumType newEnum = new EnumType(columnName, Enum);
                        dbManager.AddColumn(currentDB, currentTable, newEnum);
                    }
                    break;
            }

            RefreshGrid(currentTable);

        }

        private void deleteColumnBtn_Click(object sender, EventArgs e)
        {
            if (currentTable == null || databaseGridView.SelectedCells.Count > 1) return;

            DialogResult dialogResult = MessageBox.Show("Do you want to delete selected column?", "Delete column?", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.No)
            {
                return;
            }
             
            int columnId = databaseGridView.SelectedCells[0].ColumnIndex;
            dbManager.DeleteColumn(currentDB, currentTable, columnId);
            RefreshGrid(currentTable);
            
        }

        private void editClmnName_Click(object sender, EventArgs e)
        {
            if (currentTable == null || databaseGridView.SelectedCells.Count > 1) return;

            string newClmnName = "New Column Name";
            ShowInputDialog("Column", ref newClmnName, "Input name for column");
            int columnId = databaseGridView.SelectedCells[0].ColumnIndex;

            dbManager.EditColumnName(currentDB, currentTable, columnId, newClmnName);
            databaseGridView.Columns[columnId].HeaderText = newClmnName;
        }

        private static DialogResult ShowInputEnum(ref List<string> input)
        {
            System.Drawing.Size size = new System.Drawing.Size(400, 400);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "Table input";

            Label inputLabel = new Label();

            inputLabel.Text = "Input enum values for new Column";
            inputLabel.Size = new System.Drawing.Size(size.Width - 10, 20);
            inputLabel.Location = new System.Drawing.Point(5, 5);
            inputLabel.Visible = true;
            inputBox.Controls.Add(inputLabel);

            ListBox listBox = new ListBox();
            listBox.Size = new Size(size.Width - 10, 120);
            listBox.Location = new Point(5, 25);
            inputBox.Controls.Add(listBox);

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 25);
            textBox.Location = new System.Drawing.Point(5, 155);
            textBox.Text = "input here";
            inputBox.Controls.Add(textBox);

            Button addButton = new Button();
            addButton.Name = "addButton";
            addButton.Size = new System.Drawing.Size(200, 30);
            addButton.Text = "&Add input to enum";
            addButton.Location = new System.Drawing.Point(size.Width - 80 - 80 - 80 - 125, 189);
            addButton.Click += (sender, e) => 
            {
                listBox.Items.Add(textBox.Text);
            };
            inputBox.Controls.Add(addButton);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 30);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 189);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 30);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 189);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            foreach(var a in listBox.Items)
            {
                input.Add(a.ToString());
            }
            return result;
        }
        #endregion

        //Validation.
        #region table

        private void AddTableBtn_Click(object sender, EventArgs e)
        {
            string input = "Type here";
            DialogResult AddTable = ShowInputDialog("Table", ref input, "Input name for new Table");
            if (AddTable == DialogResult.OK)
            {
                if (currentTable == null) currentTable = input;
                tableListBox.Items.Add(input);
                dbManager.AddTable(currentDB, input);
            }
        }

        private void deleteTableBtn_Click(object sender, EventArgs e)
        {

            if (tableListBox.SelectedItem == null || 
                MessageBox.Show("Do you want to delete selected table?", "Delete table?", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            string tableName = tableListBox.SelectedItem.ToString();
                

            dbManager.DeleteTable(currentDB, tableName);
            tableListBox.Items.Remove(tableListBox.SelectedItem);

            if (currentTable == tableName)
            {
                currentTable = dbManager.GetAnyTableName(currentDB);
                RefreshGrid(currentTable);
            }
        }
            
        private void editTableNameBtn_Click(object sender, EventArgs e)
        {
            if (tableListBox.SelectedItem != null)
            {
                string tableName = tableListBox.SelectedItem.ToString();
                string newName = tableName;
                DialogResult newNameInput = ShowInputDialog("Table", ref newName, "Input new name for table " + tableName);
                
                if(newNameInput == DialogResult.OK)
                {
                    dbManager.EditTableName(currentDB, tableName, newName);
                    if (currentTable == tableName) currentTable = newName;
                    RefreshTableList();
                }
            }


        }

        #endregion

        
        #region editing

        private void databaseGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell editingCell = databaseGridView.CurrentCell;
            object cellValue = editingCell.Value;
            BaseType CellType = dbManager.GetColumnTypeById(currentDB, currentTable, e.ColumnIndex);
            
            if(cellValue == null)
            {
                editingCell.Value = CellType.defValue;
                dbManager.EditRowItem(currentDB, currentTable, e.ColumnIndex, e.RowIndex, CellType.defValue);
                return;
            }
            
            if(CellType.isCorrect(cellValue.ToString()))
            {
                dbManager.EditRowItem(currentDB, currentTable, e.ColumnIndex, e.RowIndex, cellValue.ToString());
            }
            else
            {
                editingCell.Value = dbManager.GetRowItem(currentDB, currentTable, e.ColumnIndex, e.RowIndex);
                MessageBox.Show("Invalid data type, pls use next rules:\n" + CellType.TypeRule(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void AddRowBtn_Click(object sender, EventArgs e)
        {
            if (databaseGridView.Columns.Count == 0)
            {
                MessageBox.Show("There is no column in the table\nplease add at least one", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            databaseGridView.Rows.Add();
            dbManager.AddRow(currentDB, currentTable);
            RefreshGrid(currentTable);

            List<Row> a = dbManager.GetTable(currentDB, currentTable).Rows;
        }

        private void deleteRowBtn_Click(object sender, EventArgs e)
        {
            if (databaseGridView.SelectedCells.Count < 1 || databaseGridView.SelectedCells.Count > 1)
            {
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Do you want to delete selected row?", "Delete row?", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.No)
            {
                return;
            }

            int rowId = databaseGridView.SelectedCells[0].RowIndex;
            databaseGridView.Rows.RemoveAt(rowId);
            dbManager.DeleteRow(currentDB, currentTable, rowId);
        }

        private void removeDuplicatesBtn_Click(object sender, EventArgs e)
        {
            if (currentTable == null) return;
            dbManager.DeleteEqualRows(currentDB, currentTable);
            RefreshGrid(currentTable);
        }
        #endregion

        private static DialogResult ShowInputDialog(string headerText, ref string input, string description)
        {
            System.Drawing.Size size = new System.Drawing.Size(400, 200);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = headerText;

            Label inputLabel = new Label();

            inputLabel.Text = description;
            inputLabel.Size = new System.Drawing.Size(size.Width - 10, 23);
            inputLabel.Location = new System.Drawing.Point(5, 5);
            inputBox.Controls.Add(inputLabel);

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 35);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 30);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 69);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 30);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 69);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        private void RefreshGrid(string tableName)
        {
            databaseGridView.Rows.Clear();
            databaseGridView.Columns.Clear();
            if (tableName == null) return;

            Table table = dbManager.GetTable(currentDB, tableName);
            List<string> defaultValues = new List<string>();

            for(int i = 0; i < table.columnTypes.Count; ++i)
            {
                BaseType a = table.columnTypes[i];

                if (a.typeName != "Enum")
                {
                    DataGridViewTextBoxColumn dgvCmb = new DataGridViewTextBoxColumn();
                    dgvCmb.Name = "Column" + (i + 1);
                    dgvCmb.HeaderText = a.name;
                    dgvCmb.SortMode = DataGridViewColumnSortMode.NotSortable;
                    databaseGridView.Columns.Add("Column" + (i + 1), a.name);
                }
                else
                {
                    EnumType Enum = (EnumType)a;
                    DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
                    foreach(string b in Enum.enumValues)
                    {
                        dgvCmb.Items.Add(b);
                    }
                    dgvCmb.Name = "Column" + (i + 1);
                    dgvCmb.HeaderText = Enum.name;
                    dgvCmb.SortMode = DataGridViewColumnSortMode.NotSortable;
                    databaseGridView.Columns.Add(dgvCmb);
                }
            }

            for (int i = 0; i < table.Rows.Count; ++i)
            {
                databaseGridView.Rows.Add();
                for(int j = 0; j < table.columnTypes.Count; ++j)
                {
                    databaseGridView.Rows[i].Cells[j].Value = table.Rows[i][j];
                }

            }
        }

        private void RefreshTableList()
        {

            tableListBox.Items.Clear();
            foreach(string a in dbManager.GetAllTableName(currentDB))
            {
                tableListBox.Items.Add(a);
            }
            
        }


        private void tableListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (tableListBox.SelectedItem != null)
            {
                string selected = tableListBox.SelectedItem.ToString();
                if (currentTable != selected)
                {
                    currentTable = tableListBox.SelectedItem.ToString();
                    RefreshGrid(currentTable);
                }
            }
            tableListBox.SelectedItem = null;
        }

        private void databaseGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (databaseGridView.IsCurrentCellDirty) databaseGridView.EndEdit();
        }

        
    }
}