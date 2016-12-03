using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.UploadManager;
using System.Collections;
using System.Xml;
using FarHorizon.Reservations.Common;

namespace FarHorizon.Reservations.UploadManager
{
    public class UploadHelper
    {
        public bool HandleUploadedFile(int BookingId, ENums.UploadXMLType uploadXMLType, ArrayList RecordList)
        {
            UploadHandler UploadManager = new UploadHandler();
            return UploadManager.HandleUploadedFile(BookingId, uploadXMLType, RecordList);
        }        
    }
}
