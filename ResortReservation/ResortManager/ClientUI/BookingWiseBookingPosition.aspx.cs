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

public partial class ClientUI_BookingWiseBookingPosition : MasterBasePage
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
    public DataTable dt;
    public DataTable dt1;
    public DataTable dt2;
    public DataTable dt3;
    public DataTable dt4;
    public DataTable dt5;
    public DataTable dt6;
    public DataTable dt7;
    public DataTable dtgroupby = new DataTable();
    public DataTable dtall;
    protected void Page_Load(object sender, EventArgs e)
    {
        //getall();
        if (!IsPostBack)
        {
            fetchpackage();
            lblCurrentDateTime.Text = DateTime.Now.ToLongDateString();
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss IST");
        }
    }
    private void getall()
    {
        // blbooking.action = "Upstream";
        ///  blbooking.PackageId = ddlPackage.SelectedValue;
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
        dtall = dlbooking.getmonthlyrevenueAll(blbooking);

        if (ddlPackage.SelectedValue != "All Packages")
        {
            DataView dvopendates = new DataView(dtall);
            string filter = "packageid='" + ddlPackage.SelectedValue + "'";
            dvopendates.RowFilter = filter;

            dtall = dvopendates.ToTable();

        }


        DateTime str = blbooking._dtStartDate.AddDays(7);
        DateTime end = blbooking._dtEndDate;



        //var query = from row in dtall.AsEnumerable()
        //                // group row by new { ShortPackName=row.Field<string>("ShortPackName"), StartDate = row.Field<DateTime>("StartDate").Date, BoardingFrom= row.Field<string>("BordingFrom"),BoardingTo=row.Field<string>("BoadingTo") } into z
        //            group row by new {   EndDate = row.Field<DateTime>("Edate").Date } into z
        //            select new
        //              {
        //                EndDate = z.Key.EndDate,
        //               //  BoardingFrom=z.Key.BoardingFrom,
        //               //   BoardingTo=z.Key.BoardingTo,
        //               //   ShortPackName=z.Key.ShortPackName
        //              };

        dtgroupby.Columns.Add(new DataColumn("EndDate", typeof(string)));
        //  dtgroupby.Columns.Add(new DataColumn("BordingFrom", typeof(string)));
        // dtgroupby.Columns.Add(new DataColumn("BoadingTo", typeof(string)));
        //dtgroupby.Columns.Add(new DataColumn("ShortPackName", typeof(string)));
        dtgroupby.Columns.Add(new DataColumn("StartDate", typeof(string)));


        for (DateTime i = blbooking._dtStartDate; i < end; i = i.AddDays(7))
        {

            string strtdate = i.ToShortDateString();
            string enddate = i.AddDays(7).ToShortDateString();
            DataRow row = dtgroupby.NewRow();

            row["StartDate"] = strtdate;

            row["EndDate"] = enddate;

            dtgroupby.Rows.Add(row);


        }



        if (dtall.Rows.Count > 0)
        {
            string firstrow = dtall.Rows[0]["ShortPackName"].ToString();

            if (firstrow == "Upstream")
            {




                DataView dvopendates = new DataView(dtall);
                string filter = "ShortPackName='Upstream'";
                dvopendates.RowFilter = filter;

                dt = dvopendates.ToTable();

                DataView dvopendates2 = new DataView(dtall);
                string filter2 = "ShortPackName='Downstream'";
                dvopendates2.RowFilter = filter2;

                dt1 = dvopendates2.ToTable();
                getUpstreamCalculation();
                getDownstreamCalculation();
            }

            if (firstrow == "Downstream")
            {
                DataView dvopendates = new DataView(dtall);
                string filter = "ShortPackName='Downstream'";
                dvopendates.RowFilter = filter;

                dt = dvopendates.ToTable();



                DataView dvopendates2 = new DataView(dtall);
                string filter2 = "ShortPackName='Upstream'";
                dvopendates2.RowFilter = filter2;

                dt1 = dvopendates2.ToTable();
                getUpstreamCalculation2();
                getDownstreamCalculation2();
            }

        }






        Session["getdata"] = dt;
        if (dt != null && dt.Rows.Count > 0)
        {

        }
    }
    private void getallDownstream()
    {
        blbooking.action = "Downstream";
        blbooking.PackageId = ddlPackage.SelectedValue;
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
        dt1 = dlbooking.getmonthlyrevenue(blbooking);
        // Session["getdata"] = dt1;
        if (dt1 != null && dt1.Rows.Count > 0)
        {

        }
    }
    private void getUpstreamCalculation()
    {
        blbooking.action = "Upstream";
        blbooking.PackageId = ddlPackage.SelectedValue;
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
        dt2 = dlbooking.getUpstreamCalculation(blbooking);
        // Session["getdata"] = dt2;
        if (dt2 != null && dt2.Rows.Count > 0)
        {

        }
    }


    private void getUpstreamCalculation2()
    {
        blbooking.action = "Upstream";
        blbooking.PackageId = ddlPackage.SelectedValue;
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
        dt3 = dlbooking.getUpstreamCalculation(blbooking);
        // Session["getdata"] = dt2;
        if (dt3 != null && dt3.Rows.Count > 0)
        {

        }
    }
    private void getDownstreamCalculation2()
    {
        blbooking.action = "Downstream";
        blbooking.PackageId = ddlPackage.SelectedValue;
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
        dt2 = dlbooking.getDownstreamCalculation(blbooking);
        // Session["getdata"] = dt2;
        if (dt2 != null && dt2.Rows.Count > 0)
        {

        }
    }
    private void getDownstreamCalculation()
    {
        blbooking.action = "Downstream";
        blbooking.PackageId = ddlPackage.SelectedValue;
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
        dt3 = dlbooking.getDownstreamCalculation(blbooking);
        // Session["getdata"] = dt2;
        if (dt3 != null && dt3.Rows.Count > 0)
        {

        }
    }
    //private void getUpstreamPackage()
    //{
    //    blbooking.PackageId = ddlPackage.SelectedValue;
    //    if (txtfrom.Text != "")
    //    {
    //        blbooking._dtStartDate = DateTime.Parse(txtfrom.Text);
    //    }
    //    else
    //    {
    //        blbooking._dtStartDate = DateTime.Parse("1990/01/01");
    //    }
    //    if (txtTo.Text != "")
    //    {
    //        blbooking._dtEndDate = DateTime.Parse(txtTo.Text);
    //    }
    //    else
    //    {
    //        blbooking._dtEndDate = DateTime.Parse("1990/01/01");
    //    }
    //    dt4 = dlbooking.getUpstreamPackage(blbooking);
    //    // Session["getdata"] = dt2;
    //    if (dt4 != null && dt4.Rows.Count > 0)
    //    {

    //    }
    //}
    //private void getDownstreamPackage()
    //{
    //    blbooking.PackageId = ddlPackage.SelectedValue;
    //    if (txtfrom.Text != "")
    //    {
    //        blbooking._dtStartDate = DateTime.Parse(txtfrom.Text);
    //    }
    //    else
    //    {
    //        blbooking._dtStartDate = DateTime.Parse("1990/01/01");
    //    }
    //    if (txtTo.Text != "")
    //    {
    //        blbooking._dtEndDate = DateTime.Parse(txtTo.Text);
    //    }
    //    else
    //    {
    //        blbooking._dtEndDate = DateTime.Parse("1990/01/01");
    //    }
    //    dt5 = dlbooking.getDownstreamPackage(blbooking);
    //    // Session["getdata"] = dt2;
    //    if (dt5 != null && dt5.Rows.Count > 0)
    //    {

    //    }
    //}
    public void fetchpackage()
    {
        DataTable dt = dlsearch.fetchall();
        if (dt != null & dt.Rows.Count > 0)
        {
            ddlPackage.Items.Insert(0, "-Select Package-");
            ddlPackage.DataSource = dt;
            ddlPackage.DataTextField = "PackageName";
            ddlPackage.DataValueField = "PackageId";
            ddlPackage.DataBind();
            ddlPackage.Items.Insert(0, "All Packages");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

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




        getall();
        //  getallDownstream();
        //  getUpstreamCalculation();
        //  getDownstreamCalculation();
        //getUpstreamPackage();
        // getDownstreamPackage();
        //    DataView dv = new DataView();
        //    DataSet ds = new DataSet();
        //    if (Session["getdata"] != null)
        //    {
        //        dt = Session["getdata"] as DataTable;
        //    }
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        if (txtfrom.Text != "" && txtTo.Text != "" && ddlPackage.SelectedIndex == 0 && ddlStatus.SelectedIndex == 0)
        //        {
        //            ds.Tables.Add(dt);
        //            DataTable dt2 = new DataTable();
        //            dt = ds.Tables[0].Select().Where(p => (Convert.ToDateTime(p["StartDate"]) >= Convert.ToDateTime(txtfrom.Text)) && (Convert.ToDateTime(p["StartDate"]) >= Convert.ToDateTime(txtfrom.Text))).CopyToDataTable();

        //            ds.Clear();
        //        }
        //        else if (txtfrom.Text != "" && txtTo.Text != "" && ddlPackage.SelectedIndex > 0 && ddlStatus.SelectedIndex == 0)
        //        {
        //            ds.Tables.Add(dt);
        //            DataTable dt2 = new DataTable();
        //            dt = ds.Tables[0].Select().Where(p => (Convert.ToDateTime(p["StartDate"]) >= Convert.ToDateTime(txtfrom.Text)) && (Convert.ToDateTime(p["StartDate"]) >= Convert.ToDateTime(txtfrom.Text))).CopyToDataTable();
        //            dv = new DataView(dt, "PackageName = '" + ddlPackage.SelectedItem.ToString() + "'", "PackageName", DataViewRowState.CurrentRows);
        //            dt = dv.ToTable();

        //        }
        //        else if (txtfrom.Text != "" && txtTo.Text != "" && ddlPackage.SelectedIndex > 0 && ddlStatus.SelectedIndex > 0)
        //        {
        //            ds.Tables.Add(dt);
        //            DataTable dt2 = new DataTable();
        //            dt = ds.Tables[0].Select().Where(p => (Convert.ToDateTime(p["StartDate"]) >= Convert.ToDateTime(txtfrom.Text)) && (Convert.ToDateTime(p["StartDate"]) >= Convert.ToDateTime(txtfrom.Text))).CopyToDataTable();
        //            dv = new DataView(dt, "PackageName = '" + ddlPackage.SelectedItem.ToString() + "'", "PackageName", DataViewRowState.CurrentRows);
        //            dt = dv.ToTable();
        //            dv = new DataView(dt, "Staus = '" + ddlStatus.SelectedItem.ToString() + "'", "Staus", DataViewRowState.CurrentRows);
        //            dt = dv.ToTable();

        //        }
        //    }
    }
<<<<<<< HEAD

    protected void txtTo_TextChanged(object sender, EventArgs e)
    {
        DateTime checkin = Convert.ToDateTime(txtTo.Text);
        DateTime dd = Convert.ToDateTime("09/01/2019");
        if (checkin < Convert.ToDateTime(dd))
        {
            div2.Visible = false;
            div1.Visible = true;
            
        }
        else
        {
            div1.Visible = false;
            div2.Visible = true;

        }
    }
=======
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
}