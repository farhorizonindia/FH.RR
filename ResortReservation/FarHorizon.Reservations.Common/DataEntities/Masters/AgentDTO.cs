using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Masters
{
    [Serializable]
    public class AgentDTO 
    {
        private int _AgentId;
        private String _AgentCode;
        private String _AgentName;
        private String _emailId;
        private string _password;
        private string _Phone;
        private string _category;
        private string _country;
        private byte _localagent;
        private int _BookingId;
        private string _TokenNo;
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
        public byte localagent
        {
            get { return _localagent; }
            set { _localagent = value; }
        }


        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        public string category
        {
            get { return _category; }
            set { _category = value; }
        }
        public string country
        {
            get { return _country; }
            set { _country = value; }
        }
        public string TokenNo
        {
            get { return _TokenNo; }
            set { _TokenNo = value; }
        }
        
        #endregion AgentMasterProperties
    }
}
