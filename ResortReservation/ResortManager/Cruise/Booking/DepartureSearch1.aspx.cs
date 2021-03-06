﻿

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
<<<<<<< HEAD
using System.Net;
using Newtonsoft.Json.Linq;


public partial class Cruise_Booking_DepartureSearch1 : BaseClass
=======

public partial class Cruise_Booking_DepartureSearch1 : System.Web.UI.Page
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
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
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        Session["getavailable"] = url;

<<<<<<< HEAD
        //if (Session["check"] == null)
        //{
        //    Response.Redirect("searchproperty.aspx");
        //}
=======
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

        Session["totpax"] = null;
        Session["PackageId"] = null;

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


<<<<<<< HEAD
            if (Request["agentid"] != null)
            {
                logoheading.HRef = "searchproperty.aspx?agentid=" + Request["agentid"].ToString() + "";
            }
            else
            {
                logoheading.HRef = "searchproperty.aspx";
            }

            try
            {
                if (Session["SetCurrency"].ToString() != "")
                {
                    ddlCurrency.Text = Session["SetCurrency"].ToString();
                    if (ddlCurrency.Text != "USD")
                    {

                        ddlCurrency_SelectedIndexChanged(this, e);
                    }
                }
            }catch { }
=======

>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955

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
                lblFrmTo.Text = dtPackDesc.Rows[0]["BordingFrom"].ToString() + " to " + dtPackDesc.Rows[0]["BoadingTo"].ToString();
                lblnights.Text = dtPackDesc.Rows[0]["NoOfNights"].ToString() + " Nights/" + (Convert.ToInt32(dtPackDesc.Rows[0]["NoOfNights"]) + 1).ToString() + " Days";
            }
        }
        catch
        {
        }
    }
    public static string TimeAgo(DateTime dt)
    {
        TimeSpan span = DateTime.Now - dt;
        if (span.Days > 365)
        {
            int years = (span.Days / 365);
            if (span.Days % 365 != 0)
                years += 1;
            return String.Format("about {0} {1} ago",
            years, years == 1 ? "year" : "years");
        }
        if (span.Days > 30)
        {
            int months = (span.Days / 30);
            if (span.Days % 31 != 0)
                months += 1;
            return String.Format("about {0} {1} ago",
            months, months == 1 ? "month" : "months");
        }
        if (span.Days > 0)
            return String.Format("about {0} {1} ago",
            span.Days, span.Days == 1 ? "day" : "days");
        if (span.Hours > 0)
            return String.Format("about {0} {1} ago",
            span.Hours, span.Hours == 1 ? "hour" : "hours");
        if (span.Minutes > 0)
            return String.Format("about {0} {1} ago",
            span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
        if (span.Seconds > 5)
            return String.Format("about {0} seconds ago", span.Seconds);
        if (span.Seconds <= 5)
            return "just now";
        return string.Empty;
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
    public void getpackagesearchresults(int cid, string date, int rid)
    {
        lblSuite.Visible = false;
        lblSwb.Visible = false;
        lblSwob.Visible = false;
        #region GetSearch data
        try
        {
            blsrch.action = "GetOpenDatesCruise";
            if (Session["PackId"] == null)
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
            DataTable dt12 = new DataTable();

<<<<<<< HEAD
            //  dtres = dlsrch.GetCruiseOpenDatesPackage(blsrch);
            DataTable dtsorted = new DataTable();
            dtsorted = dlsrch.GetCruiseOpenDatesPackage(blsrch);

            if (dtsorted != null && dtsorted.Rows.Count > 0)
            {
                dtsorted.DefaultView.Sort = "CheckInDate  ASC";
            }
            dtres = dtsorted;
=======
            DataTable dtsorted = new DataTable();
            dtsorted= dlsrch.GetCruiseOpenDatesPackage(blsrch);

           if(dtsorted != null && dtsorted.Rows.Count > 0)
                {
                dtsorted.DefaultView.Sort = "CheckInDate  ASC";
                 }
            dtres = dtsorted;
          //  dtres = dlsrch.GetCruiseOpenDatesPackage(blsrch);
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955

            //DataView dv = new DataView();
            //dv = new DataView(dtres, "Availability = 'Limited Availability' or Availability = 'Available'", "Availability", DataViewRowState.CurrentRows);
            //dtres = dv.ToTable();
            DataView dvFormula = dtres.DefaultView;
            dvFormula.RowFilter = " (CheckInDate >= #" +
         DateTime.Now.ToString("MM/dd/yyyy") + "# )";
            dtres = dvFormula.ToTable();
            if (dtres != null && dtres.Rows.Count > 0)
            {
                dtres.Columns.Add("Discount %", typeof(string));
                dtres.Columns.Add("Suite", typeof(string));
                dtres.Columns.Add("Swb", typeof(string));
                dtres.Columns.Add("Swob", typeof(string));
                dtres.Columns.Add("OpenClose", typeof(string));
                DataRow dr = dtres.NewRow();

<<<<<<< HEAD
                for (int i = 0; i < dtres.Rows.Count; i++)
                {
                    DataTable getdt = null;

                    if (ddlCurrency.Text != "USD")
                    {
                        currency(dtres.Rows[i]["Rate"].ToString(), ddlCurrency.Text);

                         getdt = dlsrch.fetchdiscount(dtres.Rows[i]["packageId"].ToString(),
                    Convert.ToDateTime(dtres.Rows[i]["CheckInDate"].ToString()),
                    Convert.ToDateTime(dtres.Rows[i]["CheckOutDate"].ToString()),
                  Convert.ToDecimal(dtres.Rows[i]["Rate"].ToString()));
                    }
                    else
                    {
                        Session["PricePerPerson"] = null;
                         getdt = dlsrch.fetchdiscount(dtres.Rows[i]["packageId"].ToString(),
                            Convert.ToDateTime(dtres.Rows[i]["CheckInDate"].ToString()),
                            Convert.ToDateTime(dtres.Rows[i]["CheckOutDate"].ToString()),
                            Convert.ToDecimal(dtres.Rows[i]["Rate"].ToString()));
                    }

                    DataTable dt45 = bindroomddl(dtres.Rows[i]["packageId"].ToString(), Convert.ToInt32(dtres.Rows[i]["Id"].ToString()));
=======
                DataTable getdtall = dlsrch.fetchdiscountAll();
                DataTable getallopnendate = dlsrch.GetCruiseOpenDatesPackageSearch(blsrch);

                for (int i = 0; i < dtres.Rows.Count; i++)
                {
                    // DataTable getdt = dlsrch.fetchdiscount(dtres.Rows[i]["packageId"].ToString(), Convert.ToDateTime(dtres.Rows[i]["CheckInDate"].ToString()), Convert.ToDateTime(dtres.Rows[i]["CheckOutDate"].ToString()), Convert.ToDecimal(dtres.Rows[i]["Rate"].ToString()));
                    DataView dvdis = new DataView(getdtall);
                      string st = "Packageid = '" + dtres.Rows[i]["packageId"].ToString() + "' and Boardingdate='" + Convert.ToDateTime(dtres.Rows[i]["CheckInDate"].ToString()) + "' and Deboardingdate='" + Convert.ToDateTime(dtres.Rows[i]["CheckOutDate"].ToString()) + "' and price=" + Convert.ToDecimal(dtres.Rows[i]["Rate"].ToString());
                  //  string st = "Packageid = '" + dtres.Rows[i]["packageId"].ToString() + "',Boardingdate =" + dtres.Rows[i]["CheckInDate"];
                    dvdis.RowFilter = st;
                    DataTable getdt = dvdis.ToTable();

                    DataView dvopendates = new DataView(getallopnendate);
                    string filter = "PackageId='"+ dtres.Rows[i]["packageId"].ToString()+ "' and Depid="+ Convert.ToInt32(dtres.Rows[i]["Id"].ToString());
                    dvopendates.RowFilter = filter;
                    DataTable dt45 = dvopendates.ToTable();

                 //   DataTable dt45 = bindroomddl(dtres.Rows[i]["packageId"].ToString(), Convert.ToInt32(dtres.Rows[i]["Id"].ToString()));
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
                    if (Session["UserCode"] != null)
                    {
                        lblSuite.Visible = true;
                        lblSwb.Visible = true;
                        lblSwob.Visible = true;
                        if (dt45 != null && dt45.Rows.Count > 0)
                        {
                            DataView dvac = new DataView();
                            dvac = new DataView(dt45, "getactivestatus = 'Active'", "getactivestatus", DataViewRowState.CurrentRows);
                            dt45 = dvac.ToTable();
                            DataView dv = new DataView();
                            dv = new DataView(dt45, "BookedStatus = 'Limited Availability' or BookedStatus = 'Available'", "BookedStatus", DataViewRowState.CurrentRows);
                            DataTable dtcruise1 = dv.ToTable();
                            dv = new DataView(dtcruise1, "RoomCategory = 'Suite'", "RoomCategory", DataViewRowState.CurrentRows);
                            DataTable dtcruise = dv.ToTable();
                            if (dtcruise != null && dtcruise.Rows.Count > 0)
                            {
                                dtres.Rows[i]["Suite"] = dtcruise.Rows.Count;
                            }
                            else
                            {
                                dtres.Rows[i]["Suite"] = 0;
                            }
                            DataView dv1 = new DataView();
                            dv1 = new DataView(dt45, "BookedStatus = 'Limited Availability' or BookedStatus = 'Available'", "BookedStatus", DataViewRowState.CurrentRows);
                            DataTable dtcruise11 = dv1.ToTable();
                            dv1 = new DataView(dtcruise1, "RoomCategory = 'Superior with Balcony'", "RoomCategory", DataViewRowState.CurrentRows);
                            DataTable dtcruise111 = dv1.ToTable();
                            if (dtcruise111 != null && dtcruise111.Rows.Count > 0)
                            {
                                dtres.Rows[i]["Swb"] = dtcruise111.Rows.Count;
                            }
                            else
                            {
                                dtres.Rows[i]["Swb"] = 0;
                            }
                            DataView dv2 = new DataView();
                            dv2 = new DataView(dt45, "BookedStatus = 'Limited Availability' or BookedStatus = 'Available'", "BookedStatus", DataViewRowState.CurrentRows);
                            DataTable dtcruise12 = dv2.ToTable();
                            dv2 = new DataView(dtcruise1, "RoomCategory = 'Superior without Balcony'", "RoomCategory", DataViewRowState.CurrentRows);
                            DataTable dtcruise123 = dv2.ToTable();
                            if (dtcruise123 != null && dtcruise123.Rows.Count > 0)
                            {
                                dtres.Rows[i]["Swob"] = dtcruise123.Rows.Count;
                            }
                            else
                            {
                                dtres.Rows[i]["Swob"] = 0;
                            }
                        }
                    }
                    DateTime electionPosDate = Convert.ToDateTime(dtres.Rows[i]["CheckInDate"].ToString());
                    DateTime promotionaldate = electionPosDate.Date.AddTicks(-2);
                    DateTime todaysdate = DateTime.Now.Date.AddTicks(-2);
                    if (promotionaldate == todaysdate)
                    {
                        if (getdt != null && getdt.Rows.Count > 0)
                        {
                            dtres.Rows[i]["Discount %"] = getdt.Rows[0]["LastMinutessale"].ToString().Split('.')[0];
                            //lblDiscount.Text = "Last Minutes Sale";
                            //lblPercent.Text = "% Off";
                        }
                        else
                        {
                            dtres.Rows[i]["Discount %"] = "0";
                            //dtres.Rows[i]["Discount %"] = getdt.Rows[0]["LastMinutessale"].ToString().Split('.')[0];
                            ////lblDiscount.Text = "Last Minutes Sale";
                            //lblPercent.Text = "% Off";
                        }



                    }
                    else
                    {

                        if (getdt != null && getdt.Rows.Count > 0)
                        {
                            dtres.Rows[i]["Discount %"] = getdt.Rows[0]["Discountamount"].ToString().Split('.')[0];
                            //lblDiscount.Text = "Discount On This Package";
                            //lblPercent.Text = "% Off";
                        }
                        else
                        {
                            dtres.Rows[i]["Discount %"] = "0";
                        }


                    }

                    if (getdt != null && getdt.Rows.Count > 0)
                    {
                        dtres.Rows[i]["openclose"] = getdt.Rows[0]["openclose"].ToString();
                        //if (getdt.Rows[0]["openclose"].ToString() == "Close")
                        //{
                        //    dtres.Rows[i].Delete();
                        //    dtres.AcceptChanges();
                        //}
                    }
                    else
                    {
                        dtres.Rows[i]["openclose"] = "Open";
                    }
                    //DateTime now = Convert.ToDateTime(dtres.Rows[i]["CheckInDate"].ToString());

                    //DateTime endOfDay = now.AddMinutes(1);
                    //DateTime now1 = Convert.ToDateTime(DateTime.Now);
                    //DateTime startOfDay1 = now1.Date;
                    //DateTime endOfDay1 = startOfDay1.AddMinutes(1);

                    //DataRow dr = getdt.Rows[i];
                    //dr[3].Value = "New Value";


                    //DataView dv67 = new DataView();
                    //dv67 = new DataView(dtres, "openclose = 'Open'", "openclose", DataViewRowState.CurrentRows);
                    //dtres = dv67.ToTable();
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

            else
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "<script> alert('There Is No Rate For This Package');window.open('CruiseBooking.aspx');</script>", true);
                //Response.Redirect("<a href='CruiseBooking.aspx?PackId=" + Session["PackId"].ToString() + "&PackageName=0&NoOfNights=0&CheckIndate=0&CheckOutdate=0&Discount=0&DepartureId=0'  class='btn btn-info font16 topMargin10 botMargin10 step2Btn' style='padding-left:12px;padding-right:12px;' data-departureid='5597' >Select</a>");
<<<<<<< HEAD
                Response.Redirect("searchproperty.aspx");
=======
                Response.Redirect("searchproperty1.aspx");
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
            }
        }
        catch (Exception ex)
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
        LinkButton1.Visible = false;
<<<<<<< HEAD

        if (Session["agentid"] != null)
        {
            Response.Redirect("Packageserachresult.aspx?agentid=" + Session["agentid"].ToString() + "");
        }
        else
        {
            Response.Redirect("Packageserachresult.aspx");
        }
        //Response.Redirect("Packagesearchresult.aspx");
=======
        Response.Redirect("Packageserachresult1.aspx");
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
    }







    protected void Button1_Click1(object sender, EventArgs e)
    {

    }
<<<<<<< HEAD

    private string currency(string amount, string code)
    {
        var countryname = "";
        try
        {
            if (Session["Dollar"].ToString() == "1")
            {

                WebClient obj = new WebClient();
                var rsponse = obj.DownloadString("http://www.apilayer.net/api/live?access_key=1f011c76b84ee0d412dd6611c0545986&format=1&source=USD");
                dynamic data = JObject.Parse(rsponse);
                var aa = data.quotes;
                foreach (var r in aa)
                {
                    var sd = "USD" + code;
                    if (r.Name.Trim().ToLower() == sd.Trim().ToLower())
                    {
                        var m = r.Name;
                        var n = r.Value;
                        decimal amounts = Convert.ToDecimal(amount);
                        decimal dollar = Convert.ToDecimal(r.Value);
                        Session["Dollar"] = dollar;
                        var am = amounts / dollar;
                        string value = am.ToString("#.00");
                        Session["PricePerPerson"] = value;
                        break;
                    }
                    else
                    {
                        if (r.Name.Trim().ToLower() == "usdusd")
                        {
                            var am = String.Format("{0:0.00}", r.Value * amount);
                            return am;
                        }
                    }
                }
            }
            else
            {
                decimal dollar = Convert.ToDecimal(Session["Dollar"]);

                decimal amounts = Convert.ToDecimal(amount);

                var am = amounts / dollar;
                Session["Dollar"] = dollar;
                string value = am.ToString("#.00");

                Session["PricePerPerson"] = value;

            }

        }
        catch { }
        return countryname;
    }

    protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        //modalSending.Visible = true;
        //System.Threading.Thread.Sleep(3000);//moving system to sleep to enable loader
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader",
        //   "StopProgress();alert('loader removed from code-behind');", true);
        //Thread.Sleep(5000);
        //if (ddlCurrency.Text != "USD")
        //{
        if (ddlCurrency.Text != "USD")
        {
            Session["SetCurrency"] = "INR";

        }
        else
        {
            Session["SetCurrency"] = "USD";
        }




        Session["DepartureSearchUrl"] = Request.Url.ToString();
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
        //}
    }
=======
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
}