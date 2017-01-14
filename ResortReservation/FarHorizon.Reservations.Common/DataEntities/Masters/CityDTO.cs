using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Masters
{
    [Serializable]
    public class CityDTO 
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
