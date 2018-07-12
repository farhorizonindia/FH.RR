using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.Common.DataEntities.Client;

namespace FarHorizon.Reservations.Common.DataEntities.Masters
{
    [Serializable]
    public class RoomDTO 
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
        private string _roomCategoryAlias;
        private bool _Status;
        private string _Imagepath;
        private string _activestatus;
        private int _roomcategoryid;

        //clsBookingDataDateWise[] _oRoomBookingData;
        private RoomBookingDateWiseDTO[] _oRoomBookingData;                
        #endregion
        #region Properties

        public string Imagepath
        {
            get { return _Imagepath; }
            set { _Imagepath = value; }
        }
        public string activestatus
        {
            get { return _activestatus; }
            set { _activestatus = value; }
        }
        public int roomcategoryid
        {
            get { return _roomcategoryid; }
            set { _roomcategoryid = value; }
        }
        public bool Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
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

        public RoomBookingDateWiseDTO[] RoomBookingData
        {
            get { return _oRoomBookingData; }
            set { _oRoomBookingData = value; }
        }

        public string RoomCategoryAlias
        {
            get { return _roomCategoryAlias; }
            set { _roomCategoryAlias = value; }
        }


        
        #endregion Properties
    }

    [Serializable]
    public class roommaintainDTO
    {
        int _AccomodationId;
        string _accomname;
        string _roomId;
        DateTime _StartDate;
        DateTime _EndDate;
        string _reason;

        public string Reason
        {
            get { return _reason; }
            set { _reason = value; }
        }

        public string AccomName
        {
            get { return _accomname; }
            set { _accomname = value; }
        }

        public string roomId
        {
            get { return _roomId; }
            set { _roomId = value; }
        }

        public int AccomodationId
        {
            get { return _AccomodationId; }
            set { _AccomodationId = value; }
        }

        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
    }
}
