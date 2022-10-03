namespace LocalDB
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.databaseGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DbName = new System.Windows.Forms.Label();
            this.saveDbBtn = new System.Windows.Forms.Button();
            this.downloadDbBtn = new System.Windows.Forms.Button();
            this.createDbBtn = new System.Windows.Forms.Button();
            this.AddTableBtn = new System.Windows.Forms.Button();
            this.deleteTableBtn = new System.Windows.Forms.Button();
            this.tableListBox = new System.Windows.Forms.ListBox();
            this.addColumnBtn = new System.Windows.Forms.Button();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.editTableNameBtn = new System.Windows.Forms.Button();
            this.deleteColumnBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.AddRowBtn = new System.Windows.Forms.Button();
            this.deleteRowBtn = new System.Windows.Forms.Button();
            this.removeDuplicatesBtn = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SaveAsBtn = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.databaseGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // databaseGridView
            // 
            this.databaseGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.databaseGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.databaseGridView.Location = new System.Drawing.Point(12, 45);
            this.databaseGridView.Name = "databaseGridView";
            this.databaseGridView.RowHeadersWidth = 51;
            this.databaseGridView.RowTemplate.Height = 29;
            this.databaseGridView.Size = new System.Drawing.Size(532, 393);
            this.databaseGridView.TabIndex = 0;
            this.databaseGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.databaseGridView_CellClick);
            this.databaseGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.databaseGridView_CellEndEdit);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 125;
            // 
            // DbName
            // 
            this.DbName.AutoSize = true;
            this.DbName.Location = new System.Drawing.Point(12, 22);
            this.DbName.Name = "DbName";
            this.DbName.Size = new System.Drawing.Size(116, 20);
            this.DbName.TabIndex = 1;
            this.DbName.Text = "Database Name";
            // 
            // saveDbBtn
            // 
            this.saveDbBtn.Location = new System.Drawing.Point(350, 10);
            this.saveDbBtn.Name = "saveDbBtn";
            this.saveDbBtn.Size = new System.Drawing.Size(94, 29);
            this.saveDbBtn.TabIndex = 2;
            this.saveDbBtn.Text = "Save";
            this.saveDbBtn.UseVisualStyleBackColor = true;
            this.saveDbBtn.Click += new System.EventHandler(this.saveDbBtn_Click);
            // 
            // downloadDbBtn
            // 
            this.downloadDbBtn.Location = new System.Drawing.Point(250, 10);
            this.downloadDbBtn.Name = "downloadDbBtn";
            this.downloadDbBtn.Size = new System.Drawing.Size(94, 29);
            this.downloadDbBtn.TabIndex = 4;
            this.downloadDbBtn.Text = "Download";
            this.downloadDbBtn.UseVisualStyleBackColor = true;
            this.downloadDbBtn.Click += new System.EventHandler(this.downloadDbBtn_Click);
            // 
            // createDbBtn
            // 
            this.createDbBtn.Location = new System.Drawing.Point(150, 10);
            this.createDbBtn.Name = "createDbBtn";
            this.createDbBtn.Size = new System.Drawing.Size(94, 29);
            this.createDbBtn.TabIndex = 5;
            this.createDbBtn.Text = "Create";
            this.createDbBtn.UseVisualStyleBackColor = true;
            this.createDbBtn.Click += new System.EventHandler(this.createDbBtn_Click);
            // 
            // AddTableBtn
            // 
            this.AddTableBtn.Location = new System.Drawing.Point(575, 44);
            this.AddTableBtn.Name = "AddTableBtn";
            this.AddTableBtn.Size = new System.Drawing.Size(104, 29);
            this.AddTableBtn.TabIndex = 6;
            this.AddTableBtn.Text = "Add Table";
            this.AddTableBtn.UseVisualStyleBackColor = true;
            this.AddTableBtn.Click += new System.EventHandler(this.AddTableBtn_Click);
            // 
            // deleteTableBtn
            // 
            this.deleteTableBtn.Location = new System.Drawing.Point(575, 9);
            this.deleteTableBtn.Name = "deleteTableBtn";
            this.deleteTableBtn.Size = new System.Drawing.Size(104, 29);
            this.deleteTableBtn.TabIndex = 7;
            this.deleteTableBtn.Text = "Delete Table";
            this.deleteTableBtn.UseVisualStyleBackColor = true;
            this.deleteTableBtn.Click += new System.EventHandler(this.deleteTableBtn_Click);
            // 
            // tableListBox
            // 
            this.tableListBox.FormattingEnabled = true;
            this.tableListBox.ItemHeight = 20;
            this.tableListBox.Location = new System.Drawing.Point(575, 79);
            this.tableListBox.Name = "tableListBox";
            this.tableListBox.Size = new System.Drawing.Size(213, 104);
            this.tableListBox.TabIndex = 8;
            this.tableListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tableListBox_MouseDoubleClick);
            // 
            // addColumnBtn
            // 
            this.addColumnBtn.Location = new System.Drawing.Point(550, 409);
            this.addColumnBtn.Name = "addColumnBtn";
            this.addColumnBtn.Size = new System.Drawing.Size(118, 29);
            this.addColumnBtn.TabIndex = 9;
            this.addColumnBtn.Text = "Add Column";
            this.addColumnBtn.UseVisualStyleBackColor = true;
            this.addColumnBtn.Click += new System.EventHandler(this.addColumnBtn_Click);
            // 
            // typeComboBox
            // 
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            "Int",
            "Char",
            "String",
            "Real",
            "Email",
            "Enum"});
            this.typeComboBox.Location = new System.Drawing.Point(550, 375);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(118, 28);
            this.typeComboBox.TabIndex = 10;
            // 
            // editTableNameBtn
            // 
            this.editTableNameBtn.Location = new System.Drawing.Point(685, 45);
            this.editTableNameBtn.Name = "editTableNameBtn";
            this.editTableNameBtn.Size = new System.Drawing.Size(104, 29);
            this.editTableNameBtn.TabIndex = 11;
            this.editTableNameBtn.Text = "Edit Name";
            this.editTableNameBtn.UseVisualStyleBackColor = true;
            this.editTableNameBtn.Click += new System.EventHandler(this.editTableNameBtn_Click);
            // 
            // deleteColumnBtn
            // 
            this.deleteColumnBtn.Location = new System.Drawing.Point(674, 409);
            this.deleteColumnBtn.Name = "deleteColumnBtn";
            this.deleteColumnBtn.Size = new System.Drawing.Size(118, 29);
            this.deleteColumnBtn.TabIndex = 12;
            this.deleteColumnBtn.Text = "Delete Column";
            this.deleteColumnBtn.UseVisualStyleBackColor = true;
            this.deleteColumnBtn.Click += new System.EventHandler(this.deleteColumnBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(674, 374);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 29);
            this.button1.TabIndex = 13;
            this.button1.Text = "Edit C Name";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.editClmnName_Click);
            // 
            // AddRowBtn
            // 
            this.AddRowBtn.Location = new System.Drawing.Point(550, 340);
            this.AddRowBtn.Name = "AddRowBtn";
            this.AddRowBtn.Size = new System.Drawing.Size(118, 29);
            this.AddRowBtn.TabIndex = 14;
            this.AddRowBtn.Text = "Add Row";
            this.AddRowBtn.UseVisualStyleBackColor = true;
            this.AddRowBtn.Click += new System.EventHandler(this.AddRowBtn_Click);
            // 
            // deleteRowBtn
            // 
            this.deleteRowBtn.Location = new System.Drawing.Point(674, 340);
            this.deleteRowBtn.Name = "deleteRowBtn";
            this.deleteRowBtn.Size = new System.Drawing.Size(118, 29);
            this.deleteRowBtn.TabIndex = 15;
            this.deleteRowBtn.Text = "Delete Row";
            this.deleteRowBtn.UseVisualStyleBackColor = true;
            this.deleteRowBtn.Click += new System.EventHandler(this.deleteRowBtn_Click);
            // 
            // removeDuplicatesBtn
            // 
            this.removeDuplicatesBtn.Location = new System.Drawing.Point(550, 305);
            this.removeDuplicatesBtn.Name = "removeDuplicatesBtn";
            this.removeDuplicatesBtn.Size = new System.Drawing.Size(242, 29);
            this.removeDuplicatesBtn.TabIndex = 16;
            this.removeDuplicatesBtn.Text = "Remove  duplicates";
            this.removeDuplicatesBtn.UseVisualStyleBackColor = true;
            this.removeDuplicatesBtn.Click += new System.EventHandler(this.removeDuplicatesBtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // SaveAsBtn
            // 
            this.SaveAsBtn.Location = new System.Drawing.Point(450, 9);
            this.SaveAsBtn.Name = "SaveAsBtn";
            this.SaveAsBtn.Size = new System.Drawing.Size(94, 29);
            this.SaveAsBtn.TabIndex = 17;
            this.SaveAsBtn.Text = "Save as";
            this.SaveAsBtn.UseVisualStyleBackColor = true;
            this.SaveAsBtn.Click += new System.EventHandler(this.SaveAsBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SaveAsBtn);
            this.Controls.Add(this.removeDuplicatesBtn);
            this.Controls.Add(this.deleteRowBtn);
            this.Controls.Add(this.AddRowBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.deleteColumnBtn);
            this.Controls.Add(this.editTableNameBtn);
            this.Controls.Add(this.typeComboBox);
            this.Controls.Add(this.addColumnBtn);
            this.Controls.Add(this.tableListBox);
            this.Controls.Add(this.deleteTableBtn);
            this.Controls.Add(this.AddTableBtn);
            this.Controls.Add(this.createDbBtn);
            this.Controls.Add(this.downloadDbBtn);
            this.Controls.Add(this.saveDbBtn);
            this.Controls.Add(this.DbName);
            this.Controls.Add(this.databaseGridView);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.databaseGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView databaseGridView;
        private Label DbName;
        private Button saveDbBtn;
        private Button downloadDbBtn;
        private Button createDbBtn;
        private Button AddTableBtn;
        private Button deleteTableBtn;
        private ListBox tableListBox;
        private Button addColumnBtn;
        private ComboBox typeComboBox;
        private DataGridViewComboBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private Button editTableNameBtn;
        private Button deleteColumnBtn;
        private Button button1;
        private Button AddRowBtn;
        private Button deleteRowBtn;
        private Button removeDuplicatesBtn;
        private FolderBrowserDialog folderBrowserDialog1;
        private OpenFileDialog openFileDialog1;
        private Button SaveAsBtn;
        private SaveFileDialog saveFileDialog1;
    }
}