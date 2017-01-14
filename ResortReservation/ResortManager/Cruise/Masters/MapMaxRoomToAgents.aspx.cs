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
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;

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
                blAr._Date = Convert.ToDateTime(txtCalender.Text.Trim());
                blAr.toDate = Convert.ToDateTime(txtCalenderto.Text.Trim());
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
                    if (dtStatus.Rows[0]["cnt"].ToString()== "Not Exist")
                    {
                        //insert
                        blAr._AccomId = Convert.ToInt32(lbAccomId.Text.ToString());
                        blAr._AgentId = Convert.ToInt32(ddlAgents.SelectedItem.Value);
                        blAr._Action = "AddAgentRooms";
                        if (txtMaxRooms.Text != string.Empty)
                            blAr._maxRooms = Convert.ToInt32(txtMaxRooms.Text);
                        else
                            blAr._maxRooms = 0;

                     
                        blAr._Date = Convert.ToDateTime(txtCalender.Text.Trim());
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
                       //blAr._Date = dt;
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
        bindrecords(Convert.ToInt32(ddlAgents.SelectedValue));

        DataTable dtnew = ViewState["MaxRooms"] as DataTable;
        if (ViewState["MaxRooms"] != null)
        {
            DataView dv = new DataView(dtnew);

            dv.RowFilter = "AgentId='" + ddlAgents.SelectedValue + "'";
            if (dv.ToTable().Rows.Count > 0)
            {
                gdvMaxBookableDetails.DataSource = dv;
                gdvMaxBookableDetails.DataBind();
            }
            else
            {
                gdvMaxBookableDetails.DataSource = null;
                gdvMaxBookableDetails.DataBind();
            }
        }

        //dv.RowFilter = "#" + Convert.ToDateTime(txtCalender.Text.Trim()) + "#<= date   and #" + Convert.ToDateTime(txtCalenderto.Text.Trim()) + "#>= ToDate";
    }

    public void bindrecords(int agid)
    {
        try
        {
            blAr._Action = "getDetails";
            blAr._AgentId = agid;
            dtGetReturnedData = new DataTable();
            dtGetReturnedData = dlAr.GetBookableRoomDetails(blAr);
            if (dtGetReturnedData != null)
            {
                if (dtGetReturnedData.Rows.Count > 0)
                {
                    gdvMaxBookableDetails.DataSource = dtGetReturnedData;
                    gdvMaxBookableDetails.DataBind();
                    ViewState["MaxRooms"] = dtGetReturnedData;
                }
                else
                {
                    gdvMaxBookableDetails.DataSource = null;
                    gdvMaxBookableDetails.DataBind();
                }
            }
            else
            {
                gdvMaxBookableDetails.DataSource = null;
                gdvMaxBookableDetails.DataBind();
            }
        }
        catch
        {
        }
    }

    protected void txtCalender_TextChanged(object sender, EventArgs e)
    {
       
    }
    protected void txtCalender_TextChanged1(object sender, EventArgs e)
    {
        
    }
    protected void txtCalenderto_TextChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlAgents.SelectedIndex > 0)
            {
                blAr._Action = "CheckDataBaseOnParameters";
                blAr._AgentId = Convert.ToInt32(ddlAgents.SelectedItem.Value);
                blAr._Date = Convert.ToDateTime(txtCalender.Text);
                blAr.toDate = Convert.ToDateTime(txtCalenderto.Text);

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
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('please select agent first')", true);
                txtCalender.Text = "";
            }



            DataTable dtnew = ViewState["MaxRooms"] as DataTable;
            DataView dv = new DataView(dtnew);
            if (txtCalender.Text != "" && txtCalenderto.Text != ""&&ddlAgents.SelectedIndex>0)
            {
                if (Convert.ToDateTime(txtCalenderto.Text) >= Convert.ToDateTime(txtCalender.Text))
                {
                    dv.RowFilter = "#" + Convert.ToDateTime(txtCalender.Text.Trim()) + "#<= date   and #" + Convert.ToDateTime(txtCalenderto.Text.Trim()) + "#>= ToDate and AgentId='" + ddlAgents.SelectedValue + "'";
                }
            }
            if (dv.ToTable().Rows.Count > 0)
            {
                gdvMaxBookableDetails.DataSource = dv;
                gdvMaxBookableDetails.DataBind();
            }
            else
            {
                gdvMaxBookableDetails.DataSource = null;
                gdvMaxBookableDetails.DataBind();
            }

            

        }
        catch (Exception sqe)
        {

        }
    }
    protected void gdvMaxBookableDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int Id =Convert.ToInt32( gdvMaxBookableDetails.DataKeys[e.RowIndex].Value);
            blAr._id = Id;
            blAr._Action = "DeleteBookableInfo";

            int res = dlAr.DeletebookableInfo(blAr);
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('record  Deleted')", true);
                bindrecords(Convert.ToInt32(ddlAgents.SelectedValue));
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Record could not be  Deleted')", true);
            }



        }

        catch
        {
        }
    }
}