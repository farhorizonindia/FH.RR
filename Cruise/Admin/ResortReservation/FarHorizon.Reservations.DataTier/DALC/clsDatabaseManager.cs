/***********************************************************************************************************
*File Name:- FHDataBase.cs
*
*File Description:- File will act as Wrraper class to expose the Enterprise Library Database class methods.
*Or in other words this class contains the all the functions requried for accessing database.
*Author:- Parag V. Pratapwar
*Creation Date:- 19 May 2006
*Modified By:- 
*Modified Date:-
*Reason for Modification:-
*************************************************************************************************************/

using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;
using System.Resources;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Configuration;

using DataLayer.Configuration;

namespace DataLayer.DALC
{
	/// <summary>
	/// Summary description for DalBase.
	/// </summary>
	public class clsDatabaseManager
	{
		private Database dbDatabase;
		private DBCommandWrapper dbCommandWrapper;
		private System.Data.IDbConnection dbConnection;
		private System.Data.IDbTransaction dbTrx;
		private System.Data.IDbCommand dbCmd;
		
		#region Properties
		/// <summary>
		/// Get the current database connection
		/// </summary>
		public System.Data.IDbConnection CurrentConnection
		{
			get
			{
				return this.dbConnection;
			}
		}

		/// <summary>
		/// Get the current transaction
		/// </summary>
		public System.Data.IDbTransaction CurrentTransaction
		{
			get
			{
				return this.dbTrx;
			}
		}

