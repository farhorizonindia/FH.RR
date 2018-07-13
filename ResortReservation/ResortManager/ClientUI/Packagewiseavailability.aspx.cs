using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using FarHorizon.Reservations.Bases.BasePages;

public partial class ClientUI_Packagewiseavailability : MasterBasePage
{
    int cityid = 0;
    string date = "";
    int Riverid = 0;
    public string PackageId = string.Empty;
    public DataTable dtres;
    BALSearch blsrch = new BALSearch();
    DALSearch dlsrch = new DALSearch();
    BALBooking blsr = new BALBooking();
    DALBooking dlsr = new DALBooking();
    public string PackageName = string.Empty;
    public string NoOfNight = string.Empty;
    public string CheckinDate = string.Empty;
    DALOpenDates dlOpenDates = new DALOpenDates();
    BALOpenDates blOpenDates = new BALOpenDates();
    BALRateCard blCard = new BALRateCard();
    DALRateCard dlcard = new DALRateCard();
    DALSearch dlsearch = new DALSearch();
    DataTable dtGetReturnedData;
    DALAgentPayment dlagent = new DALAgentPayment();
    DataTable newdata = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadpackage();
            //if (Session["Getalladata"] != null)
            //{
            //    dtres = Session["Getalladata"] as DataTable;
            //    dgBookings.DataSource = dtres;
            //    dgBookings.DataBind();
            //}
            //else
            //{
            //  //  fetchpackage();
            //}
            lblCurrentDateTime.Text = DateTime.Now.ToLongDateString();
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss IST");
        }
    }
    public DataTable bindroomddl(string packageid, int id)
    {
        try
        {
            blsr.action = "GetcruiseRooms";
            blsr.PackageId = packageid;

            blsr.DepartureId = id;

            if (Session["UserCode"] != null)
            {
                blsr._iAgentId = Convert.ToInt32(Session["UserCode"].ToString());
            }
            DataTable dt = new DataTable();
            dt = dlsr.GetCruiseRooms(blsr);
            if (dt != null)
            {

                return dt;
            }
            else
            {
                return null;

            }
        }
        catch
        {
            return null;
        }
    }
    private void loadpackage()
    {
        DataTable dt = dlsearch.fetchall();
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlPackagename.Items.Clear();
            ddlPackagename.DataSource = dt;
            ddlPackagename.DataTextField = "PackageName";
            ddlPackagename.DataValueField = "PackageId";
            ddlPackagename.DataBind();
            ddlPackagename.Items.Insert(0, "Select");
        }
    }
    public void fetchpackage()
    {
        dtres = new DataTable();
        blsr.PackageId = ddlPackagename.SelectedValue;
       DataTable dt = dlsearch.fetchFilterPackagewise(blsr);
        if (dt != null & dt.Rows.Count > 0)
        {
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
                blsrch.action = "PackagewiseavailbilityReport";
            // blsrch.PackageId = dt.Rows[i]["PackageId"].ToString();
            blsrch.PackageId = ddlPackagename.SelectedValue;
            if (txtFrom.Text != "")
                {
                    blsrch.StartDate = DateTime.Parse(txtFrom.Text);
                }
                else
                {
                    blsrch.StartDate = DateTime.Parse("1990/01/01");
                }
                if (txtTo.Text != "")
                {
                    blsrch.EndDate = DateTime.Parse(txtTo.Text);
                }
                else
                {
                    blsrch.EndDate = DateTime.Parse("1990/01/01");
                }
                
                blsrch.AgentId = 247;
                blsrch.PackageType = ddlCm.SelectedValue;
                blsrch.Openclose = Convert.ToInt32(ddlStatus.SelectedValue);
                newdata = dlsrch.PackagewiseavailbilityReport(blsrch);
                if (newdata != null && newdata.Rows.Count > 0)
                {
                    dtres.Merge(newdata);
                }
                else
                {
                    dgBookings.DataSource = newdata;
                    dgBookings.DataBind();
                }
            //}
<<<<<<< HEAD
            DateTime checkin = Convert.ToDateTime(txtFrom.Text);
            DateTime dd = Convert.ToDateTime("09/01/2019");
            if (checkin < Convert.ToDateTime(dd))
            {
                div1.Visible = true;
                div2.Visible = false;

                if (dtres != null && dtres.Rows.Count > 0)
                {
                    dtres.Columns.Add("Discount %", typeof(string));
                    dtres.Columns.Add("Suite", typeof(string));
                    dtres.Columns.Add("Swb", typeof(string));
                    dtres.Columns.Add("Swob", typeof(string));                
                    dtres.Columns.Add("OpenClose", typeof(string));
                    dtres.Columns.Add("Total", typeof(string));
                    DataRow dr = dtres.NewRow();
                    for (int i = 0; i < dtres.Rows.Count; i++)
                    {
                        // DataTable getdt = dlsrch.fetchdiscount(dtres.Rows[i]["packageId"].ToString(), Convert.ToDateTime(dtres.Rows[i]["CheckInDate"].ToString()), Convert.ToDateTime(dtres.Rows[i]["CheckOutDate"].ToString()), Convert.ToDecimal(dtres.Rows[i]["Rate"].ToString()));


                        DataTable dt45 = bindroomddl(dtres.Rows[i]["packageId"].ToString(), Convert.ToInt32(dtres.Rows[i]["Id"].ToString()));
                        if (dt45 != null && dt45.Rows.Count > 0)
                        {
                            DataView dv = new DataView();
                            dv = new DataView(dt45, "BookedStatus = 'Limited Availability' or BookedStatus = 'Available'", "BookedStatus", DataViewRowState.CurrentRows);
                            DataTable dtcruise1 = dv.ToTable();
                            dv = new DataView(dtcruise1, "RoomCategory = 'Suite'", "RoomCategory", DataViewRowState.CurrentRows);
                            DataTable dtcruise = dv.ToTable();
                            if (dtcruise != null && dtcruise.Rows.Count > 0)
                            {
                                int sum = Convert.ToInt32(dtcruise.Compute("SUM(Column1)", string.Empty));
                                dtres.Rows[i]["Suite"] = sum.ToString();
                            }
                            else
                            {
                                dtres.Rows[i]["Suite"] = "0";
                            }
                            DataView dv1 = new DataView();
                            dv1 = new DataView(dt45, "BookedStatus = 'Limited Availability' or BookedStatus = 'Available'", "BookedStatus", DataViewRowState.CurrentRows);
                            DataTable dtcruise11 = dv1.ToTable();
                            dv1 = new DataView(dtcruise1, "RoomCategory = 'Superior with Balcony'", "RoomCategory", DataViewRowState.CurrentRows);
                            DataTable dtcruise111 = dv1.ToTable();
                            if (dtcruise111 != null && dtcruise111.Rows.Count > 0)
                            {
                                int sum = Convert.ToInt32(dtcruise111.Compute("SUM(Column1)", string.Empty));
                                dtres.Rows[i]["Swb"] = sum.ToString();
                            }
                            else
                            {

                                dtres.Rows[i]["Swb"] = "0";
                            }
                            DataView dv2 = new DataView();
                            dv2 = new DataView(dt45, "BookedStatus = 'Limited Availability' or BookedStatus = 'Available'", "BookedStatus", DataViewRowState.CurrentRows);
                            DataTable dtcruise12 = dv2.ToTable();
                            dv2 = new DataView(dtcruise1, "RoomCategory = 'Superior without Balcony'", "RoomCategory", DataViewRowState.CurrentRows);
                            DataTable dtcruise123 = dv2.ToTable();
                            if (dtcruise123 != null && dtcruise123.Rows.Count > 0)
                            {
                                int sum = Convert.ToInt32(dtcruise123.Compute("SUM(Column1)", string.Empty));
                                dtres.Rows[i]["Swob"] = sum.ToString();
                            }
                            else
                            {
                                dtres.Rows[i]["Swob"] = "0";
                            }
                       
                            dtres.Rows[i]["Total"] = Convert.ToInt32(dtres.Rows[i]["Swob"].ToString()) + Convert.ToInt32(dtres.Rows[i]["Swb"].ToString()) + Convert.ToInt32(dtres.Rows[i]["Suite"].ToString());
                        }

                    }
                    DataTable getall = new DataTable();
                    DataView dv67 = new DataView();
                    Session["Getalladata"] = dtres;
                    dgBookings.DataSource = dtres;
                    dgBookings.DataBind();
                  
                }
                else
                {
                    dgBookings.DataSource = dtres;
                    dgBookings.DataBind();
                }
            }
            else
            {
                div2.Visible = true;
                div1.Visible = false;
                if (dtres != null && dtres.Rows.Count > 0)
                {
                    dtres.Columns.Add("Discount %", typeof(string));
                    dtres.Columns.Add("Suite", typeof(string));
                    dtres.Columns.Add("Swb", typeof(string));
                    dtres.Columns.Add("Swob", typeof(string));
                    dtres.Columns.Add("Lcwb", typeof(string));
                    dtres.Columns.Add("OpenClose", typeof(string));
                    dtres.Columns.Add("Total", typeof(string));
                    DataRow dr = dtres.NewRow();
                    for (int i = 0; i < dtres.Rows.Count; i++)
                    {
                        // DataTable getdt = dlsrch.fetchdiscount(dtres.Rows[i]["packageId"].ToString(), Convert.ToDateTime(dtres.Rows[i]["CheckInDate"].ToString()), Convert.ToDateTime(dtres.Rows[i]["CheckOutDate"].ToString()), Convert.ToDecimal(dtres.Rows[i]["Rate"].ToString()));


                        DataTable dt45 = bindroomddl(dtres.Rows[i]["packageId"].ToString(), Convert.ToInt32(dtres.Rows[i]["Id"].ToString()));
                        if (dt45 != null && dt45.Rows.Count > 0)
                        {
                            DataView dv = new DataView();
                            dv = new DataView(dt45, "BookedStatus = 'Limited Availability' or BookedStatus = 'Available'", "BookedStatus", DataViewRowState.CurrentRows);
                            DataTable dtcruise1 = dv.ToTable();
                            dv = new DataView(dtcruise1, "RoomCategory = 'Suite'", "RoomCategory", DataViewRowState.CurrentRows);
                            DataTable dtcruise = dv.ToTable();
                            if (dtcruise != null && dtcruise.Rows.Count > 0)
                            {
                                int sum = Convert.ToInt32(dtcruise.Compute("SUM(Column1)", string.Empty));
                                dtres.Rows[i]["Suite"] = sum.ToString();
                            }
                            else
                            {
                                dtres.Rows[i]["Suite"] = "0";
                            }
                            DataView dv1 = new DataView();
                            dv1 = new DataView(dt45, "BookedStatus = 'Limited Availability' or BookedStatus = 'Available'", "BookedStatus", DataViewRowState.CurrentRows);
                            DataTable dtcruise11 = dv1.ToTable();
                            dv1 = new DataView(dtcruise1, "RoomCategory = 'Superior with Balcony'", "RoomCategory", DataViewRowState.CurrentRows);
                            DataTable dtcruise111 = dv1.ToTable();
                            if (dtcruise111 != null && dtcruise111.Rows.Count > 0)
                            {
                                int sum = Convert.ToInt32(dtcruise111.Compute("SUM(Column1)", string.Empty)) - 2;
                                dtres.Rows[i]["Swb"] = sum.ToString();
                            }
                            else
                            {

                                dtres.Rows[i]["Swb"] = "0";
                            }
                            DataView dv2 = new DataView();
                            dv2 = new DataView(dt45, "BookedStatus = 'Limited Availability' or BookedStatus = 'Available'", "BookedStatus", DataViewRowState.CurrentRows);
                            DataTable dtcruise12 = dv2.ToTable();
                            dv2 = new DataView(dtcruise1, "RoomCategory = 'Superior without Balcony'", "RoomCategory", DataViewRowState.CurrentRows);
                            DataTable dtcruise123 = dv2.ToTable();
                            if (dtcruise123 != null && dtcruise123.Rows.Count > 0)
                            {
                                int sum = Convert.ToInt32(dtcruise123.Compute("SUM(Column1)", string.Empty));
                                dtres.Rows[i]["Swob"] = sum.ToString();
                            }
                            else
                            {
                                dtres.Rows[i]["Swob"] = "0";
                            }
                            DataView dv3 = new DataView();
                            dv3 = new DataView(dt45, "BookedStatus = 'Limited Availability' or BookedStatus = 'Available'", "BookedStatus", DataViewRowState.CurrentRows);
                            DataTable dtcruise3 = dv3.ToTable();
                            dv3 = new DataView(dtcruise3, "RoomCategory = 'Luxury Cabin with Balcony'", "RoomCategory", DataViewRowState.CurrentRows);
                            DataTable dtcruise33 = dv3.ToTable();
                            if (dtcruise33 != null && dtcruise33.Rows.Count > 0)
                            {
                                int sum = Convert.ToInt32(dtcruise33.Compute("SUM(Column1)", string.Empty));
                                dtres.Rows[i]["Lcwb"] = sum.ToString();
                            }
                            else
                            {
                                dtres.Rows[i]["Lcwb"] = "0";
                            }
                            dtres.Rows[i]["Total"] = Convert.ToInt32(dtres.Rows[i]["Swob"].ToString()) + Convert.ToInt32(dtres.Rows[i]["Swb"].ToString()) + Convert.ToInt32(dtres.Rows[i]["Suite"].ToString()) + Convert.ToInt32(dtres.Rows[i]["Lcwb"].ToString());
                        }



                        //if (getdt != null && getdt.Rows.Count > 0)
                        //{
                        //    dtres.Rows[i]["openclose"] = getdt.Rows[0]["openclose"].ToString();
                        //    //if (getdt.Rows[0]["openclose"].ToString() == "Close")
                        //    //{
                        //    //    dtres.Rows[i].Delete();
                        //    //    dtres.AcceptChanges();
                        //    //}
                        //}
                        //else
                        //{
                        //    dtres.Rows[i]["openclose"] = "Open";
                        //}
                        //DateTime now = Convert.ToDateTime(dtres.Rows[i]["CheckInDate"].ToString());

                        //DateTime endOfDay = now.AddMinutes(1);
                        //DateTime now1 = Convert.ToDateTime(DateTime.Now);
                        //DateTime startOfDay1 = now1.Date;
                        //DateTime endOfDay1 = startOfDay1.AddMinutes(1);

                        //DataRow dr = getdt.Rows[i];
                        //dr[3].Value = "New Value";
                    }
                    DataTable getall = new DataTable();
                    DataView dv67 = new DataView();
                    Session["Getalladata"] = dtres;
                    dgBookings1.DataSource = dtres;
                    dgBookings1.DataBind();
                    //dv67 = new DataView(dtres, "openclose = 'Open'", "openclose", DataViewRowState.CurrentRows);

                }
                else
                {
                    dgBookings1.DataSource = dtres;
                    dgBookings1.DataBind();
                }
=======
            if (dtres != null && dtres.Rows.Count > 0)
            {
                dtres.Columns.Add("Discount %", typeof(string));
                dtres.Columns.Add("Suite", typeof(string));
                dtres.Columns.Add("Swb", typeof(string));
                dtres.Columns.Add("Swob", typeof(string));
                dtres.Columns.Add("OpenClose", typeof(string));
                dtres.Columns.Add("Total", typeof(string));
                DataRow dr = dtres.NewRow();
                for (int i = 0; i < dtres.Rows.Count; i++)
                {
                    // DataTable getdt = dlsrch.fetchdiscount(dtres.Rows[i]["packageId"].ToString(), Convert.ToDateTime(dtres.Rows[i]["CheckInDate"].ToString()), Convert.ToDateTime(dtres.Rows[i]["CheckOutDate"].ToString()), Convert.ToDecimal(dtres.Rows[i]["Rate"].ToString()));
                   

                    DataTable dt45 = bindroomddl(dtres.Rows[i]["packageId"].ToString(), Convert.ToInt32(dtres.Rows[i]["Id"].ToString()));
                    if (dt45 != null && dt45.Rows.Count > 0)
                    {
                        DataView dv = new DataView();
                        dv = new DataView(dt45, "BookedStatus = 'Limited Availability' or BookedStatus = 'Available'", "BookedStatus", DataViewRowState.CurrentRows);
                        DataTable dtcruise1 = dv.ToTable();
                        dv = new DataView(dtcruise1, "RoomCategory = 'Suite'", "RoomCategory", DataViewRowState.CurrentRows);
                        DataTable dtcruise = dv.ToTable();
                        if (dtcruise != null && dtcruise.Rows.Count > 0)
                        {
                            int sum = Convert.ToInt32(dtcruise.Compute("SUM(Column1)", string.Empty));
                            dtres.Rows[i]["Suite"] = sum.ToString();
                        }
                        else
                        {
                            dtres.Rows[i]["Suite"] = "0";
                        }
                        DataView dv1 = new DataView();
                        dv1 = new DataView(dt45, "BookedStatus = 'Limited Availability' or BookedStatus = 'Available'", "BookedStatus", DataViewRowState.CurrentRows);
                        DataTable dtcruise11 = dv1.ToTable();
                        dv1 = new DataView(dtcruise1, "RoomCategory = 'Superior with Balcony'", "RoomCategory", DataViewRowState.CurrentRows);
                        DataTable dtcruise111 = dv1.ToTable();
                        if (dtcruise111 != null && dtcruise111.Rows.Count > 0)
                        {
                            int sum = Convert.ToInt32(dtcruise111.Compute("SUM(Column1)", string.Empty));
                            dtres.Rows[i]["Swb"] = sum.ToString();
                        }
                        else
                        {

                            dtres.Rows[i]["Swb"] = "0";
                        }
                        DataView dv2 = new DataView();
                        dv2 = new DataView(dt45, "BookedStatus = 'Limited Availability' or BookedStatus = 'Available'", "BookedStatus", DataViewRowState.CurrentRows);
                        DataTable dtcruise12 = dv2.ToTable();
                        dv2 = new DataView(dtcruise1, "RoomCategory = 'Superior without Balcony'", "RoomCategory", DataViewRowState.CurrentRows);
                        DataTable dtcruise123 = dv2.ToTable();
                        if (dtcruise123 != null && dtcruise123.Rows.Count > 0)
                        {
                            int sum = Convert.ToInt32(dtcruise123.Compute("SUM(Column1)", string.Empty));
                            dtres.Rows[i]["Swob"] = sum.ToString();
                        }
                        else
                        {
                            dtres.Rows[i]["Swob"] = "0";
                        }
                        dtres.Rows[i]["Total"] = Convert.ToInt32(dtres.Rows[i]["Swob"].ToString()) + Convert.ToInt32(dtres.Rows[i]["Swb"].ToString()) + Convert.ToInt32(dtres.Rows[i]["Suite"].ToString());
                    }



                    //if (getdt != null && getdt.Rows.Count > 0)
                    //{
                    //    dtres.Rows[i]["openclose"] = getdt.Rows[0]["openclose"].ToString();
                    //    //if (getdt.Rows[0]["openclose"].ToString() == "Close")
                    //    //{
                    //    //    dtres.Rows[i].Delete();
                    //    //    dtres.AcceptChanges();
                    //    //}
                    //}
                    //else
                    //{
                    //    dtres.Rows[i]["openclose"] = "Open";
                    //}
                    //DateTime now = Convert.ToDateTime(dtres.Rows[i]["CheckInDate"].ToString());

                    //DateTime endOfDay = now.AddMinutes(1);
                    //DateTime now1 = Convert.ToDateTime(DateTime.Now);
                    //DateTime startOfDay1 = now1.Date;
                    //DateTime endOfDay1 = startOfDay1.AddMinutes(1);

                    //DataRow dr = getdt.Rows[i];
                    //dr[3].Value = "New Value";
                }
                DataTable getall = new DataTable();
                DataView dv67 = new DataView();
                Session["Getalladata"] = dtres;
                dgBookings.DataSource = dtres;
                dgBookings.DataBind();
                //dv67 = new DataView(dtres, "openclose = 'Open'", "openclose", DataViewRowState.CurrentRows);

            }
            else
            {
                dgBookings.DataSource = dtres;
                dgBookings.DataBind();
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        fetchpackage();
        //if (Session["Getalladata"] != null)
        //{
        //    DataTable dt = Session["Getalladata"] as DataTable;
        //    DataView dv = new DataView();
        //    if (ddlCm.SelectedIndex > 0 && ddlPackagename.SelectedIndex == 0 && ddlStatus.SelectedIndex == 0 && txtFrom.Text == "" && txtTo.Text == "")
        //    {
        //        dv = new DataView(dt, "Packagetype = '" + ddlCm.SelectedItem.ToString() + "'", "Packagetype", DataViewRowState.CurrentRows);
        //        dt = dv.ToTable();

        //    }
        //    else if (ddlPackagename.SelectedIndex > 0 && ddlCm.SelectedIndex == 0 && ddlStatus.SelectedIndex == 0 && txtFrom.Text == "" && txtTo.Text == "")
        //    {
        //        dv = new DataView(dt, "NamePack = '" + ddlPackagename.SelectedItem.ToString() + "'", "NamePack", DataViewRowState.CurrentRows);
        //        dt = dv.ToTable();
        //    }
        //    else if (ddlPackagename.SelectedIndex == 0 && ddlCm.SelectedIndex == 0 && ddlStatus.SelectedIndex == 0 && txtFrom.Text != "" && txtTo.Text != "")
        //    {
        //        DataSet ds = new DataSet();
        //        ds.Tables.Add(dt);
        //        DataTable dt2 = new DataTable();
        //        dt2 = ds.Tables[0].Select().Where(p => (Convert.ToDateTime(p["CheckInDate"]) >= Convert.ToDateTime(txtFrom.Text)) && (Convert.ToDateTime(p["CheckOutDate"]) >= Convert.ToDateTime(txtTo.Text))).CopyToDataTable();



        //    }
        //    else if (ddlPackagename.SelectedIndex == 0 && ddlCm.SelectedIndex == 0 && ddlStatus.SelectedIndex > 0 && txtFrom.Text == "" && txtTo.Text == "")
        //    {
        //        if (ddlStatus.SelectedItem.ToString() == "All")
        //        { }
        //        else
        //        {
        //            dv = new DataView(dt, "openclose = '" + ddlStatus.SelectedItem.ToString() + "'", "openclose", DataViewRowState.CurrentRows);
        //            dt = dv.ToTable();
        //            //dt.DefaultView.RowFilter = "openclose='" + ddlStatus.SelectedItem.ToString() + "'";
        //        }
        //    }
        //    //else if (txtinfrom.Text != "" && txtinto.Text != "")
        //    //{
        //    //    dt.DefaultView.RowFilter = "InvoiceDate >=#'" + Convert.ToDateTime(txtinfrom.Text) + "' And  InvoiceDate<= #'" + Convert.ToDateTime(txtinto.Text) + "'";
        //    //}
        //    dgBookings.DataSource = dt;
        //    dgBookings.DataBind();
        //    //calculatetot(dt);
        //}
    }

    protected void dgBookings_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgBookings.CurrentPageIndex = e.NewPageIndex;
        if (Session["Getalladata"] != null)
        {
            dtres = Session["Getalladata"] as DataTable;
            dgBookings.DataSource = dtres;
            dgBookings.DataBind();
        }
    }

<<<<<<< HEAD

    protected void dgBookings1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgBookings1.CurrentPageIndex = e.NewPageIndex;
        if (Session["Getalladata"] != null)
        {
            dtres = Session["Getalladata"] as DataTable;
            dgBookings1.DataSource = dtres;
            dgBookings1.DataBind();
        }
    }

=======
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
    protected void Button2_Click(object sender, EventArgs e)
    {
        loadpackage();
        txtFrom.Text = "";
        txtTo.Text = "";
        if (Session["Getalladata"] != null)
        {
            dtres = Session["Getalladata"] as DataTable;
            dgBookings.DataSource = dtres;
            dgBookings.DataBind();
        }
    }
}