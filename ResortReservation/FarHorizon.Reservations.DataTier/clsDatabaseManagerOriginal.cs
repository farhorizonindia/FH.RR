using System;
using System.Data;
using System.Configuration;
using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.Sql;


//using System.Data;

namespace DataLayer
{
    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public class clsDatabaseManagerOriginal
    {
        //public SqlConnection	  con;
        //private static SqlConnection _DatabaseConnection;
        private SqlConnection _DatabaseConnection;
        private SqlConnection _RemoteConnection;
        //public SqlDataAdapter	  dAdapter;
        public SqlDataAdapter dAdapter;
        //public SqlDataAdapter dAdapter;

        public DataSet dSet;
        //public string conString=@"Provider=Microsoft.Jet.Sql.4.0;Data Source=..\\..\\PersonDatabase.mdb";	
        //public string conString = @"Integrated Security=true; Initial Catalog=MLM; Data Source=AMIT-K7E4N7MJGN\NETSDK";

        string strQuery;
        //public string conString = @"Integrated Security=true; Initial Catalog=MLM; DSN=MLM";

        //public string conString = @"Integrated Security=true; Initial Catalog=MLM; Data Source=BINIT-4FA630467";

        /*-------------------------------------------------------------------------
       * public method 
       * Overloaded		: no
       * Parameters		: no
       * Return value		: bool (true or false)
       * Purpose			: creates a connection to the database,a DataAdapter,
        *						  a DataSet and returns true if all ok otherwise false
        *-------------------------------------------------------------------------*/

        //public clsDataBaseManager()
        //{
        //    InitializeDatabaseConnection();
        //}

        public SqlConnection ApplicationConnection
        {
            get
            {
                return _DatabaseConnection;
            }
        }

        public SqlConnection RemoteConnection
        {
            get { return _RemoteConnection; }
        }

        public bool GetConnection(string DatabaseType)
        {
            string ConnectionString = string.Empty;
            if (string.Compare(DatabaseType, "APPDB", true) == 0)
            {
                if (_DatabaseConnection == null || _DatabaseConnection.State == ConnectionState.Closed)
                {
                    if (ConnectionString == string.Empty)
                    {
                        GetConnectionString();
                    }
                    _DatabaseConnection = new SqlConnection();
                    _DatabaseConnection.ConnectionString = ConnectionString;
                    try
                    {
                        _DatabaseConnection.Open();
                    }
                    catch (Exception ex)
                    {
                        GenFunctions.LogError("clsDatabaseManager.GetConnection", ex.Message.ToString());
                        ConnectionString = string.Empty;
                        return false;
                    }
                    ConnectionString = string.Empty;
                }
            }
            if (string.Compare(DatabaseType, "REMDB", true) == 0)
            {
                if (_RemoteConnection == null || _RemoteConnection.State == ConnectionState.Closed)
                {
                    if (ConnectionString == string.Empty)
                    {
                        GetConnectionString();
                    }
                    _RemoteConnection = new SqlConnection();
                    _RemoteConnection.ConnectionString = ConnectionString;
                    try
                    {
                        _RemoteConnection.Open();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        ConnectionString = string.Empty;
                        return false;
                    }
                    ConnectionString = string.Empty;
                }
            }

            return true;
        }

        public bool GetConnection()
        {
            string ConnectionString = string.Empty;
            if (_DatabaseConnection == null || _DatabaseConnection.State == ConnectionState.Closed)
            {
                if (ConnectionString == string.Empty)
                {
                    //ConnectionString = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString.ToString();                    
                }
                _DatabaseConnection = new SqlConnection();
                //_DatabaseConnection.ConnectionString = ConnectionString;
                ConnectionString = @"Data Source=IBM-5163992E35F\NetSDK;Initial Catalog=Reservation;Integrated Security=True";
                _DatabaseConnection.ConnectionString = ConnectionString;
                try
                {
                    _DatabaseConnection.Open();
                }
                catch (Exception ex)
                {
                    GenFunctions.LogError("clsDatabaseManager.GetConnection", ex.Message.ToString());
                    ConnectionString = string.Empty;
                    return false;
                }
                ConnectionString = string.Empty;
            }
            return true;
        }

        public string GetConnectionString(string DatabaseType)
        {
            //if (GenFunctions.GetDatabaseType() == 'S')
            //{
            //    clsXMLManager objXMLManager = new clsXMLManager();
            //    return objXMLManager.GetConnectionString(DatabaseType);
            //}
            //else
            return "";
        }

        public string GetConnectionString()
        {
            //if (GenFunctions.GetDatabaseType() == 'M')
            //{
            //    return ConfigurationManager.ConnectionStrings["ChildCareConnectionString"].ConnectionString;
            //}
            //else
            return "";
        }

        public void InitializeDatabaseConnection()
        {
            if (_DatabaseConnection == null || _DatabaseConnection.State == ConnectionState.Closed)
            {
                //if (GenFunctions.GetDatabaseType() == 'M')
                GetConnection();
                //else if (GenFunctions.GetDatabaseType() == 'S')
                //  GetConnection("APPDB");
                //if (_DatabaseConnection.State == ConnectionState.Connecting)
                //  InitializeDatabaseConnection();
            }
        }

        private void InitializeSiteDatabaseConnection()
        {
            if (_RemoteConnection == null || _RemoteConnection.State == ConnectionState.Closed)
                GetConnection("REMDB");
        }

        public void FetchData(String TableName)
        {
            InitializeDatabaseConnection();
            dAdapter = new SqlDataAdapter("select * from " + TableName, ApplicationConnection);
            dSet = new DataSet();
            //refreshes rows in the DataSet 			
            RefreshDataSet(TableName);
        }

        public void RefreshDataSet(String TableName)
        {
            InitializeDatabaseConnection();
            dSet.Clear();  //clear the contents of dataset            
            dAdapter.Fill(dSet, TableName);
        }

        public void FillDataAdaptor(string strQuery, string DataSetTableName)
        {
            InitializeDatabaseConnection();
            dAdapter = new SqlDataAdapter(strQuery, ApplicationConnection);
            dSet = new DataSet();
            //refreshes rows in the DataSet 			
            dAdapter.Fill(dSet, DataSetTableName);
        }

        public bool ExecuteQuery(String strActionString)
        {
            InitializeDatabaseConnection();
            try
            {
                SqlCommand SC = new SqlCommand(strActionString, ApplicationConnection);
                SC.CommandType = CommandType.Text;
                SC.ExecuteNonQuery();
                return true;
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
                GenFunctions.LogError("clsDatabaseManager.ExecuteQuery", E.Message);
                return false;
            }
        }

        public bool ExecuteQuery(String strActionString, SqlConnection Conn)
        {
            try
            {
                InitializeDatabaseConnection();
                SqlCommand SC = new SqlCommand(strActionString, Conn);
                SC.CommandType = CommandType.Text;
                SC.ExecuteNonQuery();
                return true;
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
                return false;
            }
        }

        public long GetMax(String TableName, String FieldName, bool FieldIsInteger)
        {
            if (GenFunctions.GetDatabaseType() == 'S')
            {
                if (FieldIsInteger == true)
                    strQuery = "Select Max(cast(" + FieldName + " as Int))  from " + TableName;
                else
                    strQuery = "Select Max(" + FieldName + ")  from " + TableName;
            }
            else if (GenFunctions.GetDatabaseType() == 'M')
            {
                strQuery = "Select Max(" + FieldName + ")  from " + TableName;
            }
            return GetSqlFunctionValue(strQuery);
        }

        public long GetCount(string TableName, string FieldName, string FieldValue, bool FieldIsInteger)
        {
            strQuery = "Select count(*)  from " + TableName + " where " + FieldName + " = ";
            if (FieldIsInteger == false)
                strQuery += "'" + FieldValue + "'";
            else
                strQuery += "" + FieldValue + "";

            return GetSqlFunctionValue(strQuery);
        }

        public string GetColumnValue(string strQuery)
        {
            string ColumnValue;
            InitializeDatabaseConnection();
            SqlCommand cmd = new SqlCommand(strQuery, this.ApplicationConnection);
            SqlDataReader reader;
            //Execute the command and get the DataReader
            reader = cmd.ExecuteReader();
            reader.Read();
            //GetValue(0) means: The contents of the first column(0)(PersonID) in the table
            //object obValue = reader.GetValue(0);
            if (reader.HasRows == true)
            {
                if (reader.IsDBNull(0))
                {
                    ColumnValue = string.Empty;
                }
                else
                {
                    ColumnValue = Convert.ToString(reader.GetValue(0));
                }
            }
            else
            {
                ColumnValue = string.Empty;
            }
            reader.Close(); //close the DataReader otherwise error
            return ColumnValue;
        }

        public bool IsRecordExists(string TableName, string WhereClause)
        {
            strQuery = "Select Count(0)  from " + TableName + " Where 1=1 and " + WhereClause;

            if (GetSqlFunctionValue(strQuery) == 0)
                return false;
            else
                return true;
        }

        private long GetSqlFunctionValue(string strQuery)
        {
            long iItemid = 0;
            InitializeDatabaseConnection();
            SqlCommand cmd = new SqlCommand(strQuery, this.ApplicationConnection);
            SqlDataReader reader;
            //Execute the command and get the DataReader
            reader = cmd.ExecuteReader();
            reader.Read();
            //GetValue(0) means: The contents of the first column(0)(PersonID) in the table
            //object obValue = reader.GetValue(0);

            if (reader.HasRows == true)
            {
                if (reader.IsDBNull(0))
                {
                    iItemid = 0;
                }
                else
                {
                    iItemid = Convert.ToInt32(reader.GetValue(0));
                }
            }
            else
            {
                iItemid = 0;
            }

            reader.Close(); //close the DataReader otherwise error
            return iItemid;
        }

        public DataSet FetchRecords(string TableName, string Query)
        {

            SqlDataAdapter objDataAdapter;
            DataSet objDataSet = null;
            try
            {
                InitializeDatabaseConnection();
                objDataAdapter = new SqlDataAdapter(Query, ApplicationConnection);
                objDataSet = new DataSet();

                //objDataSet.Clear();  //clear the contents of dataset            
                objDataAdapter.Fill(objDataSet, TableName);
                
            }
            catch (Exception e)
            {

            }
            return objDataSet;
        }
       




        public SqlDataReader FetchRecords(string strQuery, SqlConnection Conn)
        {
            SqlCommand cmd = new SqlCommand(strQuery, Conn);
            SqlDataReader reader;
            //Execute the command and get the DataReader
            reader = cmd.ExecuteReader();
            reader.Read();
            return reader;
        }

        public DataSet FetchRecords(string TableName, string Query, SqlConnection Conn)
        {
            SqlDataAdapter objDataAdapter;
            DataSet objDataSet;
            InitializeDatabaseConnection();
            objDataAdapter = new SqlDataAdapter(Query, Conn);
            objDataSet = new DataSet();

            //objDataSet.Clear();  //clear the contents of dataset            
            objDataAdapter.Fill(objDataSet, TableName);
            return objDataSet;
        }

        public string ExecuteStoredProc(string StoredProcName, SqlParameter[] Parameters)
        {
            int OutputParameterIndex;
            OutputParameterIndex = 0;
            InitializeDatabaseConnection();
            SqlCommand cmd = new SqlCommand(StoredProcName, ApplicationConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < Parameters.Length; i++)
            {
                cmd.Parameters.Add(Parameters[i]);
                if (cmd.Parameters[i].Direction == ParameterDirection.Output)
                {
                    OutputParameterIndex = i;
                }
            }
            cmd.ExecuteNonQuery();
            return cmd.Parameters[OutputParameterIndex].Value.ToString();
        }

        public int ExecuteStoredProcedure(string StoredProcName, SqlParameter[] Parameters)
        {
            InitializeDatabaseConnection();
            SqlCommand cmd = new SqlCommand(StoredProcName, ApplicationConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < Parameters.Length; i++)
            {
                cmd.Parameters.Add(Parameters[i]);
                //if (cmd.Parameters[i].Direction == ParameterDirection.Output)
                //{
                //    OutputParameterIndex = i;
                //}
            }
            int retVal = Convert.ToInt32(cmd.ExecuteScalar());
            return retVal;
        }
        public DataSet GetDatasetFromProcedure(string procname, SqlParameter[] sParameters)
        {
            InitializeDatabaseConnection();
            try
            {
                
                SqlCommand SQLCom = new SqlCommand();
                SQLCom.Connection = ApplicationConnection;
                SQLCom.CommandType = CommandType.StoredProcedure;
                SQLCom.CommandText = procname;
                for (int i = 0; i < sParameters.Length; i++)
                {
                    SQLCom.Parameters.Add(sParameters[i]);
                }
                SqlDataAdapter l_da = new SqlDataAdapter(SQLCom);
                DataSet l_ds = new DataSet();
                l_da.Fill(l_ds);
                return l_ds;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
