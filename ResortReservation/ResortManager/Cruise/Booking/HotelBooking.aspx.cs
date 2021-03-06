﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;

public partial class Hotel_HotelBooking : System.Web.UI.Page
{


    BALBooking blsr = new BALBooking();
    DALBooking dlsr = new DALBooking();

    BALHotelBooking blht = new BALHotelBooking();
    DALHotelBooking dlht = new DALHotelBooking();
    int iAccomId = 0;
    int iaccomtypeid = 0;
    int maxpax = 0;
    DataTable Returndt;
    int roomCatId = 0;
    DataView dv;
    int irpax = 0;
    int roomtypeid = 0;
    double mTotal = 0;
    double rtotal = 0;
    int iagentid = 0;
    int Totpax = 0;

    int inights = 0;

    int noofrooms = 0;
    double totAmt = 0;



    DateTime chkin;
    DateTime chkout;
    protected void Page_Load(object sender, EventArgs e)
    {


        try
        {
            //txtChkin.Attributes.Add("onchange", "return fillEndDate('" + txtChkin.ClientID + "','" + txtChkOut.ClientID + "');");
            if (!IsPostBack)
            {



                if (Session["UserCode"] != null)
                {
                    LinkButton1.Visible = true;
                }
                else
                {
                    LinkButton1.Visible = false;
                }

                if (SessionServices.RetrieveSession<DataTable>("Bookingdt") == null)
                {
                    Session["AcId"] = Request.QueryString["AccomId"];
                    Session["Agid"] = Request.QueryString["AId"];
                    Session["Hpax"] = Request.QueryString["pax"];
                    Session["AccomTypeId"] = Request.QueryString["AccomTypeId"];
                    Session["HCheckin"] = Request.QueryString["Checkin"];
                    Session["HCheckout"] = Request.QueryString["Checkout"];
                    Session["HNoofrooms"] = Request.QueryString["Noofrooms"];
                }


                Int32.TryParse(Session["AcId"].ToString(), out iAccomId);
                Int32.TryParse(Session["Agid"].ToString(), out iagentid);
                Int32.TryParse(Session["Hpax"].ToString(), out Totpax);
                Int32.TryParse(Session["AccomTypeId"].ToString(), out iaccomtypeid);

                DateTime.TryParse(Session["HCheckin"].ToString(), out chkin);
                DateTime.TryParse(Session["HCheckout"].ToString().ToString(), out chkout);

                Int32.TryParse(Session["HNoofrooms"].ToString().ToString(), out noofrooms);

                DataTable bookingDt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
                if (bookingDt != null)
                {
                    ViewState["VsRoomDetails"] = bookingDt;
                    gdvSelectedRooms.DataSource = bookingDt;
                    gdvSelectedRooms.DataBind();
                    CalculateRoomRates();
                }

                if (gdvSelectedRooms.Rows.Count > 0)
                {
                    DivRmRate.Style.Remove("display");
                }
                else
                {
                    DivRmRate.Style.Add("display", "None");

                }
                this.bindRoomRates(iAccomId, Totpax, iagentid, chkin, chkout, noofrooms, 6);
                this.bindServiceRates(iAccomId);

                this.bindMealPlans(iAccomId, iagentid, Totpax);
                adddatestoddl(Convert.ToDateTime(Session["HCheckin"].ToString().ToString()), Convert.ToDateTime(Session["HCheckout"].ToString().ToString()));

                //  DivRmRate.Style.Add("display", "None");


            }



        }
        catch
        {
        }

    }

    public void Select()
    {

        try
        {
            for (int k = 0; k < gdvHotelRoomRates.Rows.Count; k++)
            {
                DropDownList ddl = (DropDownList)gdvHotelRoomRates.Rows[k].FindControl("ddlGuests");
                ddl.SelectedValue = "6";
            }
        }

        catch
        {
        }
    }

    public void bindMealPlans(int acmid, int agntid, int TPax)
    {
        try
        {
            blht.action = "GetMealPlans";
            blht.Accomid = acmid;
            blht.agentid = agntid;
            blht.TotPax = TPax;

            Returndt = new DataTable();
            Returndt = dlht.GetMealPlans(blht);
            if (Returndt != null)
            {
                if (Returndt.Rows.Count > 0)
                {
                    ViewState["MealPlan"] = Returndt;
                    gdvMealplans.DataSource = Returndt;
                    gdvMealplans.DataBind();
                    ddlMealPlan.DataSource = Returndt;
                    ddlMealPlan.DataTextField = "MealPlan";
                    ddlMealPlan.DataValueField = "MealPlanId";
                    ddlMealPlan.DataBind();
                }
                else
                {
                    ViewState["MealPlan"] = null;
                }
            }
            else
            {
                ViewState["MealPlan"] = null;
            }

        }

        catch
        {

            ViewState["MealPlan"] = null;
        }
    }


