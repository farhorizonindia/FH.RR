using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.Masters
{
    
    public class clsRoomAvailabilityDetails
    {
        int _iAvailableRooms;

        public int AvailableRooms
        {
            get { return _iAvailableRooms; }
            set { _iAvailableRooms = value; }
        }

        
    }
}
