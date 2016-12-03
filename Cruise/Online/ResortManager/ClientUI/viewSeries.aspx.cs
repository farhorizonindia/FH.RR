using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;

public partial class ClientUI_viewSeries : ClientBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AddAttributes();
        if (!IsPostBack)
        {
            FillAccomodations();
            if (SessionServices.ViewSeries_StartSeriesDate != null && DateTime.Compare(SessionServices.ViewSeries_StartSeriesDate, DateTime.MinValue) != 0)
            {
                DateTime dt;
                DateTime.TryParse(SessionServices.ViewSeries_StartSeriesDate.ToString(), out dt);
                if (dt != DateTime.MinValue)
                {
                    txtStartDate.Text = dt.ToString("dd-MMM-yyyy");
                }
            }
            if (SessionServices.ViewSeries_SelectedAccomodation != null)
                ddlAccomName.SelectedValue = SessionServices.ViewSeries_SelectedAccomodation;
            btnShow_Click(sender, e);
        }
    }

    private void AddAttributes()
    {
        btnShow.Attributes.Add("onclick", "return validateBeforeGettingSeries()");
    }
    private void FillAccomodations()
    {
        AccomodationMaster oAccomodationMaster = new AccomodationMaster();
        AccomodationDTO[] oAccomodationData = null;
        ListItem l;
        oAccomodationData = oAccomodationMaster.GetAccomodations();
        if (oAccomodationData != null)
        {
            l = new ListItem("Choose", "-1");
            ddlAccomName.Items.Insert(0, l);
            for (int i = 0; i < oAccomodationData.Length; i++)
            {
                l = new ListItem(oAccomodationData[i].AccomodationName, oAccomodationData[i].AccomodationId.ToString());
                ddlAccomName.Items.Insert(i + 1, l);
            }
        }
    }

    protected void dgSeries_EditCommand(object source, DataGridCommandEventArgs e)
    {
        int iSeriesId = Convert.ToInt32(dgSeries.DataKeys[e.Item.ItemIndex].ToString());
        Response.Redirect("SeriesBooking.aspx?sid=" + iSeriesId);
    }
    /*
    protected void dgSeries_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        int iSeriesId = Convert.ToInt32(dgSeries.DataKeys[e.Item.ItemIndex].ToString());
        clsSeriesManager oSeriesManager;
        oSeriesManager = new clsSeriesManager();
        oSeriesManager.DeleteSeries(iSeriesId);
        RefreshGrid();
    }
    protected void dgSeries_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            if (e.Item.ItemIndex >= 0)
            {
                int iSeriesId = Convert.ToInt32(dgSeries.DataKeys[e.Item.ItemIndex].ToString());
                if (string.Compare(e.CommandName.ToString(), "Edit", true) == 0)
                    Response.Redirect("SeriesBooking.aspx?sid=" + iSeriesId.ToString());                
            }
        }
        catch (Exception exp)
        {
            GF.LogError("ViewSeries.dgSeries_ItemCommand", exp.Message);
            return;
        }
        
    }    
 */
    protected void btnShow_Click(object sender, EventArgs e)
    {
        SessionServices.DeleteSession(Constants._ViewBooking_BookingData);
        if (txtStartDate.Text != "")
            SessionServices.ViewSeries_StartSeriesDate = Convert.ToDateTime(txtStartDate.Text.ToString());
        if (ddlAccomName.SelectedIndex != 0)
            SessionServices.ViewSeries_SelectedAccomodation = ddlAccomName.SelectedValue;
        RefreshGrid();
    }
    protected void dgSeries_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgSeries.CurrentPageIndex = e.NewPageIndex;
        RefreshGrid();
    }
    /*protected void dgSeries_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType==ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (string.Compare(e.Item.Cells[5].Text, "BOOKED", true) == 0)
            {
                e.Item.Cells[0].BackColor = System.Drawing.Color.LightBlue;
                e.Item.Cells[9].Text = "";
                e.Item.Cells[10].Text = "";
            }
            else if (string.Compare(e.Item.Cells[5].Text, "CONFIRMED", true) == 0)
            {
                e.Item.Cells[0].BackColor = System.Drawing.Color.LimeGreen;
                e.Item.Cells[6].Text = "";
                LinkButton bc = (LinkButton)(e.Item.Cells[8].Controls[0]);
                if (bc != null)
                    bc.Text = "Edit Confirmation";
                e.Item.Cells[9].Visible = true;
                e.Item.Cells[10].Visible = true;
            }            
            else if (string.Compare(e.Item.Cells[5].Text, "CANCELLED", true) == 0)
            {
                e.Item.Cells[0].BackColor = System.Drawing.Color.Red;
                e.Item.Cells[6].Text = "";
                e.Item.Cells[7].Text = "";
                e.Item.Cells[8].Text = "";
                e.Item.Cells[9].Text = "";
                e.Item.Cells[10].Text = "";
            }
            else if (string.Compare(e.Item.Cells[5].Text, "WAITLISTED", true) == 0)
            {
                e.Item.Cells[0].BackColor = System.Drawing.Color.Orange;
                e.Item.Cells[7].Text = "";
                e.Item.Cells[8].Text = "";
                e.Item.Cells[9].Text = "";
                e.Item.Cells[10].Text = "";
            }                       
        }
    }*/

    private void RefreshGrid()
    {
        SeriesBookingServices oSeriesManager = new SeriesBookingServices();
        List<SeriesDTO> oSeriesData = null;
        DateTime StartDate;
        DateTime.TryParse(txtStartDate.Text, out StartDate);


        int AccomId = 0;

        Int32.TryParse(ddlAccomName.SelectedValue.ToString(), out AccomId);

        if (AccomId <= 0) AccomId = 0; //To handle the -1 value of Choose option.

        if (SessionServices.ViewBooking_BookingData == null)
        {
            if (AccomId != 0 || StartDate != DateTime.MinValue)
            {
                oSeriesData = oSeriesManager.GetSeries(AccomId, StartDate);
                SessionServices.ViewSeries_Data = oSeriesData;
            }
        }
        else
            oSeriesData = SessionServices.ViewSeries_Data;
        if (oSeriesData != null)
        {
            dgSeries.DataSource = oSeriesData;
            dgSeries.DataBind();
        }
        else
        {
            dgSeries.DataSource = null;
            dgSeries.DataBind();
        }

        oSeriesManager = null;
        oSeriesData = null;
    }
}
