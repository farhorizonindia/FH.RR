using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace FarHorizon.Reservations.DataBaseManager
{
    /// <summary>
    /// Summary description for DalBase.
    /// </summary>
    public class DatabaseHandler
    {
        //private Microsoft.Practices.EnterpriseLibrary.Data.Database dbDatabase;        
        private SqlConnection _dbConnection;
        //private System.Data.Common.DbTransaction dbTrx;
        private SqlCommand _dbCmd;
        private SqlDataAdapter _dbDataAdapter;
        public DatabaseWrapper DbDatabase { get; set; }

        private string _connectionString { get; set; }

        #region Properties
        /// <summary>
        /// Get the current database connection
        /// </summary>
        public SqlConnection CurrentConnection
        {
            get
            {
                if (_dbConnection == null)
                {
                    _dbConnection = new SqlConnection(_connectionString);
                    _dbConnection.Open();
                }
                if (_dbConnection.State == ConnectionState.Closed)
                {
                    _dbConnection.Open();
                }
                return this._dbConnection;
            }
        }

        public DbDataAdapter DataAdapter
        {
            get
            {
                if (_dbDataAdapter == null)
                {
                    _dbDataAdapter = new SqlDataAdapter();
                }
                return _dbDataAdapter;
            }
        }

        /// <summary>
        /// Get and set the IDbCommand type object
        /// </summary>
        public SqlCommand DbCmd
        {
            get
            {
                if (_dbCmd == null)
                {
                    _dbCmd = new SqlCommand();
                    _dbCmd.Connection = CurrentConnection;
                }
                return this._dbCmd;
            }
            set { _dbCmd = value; }
        }

        public void CloseCurrentConnection()
        {
            if (CurrentConnection != null)
                CurrentConnection.Close();
        }       

        /// <summary>
        /// Get the Command Timeout Value from Configuration file
        /// </summary>
        public static int CommandTimeoutVal
        {
            get
            {
                try
                {
                    // Read from the Application Configuration
                    //CISConfigurationElement calcData;
                    ////calcData = ConfigurationManager.GetConfiguration("CS3CalcEngineSettings") as CS3CalcEngineData;
                    //calcData = ConfigurationManager.GetConfiguration("CISSettings") as CISConfigurationElement;
                    //return calcData.CommandTimeoutVal;
                    return 999;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor used to create the DalBase class object 
        /// </summary>
        //public DatabaseHandler()
        //{
        //    try
        //    {
        //        //Initialize the dbDatabase object 
        //        this.dbDatabase = DatabaseFactory.CreateDatabase();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        /// <summary>
        /// Constructor used to create the DalBase class object based on instance name
        /// </summary>
        /// <param name="connectionString">Instance name</param>
        public DatabaseHandler(string connectionString)
        {
            _connectionString = connectionString;
            DbDatabase = new DatabaseWrapper();
        }

        #endregion

        #region ExecuteReader

        #endregion

        #region ExecuteDataSet
        /// <summary>
        /// Execute the SQL query and return the results in a new DataSet.
        /// </summary>
        /// <param name="theCmdStr">SQL Query</param>
        /// <returns>DataSet object</returns>
        public System.Data.DataSet ExecuteDataSet(string theCmdStr)
        {
            System.Data.DataSet ds = null;
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(theCmdStr, CurrentConnection);
                adapter.Fill(ds, "dataSet");
                CurrentConnection.Close();               
            }
            catch
            {
                throw;
            }
            return ds;
        }        

        /// <summary>
        /// Execute the command and return the results in a new DataSet.
        /// </summary>
        /// <param name="dbCommandWrapper">The DbCommand to execute.</param>
        /// <returns>A DataSet with the results of the command</returns>
        public System.Data.DataSet ExecuteDataSet(SqlCommand dbCommandWrapper)
        {
            System.Data.DataSet ds = null;
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(dbCommandWrapper);
                ds = new DataSet();
                adapter.Fill(ds, "dataSet");
                CloseCurrentConnection();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ds;
        }

        #endregion

        #region "ExecuteNonQuery"
        /// <summary>
        /// Executes the sql query
        /// </summary>
        /// <param name="theCmdStr">SQL Query</param>
        public void ExecuteNonQuery(System.String theCmdStr)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(theCmdStr, CurrentConnection))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();                    
                }
                CloseCurrentConnection();
                
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="dbCommandWrapper">The command that contains the query or storedprocedure to execute.</param>
        public void ExecuteNonQuery(SqlCommand dbCommandWrapper)
        {
            try
            {
                using (SqlCommand cmd = dbCommandWrapper)
                {
                    cmd.CommandTimeout = CommandTimeoutVal;
                    cmd.ExecuteNonQuery();
                }
                CloseCurrentConnection();                
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion

        #region "ExecuteScalar"

        /// <summary>
        /// Executes the command and returns the first column of the first row in the resultset returned by 
        /// the query. Extra columns or rows are ignored.
        /// </summary>
        /// <param name="dbCommandWrapper">The command that contains the query to execute.</param>
        /// <returns>The first column of the first row in the resultset.</returns>
        public System.Object ExecuteScalar(SqlCommand dbCommandWrapper)
        {
            System.Object obj = null;
            try
            {
                using (SqlCommand cmd = dbCommandWrapper)
                {
                    cmd.CommandTimeout = CommandTimeoutVal;
                    obj = cmd.ExecuteScalar();
                }
                CloseCurrentConnection();                
            }
            catch
            {
                throw;
            }
            return obj;
        }        
        #endregion

        #region GetStoredProcCommand
        /// <summary>
        /// Creates a DbCommand for a stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">The name of the stored procedure.</param>
        /// <returns>The DbCommand for the stored procedure.</returns>
        public SqlCommand GetStoredProcCommand(string storedProcedureName)
        {
            SqlCommand dbc = new SqlCommand
            {
                Connection = CurrentConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = storedProcedureName,
                CommandTimeout = CommandTimeoutVal
            };
            return dbc;            
        }

        /// <summary>
        /// Creates an DbCommand for a SQL query.
        /// </summary>
        /// <param name="query">The text of the query.</param>        
        /// <returns>The DbCommand for the SQL query.</returns>
        public SqlCommand GetSqlStringCommand(string sqlQuery)
        {
            SqlCommand dbc = new SqlCommand
            {
                Connection = CurrentConnection,
                CommandType = CommandType.Text,
                CommandText = sqlQuery,
                CommandTimeout = CommandTimeoutVal
            };
            return dbc;            
        }
        
        #endregion

        #region Transaction Methods
        

        #endregion
    }

    public class DatabaseWrapper
    {
        public void AddInParameter(SqlCommand cmd, string parameterName, DbType dbType)
        {
            var param = new SqlParameter
            {
                ParameterName = parameterName,
                Direction = ParameterDirection.Input,
                DbType = dbType
            };
            cmd.Parameters.Add(param);
        }

        public void AddInParameter(SqlCommand cmd, string parameterName, DbType dbType, object value)
        {
            var param = new SqlParameter
            {
                ParameterName = parameterName,
                DbType = dbType,
                Direction = ParameterDirection.Input,
                Value = value
            };
            cmd.Parameters.Add(param);
        }

        public void AddOutParameter(SqlCommand cmd, string parameterName, DbType dbType, object value)
        {
            var param = new SqlParameter
            {
                ParameterName = parameterName,
                DbType = dbType,
                Direction = ParameterDirection.Output,
                Value = value
            };
            cmd.Parameters.Add(param);
        }
    }
}
