using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities
{
    public struct RoomsAvailable
    {
        public clsOccupancyData oOccupancyData;
        public int NoOfRoomsAvailable;
    }
    public class clsRoomData
    {
        #region Variables
        private string _sRoomNo;
        private int _iSequence;
        private int _iAccomId;
        private int _iFloorId;
        private int _iTypeID;
        private char _cOccupancy;
        private int _iNo_of_Beds;
        private string _sDescription;
        private string _sTelExtnNo;
        private int _iExtraBedAllowed;
        private double _fExtraBedRate;
        //clsBookingDataDateWise[] _oRoomBookingData;
        private clsRoomBookingDataDateWise[] _oRoomBookingData;

                
        #endregion
        #region Properties
        public string RoomNo
        {
            get { return _sRoomNo; }
            set { _sRoomNo = value; }
        }
        public int Sequence
        {
            get { return _iSequence; }
            set { _iSequence = value; }
        }
        public int AccomodationId
        {
            get { return _iAccomId; }
            set { _iAccomId = value; }
        }
        public int FloorId
        {
            get { return _iFloorId; }
            set { _iFloorId = value; }
        }
        public int TypeID
        {
            get { return _iTypeID; }
            set { _iTypeID = value; }
        }
        public char Occupancy
        {
            get { return _cOccupancy; }
            set { _cOccupancy = value; }
        }
        public int No_of_Beds
        {
            get { return _iNo_of_Beds; }
            set { _iNo_of_Beds = value; }
        }
        public string Description
        {
            get { return _sDescription; }
            set { _sDescription = value; }
        }
        public string TelExtnNo
        {
            get { return _sTelExtnNo; }
            set { _sTelExtnNo = value; }
        }
        public bool ExtraBedAllowed
        {
            get
            {
                if (_iExtraBedAllowed == 1)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                    _iExtraBedAllowed = 1;
                else
                    _iExtraBedAllowed = 0;
            }
        }
        public double ExtraBedRate
        {
            get { return _fExtraBedRate; }
            set { _fExtraBedRate = value; }
        }

        public clsRoomBookingDataDateWise[] RoomBookingData
        {
            get { return _oRoomBookingData; }
            set { _oRoomBookingData = value; }
        }
        #endregion Properties
    }
}
