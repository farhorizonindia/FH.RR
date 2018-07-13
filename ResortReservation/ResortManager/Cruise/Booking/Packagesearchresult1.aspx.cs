using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using System;
using System.Data;
using System.Globalization;

using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web;

<<<<<<< HEAD
public partial class Cruise_Booking_Packageserachresult1 : BaseClass
=======
public partial class Cruise_Booking_Packageserachresult1 : System.Web.UI.Page
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
{
    int cityid = 0;
    string date = "";
    string package = "";
    string month = "";
    string year = "";
    int Riverid = 0;
    public string PackageId = string.Empty;
    public DataTable dtres;
    BALSearch blsrch = new BALSearch();
    DALSearch dlsrch = new DALSearch();
    DataTable distinctValues;
    public string CheckinDep;
    public string CheckoutDep;
    protected void Page_Load(object sender, EventArgs e)
    {
<<<<<<< HEAD
       
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        Session["getavailable"] = url;
        //if (Session["check"] == null)
        //{
        //    Response.Redirect("searchproperty1.aspx");
        //}
=======
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        Session["getavailable"] = url;
        if (Session["check"] == null)
        {
            Response.Redirect("searchproperty1.aspx");
        }
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
        if (Session["CustName"] != null)
        {
            lblUsername.Text = "Hello " + Session["CustName"].ToString();

            navlogin.Visible = false;
            LinkButton1.Visible = true;
        }
        else if (Session["UserName"] != null)

        {
            lblUsername.Text = "Hello " + Session["UserName"].ToString();
            //lnkCustomerRegis.Visible = false;
            navlogin.Visible = false;
            LinkButton1.Visible = true;
        }
        else
        {

            navlogin.Visible = true;
            LinkButton1.Visible = false;
        }

        if (!IsPostBack)
        {
<<<<<<< HEAD
         

=======
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
            if (Session["UserCode"] != null || Session["CustomerCode"] != null)
            {
                LinkButton1.Visible = true;
                lnkLogin.Visible = false;
                //lnkView.Visible = true;
                lblLoginas.Visible = false;
                //lnkCustLogin.Visible = false;
            }
            else
            {
                //LinkButton1.Visible = false;
                lnkLogin.Visible = true;
                //lnkView.Visible = false;
                lblLoginas.Visible = true;
                //lnkCustLogin.Visible = true;
            }
            if (Request.QueryString["Package"] != null)
            {
                package = Convert.ToString(Request.QueryString["Package"]);

            }
            //if (Session["getpaxckagename"] != null)
            //{
            //    lblPackagename.Text = Session["getpaxckagename"].ToString();
            //}
            //int.TryParse(Convert.ToString(Request.QueryString["CId"]), out cityid);
            //date = Convert.ToString(Request.QueryString["date"]);

            if (Request.QueryString["Year"] != null)
            {
                year = Convert.ToString(Request.QueryString["Year"]);
            }
            if (Request.QueryString["month"] != null)
            {
                month = Convert.ToString(Request.QueryString["month"]);
            }
            //int.TryParse(Convert.ToString(Request.QueryString["RId"]), out Riverid);
            //getpackagesearchresults(cityid, date, Riverid);
            getbyfilter(package, month + " " + year);
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        System.Web.Security.FormsAuthentication.SignOut();
        LinkButton1.Visible = false;
        Response.Redirect("searchproperty1.aspx");
    }
    public void getpackagesearchresults(int cid, string date, int rid)
    {
        try
        {
            string[] arr = date.Split(' ');
            blsrch.action = "getpackages";
            if (date != "0")
            {
                string mnth = arr[0].Substring(0, 3);
                int mdigit = DateTime.ParseExact(mnth, "MMM", CultureInfo.InvariantCulture).Month;
                var firstDayOfMonth = new DateTime(Convert.ToInt32(arr[1]), mdigit, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                CheckinDep = firstDayOfMonth.ToString();
                CheckoutDep = lastDayOfMonth.ToString();

                blsrch.StartDate = firstDayOfMonth;
                blsrch.EndDate = lastDayOfMonth;
            }
            else
            {
                blsrch.StartDate = Convert.ToDateTime("1900-01-01");
                blsrch.EndDate = Convert.ToDateTime("1900-01-01");
            }

            blsrch.CountryId = cid;
            if (rid != 0)
            {
                blsrch.RiverId = rid;
            }
            dtres = dlsrch.GetSearchResultsPackages(blsrch);
        }
        catch
        {
        }
    }
    //public void getpackage(int cid, string date)
    //{
    //    try
    //    {
    //        string[] arr = date.Split(' ');
    //        blsrch.action = "packagesbyfilter";
    //        if (date != "0")
    //        {
    //            string mnth = arr[0].Substring(0, 3);
    //            int mdigit = DateTime.ParseExact(mnth, "MMM", CultureInfo.InvariantCulture).Month;
    //            var firstDayOfMonth = new DateTime(Convert.ToInt32(arr[1]), mdigit, 1);
    //            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

    //            CheckinDep = firstDayOfMonth.ToString();
    //            CheckoutDep = lastDayOfMonth.ToString();

    //            blsrch.StartDate = firstDayOfMonth;
    //            blsrch.EndDate = lastDayOfMonth;
    //        }
    //        else
    //        {
    //            blsrch.StartDate = Convert.ToDateTime("1900-01-01");
    //            blsrch.EndDate = Convert.ToDateTime("1900-01-01");
    //        }

    //        blsrch.CountryId = cid;
    //        //if (rid != 0)
    //        //{
    //        //    blsrch.RiverId = rid;
    //        //}
    //        dtres = dlsrch.GetPackagesbyfilter(blsrch);
    //    }
    //    catch
    //    {
    //    }
    //}
    public void getbyfilter(string cid, string date)
    {
        try
        {
            int mdigit = 0;
            var lastDayOfMonth = new DateTime();
            var firstDayOfMonth = new DateTime();
            string[] arr = date.Split(' ');
            blsrch.action = "getbyfilter";
            if (date != "Select Month Select Year")
            {
                try
                {
                    string mnth = arr[0].Substring(0, 3);
                    try
                    {
                        mdigit = DateTime.ParseExact(mnth, "MMM", CultureInfo.InvariantCulture).Month;
                    }
                    catch
                    {
                        mdigit = DateTime.ParseExact("Jan", "MMM", CultureInfo.InvariantCulture).Month;
                    }
                    try
                    {
                        firstDayOfMonth = new DateTime(Convert.ToInt32(arr[1]), mdigit, 1);
                        lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                    }
                    catch
                    {
                        firstDayOfMonth = new DateTime(Convert.ToInt32(arr[2]), mdigit, 1);
                        lastDayOfMonth = firstDayOfMonth.AddMonths(13 - mdigit).AddDays(+1);
                    }



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
            if (cid == "All Packages")
            {
                blsrch.PackageId = null;
            }
            else
            {
                blsrch.PackageId = cid;
            }
            
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


}