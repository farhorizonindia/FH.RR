using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using System.Globalization;

public partial class Cruise_Booking_searchproperty1 : System.Web.UI.Page
{
    DALOpenDates dlOpenDates = new DALOpenDates();
    BALOpenDates blOpenDates = new BALOpenDates();
    BALRateCard blCard = new BALRateCard();
    DALRateCard dlcard = new DALRateCard();
    DALSearch dlsearch = new DALSearch();
    DataTable dtGetReturnedData;
    DALAgentPayment dlagent = new DALAgentPayment();
    int getQueryResponse = 0;
    string strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        loadmonth();
        loadall();
        Session["check"] = 1;
        Session["getcheck"] = null;
        Session["GetroomType123"] = null;
        Session["GetroomType12"] = null;
        Session["Getrowindex"] = null;
        Session["Getrowindex0"] = null;
        Session["GetroomType0"] = null;
        Session["Getrowindex1"] = null;
        Session["GetroomType1"] = null;
        Session["Getrowindex2"] = null;
        Session["GetroomType2"] = null;
        Session["Getrowindex3"] = null;
        Session["GetroomType3"] = null;
        Session["Getrowindex4"] = null;
        Session["GetroomType4"] = null;
        Session["getrates"] = null;
        Session["GetroomType1"] = null;
        Session["GetroomType"] = null;
        Session["Getcateid"] = null;
        Session["Getcateid1"] = null;
        Session["getbedconfig"] = null;
        Session["get"] = null;
        loadmonths();
        if (Session["CustName"] != null)
        {
            lblUsername.Text = "Hello " + Session["CustName"].ToString();
            //lnkCustomerRegis.Visible = false;
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
            //lnkCustomerRegis.Visible = false;
            navlogin.Visible = true;
            LinkButton1.Visible = false;
        }
        //if (ddlAccomType.SelectedIndex > 0)
        //{
        //    othr.Visible = true;
        //}
        if (!IsPostBack)
        {

            Session["get"] = null;
            //string s = "";
            //if (Session["get"] != null)
            //{
            //    s = Session["get"].ToString();
            //}
            //string path = Server.MapPath("~/images/aspnet_imagemap" + s + ".png");
            //FileInfo file = new FileInfo(path);
            //if (file.Exists)
            //{
            //    file.Delete();
            //}
            try
            {
                Session["foraddroom"] = null;
                Session["dvclass"] = null;
                Session["GetroomType"] = null;
                Session["Getcateid"] = null;
                ViewState["VsRoomDetails"] = null;
                datepicker1.Attributes.Add("onchange", "return fillEndDate('" + datepicker1.ClientID + "','" + datepicker1.ClientID + "');");
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
                BindAccomNames1();
                LoadCountries();
                fetchpackage();
                loadyear();

                if (ddlDestination.Items.Count > 1)
                {
                    ListItem li = ddlDestination.Items.FindByText("India");
                    if (li != null)
                    {
                        ddlDestination.SelectedValue = li.Value;
                    }
                }
                BindRiverMonths();


                this.BindAccomType();
                //rbtnSelectAccomtype.SelectedIndex = 0;
                if (Request.QueryString["Prop"] != null)
                {
                    AutofillSearch(Request.QueryString["Prop"].ToString());

                }
                else
                {
                    //rbtnSelectAccomtype.SelectedIndex = 0;
                    //ToggleDisplay("Cruise");
                }
                DataTable dt = dlsearch.getavaialablemonth();
                ddlDates.Items.Clear();
                if (ddlYear.SelectedValue != "")
                {
                    string year = DateTime.Now.Year.ToString();
                    if (year == ddlYear.SelectedItem.ToString())
                    {

                        int month = System.DateTime.Now.Month;
                        ddlDates.Items.Insert(0, new ListItem("Select Month", "0"));
                        var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
                        string getmonth = DateTime.Now.ToString("MMMM");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            if (Convert.ToInt32(dt.Rows[i]["monthid"].ToString()) >= month)
                            {



                                ddlDates.Items.Add(new System.Web.UI.WebControls.ListItem(dt.Rows[i]["name"].ToString(), dt.Rows[i]["monthid"].ToString()));
                            }
                            else if (Session["getrecntmonth"] != null)
                            {
                                ddlDates.Items.Add(new System.Web.UI.WebControls.ListItem(DateTimeFormatInfo.CurrentInfo.GetMonthName(i + 1), i + 1.ToString()));
                            }
                            //ddlMonth.Items.Add(i.ToString());
                        }
                        Session["getrecntmonth"] = null;
                    }
                    else
                    {
                        ddlDates.Items.Insert(0, new ListItem("Select Month", "0"));
                        var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
                        string getmonth = DateTime.Now.ToString("MMMM");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {



                            ddlDates.Items.Add(new System.Web.UI.WebControls.ListItem(dt.Rows[i]["name"].ToString(), dt.Rows[i]["monthid"].ToString()));


                            //ddlMonth.Items.Add(i.ToString());
                        }
                    }
                }
            }
            catch
            {
            }

        }
        //  ScriptManager.RegisterStartupScript(this, this.GetType(), "setDate", "setDate()", true);
        if (Request.QueryString["ID"] == "01")
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Your final payment against this booking has been received. There is no outstanding.')", true);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "setDate", "setDate()", true);
        }
    }
    private void loadall()
    {
        DataTable dt = dlagent.selectforbanner();
        if (dt != null && dt.Rows.Count > 0)
        {
            rpt1.DataSource = dt;
            rpt1.DataBind();
        }
    }
    public void AutofillSearch(string Prop)
    {
        try
        {
            if (Request.QueryString["Prop"] == "ddune")
            {
                //rbtnSelectAccomtype.SelectedIndex = 1;
                //ToggleDisplay("XYZ");
                //ddlAccomType.SelectedValue = "2";
                BindAccomNames(2);
                ddlAccomodationName.SelectedValue = "1";


            }
            else if (Request.QueryString["Prop"] == "pcamps")
            {
                //rbtnSelectAccomtype.SelectedIndex = 1;
                //ToggleDisplay("XYZ");
                //ddlAccomType.SelectedValue = "5";
                BindAccomNames(5);
                ddlAccomodationName.SelectedValue = "10";
            }
            else if (Request.QueryString["Prop"] == "fhcamp")
            {
                //rbtnSelectAccomtype.SelectedIndex = 1;
                //ToggleDisplay("XYZ");
                //ddlAccomType.SelectedValue = "5";
                BindAccomNames(5);
                ddlAccomodationName.SelectedValue = "16";
            }
            else if (Request.QueryString["Prop"] == "ncamps")
            {
                //rbtnSelectAccomtype.SelectedIndex = 1;
                //ToggleDisplay("XYZ");
                //ddlAccomType.SelectedValue = "5";
                BindAccomNames(5);
                ddlAccomodationName.SelectedValue = "17";
            }
            else if (Request.QueryString["Prop"] == "boatvk")
            {
                //rbtnSelectAccomtype.SelectedIndex = 1;
                //ToggleDisplay("XYZ");
                //ddlAccomType.SelectedValue = "3";
                BindAccomNames(3);
                ddlAccomodationName.SelectedValue = "3";
            }
            else if (Request.QueryString["Prop"] == "boatsv")
            {
                //rbtnSelectAccomtype.SelectedIndex = 1;
                //ToggleDisplay("XYZ");
                //ddlAccomType.SelectedValue = "3";
                BindAccomNames(3);
                ddlAccomodationName.SelectedValue = "4";
            }
            else if (Request.QueryString["Prop"] == "rtkalakho")
            {
                //rbtnSelectAccomtype.SelectedIndex = 1;
                //ToggleDisplay("XYZ");
                //ddlAccomType.SelectedValue = "11";
                BindAccomNames(11);

                ddlAccomodationName.SelectedValue = "6";
            }
            else
            {
                //rbtnSelectAccomtype.SelectedIndex = 0;
                //ToggleDisplay("Cruise");
            }
        }
        catch
        { }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlYear.SelectedValue != "Select Year")

        {
            //if (ddlPackege.SelectedIndex > 0)
            //{
            Session["getpaxckagename"] = ddlPackege.SelectedItem.ToString();

            //ddlPackege.Enabled = false;
            //ddlDates.Enabled = false;
            //ddlYear.Enabled = false;
            Response.Redirect("Packagesearchresult1.aspx?Package=" + ddlPackege.SelectedValue + "&Year=" + ddlYear.SelectedValue + "&month=" + ddlDates.SelectedItem);
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please select any of package')", true);
            //    return;
            //}
        }
        else
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Year')", true);
        }
    }

    private void LoadCountries()
    {
        try
        {
            blOpenDates._Action = "GetCountry";
            dtGetReturnedData = dlOpenDates.BindControls(blOpenDates);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlDestination.DataSource = dtGetReturnedData;
                ddlDestination.DataTextField = "CountryName";
                ddlDestination.DataValueField = "CountryId";
                ddlDestination.DataBind();
                ddlDestination.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlDestination.Items.Clear();
                ddlDestination.DataSource = null;
                ddlDestination.DataBind();
                ddlDestination.Items.Insert(0, "-No Destination");
            }
        }
        catch (Exception sqe)
        {
            ddlDestination.Items.Clear();
            ddlDestination.DataSource = null;
            ddlDestination.DataBind();
            ddlDestination.Items.Insert(0, "-No Destination-");

        }
    }
    private void loadmonth()
    {
        DataTable dt = dlsearch.getavaialablemonth();
        if (dt != null && dt.Rows.Count > 0)
        {
            //ddlDates.DataSource = dt;
            //ddlDates.DataTextField = "Name";
            //ddlDates.DataValueField = "Name";
            //ddlDates.DataBind();
            //ddlDates.Items.Insert(0, "Select Month");

        }
    }
    #region getpackage
    public void fetchpackage()
    {
        DataTable dt = dlsearch.fetchall();
        if (dt != null & dt.Rows.Count > 0)
        {
            ddlPackege.Items.Insert(0, "-Select Package-");
            ddlPackege.DataSource = dt;
            ddlPackege.DataTextField = "PackageName";
            ddlPackege.DataValueField = "PackageId";
            ddlPackege.DataBind();
            ddlPackege.Items.Insert(0, "All Packages");
        }
    }
    private void loadyear()
    {
        ddlYear.Items.Clear();
        //ListItem[] itens = new ListItem[4] {
        ListItem[] itens = new ListItem[4] {
   // new ListItem("Select Year"),
    new ListItem(DateTime.Today.Year.ToString()),
    new ListItem(DateTime.Today.AddYears(+1).Year.ToString()),
    new ListItem(DateTime.Today.AddYears(+2).Year.ToString()),
    new ListItem(DateTime.Today.AddYears(+3).Year.ToString())};

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
    #endregion
    public void BindRiverMonths()
    {
        #region Bind RiverDD
        try
        {
            blOpenDates._Action = "GetRiver";
            blOpenDates._CountryId = Convert.ToInt32(ddlDestination.SelectedItem.Value);
            dtGetReturnedData = dlOpenDates.GetRiverLocation(blOpenDates);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlRiver.DataSource = dtGetReturnedData;
                ddlRiver.DataTextField = "RiverName";
                ddlRiver.DataValueField = "RiverId";
                ddlRiver.DataBind();
                ddlRiver.Items.Insert(0, "-River-");
            }
            else
            {
                ddlRiver.Items.Clear();
                ddlRiver.DataSource = null;
                ddlRiver.DataBind();
                ddlRiver.Items.Insert(0, "-No River-");
            }
        }
        catch (Exception sqe)
        {
            ddlRiver.Items.Clear();
            ddlRiver.DataSource = null;
            ddlRiver.DataBind();
            ddlRiver.Items.Insert(0, "-No River-");

        }
        #endregion

        #region bindmonths

        //try
        //{
        //    blOpenDates._Action = "getseasonmonths";
        //    if (ddlDestination.SelectedIndex > 0)
        //    {
        //        blOpenDates._CountryId = Convert.ToInt32(ddlDestination.SelectedValue);
        //    }
        //    else
        //    {

        //        blOpenDates._CountryId = 0;
        //    }

        //    dtGetReturnedData = dlOpenDates.getMonthsforddl(blOpenDates);
        //    if (dtGetReturnedData != null && dtGetReturnedData.Rows.Count > 0)
        //    {
        //        ddlDates.DataSource = dtGetReturnedData;
        //        ddlDates.DataTextField = "MonthYYYY";
        //        ddlDates.DataValueField = "MonthYYYY";
        //        ddlDates.DataBind();
        //        ddlDates.Items.Insert(0, new ListItem("-Month-", "0"));
        //    }

        //    else
        //    {
        //        ddlDates.Items.Clear();
        //        ddlDates.DataSource = null;
        //        ddlDates.DataBind();
        //        ddlDates.Items.Insert(0, "-No Dates-");
        //    }
        //}
        //catch
        //{

        //    ddlDates.Items.Clear();
        //    ddlDates.DataSource = null;
        //    ddlDates.DataBind();
        //    ddlDates.Items.Insert(0, "-No Dates-");
        //}

        #endregion
    }
    private void loadmonths()
    {
        try
        {
            DataTable dt = dlsearch.getavaialablemonth();
            int month = System.DateTime.Now.Month;
            ddlDates.Items.Insert(0, new ListItem("Select Month", "0"));
            var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            string getmonth = DateTime.Now.ToString("MMMM");
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (Convert.ToInt32(dt.Rows[i]["monthid"].ToString()) >= month)
                {



                    ddlDates.Items.Add(new System.Web.UI.WebControls.ListItem(dt.Rows[i]["name"].ToString(), dt.Rows[i]["monthid"].ToString()));
                }
                else if (Session["getrecntmonth"] != null)
                {
                    ddlDates.Items.Add(new System.Web.UI.WebControls.ListItem(DateTimeFormatInfo.CurrentInfo.GetMonthName(i + 1), i + 1.ToString()));
                }
                //ddlMonth.Items.Add(i.ToString());
            }
            Session["getrecntmonth"] = null;
        }
        catch
        { }
    }
    protected void ddlDestination_SelectedIndexChanged(object sender, EventArgs e)
    {



    }
    //protected void rbtnSelectAccomtype_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ToggleDisplay(rbtnSelectAccomtype.SelectedItem.Text);
    //}

    //public void ToggleDisplay(string str)
    //{
    //    try
    //    {
    //        if (str == "Cruise")
    //        {
    //            divCruise.Style.Remove("display");
    //            OtherAccoms.Style.Add("display", "none");

    //        }
    //        else
    //        {
    //            OtherAccoms.Style.Remove("display");
    //            divCruise.Style.Add("display", "none");
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "setDate", "setDate()", true);
    //        }

    //    }

    //    catch
    //    {

    //    }
    //}

    public int totalguests(out int[] arr1)
    {
        try
        {
            int count = 0;
            int[] arr = new int[10];
            for (int l = 0; l < gdvRooms.Rows.Count; l++)
            {
                DropDownList ddlguest = (DropDownList)gdvRooms.Rows[l].FindControl("ddlGuests");

                arr[l] = Convert.ToInt32(ddlguest.SelectedValue);

                count = count + Convert.ToInt32(ddlguest.SelectedValue);

            }
            arr1 = arr;
            return count;
        }

        catch
        {
            arr1 = null;
            return 0;
        }
    }

    protected void btnSearchOthAccom_Click(object sender, EventArgs e)
    {
        if (ddlAccomodationName.SelectedIndex > 0)
        {
            try
            {
                //              DateTime CreatdDate = DateTime.ParseExact(datepicker1.Text,
                //  "dd MM yy",
                //  System.Globalization.CultureInfo.InvariantCulture);
                //              DateTime enddate = DateTime.ParseExact(datepicker2.Text,
                //"dd MM yy",
                //System.Globalization.CultureInfo.InvariantCulture);
                string checkin = datepicker1.Text;
                string checkout = datepicker2.Text;
                Session["getcheckin"] = checkin;
                Session["getcheckout"] = checkout;
                //if (CreatdDate >= System.DateTime.Now.Date)
                //{
                if (Session["UserCode"] != null)
                {
                    string guests = string.Empty;
                    int[] arr = new int[10];
                    int pax = totalguests(out arr);
                    Session["AccomName"] = ddlAccomodationName.SelectedItem.Text;

                    string url = "available.aspx?AId=" + Request.QueryString["Aid"].ToString() + "&AccomId=" + ddlAccomodationName.SelectedValue + "&pax=" + pax.ToString() + "&Checkin=" + checkin + "&Checkout=" + checkout + "&AccomName=" + ddlAccomodationName.SelectedItem.Text + "";
                    for (int k = 0; k < gdvRooms.Rows.Count; k++)
                    {
                        guests = guests + " &guest" + (k + 1).ToString() + "=" + arr[k].ToString();

                    }
                    url = url + guests;
                    Session["Bookingdt"] = null;
                    Response.Redirect(url);
                }
                else
                {
                    string guests = string.Empty;
                    int[] arr = new int[10];
                    int pax = totalguests(out arr);
                    Session["AccomName"] = ddlAccomodationName.SelectedItem.Text;
                    string url = "available.aspx?AId=248&AccomId=" + ddlAccomodationName.SelectedValue + "&pax=" + pax.ToString() + "&Checkin=" + checkin + "&Checkout=" + checkout + "&AccomName=" + ddlAccomodationName.SelectedItem.Text + " ";

                    for (int k = 0; k < gdvRooms.Rows.Count; k++)
                    {
                        guests = guests + " &guest" + (k + 1).ToString() + "=" + arr[k].ToString();

                    }
                    url = url + guests;
                    Session["Bookingdt"] = null;
                    Response.Redirect(url);
                }
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Sorry!Previous Dates Cannot be booked')", true);
                //}
            }

            catch (Exception E)
            {

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Sorry!Please select No of Rooms')", true);
            return;
        }
    }

    private void BindAccomType()
    {
        try
        {
            blCard._Action = "GetAllAcoomTypes";
            dtGetReturnedData = dlcard.BindControls(blCard);

            ///ARC - Dec-18-2016
            #region This is to support the business requirement to remove "Camps" as one of the destination
            var campRow = dtGetReturnedData.Select("AccomTypeId=5").FirstOrDefault();
            if (campRow != null)
            {
                dtGetReturnedData.Rows.Remove(campRow);
            }
            #endregion

            if (dtGetReturnedData.Rows.Count > 0)
            {
                //    ddlAccomType.Items.Clear();
                //    ddlAccomType.DataSource = dtGetReturnedData;
                //    ddlAccomType.DataTextField = "AccomType";
                //    ddlAccomType.DataValueField = "AccomTypeId";
                //    ddlAccomType.DataBind();
                //    ddlAccomType.Items.Insert(0, "-Select-");
            }
            else
            {
                //ddlAccomType.Items.Clear();
                //ddlAccomType.Items.Insert(0, "-No AccomType-");
            }
        }
        catch
        {
        }
    }

    public DataTable GetAccomname(BALRateCard obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_cardMaster]", cn);
            da.SelectCommand.Parameters.Clear();
            da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            cn.Open();
            da.SelectCommand.ExecuteReader();
            DataTable dtReturnData = new DataTable();
            cn.Close();
            da.Fill(dtReturnData);
            if (dtReturnData != null)
                return dtReturnData;
            else
                return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
    public void BindAccomNames(int AccomTypeId)
    {
        try
        {
            othr.Visible = false;
            blCard._Action = "GetAccom";
            blCard._AccomTypeId = AccomTypeId;
            dtGetReturnedData = dlcard.GetAccom(blCard);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlAccomodationName.Items.Clear();
                ddlAccomodationName.DataSource = dtGetReturnedData;
                ddlAccomodationName.DataTextField = "AccomName";
                ddlAccomodationName.DataValueField = "AccomId";
                ddlAccomodationName.DataBind();
                ddlAccomodationName.Items.Insert(0, "-Select Hotel-");
            }
            else
            {

                ddlAccomodationName.Items.Clear();
                ddlAccomodationName.DataSource = null;
                ddlAccomodationName.DataBind();
                ddlAccomodationName.Items.Insert(0, "-No Hotel-");
            }
        }
        catch (Exception sqe)
        {
            ddlAccomodationName.Items.Clear();
            ddlAccomodationName.DataSource = null;
            ddlAccomodationName.DataBind();
            ddlAccomodationName.Items.Insert(0, "-No Hotel-");
        }
    }
    public void BindAccomNames1()
    {
        try
        {

            blCard._Action = "GetAccomname";

            dtGetReturnedData = dlcard.GetAccomname(blCard);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlAccomodationName.Items.Clear();
                ddlAccomodationName.DataSource = dtGetReturnedData;
                ddlAccomodationName.DataTextField = "AccomName";
                ddlAccomodationName.DataValueField = "AccomId";
                ddlAccomodationName.DataBind();
                ddlAccomodationName.Items.Insert(0, "-Select Hotel-");
            }
            else
            {
                othr.Visible = false;
                ddlAccomodationName.Items.Clear();
                ddlAccomodationName.DataSource = null;
                ddlAccomodationName.DataBind();
                ddlAccomodationName.Items.Insert(0, "-No Hotel-");
            }
        }
        catch (Exception sqe)
        {
            ddlAccomodationName.Items.Clear();
            ddlAccomodationName.DataSource = null;
            ddlAccomodationName.DataBind();
            ddlAccomodationName.Items.Insert(0, "-No Hotel-");
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        System.Web.Security.FormsAuthentication.SignOut();
        LinkButton1.Visible = false;
        Response.Redirect("SearchProperty1.aspx");
    }

    protected void ddlNoofrooms_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    SetInitialRow(Convert.ToInt32(ddlNoofrooms.SelectedValue));

        //}
        //catch
        //{
        //}
    }


    private void SetInitialRow(int num)
    {
        try
        {
            DataTable dtd = new DataTable();
            DataRow dr = null;
            for (int k = 0; k < num; k++)
            {
                if (dtd.Rows.Count < 1)
                {
                    dtd.Columns.Add(new DataColumn("Column1", typeof(string)));
                    dtd.Columns.Add(new DataColumn("Column2", typeof(string)));
                }


                dr = dtd.NewRow();

                dr["Column1"] = string.Empty;
                dr["Column2"] = string.Empty;

                dtd.Rows.Add(dr);
            }


            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dtd;

            gdvRooms.DataSource = dtd;
            gdvRooms.DataBind();
            gdvRooms.DataSource = dtd;
            gdvRooms.DataBind();
        }
        catch
        {
        }
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewAllBookings.aspx");
    }
    protected void lnkCustLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerLogin.aspx");
    }

    protected void lnkCustomerRegis_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Masters/NewuserRegister.aspx");
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataTable dt = dlsearch.getavaialablemonth();
        ddlDates.Items.Clear();
        if (ddlYear.SelectedIndex > 0)
        {
            string year = DateTime.Now.Year.ToString();
            if (year == ddlYear.SelectedItem.ToString())
            {

                int month = System.DateTime.Now.Month;
                ddlDates.Items.Insert(0, new ListItem("Select Month", "0"));
                var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
                string getmonth = DateTime.Now.ToString("MMMM");
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    if (Convert.ToInt32(dt.Rows[i]["monthid"].ToString()) >= month)
                    {



                        ddlDates.Items.Add(new System.Web.UI.WebControls.ListItem(dt.Rows[i]["name"].ToString(), dt.Rows[i]["monthid"].ToString()));
                    }
                    else if (Session["getrecntmonth"] != null)
                    {
                        ddlDates.Items.Add(new System.Web.UI.WebControls.ListItem(DateTimeFormatInfo.CurrentInfo.GetMonthName(i + 1), i + 1.ToString()));
                    }
                    //ddlMonth.Items.Add(i.ToString());
                }
                Session["getrecntmonth"] = null;
            }
            else
            {
                ddlDates.Items.Insert(0, new ListItem("Select Month", "0"));
                var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
                string getmonth = DateTime.Now.ToString("MMMM");
                for (int i = 0; i < dt.Rows.Count; i++)
                {



                    ddlDates.Items.Add(new System.Web.UI.WebControls.ListItem(dt.Rows[i]["name"].ToString(), dt.Rows[i]["monthid"].ToString()));


                    //ddlMonth.Items.Add(i.ToString());
                }
            }
        }
        else
        {
            loadmonths();
        }
    }
}