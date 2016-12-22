using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class Cruise_booking_SearchBasedPackage : System.Web.UI.Page
{
    int cityid = 0;
    string date = "";
    int Riverid = 0;

    public string PackageId = string.Empty;
    public DataTable dtres;
    BALSearch blsrch = new BALSearch();
    DALSearch dlsrch = new DALSearch();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
            if (Session["Usercode"] != null)
            {
                blsrch.action = "GetResultBasedOnPackage";
                blsrch.PackageId = Request.QueryString["PackId"].ToString();
                dtres = dlsrch.GetResultBasedOnPackage(blsrch);
            }
            else
            {
                Response.Redirect("agentLogin.aspx");
            }

        }
        catch
        {

        }
    }
}