    public void RemoveZeroes()
    {
        foreach (GridViewRow row in gdvHotelRoomRates.Rows)
        {
            try
            {
                string[] arr=  row.Cells[10].Text.ToString().Split(' ');

                row.Cells[10].Text = arr[0].ToString() +" "+ Convert.ToDouble(arr[1]).ToString("#.##");
            }

            catch
            {
            }
        }
    }



    private void bindRoomRates(int accmid, int Totpax, int agid, DateTime chkin, DateTime chkout, int norooms, int RtypeId)
    {
        try
        {
            if (Session["UserCode"] != null)
            {
                blht.action = "RoomRate";
            }
            else
            {
                blht.action = "RoomRateCust";
            }
            blht.Accomid = accmid;
            blht.TotPax = Totpax;
            blht.Reqnoofrooms = norooms;
            blht.checkin = chkin;
            blht.Checkout = chkout;
            blht.RoomTypeId = RtypeId;


            blht.agentid = agid;
            Returndt = dlht.GetHotelRates(blht);

            if (Returndt != null)
            {
                SessionServices.SaveSession<DataTable>("RoomInfo", Returndt);
                dv = new DataView(Returndt);
                //  dv.RowFilter = "ActualRoomTypeId<>0";

                gdvHotelRoomRates.DataSource = dv;
                gdvHotelRoomRates.DataBind();
                RemoveZeroes();
                ViewState["Rrate"] = Returndt;
            }
        }
        catch
        {
        }
    }


    public void bindRatesOnly(int accmid, int Totpax, int agid, DateTime chkin, DateTime chkout, int norooms, int RtypeId, int Rindex)
    {
        try
        {
            if (Session["UserCode"] != null)
            {
                blht.action = "RoomRate";
            }
            else
            {
                blht.action = "RoomRateCust";
            }
            blht.Accomid = accmid;
            blht.TotPax = Totpax;
            blht.Reqnoofrooms = norooms;
            blht.checkin = chkin;
            blht.Checkout = chkout;
            blht.RoomTypeId = RtypeId;


            blht.agentid = agid;
            Returndt = new DataTable();
            Returndt = dlht.GetHotelRates(blht);
            DataTable dtnew = ViewState["Rrate"] as DataTable;

            if (Returndt != null)
            {
                // Session["RoomInfo"] = Returndt;
                dv = new DataView(Returndt);
                //  dv.RowFilter = "ActualRoomTypeId<>0";

                DataRow row = dtnew.Rows[Rindex];
                DataView dv1 = new DataView(Returndt);
                dv1.RowFilter = "RoomCategoryId=" + row["RoomCategoryId"].ToString() + "";
                row["Amt"] = dv1.ToTable().Rows[0]["Amt"].ToString();
            }

            gdvHotelRoomRates.DataSource = dtnew;
            gdvHotelRoomRates.DataBind();
            RemoveZeroes();




        }







        catch
        {
        }
    }



    private void bindServiceRates(int acmid)
    {
        try
        {
            blht.action = "MealRate";
            blht.Accomid = acmid;
            Returndt = new DataTable();
            Returndt = dlht.GetHotelRates(blht);
            if (Returndt != null)
            {


                gdvHotelServiceRates.DataSource = Returndt;
                gdvHotelServiceRates.DataBind();
            }


        }

        catch
        {
        }
    }



    //public void noofpax(string roomno)
    //{
    //    try
    //    {
    //        ddlPax.Items.Clear();
    //        blht.action = "GetMaxPax";
    //        Int32.TryParse(Session["AcId"].ToString(), out iAccomId);
    //        blht.Accomid = iAccomId;
    //        blht.roomno = roomno;
    //        Returndt = new DataTable();
    //        Returndt = dlht.getmaxPax(blht);
    //        if (Returndt != null)
    //        {
    //            if (Returndt.Rows.Count > 0)
    //            {
    //                if (Convert.ToBoolean(Returndt.Rows[0][1]) == true)
    //                {
    //                    //ddlConvert.Enabled = true;
    //                }
    //                else
    //                {
    //                    // ddlConvert.Enabled = false;
    //                }
    //                Int32.TryParse(Returndt.Rows[0][0].ToString(), out maxpax);

    //                for (int k = 0; k <= maxpax; k++)
    //                {
    //                    ddlPax.Items.Add(k.ToString());
    //                }

    //            }



    //        }


    //    }

    //    catch
    //    {

    //    }
    //}



    protected void btnAdd_Click(object sender, EventArgs e)
    {


    }

