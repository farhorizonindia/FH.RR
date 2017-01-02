using FarHorizon.DataSecurity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace WindowsFormsApplication1
{
    public class DataManager
    {
        SqlConnection _connection;
        List<ParentItem> tablesAndColumns = new List<ParentItem>();

        public DataManager(string connectionString)
        {
            try
            {
                _connection = new SqlConnection(connectionString);
                _connection.Open();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public List<ParentItem> GetTablesAndColumns()
        {
            string query = "select t.name, c.name from sys.objects t join sys.columns c on t.object_id = c.object_id and t.type = 'U' and t.name <>'dtproperties' order by t.name";
            var reader = GetData(query);

            if (reader != null)
            {
                while (reader.Read())
                {
                    string tableName = reader.GetString(0);
                    string columnName = reader.GetString(1);

                    var parentItem = tablesAndColumns.FirstOrDefault(parent => string.Compare(parent.ItemName, tableName, true) == 0);
                    if (parentItem == null)
                    {
                        parentItem = new ParentItem();
                        parentItem.ItemName = tableName;
                        parentItem.Children.Add(new ChildItem { ItemName = columnName });
                        tablesAndColumns.Add(parentItem);
                    }
                    else
                    {
                        var childItem = parentItem.Children.FirstOrDefault(child => string.Compare(child.ItemName, columnName, true) == 0);
                        if (childItem == null)
                        {
                            parentItem.Children.Add(new ChildItem { ItemName = columnName });
                        }
                    }
                }
            }
            reader.Close();
            return tablesAndColumns;
        }

        //SELECT DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, CHARACTER_OCTET_LENGTH, NUMERIC_PRECISION, NUMERIC_PRECISION_RADIX, NUMERIC_SCALE, DATETIME_PRECISION
        //FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'GetSeasons' AND COLUMN_NAME = 'SeasonName'

        internal void EncryptData(List<ParentItem> items)
        {
            List<string> updateQueryCollection = new List<string>();
            string updateQuery = string.Empty;

            List<string> columnNames = new List<string>();
            foreach (var parentItem in items)
            {
                string tableName = parentItem.ItemName;
                foreach (var childItem in parentItem.Children)
                {
                    string columnName = childItem.ItemName;
                    if (childItem.Selected)
                    {
                        ChangeColumnLength(tableName, columnName);
                    }
                    columnNames.Add(columnName);
                }
                string query = "select " + string.Join(", ", columnNames) + " from " + tableName;

                List<ColumnDetail> columnDetails = new List<ColumnDetail>();

                var reader = GetData(query);
                while (reader.Read())
                {
                    columnDetails.Clear();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        ColumnDetail cd = new ColumnDetail();
                        cd.ColumnName = reader.GetName(i);
                        cd.ColumnType = reader.GetFieldType(i);
                        cd.Value = reader.GetValue(i);
                        columnDetails.Add(cd);
                    }

                    foreach (var columnToBeEncrypted in parentItem.Children.Where(c => c.Selected))
                    {
                        ColumnDetail cd = columnDetails.FirstOrDefault(c => string.Compare(c.ColumnName, columnToBeEncrypted.ItemName, true) == 0);

                        updateQuery = "update " + tableName + " set " + columnToBeEncrypted.ItemName + " = '" + DataSecurityManager.Encrypt(cd.Value.ToString()) + "' where 1 = 1";

                        foreach (var otherColumn in columnDetails.Where(c => string.Compare(c.ColumnName, columnToBeEncrypted.ItemName, true) != 0))
                        {
                            updateQuery += " and " + otherColumn.ColumnName + " = " + DecorateForSQlQuery(otherColumn.ColumnType, otherColumn.Value.ToString());
                        }
                        updateQueryCollection.Add(updateQuery);
                    }
                }
                reader.Close();
            }

            foreach (var q in updateQueryCollection)
            {
                ExecuteNonQuery(q);
            }
        }

        private void ChangeColumnLength(string tableName, string columnName)
        {
            string query = "SELECT CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tableName + "' AND COLUMN_NAME = '" + columnName + "'";
            var ml = GetScalarData(query);

            if (ml == null)
            {
                ml = 0;
            }

            int maxLength = 0;
            int.TryParse(ml.ToString(), out maxLength);

            if (maxLength < 150)
            {
                query = "Alter table " + tableName + " Alter column " + columnName + " varchar(150)";
                ExecuteNonQuery(query);
            }
        }


        private string DecorateForSQlQuery(Type type, string text)
        {
            if (type == typeof(string) || type == typeof(DateTime))
            {
                return "'" + text + "'";
            }
            if (type == typeof(bool))
            {
                return string.Compare(text, Boolean.TrueString, true) == 0 ? "1" : "0";
            }
            return text;
        }

        private SqlDataReader GetData(string query)
        {
            SqlDataReader reader = null;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                reader = cmd.ExecuteReader();
            }
            return reader;
        }

        private object GetScalarData(string query)
        {
            object response = null;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                response = cmd.ExecuteScalar();
            }
            return response;
        }

        private void ExecuteNonQuery(string query)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
