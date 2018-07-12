using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using System;
using System.Data;
using System.Globalization;

using System.Data.SqlClient;
using System.Configuration;
public partial class Cruise_PackageSearchResults : System.Web.UI.Page
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
  
    public string CheckinDep;
    public string CheckoutDep;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CustName"] != null)
        {
            lblUsername.Text = "Hello " + Session["CustName"].ToString();
        }
        if (Session["UserName"] != null)

        {
            lblUsername.Text = "Hello " + Session["UserName"].ToString();
            
        }
        if (!IsPostBack)
        {
            if (Session["UserCode"] != null || Session["CustomerCode"] != null)
            {
                LinkButton1.Visible = true;
            }
            else
            {
                LinkButton1.Visible = false;
            }
            if (Request.QueryString["Package"] != null)
            {
                package = Convert.ToString(Request.QueryString["Package"]);
            }
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
            string[] arr = date.Split(' ');
            blsrch.action = "getbyfilter";
            if (date != "0")
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
            //dtres = dlsrch.getbyfilter(Convert.ToDateTime(CheckinDep), Convert.ToDateTime(CheckoutDep), cid);
        }
        catch
        {
        }
    }
    
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        System.Web.Security.FormsAuthentication.SignOut();
        Response.Redirect("SearchProperty.aspx");
    }
}