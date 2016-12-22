/***********************************************************************************************************
*File Name:- FHConfigurationElement.cs
*
*File Description:- File contains only set of properties required used for DALC layer
*Author:- Parag V. Pratapwar
*Creation Date:- 19 May 2006
*Modified By:- 
*Modified Date:-
*Reason for Modification:-
*************************************************************************************************************/


using System;
using System.Xml.Serialization;

namespace DataLayer.Configuration
{
	/// <summary>
	/// Summary description for CS3CalcEngineData.
	/// </summary>
	public class FHConfigurationElement
	{
		private string dbName;
		private int commandTimeoutVal;
		public FHConfigurationElement()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Constructor sets the DBName value
		/// </summary>
		/// <param name="dbname">DBName value </param>
		public FHConfigurationElement(string dbname)
		{
			this.DBName = dbname;
		}

		/// <summary>
		/// Constructor sets the command timeout value
		/// </summary>
		/// <param name="commandtimeoutval">Command timeout value</param>
		public FHConfigurationElement(int commandtimeoutval)
		{
			this.CommandTimeoutVal = commandtimeoutval;
		}

		/// <summary>
		/// Constructor sets the command timeout value and DBName value
		/// </summary>
		/// <param name="dbname"></param>
		/// <param name="commandtimeoutval"></param>
		public FHConfigurationElement(string dbname, int commandtimeoutval)
		{
			this.DBName = dbname;
			this.CommandTimeoutVal = commandtimeoutval;
		}

		/// <summary>
		/// Database Name
		/// </summary>
		public string DBName
		{
			get { return this.dbName; }
			set { this.dbName = value; }
		} 

		/// <summary>
		/// Command Timeout Value from the configuration file
		/// </summary>
		public int CommandTimeoutVal
		{
			get { return this.commandTimeoutVal; }
			set { this.commandTimeoutVal = value; }
		}
	}
}
