using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;

namespace FarHorizon.Reservations.UploadManager
{
    interface ITouristUploader
    {
        bool processUplodedFile(int BookingId, ArrayList RecordList, XmlDocument XMLMapper);
    }
}
