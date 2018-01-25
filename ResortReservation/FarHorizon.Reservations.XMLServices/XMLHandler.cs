using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using FarHorizon.Reservations.Common;


namespace FarHorizon.Reservations.XMLManager
{
   
    internal static class XMLHandler
    {               
        internal static XmlDocument LoadXML(ENums.UploadXMLType uploadXMLType)
        {
            string xmlFileName =string.Empty;
            switch (uploadXMLType)
            {
                case ENums.UploadXMLType.Tourist:
                    xmlFileName = XMLConstants.TOURISTMAPPER;
                    break;
                case ENums.UploadXMLType.ApplicationRights:
                    xmlFileName = XMLConstants.APPLICATIONRIGHTS;
                    break;
                default:
                    break;
            }

            if (!String.IsNullOrEmpty(xmlFileName))
            {
                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    string dataDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    //dataDirectory = dataDirectory.Replace("ResortManager\\", "");
                    dataDirectory = dataDirectory + XMLConstants.baseDirectory + xmlFileName;
                    xmlDoc.Load(dataDirectory);
                    return xmlDoc;
                }
                catch (Exception exp)
                {
                    throw exp;
                }
            }
            return null;
        }
    }

    static class XMLConstants
    {
        public const string APPLICATIONRIGHTS = "ApplicationRights.xml";
        public const string TOURISTMAPPER = "touristMapper.xml";
        public const String baseDirectory = @"\FarHorizon.Reservations.XMLServices\XMLs\";
    }
}
