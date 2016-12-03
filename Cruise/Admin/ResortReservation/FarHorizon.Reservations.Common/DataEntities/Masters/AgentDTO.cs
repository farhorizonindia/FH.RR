using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Masters
{
    public class AgentDTO 
    {
        private int _AgentId;
        private String _AgentCode;
        private String _AgentName;
        private String _emailId;
        private string _password;
        private string _Phone;

        #region AgentMasterProperties

        public int AgentId
        {
            get { return _AgentId; }
            set { _AgentId = value; }
        }

        public string AgentCode
        {
            get { return _AgentCode; }
            set { _AgentCode = value; }
        }

        public string AgentName
        {
            get { return _AgentName; }
            set { _AgentName = value; }
        }

        public String EmailId
        {
            get { return _emailId; }
            set { _emailId = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        #endregion AgentMasterProperties
    }
}
