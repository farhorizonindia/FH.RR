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
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.MasterServices;

public partial class ClientUI_PaymentRegister : MasterBasePage
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadaccom();
            //FillAgents();
            //getall();
        }
    }
    private void loadaccom()
    {

        try
        {

            blCard._Action = "GetallAccomname";

            dtGetReturnedData = dlcard.GetAccomname(blCard);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlaccom.Items.Clear();
                ddlaccom.DataSource = dtGetReturnedData;
                ddlaccom.DataTextField = "AccomName";
                ddlaccom.DataValueField = "AccomId";
                ddlaccom.DataBind();
                ListItem l = new ListItem("-Select-", "0");
                ddlaccom.Items.Insert(0, l);
                //ddlaccom.Items.Insert(0, "-Select-");
              
            }
            else
            {
                //ddlaccom.Visible = false;
                ddlaccom.Items.Clear();
                ddlaccom.DataSource = null;
                ddlaccom.DataBind();
                ddlaccom.Items.Insert(0, "-No Data-");
            }
        }
        catch (Exception sqe)
        {

        }
    }
    public void getall()
    {
        blbooking.AgentId = Convert.ToInt32(ddlAgent.SelectedValue);
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
        if (txtofrom.Text != "")
        {
            blbooking._dtPaymentFDate = DateTime.Parse(txtofrom.Text);
        }
        else
        {
            blbooking._dtPaymentFDate = DateTime.Parse("1990/01/01");
        }
        if (txtoto.Text != "")
        {
            blbooking._dtPaymentTDate = DateTime.Parse(txtoto.Text);
        }
        else
        {
            blbooking._dtPaymentTDate = DateTime.Parse("1990/01/01");
        }
        DataTable dt = dlbooking.getPaymentRegister(blbooking);
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
    private void FillAgents()
    {
        try
        {
            AgentMaster oAgentMaster = new AgentMaster();
            AgentDTO[] oAgentData = oAgentMaster.GetData();
            ListItemCollection li = new ListItemCollection();
            ListItem l = new ListItem("Choose Agent", "0");
            ddlAgent.Items.Insert(0, l);
            if (oAgentData != null)
            {
                for (int i = 0; i < oAgentData.Length; i++)
                {
                    l = new ListItem(oAgentData[i].AgentName.ToString(), oAgentData[i].AgentId.ToString());
                    ddlAgent.Items.Insert(i + 1, l);
                }
            }
        }
        catch { }
    }
    //private void getlocalagent()
    //{
    //    DataTable dt = dlAgentpayment.getlocalagent();
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        ddlAgentType.Items.Clear();
    //        ddlAgentType.DataSource = dt;

    //        ddlAgentType.DataTextField = "AgentName";
    //        ddlAgentType.DataValueField = "AgentId";
    //        ddlAgentType.DataBind();
    //        ddlAgentType.Items.Insert(0, "Select");

    //    }
    //}
    private void calculatetot(DataTable dt)
    {
        double total = 0;
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    total = total + Convert.ToDouble(dt.Rows[i]["PaidAmt"].ToString());
                }
                catch { }
            }
        }
        lblTotal.Text = "INR " + total.ToString("##,0");
    }

    protected void Button1_Click(object sender, EventArgs e)



    {
        //if (Session["getdata"] == null)
        //{
            getall();
        //}
        //if (Session["getdata"] != null)
        //{
        //    DataTable dt = Session["getdata"] as DataTable;
        //    DataView dv = new DataView();
        //    DataSet ds = new DataSet();
        //    if (ddlaccom.SelectedIndex > 0 && txtfrom.Text == "" && txtTo.Text == "" && ddlAgent.SelectedIndex == 0 && txtofrom.Text == "" && txtoto.Text == "")
        //    {
        //        dv = new DataView(dt, "AccomName = '" + ddlaccom.SelectedItem.ToString() + "'", "AccomName", DataViewRowState.CurrentRows);
        //        dt = dv.ToTable();
        //        dgBookings.DataSource = dt;
        //        dgBookings.DataBind();
        //        calculatetot(dt);
        //        //dt.DefaultView.RowFilter = "AccomName='" + txtAccomdation.Text.Trim() + "'";
        //    }
        //    else if (ddlaccom.SelectedIndex == 0 && txtfrom.Text != "" && txtTo.Text != "" && ddlAgent.SelectedIndex == 0 && txtofrom.Text == "" && txtoto.Text == "")
        //    {
        //       //ds= Session["getdata"] as DataSet;
        //        //ds.Tables.Add(dt);
        //        DataTable dt2 = new DataTable();
        //        //  dt2 = ds.Tables[0].Select().Where(p => (Convert.ToDateTime(p["StartDate"]) >= Convert.ToDateTime(txtfrom.Text)) && (Convert.ToDateTime(p["StartDate"]) >= Convert.ToDateTime(txtoto.Text))).CopyToDataTable();
        //        dt2 = dt.Select().Where(p => (Convert.ToDateTime(p["StartDate"]) >= Convert.ToDateTime(txtfrom.Text)) && (Convert.ToDateTime(p["StartDate"]) >= Convert.ToDateTime(txtoto.Text))).CopyToDataTable();
        //        dgBookings.DataSource = dt2;
        //        dgBookings.DataBind();
        //        calculatetot(dt2);
        //        ds.Clear();
        //        //dt.DefaultView.RowFilter = "StartDate >=#'" + Convert.ToDateTime(txtfrom.Text).Date + "' And  enddate<= #'" + Convert.ToDateTime(txtTo.Text).Date + "'";
        //    }
        //    else if (ddlaccom.SelectedIndex == 0 && txtfrom.Text == "" && txtTo.Text == "" && ddlAgent.SelectedIndex > 0 && txtofrom.Text == "" && txtoto.Text == "")
        //    {
        //        dv = new DataView(dt, "Agentname = '" + ddlAgent.SelectedItem.ToString() + "'", "Agentname", DataViewRowState.CurrentRows);
        //        dt = dv.ToTable();
        //        dgBookings.DataSource = dt;
        //        dgBookings.DataBind();
        //        calculatetot(dt);
        //        //dt.DefaultView.RowFilter = "Fagent='" + txtFagent.Text.Trim() + "'";
        //    }

        //    else if (ddlaccom.SelectedIndex == 0 && txtfrom.Text == "" && txtTo.Text == "" && ddlAgent.SelectedIndex == 0 && txtofrom.Text != "" && txtoto.Text != "")
        //    {
        //       // ds = Session["getdata"] as DataSet;
        //       // ds.Tables.Add(dt);
        //        DataTable dt2 = new DataTable();
        //        // dt2 = ds.Tables[0].Select().Where(p => (Convert.ToDateTime(p["InvoiceDate"]) >= Convert.ToDateTime(txtofrom.Text)) && (Convert.ToDateTime(p["InvoiceDate"]) >= Convert.ToDateTime(txtoto.Text))).CopyToDataTable();
        //        dt2 = dt.Select().Where(p => (Convert.ToDateTime(p["InvoiceDate"]) >= Convert.ToDateTime(txtofrom.Text)) && (Convert.ToDateTime(p["InvoiceDate"]) >= Convert.ToDateTime(txtoto.Text))).CopyToDataTable();
        //        dgBookings.DataSource = dt2;
        //        dgBookings.DataBind();
        //        calculatetot(dt2);
        //        ds.Clear();
        //        //dt.DefaultView.RowFilter = "InvoiceDate >=#'" + Convert.ToDateTime(txtinfrom.Text) + "' And  InvoiceDate<= #'" + Convert.ToDateTime(txtinto.Text) + "'";
        //    }

        //}
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Session["getdata"] != null)
        {
            DataTable dt = Session["getdata"] as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                dgBookings.DataSource = dt;
                dgBookings.DataBind();
            }
        }
        loadaccom();
      //  FillAgents();
        txtfrom.Text = "";
        txtofrom.Text = "";

        txtoto.Text = "";
        txtTo.Text = "";

    }

    protected void dgBookings_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void dgBookings_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgBookings.CurrentPageIndex = e.NewPageIndex;
        getall();
    }
}