    public void adddatestoddl(DateTime cin, DateTime cout)
    {
        try
        {
            DateTime dtm;
            ddlDates.Items.Insert(0, "-Select-");
            dtm = cin;

            Int32.TryParse((cout - cin).TotalDays.ToString(), out inights);
            for (int k = 0; k < inights + 1; k++)
            {


                ddlDates.Items.Insert(k + 1, dtm.ToString("dd-MMM-yyyy"));
                dtm = dtm.AddDays(1);
            }

        }

        catch
        {
        }
    }


    public void CalculateRoomRates()
    {

        try
        {
            for (int j = 0; j < gdvSelectedRooms.Rows.Count; j++)
            {
                string[] arr = gdvSelectedRooms.Rows[j].Cells[6].Text.ToString().Split(' ');


                rtotal = rtotal + Convert.ToDouble(arr[1]);
            }
            lblRmRate.Text = rtotal.ToString();
        }
        catch
        {

        }
    }

    public void addrows(DataView view, int roomctid, int pax, string conv)
    {
        try
        {
            roomstobebooked(conv);
            if (ViewState["RoomsTobeBooked"] != null)
            {
                Returndt = new DataTable();
                Returndt = ViewState["RoomsTobeBooked"] as DataTable;

            }


            DataTable dtInsertable = new DataTable();

            dtInsertable.Columns.Add("Rooms", typeof(int));
            dtInsertable.Columns.Add("RoomCategoryId", typeof(int));
            dtInsertable.Columns.Add("categoryName", typeof(string));
            dtInsertable.Columns.Add("Pax", typeof(int));
            dtInsertable.Columns.Add("Price", typeof(string));


            dtInsertable.Columns.Add("Nights", typeof(Int32));
            dtInsertable.Columns.Add("Total1", typeof(string));
            dtInsertable.Columns.Add("Total", typeof(decimal));
            dtInsertable.Columns.Add("RoomNo", typeof(string));
            dtInsertable.Columns.Add("Currency", typeof(string));

            dtInsertable.Columns.Add("ConvDouble", typeof(string));
            if (ViewState["VsRoomDetails"] == null)
            {
                DataRow dr = dtInsertable.NewRow();

                dr["RoomNo"] = Returndt.Rows[0][0].ToString();
                dr["Rooms"] = 1;
                dr["RoomCategoryId"] = roomctid;
                dr["Pax"] = pax;

                dr["Currency"] = view.ToTable().Rows[0]["Currency"].ToString();
                dr["Price"] =view.ToTable().Rows[0]["Currency"].ToString()+" "+ Convert.ToDouble(view.ToTable().Rows[0]["Amt"]).ToString();

                DateTime.TryParse(Session["HCheckin"].ToString(), out chkin);
                DateTime.TryParse(Session["HCheckout"].ToString().ToString(), out chkout);
                Int32.TryParse((chkout - chkin).TotalDays.ToString(), out inights);
                dr["Nights"] = inights;
                dr["Total"] = inights * Convert.ToDouble(view.ToTable().Rows[0]["Amt"]);

                dr["Total1"] =view.ToTable().Rows[0]["Currency"].ToString()+" "+( inights * Convert.ToDouble(view.ToTable().Rows[0]["Amt"])).ToString();
                dr["categoryName"] = view.ToTable().Rows[0][4].ToString();
                dr["ConvDouble"] = conv == "1" ? "false" : "true";
                dtInsertable.Rows.Add(dr);
                ViewState["VsRoomDetails"] = dtInsertable;
                gdvSelectedRooms.DataSource = dtInsertable;
                gdvSelectedRooms.DataBind();
                gdvSelectedRooms.Columns[1].ItemStyle.Width = 0;
            }
            else
            {



                dtInsertable = ViewState["VsRoomDetails"] as DataTable;


                DataRow dr = dtInsertable.NewRow();

                dr["RoomNo"] = Returndt.Rows[0][0].ToString();
                dr["Rooms"] = Convert.ToInt32(dtInsertable.Rows[dtInsertable.Rows.Count - 1][0]) + 1;

                dr["RoomCategoryId"] = roomctid;

                dr["categoryName"] = view.ToTable().Rows[0][4].ToString();

                dr["Pax"] = pax;

                dr["Currency"] = view.ToTable().Rows[0]["Currency"].ToString();
                dr["Price"] = view.ToTable().Rows[0]["Currency"].ToString()+" "+ Convert.ToDouble(view.ToTable().Rows[0][3]).ToString();



                DateTime.TryParse(Session["HCheckin"].ToString(), out chkin);
                DateTime.TryParse(Session["HCheckout"].ToString().ToString(), out chkout);
                Int32.TryParse((chkout - chkin).TotalDays.ToString(), out inights);
                dr["Nights"] = inights;
                dr["Total"] = inights * Convert.ToDouble(view.ToTable().Rows[0]["Amt"]);

                dr["Total1"] = view.ToTable().Rows[0]["Currency"].ToString() + " " +( inights * Convert.ToDouble(view.ToTable().Rows[0][3])).ToString();
                dr["ConvDouble"] = conv == "1" ? "false" : "true";


                // int Counter = 0;
                //foreach (DataRow dr1 in dtInsertable.Rows)
                //{
                //    if (dr1["RoomNumber"].ToString() == "")
                //    {
                //        dr1.Delete();
                //        Counter++;
                //        break;
                //    }
                //    else
                //    {
                //        //do nothing
                //    }

                //}
                //if (Counter > 0)
                //{
                //    dtInsertable.AcceptChanges();
                //    dtInsertable.Rows.Add(dr);
                //}
                //else
                Int32.TryParse(Session["HNoofrooms"].ToString().ToString(), out noofrooms);

                if (dtInsertable.Rows.Count < noofrooms)
                {
                    dtInsertable.Rows.Add(dr);
                }
                ViewState["VsRoomDetails"] = dtInsertable;
                gdvSelectedRooms.DataSource = dtInsertable;
                gdvSelectedRooms.DataBind();
                gdvSelectedRooms.Columns[1].ItemStyle.Width = 0;
            }
        }
        catch
        {

        }


    }


