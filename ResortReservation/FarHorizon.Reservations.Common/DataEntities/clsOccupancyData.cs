using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities
{
    public class clsOccupancyData
    {
        private int _iOccupancyID;

        public int OccupancyID
        {
            get { return _iOccupancyID; }
            set { _iOccupancyID = value; }
        }
        private string _sOccupancyType;

        public string OccupancyType
        {
            get { return _sOccupancyType; }
            set { _sOccupancyType = value; }
        }

    }
}
