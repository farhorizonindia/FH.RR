using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using FarHorizon.Reservations.Common;
using System.IO;

namespace FarHorizon.Reservations.BusinessServices.Online.DAL
{

    /// <summary>
    /// Summary description for XMLCLassnew
    /// </summary>
    public class XMLCLassnew
    {
        public XMLCLassnew()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public XmlDocument xmlload()
        {
            XmlDocument xmlDoc = new XmlDocument();
            string dataDirectory = System.Web.HttpContext.Current.Server.MapPath("~/FarHorizon.Reservations.XMLServices/XMLs/ApplicationRights.xml");

            //dataDirectory = dataDirectory.Replace("ResortManager\\", "");

            //dataDirectory = dataDirectory + XMLConstants.baseDirectory + "ApplicationRights.xml";
            xmlDoc.Load(dataDirectory);
            return xmlDoc;


        }

        static class XMLConstants
        {
            public const string APPLICATIONRIGHTS = "ApplicationRights.xml";
            public const string TOURISTMAPPER = "touristMapper.xml";
            public const String baseDirectory = @"\FarHorizon.Reservations.XMLServices\XMLs\";
        }
    }
}