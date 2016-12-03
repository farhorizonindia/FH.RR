using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Masters
{
    public class TransportDTO 
    {
        private int _iTransportID;
        private string _Transport;

        public int TransportId
        {
            get { return _iTransportID; }
            set { _iTransportID = value; }
        }
        public string TransportName
        {
            get { return _Transport; }
            set { _Transport = value; }
        }

    }
}
