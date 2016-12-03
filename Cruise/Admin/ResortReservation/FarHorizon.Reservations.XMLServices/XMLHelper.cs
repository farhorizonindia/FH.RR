using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.Common;
using System.Xml;

namespace FarHorizon.Reservations.XMLManager
{
    public class XMLHelper
    {
        public static XmlDocument LoadXML(ENums.UploadXMLType uploadXMLType)
        {
            return XMLHandler.LoadXML(uploadXMLType);
        }
    }
}
