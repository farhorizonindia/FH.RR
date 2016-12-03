using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    public class EmailDTO
    {
        string _sTo;
        string _sCC;
        string _sBCC;
        string _sSubject;
        string _sBody;
        string[] _Attachments;

        public string To
        {
            get { return _sTo; }
            set { _sTo = value; }
        }

        public string CC
        {
            get { return _sCC; }
            set { _sCC = value; }
        }

        public string BCC
        {
            get { return _sBCC; }
            set { _sBCC = value; }
        }

        public string Subject
        {
            get { return _sSubject; }
            set { _sSubject = value; }
        }

        public string Body
        {
            get { return _sBody; }
            set { _sBody = value; }
        }

        public string[] Attachments
        {
            get { return _Attachments; }
            set { _Attachments = value; }
        }
    }
}
