using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using System.Data;
using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.Common.DataEntities.Masters;

public partial class ClientUI_BookingDetailreportCruise : MasterBasePage
{
    DALBooking dlbooking = new DALBooking();
    BALBooking blbooking = new BALBooking();
    BALAgentPayment blAgentpayment = new BALAgentPayment();
    DALAgentPayment dlAgentpayment = new DALAgentPayment();

    DALOpenDates dlOpenDates = new DALOpenDates();
    BALOpenDates blOpenDates = new BALOpenDates();
    BALRateCard blCard = new BALRateCard();
    DALRateCard dlcard = new DALRateCard();
    DALSearch dlsearch = new DALSearch();
    DataTable dtGetReturnedData;
    DALAgentPayment dlagent = new DALAgentPayment();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillAgents(); getlocalagent(); loadaccom();
            // getall();
            lblCurrentDateTime.Text = DateTime.Now.ToLongDateString();
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss IST");
        }
    }
    private void loadaccom()
    {

        try
        {

            blCard._Action = "GetCruise";

            dtGetReturnedData = dlcard.GetAccomname(blCard);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlAccom.Items.Clear();
                ddlAccom.DataSource = dtGetReturnedData;
                ddlAccom.DataTextField = "AccomName";
                ddlAccom.DataValueField = "AccomId";
                ddlAccom.DataBind();
                //ddlAccom.Items.Insert(0, "-Select-");
                ListItem l = new ListItem("-Select-", "0");
                ddlAccom.Items.Insert(0, l);
            }
            else
            {
                //ddlaccom.Visible = false;
                ddlAccom.Items.Clear();
                ddlAccom.DataSource = null;
                ddlAccom.DataBind();
                ddlAccom.Items.Insert(0, "-No Data-");
            }
        }
        catch (Exception sqe)
        {

        }
    }
    public void getall()
    {
        //blbooking.AgentId = Convert.ToInt32(ddlFagent.SelectedValue);
        //blbooking._dtStartDate = Convert.ToDateTime(txtfrom.Text);
        blbooking.AgentId = Convert.ToInt32(ddlFagent.SelectedValue);
        blbooking.accomId = Convert.ToInt32(ddlAccom.SelectedValue);
        if (txtfrom.Text != "")
        {
            blbooking._dtStartDate = DateTime.Parse(txtfrom.Text);
        }
        else
        {
            blbooking._dtStartDate = DateTime.Parse("1990/01/01");
        }
        if (txtTo.Text != "")
        {
            blbooking._dtEndDate = DateTime.Parse(txtTo.Text);
        }
        else
        {
            blbooking._dtEndDate = DateTime.Parse("1990/01/01");
        }
        DataTable dt = dlbooking.bookingDetailsforCruise(blbooking);
        Session["getdata"] = dt;
        if (dt != null && dt.Rows.Count > 0)
        {
            dgBookings.DataSource = dt;
            dgBookings.DataBind();
        }
        else
        {
            dgBookings.DataSource = null;
            dgBookings.DataBind();
        }
        calculatetot(dt);
    }
    private void calculatetot(DataTable dt)
    {
        double total = 0;
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    total = total + Convert.ToDouble(dt.Rows[i]["Invoiceamount"].ToString());
                }
                catch { }
            }
        }
        lblTotal.Text = "INR " + total.ToString("##,0");
    }

    protected void Button1_Click(object sender, EventArgs e)

    {
        getall();
       // DataTable dt = Session["getdata"] as DataTable;
        //calculatetot(dt);
        //if (Session["getdata"] != null)
        //{
        //    DataTable dt = Session["getdata"] as DataTable;
        //    DataView dv = new DataView();
        //    DataSet ds = new DataSet();
        //    if (ddlAccom.SelectedIndex > 0 && txtfrom.Text == "" && txtTo.Text == "" && ddlFagent.SelectedIndex == 0 && ddlLocalAgent.SelectedIndex == 0 && txtinto.Text == "" && txtinfrom.Text == "")
        //    {
        //        dv = new DataView(dt, "AccomName = '" + ddlAccom.SelectedItem.ToString() + "'", "AccomName", DataViewRowState.CurrentRows);
        //        dt = dv.ToTable();
        //        dgBookings.DataSource = dt;
        //        dgBookings.DataBind();
        //        calculatetot(dt);
        //        //dt.DefaultView.RowFilter = "AccomName='" + txtAccomdation.Text.Trim() + "'";
        //    }
        //    else if (ddlAccom.SelectedIndex == 0 && txtfrom.Text != "" && txtTo.Text != "" && ddlFagent.SelectedIndex == 0 && ddlLocalAgent.SelectedIndex == 0 && txtinto.Text == "" && txtinfrom.Text == "")
        //    {

        //        ds.Tables.Add(dt);
        //        DataTable dt2 = new DataTable();
        //        dt2 = ds.Tables[0].Select().Where(p => (Convert.ToDateTime(p["StartDate"]) >= Convert.ToDateTime(txtfrom.Text)) && (Convert.ToDateTime(p["StartDate"]) >= Convert.ToDateTime(txtfrom.Text))).CopyToDataTable();
        //        dgBookings.DataSource = dt2;
        //        dgBookings.DataBind();
        //        calculatetot(dt2);
        //        ds.Clear();
        //        //dt.DefaultView.RowFilter = "StartDate >=#'" + Convert.ToDateTime(txtfrom.Text).Date + "' And  enddate<= #'" + Convert.ToDateTime(txtTo.Text).Date + "'";
        //    }
        //    else if (ddlAccom.SelectedIndex == 0 && txtfrom.Text == "" && txtTo.Text == "" && ddlFagent.SelectedIndex > 0 && ddlLocalAgent.SelectedIndex == 0 && txtinto.Text == "" && txtinfrom.Text == "")
        //    {
        //        dv = new DataView(dt, "Fagent = '" + ddlFagent.SelectedItem.ToString() + "'", "Fagent", DataViewRowState.CurrentRows);
        //        dt = dv.ToTable();
        //        dgBookings.DataSource = dt;
        //        dgBookings.DataBind();
        //        calculatetot(dt);
        //        //dt.DefaultView.RowFilter = "Fagent='" + txtFagent.Text.Trim() + "'";
        //    }
        //    else if (ddlAccom.SelectedIndex == 0 && txtfrom.Text == "" && txtTo.Text == "" && ddlFagent.SelectedIndex == 0 && ddlLocalAgent.SelectedIndex > 0 && txtinto.Text == "" && txtinfrom.Text == "")
        //    {
        //        dv = new DataView(dt, "Lagent = '" + ddlLocalAgent.SelectedItem.ToString() + "'", "Lagent", DataViewRowState.CurrentRows);
        //        dt = dv.ToTable();
        //        dgBookings.DataSource = dt;
        //        dgBookings.DataBind();
        //        calculatetot(dt);
        //        //dt.DefaultView.RowFilter = "Lagent='" + txtlAgent.Text.Trim() + "'";
        //    }
        //    else if (ddlAccom.SelectedIndex == 0 && txtfrom.Text == "" && txtTo.Text == "" && ddlFagent.SelectedIndex == 0 && ddlLocalAgent.SelectedIndex == 0 && txtinto.Text != "" && txtinfrom.Text != "")
        //    {

        //        ds.Tables.Add(dt);
        //        DataTable dt2 = new DataTable();
        //        dt2 = ds.Tables[0].Select().Where(p => (Convert.ToDateTime(p["InvoiceDate"]) >= Convert.ToDateTime(txtfrom.Text)) && (Convert.ToDateTime(p["InvoiceDate"]) >= Convert.ToDateTime(txtfrom.Text))).CopyToDataTable();
        //        dgBookings.DataSource = dt2;
        //        dgBookings.DataBind();
        //        calculatetot(dt2);
        //        ds.Clear();
        //        //dt.DefaultView.RowFilter = "InvoiceDate >=#'" + Convert.ToDateTime(txtinfrom.Text) + "' And  InvoiceDate<= #'" + Convert.ToDateTime(txtinto.Text) + "'";
        //    }

        //}
    }
    private void FillAgents()
    {
        try
        {
            AgentMaster oAgentMaster = new AgentMaster();
            AgentDTO[] oAgentData = oAgentMaster.GetData();
            ListItemCollection li = new ListItemCollection();
            ListItem l = new ListItem("Choose Agent", "0");
            ddlFagent.Items.Insert(0, l);
            if (oAgentData != null)
            {
                for (int i = 0; i < oAgentData.Length; i++)
                {
                    l = new ListItem(oAgentData[i].AgentName.ToString(), oAgentData[i].AgentId.ToString());
                    ddlFagent.Items.Insert(i + 1, l);
                }
            }
        }
        catch { }
    }
    private void getlocalagent()
    {
        DataTable dt = dlAgentpayment.getlocalagent();
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlLocalAgent.Items.Clear();
            ddlLocalAgent.DataSource = dt;

            ddlLocalAgent.DataTextField = "AgentName";
            ddlLocalAgent.DataValueField = "AgentId";
            ddlLocalAgent.DataBind();
            ddlLocalAgent.Items.Insert(0, "Select");

        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        FillAgents(); getlocalagent(); loadaccom();
        txtfrom.Text = "";
        txtinfrom.Text = "";
        txtinto.Text = "";
        txtTo.Text = "";

        if (Session["getdata"] != null)
        {
            DataTable dt = Session["getdata"] as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                dgBookings.DataSource = dt;
                dgBookings.DataBind();
            }
        }

    }

    protected void dgBookings_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgBookings.CurrentPageIndex = e.NewPageIndex;
        getall();
    }
}