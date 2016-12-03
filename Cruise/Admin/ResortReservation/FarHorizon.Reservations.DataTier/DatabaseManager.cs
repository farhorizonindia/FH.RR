using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.DataBaseManager
{
    public class DatabaseManager : DatabaseHandler
    {
        /// <summary>
        /// Get the DBName from the configuration file
        /// </summary>
        protected static string DBName
        {
            get
            {
                // Read from the Application Configuration
                //CISConfigurationElement calcData;
                //calcData = ConfigurationManager.GetConfiguration("CISSettings") as CISConfigurationElement;

                //return calcData.DBName;
                return "ReservationConnectionString";
            }
        }

        /// <summary>
        /// Default Constructor inherits the CISDataBase parameter constructor
        /// </summary>
        public DatabaseManager()
            : base(DBName)
        {

        }

    }
}
