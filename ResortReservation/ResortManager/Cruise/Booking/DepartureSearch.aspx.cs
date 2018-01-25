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

public partial class Cruise_booking_DepartureSearch : System.Web.UI.Page
{

    int cityid = 0;
    string date = "";
    int Riverid = 0;
    public string PackageId = string.Empty;
    public DataTable dtres;
    BALSearch blsrch = new BALSearch();
    DALSearch dlsrch = new DALSearch();
    public string PackageName = string.Empty;
    public string NoOfNight = string.Empty;
    public string CheckinDate = string.Empty;
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
        Session["totpax"] = null;
        Session["PackageId"] = null;
        Session["checkin"] = null;
        Session["BookedRooms"] = null;
        Session["Redirecturl"] = null;
        Session["Rrate"] = null;
        Session["DepartureId"] = null;
        Session["PackId"] = null;
        Session["CheckinDep"] = null;
        Session["CheckoutDep"] = null;


        if (!IsPostBack)
        {

            Session["DepartureSearchUrl"] = Request.Url.ToString();
            if (Session["UserCode"] != null|| Session["CustomerCode"]!=null)
            {
                LinkButton1.Visible = true;
            }
            else
            {
                LinkButton1.Visible = false;
            }

            Session["Cid"] = Request.QueryString["CId"];
            Session["RId"] = Request.QueryString["RId"];
            Session["dDate"] = Request.QueryString["date"];

            int.TryParse(Convert.ToString(Session["Cid"]), out cityid);
            date = Convert.ToString(Session["dDate"]);
            int.TryParse(Convert.ToString(Session["RId"]), out Riverid);
            if (Session["PackId"] == null)
            {
                Session["PackId"] = Request.QueryString["PackId"];
            }

            PackageDesc(Session["PackId"].ToString());
            


            getpackagesearchresults(cityid, date, Riverid);




        }
    }

    public void PackageDesc(string PackId)
    {
        try
        {
            blsrch.action = "getPackagebyPackId";
            blsrch.PackageId = PackId;
            DataTable dtPackDesc = dlsrch.getPackageDescription(blsrch);
            if (dtPackDesc != null)
            {
                lblPackDesc.Text = dtPackDesc.Rows[0]["PackageDescription"].ToString();
                lblFrmTo.Text = dtPackDesc.Rows[0]["BordingFrom"].ToString() + " to " + dtPackDesc.Rows[0]["BoadingTo"].ToString() ;
                lblnights.Text = dtPackDesc.Rows[0]["NoOfNights"].ToString() + " Nights/" +(Convert.ToInt32( dtPackDesc.Rows[0]["NoOfNights"])+1).ToString()+" Days";
            }
        }
        catch
        {
        }
    }

    public void getpackagesearchresults(int cid, string date, int rid)
    {
        #region GetSearch data
        try
        {
            blsrch.action = "GetOpenDatesCruise";
            if (Session["PackId"]== null)
            {
                Session["PackId"] = Request.QueryString["PackId"];
            }

            blsrch.PackageId = Session["PackId"].ToString();

            if (Request.QueryString["CheckinDep"] != "" && Request.QueryString["CheckoutDep"] != "")
            {
                Session["CheckinDep"] = Request.QueryString["CheckinDep"];
                Session["CheckoutDep"] = Request.QueryString["CheckoutDep"];


                blsrch.StartDate = Convert.ToDateTime(Session["CheckinDep"]);
                blsrch.EndDate = Convert.ToDateTime(Session["CheckoutDep"]);
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

            // dtres = dlsrch.GetCruiseOpenDatesPackage(blsrch);
            DataTable dtsorted = new DataTable();
            dtsorted = dlsrch.GetCruiseOpenDatesPackage(blsrch);

            if (dtsorted != null && dtsorted.Rows.Count > 0)
            {
                dtsorted.DefaultView.Sort = "CheckInDate  ASC";
            }
            dtres = dtsorted;

        }
        catch
        {

        }
        #endregion
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