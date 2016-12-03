using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Masters
{
    public class UserDTO 
    {
        #region Variables        
        private string _userId;
        private string _sUsername;
        private string _sPassword;        
        private int _iActive;
        private int _iParent;
        private int _administrator;
        private int _roleId;
        private string _roleName;
        private UserRoleDTO _roleData;
        private UserRightsData _userRights;
        private String _emailId;
        #endregion

        #region Properies
        
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string UserName
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


        public bool Administrator
        {
            get
            {
                if (_administrator == 1)
                    return true;
                else
                    return false;
            }
            set
            {
                _administrator = 0;
                if (value == true)                
                    _administrator = 1;                
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
        public UserRightsData UserRights
        {
            get { return _userRights; }
            set { _userRights = value; }
        }
        public int RoleIdForDisplay
        {
            get { return _roleId; }
            set { _roleId = value; }
        }
        public string RoleNameForDisplay
        {
            get { return _roleName; }
            set { _roleName = value; }
        }
        public UserRoleDTO UserRoleData
        {
            get { return _roleData; }
            set { _roleData = value; }
        }

        public String EmailId
        {
            get { return _emailId; }
            set { _emailId = value; }
        }
        #endregion
    }

    public class LoggedInUser
    {
        UserDTO _user;
        List<RoleRightsDTO> _roleRigthsList;

        public UserDTO User
        {
            get { return _user; }
            set { _user = value; }
        }        

        public List<RoleRightsDTO> RoleRigthsList
        {
            get { return _roleRigthsList; }
            set { _roleRigthsList = value; }
        }
    }

    public class UserRoleDTO
    {        
        private int _roleId;
        private string _roleName;

        
        public int UserRoleId
        {
            get { return _roleId; }
            set { _roleId = value; }
        }
        public string UserRoleName
        {
            get { return _roleName; }
            set { _roleName = value; }
        }
    }
    public class UserRightsData
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
