using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using FarHorizon.Reservations.Bases.BasePages;
public partial class MasterUI_DiscountEntry : MasterBasePage
{
    DALOpenDates dlOpenDates = new DALOpenDates();
    BALOpenDates blOpenDates = new BALOpenDates();
    BALRateCard blCard = new BALRateCard();
    DALRateCard dlcard = new DALRateCard();
    DALSearch dlsearch = new DALSearch();
    DataTable dtGetReturnedData;
    int cityid = 0;
    BALBooking blsr = new BALBooking();
    DALBooking dlsr = new DALBooking();
    string date = "";
    string package = "";
    string month = "";
    string year = "";
    int Riverid = 0;
    DataTable dt;
    public string PackageId = string.Empty;
    public DataTable dtres;
    BALSearch blsrch = new BALSearch();
    DALSearch dlsrch = new DALSearch();
    DALPackageRateCard dalp = new DALPackageRateCard();
    DataTable distinctValues;
    public string CheckinDep;
    public string CheckoutDep;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["getadmin"] != null)
        {
            //if (Session["getadmin"].ToString() == "admin")
            //{
            //    gvPaymentEntryInfo.Columns[8].Visible = false;
            //}
            //else
            //{
            //    gvPaymentEntryInfo.Columns[8].Visible = true;
            //}
        }
        if (!IsPostBack)
        {

            fetchpackage();
            loadyear();
            loadmonth();
        }
    }
    public DataTable bindroomddl(int departureid)
    {
        try
        {
            blsr.action = "GetcruiseRooms";
            blsr.PackageId = ddlPackage.SelectedValue.ToString();


            blsr.DepartureId = departureid;

            if (Session["UserCode"] != null)
            {
                blsr._iAgentId = Convert.ToInt32(Session["UserCode"].ToString());
            }
            dt = new DataTable();
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
            ddlPackage.Items.Insert(0, "-Select Package-");
        }
    }
    private void loadmonth()
    {
        DataTable dt = dlsearch.getavaialablemonth();
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlMonth.DataSource = dt;
            ddlMonth.DataTextField = "Name";
            ddlMonth.DataValueField = "Name";
            ddlMonth.DataBind();
            ddlMonth.Items.Insert(0, "Select Month");

        }
    }
    public void getbyfilter(string cid, string date)
    {
        try
        {
            string[] arr = date.Split(' ');
            blsrch.action = "getbyfilter";
            if (date != "0" && date != "Month--Year--")
            {
                try
                {
                    string mnth = arr[0].Substring(0, 3);
                    int mdigit = DateTime.ParseExact(mnth, "MMM", CultureInfo.InvariantCulture).Month;
                    var firstDayOfMonth = new DateTime(Convert.ToInt32(arr[1]), mdigit, 1);
                    var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                    CheckinDep = firstDayOfMonth.ToString();
                    CheckoutDep = lastDayOfMonth.ToString();

                    blsrch.StartDate = firstDayOfMonth.Date;
                    blsrch.EndDate = lastDayOfMonth.Date;
                }
                catch
                {
                    blsrch.StartDate = Convert.ToDateTime("1900-01-01");
                    blsrch.EndDate = Convert.ToDateTime("1900-01-01");
                }
            }
            else
            {
                blsrch.StartDate = Convert.ToDateTime("1900-01-01");
                blsrch.EndDate = Convert.ToDateTime("1900-01-01");
            }

            blsrch.PackageId = cid;
            //if (rid != 0)
            //{
            //    blsrch.RiverId = rid;
            //}
            //dtres = getbyfilter1(Convert.ToDateTime(CheckinDep), Convert.ToDateTime(CheckoutDep), cid);
            dtres = dlsrch.getbyfilter1(blsrch);
            DataView view = new DataView(dtres);
            dtres = view.ToTable(true, "packageId", "Img", "PackageName", "BFrom", "BTo", "NoOfNights", "PackageDescription", "ItineraryLink");
            //var myResult = dtres.AsEnumerable().Select(c => (DataRow)c["packageId"]).Distinct().ToList();
            Session["forimage"] = dtres.Rows[0]["img"].ToString();
            //dtres = dlsrch.getbyfilter(Convert.ToDateTime(CheckinDep), Convert.ToDateTime(CheckoutDep), cid);
        }
        catch (Exception ex)
        {
        }
    }
    public void getpackagesearchresults(string package, string checkin, string checkout)
    {
        #region GetSearch data
        try
        {
            string rate = "";
            string availabilty = "";
            blsrch.action = "GetOpenDatesCruise";
            //if (Session["PackId"] == null)
            //{
            //    Session["PackId"] = Request.QueryString["PackId"];
            //}

            blsrch.PackageId = package;

            if (checkin != "" && checkin != null && checkout != "" && checkout != null)
            {
                //Session["CheckinDep"] = Request.QueryString["CheckinDep"];
                //Session["CheckoutDep"] = Request.QueryString["CheckoutDep"];



                blsrch.StartDate = Convert.ToDateTime(checkin);
                blsrch.EndDate = Convert.ToDateTime(checkout);
            }
            else
            {
                blsrch.StartDate = System.DateTime.Now;
                blsrch.EndDate = Convert.ToDateTime("1900-01-01");
            }

            if (Session["UserCode"] != null)
            {
                blsrch.AgentId = Convert.ToInt32(Session["UserCode"]);
            }
            else
            {
                blsrch.AgentId = 247;
            }
            DataTable dt12 = new DataTable();
<<<<<<< HEAD


            DataTable dtsorted = new DataTable();
            dtsorted = dlsrch.GetCruiseOpenDatesPackage(blsrch);

            if (dtsorted != null && dtsorted.Rows.Count > 0)
            {
                DataView dv = dtsorted.DefaultView;
                dv.Sort = "CheckInDate  ASC";
                dtres = dv.ToTable();

                // dtsorted.DefaultView.Sort = "CheckInDate  ASC";
            }
           // dtres = dtsorted;


            //   dtres = dlsrch.GetCruiseOpenDatesPackage(blsrch);
=======
            dtres = dlsrch.GetCruiseOpenDatesPackage(blsrch);
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955

            DataTable dtnew = new DataTable();
            dtnew.Columns.Add("Id", typeof(int));
            dtnew.Columns.Add("DepurtureId", typeof(int));
            dtnew.Columns.Add("Boarding Date", typeof(string));
            dtnew.Columns.Add("De-Boarding Date", typeof(string));
            dtnew.Columns.Add("Nights", typeof(string));
            dtnew.Columns.Add("Price", typeof(string));
            dtnew.Columns.Add("Availability", typeof(string));
            dtnew.Columns.Add("condition", typeof(string));
            dtnew.Columns.Add("Discount %", typeof(string));
            dtnew.Columns.Add("LastMinutessale %", typeof(string));
            dtnew.Columns.Add("openclose", typeof(string));
            //dtnew.Columns.Add("Enter Discount Amount", typeof(string));
            for (int i = 0; i < dtres.Rows.Count; i++)
            {





                DataRow dr = dtnew.NewRow();

                dr["Boarding Date"] = Convert.ToDateTime(dtres.Rows[i]["CheckInDate"]).ToString("dddd, MMMM d, yyyy");
                dr["DepurtureId"] = dtres.Rows[i]["Id"].ToString();
                dr["De-Boarding Date"] = Convert.ToDateTime(dtres.Rows[i]["CheckInDate"]).AddDays(Convert.ToInt32(dtres.Rows[i]["NoOfNights"])).ToString("dddd, MMMM d, yyyy");
                dr["Nights"] = dtres.Rows[i]["NoOfNights"].ToString();
                if (dtres.Rows[i]["Rate"].ToString() == "0.00")
                {
                    rate = "0.00";
                }
                else
                {
                    rate = (dtres.Rows[i]["Rate"]).ToString();
                }
                dr["Price"] = dtres.Rows[i]["Currency"].ToString() + rate;
                if (dtres.Rows[i]["Availability"].ToString() == "Limited Availability")
                {
                    availabilty = "<span style='color:#f99646'> " + dtres.Rows[i]["Availability"].ToString() + " </span>";
                }
                if (dtres.Rows[i]["Availability"].ToString() == "Available")
                {
                    availabilty = "<span style='color:#4f81bd'> " + dtres.Rows[i]["Availability"].ToString() + " </span>";
                }

                if (dtres.Rows[i]["Availability"].ToString() == "Sold Out")
                {
                    availabilty = "<span style='color:Red'> " + dtres.Rows[i]["Availability"].ToString() + " </span>";
                }
                dr["Availability"] = availabilty;
                dr["condition"] = dtres.Rows[i]["Availability"].ToString();
                try
                {
                    DataTable getdt = dlsrch.fetchdiscount(dtres.Rows[i]["packageId"].ToString(), Convert.ToDateTime(dtres.Rows[i]["CheckInDate"].ToString()), Convert.ToDateTime(dtres.Rows[i]["CheckOutDate"].ToString()), Convert.ToDecimal(dtres.Rows[i]["Rate"].ToString()));
                    dr["Discount %"] = getdt.Rows[0]["Discountamount"].ToString();
                    dr["LastMinutessale %"] = getdt.Rows[0]["LastMinutessale"].ToString();
                    if (getdt.Rows[0]["MasterPackageId"].ToString() != "")
                    {
                        dr["openclose"] = "Not allow";
                    }
                    else
                    {
                        dr["openclose"] = getdt.Rows[0]["openclose"].ToString();

                    }

                    dr["Id"] = Convert.ToInt32(getdt.Rows[0]["Id"].ToString());
                }
                catch { }
                //DataRow dr = getdt.Rows[i];
                //dr[3].Value = "New Value";
                //dr["Discount %"] = "";
                dtnew.Rows.Add(dr);
            }
            if (dtnew != null && dtnew.Rows.Count > 0)
            {
                gvPaymentEntryInfo.DataSource = dtnew;
                gvPaymentEntryInfo.DataBind();
                btnSave.Visible = true;
            }
            //DataTable dt = dlsrch.fetchall();
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        blsrch.PackageId = dt.Rows[i]["PackageId"].ToString();
            //        dtres = dlsrch.GetCruiseOpenDatesPackage(blsrch);
            //        dt12.Merge(dtres);
            //        dt12.AcceptChanges();

            //    }
            //}


        }
        catch (Exception ex)
        {

        }
        #endregion
    }
    private void loadyear()
    {
        ddlYear.Items.Clear();
        ListItem[] itens = new ListItem[4] {
    new ListItem("Select Year"),
    new ListItem(DateTime.Today.Year.ToString()),
    new ListItem(DateTime.Today.AddYears(+1).Year.ToString()),
    new ListItem(DateTime.Today.AddYears(+2).Year.ToString()) };

        ddlYear.Items.AddRange(itens);
        //ddlYear.Items.Clear();
        //ddlYear.Items.Add("--Year--");
        //var currentYear = DateTime.Today.Year;
        //for (int i = 2; i >= 0; i++)
        //{
        //    // Now just add an entry that's the current year minus the counter
        //    ddlYear.Items.Add((currentYear - i).ToString());
        //}
    }
    private void refreshgrid()
    {
        if (ddlPackage.SelectedIndex > 0)
        {
            package = ddlPackage.SelectedValue.ToString();
        }
        year = ddlYear.SelectedValue;
        month = ddlMonth.SelectedItem.ToString();
        getbyfilter(package, month + " " + year);
        getpackagesearchresults(package, CheckinDep, CheckoutDep);
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        if (ddlPackage.SelectedIndex > 0)
        {
            package = ddlPackage.SelectedValue.ToString();
        }
        year = ddlYear.SelectedValue;
        month = ddlMonth.SelectedItem.ToString();
        getbyfilter(package, month + " " + year);
        getpackagesearchresults(package, CheckinDep, CheckoutDep);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlPackage.SelectedIndex > 0)
        {
            for (int i = 0; i < gvPaymentEntryInfo.Rows.Count; i++)
            {
                Label lblBoardingDate = gvPaymentEntryInfo.Rows[Convert.ToInt32(i)].Cells[0].FindControl("lblBoardingDate") as Label;
                Label lblDeBoardingDate = gvPaymentEntryInfo.Rows[Convert.ToInt32(i)].Cells[1].FindControl("lblDeBoardingDate") as Label;
                Label lblNight = gvPaymentEntryInfo.Rows[Convert.ToInt32(i)].Cells[2].FindControl("lblNight") as Label;
                Label lblPrice = gvPaymentEntryInfo.Rows[Convert.ToInt32(i)].Cells[3].FindControl("lblPrice") as Label;
                Label lblDiscount = gvPaymentEntryInfo.Rows[Convert.ToInt32(i)].Cells[3].FindControl("lbldiscountpercentage") as Label;
                Label lblLast = gvPaymentEntryInfo.Rows[Convert.ToInt32(i)].Cells[3].FindControl("lblLastminutes") as Label;
                string price = lblPrice.Text.ToString().Split('R')[1];
                TextBox txtreceive = gvPaymentEntryInfo.Rows[Convert.ToInt32(i)].Cells[6].FindControl("txtreceive") as TextBox;
                TextBox txtlastminute = gvPaymentEntryInfo.Rows[Convert.ToInt32(i)].Cells[7].FindControl("txtlastminute") as TextBox;
                double recieve = 0;
                double lastminute = 0;
                if (txtreceive.Text == "")
                {
                    try
                    {
                        recieve = Convert.ToDouble(lblDiscount.Text);
                    }
                    catch
                    {
                        recieve = 0;
                    }
                }
                else
                {
                    recieve = Convert.ToDouble(txtreceive.Text);
                }
                if (txtlastminute.Text == "")
                {
                    try
                    {
                        lastminute = Convert.ToDouble(lblLast.Text);
                    }

                    catch { lastminute = 0; }

                }
                else
                {
                    lastminute = Convert.ToDouble(txtlastminute.Text);
                }
                int n = dalp.savediscount(ddlPackage.SelectedValue.ToString(), Convert.ToDateTime(lblBoardingDate.Text), Convert.ToDateTime(lblDeBoardingDate.Text), Convert.ToDecimal(price), Convert.ToDecimal(recieve), Convert.ToDecimal(lastminute));
                if (n > 0)
                {

                }
            }
            if (ddlPackage.SelectedIndex > 0)
            {
                package = ddlPackage.SelectedValue.ToString();
            }
            year = ddlYear.SelectedValue;
            month = ddlMonth.SelectedItem.ToString();
            getbyfilter(package, month + " " + year);
            getpackagesearchresults(package, CheckinDep, CheckoutDep);
            fetchpackage();
            loadyear();
            loadmonth();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('Please select package')", true);
            return;
        }
    }

    protected void gvPaymentEntryInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        LinkButton txt = (LinkButton)gvPaymentEntryInfo.Rows[0].FindControl("lnkOpen");
        if (txt.Text != "Not allow")
        {
            blsr.action = "getdepartureid";
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            byte get = 0;
            DataTable dt = dlsrch.fetchbydiscountid(id);
            if (dt != null && dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["openclose"].ToString() == "Close")
                {
                    get = 1;
                }
                else
                {
                    get = 0;
                }
            }
            string lh = e.CommandName.ToString();
            DataTable dt1 = bindroomddl(Convert.ToInt32(e.CommandName.ToString()));

            DataView dv = new DataView();
            dv = new DataView(dt1, "BookedStatus='Not Available'", "BookedStatus", DataViewRowState.CurrentRows);
            DataTable dt2 = dv.ToTable();
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('Close can only happen if there is no booking on those departures')", true);
                return;

            }
            else
            {
                blsr.action = "getchilid";
                blsr.PackageId = ddlPackage.SelectedValue.ToString();
                DataTable dtchild = dlsr.getchilid(blsr);
                if (dtchild != null && dtchild.Rows.Count > 0)
                {
                    for (int i = 0; i < dtchild.Rows.Count; i++)
                    {
                        blsr.action = "getdepartureid";
                        blsr.PackageId = dtchild.Rows[i]["PackageId"].ToString();
                        blsr._dtStartDate = Convert.ToDateTime(dt.Rows[0]["Boardingdate"].ToString());
                        blsr._dtEndDate = Convert.ToDateTime(dt.Rows[0]["Deboardingdate"].ToString());
                        DataTable dtgetdepurtureid = dlsr.getdepartureid(blsr);
                        if (dtgetdepurtureid != null && dtgetdepurtureid.Rows.Count > 0)
                        {
                            blsr.action = "GetcruiseRooms";
                            blsr.DepartureId = Convert.ToInt32(dtgetdepurtureid.Rows[0]["Id"].ToString());
                            DataTable dtgetchild = dlsr.GetCruiseRooms(blsr);
                            DataView dvgetchild = new DataView();
                            dvgetchild = new DataView(dtgetchild, "BookedStatus='Not Available'", "BookedStatus", DataViewRowState.CurrentRows);
                            dtgetchild = dvgetchild.ToTable();
                            if (dtgetchild != null && dtgetchild.Rows.Count > 0)
                            {
                                Session["set"] = null;
                            }
                            else
                            {
                                Session["set"] = 1;
                            }
                        }
                    }
                }
                if (Session["set"] != null)
                {
                    int n = dalp.updateopenclose(id);
                    if (n == 1)
                    {
                        for (int i = 0; i < dtchild.Rows.Count; i++)
                        {
                            try
                            {
                                //int n1 = dalp.updateopenclosebydate(dtchild.Rows[i]["PackageId"].ToString(), Convert.ToDateTime(dt.Rows[0]["Boardingdate"].ToString()));
                                //if (n > 1)
                                //{

                                //}
                            }
                            catch (Exception ex) { }
                        }
                        refreshgrid();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('Update Successfully')", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('Please try again')", true);
                        return;
                    }
                }
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('Sorry you are not allow to open or close child package')", true);
            return;
        }
    }
}