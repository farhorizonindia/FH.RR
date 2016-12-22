using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities
{
    public class clsUserData
    {
        string _sUserId;
        string _sUserName;
        string _sPassword;
        int _iActive;

        public string UserId
        {
            get { return _sUserId; }
            set { _sUserId = value; }
        }

        public string UserName
        {
            get { return _sUserName; }
            set { _sUserName = value; }
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
                else if(value==false)
                _iActive =0;
            }

        }
    }
}
