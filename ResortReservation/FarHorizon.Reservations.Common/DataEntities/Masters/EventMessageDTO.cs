using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Masters
{
    public class EventMessageDTO 
    {
        int _MessageId;
        string _EventName;
        string _EventMessage;
        string _EventSubject;
        string _EventMessageDefault;
        bool _MailAllowed;

        public int MessageId
        {
            get { return _MessageId; }
            set { _MessageId = value; }
        }        

        public string EventName
        {
            get { return _EventName; }
            set { _EventName = value; }
        }
        
        public string EventMessage
        {
            get { return _EventMessage; }
            set { _EventMessage = value; }
        }
        
        public string EventSubject
        {
            get { return _EventSubject; }
            set { _EventSubject = value; }
        }
        
        public string EventMessageDefault
        {
            get { return _EventMessageDefault; }
            set { _EventMessageDefault = value; }
        }
        
        public bool MailAllowed
        {
            get { return _MailAllowed; }
            set { _MailAllowed = value; }
        }
    }
}
