using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using FarHorizon.Reservations.XMLManager;
using FarHorizon.Reservations.Common;

namespace FarHorizon.Reservations.UploadManager
{
    internal class UploadHandler
    {
        public bool HandleUploadedFile(int bookingId, ENums.UploadXMLType uploadXMLType, ArrayList recordList)
        {
            
            switch (uploadXMLType)
            {
                case ENums.UploadXMLType.Tourist:
                    return UploadTourist(bookingId, uploadXMLType, recordList);                    
                default:
                    break;
            }
            return false;
        }

        private bool UploadTourist(int bookingId, ENums.UploadXMLType uploadXMLType, ArrayList recordList)
        {
            ITouristUploader touristUploader = null;
            touristUploader = new TouristUploader();

            try
            {
                XmlDocument xmlMappingDoc = XMLHelper.LoadXML(uploadXMLType);
                //string xPath = @"//table";
                return touristUploader.processUplodedFile(bookingId, recordList, xmlMappingDoc);
            }
            catch (Exception exp)
            {
                throw exp;
            }            
        }
    }
}
