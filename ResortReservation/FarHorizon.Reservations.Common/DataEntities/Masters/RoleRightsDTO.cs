using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Masters
{
    [Serializable]
    public class RoleRightsDTO 
    {
        #region Variables
        int _roleId;
        string _roleName;
        string _screenName;
        string _rightName;
        string _rightKey;
        #endregion

        #region Properties
        public int RoleId
        {
            get { return _roleId; }
            set { _roleId = value; }
        }        

        public string RoleName
        {
            get { return _roleName; }
            set { _roleName = value; }
        }        

        public string ScreenName
        {
            get { return _screenName; }
            set { _screenName = value; }
        }

        public string RightName
        {
            get { return _rightName; }
            set { _rightName = value; }
        }      

        public string RightKey
        {
            get { return _rightKey; }
            set { _rightKey = value; }
        }
        #endregion
    }
}
