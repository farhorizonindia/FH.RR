using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class Cruise_PackageSearchResults : System.Web.UI.Page
{
    int cityid = 0;
    string date = "";
    int Riverid = 0;
    public string PackageId = string.Empty;
    public DataTable dtres;
    BALSearch blsrch = new BALSearch();
    DALSearch dlsrch = new DALSearch();

    public string CheckinDep;
    public string CheckoutDep;
    protected void Page_Load(object sender, EventArgs e)
    {
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
           
                int.TryParse(Convert.ToString(Request.QueryString["CId"]), out cityid);
                date = Convert.ToString(Request.QueryString["date"]);
                int.TryParse(Convert.ToString(Request.QueryString["RId"]), out Riverid);
                getpackagesearchresults(cityid, date, Riverid);
          
           
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
                CheckoutDep =lastDayOfMonth.ToString();

                blsrch.StartDate = firstDayOfMonth;
                blsrch.EndDate = lastDayOfMonth;
            }
            else
            {
                    blsrch.StartDate =Convert.ToDateTime( "1900-01-01");
                blsrch.EndDate =Convert.ToDateTime( "1900-01-01");
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




    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        System.Web.Security.FormsAuthentication.SignOut();
        Response.Redirect("SearchProperty.aspx");
    }
}