using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cruise_booking_CruiseBooking : System.Web.UI.Page
{
    RoomTypeMaster rtypemaster = new RoomTypeMaster();
    RoomCategoryMaster rcmaster = new RoomCategoryMaster();
    BALBooking blsr = new BALBooking();
    DALBooking dlsr = new DALBooking();
    public string packageid = string.Empty;
    DataView dv;
    DataTable dt;
    int totol = 0;
    int totpax = 0;
    int roomCatId = 0;
    int irpax = 0;
    double Totamt = 0;
    public int BookedId = 0;
    DataTable dtGetReturnedData;
    BALPackageRateCard blRate = new BALPackageRateCard();
    DALPackageRateCard dlRate = new DALPackageRateCard();
    string departureId;
    Label lablroomno = new Label();
    string b = "";
    List<string> checkvalue = new List<string>();
    string get = "";
    string packageid1 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["check"] == null)
        {
            Response.Redirect("searchproperty1.aspx");
        }
        if (Session["CustName"] != null)
        {
            lblUsername.Text = "Hello " + Session["CustName"].ToString();
            lnkCustomerRegis.Visible = false;
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
          //  lnkCustomerRegis.Visible = false;
            navlogin.Visible = true;
            LinkButton1.Visible = false;
        }
        //Fill All Agents Ref
        //  FillAgents();
        //BindCruiseRoomRates();
        for (int k = 0; k < GridRoomPaxDetail.Rows.Count; k++)
        {
            try
            {
                ImageButton imgbtn = (ImageButton)GridRoomPaxDetail.Rows[k].FindControl("imgbtnDelete");
                ScriptManager.GetCurrent(this).RegisterPostBackControl(imgbtn);
            }
            catch
            {
            }
        }

        if (!IsPostBack)
        {
            FillAgents();
            Session["get"] = null;
            try
            {
                ViewState["VsRoomDetails"] = null;
                //Session.Add("PackageId", Request.QueryString["PackId"].ToString());
                packageid1 = Request.QueryString["PackId"].ToString();
                if (packageid1 != "")
                {
                    Session["PackageId"] = packageid1;
                }
                else
                {
                    ViewState["VsRoomDetails"] = null;
                }
                departureId = Request.QueryString["DepartureId"].ToString();
                if (departureId != "")
                {
                    Session["getDepartureId"] = departureId;
                }
                Session["GetcruiseCheckin"] = Convert.ToDateTime(Request.QueryString["CheckInDate"].ToString()).ToString("dd MMM yyyy");
                Session["GetcruiseCheckOUt"] = Convert.ToDateTime(Request.QueryString["CheckOutDate"].ToString()).ToString("dd MMM yyyy");
                lblCheckInDate.Text = Convert.ToDateTime(Request.QueryString["CheckInDate"].ToString()).ToString("dd MMM yyyy");
                lblCheckOutDate.Text = Convert.ToDateTime(Request.QueryString["CheckOutDate"].ToString()).ToString("dd MMM yyyy");
                lblPackageName.Text = Request.QueryString["PackageName"].ToString();
                if (Request.QueryString["Discount"].ToString() == "")
                {
                    Session["getdiscountvalue"] = 0;
                }
                else
                {
                    Session["getdiscountvalue"] = Request.QueryString["Discount"].ToString();
                    string kjhlk = Request.QueryString["Discount"].ToString();
                }


                get = Request.QueryString["Discount"].ToString();
            }
            catch { }
            ddlConvert.SelectedIndex = 1;
            ddlpax1rm.SelectedIndex = 2;
            ButtonsDiv.Style.Add("display", "none");
            div1.Style.Add("display", "none");
            if (SessionServices.RetrieveSession<DataTable>("BookedRooms") != null)
            //if (Session["BookedRooms"] != null)
            {

            }
            else
            {
                //return;
            }
            DataTable bookedRooms = SessionServices.RetrieveSession<DataTable>("BookedRooms");
            //DataTable bookedRooms = Session["BookedRooms"] as DataTable;
            if (bookedRooms != null)
            {
                if (packageid1 != "")

                {
                    ViewState["VsRoomDetails"] = bookedRooms;
                }
                else
                {
                    ViewState["VsRoomDetails"] = null;
                }




                GridRoomPaxDetail.DataSource = bookedRooms;
                GridRoomPaxDetail.DataBind();

                if (GridRoomPaxDetail.Rows.Count > 0)
                {
                    ButtonsDiv.Style.Remove("display");
                }
                else
                {
                    ButtonsDiv.Style.Add("display", "none");
                }
                calculateTotal();
            }
            //Session["checkin"] = Request.QueryString["CheckIndate"].ToString();
            //BindCruiseRoomRates();
            bindRoomRates();
            //if (Session["getpax"] != null)
            //{
            //    ddlpax.Items.FindByText(Session["getpax"].ToString()).Selected = true;
            //}

            if (Session["UserCode"] != null || Session["CustomerCode"] != null)
            {
                //lnkLogout.Visible = true;
            }
            else
            {
                //lnkLogout.Visible = false;
            }
            loadall();
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "blockArea", "blockArea();", true);
    }

    private void setImageMap()
    {

        try
        {
            long get = 0;
            long get1 = 0;
            long getall = 0;
            string s = "";
            DataTable dtRoomsdata;

            dtRoomsdata = bindroomddl();
            PolygonHotSpot hotSpot;
            if (Session["get"] == null)
            {
                ImageMap1.ImageUrl = "~/images/aspnet_imagemap.png";
            }

            foreach (DataRow dr in dtRoomsdata.Rows)
            {
                hotSpot = new PolygonHotSpot();

                //HtmlTextWriter writer = new HtmlTextWriter(tex);
                //writer.AddStyleAttribute=

                if (dr["BookedStatus"].ToString() == "Available" && dr["ActiveStatus"].ToString() == "")
                {
                    hotSpot.HotSpotMode = HotSpotMode.PostBack;
                    hotSpot.AlternateText = "Available";


                }
                else
                {
                    hotSpot.HotSpotMode = HotSpotMode.Inactive;
                    try
                    {
                        if (Session["get"] != null)
                        {
                            s = Session["get"].ToString();
                        }

                        get = Convert.ToInt32(dr["Coordinates"].ToString().Split(',')[0]);
                        get1 = Convert.ToInt32(dr["Coordinates"].ToString().Split(',')[1]);
                        string path = Server.MapPath("inv/aspnet_imagemap" + s + ".png");
                        FileInfo file = new FileInfo(path);
                        if (file.Exists)
                        {



                        }
                        else
                        {
                            path = Server.MapPath("~/images/aspnet_imagemap.png");
                        }
                        Bitmap bitMapIm = new
         System.Drawing.Bitmap(path);
                        Graphics graphicIm = Graphics.FromImage(bitMapIm);

                        Pen penGreen = new Pen(Color.Green, 3);
                        Pen penRed = new Pen(Color.Red, 3);

                        //graphicIm.DrawEllipse(penGreen, hotSpot, hotSpot, 7, 7);

                        graphicIm.DrawString("X", new Font("Arial", 10, FontStyle.Bold), Brushes.Red, get, get1);
                        getall = get + get1;
                        Random rand = new Random();
                        long data = rand.Next(10, 100000000);
                        getall = getall + data;

                        while (File.Exists(Server.MapPath("inv/aspnet_imagemap" + getall + ".png")))
                        {
                            int count = 1;
                            Random rand1 = new Random();
                            long data1 = rand1.Next(10, 100000000);
                            getall = getall + data1;
                        }
                        FileInfo file1 = new FileInfo(Server.MapPath("inv/aspnet_imagemap" + getall + ".png"));


                        bitMapIm.Save((Server.MapPath("inv/aspnet_imagemap" + getall + ".png")), ImageFormat.Png);
                        graphicIm.Dispose();
                        bitMapIm.Dispose();
                        if (Session["get"] != null)
                        {
                            if (file.Exists)//check file exsit or not
                            {
                                file.Delete();
                            }
                        }
                        Session["get"] = getall;
                    }
                    catch { }
                    hotSpot.AlternateText = "Not Available";
                }
                //               
                hotSpot.Coordinates = dr["Coordinates"].ToString();
                // hotSpot.AlternateText = dr["BookedStatus"].ToString();
                //ImageMap1.Attributes.Add("color", "fuchsia");
                hotSpot.PostBackValue = dr["RoomNo"].ToString();

                // ImageMap1.ImageUrl = "~/images/aspnet_imagemap.png";
                ImageMap1.HotSpots.Add(hotSpot);
                if (Session["get"] == null)
                {
                    ImageMap1.ImageUrl = "~/images/aspnet_imagemap.png";
                }
                else
                {
                    ImageMap1.ImageUrl = "inv/aspnet_imagemap" + getall + ".png";
                }

            }


        }
        catch (Exception ex)
        {
        }
    }
    public string rename(string fullpath)
    {
        int count = 1;

        string fileNameOnly = Path.GetFileNameWithoutExtension(fullpath);
        string extension = Path.GetExtension(fullpath);
        string path = Path.GetDirectoryName(fullpath);
        string newFullPath = fullpath;

        while (File.Exists(newFullPath))
        {
            string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
            newFullPath = Path.Combine(path, tempFileName + extension);
        }
        return newFullPath;
    }
    #region UDF
    public void bindRoomRates()
    {
        try
        {
            if (Session["UserCode"] != null)
            {
                blsr.action = "RoomRates";
                blsr.AgentId = Convert.ToInt32(Session["UserCode"].ToString());
                if (ddlAgentRef.SelectedValue != "")
                {
                    blsr.AgentIdRef = Convert.ToInt32(ddlAgentRef.SelectedItem.Value);
                }
                else
                {
                    blsr.AgentIdRef = 0;
                }
                //blsr.AgentId = 247;
            }
            else
            {
                blsr.action = "RoomRatesCustAgent";
                blsr.AgentId = 247;
            }
            try
            {
                if (Session["GetcruiseCheckin"] != null)
                {
                    blsr._dtStartDate = Convert.ToDateTime(Session["GetcruiseCheckin"].ToString());
                }
            }
            catch { }
            blsr.PackageId = Session["PackId"].ToString();

            blsr.totpax = 2;
            //if (Session["totpax"] != null)
            //{
            //    blsr.totpax = Int32.Parse(Session["totpax"].ToString());
            //}

            dt = dlsr.GetRoomCategoryWiseRates(blsr);
            Session["getdefaultnoofbed"] = dt.Rows[0]["DefaultNoOfBeds"].ToString();
            Session["Getroomcatid"] = dt;
            if (Convert.ToInt32(dt.Rows[0]["DefaultNoOfBeds"].ToString()) >= 3)
            {

            }
            else
            {
                ddlpax.Items.FindByValue("3").Enabled = false;
            }
            DataView dv = dt.DefaultView;
            dv.Sort = "PPRoomRate asc";
            DataTable sortedDT = dv.ToTable();
            ViewState["RoomRate"] = sortedDT.Rows[0]["PPRoomRate"].ToString();
            string RoomRate = ViewState["RoomRate"].ToString();
            if (sortedDT != null && RoomRate != "0")
            {
                if (sortedDT.Rows.Count > 0)
                {
                    //Session["Rrate"] = sortedDT;
                    SessionServices.SaveSession<DataTable>("Rrate", sortedDT);
                    //Session["cTax"] = sortedDT.Rows[0]["Tax va"].ToString();
                    for (int i = 0; i < sortedDT.Rows.Count; i++)
                    {
                        long get1 = Convert.ToInt32(sortedDT.Rows[i]["Price Per Person sharing"].ToString().Split('R')[1].Split('.')[0]);
                        long get2 = Convert.ToInt32(sortedDT.Rows[i]["Price Per Person Single Use"].ToString().Split('R')[1].Split('.')[0]);
                        string get12 = Convert.ToInt32(get1).ToString("##,0");
                        string get22 = Convert.ToInt32(get2).ToString("##,0");

                        sortedDT.Rows[i]["Price Per Person sharing"] = "INR " + get12;
                        sortedDT.Rows[i]["Price Per Person Single Use"] = "INR " + Convert.ToInt32(sortedDT.Rows[i]["Price Per Person Single Use"].ToString().Split('R')[1].Split('.')[0]).ToString("##,0");
                        //lblTax.Text = "Tax Value also add separately.Tax percentage value is " + Convert.ToString(Convert.ToInt32(sortedDT.Rows[i]["Tax Value"].ToString().Split('R')[1].Split('.')[0])) + "%";
                        lblTax.Text = "Prices are excluding Government Taxes";
                        Session["gettaxpercentage"] = sortedDT.Rows[i]["Tax Value"].ToString().Split('R')[1].Split('.')[0] + "%";
                    }
                    gdvRoomCategories.DataSource = sortedDT;
                    gdvRoomCategories.DataBind();

                    div1.Style.Remove("display");
                    //roundoff();
                }
                else
                {
                    gdvRoomCategories.DataSource = null;
                    gdvRoomCategories.DataBind();
                    div1.Style.Add("display", "none");

                }
            }
            else
            {
                gdvRoomCategories.DataSource = null;
                gdvRoomCategories.DataBind();
                div1.Style.Add("display", "none");

                DataTable dt1 = new DataTable();
                DataColumn dc;
                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "RoomNumber";
                dt1.Columns.Add(dc);

                //dc = new DataColumn();
                //dc.DataType = Type.GetType("System.Int32");
                //dc.ColumnName = "RoomCategoryId";
                //dt.Columns.Add(dc);

                // Create second column.
                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "Pax";
                dt1.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "Currency";
                dt1.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "CRPrice";
                dt1.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "Tax1";
                dt1.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "Tax";
                dt1.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "pricewithouttax1";
                dt1.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "categoryName";
                dt1.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "Total";
                dt1.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "Discount";
                dt1.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "Discountprice";
                dt1.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "Totalprice";
                dt1.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "texableprice";
                dt1.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "taxablepamt";
                dt1.Columns.Add(dc);
                DataRow dr = dt1.NewRow();
                dr["Tax"] = " ";
                dt1.Rows.Add(dr, 0);
                Session["blank1"] = dt1;
                if (dt != null && dt.Rows.Count > 0)
                {
                    GridRoomPaxDetail.DataSource = dt1;
                    GridRoomPaxDetail.DataBind();
                    GridRoomPaxDetail.Columns[0].Visible = false;
                }
            }
        }
        catch (Exception exp)
        {
            div1.Style.Add("display", "none");
        }
    }

    public void roundoff()
    {
        foreach (GridViewRow row in gdvRoomCategories.Rows)
        {


            for (int k = 0; k < row.Cells.Count; k++)
            {
                try
                {
                    Label lblptws = (Label)row.FindControl("lbltws");
                    Label lblss = (Label)row.FindControl("lblsc");

                    string[] arrtws = lblptws.Text.ToString().Split(' ');
                    string[] arrss = lblss.Text.ToString().Split(' ');

                    lblptws.Text = arrtws[0].ToString() + " " + Convert.ToDecimal(arrtws[1]).ToString("#.##");

                    lblss.Text = arrss[0].ToString() + " " + Convert.ToDecimal(arrss[1]).ToString("#.##");
                    row.Cells[k].HorizontalAlign = HorizontalAlign.Center;
                }
                catch
                {

                }
            }
        }
    }

    private void GetRoomLimit()
    {
        try
        {

        }
        catch (Exception sqe)
        {

        }
    }
    public void hidecolumn(GridView grv, int num)
    {
        try
        {
            grv.HeaderRow.Cells[num].Visible = false;
            foreach (GridViewRow grow in grv.Rows)
            {
                grow.Cells[num].Visible = false;

            }

        }
        catch
        {

        }
    }
    public void initializetable()
    {

        try
        {
            dt = new DataTable();
            dt.Columns.Add("RoomCategory");
            dt.Columns.Add("NoofRooms");
            dt.Columns.Add("Total");
            dt.Columns.Add("roomcategoryid");
            dt.Columns.Add("Pax");
        }
        catch
        {
        }
    }
    public void addrows(DataView view, int roomcateId)
    {
        try
        {
            //The value coming from the stored procedure is like "INR 50000". That's why such a string is splitted to extract quantity from it.
            string[] arr1 = view.ToTable().Rows[0][1].ToString().Split(' ');
            string[] arr = view.ToTable().Rows[0][2].ToString().Split(' ');

            double pricePerPersonSharing = 0;
            double pricePerPersonSingleUse = 0;

            if (arr1.Length > 1)
            {
                double.TryParse(arr1[1], out pricePerPersonSharing);
            }
            if (arr.Length > 1)
            {
                double.TryParse(arr[1], out pricePerPersonSingleUse);
            }

            int count = 0;
            dt = new DataTable();
            dt = ViewState["dt"] as DataTable;
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    if (view.ToTable().Rows[0][10].ToString() == "2")
                        dr["RoomCategory"] = view.ToTable().Rows[0][0].ToString() + " Sharing";
                    else if (txtPassengers.Text == "1")
                        dr["RoomCategory"] = view.ToTable().Rows[0][0].ToString() + " Single Use";
                    else

                        dr["NoofRooms"] = 1;
                    if (Session["getbedconfig"] != null)
                    {
                        if (Session["getbedconfig"].ToString() == "Single")
                        {
                            dr["Total"] = (Convert.ToInt32(view.ToTable().Rows[0][10].ToString()) * pricePerPersonSingleUse);
                        }
                        else if (Session["getbedconfig"].ToString() == "Triple")
                        {
                            dr["Total"] = (Convert.ToInt32(view.ToTable().Rows[0][10].ToString()) * pricePerPersonSharing);
                        }
                        else
                        {
                            dr["Total"] = (Convert.ToInt32(view.ToTable().Rows[0][10].ToString()) * pricePerPersonSharing);
                        }


                    }
                    else
                    {
                        if (Convert.ToInt32(Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) < 2)
                            dr["Total"] = (Convert.ToInt32(Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) * pricePerPersonSingleUse);
                        else
                            dr["Total"] = (Convert.ToInt32(Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) * pricePerPersonSharing);
                    }

                    dr["roomcategoryid"] = view.ToTable().Rows[0][3].ToString();

                    foreach (DataRow dr1 in dt.Rows)
                    {
                        if (dr1["roomcategoryid"].ToString() == (view.ToTable().Rows[0][3].ToString()))
                        {
                            if (Convert.ToInt32(view.ToTable().Rows[0][10].ToString()) < 2)
                            {
                                if (dr1["Pax"] == view.ToTable().Rows[0][10].ToString())
                                {
                                    dr1["Total"] = Convert.ToDouble(dr1["Total"]) + (Convert.ToInt32(view.ToTable().Rows[0][10].ToString()) * pricePerPersonSingleUse);
                                    dr1["NoofRooms"] = Convert.ToInt32(dr1["NoofRooms"]) + 1;
                                    dr["RoomCategory"] = view.ToTable().Rows[0][0].ToString() + " Single Use";
                                    count++;
                                }
                            }
                            else
                            {
                                if (dr1["Pax"] == view.ToTable().Rows[0][10].ToString())
                                {
                                    dr1["Total"] = Convert.ToDouble(dr1["Total"]) + (Convert.ToInt32(view.ToTable().Rows[0][10].ToString()) * pricePerPersonSharing);
                                    dr1["NoofRooms"] = Convert.ToInt32(dr1["NoofRooms"]) + 1;
                                    dr["RoomCategory"] = view.ToTable().Rows[0][0].ToString() + " Sharing";
                                    count++;
                                }
                            }
                        }
                    }
                    dr["Pax"] = view.ToTable().Rows[0][10].ToString();

                    if (count == 0)
                    {
                        dt.Rows.Add(dr);
                    }

                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    hidecolumn(GridView1, 3);
                    ViewState["dt"] = dt;
                    this.RoomNumberWiseDetail(dv, roomcateId);
                }
            }
            else
            {
                initializetable();
                DataRow dr = dt.NewRow();
                if (view.ToTable().Rows[0][10].ToString() == "2")
                {
                    dr["RoomCategory"] = view.ToTable().Rows[0][0].ToString() + " Sharing";
                }
                else if (txtPassengers.Text == "1")
                {
                    dr["RoomCategory"] = view.ToTable().Rows[0][0].ToString() + " Single Use";
                }
                dr["NoofRooms"] = 1;
                if (Session["getbedconfig"] != null)
                {
                    if (Session["getbedconfig"].ToString() == "Single")
                    {
                        dr["Total"] = (Convert.ToInt32(view.ToTable().Rows[0][10].ToString()) * pricePerPersonSingleUse);
                    }
                    else if (Session["getbedconfig"].ToString() == "Triple")
                    {
                        dr["Total"] = (3 * pricePerPersonSharing); ;
                    }
                    else
                    {
                        dr["Total"] = (2 * pricePerPersonSharing);
                    }


                }
                else
                {
                    if (Convert.ToInt32(view.ToTable().Rows[0][10].ToString()) < 2)
                        dr["Total"] = (Convert.ToInt32(view.ToTable().Rows[0][10].ToString()) * pricePerPersonSingleUse);
                    else
                        dr["Total"] = (Convert.ToInt32(view.ToTable().Rows[0][10].ToString()) * pricePerPersonSharing);
                }
                dr["roomcategoryid"] = view.ToTable().Rows[0][3].ToString();
                dr["Pax"] = txtPassengers.Text;
                dt.Rows.Add(dr);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                hidecolumn(GridView1, 3);
                ViewState["dt"] = dt;

                this.RoomNumberWiseDetail(dv, roomcateId); // calling Insertable RoomDetail Function 
            }
            calculateTotal();
        }
        catch (Exception ex)
        {

        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        System.Web.Security.FormsAuthentication.SignOut();
        Response.Redirect("SearchProperty1.aspx");
    }

    public void calculateTotal()
    {
        try
        {
            Totamt = 0;
            DataTable dt1 = new DataTable();
            dt1 = ViewState["VsRoomDetails"] as DataTable;
            if (dt1 != null)
            {
                for (int k = 0; k < dt1.Rows.Count; k++)
                {
                    Totamt = Totamt + Convert.ToDouble(dt1.Rows[k]["Totalprice"].ToString());
                }

                lblTotal.Text = "Total: ";
                lblTotAmt.Text = Totamt.ToString();
                lblTotalCabins.Text = dt1.Rows.Count.ToString();
                TotalCabins.Text = "Cabins Selected";
                //GridRoomPaxDetail.FooterRow.Cells[3].Text =;
                //GridRoomPaxDetail.FooterRow.Cells[6].Text = "<strong style='font-weight: bolder;float: left; color: Black;'>Total :</strong>" + " " + "<strong style='font-weight: bolder; color: Black;float: right;padding-right: 24%;'> INR" + " " + Totamt.ToString("##,0") + " </strong>" + "           " + " ";
                lblgetTotal.Text = "INR " + Totamt.ToString("##,0");


            }
            else
            {
                lblTotal.Text = "";
                lblTotAmt.Text = "";
                lblTotalCabins.Text = "";
                TotalCabins.Text = "";
            }
        }

        catch
        {
        }
    }
    public DataTable bindroomddl()
    {
        try
        {
            blsr.action = "GetcruiseRooms";
            blsr.PackageId = Session["PackageId"].ToString();
            if (Request.QueryString["DepartureId"] != null)
            {
                Session["DepartureId"] = Request.QueryString["DepartureId"].ToString();
            }
            Session["DepartureId"] = Session["getDepartureId"].ToString();
            blsr.DepartureId = Convert.ToInt32(Session["getDepartureId"].ToString());

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

    private int InsertParentTableData()
    {
        try
        {
            blsr.action = "GetDepartureDetails";
            blsr.PackageId = Request.QueryString["PackId"].ToString();
            dtGetReturnedData = dlsr.GetDepartureDetails(blsr);
            blsr._sBookingRef = txtBookingRef.Text.Trim().ToString();
            blsr._dtStartDate = Convert.ToDateTime(dtGetReturnedData.Rows[0]["CheckInDate"]);
            blsr._dtEndDate = Convert.ToDateTime(dtGetReturnedData.Rows[0]["CheckOutDate"]);
            blsr._iAccomTypeId = Convert.ToInt32(dtGetReturnedData.Rows[0]["AccomTypeId"]);
            blsr._iAccomId = Convert.ToInt32(dtGetReturnedData.Rows[0]["AccomId"]);
            blsr._iAgentId = Convert.ToInt32(Session["UserCode"].ToString());
            blsr._iNights = Convert.ToInt32(dtGetReturnedData.Rows[0]["NoOfNights"]);
            DataTable dtRoomBookingDetails = ViewState["VsRoomDetails"] as DataTable;
            blsr._iPersons = Convert.ToInt32(dtRoomBookingDetails.Compute("SUM(Pax)", string.Empty));
            blsr._BookingStatusId = 1;
            blsr._SeriesId = 0;
            blsr._proposedBooking = false;
            blsr._chartered = false;
            Session.Add("tblBookingBAL", blsr);
            int GetQueryResponse = dlsr.AddParentBookingDetail(blsr);
            if (GetQueryResponse > 0)
                return 1;
            else
                return 0;
        }
        catch
        {
            return 0;
        }
    }
    private int InsertChildTableData()
    {
        #region Fetching Departure Details
        blsr.action = "GetDepartureDetails";
        blsr.PackageId = Request.QueryString["PackId"].ToString();
        dtGetReturnedData = dlsr.GetDepartureDetails(blsr);
        blsr._iAccomId = Convert.ToInt32(dtGetReturnedData.Rows[0]["AccomId"]); ;
        #endregion
        blsr.action = "getMaxBookId";
        DataTable dtmaxId = dlsr.GetMaxBookingId(blsr);
        if (dtGetReturnedData != null)
        {
            int MaxBookingId = Convert.ToInt32(dtmaxId.Rows[0].ItemArray[0].ToString());
            BookedId = MaxBookingId;
            blsr._iBookingId = MaxBookingId;
            int LoopInsertStatus = 0;
            try
            {
                for (int LoopCounter = 0; LoopCounter < GridRoomPaxDetail.Rows.Count - 1; LoopCounter++)
                {
                    Label lbRoomNo = (Label)GridRoomPaxDetail.Rows[LoopCounter].Cells[0].FindControl("RoomId");
                    Label bRoomCategoryId = (Label)GridRoomPaxDetail.Rows[LoopCounter].Cells[1].FindControl("RoomCategoryId");
                    Label lbPax = (Label)GridRoomPaxDetail.Rows[LoopCounter].Cells[2].FindControl("Pax");
                    Label lbPrice = (Label)GridRoomPaxDetail.Rows[LoopCounter].Cells[3].FindControl("Price");
                    blsr._dtStartDate = Convert.ToDateTime(dtGetReturnedData.Rows[0]["CheckInDate"]);
                    blsr._dtEndDate = Convert.ToDateTime(dtGetReturnedData.Rows[0]["CheckOutDate"]);
                    blsr._iPaxStaying = Convert.ToInt32(lbPax.Text.Trim().ToString());
                    blsr._bConvertTo_Double_Twin = false;
                    blsr._cRoomStatus = "B";
                    blsr._sRoomNo = lbRoomNo.Text.Trim().ToString();
                    blsr.action = "AddPriceDetailsToo";
                    blsr._Amt = Convert.ToDecimal(lbPrice.Text.ToString());
                    int GetQueryResponse = dlsr.AddRoomBookingDetails(blsr);
                    if (GetQueryResponse > 0)
                        LoopInsertStatus++;
                    else
                    {
                        //do nothing
                    }


                }
                if (LoopInsertStatus == GridRoomPaxDetail.Rows.Count - 1)
                    return 1;
                else
                    return
                        0;

            }
            catch
            {
                return 0;
            }
        }
        else
            return 0;
    }
    #endregion

    #region Control Events
    private void RoomNumberWiseDetail(DataView view, int RoomcateId)
    {
        try
        {
            string[] arrWZ;
            string[] arr = view.ToTable().Rows[0][2].ToString().Split(' ');
            double arrr = 0;
            if (arr.Length > 1)
            {
                double.TryParse(arr[1], out arrr);
            }

            string[] arr1 = view.ToTable().Rows[0][1].ToString().Split(' ');
            double arrr1 = 0;
            if (arr1.Length > 1)
            {
                double.TryParse(arr1[1], out arrr1);
            }

            string[] arrtx = view.ToTable().Rows[0][5].ToString().Split(' ');
            double arrrtx = 0;
            if (arrtx.Length > 1)
            {
                double.TryParse(arrtx[1], out arrrtx);
            }

            string[] arr1tx = view.ToTable().Rows[0][4].ToString().Split(' ');
            double arrr1tx = 0;
            if (arr1tx.Length > 1)
            {
                double.TryParse(arr1tx[1], out arrr1tx);
            }
            Session["gettax"] = view.ToTable().Rows[0][6].ToString();

            DataTable dtInsertable = new DataTable();

            dtInsertable.Columns.Add("RoomNumber", typeof(string));
            dtInsertable.Columns.Add("RoomCategoryId", typeof(int));
            dtInsertable.Columns.Add("categoryName", typeof(string));
            dtInsertable.Columns.Add("Pax", typeof(int));
            dtInsertable.Columns.Add("RoomType", typeof(string));
            dtInsertable.Columns.Add("Price", typeof(decimal));

            dtInsertable.Columns.Add("Tax", typeof(decimal));
            dtInsertable.Columns.Add("Tax1", typeof(string));
            dtInsertable.Columns.Add("Currency", typeof(string));
            dtInsertable.Columns.Add("Convertable", typeof(string));
            dtInsertable.Columns.Add("CRPrice", typeof(string));
            dtInsertable.Columns.Add("pricewithouttax", typeof(string));
            dtInsertable.Columns.Add("pricewithouttax1", typeof(string));
            dtInsertable.Columns.Add("Total", typeof(string));
            dtInsertable.Columns.Add("Discount", typeof(string));
            dtInsertable.Columns.Add("Totalprice", typeof(string));
            dtInsertable.Columns.Add("Discountprice", typeof(string));
            dtInsertable.Columns.Add("texableprice", typeof(string));
            dtInsertable.Columns.Add("taxablepamt", typeof(string));
            dtInsertable.Columns.Add("Scat", typeof(string));
            dtInsertable.Columns.Add("Bconfig", typeof(string));

            if (ViewState["VsRoomDetails"] == null)
            {
                DataRow dr = dtInsertable.NewRow();
                if (hfRoomId.Value == "")
                {
                    if (Session["rn"] != null)
                    {
                        dr["RoomNumber"] = Session["rn"].ToString();
                    }

                }
                else
                {
                    dr["RoomNumber"] = hfRoomId.Value;
                    Session["rn"] = hfRoomId.Value;
                }


                dr["RoomCategoryId"] = RoomcateId;
                dr["Tax"] = view.ToTable().Rows[0][6].ToString().Split('R')[1].Split('.')[0];
                string klj = view.ToTable().Rows[0][6].ToString().Split('R')[1].Split('.')[0];
                dr["Discount"] = Session["getdiscountvalue"].ToString() + "%";
                Session["GetCruiseRoomcatid"] = RoomcateId;
                Session["totpax"] = Convert.ToInt32(view.ToTable().Rows[0][10].ToString());
                dr["Pax"] = Convert.ToInt32(view.ToTable().Rows[0][10].ToString());
                dr["Currency"] = view.ToTable().Rows[0][8].ToString();

                //if ((txtPassengers.Text.ToString() == "2"))
                //{
                //    dr["RoomType"] = view.ToTable().Rows[0][9].ToString();
                //}
                //else if (txtPassengers.Text.ToString() == "1")
                //{
                //    dr["RoomType"] = view.ToTable().Rows[0][9].ToString();
                //}
                //else
                {
                    dr["RoomType"] = view.ToTable().Rows[0][9].ToString();
                    dr["Bconfig"] = view.ToTable().Rows[0][9].ToString() + " Bed";
                }


                dr["Convertable"] = ddlConvert.SelectedValue.ToString();

                //dr["Price"] = Convert.ToDecimal(Convert.ToInt32(ddlpax1rm.SelectedValue) * Convert.ToDouble(view.ToTable().Rows[0][2]));
                if (Convert.ToInt32(view.ToTable().Rows[0][10].ToString()) >= 2)
                {
                    if (Session["getbedconfig"] != null)
                    {
                        if (Session["getbedconfig"].ToString() == "Single")
                        {
                            Session["totpax"] = 1;
                            //Session["taxinclusive"]
                            string TaxStatus = (view.ToTable().Rows[0][7].ToString());
                            string TaxValue = (view.ToTable().Rows[0][6].ToString());
                            //if (TaxStatus == "Tax Applied")
                            //{
                            arrWZ = view.ToTable().Rows[0][4].ToString().Split(' ');
                            dr["CRPrice"] = arrWZ[0].ToString() + " " + (arrrtx * Convert.ToInt32(1)).ToString("##,0");
                            dr["Price"] = Convert.ToDouble(arrrtx * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())).ToString("##,0");
                            /* dr["Tax"] = 0;*/// Convert.ToDecimal(TaxValue.ToString());
                            dr["pricewithouttax"] = arrr.ToString("##,0");

                            dr["pricewithouttax1"] = arrr.ToString("##,0");
                            dr["Total"] = Convert.ToDouble(arrr * 1).ToString("##,0");

                            double getdiscountamount = (Convert.ToDouble(arrr * 1) * (1 * Convert.ToDouble(Session["getdiscountvalue"].ToString()))) / 100;
                            dr["taxablepamt"] = (Convert.ToDouble(arrr * 1) - getdiscountamount).ToString("##,0");
                            double taxableamnt = (Convert.ToDouble(arrr * 1) - getdiscountamount);
                            dr["Discountprice"] = getdiscountamount.ToString("##,0");

                            double tax = Math.Round((taxableamnt * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Tax1"] = Convert.ToDouble(tax).ToString("##,0");
                            double taxableprice = Math.Round(((arrr * Convert.ToInt32(1)) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Totalprice"] = Convert.ToDouble(taxableamnt + tax).ToString("##,0");
                            dr["texableprice"] = Convert.ToDouble(taxableprice).ToString("##,0");

                        }
                        else if (Session["getbedconfig"].ToString() == "Triple")
                        {
                            Session["totpax"] = 3;
                            //Session["taxinclusive"]
                            string TaxStatus = (view.ToTable().Rows[0][7].ToString());
                            string TaxValue = (view.ToTable().Rows[0][6].ToString());
                            //if (TaxStatus == "Tax Applied")
                            //{
                            arrWZ = view.ToTable().Rows[0][4].ToString().Split(' ');
                            dr["CRPrice"] = arrWZ[0].ToString() + " " + (arrrtx * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())).ToString("##,0");
                            dr["Price"] = Convert.ToDouble(arrrtx * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())).ToString("##,0");
                            /* dr["Tax"] = 0;*/// Convert.ToDecimal(TaxValue.ToString());
                            dr["pricewithouttax"] = arrr.ToString("##,0");

                            dr["pricewithouttax1"] = arrr.ToString("##,0");
                            dr["Total"] = Convert.ToDouble(arrr * 3).ToString("##,0");
                            double getdiscountamount = (Convert.ToDouble(arrr * 3) * (3 * Convert.ToDouble(Session["getdiscountvalue"].ToString()))) / 100;
                            dr["taxablepamt"] = (Convert.ToDouble(arrr * 3) - getdiscountamount).ToString("##,0");
                            double taxableamnt = (Convert.ToDouble(arrr * 3) - getdiscountamount);
                            dr["Discountprice"] = getdiscountamount.ToString("##,0");

                            double tax = Math.Round((taxableamnt * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Tax1"] = Convert.ToDouble(tax).ToString("##,0");
                            double taxableprice = Math.Round(((arrr * Convert.ToInt32(1)) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Totalprice"] = Convert.ToDouble(taxableamnt + tax).ToString("##,0");
                            dr["texableprice"] = Convert.ToDouble(taxableprice).ToString("##,0");

                        }
                        else
                        {
                            Session["totpax"] = 2;
                            string TaxStatus = (view.ToTable().Rows[0][7].ToString());
                            string TaxValue = (view.ToTable().Rows[0][6].ToString());
                            //if (TaxStatus == "Tax Applied")
                            //{
                            arrWZ = view.ToTable().Rows[0][4].ToString().Split(' ');
                            dr["CRPrice"] = arrWZ[0].ToString() + " " + (arrr1tx * Convert.ToInt32(2)).ToString("##,0");
                            dr["Price"] = Convert.ToDouble(arrr1tx * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())).ToString("##,0");
                            /* dr["Tax"] = 0;*/// Convert.ToDecimal(TaxValue.ToString());
                            dr["pricewithouttax"] = arrr1.ToString("##,0");

                            dr["pricewithouttax1"] = arrr1.ToString("##,0");
                            dr["Total"] = Convert.ToDouble(arrr1 * Convert.ToInt32(2)).ToString("##,0");
                            double getdiscountamount = (Convert.ToDouble(arrr1 * 2) * (2 * Convert.ToDouble(Session["getdiscountvalue"].ToString()))) / 100;
                            dr["taxablepamt"] = (Convert.ToDouble(arrr1 * 2) - getdiscountamount).ToString("##,0");
                            double taxableamnt = (Convert.ToDouble(arrr1 * 2) - getdiscountamount);
                            dr["Discountprice"] = getdiscountamount.ToString("##,0");

                            double tax = Math.Round(((taxableamnt) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Tax1"] = Convert.ToDouble(tax).ToString("##,0");
                            double taxableprice = Math.Round(((arrr1 * Convert.ToInt32(1)) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Totalprice"] = Convert.ToDouble(taxableamnt + tax).ToString("##,0");
                            dr["texableprice"] = Convert.ToDouble(taxableprice).ToString("##,0");
                        }
                    }
                    else
                    {

                        string TaxStatus = (view.ToTable().Rows[0][7].ToString());
                        string TaxValue = (view.ToTable().Rows[0][6].ToString());
                        //if (TaxStatus == "Tax Applied")
                        //{
                        arrWZ = view.ToTable().Rows[0][4].ToString().Split(' ');
                        dr["CRPrice"] = arrWZ[0].ToString() + " " + (arrr1tx * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())).ToString("##,0");
                        dr["Price"] = Convert.ToDouble(arrr1tx * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())).ToString("##,0");
                        /* dr["Tax"] = 0;*/// Convert.ToDecimal(TaxValue.ToString());
                        dr["pricewithouttax"] = arrr1.ToString("##,0");

                        dr["pricewithouttax1"] = arrr1.ToString("##,0");
                        dr["Total"] = Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())).ToString("##,0");
                        //double getdiscountamount = (Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) * (Convert.ToInt32(view.ToTable().Rows[0][10].ToString()) * Convert.ToDouble(Session["getdiscountvalue"].ToString()))) / 100;
                        double getdiscountamount = (Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) * (Convert.ToDouble(Session["getdiscountvalue"].ToString()))) / 100;
                        dr["taxablepamt"] = (Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) - getdiscountamount).ToString("##,0");
                        double taxableamnt = (Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) - getdiscountamount);
                        dr["Discountprice"] = getdiscountamount.ToString("##,0");

                        double tax = Math.Round((taxableamnt * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                        dr["Tax1"] = Convert.ToDouble(tax).ToString("##,0");
                        double taxableprice = Math.Round(((arrr1 * Convert.ToInt32(1)) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                        dr["Totalprice"] = Convert.ToDouble(taxableamnt + tax).ToString("##,0");
                        dr["texableprice"] = Convert.ToDouble(taxableprice).ToString("##,0");
                    }

                }
                else
                {
                    if (Session["getbedconfig"] != null)
                    {
                        if (Session["getbedconfig"].ToString() == "Single")
                        {

                            string TaxStatus = (view.ToTable().Rows[0][7].ToString());
                            string TaxValue = (view.ToTable().Rows[0][6].ToString());
                            //if (TaxStatus == "Tax Applied")
                            //{
                            arrWZ = view.ToTable().Rows[0][5].ToString().Split(' ');
                            dr["CRPrice"] = arrWZ[0].ToString() + " " + Convert.ToDouble(arrWZ[1]).ToString("##,0");
                            dr["Price"] = Convert.ToDouble(arrrtx).ToString("##,0");
                            /* dr["Tax"] = 0;*/// Convert.ToDecimal(TaxValue.ToString());
                            dr["pricewithouttax"] = arrr.ToString("##,0");

                            dr["pricewithouttax1"] = arrr.ToString("##,0");
                            dr["Total"] = Convert.ToDouble(arrr * 1).ToString("##,0");
                            double getdiscountamount = (Convert.ToDouble(arrr * 1) * (1 * Convert.ToDouble(Session["getdiscountvalue"].ToString()))) / 100;
                            dr["taxablepamt"] = (Convert.ToDouble(arrr * 1) - getdiscountamount).ToString("##,0");
                            double taxableamnt = (Convert.ToDouble(arrr * 1) - getdiscountamount);
                            dr["Discountprice"] = getdiscountamount.ToString("##,0");

                            double tax = Math.Round(((taxableamnt) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Tax1"] = Convert.ToDouble(tax).ToString("##,0");
                            double taxableprice = Math.Round(((arrr * Convert.ToInt32(1)) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Totalprice"] = Convert.ToDouble(taxableamnt + tax).ToString("##,0");
                            dr["texableprice"] = Convert.ToDouble(taxableprice).ToString("##,0");
                        }
                        else
                        {

                            string TaxStatus = (view.ToTable().Rows[0][7].ToString());
                            string TaxValue = (view.ToTable().Rows[0][6].ToString());
                            //if (TaxStatus == "Tax Applied")
                            //{
                            arrWZ = view.ToTable().Rows[0][5].ToString().Split(' ');
                            dr["CRPrice"] = arrWZ[0].ToString() + " " + Convert.ToDouble(arrr1tx).ToString("##,0");
                            dr["Price"] = Convert.ToDouble(arrr1tx).ToString("##,0");
                            /* dr["Tax"] = 0;*/// Convert.ToDecimal(TaxValue.ToString());
                            dr["pricewithouttax"] = arrr1.ToString("##,0");
                            dr["pricewithouttax1"] = arrr1.ToString("##,0");
                            dr["Total"] = Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())).ToString("##,0");
                            double getdiscountamount = (Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) * (Convert.ToInt32(view.ToTable().Rows[0][10].ToString()) * Convert.ToDouble(Session["getdiscountvalue"].ToString()))) / 100;
                            dr["taxablepamt"] = (Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) - getdiscountamount).ToString("##,0");
                            double taxableamnt = (Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) - getdiscountamount);
                            dr["Discountprice"] = getdiscountamount.ToString("##,0");

                            double tax = Math.Round(((taxableamnt) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Tax1"] = Convert.ToDouble(tax).ToString("##,0");
                            double taxableprice = Math.Round(((arrr1 * Convert.ToInt32(1)) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Totalprice"] = Convert.ToDouble(Convert.ToDouble(taxableamnt + tax)).ToString("##,0");
                            dr["texableprice"] = Convert.ToDouble(taxableprice).ToString("##,0");
                        }
                    }
                    else
                    {

                        string TaxStatus = (view.ToTable().Rows[0][7].ToString());
                        string TaxValue = (view.ToTable().Rows[0][6].ToString());
                        //if (TaxStatus == "Tax Applied")
                        //{
                        arrWZ = view.ToTable().Rows[0][5].ToString().Split(' ');
                        dr["CRPrice"] = arrWZ[0].ToString() + " " + Convert.ToDouble(arrr1tx).ToString("##,0");
                        dr["Price"] = Convert.ToDouble(arrr1tx).ToString("##,0");
                        /* dr["Tax"] = 0;*/// Convert.ToDecimal(TaxValue.ToString());
                        dr["pricewithouttax"] = arrr1.ToString("##,0");
                        dr["pricewithouttax1"] = arrr1.ToString("##,0");
                        dr["Total"] = Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())).ToString("##,0");
                        double getdiscountamount = (Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) * (Convert.ToInt32(view.ToTable().Rows[0][10].ToString()) * Convert.ToDouble(Session["getdiscountvalue"].ToString()))) / 100;
                        dr["taxablepamt"] = (Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) - getdiscountamount).ToString("##,0");
                        double taxableamnt = (Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) - getdiscountamount);
                        dr["Discountprice"] = getdiscountamount.ToString("##,0");

                        double tax = Math.Round(((Convert.ToDouble(taxableamnt)) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                        dr["Tax1"] = Convert.ToDouble(tax).ToString("##,0");
                        double taxableprice = Math.Round(((arrr1 * Convert.ToInt32(1)) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                        dr["Totalprice"] = Convert.ToDouble(taxableamnt + tax).ToString("##,0");
                        dr["texableprice"] = Convert.ToDouble(taxableprice).ToString("##,0");
                    }
                }
                if (txtPassengers.Text.ToString() == "2")
                    dr["categoryName"] = view.ToTable().Rows[0][0].ToString();
                else
                    dr["categoryName"] = view.ToTable().Rows[0][0].ToString();
                dr["SCat"] = "Cruise With " + view.ToTable().Rows[0][0].ToString();
                dtInsertable.Rows.Add(dr);
                ViewState["VsRoomDetails"] = dtInsertable;

                GridRoomPaxDetail.DataSource = dtInsertable;
                GridRoomPaxDetail.DataBind();
                GridRoomPaxDetail.Columns[0].Visible = true;
            }
            else
            {

                dtInsertable = ViewState["VsRoomDetails"] as DataTable;
                DataRow dr = dtInsertable.NewRow();
                dr["Tax"] = view.ToTable().Rows[0][6].ToString().Split('R')[1].Split('.')[0];
                dr["Convertable"] = ddlConvert.SelectedValue.ToString();

                {
                    dr["RoomNumber"] = hfRoomId.Value;
                }
                Session["totpax"] = Convert.ToInt32(view.ToTable().Rows[0][10].ToString());
                dr["Discount"] = Session["getdiscountvalue"].ToString() + "%";
                dr["RoomCategoryId"] = RoomcateId;
                if (Session["getbedconfig"] != null)
                {
                    dr["RoomType"] = Session["getbedconfig"].ToString();
                    dr["Bconfig"] = Session["getbedconfig"].ToString() + " Bed";
                }
                else
                {
                    try
                    {
                        if ((txtPassengers.Text.ToString() == "2"))
                        {
                            dr["RoomType"] = dtInsertable.Rows[0]["RoomType"].ToString();
                            dr["Bconfig"] = dtInsertable.Rows[0]["RoomType"].ToString() + " Bed";
                        }
                        else if (txtPassengers.Text.ToString() == "1")
                        {
                            dr["RoomType"] = dtInsertable.Rows[0]["RoomType"].ToString();
                            dr["Bconfig"] = dtInsertable.Rows[0]["RoomType"].ToString() + " Bed";
                        }
                        else
                        {
                            dr["RoomType"] = dtInsertable.Rows[0]["RoomType"].ToString();
                            dr["Bconfig"] = dtInsertable.Rows[0]["RoomType"].ToString() + " Bed";
                        }
                    }
                    catch
                    {
                        dr["RoomType"] = "Twin";
                        dr["Bconfig"] = "Twin" + " Bed";
                    }
                }
                dr["Currency"] = view.ToTable().Rows[0][8].ToString();
                if (txtPassengers.Text.ToString() == "2")
                    dr["categoryName"] = view.ToTable().Rows[0][0].ToString();
                else
                    dr["categoryName"] = view.ToTable().Rows[0][0].ToString();
                dr["SCat"] = "Cruise With " + view.ToTable().Rows[0][0].ToString();
                if (Session["getbedconfig"] != null)
                {
                    if (Session["getbedconfig"].ToString() == "Single")
                    {
                        dr["Pax"] = 1;
                    }
                    else if (Session["getbedconfig"].ToString() == "Triple")
                    {
                        dr["Pax"] = 3;
                    }
                    else
                    {
                        dr["Pax"] = 2;
                    }
                }
                else
                {
                    dr["Pax"] = 2;
                }

                //dr["Price"] = Convert.ToDecimal(Convert.ToInt32(ddlpax1rm.SelectedValue) * Convert.ToDouble(view.ToTable().Rows[0][2]));
                if (Convert.ToInt32(view.ToTable().Rows[0][10].ToString()) >= 2)
                {
                    if (Session["getbedconfig"] != null)
                    {
                        if (Session["getbedconfig"].ToString() == "Single")
                        {
                            Session["totpax"] = 1;
                            string TaxStatus = (view.ToTable().Rows[0][7].ToString());
                            string TaxValue = (view.ToTable().Rows[0][6].ToString());

                            arrWZ = view.ToTable().Rows[0][4].ToString().Split(' ');
                            dr["CRPrice"] = arrWZ[0].ToString() + " " + (arrrtx * Convert.ToInt32(1)).ToString("##,0");
                            dr["Price"] = Convert.ToDouble(arrrtx * Convert.ToInt32(1)).ToString("##,0");
                            /* dr["Tax"] = 0;*/// Convert.ToDecimal(TaxValue.ToString());     
                            dr["pricewithouttax"] = arrr.ToString("##,0");
                            dr["pricewithouttax1"] = arrr.ToString("##,0");
                            dr["Total"] = Convert.ToDouble(arrr * 1).ToString("##,0");
                            double getdiscountamount = (Convert.ToDouble(arrr * 1) * (1 * Convert.ToDouble(Session["getdiscountvalue"].ToString()))) / 100;
                            dr["taxablepamt"] = (Convert.ToDouble(arrr * 1) - getdiscountamount).ToString("##,0");
                            double taxableamnt = (Convert.ToDouble(arrr * 1) - getdiscountamount);
                            dr["Discountprice"] = getdiscountamount.ToString("##,0");

                            double tax = Math.Round(((taxableamnt) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Tax1"] = Convert.ToDouble(tax).ToString("##,0");
                            double taxableprice = Math.Round(((arrr * Convert.ToInt32(1)) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Totalprice"] = Convert.ToDouble(taxableamnt + tax).ToString("##,0");
                            dr["texableprice"] = Convert.ToDouble(taxableprice).ToString("##,0");
                        }
                        else if (Session["getbedconfig"].ToString() == "Triple")
                        {
                            Session["totpax"] = 3;
                            string TaxStatus = (view.ToTable().Rows[0][7].ToString());
                            string TaxValue = (view.ToTable().Rows[0][6].ToString());

                            arrWZ = view.ToTable().Rows[0][4].ToString().Split(' ');
                            dr["CRPrice"] = arrWZ[0].ToString() + " " + (arrrtx * Convert.ToInt32(3)).ToString("##,0");
                            dr["Price"] = Convert.ToDouble(arrrtx * Convert.ToInt32(3)).ToString("##,0");
                            /* dr["Tax"] = 0;*/// Convert.ToDecimal(TaxValue.ToString());     
                            dr["pricewithouttax"] = arrr.ToString("##,0");
                            dr["pricewithouttax1"] = arrr.ToString("##,0");
                            dr["Total"] = Convert.ToDouble(arrr * 3).ToString("##,0");
                            double getdiscountamount = (Convert.ToDouble(arrr * 3) * (3 * Convert.ToDouble(Session["getdiscountvalue"].ToString()))) / 100;
                            dr["taxablepamt"] = (Convert.ToDouble(arrr * 3) - getdiscountamount).ToString("##,0");
                            double taxableamnt = (Convert.ToDouble(arrr * 3) - getdiscountamount);
                            dr["Discountprice"] = getdiscountamount.ToString("##,0");

                            double tax = Math.Round(((taxableamnt) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Tax1"] = Convert.ToDouble(tax).ToString("##,0");
                            double taxableprice = Math.Round(((arrr * Convert.ToInt32(1)) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Totalprice"] = Convert.ToDouble(taxableamnt + tax).ToString("##,0");
                            dr["texableprice"] = Convert.ToDouble(taxableprice).ToString("##,0");
                        }
                        else
                        {
                            Session["totpax"] = 2;
                            string TaxStatus = (view.ToTable().Rows[0][7].ToString());
                            string TaxValue = (view.ToTable().Rows[0][6].ToString());

                            arrWZ = view.ToTable().Rows[0][4].ToString().Split(' ');
                            dr["CRPrice"] = arrWZ[0].ToString() + " " + (arrr1tx * Convert.ToInt32(2)).ToString("##,0");
                            dr["Price"] = Convert.ToDouble(arrr1tx * Convert.ToInt32(2)).ToString("##,0");
                            /* dr["Tax"] = 0;*/// Convert.ToDecimal(TaxValue.ToString());      
                            dr["pricewithouttax"] = arrr1.ToString("##,0");
                            dr["pricewithouttax1"] = arrr1.ToString("##,0");
                            dr["Total"] = Convert.ToDouble(arrr1 * Convert.ToInt32(2)).ToString("##,0");
                            double getdiscountamount = (Convert.ToDouble(arrr1 * 2) * (2 * Convert.ToDouble(Session["getdiscountvalue"].ToString()))) / 100;
                            dr["taxablepamt"] = (Convert.ToDouble(arrr1 * 2) - getdiscountamount).ToString("##,0");
                            double taxableamnt = (Convert.ToDouble(arrr1 * 2) - getdiscountamount);
                            dr["Discountprice"] = getdiscountamount.ToString("##,0");

                            double tax = Math.Round(((taxableamnt) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Tax1"] = Convert.ToDouble(tax).ToString("##,0");
                            double taxableprice = Math.Round(((arrr1 * Convert.ToInt32(1)) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Totalprice"] = Convert.ToDouble(taxableamnt + tax).ToString("##,0");
                            dr["texableprice"] = Convert.ToDouble(taxableprice).ToString("##,0");
                        }
                    }
                    else
                    {
                        Session["totpax"] = 2;
                        string TaxStatus = (view.ToTable().Rows[0][7].ToString());
                        string TaxValue = (view.ToTable().Rows[0][6].ToString());

                        arrWZ = view.ToTable().Rows[0][4].ToString().Split(' ');
                        dr["CRPrice"] = arrWZ[0].ToString() + " " + (arrr1tx * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())).ToString("##,0");
                        dr["Price"] = Convert.ToDouble(arrr1tx * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())).ToString("##,0");
                        /* dr["Tax"] = 0;*/// Convert.ToDecimal(TaxValue.ToString());    
                        dr["pricewithouttax"] = arrr1.ToString("##,0");
                        dr["pricewithouttax1"] = arrr1.ToString("##,0");
                        dr["Total"] = Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())).ToString("##,0");
                        //double getdiscountamount = (Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) * (Convert.ToInt32(view.ToTable().Rows[0][10].ToString()) * Convert.ToDouble(Session["getdiscountvalue"].ToString()))) / 100;
                        double getdiscountamount = (Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) * (Convert.ToDouble(Session["getdiscountvalue"].ToString()))) / 100;
                        dr["taxablepamt"] = (Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) - getdiscountamount).ToString("##,0");
                        double taxableamnt = (Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) - getdiscountamount);
                        dr["Discountprice"] = getdiscountamount.ToString("##,0");

                        double tax = Math.Round(((taxableamnt) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                        dr["Tax1"] = Convert.ToDouble(tax).ToString("##,0");
                        double taxableprice = Math.Round(((arrr1 * Convert.ToInt32(1)) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                        dr["Totalprice"] = Convert.ToDouble(taxableamnt + tax).ToString("##,0");
                        dr["texableprice"] = Convert.ToDouble(taxableprice).ToString("##,0");
                    }

                }
                else
                {
                    if (Session["getbedconfig"] != null)
                    {
                        if (Session["getbedconfig"].ToString() == "Single")
                        {
                            Session["totpax"] = 1;
                            string TaxStatus = (view.ToTable().Rows[0][7].ToString());
                            string TaxValue = (view.ToTable().Rows[0][6].ToString());

                            arrWZ = view.ToTable().Rows[0][5].ToString().Split(' ');
                            dr["CRPrice"] = arrWZ[0].ToString() + " " + Convert.ToDouble(arrrtx).ToString("##,0");
                            dr["Price"] = Convert.ToDouble(arrrtx).ToString("##,0");
                            /* dr["Tax"] = 0;*/// Convert.ToDecimal(TaxValue.ToString()); 
                            dr["pricewithouttax"] = arrr.ToString("##,0");
                            dr["pricewithouttax1"] = arrr.ToString("##,0");
                            dr["Total"] = Convert.ToDouble(arrr * Convert.ToInt32(1)).ToString("##,0");
                            double getdiscountamount = (Convert.ToDouble(arrr * 1) * (1 * Convert.ToDouble(Session["getdiscountvalue"].ToString()))) / 100;
                            dr["taxablepamt"] = (Convert.ToDouble(arrr * 1) - getdiscountamount).ToString("##,0");
                            double taxableamnt = (Convert.ToDouble(arrr * 1) - getdiscountamount);
                            dr["Discountprice"] = getdiscountamount.ToString("##,0");

                            double tax = Math.Round(((taxableamnt) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Tax1"] = Convert.ToDouble(tax).ToString("##,0");
                            double taxableprice = Math.Round(((arrr * Convert.ToInt32(1)) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Totalprice"] = Convert.ToDouble(taxableamnt + tax).ToString("##,0");
                            dr["texableprice"] = Convert.ToDouble(taxableprice).ToString("##,0");
                        }
                        else if (Session["getbedconfig"].ToString() == "Triple")
                        {
                            Session["totpax"] = 3;
                            string TaxStatus = (view.ToTable().Rows[0][7].ToString());
                            string TaxValue = (view.ToTable().Rows[0][6].ToString());

                            arrWZ = view.ToTable().Rows[0][5].ToString().Split(' ');
                            dr["CRPrice"] = arrWZ[0].ToString() + " " + Convert.ToDouble(arrrtx).ToString("##,0");
                            dr["Price"] = Convert.ToDouble(arrrtx).ToString("##,0");
                            /* dr["Tax"] = 0;*/// Convert.ToDecimal(TaxValue.ToString()); 
                            dr["pricewithouttax"] = arrr.ToString("##,0");
                            dr["pricewithouttax1"] = arrr.ToString("##,0");
                            dr["Total"] = Convert.ToDouble(arrr * Convert.ToInt32(3)).ToString("##,0");
                            double getdiscountamount = (Convert.ToDouble(arrr * 3) * (3 * Convert.ToDouble(Session["getdiscountvalue"].ToString()))) / 100;
                            dr["taxablepamt"] = (Convert.ToDouble(arrr * 3) - getdiscountamount).ToString("##,0");
                            double taxableamnt = (Convert.ToDouble(arrr * 3) - getdiscountamount);
                            dr["Discountprice"] = getdiscountamount.ToString("##,0");

                            double tax = Math.Round(((taxableamnt) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Tax1"] = Convert.ToDouble(tax).ToString("##,0");
                            double taxableprice = Math.Round(((arrr * Convert.ToInt32(1)) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Totalprice"] = Convert.ToDouble(taxableamnt + tax).ToString("##,0");
                            dr["texableprice"] = Convert.ToDouble(taxableprice).ToString("##,0");
                        }
                        else
                        {
                            Session["totpax"] = 2;
                            string TaxStatus = (view.ToTable().Rows[0][7].ToString());
                            string TaxValue = (view.ToTable().Rows[0][6].ToString());

                            arrWZ = view.ToTable().Rows[0][5].ToString().Split(' ');
                            dr["CRPrice"] = arrWZ[0].ToString() + " " + Convert.ToDouble(arrr1tx).ToString("##,0");
                            dr["Price"] = Convert.ToDouble(arrr1tx).ToString("##,0");
                            /* dr["Tax"] = 0;*/// Convert.ToDecimal(TaxValue.ToString()); 
                            dr["pricewithouttax"] = arrr1.ToString("##,0");
                            dr["pricewithouttax1"] = arrr1.ToString("##,0");
                            dr["Total"] = Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())).ToString("##,0");
                            double getdiscountamount = (Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) * (Convert.ToInt32(view.ToTable().Rows[0][10].ToString()) * Convert.ToDouble(Session["getdiscountvalue"].ToString()))) / 100;
                            dr["taxablepamt"] = (Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) - getdiscountamount).ToString("##,0");
                            double taxableamnt = (Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) - getdiscountamount);
                            dr["Discountprice"] = getdiscountamount.ToString("##,0");

                            double tax = Math.Round(((taxableamnt) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Tax1"] = Convert.ToDouble(tax).ToString("##,0");
                            double taxableprice = Math.Round(((arrr1 * Convert.ToInt32(1)) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                            dr["Totalprice"] = Convert.ToDouble(taxableamnt + tax).ToString("##,0");
                            dr["texableprice"] = Convert.ToDouble(taxableprice).ToString("##,0");
                        }
                    }
                    else

                    {
                        Session["totpax"] = 2;
                        string TaxStatus = (view.ToTable().Rows[0][7].ToString());
                        string TaxValue = (view.ToTable().Rows[0][6].ToString());

                        arrWZ = view.ToTable().Rows[0][5].ToString().Split(' ');
                        dr["CRPrice"] = arrWZ[0].ToString() + " " + Convert.ToDouble(arrr1tx).ToString("##,0");
                        dr["Price"] = Convert.ToDouble(arrr1tx).ToString("##,0");
                        /* dr["Tax"] = 0;*/// Convert.ToDecimal(TaxValue.ToString());
                        dr["pricewithouttax"] = arrr1.ToString("##,0");
                        dr["pricewithouttax1"] = arrr1.ToString("##,0");
                        dr["Total"] = Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())).ToString("##,0");
                        double getdiscountamount = (Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) * (Convert.ToInt32(view.ToTable().Rows[0][10].ToString()) * Convert.ToDouble(Session["getdiscountvalue"].ToString()))) / 100;
                        dr["taxablepamt"] = (Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) - getdiscountamount).ToString("##,0");
                        double taxableamnt = (Convert.ToDouble(arrr1 * Convert.ToInt32(view.ToTable().Rows[0][10].ToString())) - getdiscountamount);
                        dr["Discountprice"] = getdiscountamount.ToString("##,0");

                        double tax = Math.Round(((taxableamnt) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                        dr["Tax1"] = Convert.ToDouble(tax).ToString("##,0");
                        double taxableprice = Math.Round(((arrr1 * Convert.ToInt32(1)) * Convert.ToDouble(view.ToTable().Rows[0][6].ToString().Split('R')[1])) / 100);
                        dr["Totalprice"] = Convert.ToDouble(taxableamnt + tax).ToString("##,0");
                        dr["texableprice"] = Convert.ToDouble(taxableprice).ToString("##,0");
                    }

                }

                int Counter = 0;
                foreach (DataRow dr1 in dtInsertable.Rows)
                {
                    Counter++;
                    if (dr1["RoomNumber"].ToString() == hfRoomId.Value)
                    {
                        dr1.Delete();
                        hfRoomId.Value = "";
                        break;
                    }
                    else
                    {
                        //do nothing
                    }
                }
                int i = 0;
                //if (Session["getroowindex"] != null)
                //{
                //    try
                //    {
                //        i = Convert.ToInt32(Session["getroowindex"].ToString());
                //        dtInsertable.Rows[i].Delete();
                //        Session["getroowindex"] = null;
                //    }
                //    catch { }
                //}

                if (Counter > 0)
                {

                    dtInsertable.AcceptChanges();
                    if (Session["getroowindex"] != null)
                    {
                        dtInsertable.Rows.InsertAt(dr, Convert.ToInt32(Session["getroowindex"].ToString()));
                        Session["getroowindex"] = null;
                    }
                    else
                    {
                        dtInsertable.Rows.Add(dr);
                    }

                }
                else
                    dtInsertable.Rows.Add(dr);

                ViewState["VsRoomDetails"] = dtInsertable;

                GridRoomPaxDetail.DataSource = dtInsertable;
                GridRoomPaxDetail.DataBind();
                //hfRoomId.Value = null;

            }
            if (GridRoomPaxDetail.Rows.Count > 0)
            {
                ButtonsDiv.Style.Remove("display");
                GridRoomPaxDetail.Columns[0].Visible = true;
            }
            else
            {
                ButtonsDiv.Style.Add("display", "none");
            }
            if (Session["getdiscountvalue"] != null)
            {
                if (Session["getdiscountvalue"].ToString() == "0")
                {
                    GridRoomPaxDetail.Columns[6].Visible = false;
                    GridRoomPaxDetail.Columns[7].Visible = false;


                }
            }
            Session["getpax"] = txtPassengers.Text.ToString();
        }
        catch (Exception EX)
        {

        }
    }
    protected void ddlpax_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["totpax"] = null;

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Totamt = Totamt + Convert.ToDouble(e.Row.Cells[2].Text);

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = Totamt.ToString();

            }
        }

        catch
        {
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Session["Rrate"] = null;
        Session["totpax"] = null;
        ViewState["VsRoomDetails"] = null;
        Session["BookedRooms"] = null;
        gdvRoomCategories.DataSource = null;
        gdvRoomCategories.DataBind();
        GridRoomPaxDetail.DataSource = null;
        GridRoomPaxDetail.DataBind();
        Response.Redirect(Request.RawUrl);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (GridRoomPaxDetail.Rows.Count > 0)
            {
                #region Inserting Booking Data
                Session["cruiseBookingUrl"] = Request.Url.ToString();

                DataTable RoomDetails = ViewState["VsRoomDetails"] as DataTable;
                SessionServices.SaveSession<DataTable>("BookedRooms", RoomDetails);
                //Session["BookedRooms"] = RoomDetails;

                LockTheBooking(RoomDetails);

                ///    
                //Response.Redirect("sendtoairpay.aspx?BookedId=" + BookedId + "&PackName=" + Request.QueryString["PackageName"].ToString() + "&NoOfNights=" + Request.QueryString["NoOfNights"].ToString() + "&CheckinDate=" + Request.QueryString["CheckinDate"].ToString());
                if (Session["Redirecturl"] == null)
                {
                    if (Convert.ToInt32(RoomDetails.Compute("SUM(Pax)", string.Empty)) >= Convert.ToInt32(Session["totpax"]))
                    {
                        string Redirecturl = "SummarizedDetails1.aspx?BookedId=" + BookedId + "&PackName=" + Request.QueryString["PackageName"].ToString() + "&NoOfNights=" + Request.QueryString["NoOfNights"].ToString() + "&CheckinDate=" + Request.QueryString["CheckInDate"].ToString() + "&CheckOutdate=" + Request.QueryString["CheckOutDate"].ToString() + "&Discount=" + Request.QueryString["Discount"].ToString() + "&PackId=" + Session["PackageId"].ToString() + "&DepartureId=" + Request.QueryString["DepartureId"].ToString();
                        Session["Redirecturl"] = Redirecturl;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('You have not selected enough Rooms to Accomodate all Guests ')", true);
                        return;
                    }
                }
                Response.Redirect(Session["Redirecturl"].ToString());
                #endregion
            }
            else
            {
                pMessages.InnerText = "No rooms selected.";
            }
        }
        catch (Exception sqe)
        {
            throw sqe;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }
    #endregion

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        System.Web.Security.FormsAuthentication.SignOut();
        Response.Redirect("SearchProperty1.aspx");
    }

    protected void GridRoomPaxDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridRoomPaxDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            try
            {
                ImageButton imgbtn = (ImageButton)e.CommandSource;
                GridViewRow grow = (GridViewRow)imgbtn.NamingContainer;
                DataTable dtnew = ViewState["VsRoomDetails"] as DataTable;
                dtnew.Rows.RemoveAt(grow.RowIndex);
                dtnew.AcceptChanges();
                ViewState["VsRoomDetails"] = dtnew;
                SessionServices.SaveSession<DataTable>("BookedRooms", dtnew);
                //Session["BookedRooms"] = dtnew;
                GridRoomPaxDetail.DataSource = dtnew;
                GridRoomPaxDetail.DataBind();
                calculateTotal();

            }
            catch
            {
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Session["DepartureSearchUrl"].ToString());
    }
    protected void GridRoomPaxDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable getcat = new DataTable();
                string getvalue = "";
                if (ViewState["VsRoomDetails"] != null)
                {
                    getcat = ViewState["VsRoomDetails"] as DataTable;
                }
                var ddl = (DropDownList)e.Row.FindControl("ddlbedconfiguration");
                var ddlrcm = (DropDownList)e.Row.FindControl("ddlCategoryType");
                DataSet ds = rtypemaster.GetallData();
                DataTable dt = ds.Tables[0];
                DataTable dt12 = Session["Getroomcatid"] as DataTable;

                try
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ddl.DataSource = dt;
                        ddl.DataValueField = "RoomTypeId";
                        ddl.DataTextField = "RoomType";
                        ddl.DataBind();
                        ddl.Items.Insert(0, "-Select-");
                        if (dt12 != null && dt12.Rows.Count > 0)
                        {
                            if (dt12.Rows[0]["RmType"].ToString() != "Triple")
                            {

                                ddl.Items.FindByValue("3").Enabled = false;
                                //ddl.Items[3].Attributes["disabled"] = "disabled";
                            }
                            else
                            {
                                ddl.Items.FindByValue("3").Enabled = true;
                                //ddl.Items[3].Attributes["enabled"] = "enabled";
                            }
                        }
                        try
                        {
                            if (e.Row.RowIndex == 0)
                            {
                                ddl.Items.FindByText(getcat.Rows[0]["RoomType"].ToString()).Selected = true;
                            }
                        }
                        catch { }
                        try
                        {
                            if (e.Row.RowIndex == 1)
                            {
                                ddl.Items.FindByText(getcat.Rows[1]["RoomType"].ToString()).Selected = true;
                            }
                        }
                        catch { }
                        try
                        {
                            if (e.Row.RowIndex == 2)
                            {
                                ddl.Items.FindByText(getcat.Rows[2]["RoomType"].ToString()).Selected = true;
                            }
                        }
                        catch { }
                        try
                        {
                            if (e.Row.RowIndex == 3)
                            {
                                ddl.Items.FindByText(getcat.Rows[3]["RoomType"].ToString()).Selected = true;
                            }
                        }
                        catch { }
                        try
                        {
                            if (e.Row.RowIndex == 4)
                            {
                                ddl.Items.FindByText(getcat.Rows[4]["RoomType"].ToString()).Selected = true;
                            }
                        }
                        catch { }
                        try
                        {
                            if (e.Row.RowIndex == 5)
                            {
                                ddl.Items.FindByText(getcat.Rows[5]["RoomType"].ToString()).Selected = true;
                            }
                        }
                        catch { }
                        try
                        {
                            if (e.Row.RowIndex == 6)
                            {
                                ddl.Items.FindByText(getcat.Rows[6]["RoomType"].ToString()).Selected = true;
                            }
                        }
                        catch { }

                        if (Session["GetroomType1"] == null)
                        {

                        }
                        else
                        {

                        }
                        SessionServices.SaveSession("savebadcon", ddl.SelectedValue.ToString());

                    }

                }
                catch (Exception ex)
                { }
                try
                {

                    DataSet ds1 = rcmaster.GetallData();
                    DataTable dt1 = Session["Getroomcatid"] as DataTable;
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        ddlrcm.DataSource = dt1;
                        ddlrcm.DataValueField = "roomcategoryid";
                        ddlrcm.DataTextField = "Cabin Category";
                        ddlrcm.DataBind();
                        ddlrcm.Items.Insert(0, "-Select-");
                        try
                        {
                            if (e.Row.RowIndex == 0)
                            {
                                getvalue = getcat.Rows[0]["RoomCategoryId"].ToString();
                                ddlrcm.Items.FindByValue(getvalue).Selected = true;
                            }
                        }
                        catch { }
                        try
                        {
                            if (e.Row.RowIndex == 1)
                            {
                                ddlrcm.Items.FindByValue(getcat.Rows[1]["RoomCategoryId"].ToString()).Selected = true;
                            }
                        }
                        catch { }
                        try
                        {
                            if (e.Row.RowIndex == 2)
                            {
                                ddlrcm.Items.FindByValue(getcat.Rows[2]["RoomCategoryId"].ToString()).Selected = true;
                            }
                        }
                        catch { }
                        try
                        {
                            if (e.Row.RowIndex == 3)
                            {
                                ddlrcm.Items.FindByValue(getcat.Rows[3]["RoomCategoryId"].ToString()).Selected = true;
                            }
                        }
                        catch { }
                        try
                        {
                            if (e.Row.RowIndex == 4)
                            {
                                ddlrcm.Items.FindByValue(getcat.Rows[4]["RoomCategoryId"].ToString()).Selected = true;
                            }
                        }
                        catch { }
                        try
                        {
                            if (e.Row.RowIndex == 5)
                            {
                                ddlrcm.Items.FindByValue(getcat.Rows[5]["RoomCategoryId"].ToString()).Selected = true;
                            }
                        }
                        catch { }
                        try
                        {
                            if (e.Row.RowIndex == 6)
                            {
                                ddlrcm.Items.FindByValue(getcat.Rows[6]["RoomCategoryId"].ToString()).Selected = true;
                            }
                        }
                        catch { }
                        if (Session["Getcateid1"] == null)
                        {

                        }
                        else
                        {

                        }
                    }
                }
                catch (Exception ex) { }
            }
        }

        catch
        {
        }

    }
    protected void txtPassengers_TextChanged(object sender, EventArgs e)
    {
        Session["totpax"] = null;
    }
    private void loadall()
    {
        Session["totpax"] = txtPassengers.Text.ToString();
        string get = Request.QueryString["CheckIndate"].ToString();
        if (Request.QueryString["CheckIndate"].ToString() != "")
        {
            Session["checkin"] = Request.QueryString["CheckIndate"].ToString();
        }
        BindCruiseRoomRates();
        //if (GridRoomPaxDetail.Rows.Count > 0)
        //{
        //    ButtonsDiv.Style.Remove("display");
        //}
        //else
        //load();
        DataTable dt = new DataTable();
        DataColumn dc;
        dc = new DataColumn();
        dc.DataType = Type.GetType("System.String");
        dc.ColumnName = "RoomNumber";
        dt.Columns.Add(dc);

        //dc = new DataColumn();
        //dc.DataType = Type.GetType("System.Int32");
        //dc.ColumnName = "RoomCategoryId";
        //dt.Columns.Add(dc);

        // Create second column.
        dc = new DataColumn();
        dc.DataType = Type.GetType("System.String");
        dc.ColumnName = "Pax";
        dt.Columns.Add(dc);

        dc = new DataColumn();
        dc.DataType = Type.GetType("System.String");
        dc.ColumnName = "Currency";
        dt.Columns.Add(dc);

        dc = new DataColumn();
        dc.DataType = Type.GetType("System.String");
        dc.ColumnName = "CRPrice";
        dt.Columns.Add(dc);

        dc = new DataColumn();
        dc.DataType = Type.GetType("System.String");
        dc.ColumnName = "Tax1";
        dt.Columns.Add(dc);

        dc = new DataColumn();
        dc.DataType = Type.GetType("System.String");
        dc.ColumnName = "Tax";
        dt.Columns.Add(dc);
        dc = new DataColumn();
        dc.DataType = Type.GetType("System.String");
        dc.ColumnName = "pricewithouttax1";
        dt.Columns.Add(dc);
        dc = new DataColumn();
        dc.DataType = Type.GetType("System.String");
        dc.ColumnName = "categoryName";
        dt.Columns.Add(dc);
        dc = new DataColumn();
        dc.DataType = Type.GetType("System.String");
        dc.ColumnName = "Total";
        dt.Columns.Add(dc);
        dc = new DataColumn();
        dc.DataType = Type.GetType("System.String");
        dc.ColumnName = "Discount";
        dt.Columns.Add(dc);
        dc = new DataColumn();
        dc.DataType = Type.GetType("System.String");
        dc.ColumnName = "Discountprice";
        dt.Columns.Add(dc);
        dc = new DataColumn();
        dc.DataType = Type.GetType("System.String");
        dc.ColumnName = "Totalprice";
        dt.Columns.Add(dc);
        dc = new DataColumn();
        dc.DataType = Type.GetType("System.String");
        dc.ColumnName = "texableprice";
        dt.Columns.Add(dc);
        dc = new DataColumn();
        dc.DataType = Type.GetType("System.String");
        dc.ColumnName = "taxablepamt";
        dt.Columns.Add(dc);
        DataRow dr = dt.NewRow();
        dr["Tax"] = " ";
        dt.Rows.Add(dr, 0);
        Session["blank"] = dt;
        if (dt != null && dt.Rows.Count > 0)
        {
            GridRoomPaxDetail.DataSource = dt;
            GridRoomPaxDetail.DataBind();
            GridRoomPaxDetail.Columns[0].Visible = false;
        }
        else
        {

        }
        DataTable bookedRooms = SessionServices.RetrieveSession<DataTable>("BookedRooms");
        if (bookedRooms != null)
        {
            if (packageid1 != "")

            {
                ViewState["VsRoomDetails"] = bookedRooms;
            }
            else
            {
                ViewState["VsRoomDetails"] = null;
            }




            GridRoomPaxDetail.DataSource = bookedRooms;
            GridRoomPaxDetail.DataBind();
            GridRoomPaxDetail.Columns[0].Visible = true;
            calculateTotal();
        }
        {
            ButtonsDiv.Style.Remove("display");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)

    {
        if (ViewState["VsRoomDetails"] != null)
        {

        }
        else
        {
            if (txtPassengers.Text == " ")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('Please enter occupacy')", true);
                return;
            }
            else
            {
                int count = 0;
                try
                {
                    count = Convert.ToInt32(txtPassengers.Text);

                    if (Convert.ToInt32(Session["getdefaultnoofbed"].ToString()) == 3)
                    {
                        if (Convert.ToInt32(txtPassengers.Text) <= 3)
                        {
                            Session["1"] = 1;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('occupacy should be equal or less than 3')", true);
                            return;
                        }
                    }
                    else if (Convert.ToInt32(Session["getdefaultnoofbed"].ToString()) < 3)
                    {
                        if (Convert.ToInt32(txtPassengers.Text) < 3)
                        {

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('occupacy should be less than 3')", true);
                            return;
                        }
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('Please enter valid occupacy')", true);
                    return;
                }
            }
            //Session["totpax"] = txtPassengers.Text;
            if (Convert.ToInt32(txtPassengers.Text) > 0)
            {
                Session["totpax"] = txtPassengers.Text.ToString();

                Session["checkin"] = Request.QueryString["CheckIndate"].ToString(); ;
                BindCruiseRoomRates();
                //if (GridRoomPaxDetail.Rows.Count > 0)
                //{
                //    ButtonsDiv.Style.Remove("display");
                //}
                //else
                //load();
                DataTable dt = new DataTable();
                DataColumn dc;
                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "RoomNumber";
                dt.Columns.Add(dc);

                //dc = new DataColumn();
                //dc.DataType = Type.GetType("System.Int32");
                //dc.ColumnName = "RoomCategoryId";
                //dt.Columns.Add(dc);

                // Create second column.
                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "Pax";
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "Currency";
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "CRPrice";
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "Tax1";
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "Tax";
                dt.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = "pricewithouttax1";
                dt.Columns.Add(dc);


                DataRow dr = dt.NewRow();
                dr["Tax"] = " ";
                dt.Rows.Add(dr, 0);
                Session["blank"] = dt;
                if (dt != null && dt.Rows.Count > 0)
                {
                    GridRoomPaxDetail.DataSource = dt;
                    GridRoomPaxDetail.DataBind();
                    GridRoomPaxDetail.Columns[0].Visible = false;
                }
                else
                {

                }
                {
                    ButtonsDiv.Style.Remove("display");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('Please select occupacy')", true);
                return;
            }
        }
    }
    public void BindCruiseRoomRates()
    {

        try
        {
            // bindroomddl();
            bindRoomRates();
            setImageMap();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "blockArea", "blockArea();", true);
        }

        catch
        {
        }
    }
    public void load()

    {
        try
        {
            if (gdvRoomCategories.Rows.Count > 1)
            {
                dt = SessionServices.RetrieveSession<DataTable>("Rrate");
                #region getRoomCategory
                blsr.action = "GetRoomCateId";

                //hfRoomId.Value = e.PostBackValue.ToString();
                blsr.RoomId = hfRoomId.Value;
                blsr.PackageId = Session["PackageId"].ToString();

                roomCatId = dlsr.getRoomCategory(blsr);
                #endregion
                Int32.TryParse(txtPassengers.Text, out irpax);
                dv = new DataView();
                dv = new DataView(dt, "roomcategoryid='" + roomCatId + "'", "roomcategoryid", DataViewRowState.CurrentRows);
                if (Convert.ToInt32(txtPassengers.Text) > 0)
                {
                    blsr._iAgentId = Session["UserCode"] != null ? Convert.ToInt32(Session["UserCode"].ToString()) : 247;
                    blsr.action = "Getmaxrooms";
                    blsr._dtStartDate = Convert.ToDateTime(Session["checkin"]);
                    dtGetReturnedData = dlsr.getMaxRoomsBookable(blsr);
                    if (dtGetReturnedData != null)
                    {
                        if (GridRoomPaxDetail.Rows.Count < Convert.ToInt32(dtGetReturnedData.Rows[0][0]))
                        {
                            try
                            {
                                lblCurr.Text = dv.ToTable().Rows[0]["Currency"].ToString();
                            }
                            catch
                            { }
                            addrows(dv, roomCatId);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('You cannot book any more rooms. Please contact our reservations office to make additional bookings.')", true);
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('No data found for this room,try selecting Passengers first')", true);
            }
        }
        catch
        {

        }
    }
    protected void ImageMap1_Click(object sender, ImageMapEventArgs e)
    {
        try
        {
            if (gdvRoomCategories.Rows.Count > 1)
            {
                dt = SessionServices.RetrieveSession<DataTable>("Rrate");
                //dt = Session["Rrate"] as DataTable;
                #region getRoomCategory
                blsr.action = "GetRoomCateId";

                hfRoomId.Value = e.PostBackValue.ToString();
                blsr.RoomId = hfRoomId.Value;
                blsr.PackageId = Session["PackageId"].ToString();

                roomCatId = dlsr.getRoomCategory(blsr);
                #endregion
                Int32.TryParse(txtPassengers.Text, out irpax);
                dv = new DataView();
                dv = new DataView(dt, "roomcategoryid='" + roomCatId + "'", "roomcategoryid", DataViewRowState.CurrentRows);
                if (Session["Getcateid"] == null)
                {
                    Session["Getcateid"] = roomCatId.ToString();
                }
                else if (Session["Getcateid1"] == null)
                {
                    Session["Getcateid1"] = roomCatId.ToString();
                }
                else if (Session["Getcateid2"] == null)
                {
                    Session["Getcateid2"] = roomCatId.ToString();
                }
                else if (Session["Getcateid3"] == null)
                {
                    Session["Getcateid3"] = roomCatId.ToString();
                }
                else if (Session["Getcateid4"] == null)
                {
                    Session["Getcateid4"] = roomCatId.ToString();
                }
                DataTable dtget = dv.ToTable();
                if (Session["GetroomType"] == null)
                {
                    Session["GetroomType"] = dtget.Rows[0]["RmType"].ToString();
                }
                else if (Session["GetroomType1"] == null)
                {
                    Session["GetroomType1"] = dtget.Rows[0]["RmType"].ToString();
                }

                blsr._iAgentId = Session["UserCode"] != null ? Convert.ToInt32(Session["UserCode"].ToString()) : 247;
                blsr.action = "Getmaxrooms";
                blsr._dtStartDate = Convert.ToDateTime(Session["checkin"]);
                dtGetReturnedData = dlsr.getMaxRoomsBookable(blsr);
                if (dtGetReturnedData != null)
                {
                    if (GridRoomPaxDetail.Rows.Count < Convert.ToInt32(dtGetReturnedData.Rows[0][0]))
                    {
                        lblCurr.Text = dv.ToTable().Rows[0]["Currency"].ToString();
                        addrows(dv, roomCatId);
                        lblmessage.Text = "cabin no "+ hfRoomId.Value + " reserved successfully";
                        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>disp_confirm()</script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('You cannot book any more rooms. Please contact our reservations office to make additional bookings.')", true);
                    }
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('No data found for this room,try selecting Passengers first')", true);
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void LockTheBooking(DataTable roomDetails)
    {
        try
        {
            int lockDuration = ConfigurationManager.AppSettings["LockDuration"] != null ? Convert.ToInt16(ConfigurationManager.AppSettings["LockDuration"]) : 10;
            BALBookingLock bl = new BALBookingLock();

            int accomId = Session["AccomId"] != null ? Convert.ToInt16(Session["AccomId"]) : 7;
            Session["AccomId"] = accomId;
            Guid uniqueIdentifier = Guid.NewGuid();

            bl.AccomId = accomId;
            bl.LockIdentifier = uniqueIdentifier.ToString();
             bl.LockExpireAt = DateTime.Now.AddMinutes(lockDuration);
         //   bl.LockExpireAt = System.DateTime.Now;
            bl.LockRooms = new List<LockRoom>();

            foreach (DataRow row in roomDetails.Rows)
            {
                LockRoom lr = new LockRoom { RoomCategoryId = Convert.ToInt16(row["RoomCategoryId"]), RoomNo = row["RoomNumber"].ToString() };
                bl.LockRooms.Add(lr);
            }
            DALBookingLock dbl = new DALBookingLock();
            dbl.PlaceLock(bl);

            SessionServices.SaveSession<BALBookingLock>("BookingLock", bl);
            //Session["BookingLock"] = bl;
        }
        catch (Exception exp)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LockError", "javascript:alert('" + exp.Message + "')", true);
            throw exp;
        }
    }

    protected void GridRoomPaxDetail_DataBound(object sender, EventArgs e)
    {

    }

    protected void GridRoomPaxDetail_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void FillAgents()
    {
        try
        {
            AgentMaster oAgentMaster = new AgentMaster();
            blsr._iAgentId = Session["UserCode"] != null ? Convert.ToInt32(Session["UserCode"].ToString()) : 247;
            AgentDTO[] oAgentData = oAgentMaster.GetData1(blsr._iAgentId);
            ListItemCollection li = new ListItemCollection();
            ListItem l = new ListItem("Select Ref Agent", "0");
            ddlAgentRef.Items.Insert(0, l);
            if (oAgentData != null)
            {
                for (int i = 0; i < oAgentData.Length; i++)
                {
                    l = new ListItem(oAgentData[i].AgentName.ToString(), oAgentData[i].AgentId.ToString());
                    ddlAgentRef.Items.Insert(i + 1, l);
                }
            }
        }
        catch { ddlAgentRef.Visible = false; }
    }
    protected void ddlAgentRef_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindRoomRates();
    }
    protected void ddlbedconfiguration_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (gdvRoomCategories.Rows.Count > 1)
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow grow = (GridViewRow)ddl.NamingContainer;

                Session["getroowindex"] = grow.RowIndex;
                Session["getbedconfig"] = ddl.SelectedItem.ToString();
                if (grow.RowIndex == 0)
                {
                    Session["GetroomType1"] = ddl.SelectedItem.ToString();
                }
                if (grow.RowIndex == 1)
                {
                    Session["GetroomType2"] = ddl.SelectedItem.ToString();
                }
                if (grow.RowIndex == 2)
                {
                    Session["GetroomType3"] = ddl.SelectedItem.ToString();
                }
                if (grow.RowIndex == 3)
                {
                    Session["GetroomType4"] = ddl.SelectedItem.ToString();
                }
                if (grow.RowIndex == 4)
                {
                    Session["GetroomType5"] = ddl.SelectedItem.ToString();
                }
                ViewState["dt"] = null;
                DataTable dt1 = SessionServices.RetrieveSession<DataTable>("Rrate");
                //DataTable dt1 = Session["Rrate"] as DataTable;
                #region getRoomCategory
                blsr.action = "GetRoomCateId";
                string jgkj = "";
                //hfRoomId.Value = e.PostBackValue.ToString();
                //blsr.RoomId = hfRoomId.Value;
                //blsr.PackageId = Session["PackageId"].ToString();
                string rmtype = "";
                foreach (GridViewRow row in GridRoomPaxDetail.Rows)
                {
                    if (row.RowIndex == grow.RowIndex)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {

                            DropDownList StopButton = (DropDownList)row.FindControl("ddlCategoryType");
                            DropDownList StopButton1 = (DropDownList)row.FindControl("ddlbedconfiguration");
                            Label getroomno = (Label)row.FindControl("RoomId");
                            hfRoomId.Value = getroomno.Text;
                            if (StopButton.SelectedIndex > 0)
                            {

                            }
                            else
                            {
                                if (Convert.ToInt32(dt.Rows[0]["DefaultNoOfBeds"].ToString()) >= 3)
                                {

                                }
                                else
                                {
                                    StopButton1.Items.FindByValue("3").Enabled = false;
                                }
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('Please select category first')", true);
                                return;
                            }

                            roomCatId = Convert.ToInt32(StopButton.SelectedValue.ToString());

                            rmtype = StopButton1.SelectedItem.ToString();
                            //Session["getbedconfig"] = StopButton1.SelectedItem.ToString();
                            Session["GetroomType"] = StopButton1.SelectedItem.ToString();
                            //if (roomCatId == 1)
                            //{
                            //    ddlpax.SelectedValue = "1";
                            //}
                            //if (roomCatId == 2)
                            //{
                            //    ddlpax.SelectedValue = "2";
                            //}
                            //if (roomCatId == 3)
                            //{
                            //    ddlpax.SelectedValue = "2";
                            //}
                            //if (roomCatId == 4)
                            //{
                            //    ddlpax.SelectedValue = "2";
                            //}

                            //ImageButton StartButton = (ImageButton)row.FindControl("startImageButton");


                        }
                    }
                }
                var uyktu = jgkj;


                //roomCatId = DD;
                #endregion
                Int32.TryParse(txtPassengers.Text, out irpax);
                dv = new DataView();

                dv = new DataView(dt1, "roomcategoryid='" + roomCatId + "'", "roomcategoryid", DataViewRowState.CurrentRows);
                //dv.RowFilter = "RmType = '" + rmtype + "'";
                if (dv != null && dv.Count >= 1)
                {

                    blsr._iAgentId = Session["UserCode"] != null ? Convert.ToInt32(Session["UserCode"].ToString()) : 247;
                    blsr.action = "Getmaxrooms";
                    blsr._dtStartDate = Convert.ToDateTime(Session["checkin"]);
                    dtGetReturnedData = dlsr.getMaxRoomsBookable(blsr);
                    if (dtGetReturnedData != null)
                    {
                        if (GridRoomPaxDetail.Rows.Count < Convert.ToInt32(dtGetReturnedData.Rows[0][0]))
                        {
                            //ViewState["VsRoomDetails"] = null;
                            lblCurr.Text = dv.ToTable().Rows[0]["Currency"].ToString();
                            addrows(dv, roomCatId);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('You cannot book any more rooms. Please contact our reservations office to make additional bookings.')", true);
                        }
                    }

                }
                else
                {
                    DataTable dt = new DataTable();
                    if (ViewState["VsRoomDetails"] != null)
                    {
                        dt = ViewState["VsRoomDetails"] as DataTable;
                    }
                    else
                    {
                        if (Session["blank"] != null)
                        {
                            dt = Session["blank"] as DataTable;
                        }
                    }
                    GridRoomPaxDetail.DataSource = dt;
                    GridRoomPaxDetail.DataBind();

                    if (Convert.ToInt32(dt.Rows[0]["DefaultNoOfBeds"].ToString()) >= 3)
                    {

                    }
                    else
                    {
                        ddlpax.Items.FindByValue("3").Enabled = false;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('Sorry,Not available at this time')", true);
                    return;
                }
            }
            else
            {
                DataTable dt = new DataTable();
                if (ViewState["VsRoomDetails"] != null)
                {
                    dt = ViewState["VsRoomDetails"] as DataTable;
                }
                else
                {
                    if (Session["blank"] != null)
                    {
                        dt = Session["blank"] as DataTable;
                    }
                }
                GridRoomPaxDetail.DataSource = dt;
                GridRoomPaxDetail.DataBind();
                if (Convert.ToInt32(dt.Rows[0]["DefaultNoOfBeds"].ToString()) >= 3)
                {

                }
                else
                {
                    ddlpax.Items.FindByValue("3").Enabled = false;
                }


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('No data found for this room,try selecting Passengers first')", true);
                return;
            }
        }
        catch
        {

        }
    }
    protected void ddlCategoryType_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (gdvRoomCategories.Rows.Count > 1)
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow grow = (GridViewRow)ddl.NamingContainer;
                Session["getroowindex"] = grow.RowIndex;
                Random rand = new Random();
                int getvalue = rand.Next(0, 2);
                if (grow.RowIndex == 0)
                {
                    Session["Getcateid"] = ddl.SelectedValue.ToString();
                }
                if (grow.RowIndex == 1)
                {
                    Session["Getcateid1"] = ddl.SelectedValue.ToString();
                }
                if (grow.RowIndex == 2)
                {
                    Session["Getcateid2"] = ddl.SelectedValue.ToString();
                }
                if (grow.RowIndex == 3)
                {
                    Session["Getcateid3"] = ddl.SelectedValue.ToString();
                }
                if (grow.RowIndex == 4)
                {
                    Session["Getcateid4"] = ddl.SelectedValue.ToString();
                }
                int getit = grow.RowIndex;
                ViewState["dt"] = null;
                DataTable dt1 = SessionServices.RetrieveSession<DataTable>("Rrate");
                #region getRoomCategory
                blsr.action = "GetRoomCateId";
                string jgkj = "";
                //hfRoomId.Value = e.PostBackValue.ToString();
                //blsr.RoomId = hfRoomId.Value;
                //blsr.PackageId = Session["PackageId"].ToString();
                foreach (GridViewRow row in GridRoomPaxDetail.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {

                        DropDownList StopButton = (DropDownList)row.FindControl("ddlCategoryType");
                        roomCatId = Convert.ToInt32(ddl.SelectedValue.ToString());
                        DataTable dtRoomsdata;

                        dtRoomsdata = bindroomddl();
                        DataView dv1 = dtRoomsdata.DefaultView;

                        dv1.RowFilter = "RoomCategory = '" + ddl.SelectedItem.ToString() + "' and BookedStatus = 'Available'";
                        {
                            hfRoomId.Value = (string)dv1[getvalue]["RoomNo"];
                        }
                        break;
                        //ImageButton StartButton = (ImageButton)row.FindControl("startImageButton");


                    }
                }
                var uyktu = jgkj;


                //roomCatId = DD;
                #endregion
                Int32.TryParse(txtPassengers.Text, out irpax);
                dv = new DataView();
                dv = new DataView(dt1, "roomcategoryid='" + roomCatId + "'", "roomcategoryid", DataViewRowState.CurrentRows);
                if (dv != null && dv.Count >= 1)
                {
                    if (Convert.ToInt32(txtPassengers.Text) > 0)
                    {
                        Session["getbedconfig"] = null;
                        DataTable dt123 = dv.ToTable();
                        Session["GetroomType"] = dt123.Rows[0]["RmType"].ToString();
                        Session["Getcateid"] = roomCatId;
                        blsr._iAgentId = Session["UserCode"] != null ? Convert.ToInt32(Session["UserCode"].ToString()) : 247;
                        blsr.action = "Getmaxrooms";
                        blsr._dtStartDate = Convert.ToDateTime(Session["checkin"]);
                        dtGetReturnedData = dlsr.getMaxRoomsBookable(blsr);
                        if (dtGetReturnedData != null)
                        {
                            if (GridRoomPaxDetail.Rows.Count < Convert.ToInt32(dtGetReturnedData.Rows[0][0]))
                            {
                                //ViewState["VsRoomDetails"] = null;
                                lblCurr.Text = dv.ToTable().Rows[0]["Currency"].ToString();
                                addrows(dv, roomCatId);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('You cannot book any more rooms. Please contact our reservations office to make additional bookings.')", true);
                            }
                        }
                    }
                }
                else
                {

                    DataTable dt = new DataTable();
                    if (ViewState["VsRoomDetails"] != null)
                    {
                        dt = ViewState["VsRoomDetails"] as DataTable;
                    }
                    else
                    {
                        if (Session["blank"] != null)
                        {
                            dt = Session["blank"] as DataTable;
                        }
                    }
                    GridRoomPaxDetail.DataSource = dt;
                    GridRoomPaxDetail.DataBind();

                    calculateTotal();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('No data found for this room,try selecting Passengers first')", true);
                    return;
                }
            }
            else
            {

                DataTable dt = new DataTable();
                if (ViewState["VsRoomDetails"] != null)
                {
                    dt = ViewState["VsRoomDetails"] as DataTable;
                }
                else
                {
                    if (Session["blank"] != null)
                    {
                        dt = Session["blank"] as DataTable;
                    }
                }
                GridRoomPaxDetail.DataSource = dt;
                GridRoomPaxDetail.DataBind();

                calculateTotal();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('No data found for this room,try selecting Passengers first')", true);
                return;
            }
        }
        catch (Exception ex)
        {

            DataTable dt = new DataTable();
            if (ViewState["VsRoomDetails"] != null)
            {
                dt = ViewState["VsRoomDetails"] as DataTable;
            }
            else
            {
                if (Session["blank"] != null)
                {
                    dt = Session["blank"] as DataTable;
                }
            }
            GridRoomPaxDetail.DataSource = dt;
            GridRoomPaxDetail.DataBind();

            calculateTotal();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('No data found for this category,try another category')", true);
            return;
        }
    }

    protected void GridRoomPaxDetail_SelectedIndexChanged1(object sender, EventArgs e)
    {
        GridViewRow row = GridRoomPaxDetail.SelectedRow;
        b = row.Cells[0].Text;
    }

    protected void ddlpax1rm_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}