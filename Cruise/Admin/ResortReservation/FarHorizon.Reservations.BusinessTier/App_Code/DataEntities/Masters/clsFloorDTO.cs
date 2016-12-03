using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities.Masters
{
    public class clsFloorDTO
    {
        private int _iFloorID;
        private int _iFloor;

        public int FloorId
        {
            get { return _iFloorID; }
            set { _iFloorID = value; }
        }
        public int Floor
        {
            get { return _iFloor; }
            set { _iFloor = value; }
        }

    }
}
