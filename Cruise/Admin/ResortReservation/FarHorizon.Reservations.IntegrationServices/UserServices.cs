using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.UserManager;

namespace FarHorizon.Reservations.BusinessServices
{
    public class UserServices
    {
        UserRightsHelper userRightsProxy = null;

        #region User Related Fnctions
        public int ValidateUser(UserDTO oUserData)
        {
            if (userRightsProxy == null)
                userRightsProxy = new UserRightsHelper();
            return userRightsProxy.ValidateUser(oUserData);
        }
        #endregion  User Related Fnctions
    }
}
