using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities.Masters
{
    public class clsUserDTO
    {
        #region Variables

        
        private int _iUserID;
        private string _sUsername;
        private string _sPassword;        
        private int _iActive;
        private int _iParent;

        
        private clsWorkgroupData oWorkgroupData;
        private clsUserRightsData oUserRights;

        

        #endregion

        #region Properies
        
        public int UserID
        {
            get { return _iUserID; }
            set { _iUserID = value; }
        }

        public string Username
        {
            get { return _sUsername; }
            set { _sUsername = value; }
        }

        public string Password
        {
            get { return _sPassword; }
            set { _sPassword = value; }
        }
        
        public bool Active
        {
            get
            {
                if (_iActive == 1)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                    _iActive = 1;
                else if (value == false)
                    _iActive = 0;
            }

        }
        /**
         * 
         * 
         * THIS PROPERTY REFLECTS THE CREATOR OF THE USER WHETHER IT IS ADMIN OR TL OR SIMPLE USER
         * IF THE USER IS ADMIN, THIS PROPERTY WIL HAVE A VALUE OF 0, ELSE THE ID OF THE CREATOR
         * 
         * */
        public int Parent
        {
            get { return _iParent; }
            set { _iParent = value; }
        }
        public clsUserRightsData UserRights
        {
            get { return oUserRights; }
            set { oUserRights = value; }
        }

        public clsWorkgroupData WorkgroupData
        {
            get { return oWorkgroupData; }
            set { oWorkgroupData = value; }
        }

        #endregion


    }
    public class clsWorkgroupData
    {
        
        private int _iWorkgroupID;
        private string _sWorkgroupName;

        
        public int WorkgroupID
        {
            get { return _iWorkgroupID; }
            set { _iWorkgroupID = value; }
        }
        public string WorkgroupName
        {
            get { return _sWorkgroupName; }
            set { _sWorkgroupName = value; }
        }
    }
    public class clsUserRightsData
    {
        private int _iUserID;
        private int _iAccomTypeID;
        private int _iAccomID;

        public int UserID
        {
            get { return _iUserID; }
            set { _iUserID = value; }
        }
        public int AccomTypeID
        {
            get { return _iAccomTypeID; }
            set { _iAccomTypeID = value; }
        }
        public int AccomID
        {
            get { return _iAccomID; }
            set { _iAccomID = value; }
        }
    }
}
