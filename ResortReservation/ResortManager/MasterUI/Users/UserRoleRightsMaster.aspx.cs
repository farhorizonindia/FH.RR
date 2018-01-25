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
using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using System.Collections.Generic;
using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Bases.BasePages;
using System.Xml;
using FarHorizon.Reservations.BusinessServices.Online.DAL;

public partial class MasterUI_Users_UserRoleRightsMaster : MasterBasePage
{
    public XMLCLassnew xmlclss = new XMLCLassnew();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //xmlload();
            //LoadXML();
            FillRoles();
            FillRights();
        }
    }
    public XmlDocument xmlload()
    {
        XmlDocument xmlDoc = new XmlDocument();
        string dataDirectory = System.Web.HttpContext.Current.Server.MapPath("~/FarHorizon.Reservations.XMLServices/XMLs/ApplicationRights.xml");

        //dataDirectory = dataDirectory.Replace("ResortManager\\", "");

        //dataDirectory = dataDirectory + XMLConstants.baseDirectory + "ApplicationRights.xml";
        xmlDoc.Load(dataDirectory);
        return xmlDoc;
    }
    private void FillRoles()
    {
        UserRoleMaster userRoleMaster = new UserRoleMaster();
        UserRoleDTO[] userRoleDto = userRoleMaster.GetUserRoles();
        ListItem l;
        if (userRoleDto != null && userRoleDto.Length > 0)
        {
            for (int i = 0; i < userRoleDto.Length; i++)
            {
                l = new ListItem(userRoleDto[i].UserRoleName, userRoleDto[i].UserRoleId.ToString());
                lstUserRoles.Items.Add(l);
            }
        }
    }
    internal static XmlDocument LoadXML()
    {
        string xmlFileName = string.Empty;



        xmlFileName = XMLConstants.APPLICATIONRIGHTS;



        if (!String.IsNullOrEmpty(xmlFileName))
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                string dataDirectory = AppDomain.CurrentDomain.BaseDirectory;
                //dataDirectory = dataDirectory.Replace("ResortManager\\", "");
                dataDirectory = dataDirectory + XMLConstants.baseDirectory + xmlFileName;
                
                xmlDoc.Load(dataDirectory);
                return xmlDoc;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        return null;
    }
    private void FillRights()
    {
        ApplicationRightsMaster applicationRightsMaster = new ApplicationRightsMaster();
        List<ApplicationRightsDTO> applicationRightsList = applicationRightsMaster.GetApplicationRights();

        TreeNode operationTypeNode;
        TreeNode pageNode;
        TreeNode rightNode;
        foreach (ApplicationRightsDTO applicationRights in applicationRightsList)
        {
            operationTypeNode = new TreeNode(applicationRights.OperationType, applicationRights.OperationType);
            foreach (PageWiseRights pageWiseRights in applicationRights.PageWiseRights)
            {
                pageNode = new TreeNode(pageWiseRights.PageDescription, pageWiseRights.PageId);
                foreach (Rights right in pageWiseRights.Rights)
                {
                    rightNode = new TreeNode(right.Description, right.Id);
                    pageNode.ChildNodes.Add(rightNode);
                }
                operationTypeNode.ChildNodes.Add(pageNode);
            }
            tvRights.Nodes.Add(operationTypeNode);
            tvRights.LeafNodeStyle.CssClass = "rightStyle";
            tvRights.NodeStyle.CssClass = "pageStyle";
            tvRights.RootNodeStyle.CssClass = "operationStyle";
            //tvRights.ExpandDepth = 1;
        }
    }

    private void FillRoleRights()
    {
        int roleId = Convert.ToInt32(lstUserRoles.SelectedItem.Value);
        if (roleId != 0)
        {
            dgRoleRights.DataSource = null;
            dgRoleRights.DataBind();
            List<RoleRightsDTO> roleRightsList;
            RoleRightsMaster roleRightsMaster = new RoleRightsMaster();
            roleRightsList = roleRightsMaster.GetRoleRights(roleId);
            dgRoleRights.DataSource = roleRightsList;
            dgRoleRights.DataBind();

            MapObjectToControl(roleRightsList);
        }
    }

    private void MapObjectToControl(List<RoleRightsDTO> roleRightsList)
    {
        foreach (TreeNode parentNode in tvRights.Nodes)
        {
            foreach (TreeNode screenNode in parentNode.ChildNodes)
            {
                foreach (TreeNode right in screenNode.ChildNodes)
                {
                    right.Checked = false;
                }
            }
        }

        if (roleRightsList != null)
        {
            foreach (RoleRightsDTO right in roleRightsList)
            {
                string path = string.Empty;
                foreach (TreeNode parentNode in tvRights.Nodes)
                {
                    path = parentNode.ValuePath + @"/" + right.ScreenName + @"/" + right.RightKey;
                    TreeNode node = tvRights.FindNode(path);
                    if (node != null)
                    {
                        node.Checked = true;
                    }
                }
            }
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void lstUserRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillRoleRights();
    }

    protected void btnRoleRightsAssignment_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        bool done = false;

        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Update))
            return;

        try
        {
            List<RoleRightsDTO> roleRightsList = MapControlsToObject();
            RoleRightsMaster roleRightsMaster = new RoleRightsMaster();
            if (roleRightsList != null && roleRightsList.Count > 0)
            {
                done = roleRightsMaster.Insert(roleRightsList);
            }
            FillRoleRights();
        }
        catch (Exception exp)
        {
            msg = exp.Message;
            base.DisplayAlert(msg);
            return;
        }
        if (done)
            msg = "Insert Successfull";
        else
            msg = "Insert Failed";
        base.DisplayAlert(msg);
    }

    private List<RoleRightsDTO> MapControlsToObject()
    {
        List<RoleRightsDTO> roleRightsList = new List<RoleRightsDTO>();
        RoleRightsDTO roleRight;

        TreeNodeCollection selectedRights = tvRights.CheckedNodes;

        foreach (TreeNode rightNode in selectedRights)
        {
            roleRight = new RoleRightsDTO();
            roleRight.RoleId = Convert.ToInt32(lstUserRoles.SelectedValue);
            roleRight.RoleName = lstUserRoles.SelectedItem.Text;
            roleRight.ScreenName = rightNode.Parent.Value;
            roleRight.RightName = rightNode.Text;
            roleRight.RightKey = rightNode.Value;
            roleRightsList.Add(roleRight);
        }
        return roleRightsList;
    }

    protected void btnSelectAll_Click(object sender, EventArgs e)
    {
        foreach (TreeNode operationTypeNodes in tvRights.Nodes)
        {
            foreach (TreeNode screenNodes in operationTypeNodes.ChildNodes)
            {
                foreach (TreeNode rightNode in screenNodes.ChildNodes)
                {
                    rightNode.Checked = true;
                }
            }
        }
    }
    protected void btnDeSelectAll_Click(object sender, EventArgs e)
    {
        foreach (TreeNode operationTypeNodes in tvRights.Nodes)
        {
            foreach (TreeNode screenNodes in operationTypeNodes.ChildNodes)
            {
                foreach (TreeNode rightNode in screenNodes.ChildNodes)
                {
                    rightNode.Checked = false;
                }
            }
        }
    }
}
static class XMLConstants
{
    public const string APPLICATIONRIGHTS = "ApplicationRights.xml";
    public const string TOURISTMAPPER = "touristMapper.xml";
    public const String baseDirectory = @"\FarHorizon.Reservations.XMLServices\XMLs\";
}