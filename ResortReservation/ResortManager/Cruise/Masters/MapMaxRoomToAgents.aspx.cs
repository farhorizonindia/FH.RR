using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

public partial class Cruise_Masters_MapMaxRoomToAgents : System.Web.UI.Page
{
    DataTable dtGetReturnedData;
    int getQueryResponse = 0;
    BALAgentRooms blAr = new BALAgentRooms();
    DALAgentRooms dlAr = new DALAgentRooms();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BindAccomGrid();
            this.BindAgents();
        }
    }

    #region UDF
    private void BindAccomGrid()
    {
        try
        {
            blAr._Action = "GetAccomodations";
            dtGetReturnedData = dlAr.BindControls(blAr);
            if (dtGetReturnedData != null && dtGetReturnedData.Rows.Count > 0)
            {
                GridAccomodations.DataSource = dtGetReturnedData;
                GridAccomodations.DataBind();
            }
            else
            {
                GridAccomodations.DataSource = null;
                GridAccomodations.DataBind();
            }
        }
        catch (Exception sqe)
        {
            GridAccomodations.DataSource = null;
            GridAccomodations.DataBind();
        }

    }
    private void BindAgents()
    {
        try
        {
            blAr._Action = "GetAllAgents";
            dtGetReturnedData = dlAr.BindControls(blAr);
            if (dtGetReturnedData != null && dtGetReturnedData.Rows.Count > 0)
            {
                ddlAgents.DataSource = dtGetReturnedData;
                ddlAgents.DataTextField = "AgentName";
                ddlAgents.DataValueField = "AgentId";
                ddlAgents.DataBind();
                ddlAgents.Items.Insert(0, "-Select-");
            }
            else
            {
                GridAccomodations.DataSource = null;
                GridAccomodations.DataBind();
            }
        }
        catch (Exception sqe)
        {
            GridAccomodations.DataSource = null;
            GridAccomodations.DataBind();
        }

    }
    #endregion

    #region Control events
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        try
        {
            for (int count = 0; count < GridAccomodations.Rows.Count; count++)
            {

                Label lbAccomId = (Label)GridAccomodations.Rows[count].Cells[0].FindControl("lbAccomId");
                Label lbAccomName = (Label)GridAccomodations.Rows[count].Cells[1].FindControl("lbAccomName");
                Label lbStatus = (Label)GridAccomodations.Rows[count].Cells[2].FindControl("lbStatus");
                TextBox txtMaxRooms = (TextBox)GridAccomodations.Rows[count].Cells[2].FindControl("txtMaxRoom");
                blAr._Action = "GetConfirm";
                blAr._AccomId = Convert.ToInt32(lbAccomId.Text.ToString());
                if (txtMaxRooms.Text != string.Empty)
                    blAr._maxRooms = Convert.ToInt32(txtMaxRooms.Text);
                else
                    blAr._maxRooms = 0;
                dtGetReturnedData = dlAr.GetConfirmation(blAr);
                if (dtGetReturnedData.Rows[0]["cnt"].ToString() == "A")
                {
                    lbStatus.Text = "Available";
                    blAr._Action = "CheckRow";
                    blAr._AccomId = Convert.ToInt32(lbAccomId.Text.ToString());
                    blAr._AgentId = Convert.ToInt32(ddlAgents.SelectedItem.Value);
                    DataTable dtStatus = dlAr.GetExists(blAr);
                    if (dtStatus.Rows[0]["cnt"].ToString() == "Not Exist")
                    {
                        //insert
                        blAr._AccomId = Convert.ToInt32(lbAccomId.Text.ToString());
                        blAr._AgentId = Convert.ToInt32(ddlAgents.SelectedItem.Value);
                        blAr._Action = "AddAgentRooms";
                        if (txtMaxRooms.Text != string.Empty)
                            blAr._maxRooms = Convert.ToInt32(txtMaxRooms.Text);
                        else
                            blAr._maxRooms = 0;

                        string dateTime = DateTime.Now.ToString();
                        string createddate = Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd");
                        DateTime dt = DateTime.ParseExact(createddate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        blAr._Date = dt;
                        if (txtMaxRooms.Text != "")
                        {
                            getQueryResponse = dlAr.AddAgentRoom(blAr);
                            if (getQueryResponse > 0)
                            {
                                lbStatus.Text = "Added";
                                lbStatus.ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                lbStatus.Text = "Not Added";
                                lbStatus.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            lbStatus.Text = "Invalid Room Number";
                            lbStatus.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        blAr._Action = "UpdateRooms";
                        blAr._AccomId = Convert.ToInt32(lbAccomId.Text.ToString());
                        blAr._AgentId = Convert.ToInt32(ddlAgents.SelectedItem.Value);
                        blAr._Action = "UpdateRooms";
                        if (txtMaxRooms.Text != string.Empty)
                            blAr._maxRooms = Convert.ToInt32(txtMaxRooms.Text);
                        else
                            blAr._maxRooms = 0;
                        string dateTime = DateTime.Now.ToString();
                        string createddate = Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd");
                        DateTime dt = DateTime.ParseExact(createddate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        blAr._Date = dt;
                        if (txtMaxRooms.Text != "")
                        {
                            getQueryResponse = dlAr.UpdateRooms(blAr);
                            if (getQueryResponse > 0)
                            {
                                lbStatus.Text = "Updated";
                                lbStatus.ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                lbStatus.Text = "Not Updated";
                                lbStatus.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            lbStatus.Text = "Invalid Room Number";
                            lbStatus.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
                else if (dtGetReturnedData.Rows[0]["cnt"].ToString() == "NA")
                    lbStatus.Text = "Not Available";
            }
        }
        catch (Exception sqe)
        {

        }
    }
    #endregion



    protected void ddlAgents_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            blAr._Action = "CheckDataBaseOnParameters";
            blAr._AgentId = Convert.ToInt32(ddlAgents.SelectedItem.Value);

            for (int count = 0; count < GridAccomodations.Rows.Count; count++)
            {
                Label lbAccomId = (Label)GridAccomodations.Rows[count].Cells[0].FindControl("lbAccomId");
                Label lbstatus = (Label)GridAccomodations.Rows[count].Cells[2].FindControl("lbStatus");
                TextBox txtMaxRoom = (TextBox)GridAccomodations.Rows[count].Cells[2].FindControl("txtMaxRoom");
                blAr._AccomId = Convert.ToInt32(lbAccomId.Text);
                dtGetReturnedData = dlAr.GetExists(blAr);
                if (dtGetReturnedData != null)
                {
                    if (dtGetReturnedData.Rows.Count > 0)
                        txtMaxRoom.Text = dtGetReturnedData.Rows[0]["MaxRooms"].ToString();
                    else
                    {
                        txtMaxRoom.Text = "0";
                    }
                }
                lbstatus.Text = string.Empty;
            }
        }
        catch (Exception sqe)
        {

        }
    }
}