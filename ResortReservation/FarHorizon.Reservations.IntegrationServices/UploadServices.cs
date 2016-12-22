using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.UploadManager;
using System.Collections;
using System.Xml;
using FarHorizon.Reservations.Common;

namespace FarHorizon.Reservations.BusinessServices
{
    public class UploadServices
    {
        public bool HandleUploadedFile(int BookingId, ENums.UploadXMLType uploadXMLType, ArrayList RecordList)
        {
            UploadHelper uploadHelper = new UploadHelper();
            return uploadHelper.HandleUploadedFile(BookingId, uploadXMLType, RecordList);
        }        
    }
}
