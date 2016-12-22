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
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;


using DataLayer.Configuration;

namespace DataLayer.DALC
{
	/// <summary>
	/// Summary description for DB2BaseDALC.
	/// </summary>
	public class clsDALC : clsDatabaseManager
	{
		/// <summary>
		/// Get the DBName from the configuration file
		/// </summary>
		protected static string DBName
		{
			get
			{
				// Read from the Application Configuration
				FHConfigurationElement calcData;
				 calcData = ConfigurationManager.GetConfiguration("FHSettings") as FHConfigurationElement;
				
				//return calcData.DBName;
                return "";
			}
		}

		/// <summary>
		/// Default Constructor inherits the FHDataBase parameter constructor
		/// </summary>
		public clsDALC() : base(DBName)
		{

		}
	}
}
