using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Cruise_booking_SearchProperty : System.Web.UI.Page
{

    DALOpenDates dlOpenDates = new DALOpenDates();
    BALOpenDates blOpenDates = new BALOpenDates();
    BALRateCard blCard = new BALRateCard();
    DALRateCard dlcard = new DALRateCard();
    DataTable dtGetReturnedData;
    int getQueryResponse = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                txtChkin.Attributes.Add("onchange", "return fillEndDate('" + txtChkin.ClientID + "','" + txtChkOut.ClientID + "');");
                if (Session["UserCode"] != null || Session["CustomerCode"] != null)
                {
                    LinkButton1.Visible = true;
                    lnkLogin.Visible = false;
                    lnkView.Visible = true;
                    lblLoginas.Visible = false;
                    lnkCustLogin.Visible = false;

                }
                else
                {
                    LinkButton1.Visible = false;
                    lnkLogin.Visible = true;
                    lnkView.Visible = false;
                    lblLoginas.Visible = true;
                    lnkCustLogin.Visible = true;

                }

                LoadCountries();
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
                rbtnSelectAccomtype.SelectedIndex = 0;
                if (Request.QueryString["Prop"] != null)
                {
                    AutofillSearch(Request.QueryString["Prop"].ToString());

                }
                else
                {
                    rbtnSelectAccomtype.SelectedIndex = 0;
                    ToggleDisplay("Cruise");
                }
            }
            catch
            {
            }

        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "setDate", "setDate()", true);
    }

    public void AutofillSearch(string Prop)
    {
        try
        {
            if (Request.QueryString["Prop"] == "ddune")
            {
                rbtnSelectAccomtype.SelectedIndex = 1;
                ToggleDisplay("XYZ");
                ddlAccomType.SelectedValue = "2";
                BindAccomNames(2);
                ddlAccomodationName.SelectedValue = "1";


            }
            else if (Request.QueryString["Prop"] == "pcamps")
            {
                rbtnSelectAccomtype.SelectedIndex = 1;
                ToggleDisplay("XYZ");
                ddlAccomType.SelectedValue = "5";
                BindAccomNames(5);
                ddlAccomodationName.SelectedValue = "10";
            }
            else if (Request.QueryString["Prop"] == "fhcamp")
            {
                rbtnSelectAccomtype.SelectedIndex = 1;
                ToggleDisplay("XYZ");
                ddlAccomType.SelectedValue = "5";
                BindAccomNames(5);
                ddlAccomodationName.SelectedValue = "16";
            }
            else if (Request.QueryString["Prop"] == "ncamps")
            {
                rbtnSelectAccomtype.SelectedIndex = 1;
                ToggleDisplay("XYZ");
                ddlAccomType.SelectedValue = "5";
                BindAccomNames(5);
                ddlAccomodationName.SelectedValue = "17";
            }
            else if (Request.QueryString["Prop"] == "boatvk")
            {
                rbtnSelectAccomtype.SelectedIndex = 1;
                ToggleDisplay("XYZ");
                ddlAccomType.SelectedValue = "3";
                BindAccomNames(3);
                ddlAccomodationName.SelectedValue = "3";
            }
            else if (Request.QueryString["Prop"] == "boatsv")
            {
                rbtnSelectAccomtype.SelectedIndex = 1;
                ToggleDisplay("XYZ");
                ddlAccomType.SelectedValue = "3";
                BindAccomNames(3);
                ddlAccomodationName.SelectedValue = "4";
            }
            else if (Request.QueryString["Prop"] == "rtkalakho")
            {
                rbtnSelectAccomtype.SelectedIndex = 1;
                ToggleDisplay("XYZ");
                ddlAccomType.SelectedValue = "11";
                BindAccomNames(11);
                ddlAccomodationName.SelectedValue = "6";
            }




            else
            {
                rbtnSelectAccomtype.SelectedIndex = 0;
                ToggleDisplay("Cruise");
            }
        }

        catch
        { }
    }




    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("PackageSearchResults.aspx?CId=" + ddlDestination.SelectedValue + "&date=" + ddlDates.SelectedValue + "&RId=" + ddlRiver.SelectedValue + "");
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

        try
        {
            blOpenDates._Action = "getseasonmonths";
            if (ddlDestination.SelectedIndex > 0)
            {
                blOpenDates._CountryId = Convert.ToInt32(ddlDestination.SelectedValue);
            }
            else
            {

                blOpenDates._CountryId = 0;
            }

            dtGetReturnedData = dlOpenDates.getMonthsforddl(blOpenDates);
            if (dtGetReturnedData != null && dtGetReturnedData.Rows.Count > 0)
            {
                ddlDates.DataSource = dtGetReturnedData;
                ddlDates.DataTextField = "MonthYYYY";
                ddlDates.DataValueField = "MonthYYYY";
                ddlDates.DataBind();
                ddlDates.Items.Insert(0, new ListItem("-Month-", "0"));
            }

            else
            {
                ddlDates.Items.Clear();
                ddlDates.DataSource = null;
                ddlDates.DataBind();
                ddlDates.Items.Insert(0, "-No Dates-");
            }
        }
        catch
        {

            ddlDates.Items.Clear();
            ddlDates.DataSource = null;
            ddlDates.DataBind();
            ddlDates.Items.Insert(0, "-No Dates-");
        }

        #endregion

    }

    protected void ddlDestination_SelectedIndexChanged(object sender, EventArgs e)
    {
        


    }
    protected void rbtnSelectAccomtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        ToggleDisplay(rbtnSelectAccomtype.SelectedItem.Text);
    }

    public void ToggleDisplay(string str)
    {
        try
        {
            if (str == "Cruise")
            {
                divCruise.Style.Remove("display");
                OtherAccoms.Style.Add("display", "none");

            }
            else
            {
                OtherAccoms.Style.Remove("display");
                divCruise.Style.Add("display", "none");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "setDate", "setDate()", true);
            }

        }

        catch
        {

        }
    }

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
        try
        {

            if (Convert.ToDateTime(txtChkin.Text).Date >= System.DateTime.Now.Date)
            {
                if (Session["UserCode"] != null)
                {
                    string guests = string.Empty;
                    int[] arr = new int[10];
                    int pax = totalguests(out arr);
                    Session["AccomName"] = ddlAccomodationName.SelectedItem.Text;
                    string url = "~/Hotel/HotelBooking.aspx?AId=" + Request.QueryString["Aid"].ToString() + "&AccomId=" + ddlAccomodationName.SelectedValue + "&AccomTypeId=" + ddlAccomType.SelectedValue + "&pax=" + pax.ToString() + "&Checkin=" + txtChkin.Text.Trim() + "&Checkout=" + txtChkOut.Text.Trim() + "&Noofrooms=" + ddlNoofrooms.SelectedValue + "&AccomName=" + ddlAccomodationName.SelectedItem.Text + "";
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
                    string url = "~/Hotel/HotelBooking.aspx?AId=0&AccomId=" + ddlAccomodationName.SelectedValue + "&AccomTypeId=" + ddlAccomType.SelectedValue + "&pax=" + pax.ToString() + "&Checkin=" + txtChkin.Text.Trim() + "&Checkout=" + txtChkOut.Text.Trim() + "&Noofrooms=" + ddlNoofrooms.SelectedValue + "&AccomName=" + ddlAccomodationName.SelectedItem.Text + " ";

                    for (int k = 0; k < gdvRooms.Rows.Count; k++)
                    {
                        guests = guests + " &guest" + (k + 1).ToString() + "=" + arr[k].ToString();

                    }
                    url = url + guests;
                    Session["Bookingdt"] = null;
                    Response.Redirect(url);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Sorry!Previous Dates Cannot be booked')", true);
            }
        }

        catch
        {

        }
    }



    private void BindAccomType()
    {
        try
        {
            blCard._Action = "GetAllAcoomTypes";
            dtGetReturnedData = dlcard.BindControls(blCard);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlAccomType.Items.Clear();
                ddlAccomType.DataSource = dtGetReturnedData;
                ddlAccomType.DataTextField = "AccomType";
                ddlAccomType.DataValueField = "AccomTypeId";
                ddlAccomType.DataBind();
                ddlAccomType.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlAccomType.Items.Clear();
                ddlAccomType.Items.Insert(0, "-No AccomType-");
            }
        }
        catch
        {
        }
    }
    protected void ddlAccomType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAccomNames(Convert.ToInt32(ddlAccomType.SelectedItem.Value));
    }

    public void BindAccomNames(int AccomTypeId)
    {
        try
        {
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
                ddlAccomodationName.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlAccomodationName.Items.Clear();
                ddlAccomodationName.DataSource = null;
                ddlAccomodationName.DataBind();
                ddlAccomodationName.Items.Insert(0, "-No Accom-");
            }
        }
        catch (Exception sqe)
        {
            ddlAccomodationName.Items.Clear();
            ddlAccomodationName.DataSource = null;
            ddlAccomodationName.DataBind();
            ddlAccomodationName.Items.Insert(0, "-No Accom-");
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

    protected void ddlNoofrooms_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SetInitialRow(Convert.ToInt32(ddlNoofrooms.SelectedValue));

        }
        catch
        {
        }
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
}