using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Masters
{
    public class UserAgentMapperDTO
    {
        String _userId;
        List<AgentDTO> _agentList;

        public String UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public List<AgentDTO> AgentList
        {
            get { return _agentList; }
            set { _agentList = value; }
        }        
    }

    public class AgentUserMapperDTO
    {
        AgentDTO _agent;

        public AgentDTO Agent
        {
            get { return _agent; }
            set { _agent = value; }
        }
        List<UserDTO> _userList;

        public List<UserDTO> UserList
        {
            get { return _userList; }
            set { _userList = value; }
        }                
    }
}
