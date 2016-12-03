using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.WebHelper.Utility;

namespace FarHorizon.Reservations.WebHelper
{
    public class WebSiteHelper
    {
        public string ConvertObjetToJSON(object objectTobeJSON)
        {
            try
            {
                //XmlDocument serializedXMLDoc;
                string JSONObjectName;
                //clsSerializtionHandler oBookingSerializtionHandler = new clsSerializtionHandler();
                //serializedXMLDoc = oBookingSerializtionHandler.SerializeObject(objectTobeJSON);
                //JSONObjectName = clsJSONHandler.XmlToJSON(serializedXMLDoc);
                JSONObjectName = clsJSONHandler.ObjectToJSON(objectTobeJSON);
                return JSONObjectName;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

    }
}
