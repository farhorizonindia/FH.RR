using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.UserManager;

namespace FarHorizon.Reservations.UserManager
{
    public class UserRightsHelper
    {
        #region User Related Fnctions
        public int ValidateUser(UserDTO oUserData)
        {
            UserHandler userHandler = new UserHandler();
            return userHandler.ValidateUser(oUserData);
        }
        #endregion  User Related Fnctions
    }
}