		/// <summary>
		/// Gets and sets the DBCommandWrapper object
		/// </summary>		
		public DBCommandWrapper DbCommandWrapper
		{
			get
			{
				return this.dbCommandWrapper;
			}

			set
			{
				this.dbCommandWrapper = value;
			}
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
					FHConfigurationElement calcData;
					//calcData = ConfigurationManager.GetConfiguration("CS3CalcEngineSettings") as CS3CalcEngineData;
					calcData = ConfigurationManager.GetConfiguration("FHSettings") as FHConfigurationElement;
					return calcData.CommandTimeoutVal;
				}
				catch(Exception ex)
				{
					throw ex;
				}
			}
		}

		/// <summary>
		/// Get and set the IDbCommand type object
		/// </summary>
		private System.Data.IDbCommand DbCmd
		{
			get
			{
				return this.dbCmd;
			}

			set
			{
				this.dbCmd = value;
			}
		}

		/// <summary>
		/// Get and set the database connection in the form of IDbConnection
		/// </summary>
		private System.Data.IDbConnection DbCn
		{
			get
			{
				return this.dbConnection;
			}

			set
			{
				this.dbConnection = value;
			}
		}

		/// <summary>
		/// Gets and sets the IDbTransaction
		/// </summary>
		private System.Data.IDbTransaction DbTrx
		{
			get
			{
				return this.dbTrx;
			}

			set
			{
				this.dbTrx = value;
			}
		}

		/// <summary>
		/// Gets and Sets the Database object
		/// </summary>
		private Database DbDatabase
		{
			get
			{
				return this.dbDatabase;
			}

			set
			{
				this.dbDatabase = value;
			}
		}


		#endregion

		#region Constructor
		/// <summary>
		/// Constructor used to create the DalBase class object 
		/// </summary>
		public clsDatabaseManager()
		{
			try
			{
				//Initialize the dbDatabase object 
				this.dbDatabase = DatabaseFactory.CreateDatabase();
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Constructor used to create the DalBase class object based on instance name
		/// </summary>
		/// <param name="theDbName">Instance name</param>
		public clsDatabaseManager(String theDbName)
		{
			try
			{
				this.dbDatabase = DatabaseFactory.CreateDatabase(theDbName);
			}
			catch
			{
				throw;
			}
		}
		
		#endregion

		#region ExecuteReader

		/// <summary>
		/// Execute the commandText interpreted as specified by the 
		/// commandType within the given transaction and returns an IDataReader 
		/// through which the result can be read. 
		/// It is the responsibility of the caller to close the
		/// reader when finished.
		/// </summary>
		/// <param name="transaction">The IDbTransaction to execute the command within.</param>
		/// <param name="commandType">One of the CommandType values.</param>
		/// <param name="commandText">The command text to execute.</param>
		/// <returns>An IDataReader object.</returns>
		public System.Data.IDataReader ExecuteReader (IDbTransaction transaction,CommandType commandType,string commandText)
		{
			System.Data.IDataReader idr = null;

			try
			{
				idr = this.dbDatabase.ExecuteReader(transaction,commandType,commandText);			
			}
			catch
			{
				throw;
			}
			return idr;		
		}

		/// <summary>
		/// Executes the storedProcedureName with the given parameterValues within the 
		/// given transaction and returns an IDataReader through which the result 
		/// can be read. It is the responsibility of the caller to close the 
		/// reader when finished.
		/// </summary>
		/// <param name="transaction">The IDbTransaction to execute the command within</param>
		/// <param name="stroredProcedureName">The command that contains the query to execute.</param>
		/// <param name="parameterValues">
		/// An array of paramters to pass to the stored procedure. The parameter values must 
		/// be in call order as they appear in the stored procedure
		/// </param>
		/// <returns>An IDataReader object.</returns>
		public System.Data.IDataReader ExecuteReader(IDbTransaction transaction,string stroredProcedureName, object[] parameterValues)
		{
			System.Data.IDataReader idr = null;
			try
			{
				idr = this.dbDatabase.ExecuteReader(transaction,stroredProcedureName,parameterValues);
			}
			catch
			{
				throw;
			}
			return idr;
		}

		/// <summary>
		/// Executes the command within a transaction and returns an IDataReader 
		/// through which the result can be read. It is the responsibility of the 
		/// caller to close the reader when finished.
		/// </summary>
		/// <param name="command">The command that contains the query to execute.</param>
		/// <param name="transaction">The IDbTransaction to execute the command within</param>
		/// <returns>An IDataReader object</returns>
		public System.Data.IDataReader ExecuteReader(DBCommandWrapper command,IDbTransaction transaction)
		{
			System.Data.IDataReader idr = null;
			try
			{
				idr =this.dbDatabase.ExecuteReader(command,transaction);
			}
			catch
			{
				throw;
			}
			return idr;
		}

		/// <summary>
		/// Executes the storedProcedureName with the given parameterValues and returns 
		/// an IDataReader through which the result can be read. It is the 
		/// responsibility of the caller to close the 
		/// reader when finished.
		/// </summary>
		/// <param name="storedProcedureName">The command that contains the query to execute</param>
		/// <param name="parameterValues">
		/// An array of paramters to pass to the stored procedure. The parameter 
		/// values must be in call order as they appear in the stored procedure.
		/// </param>
		/// <returns>An IDataReader object.</returns>
		public System.Data.IDataReader ExecuteReader (string storedProcedureName,object[] parameterValues)
		{
			System.Data.IDataReader idr = null;
			try
			{
				idr = this.dbDatabase.ExecuteReader(storedProcedureName,parameterValues);
			}
			catch
			{
				throw;
			}
			return idr;
		}

		/// <summary>
		/// Executes the query theCmdStr and returns the IDataReader
		/// It is the responsibility of the caller to close the reader when finished.
		/// </summary>
		/// <param name="theCmdStr">The text of the query.</param>
		/// <returns>IDataReader object</returns>
		public System.Data.IDataReader ExecuteReader ( System.String theCmdStr )
		{
			System.Data.IDataReader idr = null;
			try
			{
				this.dbCommandWrapper = this.dbDatabase.GetSqlStringCommandWrapper(theCmdStr);
				this.dbCommandWrapper.CommandTimeout = CommandTimeoutVal;
				idr = this.dbDatabase.ExecuteReader(this.dbCommandWrapper);
			}
			catch
			{
				throw;
			}
			return idr;
		}

		/// <summary>
		/// Executes the query theCmdStr and returns the IDataReader 
		/// by setting TimeOut Value for query execution.
		/// It is the responsibility of the caller to close the reader when finished.
		/// </summary>
		/// <param name="theCmdStr">The text of the query.</param>
		/// <param name="timeOutVal">Query Timeout value</param>
		/// <returns>IDataReader object</returns>
		public System.Data.IDataReader ExecuteReader ( System.String theCmdStr ,int timeOutVal)
		{
			System.Data.IDataReader idr = null;
			try
			{
				this.dbCommandWrapper = this.dbDatabase.GetSqlStringCommandWrapper(theCmdStr);
				this.dbCommandWrapper.CommandTimeout = timeOutVal;
				idr = this.dbDatabase.ExecuteReader(this.dbCommandWrapper);
				

			}
			catch
			{
				throw;
			}
			return idr;
		}

		/// <summary>
		/// Executes the command and returns an IDataReader through which the result can be read. 
		/// It is the responsibility of the caller to close the reader when finished.
		/// </summary>
		/// <param name="dbCommandWrapper">The command that contains the query to execute</param>
		/// <returns>IDataReader object</returns>
		public System.Data.IDataReader ExecuteReader ( DBCommandWrapper dbCommandWrapper)
		{
			System.Data.IDataReader idr = null;
			try
			{
				this.dbCommandWrapper = dbCommandWrapper;
				this.dbCommandWrapper.CommandTimeout = CommandTimeoutVal;
				idr = this.dbDatabase.ExecuteReader(this.dbCommandWrapper);
//				System.Collections.IDictionaryEnumerator id =  System.Web.HttpContext.Current.Cache.GetEnumerator();
//				while(id.MoveNext())
//				{
//					id.Key.ToString();
//				}
			}
			catch
			{
				throw;
			}
			return idr;
		}

		/// <summary>
		/// Executes the command and returns an IDataReader through which the result can be read by setting the
		/// command time out value.
		/// </summary>
		/// <param name="dbCommandWrapper">The command that contains the query to execute</param>
		/// <param name="timeOutVal">Command time out value</param>
		/// <returns>IDataReader object</returns>
		public System.Data.IDataReader ExecuteReader ( DBCommandWrapper dbCommandWrapper ,int timeOutVal)
		{
			System.Data.IDataReader idr = null;
			try
			{
				this.dbCommandWrapper = dbCommandWrapper;
				this.dbCommandWrapper.CommandTimeout = timeOutVal;
				idr = this.dbDatabase.ExecuteReader(this.dbCommandWrapper);
			}
			catch
			{
				throw;
			}
			return idr;
		}
	
		
		#endregion

		#region ExecuteDataSet

		/// <summary>
		/// Execute the commandText as part of the given transaction 
		/// and return the results in a new DataSet.
		/// </summary>
		/// <param name="transaction">The IDbTransaction to execute the command within.</param>
		/// <param name="commandType">One of the CommandType values.</param>
		/// <param name="commandText">The command text to execute.</param>
		/// <returns>A DataSet with the results of the commandText</returns>
		public System.Data.DataSet ExecuteDataSet(IDbTransaction transaction,CommandType commandType,string commandText)
		{
			System.Data.DataSet ds = null;
			try
			{
				ds = this.dbDatabase.ExecuteDataSet(transaction,commandType,commandText);
			}
			catch
			{
				throw;
			}
			return ds;
		}

		/// <summary>
		/// Execute the storedProcedureName ith parameter Values as part of the transaction and 
		/// return the results in a new DataSet within a transaction.
		/// </summary>
		/// <param name="transaction">The IDbTransaction to execute the command within.</param>
		/// <param name="stroredProcedureName">The stored procedure to execute.</param>
		/// <param name="parameterValues">
		/// An array of paramters to pass to the stored procedure. 
		/// The parameter values must be in call order as they appear in the stored procedure.</param>
		/// <returns>A DataSet with the results of the storedProcedureName</returns>
		public System.Data.DataSet ExecuteDataSet(IDbTransaction transaction,string stroredProcedureName, object[] parameterValues)
		{
			System.Data.DataSet ds = null;
			try
			{
				ds = this.dbDatabase.ExecuteDataSet(transaction,stroredProcedureName,parameterValues);
			}
			catch
			{
				throw;
			}
			return ds;
		}

		/// <summary>
		/// Execute the command as part of the transaction and return the results in a new DataSet.
		/// </summary>
		/// <param name="command">The DBCommandWrapper to execute.</param>
		/// <param name="transaction">The IDbTransaction to execute the command within.</param>
		/// <returns>A DataSet with the results of the command.</returns>
		public System.Data.DataSet ExecuteDataSet(DBCommandWrapper command,IDbTransaction transaction)
		{
			System.Data.DataSet ds = null;
			try
			{
				ds = this.dbDatabase.ExecuteDataSet(command,transaction);
			}
			catch
			{
				throw ;
			}
			return ds;
		}

		/// <summary>
		/// Execute the storedProcedureName with parameterValues and return the results in a new DataSet.
		/// </summary>
		/// <param name="storedProcedureName">The stored procedure to execute.</param>
		/// <param name="parameterValues">
		/// An array of paramters to pass to the stored procedure. The parameter values must be in 
		/// call order as they appear in the stored procedure.
		/// </param>
		/// <returns>A DataSet with the results of the storedProcedureName</returns>
		public System.Data.DataSet ExecuteDataSet(string storedProcedureName,object[] parameterValues)
		{
			System.Data.DataSet ds = null;
			try
			{
				ds = this.dbDatabase.ExecuteDataSet(storedProcedureName,parameterValues);
			}
			catch
			{
				throw;
			}
			return ds;
		}

		/// <summary>
		/// Execute the SQL query and return the results in a new DataSet.
		/// </summary>
		/// <param name="theCmdStr">SQL Query</param>
		/// <returns>DataSet object</returns>
		public System.Data.DataSet ExecuteDataSet (System.String theCmdStr)
		{
			System.Data.DataSet ds = null;
			try
			{
				this.dbCommandWrapper = this.dbDatabase.GetSqlStringCommandWrapper(theCmdStr);
				this.dbCommandWrapper.CommandTimeout = CommandTimeoutVal;
				ds = this.dbDatabase.ExecuteDataSet(this.dbCommandWrapper);
			}
			catch
			{
				throw;
			}
			return ds;
		}

		/// <summary>
		/// Execute the SQL query by setting command timeout and return the results in a new DataSet
		/// </summary>
		/// <param name="theCmdStr">SQL Query</param>
		/// <param name="timeOutVal">Command timeout value</param>
		/// <returns>DataSet object</returns>
		public System.Data.DataSet ExecuteDataSet (System.String theCmdStr ,int timeOutVal)
		{
			System.Data.DataSet ds = null;
			try
			{
				this.dbCommandWrapper = this.dbDatabase.GetSqlStringCommandWrapper(theCmdStr);
				this.dbCommandWrapper.CommandTimeout = timeOutVal;
				ds = this.dbDatabase.ExecuteDataSet(this.dbCommandWrapper);
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
		/// <param name="dbCommandWrapper">The DBCommandWrapper to execute.</param>
		/// <returns>A DataSet with the results of the command</returns>
		public System.Data.DataSet ExecuteDataSet (DBCommandWrapper dbCommandWrapper)
		{
			System.Data.DataSet ds = null;
			try
			{
				this.dbCommandWrapper = dbCommandWrapper;
				this.dbCommandWrapper.CommandTimeout = CommandTimeoutVal;
				ds = this.dbDatabase.ExecuteDataSet(this.dbCommandWrapper);
			}
			catch
			{
				throw;
			}
			return ds;
		}

		/// <summary>
		/// Execute the command by setting the command timeout value and return the results in a new DataSet.
		/// </summary>
		/// <param name="dbCommandWrapper">The DBCommandWrapper to execute.</param>
		/// <param name="timeOutVal">command timeout value</param>
		/// <returns>A DataSet with the results of the command.</returns>
		public System.Data.DataSet ExecuteDataSet (DBCommandWrapper dbCommandWrapper ,int timeOutVal)
		{
			System.Data.DataSet ds = null;
			try
			{
				this.dbCommandWrapper = dbCommandWrapper;
				this.dbCommandWrapper.CommandTimeout = timeOutVal;
				ds = this.dbDatabase.ExecuteDataSet(this.dbCommandWrapper);
			}
			catch
			{
				throw;
			}
			return ds;
		}


		#endregion

		#region "ExecuteNonQuery"
		
		/// <summary>
		/// Execute the commandText interpreted as specified by the commandType as part of the given 
		/// transaction and return the number of rows affected
		/// </summary>
		/// <param name="transaction">The IDbTransaction to execute the command within.</param>
		/// <param name="commandType">One of the CommandType values.</param>
		/// <param name="commandText">The command text to execute.</param>
		/// <returns>The number of rows affected</returns>
		public int ExecuteNonQuery (IDbTransaction transaction,CommandType commandType,string commandText)
		{
			try
			{
				return this.dbDatabase.ExecuteNonQuery(transaction,commandType,commandText);
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Executes the storedProcedureName using the given parameterValues within a transaction and 
		/// returns the number of rows affected.
		/// </summary>
		/// <param name="transaction">The IDbTransaction to execute the command within.</param>
		/// <param name="stroredProcedureName">The command that contains the query to execute.</param>
		/// <param name="parameterValues">
		/// An array of paramters to pass to the stored procedure. The parameter values must be in call 
		/// order as they appear in the stored procedure.
		/// </param>
		/// <returns>The number of rows affected</returns>
		public int ExecuteNonQuery(IDbTransaction transaction,string stroredProcedureName, object[] parameterValues)
		{
			try
			{
				return this.dbDatabase.ExecuteNonQuery(transaction,stroredProcedureName,parameterValues);
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Executes the command within the given transaction, and returns the number of rows affected.
		/// </summary>
		/// <param name="command">The command that contains the query to execute.</param>
		/// <param name="transaction">The IDbTransaction to execute the command within.</param>
		public void ExecuteNonQuery(DBCommandWrapper command,IDbTransaction transaction)
		{
			try
			{
				this.dbDatabase.ExecuteNonQuery(command,transaction);
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Executes the storedProcedureName using the given parameterValues and returns the number of 
		/// rows affected.
		/// </summary>
		/// <param name="storedProcedureName">The command that contains the query to execute.</param>
		/// <param name="parameterValues">
		/// An array of paramters to pass to the stored procedure. The parameter values must be in call 
		/// order as they appear in the stored procedure.
		/// </param>
		/// <returns>The number of rows affected</returns>
		public int ExecuteNonQuery (string storedProcedureName,object[] parameterValues)
		{
			try
			{
				return this.dbDatabase.ExecuteNonQuery(storedProcedureName,parameterValues);
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Executes the sql query
		/// </summary>
		/// <param name="theCmdStr">SQL Query</param>
		public void ExecuteNonQuery ( System.String theCmdStr )
		{	 
			try
			{
				this.dbCommandWrapper = this.dbDatabase.GetSqlStringCommandWrapper(theCmdStr);
				this.dbCommandWrapper.CommandTimeout = CommandTimeoutVal;
				this.dbDatabase.ExecuteNonQuery(this.dbCommandWrapper);
			}
			catch
			{
				throw;
			}
		}
		
		/// <summary>
		/// Executes the sql query by setting command timeout 
		/// </summary>
		/// <param name="theCmdStr">SQL Query</param>
		/// <param name="timeOutVal">Command timeout</param>
		public void ExecuteNonQuery ( System.String theCmdStr ,int timeOutVal)
		{	 
			try
			{
				this.dbCommandWrapper = this.dbDatabase.GetSqlStringCommandWrapper(theCmdStr);
				this.dbCommandWrapper.CommandTimeout = timeOutVal;
				this.dbDatabase.ExecuteNonQuery(this.dbCommandWrapper);
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Executes the command.
		/// </summary>
		/// <param name="dbCommandWrapper">The command that contains the query or storedprocedure to execute.</param>
		public void ExecuteNonQuery (DBCommandWrapper dbCommandWrapper)
		{	 
			try
			{
				this.dbCommandWrapper = dbCommandWrapper;
				this.dbCommandWrapper.CommandTimeout = CommandTimeoutVal;
				this.dbDatabase.ExecuteNonQuery(this.dbCommandWrapper);			
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Executes the command by setting the command timeout.
		/// </summary>
		/// <param name="dbCommandWrapper">The command that contains the query or storedprocedure to execute.</param>
		/// <param name="timeOutVal">Command timeout</param>
		public void ExecuteNonQuery ( DBCommandWrapper dbCommandWrapper ,int timeOutVal)
		{	 
			try
			{
				this.dbCommandWrapper = dbCommandWrapper;
				this.dbCommandWrapper.CommandTimeout = timeOutVal;
				this.dbDatabase.ExecuteNonQuery(this.dbCommandWrapper);
			}
			catch
			{
				throw;
			}
		}

		
		#endregion

		#region "ExecuteScalar"

		/// <summary>
		/// Executes the commandText interpreted as specified by the commandType within the given transaction 
		/// and returns the first column of the first row in the resultset returned by the query. Extra 
		/// columns or rows are ignored.
		/// </summary>
		/// <param name="transaction">The IDbTransaction to execute the command within.</param>
		/// <param name="commandType">One of the CommandType values.</param>
		/// <param name="commandText">The command text to execute</param>
		/// <returns>The first column of the first row in the resultset.</returns>
		public System.Object ExecuteScalar(IDbTransaction transaction,CommandType commandType,string commandText)
		{
			System.Object obj = null;
			try
			{
				obj = this.dbDatabase.ExecuteScalar(transaction,commandType,commandText);
			}
			catch
			{
				throw;
			}
			return obj;
		}

		/// <summary>
		/// Executes the storedProcedureName with the given parameterValues within a transaction and returns 
		/// the first column of the first row in the resultset returned by the query. Extra columns or rows 
		/// are ignored.
		/// </summary>
		/// <param name="transaction">The IDbTransaction to execute the command within.</param>
		/// <param name="stroredProcedureName">The stored procedure to execute.</param>
		/// <param name="parameterValues">
		/// An array of paramters to pass to the stored procedure. The parameter values must be in call 
		/// order as they appear in the stored procedure.
		/// </param>
		/// <returns>The first column of the first row in the resultset.</returns>
		public System.Object ExecuteScalar(IDbTransaction transaction,string stroredProcedureName, object[] parameterValues)
		{
			System.Object obj = null;
			try
			{
				obj = this.dbDatabase.ExecuteScalar(transaction,stroredProcedureName,parameterValues);
			}
			catch
			{
				throw;
			}
			return obj;
		}
		/// <summary>
		/// Executes the command within a transaction, and returns the first column of the first row in the 
		/// resultset returned by the query. Extra columns or rows are ignored.
		/// </summary>
		/// <param name="command">The command that contains the query to execute.</param>
		/// <param name="transaction">The IDbTransaction to execute the command within.</param>
		/// <returns>The first column of the first row in the resultset.</returns>
		public System.Object  ExecuteScalar(DBCommandWrapper command,IDbTransaction transaction)
		{
			System.Object obj= null;
			try
			{
				obj = this.dbDatabase.ExecuteScalar(command,transaction);
			}
			catch
			{
				throw;
			}
			return obj;
		}
		/// <summary>
		/// Executes the storedProcedureName with the given parameterValues and returns the first column of the 
		/// first row in the resultset returned by the query. Extra columns or rows are ignored.
		/// </summary>
		/// <param name="storedProcedureName">The stored procedure to execute.</param>
		/// <param name="parameterValues">
		/// An array of paramters to pass to the stored procedure. The parameter values must be in call order as 
		/// they appear in the stored procedure.
		/// </param>
		/// <returns>The first column of the first row in the resultset.</returns>
		public System.Object  ExecuteScalar(string storedProcedureName,object[] parameterValues)
		{
			System.Object obj = null;
			try
			{
				obj = this.dbDatabase.ExecuteScalar(storedProcedureName,parameterValues);
			}
			catch
			{
				throw;
			}
			return obj;
		}
		/// <summary>
		/// Executes the sql query and returns the first column of the 
		/// first row in the resultset returned by the query. Extra columns or rows are ignored.
		/// </summary>
		/// <param name="theCmdStr">SQL Query</param>
		/// <returns>The first column of the first row in the resultset.</returns>
		public System.Object ExecuteScalar ( System.String theCmdStr )
		{
			System.Object obj = null;
			try
			{
				this.dbCommandWrapper = this.dbDatabase.GetSqlStringCommandWrapper(theCmdStr);
				this.dbCommandWrapper.CommandTimeout = CommandTimeoutVal;
				obj = this.dbDatabase.ExecuteScalar(this.dbCommandWrapper);
			}
			catch
			{
				throw;
			}
			return obj;
		}

		/// <summary>
		/// Executes the sql query by setting command timeout and returns the first column of the 
		/// first row in the resultset returned by the query. Extra columns or rows are ignored.
		/// </summary>
		/// <param name="theCmdStr">SQL Query</param>
		/// <param name="timeOutVal">Command Timeout</param>
		/// <returns>The first column of the first row in the resultset.</returns>
		public System.Object ExecuteScalar ( System.String theCmdStr ,int timeOutVal)
		{
			System.Object obj = null;
			try
			{
				this.dbCommandWrapper = this.dbDatabase.GetSqlStringCommandWrapper(theCmdStr);
				this.dbCommandWrapper.CommandTimeout = timeOutVal;
				obj = this.dbDatabase.ExecuteScalar(this.dbCommandWrapper);
			}
			catch
			{
				throw;
			}
			return obj;
		}

		/// <summary>
		/// Executes the command and returns the first column of the first row in the resultset returned by 
		/// the query. Extra columns or rows are ignored.
		/// </summary>
		/// <param name="dbCommandWrapper">The command that contains the query to execute.</param>
		/// <returns>The first column of the first row in the resultset.</returns>
		public System.Object ExecuteScalar ( DBCommandWrapper dbCommandWrapper )
		{
			System.Object obj = null;
			try
			{
				this.dbCommandWrapper = dbCommandWrapper;
				this.dbCommandWrapper.CommandTimeout = CommandTimeoutVal;
				obj = this.dbDatabase.ExecuteScalar(this.dbCommandWrapper);
			}
			catch
			{
				throw;
			}
			return obj;
		}

		/// <summary>
		/// Executes the command by setting the command timeout value and returns the first column 
		/// of the first row in the resultset returned by the query. Extra columns or rows are ignored.
		/// </summary>
		/// <param name="dbCommandWrapper">The command that contains the query to execute.	</param>
		/// <param name="timeOutVal">Command timeout value</param>
		/// <returns>The first column of the first row in the resultset</returns>
		public System.Object ExecuteScalar ( DBCommandWrapper dbCommandWrapper ,int timeOutVal)
		{
			System.Object obj = null;
			try
			{
				this.dbCommandWrapper = dbCommandWrapper;
				this.dbCommandWrapper.CommandTimeout = timeOutVal;
				obj = this.dbDatabase.ExecuteScalar(this.dbCommandWrapper);
			}
			catch
			{
				throw;
			}
			return obj;
		}


		#endregion

		#region Transaction Methods
		/// <summary>
		/// Get new database connection from pool.
		/// Begins a database transaction. 
		/// Developer can access the transaction using CurrentTransaction object
		/// </summary>
		public void BeginTransaction()
		{
			try
			{
				this.dbConnection = this.DbDatabase.GetConnection();

				this.dbConnection.Open();

				this.dbTrx = this.dbConnection.BeginTransaction();
			}
			catch
			{
				throw;
			}
		}
		/// <summary>
		/// Get new database connection from pool.
		/// Begins a database transaction. 
		/// Developer can access the transaction using CurrentTransaction object
		/// </summary>
		public void BeginTransaction(IsolationLevel isoLevel)
		{
			try
			{
				this.dbConnection = this.DbDatabase.GetConnection();
				this.dbConnection.Open();
				this.dbTrx = this.dbConnection.BeginTransaction(isoLevel);
			}
			catch
			{
				throw;
			}
		}
		/// <summary>
		/// Commits current database transaction.
		/// Closes the current database connection
		/// </summary>
		public void CommitTransaction()
		{
			try
			{
				if(this.dbConnection != null)
				{
					if(this.dbConnection.State == ConnectionState.Open)
					{
						this.dbTrx.Commit();
						this.dbConnection.Close();
					}
				}
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Rollback the current transaction.
		/// Closes the current database connection.
		/// </summary>
		public void RollbackTransaction()
		{
			try
			{
				if(this.dbConnection.State == ConnectionState.Open)
				{
					this.dbTrx.Rollback();
					this.dbConnection.Close();
				}
			}
			catch
			{
				throw;
			}
		}

		#endregion

		#region GetStoredProcCommandWrapper
		/// <summary>
		/// Creates a DBCommandWrapper for a stored procedure.
		/// </summary>
		/// <param name="storedProcedureName">The name of the stored procedure.</param>
		/// <returns>The DBCommandWrapper for the stored procedure.</returns>
		public DBCommandWrapper GetStoredProcCommandWrapper(string storedProcedureName)
		{
			DBCommandWrapper dbc =   this.dbDatabase.GetStoredProcCommandWrapper(storedProcedureName);
			dbc.CommandTimeout= CommandTimeoutVal;
			return dbc;
			//return this.dbDatabase.GetStoredProcCommandWrapper(storedProcedureName);
		}

		/// <summary>
		/// Creates an DBCommandWrapper for a stored procedure.
		/// </summary>
		/// <param name="storedProcedureName">The name of the stored procedure.</param>
		/// <param name="parameterValues">The list of parameters for the procedure.</param>
		/// <returns><para>The DBCommandWrapper for the stored procedure.</returns>
		/// <remarks>
		/// The parameters for the stored procedure will be 
		/// discovered and the values are assigned in positional order.
		/// </remarks>    
		public DBCommandWrapper GetStoredProcCommandWrapper(string storedProcedureName,params object[] parameterValues)
		{
			//return this.dbDatabase.GetStoredProcCommandWrapper(storedProcedureName,parameterValues);
			DBCommandWrapper dbc = this.dbDatabase.GetStoredProcCommandWrapper(storedProcedureName,parameterValues);
			dbc.CommandTimeout= CommandTimeoutVal;
			return dbc;
			
		}

		/// <summary>
		/// Creates an DBCommandWrapper for a SQL query.
		/// </summary>
		/// <param name="query">The text of the query.</param>        
		/// <returns>The DBCommandWrapper for the SQL query.</returns>
		public DBCommandWrapper GetSqlStringCommandWrapper(string sqlQuery)
		{
//			return this.dbDatabase.GetSqlStringCommandWrapper(sqlQuery);
			DBCommandWrapper dbc = this.dbDatabase.GetSqlStringCommandWrapper(sqlQuery);
			dbc.CommandTimeout= CommandTimeoutVal;
			return dbc;
		}
		#endregion
	}
}