    protected void gdvMealplans_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    public double CalculatemealRate(int mid)
    {
        try
        {

            for (int i = 0; i < gdvMealplans.Rows.Count; i++)
            {
                GridViewRow grow = (GridViewRow)gdvMealplans.Rows[i];
                int chk = Convert.ToInt32(grow.Cells[1].Text);
                if (chk == mid)
                {
                    for (int j = 2; j < grow.Cells.Count; j++)
                    {
                        mTotal = mTotal + Convert.ToDouble(grow.Cells[j].Text);
                    }

                    return mTotal;
                }
            }
            return 0;
        }

        catch
        {
            return 0;
        }
    }

    public void uncheckothercheckboxes(int index)
    {
        try
        {
            for (int i = 0; i < gdvMealplans.Rows.Count; i++)
            {
                GridViewRow grow = (GridViewRow)gdvMealplans.Rows[i];
                CheckBox chk = (CheckBox)grow.FindControl("chkMP"); ;
                if (grow.RowIndex != index)
                {
                    chk.Checked = false;
                }


            }
        }

        catch
        {
        }
    }

    protected void chkMP_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow grow = (GridViewRow)chk.NamingContainer;
            uncheckothercheckboxes(grow.RowIndex);
            mTotal = 0;
            //   CalculatemealRate();



        }
        catch
        {
        }
    }


    //private int InsertBookingTableData()
    //{
    //    try
    //    {

    //        //  blsr._sBookingRef = txtBookRef.Text.Trim().ToString();
    //        blsr._dtStartDate = Convert.ToDateTime(txtChkin.Text.Trim());
    //        blsr._dtEndDate = Convert.ToDateTime(txtChkOut.Text.Trim());


    //        Int32.TryParse(Session["AcId"].ToString(), out iAccomId);
    //        Int32.TryParse(Session["AccomTypeId"].ToString(), out iaccomtypeid);
    //        Int32.TryParse(Session["Agid"].ToString(), out iagentid);
    //        blsr._iAccomTypeId = iaccomtypeid;
    //        blsr._iAccomId = iAccomId;
    //        blsr._iAgentId = iagentid;
    //        //Convert.ToInt32(Session["UserCode"].ToString());
    //        blsr._iNights = Convert.ToInt32((Convert.ToDateTime(txtChkOut.Text.Trim()) - Convert.ToDateTime(txtChkin.Text.Trim())).TotalDays);
    //        DataTable dtRoomBookingDetails = ViewState["VsRoomDetails"] as DataTable;
    //        blsr._iPersons = Convert.ToInt32(dtRoomBookingDetails.Compute("SUM(Pax)", string.Empty));
    //        blsr._BookingStatusId = 1;
    //        blsr._SeriesId = 0;
    //        blsr._proposedBooking = false;
    //        blsr._chartered = false;
    //        int GetQueryResponse = dlsr.AddParentBookingDetail(blsr);
    //        if (GetQueryResponse > 0)
    //            return 1;
    //        else
    //            return 0;
    //    }
    //    catch
    //    {
    //        return 0;
    //    }
    //}



    //private int InsertRoomBookingTableData()
    //{



    //    Int32.TryParse(Session["AcId"].ToString(), out iAccomId);
    //    blsr._iAccomId = iAccomId;

    //    blsr.action = "getMaxBookId";



    //    DataTable dtmaxId = dlsr.GetMaxBookingId(blsr);

    //    int MaxBookingId = Convert.ToInt32(dtmaxId.Rows[0].ItemArray[0].ToString());
    //    //  BookedId = MaxBookingId;
    //    blsr._iBookingId = MaxBookingId;
    //    int LoopInsertStatus = 0;
    //    try
    //    {

    //        for (int LoopCounter = 0; LoopCounter < gdvSelectedRooms.Rows.Count; LoopCounter++)
    //        {

    //            blsr._dtStartDate = Convert.ToDateTime(txtChkin.Text.Trim());
    //            blsr._dtEndDate = Convert.ToDateTime(txtChkOut.Text.Trim());
    //            blsr._iPaxStaying = Convert.ToInt32(gdvSelectedRooms.Rows[LoopCounter].Cells[3].Text);
    //            blsr._bConvertTo_Double_Twin = false;
    //            blsr._cRoomStatus = "B";
    //            HiddenField hfnrm = (HiddenField)gdvSelectedRooms.Rows[LoopCounter].FindControl("hdnRmno");


    //            blsr._sRoomNo = hfnrm.Value.ToString();
    //            //   blsr.action = "AddPriceDetailsToo";
    //            blsr._Amt = Convert.ToDecimal(gdvSelectedRooms.Rows[LoopCounter].Cells[4].Text);

    //            int GetQueryResponse = dlsr.AddRoomBookingDetails(blsr);
    //            if (GetQueryResponse > 0)
    //                LoopInsertStatus++;
    //            else
    //            {
    //                //do nothing
    //            }


    //        }
    //        //   insertbookingMealData(MaxBookingId);
    //        if (LoopInsertStatus == gdvSelectedRooms.Rows.Count)
    //        {

    //            return 1;
    //        }
    //        else
    //            return
    //                0;



    //    }
    //    catch
    //    {
    //        return 0;
    //    }


    //}

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {


            Session["HotelBokingUrl"] = Request.Url.ToString();


            Int32.TryParse(Session["AcId"].ToString(), out iAccomId);
            Int32.TryParse(Session["AccomTypeId"].ToString(), out iaccomtypeid);

            DataTable fg = ViewState["VsRoomDetails"] as DataTable;
            SessionServices.SaveSession<DataTable>("Bookingdt", fg);
            
            Session["Chkin"] = Session["HCheckin"].ToString();
            Session["chkout"] = Session["HCheckout"].ToString();
            //   Session["BookRef"] = txtBookRef.Text.Trim();
            Session["AccomId"] = iAccomId;
            Session["iAccomtypeId"] = iaccomtypeid;
            Session["AId"] = Session["Agid"].ToString();
            if (Session["RedUrl"] == null)
            {
                string redurl = "AfterBookingDetails.aspx?AccomName=" + Request.QueryString["AccomName"].ToString() + "";
                Session["RedUrl"] = redurl;
            }

            Response.Redirect(Session["RedUrl"].ToString());

        }
        catch
        {

        }

    }

    public void insertbookingMealData(int bookingid)
    {
        try
        {
            int count = 0;
            int flag = 0;
            Returndt = new DataTable();
            Returndt = ViewState["MealPlan"] as DataTable;
            DataView dv;
            blht.iBookingId = bookingid;

            if (gdvselectedMealplan.Rows.Count > 0)
            {
                for (int k = 0; k < gdvselectedMealplan.Rows.Count; k++)
                {
                    count = 0;
                    blht.dtMealDate = Convert.ToDateTime(gdvselectedMealplan.Rows[k].Cells[0].Text);
                    blht.iMealPlanId = Convert.ToInt32(gdvselectedMealplan.Rows[k].Cells[1].Text);
                    dv = new DataView(Returndt);
                    dv.RowFilter = "MealPlanId='" + gdvselectedMealplan.Rows[k].Cells[1].Text + "'";
                    blht.bWelcomeDrink = Convert.ToBoolean(dv.ToTable().Rows[0][2]);
                    blht.bBreakfast = Convert.ToBoolean(dv.ToTable().Rows[0][3]);
                    blht.bLunch = Convert.ToBoolean(dv.ToTable().Rows[0][4]);
                    blht.bEveSnacks = Convert.ToBoolean(dv.ToTable().Rows[0][5]);
                    blht.bDinner = Convert.ToBoolean(dv.ToTable().Rows[0][6]);
                    int l = dlht.AddBookingMealRates(blht);


                    if (l > 0)
                    {
                        flag++;
                    }




                }

                if (flag == count && count > 0)
                {
                }
            }
        }
        catch
        {
        }
    }
    protected void btnAddmealplan_Click(object sender, EventArgs e)
    {
        try
        {
            addmealplanRows();
        }
        catch
        {
        }
    }

    public void addmealplanRows()
    {
        try
        {
            DataTable dtInsertable = new DataTable();
            dtInsertable.Columns.Add("Date", typeof(string));
            dtInsertable.Columns.Add("MealPlanId", typeof(int));
            dtInsertable.Columns.Add("MealPlan", typeof(string));

            dtInsertable.Columns.Add("Total", typeof(decimal));
            if (ViewState["VsDetails"] == null)
            {
                DataRow dr = dtInsertable.NewRow();
                dr["Date"] = ddlDates.SelectedItem.Text;
                dr["MealPlanId"] = ddlMealPlan.SelectedValue;
                dr["MealPlan"] = ddlMealPlan.SelectedItem.Text;
                dr["Total"] = CalculatemealRate(Convert.ToInt32(ddlMealPlan.SelectedValue));


                dtInsertable.Rows.Add(dr);
                ViewState["VsDetails"] = dtInsertable;
                gdvselectedMealplan.DataSource = dtInsertable;
                gdvselectedMealplan.DataBind();
            }
            else
            {
                dtInsertable = ViewState["VsDetails"] as DataTable;
                DataRow dr = dtInsertable.NewRow();
                dr["Date"] = ddlDates.SelectedItem.Text;
                dr["MealPlanId"] = ddlMealPlan.SelectedValue;
                dr["MealPlan"] = ddlMealPlan.SelectedItem.Text;
                dr["Total"] = CalculatemealRate(Convert.ToInt32(ddlMealPlan.SelectedValue));








                int Counter = 0;
                foreach (DataRow dr1 in dtInsertable.Rows)
                {
                    if (dr1["Date"].ToString() == ddlDates.SelectedItem.Text.ToString())
                    {
                        dr1.Delete();
                        Counter++;
                        break;
                    }
                    else
                    {
                        //do nothing
                    }

                }
                if (Counter > 0)
                {
                    dtInsertable.AcceptChanges();
                    dtInsertable.Rows.Add(dr);
                }
                else
                    dtInsertable.Rows.Add(dr);

                ViewState["VsDetails"] = dtInsertable;

                gdvselectedMealplan.DataSource = dtInsertable;
                gdvselectedMealplan.DataBind();
            }
        }
        catch
        {

        }

    }



    protected void chkCategory_CheckedChanged(object sender, EventArgs e)
    {

    }


    public void roomstobebooked(string conv)
    {

        try
        {

            Int32.TryParse(Session["AcId"].ToString(), out iAccomId);
            Int32.TryParse(Session["HNoofrooms"].ToString().ToString(), out noofrooms);
            blht.action = "GetRoomNoToBook";
            blht.Accomid = iAccomId;
            blht.checkin = Convert.ToDateTime(Session["HCheckin"].ToString());
            blht.Checkout = Convert.ToDateTime(Session["HCheckout"].ToString());
            blht.RoomCateId = Convert.ToInt32(ViewState["rcatid"]);
            blht.RoomTypeId = Convert.ToInt32(ViewState["rtypeid"]);
            blht.Convertible_To_Double = Convert.ToBoolean(conv == "0" ? "true" : "false");


            blht.Reqnoofrooms = noofrooms;


            DataTable nalrms = new DataTable();


            List<string> myCollection = new List<string>();
            if (ViewState["VsRoomDetails"] != null)
            {
                nalrms = ViewState["VsRoomDetails"] as DataTable;
                for (int j = 0; j < nalrms.Rows.Count; j++)
                {
                    myCollection.Add(nalrms.Rows[j]["RoomNo"].ToString());
                }
            }

            blht.roomstring = string.Join(",", myCollection.ToArray());
            Returndt = new DataTable();
            Returndt = dlht.GetRoomNosToBook(blht);

            if (Returndt != null)
            {
                if (Returndt.Rows.Count > 0)
                {
                    ViewState["RoomsTobeBooked"] = Returndt;
                }
                else
                {
                    ViewState["RoomsTobeBooked"] = null;
                }
            }
        }

        catch
        {
        }
    }



    protected void gdvSelectedRooms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Width = 0;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        System.Web.Security.FormsAuthentication.SignOut();
        Response.Redirect("../Cruise/booking/SearchProperty.aspx");
    }
    protected void btnBook_Click(object sender, EventArgs e)
    {




        try
        {

            int rowindex = 0;
            Button chk = (Button)sender;

            GridViewRow Grow = (GridViewRow)chk.NamingContainer;

            rowindex = Grow.RowIndex;



            int rcatid = 0;
            int rtypeid = 0;

            HiddenField hdnctid = (HiddenField)Grow.FindControl("hfrctId");
            HiddenField hfntpid = (HiddenField)Grow.FindControl("hfrtype");
            DropDownList ddlConv = (DropDownList)Grow.FindControl("ddlConvert");
            DropDownList ddlGuests = (DropDownList)Grow.FindControl("ddlGuests");


            Int32.TryParse(hdnctid.Value.ToString(), out rcatid);
            Int32.TryParse(hfntpid.Value.ToString(), out rtypeid);

            ViewState["rcatid"] = rcatid;
            ViewState["rtypeid"] = rtypeid;



            int al = gdvSelectedRooms.Rows.Count + 1;
            int pax = 0;
            Int32.TryParse(ddlGuests.SelectedItem.Text, out pax);
            Returndt = new DataTable();
            Returndt = ViewState["Rrate"] as DataTable;
            lblCurrency.Text = Returndt.Rows[0]["Currency"].ToString();
            Int32.TryParse(ViewState["rcatid"].ToString(), out roomCatId);
            Int32.TryParse(ViewState["rtypeid"].ToString(), out roomtypeid);
            // Int32.TryParse(pax, out irpax);
            dv = new DataView();
            dv = new DataView(Returndt, "RoomCategoryId='" + roomCatId + "' and RoomTypeId='" + roomtypeid + "'", "RoomCategoryId,RoomTypeId", DataViewRowState.CurrentRows);
            if (pax > 0 && gdvSelectedRooms.Rows.Count < Convert.ToInt32(dv.ToTable().Rows[0]["rcount"]))
            {


                if (Session["UserCode"] != null)
                {

                    DateTime.TryParse(Session["HCheckin"].ToString(), out chkin);
                    Int32.TryParse(Session["AcId"].ToString(), out iAccomId);
                    blsr.accomId = iAccomId;
                    blsr._iAgentId = Convert.ToInt32(Session["UserCode"].ToString());
                    blsr.action = "getmaxroomsHotel";
                    blsr._dtStartDate = chkin;

                    Returndt = new DataTable();
                    Returndt = dlsr.getMaxRoomsBookable(blsr);
                    if (Returndt != null)
                    {
                        if (gdvSelectedRooms.Rows.Count < Convert.ToInt32(Returndt.Rows[0][0]))
                        {
                            addrows(dv, roomCatId, pax, ddlConv.SelectedIndex.ToString());
                            CalculateRoomRates();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('You cannot book any more rooms. Please contact our reservations office to make additional bookings.')", true);
                        }
                    }
                }
                else
                {
                    addrows(dv, roomCatId, pax, ddlConv.SelectedIndex.ToString());
                    CalculateRoomRates();
                }

            }

            if (gdvSelectedRooms.Rows.Count > 0)
            {
                DivRmRate.Style.Remove("display");
            }
            else
            {
                DivRmRate.Style.Add("display", "None");

            }





        }
        catch
        {

        }

    }
    protected void ddlGuests_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow grow = (GridViewRow)ddl.NamingContainer;
            HiddenField rtypeid = (HiddenField)grow.FindControl("hfrtype");
            HiddenField mxgst = (HiddenField)grow.FindControl("hfMaxGuests");

            switch (Convert.ToInt32(ddl.SelectedItem.Text))
            {
                case 1:
                    roomtypeid = 1;
                    break;
                case 2:
                    roomtypeid = 6;
                    break;
                case 3:
                    roomtypeid = 3;
                    break;



            }
            Int32.TryParse(Session["AcId"].ToString(), out iAccomId);
            Int32.TryParse(Session["Agid"].ToString(), out iagentid);
            Int32.TryParse(Session["Hpax"].ToString(), out Totpax);
            Int32.TryParse(Session["AccomTypeId"].ToString(), out iaccomtypeid);

            DateTime.TryParse(Session["HCheckin"].ToString(), out chkin);
            DateTime.TryParse(Session["HCheckout"].ToString().ToString(), out chkout);

            Int32.TryParse(Session["HNoofrooms"].ToString().ToString(), out noofrooms);

            ViewState["rowindex1"] = grow.RowIndex;

            ViewState["pax"] = roomtypeid;
            //   this.bindRoomRates(iAccomId, Totpax, iagentid, chkin, chkout, noofrooms, roomtypeid);
            bindRatesOnly(iAccomId, Totpax, iagentid, chkin, chkout, noofrooms, roomtypeid, grow.RowIndex);

            //Returndt = new DataTable();

            //Returndt = ViewState["Rrate"] as DataTable;

            //Session["RoomInfo"] = Returndt;
            //dv = new DataView(Returndt);
            //dv.RowFilter = "RoomTypeId='" + ddl.SelectedValue + "'";



            //gdvHotelRoomRates.DataSource = dv;
            //gdvHotelRoomRates.DataBind();

            DropDownList ddl1 = (DropDownList)gdvHotelRoomRates.Rows[Convert.ToInt32(ViewState["rowindex1"])].FindControl("ddlGuests");
            ddl1.SelectedValue = ViewState["pax"].ToString();
            //DropDownList ddl = (DropDownList)gdvHotelRoomRates.Rows[k].FindControl("ddlGuests")

            DropDownList ddl2 = (DropDownList)gdvHotelRoomRates.Rows[Convert.ToInt32(ViewState["rowindex"])].FindControl("ddlConvert");
            //   string ab = ViewState["index"].ToString();

            ddl2.SelectedIndex = 1;





        }
        catch
        {
        }
    }
    protected void gdvHotelRoomRates_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //if (!IsPostBack)
        //{
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1)
            {
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlGuests");
                DropDownList ddlConvert = (DropDownList)e.Row.FindControl("ddlConvert");
                HiddenField hdnMxguests = (HiddenField)e.Row.FindControl("hfMaxGuests");
                HiddenField hdnRtype = (HiddenField)e.Row.FindControl("hfrtype");
                // ddl.Items.Clear();
                for (int j = 0; j < Convert.ToInt32(hdnMxguests.Value); j++)
                {
                    switch (j + 1)
                    {
                        case 1:
                            roomtypeid = 1;
                            break;
                        case 2:
                            roomtypeid = 6;
                            break;
                        case 3:
                            roomtypeid = 3;
                            break;



                    }


                    ddl.Items.Insert(j, new ListItem((j + 1).ToString(), roomtypeid.ToString()));

                }
                ddlConvert.SelectedIndex = 1;
                ddl.SelectedValue = "6";
            }

        }
        //}
    }
    protected void ddlConvert_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow grow = (GridViewRow)ddl.NamingContainer;
            HiddenField rtypeid = (HiddenField)grow.FindControl("hfrtype");
            HiddenField mxgst = (HiddenField)grow.FindControl("hfMaxGuests");


            if (ddl.SelectedIndex == 0)
            {
                roomtypeid = 2;
            }
            else
            {
                roomtypeid = 6;

            }
            ViewState["rowindex"] = grow.RowIndex;

            ViewState["index"] = ddl.SelectedIndex;



            Int32.TryParse(Session["AcId"].ToString(), out iAccomId);
            Int32.TryParse(Session["Agid"].ToString(), out iagentid);
            Int32.TryParse(Session["Hpax"].ToString(), out Totpax);
            Int32.TryParse(Session["AccomTypeId"].ToString(), out iaccomtypeid);

            DateTime.TryParse(Session["HCheckin"].ToString(), out chkin);
            DateTime.TryParse(Session["HCheckout"].ToString().ToString(), out chkout);

            Int32.TryParse(Session["HNoofrooms"].ToString().ToString(), out noofrooms);

            if (ddl.SelectedIndex == 0)
            {
                bindRatesOnly(iAccomId, Totpax, iagentid, chkin, chkout, noofrooms, roomtypeid, grow.RowIndex);
                //   this.bindRoomRates(iAccomId, Totpax, iagentid, chkin, chkout, noofrooms, roomtypeid);
            }
            else
            {
                bindRatesOnly(iAccomId, Totpax, iagentid, chkin, chkout, noofrooms, roomtypeid, grow.RowIndex);
                // this.bindRoomRates(iAccomId, Totpax, iagentid, chkin, chkout, noofrooms, 6);
            }

            DropDownList ddl1 = (DropDownList)gdvHotelRoomRates.Rows[Convert.ToInt32(ViewState["rowindex"])].FindControl("ddlConvert");
            ddl1.SelectedIndex = Convert.ToInt32(ViewState["index"]);

            DropDownList ddl2 = (DropDownList)gdvHotelRoomRates.Rows[Convert.ToInt32(ViewState["rowindex1"])].FindControl("ddlGuests");
            ddl2.SelectedValue = ViewState["pax"].ToString();


        }

        catch
        {
        }
    }
    protected void gdvSelectedRooms_RowCommand(object sender, GridViewCommandEventArgs e)
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
                gdvSelectedRooms.DataSource = dtnew;
                gdvSelectedRooms.DataBind();
                CalculateRoomRates();
                if (gdvSelectedRooms.Rows.Count > 0)
                {
                    DivRmRate.Style.Remove("display");
                }
                else
                {
                    DivRmRate.Style.Add("display", "None");
                    ViewState["VsRoomDetails"] = null;

                }

            }
            catch
            {
            }
        }
    }
}