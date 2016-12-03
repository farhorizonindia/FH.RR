using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities.Masters
{
    public class clsAgentDTO
    {
        private int _AgentId;
        private string _AgentCode;
        private string _AgentName;

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

        #endregion AgentMasterProperties
    }
}
