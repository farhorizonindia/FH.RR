using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities.Masters
{
    public class clsSMTPDTO
    {
        string _SMTPServer;
        string _UserId;
        string _Password;
        string _FromEmail;
        string _ReplyTo;
        string _FromDisplayName;
        int _Port;        

        public string SMTPServer
        {
            get { return _SMTPServer; }
            set { _SMTPServer = value; }
        }

        public string SMTPUserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public string SMTPPassword
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public string FromEmailId
        {
            get { return _FromEmail; }

            set { _FromEmail = value; }
        }

        public string ReplyToId
        {
            get { return _ReplyTo; }
            set { _ReplyTo = value; }
        }

        public string FromDisplayName
        {
            get { return _FromDisplayName; }
            set { _FromDisplayName = value;  }
        }

        public int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }
    }
}
