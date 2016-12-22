using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace FarHorizon.Reservations.SessionManager
{
    internal class SessionManager
    {
        private static Guid sessionId;

        #region Private Constants
        private const String SESSIONID = "Farhorizon.StateManagement.WebSession.CurrentSession.Guid";
        #endregion

        #region Constructor
        static SessionManager()
        {
            if (HttpContext.Current.Session.IsNewSession)
            {
                // Create new session.
                HttpContext.Current.Session[SESSIONID] = System.Guid.NewGuid().ToString();
            }

            HttpContext.Current.Session[SESSIONID] = System.Guid.NewGuid().ToString();

            //sessionId = (Guid)HttpContext.Current.Session[SESSIONID];
        }
        #endregion

        #region Public Interface Method(s)
        #region Save Session
        public static Boolean SaveSession(String key, Object value)
        {
            if (key.Equals(String.Empty))
                return false;
            if (value == null)
                return false;

            try
            {
                /*Byte[] byteArray = Utility.ConvertObjectToByteArray(value);
                byteArray = Utility.TripleDesEncryptValue(byteArray);

                HttpContext.Current.Session.Add(key, byteArray);*/
                HttpContext.Current.Session.Add(key, value);
                return true;
            }
            catch
            { return false; }
        }
        #endregion

        #region Retrieve Session
        public static Object RetrieveSession(String key)
        {
            if (key.Equals(String.Empty))
                return null;

            try
            {
                Object sessionValue = new Object();
                /*Byte[] byteArray = (Byte[])HttpContext.Current.Session[key];
                byteArray = Utility.TripleDesEncryptValue(byteArray);
                sessionValue = Utility.ConvertByteArrayToObject(byteArray);*/
                sessionValue = HttpContext.Current.Session[key];
                return sessionValue;
            }
            catch
            { return null; }
        }
        #endregion

        #region Delete Session
        public static Boolean DeleteSession(String key)
        {
            if (key.Equals(String.Empty))
                return false;

            try
            {
                HttpContext.Current.Session.Contents.Remove(key);
                HttpContext.Current.Session.Remove(key);
                return true;
            }
            catch
            { return false; }
        }
        #endregion

        #region Abandon Session
        public static Boolean AbandonSession()
        {
            try
            {
                HttpContext.Current.Session.Abandon();
                return true;
            }
            catch
            { return false; }
        }
        #endregion
        #endregion
    }
}

