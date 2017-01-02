using FarHorizon.DataSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        DataSecurityManager dsm;
        DataManager dm;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dsm = new DataSecurityManager();

            List<UserMaster> userMasterFromDBList = new List<UserMaster>();
            this.tblUserMasterTableAdapter.Fill(this.cruiseDataSet.tblUserMaster);
            foreach (var item in this.cruiseDataSet.tblUserMaster.AsQueryable())
            {
                UserMaster userMasterFromDB = new UserMaster
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    UserName = item.UserName,
                    Password = item.Password,
                    userEmailId = item.userEmailId,
                    Active = item.Active,
                    Administrator = item.Administrator,
                    UserRoleId = item.UserRoleId
                };
                userMasterFromDBList.Add(userMasterFromDB);
            }
            dgDB.DataSource = userMasterFromDBList;
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            List<UserMaster> userMasterEncryptedList = new List<UserMaster>();
            foreach (DataGridViewRow row in dgDB.Rows)
            {
                UserMaster um = row.DataBoundItem as UserMaster;
                um.UserName = dsm.Encrypt(um.UserName);
                um.Password = dsm.Encrypt(um.Password);
                userMasterEncryptedList.Add(um);
            }
            dgEncrypted.DataSource = userMasterEncryptedList;
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            List<UserMaster> userMasterDecryptedList = new List<UserMaster>();
            foreach (DataGridViewRow row in dgEncrypted.Rows)
            {
                UserMaster um = row.DataBoundItem as UserMaster;

                UserMaster num = new UserMaster
                {
                    Id = um.Id,
                    UserId = um.UserId,
                    UserName = dsm.Decrypt(um.UserName),
                    Password = dsm.Decrypt(um.Password),
                    userEmailId = um.userEmailId,
                    Active = um.Active,
                    Administrator = um.Administrator,
                    UserRoleId = um.UserRoleId
                };
                userMasterDecryptedList.Add(num);
            }
            dgDecrypted.DataSource = userMasterDecryptedList;
        }

        private void btnEncryptText_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlainText.Text))
            {
                MessageBox.Show("Please enter the plain text to encrypt");
                return;
            }
            txtResult.Text = dsm.Encrypt(txtPlainText.Text);

        }

        private void btnDecryptText_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEncryptedText.Text))
            {
                MessageBox.Show("Please enter the encrypted text to decrypt");
                return;
            }
            txtResult.Text = dsm.Decrypt(txtEncryptedText.Text);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtConnectionString.Text))
            {
                MessageBox.Show("Please enter the connection string to connect to.");
                return;
            }

            dm = new DataManager(txtConnectionString.Text);
            List<ParentItem> items = dm.GetTablesAndColumns();

            items.Sort((x, y) => x.ItemName.CompareTo(y.ItemName));

            TreeNode rootNode = new TreeNode("Tables");
            rootNode.Expand();

            if (items != null)
            {
                foreach (var item in items)
                {
                    TreeNode tableNode = new TreeNode(item.ItemName);
                    foreach (var child in item.Children)
                    {
                        if (child == null) continue;
                        TreeNode childNode = new TreeNode(child.ItemName);

                        tableNode.Nodes.Add(childNode);
                    }
                    rootNode.Nodes.Add(tableNode);
                }
            }
            treeView1.Nodes.Add(rootNode);
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 1)
            {
                foreach (TreeNode childNode in e.Node.Nodes)
                {
                    childNode.Checked = e.Node.Checked;
                }
            }
        }

        private void btnEncryptTheColumns_Click(object sender, EventArgs e)
        {
            TreeNode rootNode = treeView1.Nodes[0];
            List<ParentItem> items = new List<ParentItem>();

            foreach (TreeNode table in rootNode.Nodes)
            {
                bool isAnyColumnSelected = false;
                foreach (TreeNode column in table.Nodes)
                {
                    if (column.Checked)
                        isAnyColumnSelected = true;
                }
                if (!isAnyColumnSelected)
                {
                    continue;
                }

                string tableName = table.Text;
                foreach (TreeNode column in table.Nodes)
                {
                    ParentItem pi = items.FirstOrDefault(parentItem => string.Compare(parentItem.ItemName, tableName, true) == 0);
                    if (pi == null)
                    {
                        pi = new ParentItem { ItemName = tableName };
                        items.Add(pi);
                    }
                    string columnName = column.Text;

                    ChildItem ci = pi.Children.FirstOrDefault(childItem => string.Compare(childItem.ItemName, columnName, true) == 0);
                    if (ci == null)
                    {
                        ci = new ChildItem { ItemName = columnName, Selected = column.Checked };
                        pi.Children.Add(ci);
                    }

                }
            }
            if (dm == null)
            {
                dm = new DataManager(txtConnectionString.Text);
            }
            dm.EncryptData(items);
        }
    }
}
