namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tblUserMasterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cruiseDataSet = new WindowsFormsApplication1.cruiseDataSet();
            this.tblUserMasterTableAdapter = new WindowsFormsApplication1.cruiseDataSetTableAdapters.tblUserMasterTableAdapter();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnDecryptText = new System.Windows.Forms.Button();
            this.txtEncryptedText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnEncryptText = new System.Windows.Forms.Button();
            this.txtPlainText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dgDecrypted = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dgEncrypted = new System.Windows.Forms.DataGridView();
            this.dgDB = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.passwordDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.administratorDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.userRoleIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userEmailIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnEncryptTheColumns = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tblUserMasterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cruiseDataSet)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDecrypted)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEncrypted)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDB)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblUserMasterBindingSource
            // 
            this.tblUserMasterBindingSource.DataMember = "tblUserMaster";
            this.tblUserMasterBindingSource.DataSource = this.cruiseDataSet;
            // 
            // cruiseDataSet
            // 
            this.cruiseDataSet.DataSetName = "cruiseDataSet";
            this.cruiseDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tblUserMasterTableAdapter
            // 
            this.tblUserMasterTableAdapter.ClearBeforeFill = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1030, 821);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.txtResult);
            this.tabPage1.Controls.Add(this.btnDecryptText);
            this.tabPage1.Controls.Add(this.txtEncryptedText);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.btnEncryptText);
            this.tabPage1.Controls.Add(this.txtPlainText);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1022, 795);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Encrypt/ Decrypt Text";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 291);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Result";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(22, 307);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(894, 253);
            this.txtResult.TabIndex = 6;
            // 
            // btnDecryptText
            // 
            this.btnDecryptText.Location = new System.Drawing.Point(922, 173);
            this.btnDecryptText.Name = "btnDecryptText";
            this.btnDecryptText.Size = new System.Drawing.Size(75, 68);
            this.btnDecryptText.TabIndex = 5;
            this.btnDecryptText.Text = "Decrypt";
            this.btnDecryptText.UseVisualStyleBackColor = true;
            this.btnDecryptText.Click += new System.EventHandler(this.btnDecryptText_Click);
            // 
            // txtEncryptedText
            // 
            this.txtEncryptedText.Location = new System.Drawing.Point(22, 173);
            this.txtEncryptedText.Multiline = true;
            this.txtEncryptedText.Name = "txtEncryptedText";
            this.txtEncryptedText.Size = new System.Drawing.Size(894, 68);
            this.txtEncryptedText.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Encrypted Text";
            // 
            // btnEncryptText
            // 
            this.btnEncryptText.Location = new System.Drawing.Point(922, 43);
            this.btnEncryptText.Name = "btnEncryptText";
            this.btnEncryptText.Size = new System.Drawing.Size(75, 68);
            this.btnEncryptText.TabIndex = 2;
            this.btnEncryptText.Text = "Encrypt";
            this.btnEncryptText.UseVisualStyleBackColor = true;
            this.btnEncryptText.Click += new System.EventHandler(this.btnEncryptText_Click);
            // 
            // txtPlainText
            // 
            this.txtPlainText.Location = new System.Drawing.Point(22, 43);
            this.txtPlainText.Multiline = true;
            this.txtPlainText.Name = "txtPlainText";
            this.txtPlainText.Size = new System.Drawing.Size(894, 68);
            this.txtPlainText.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Plain Text";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnDecrypt);
            this.tabPage2.Controls.Add(this.btnEncrypt);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.dgDecrypted);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.dgEncrypted);
            this.tabPage2.Controls.Add(this.dgDB);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1022, 795);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(936, 487);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(75, 23);
            this.btnDecrypt.TabIndex = 13;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(936, 219);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEncrypt.TabIndex = 12;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 524);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Decrypted";
            // 
            // dgDecrypted
            // 
            this.dgDecrypted.AllowUserToAddRows = false;
            this.dgDecrypted.AllowUserToDeleteRows = false;
            this.dgDecrypted.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDecrypted.Location = new System.Drawing.Point(7, 543);
            this.dgDecrypted.Name = "dgDecrypted";
            this.dgDecrypted.ReadOnly = true;
            this.dgDecrypted.Size = new System.Drawing.Size(922, 236);
            this.dgDecrypted.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 255);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Encrypted";
            // 
            // dgEncrypted
            // 
            this.dgEncrypted.AllowUserToAddRows = false;
            this.dgEncrypted.AllowUserToDeleteRows = false;
            this.dgEncrypted.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEncrypted.Location = new System.Drawing.Point(7, 274);
            this.dgEncrypted.Name = "dgEncrypted";
            this.dgEncrypted.ReadOnly = true;
            this.dgEncrypted.Size = new System.Drawing.Size(922, 236);
            this.dgEncrypted.TabIndex = 8;
            // 
            // dgDB
            // 
            this.dgDB.AllowUserToAddRows = false;
            this.dgDB.AllowUserToDeleteRows = false;
            this.dgDB.AutoGenerateColumns = false;
            this.dgDB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDB.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.userIdDataGridViewTextBoxColumn,
            this.userNameDataGridViewTextBoxColumn,
            this.passwordDataGridViewTextBoxColumn,
            this.activeDataGridViewCheckBoxColumn,
            this.administratorDataGridViewCheckBoxColumn,
            this.userRoleIdDataGridViewTextBoxColumn,
            this.userEmailIdDataGridViewTextBoxColumn});
            this.dgDB.DataSource = this.tblUserMasterBindingSource;
            this.dgDB.Location = new System.Drawing.Point(7, 7);
            this.dgDB.Name = "dgDB";
            this.dgDB.ReadOnly = true;
            this.dgDB.Size = new System.Drawing.Size(922, 236);
            this.dgDB.TabIndex = 7;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // userIdDataGridViewTextBoxColumn
            // 
            this.userIdDataGridViewTextBoxColumn.DataPropertyName = "UserId";
            this.userIdDataGridViewTextBoxColumn.HeaderText = "UserId";
            this.userIdDataGridViewTextBoxColumn.Name = "userIdDataGridViewTextBoxColumn";
            this.userIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // userNameDataGridViewTextBoxColumn
            // 
            this.userNameDataGridViewTextBoxColumn.DataPropertyName = "UserName";
            this.userNameDataGridViewTextBoxColumn.HeaderText = "UserName";
            this.userNameDataGridViewTextBoxColumn.Name = "userNameDataGridViewTextBoxColumn";
            this.userNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // passwordDataGridViewTextBoxColumn
            // 
            this.passwordDataGridViewTextBoxColumn.DataPropertyName = "Password";
            this.passwordDataGridViewTextBoxColumn.HeaderText = "Password";
            this.passwordDataGridViewTextBoxColumn.Name = "passwordDataGridViewTextBoxColumn";
            this.passwordDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // activeDataGridViewCheckBoxColumn
            // 
            this.activeDataGridViewCheckBoxColumn.DataPropertyName = "Active";
            this.activeDataGridViewCheckBoxColumn.HeaderText = "Active";
            this.activeDataGridViewCheckBoxColumn.Name = "activeDataGridViewCheckBoxColumn";
            this.activeDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // administratorDataGridViewCheckBoxColumn
            // 
            this.administratorDataGridViewCheckBoxColumn.DataPropertyName = "Administrator";
            this.administratorDataGridViewCheckBoxColumn.HeaderText = "Administrator";
            this.administratorDataGridViewCheckBoxColumn.Name = "administratorDataGridViewCheckBoxColumn";
            this.administratorDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // userRoleIdDataGridViewTextBoxColumn
            // 
            this.userRoleIdDataGridViewTextBoxColumn.DataPropertyName = "UserRoleId";
            this.userRoleIdDataGridViewTextBoxColumn.HeaderText = "UserRoleId";
            this.userRoleIdDataGridViewTextBoxColumn.Name = "userRoleIdDataGridViewTextBoxColumn";
            this.userRoleIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // userEmailIdDataGridViewTextBoxColumn
            // 
            this.userEmailIdDataGridViewTextBoxColumn.DataPropertyName = "userEmailId";
            this.userEmailIdDataGridViewTextBoxColumn.HeaderText = "userEmailId";
            this.userEmailIdDataGridViewTextBoxColumn.Name = "userEmailIdDataGridViewTextBoxColumn";
            this.userEmailIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage3.Controls.Add(this.btnEncryptTheColumns);
            this.tabPage3.Controls.Add(this.treeView1);
            this.tabPage3.Controls.Add(this.btnConnect);
            this.tabPage3.Controls.Add(this.txtConnectionString);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1022, 795);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Encrypt/ Decrypt Tables";
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Location = new System.Drawing.Point(15, 51);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(359, 732);
            this.treeView1.TabIndex = 6;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(915, 20);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 31);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(15, 24);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(894, 20);
            this.txtConnectionString.TabIndex = 4;
            this.txtConnectionString.Text = "Data Source=QJ07302866;Initial Catalog=cruise;Integrated Security=True;";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Plain Text";
            // 
            // btnEncryptTheColumns
            // 
            this.btnEncryptTheColumns.Location = new System.Drawing.Point(380, 51);
            this.btnEncryptTheColumns.Name = "btnEncryptTheColumns";
            this.btnEncryptTheColumns.Size = new System.Drawing.Size(230, 31);
            this.btnEncryptTheColumns.TabIndex = 7;
            this.btnEncryptTheColumns.Text = "Encrypt The Columns";
            this.btnEncryptTheColumns.UseVisualStyleBackColor = true;
            this.btnEncryptTheColumns.Click += new System.EventHandler(this.btnEncryptTheColumns_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 845);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tblUserMasterBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cruiseDataSet)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDecrypted)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEncrypted)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDB)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private cruiseDataSet cruiseDataSet;
        private System.Windows.Forms.BindingSource tblUserMasterBindingSource;
        private cruiseDataSetTableAdapters.tblUserMasterTableAdapter tblUserMasterTableAdapter;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgDecrypted;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgEncrypted;
        private System.Windows.Forms.DataGridView dgDB;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn passwordDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn activeDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn administratorDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userRoleIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userEmailIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnEncryptText;
        private System.Windows.Forms.TextBox txtPlainText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDecryptText;
        private System.Windows.Forms.TextBox txtEncryptedText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button btnEncryptTheColumns;
    }
}

