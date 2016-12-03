using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common;
using System.Collections.Generic;

public partial class MasterUI_Users_UserAgentMaster : MasterBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillUsers();
        }
    }
    
    protected void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillUnhookedAgents();
        FillHookedAgents();
    }

    protected void btnUnAssignAgent_Click(object sender, EventArgs e)
    {
        UserAgentMapperMaster userAgentMapperMaster;
        UserAgentMapperDTO userAgentMapperDTO;

        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Update))
            return;

        if (lstUsers.SelectedValue == null)
        {
            DisplayAlert("Please select user to assign agents.");
            return;
        }
        
        if (lstHookedAgents.GetSelectedIndices().Length == 0)
        {
            DisplayAlert("Please select unassigned agents to be assgined.");
            return;
        }

        try
        {
            userAgentMapperMaster = new UserAgentMapperMaster();
            userAgentMapperDTO = MapHookedAgentsToObject();
            bool success = userAgentMapperMaster.Delete(userAgentMapperDTO);
            if (success)
            {
                DisplayAlert("Agent mapped successfully.");
            }
            else
            {
                DisplayAlert("Agent mapping failed.");
            }
            FillHookedAgents();
            FillUnhookedAgents();
        }
        catch (Exception exp)
        {
            DisplayAlert(exp.Message);
        }
    }

    protected void btnAssignAgents_Click(object sender, EventArgs e)
    {
        UserAgentMapperMaster userAgentMapperMaster;
        UserAgentMapperDTO userAgentMapperDTO;

        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Update))
            return;

        if (lstUsers.SelectedValue == null)
        {
            DisplayAlert("Please select user to assign agents.");
            return;
        }

        if (lstUnHookedAgents.GetSelectedIndices().Length == 0)
        {
            DisplayAlert("Please select unassigned agents to be assgined.");
            return;
        }
        try
        {
            userAgentMapperMaster = new UserAgentMapperMaster();
            userAgentMapperDTO = MapUnHookedAgentsToObject();
            bool success = userAgentMapperMaster.Insert(userAgentMapperDTO);
            if (success)
            {
                DisplayAlert("Agent mapped successfully.");
            }
            else
            {
                DisplayAlert("Agent mapping failed.");
            }
            FillHookedAgents();
            FillUnhookedAgents();
        }
        catch (Exception exp)
        {
            DisplayAlert(exp.Message);
        }

    }

    private void FillUsers()
    {
        UserAgentMapperMaster userAgentMapperMaster = new UserAgentMapperMaster();
        UserDTO[] userDto = userAgentMapperMaster.Getusers();
        ListItem l;
        if (userDto != null && userDto.Length > 0)
        {
            for (int i = 0; i < userDto.Length; i++)
            {
                l = new ListItem(userDto[i].UserName, userDto[i].UserId.ToString());
                lstUsers.Items.Add(l);
            }
        }
    }

    private void FillUnhookedAgents()
    {
        UserAgentMapperMaster userAgentMapperMaster = new UserAgentMapperMaster();
        lstUnHookedAgents.Items.Clear();
        if (lstUsers.SelectedValue != null)
        {
            String userId = lstUsers.SelectedValue.ToString();
            UserAgentMapperDTO userAgentMapperDto = userAgentMapperMaster.GetUnHookedAgents(userId);
            ListItem l;
            if (userAgentMapperDto != null && userAgentMapperDto.AgentList != null && userAgentMapperDto.AgentList.Count > 0)
            {
                for (int i = 0; i < userAgentMapperDto.AgentList.Count; i++)
                {
                    l = new ListItem(userAgentMapperDto.AgentList[i].AgentName, userAgentMapperDto.AgentList[i].AgentId.ToString());
                    lstUnHookedAgents.Items.Add(l);
                }
            }
        }
    }

    private void FillHookedAgents()
    {
        UserAgentMapperMaster userAgentMapperMaster = new UserAgentMapperMaster();
        lstHookedAgents.Items.Clear();
        if (lstUsers.SelectedValue != null)
        {
            String userId = lstUsers.SelectedValue.ToString();
            UserAgentMapperDTO userAgentMapperDto = userAgentMapperMaster.GetHookedAgents(userId);
            ListItem l;
            if (userAgentMapperDto != null && userAgentMapperDto.AgentList != null && userAgentMapperDto.AgentList.Count > 0)
            {
                for (int i = 0; i < userAgentMapperDto.AgentList.Count; i++)
                {
                    l = new ListItem(userAgentMapperDto.AgentList[i].AgentName, userAgentMapperDto.AgentList[i].AgentId.ToString());
                    lstHookedAgents.Items.Add(l);
                }
            }
        }
    }

    private void MapObjectToControl()
    {

    }

    private UserAgentMapperDTO MapUnHookedAgentsToObject()
    {
        UserAgentMapperDTO userAgentMapperDTO = new UserAgentMapperDTO();
        List<AgentDTO> newhookedAgentList = new List<AgentDTO>();
        AgentDTO agent;

        int[] selectedItems = lstUnHookedAgents.GetSelectedIndices();
        userAgentMapperDTO.UserId = lstUsers.SelectedValue.ToString();
        for (int i = 0; i < selectedItems.Length; i++)
        {
            agent = new AgentDTO();
            agent.AgentId = Convert.ToInt32(lstUnHookedAgents.Items[selectedItems[i]].Value);
            agent.AgentName = lstUnHookedAgents.Items[selectedItems[i]].Text;
            newhookedAgentList.Add(agent);
        }
        userAgentMapperDTO.AgentList = newhookedAgentList;
        return userAgentMapperDTO;
    }

    private UserAgentMapperDTO MapHookedAgentsToObject()
    {
        UserAgentMapperDTO userAgentMapperDTO = new UserAgentMapperDTO();
        List<AgentDTO> oldHookedAgentList = new List<AgentDTO>();
        AgentDTO agent;

        userAgentMapperDTO.UserId = lstUsers.SelectedValue.ToString();
        int[] selectedItems = lstHookedAgents.GetSelectedIndices();

        for (int i = 0; i < selectedItems.Length; i++)
        {
            agent = new AgentDTO();
            agent.AgentId = Convert.ToInt32(lstHookedAgents.Items[selectedItems[i]].Value);
            agent.AgentName = lstHookedAgents.Items[selectedItems[i]].Text;
            oldHookedAgentList.Add(agent);
        }
        userAgentMapperDTO.AgentList = oldHookedAgentList;
        return userAgentMapperDTO;
    }        
}
