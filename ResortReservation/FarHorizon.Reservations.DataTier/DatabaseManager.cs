using System;
using System.Configuration;

namespace FarHorizon.Reservations.DataBaseManager
{
    public class DatabaseManager : DatabaseHandler
    {
        /// <summary>
        /// Get the DBName from the configuration file
        /// </summary>
        protected static string ConnectionString
        {
            get
            {
                //return "ReservationConnectionString";

                // Read from the Application Configuration
                return ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;
            }
        }

        /// <summary>
        /// Default Constructor inherits the CISDataBase parameter constructor
        /// </summary>
        public DatabaseManager()
            : base(ConnectionString)
        {

        }        
    }
}
