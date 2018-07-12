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

public partial class ClientUI_BookingSummeryReport : MasterBasePage
{
    DALBooking dlbooking = new DALBooking();
    BALBooking blbooking = new BALBooking();
    DALOpenDates dlOpenDates = new DALOpenDates();
    BALOpenDates blOpenDates = new BALOpenDates();
    BALRateCard blCard = new BALRateCard();
    DALRateCard dlcard = new DALRateCard();
    DALSearch dlsearch = new DALSearch();
    DataTable dtGetReturnedData;
    DALAgentPayment dlagent = new DALAgentPayment();
    BALAgentPayment blAgentpayment = new BALAgentPayment();
    DALAgentPayment dlAgentpayment = new DALAgentPayment();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // getall();
            FillAgents();
            getlocalagent();
            BindAccomNames1();
            lblCurrentDateTime.Text = DateTime.Now.ToLongDateString();
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss IST");
        }
    }
    private void FillAgents()
    {
        try
        {
            AgentMaster oAgentMaster = new AgentMaster();
            AgentDTO[] oAgentData = oAgentMaster.GetData();
            ListItemCollection li = new ListItemCollection();
            ListItem l = new ListItem("Choose Agent", "0");
            ddlagent.Items.Insert(0, l);
            if (oAgentData != null)
            {
                for (int i = 0; i < oAgentData.Length; i++)
                {
                    l = new ListItem(oAgentData[i].AgentName.ToString(), oAgentData[i].AgentId.ToString());
                    ddlagent.Items.Insert(i + 1, l);
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
            ddllagent.Items.Clear();
            ddllagent.DataSource = dt;

            ddllagent.DataTextField = "AgentName";
            ddllagent.DataValueField = "AgentId";
            ddllagent.DataBind();
            //ddllagent.Items.Insert(0, "Select");
            ListItem l = new ListItem("Select", "0");
            ddllagent.Items.Insert(0, l);

        }
    }
    public void getall()
    {
        blbooking.AgentId = Convert.ToInt32(ddlagent.SelectedValue);
        blbooking.accomId = Convert.ToInt32(ddlaccom.SelectedValue);
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
        DataTable dt = dlbooking.bookingSummery(blbooking);
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
                    total = total + Convert.ToDouble(dt.Rows[i]["Paidamount"].ToString());
                }
                catch { }
            }
        }
        lblTotal.Text = "INR " + total.ToString("##,0");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        getall();
        //if (Session["getdata"] != null)
        //{
        //    DataTable dt = Session["getdata"] as DataTable;
        //    DataView dv = new DataView();
        //    DataSet ds = new DataSet();
        //    if (ddlaccom.SelectedIndex > 0 && txtfrom.Text == "" && txtTo.Text == "" && ddlagent.SelectedIndex == 0 && ddllagent.SelectedIndex == 0 && txtinto.Text == "" && txtinfrom.Text == "")
        //    {
        //        dv = new DataView(dt, "AccomName = '" + ddlaccom.SelectedItem.ToString() + "'", "AccomName", DataViewRowState.CurrentRows);
        //        dt = dv.ToTable();
        //        dgBookings.DataSource = dt;
        //        dgBookings.DataBind();
        //        calculatetot(dt);
        //        //dt.DefaultView.RowFilter = "AccomName='" + txtAccomdation.Text.Trim() + "'";
        //    }
        //    else if (ddlaccom.SelectedIndex == 0 && txtfrom.Text != "" && txtTo.Text != "" && ddlagent.SelectedIndex == 0 && ddllagent.SelectedIndex == 0 && txtinto.Text == "" && txtinfrom.Text == "")
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

        //    else if (ddlaccom.SelectedIndex == 0 && txtfrom.Text == "" && txtTo.Text == "" && ddlagent.SelectedIndex > 0 && ddllagent.SelectedIndex == 0 && txtinto.Text == "" && txtinfrom.Text == "")
        //    {
        //        dv = new DataView(dt, "Fagent = '" + ddlagent.SelectedItem.ToString() + "'", "Fagent", DataViewRowState.CurrentRows);
        //        dt = dv.ToTable();
        //        dgBookings.DataSource = dt;
        //        dgBookings.DataBind();
        //        calculatetot(dt);
        //        //dt.DefaultView.RowFilter = "Fagent='" + txtFagent.Text.Trim() + "'";
        //    }
        //    else if (ddlaccom.SelectedIndex == 0 && txtfrom.Text == "" && txtTo.Text == "" && ddlagent.SelectedIndex == 0 && ddllagent.SelectedIndex > 0 && txtinto.Text == "" && txtinfrom.Text == "")
        //    {
        //        dv = new DataView(dt, "Lagent = '" + ddllagent.SelectedItem.ToString() + "'", "Lagent", DataViewRowState.CurrentRows);
        //        dt = dv.ToTable();
        //        dgBookings.DataSource = dt;
        //        dgBookings.DataBind();
        //        calculatetot(dt);
        //        //dt.DefaultView.RowFilter = "Lagent='" + txtlAgent.Text.Trim() + "'";
        //    }
        //    else if (ddlaccom.SelectedIndex == 0 && txtfrom.Text == "" && txtTo.Text == "" && ddlagent.SelectedIndex == 0 && ddllagent.SelectedIndex == 0 && txtinto.Text != "" && txtinfrom.Text != "")
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

    protected void dgBookings_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgBookings.CurrentPageIndex = e.NewPageIndex;
        getall();
    }
    public void BindAccomNames1()
    {
        try
        {

            blCard._Action = "GetallAccomname";

            dtGetReturnedData = dlcard.GetallAccomname(blCard);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlaccom.Items.Clear();
                ddlaccom.DataSource = dtGetReturnedData;
                ddlaccom.DataTextField = "AccomName";
                ddlaccom.DataValueField = "AccomId";
                ddlaccom.DataBind();
                ListItem l = new ListItem("-Select Accom-", "0");
                ddlaccom.Items.Insert(0, l);
                //ddlaccom.Items.Insert(0, "-Select Accom-");

            }
            else
            {
                //othr.Visible = false;
                ddlaccom.Items.Clear();
                ddlaccom.DataSource = null;
                ddlaccom.DataBind();
                ddlaccom.Items.Insert(0, "-No Accom-");
            }
        }
        catch (Exception sqe)
        {
            ddlaccom.Items.Clear();
            ddlaccom.DataSource = null;
            ddlaccom.DataBind();
            ddlaccom.Items.Insert(0, "-No Accom-");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        FillAgents();
        getlocalagent();
        BindAccomNames1();
        txtfrom.Text = "";
        txtinfrom.Text = "";
        txtinto.Text = "";
        txtPaymentdue.Text = "";
        txtTo.Text = "";

        getall();
    }
}