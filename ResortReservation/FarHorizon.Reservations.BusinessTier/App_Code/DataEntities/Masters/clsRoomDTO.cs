using System;
using System.Collections.Generic;
using System.Text;
using BusinessTier.App_Code.DataEntities.Client;

namespace BusinessTier.App_Code.DataEntities.Masters
{
    
    public class clsRoomDTO
    {
        #region Variables
        private string _sRoomNo;
        private int _iSequence;
        private int _iAccomId;
        private int _iFloorId;
        private int _iRoomCategoryId;
        private int _iRoomTypeId;
        private int _iNo_of_Beds;
        private string _sDescription;
        private string _sTelExtnNo;
        private int _iExtraBeds;
        private double _fExtraBedRate;
        private string _sRoomCategory;
        private string _sRoomType;
        private bool _bConvertable;       
        
        //clsBookingDataDateWise[] _oRoomBookingData;
        private clsRoomBookingDateWiseDTO[] _oRoomBookingData;                
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
        public int RoomCategoryId
        {
            get { return _iRoomCategoryId; }
            set { _iRoomCategoryId = value; }
        }
        public int RoomTypeId
        {
            get { return _iRoomTypeId; }
            set { _iRoomTypeId = value; }
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
        public int ExtraBeds
        {
            get { return _iExtraBeds; }
            set { _iExtraBeds = value; }
        }
        public double ExtraBedRate
        {
            get { return _fExtraBedRate; }
            set { _fExtraBedRate = value; }
        }

        public string RoomType
        {
            get { return _sRoomType; }
            set { _sRoomType = value; }
        }

        public string RoomCategory
        {
            get { return _sRoomCategory; }
            set { _sRoomCategory = value; }
        }

        public bool Convertable
        {
            get { return _bConvertable; }
            set { _bConvertable = value; }
        }

        public clsRoomBookingDateWiseDTO[] RoomBookingData
        {
            get { return _oRoomBookingData; }
            set { _oRoomBookingData = value; }
        }
        #endregion Properties
    }
}
