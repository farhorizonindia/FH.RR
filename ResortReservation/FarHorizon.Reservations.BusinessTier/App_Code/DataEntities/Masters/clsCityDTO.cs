using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities.Masters
{
    public class clsCityDTO
    {
        private int _CityId;
        private string _CityCode;
        private string _CityName;

        #region CityMasterProperties

        public int CityId
        {
            get { return _CityId; }
            set { _CityId = value; }
        }

        public string CityCode
        {
            get { return _CityCode; }
            set { _CityCode = value; }
        }

        public string CityName
        {
            get { return _CityName; }
            set { _CityName = value; }
        }

        #endregion CityMasterProperties
    }
}
