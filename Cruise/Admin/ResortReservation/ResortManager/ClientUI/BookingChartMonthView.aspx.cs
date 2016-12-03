using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Common.DataEntities.Masters;

using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;

public partial class ClientUI_BookingChartMonthView : ClientBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillAccomodationType();
            FillMonths();
            FillYears();
            ddlMonths.SelectedValue = DateTime.Today.Month.ToString();
            ddlYears.SelectedValue = DateTime.Today.Year.ToString();
            ShowMonthChart();
        }
        if (IsPostBack)
        {
            Table t = new Table();
            if (SessionServices.BookingChart_TableMonthChart != null)
                t = (Table)SessionServices.BookingChart_TableMonthChart;
            AddMonthChartToPanel(t);
        }
    }

    protected void ddlAccomType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccomType.SelectedValue != null)
            FillAccomodations(Convert.ToInt32(ddlAccomType.SelectedValue));
    }

    private void FillAccomodationType()
    {
        AccomodationTypeMaster oAccomTypeMaster = new AccomodationTypeMaster();
        AccomTypeDTO[] oAccomTypeData = oAccomTypeMaster.GetData();
        ddlAccomType.Items.Clear();
        SortedList slAccomMaster = new SortedList();
        slAccomMaster.Add("0", "Choose Accomodation Type");
        for (int i = 0; i < oAccomTypeData.Length; i++)
        {
            slAccomMaster.Add(Convert.ToString(oAccomTypeData[i].AccomodationTypeId), Convert.ToString(oAccomTypeData[i].AccomodationType));
        }
        ddlAccomType.DataSource = slAccomMaster;
        ddlAccomType.DataTextField = "value";
        ddlAccomType.DataValueField = "key";
        ddlAccomType.DataBind();
        oAccomTypeData = null;
        oAccomTypeMaster = null;
    }

    private void FillAccomodations(int iAccomTypeID)
    {
        AccomodationMaster oAccomMaster = new AccomodationMaster();
        AccomodationDTO[] oAccomData = oAccomMaster.GetData(0, iAccomTypeID, 0);
        if (oAccomData != null)
        {
            if (oAccomData.Length > 0)
            {
                SortedList slAccomMaster = new SortedList();
                slAccomMaster.Add("0", "Choose Accomodation");
                for (int i = 0; i < oAccomData.Length; i++)
                {
                    slAccomMaster.Add(Convert.ToString(oAccomData[i].AccomodationId), Convert.ToString(oAccomData[i].AccomodationName));
                }
                ddlAccomodations.DataSource = slAccomMaster;
                ddlAccomodations.DataTextField = "value";
                ddlAccomodations.DataValueField = "key";
                ddlAccomodations.DataBind();
            }
        }
    }

    private void FillMonths()
    {
        ddlMonths.Items.Clear();
        SortedList slMonths = new SortedList();
        slMonths.Add(0, "Months");
        slMonths.Add(1, "Jan");
        slMonths.Add(2, "Feb");
        slMonths.Add(3, "Mar");
        slMonths.Add(4, "Apr");
        slMonths.Add(5, "May");
        slMonths.Add(6, "Jun");
        slMonths.Add(7, "Jul");
        slMonths.Add(8, "Aug");
        slMonths.Add(9, "Sep");
        slMonths.Add(10, "Oct");
        slMonths.Add(11, "Nov");
        slMonths.Add(12, "Dec");
        
        ddlMonths.DataSource = slMonths;
        ddlMonths.DataTextField = "value";
        ddlMonths.DataValueField = "key";
        ddlMonths.DataBind();
        slMonths = null;
    }

    private void FillYears()
    {
        ddlYears.Items.Clear();
        SortedList slYears = new SortedList();        
        slYears.Add(0, "Years");        
        for (int i = 1; i < 50; i++)
        {            
            slYears.Add(i + 2000, Convert.ToString(i+2000));
        }        
        ddlYears.DataSource = slYears;
        ddlYears.DataTextField = "value";
        ddlYears.DataValueField = "key";
        ddlYears.DataBind();
        slYears = null;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        ShowMonthChart();
    }

    private void ShowMonthChart()
    {
        Table t;
        t = GenerateMonthTable(Convert.ToInt32(ddlYears.SelectedValue), Convert.ToInt32(ddlMonths.SelectedValue));
        t = FormatMonthTable(t);
        AddMonthChartToPanel(t);
        t = null;
    }

    
    private Table GenerateMonthTable(int Year, int Month)
    {
        Table tblMonthView;
        TableRow HeaderRow, tr;
        TableCell tc;
        tblMonthView = new Table();
        tr = new TableRow();
        HeaderRow = new TableRow();

        #region MonthHeader
        tc = new TableCell();
        tc.Text = "S";
        tc.ID = "1";
        HeaderRow.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "M";
        tc.ID = "2";
        HeaderRow.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "T";
        tc.ID = "3";
        HeaderRow.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "W";
        tc.ID = "4";
        HeaderRow.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Th";
        tc.ID = "5";
        HeaderRow.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "F";
        tc.ID = "6";
        HeaderRow.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "S";
        tc.ID = "7";
        HeaderRow.Cells.Add(tc);

        tblMonthView.Rows.Add(HeaderRow);
        #endregion MonthHeader

        DateTime startdate;
        int startcell =0;
        startdate = Convert.ToDateTime(Year.ToString("0000") + "-" + Month.ToString("00") + "-" + "01");

        switch (startdate.DayOfWeek)
        {
            case DayOfWeek.Sunday:
                startcell = 0;
                break;
            case DayOfWeek.Monday:
                startcell = 1;
                break;
            case DayOfWeek.Tuesday:
                startcell = 2;
                break;
            case DayOfWeek.Wednesday:
                startcell = 3;
                break;
            case DayOfWeek.Thursday:
                startcell = 4;
                break;
            case DayOfWeek.Friday:
                startcell = 5;
                break;            
            case DayOfWeek.Saturday:
                startcell = 6;
                break;
        }        

        tr = new TableRow();
        for (int i = 0; i < startcell; i++)
        {
            tc = new TableCell();
            tr.Cells.Add(tc);
        }

        //startcell++;
        int startday = 1;
        while (startday < DateTime.DaysInMonth(Year, Month))
        {
            if (startcell == 0)
                tr = new TableRow();
            for (int j = startcell; j < 7; j++)
            {
                tc = new TableCell();
                tc.Text = startdate.Day.ToString();
                tr.Cells.Add(tc);
                startdate = startdate.AddDays(1);
                startday++;
                if (startdate.Month != Month)
                    break;                
            }
            tblMonthView.Rows.Add(tr);
            startcell = 0;
        }        
        return tblMonthView;
    }

    private Table FormatMonthTable(Table tBoookingMonthView)
    {
        tBoookingMonthView.Style[HtmlTextWriterStyle.BorderWidth] = "1";
        tBoookingMonthView.Style[HtmlTextWriterStyle.BorderColor] = "#507cd1";
        if (tBoookingMonthView.Rows.Count > 1)
        {
            tBoookingMonthView.Rows[1].Style[HtmlTextWriterStyle.BorderWidth] = "1";
            tBoookingMonthView.Rows[1].Style[HtmlTextWriterStyle.BorderColor] = "#507cd1";
        }
        if (tBoookingMonthView.Rows.Count > 2)
        {
            tBoookingMonthView.Rows[2].Style[HtmlTextWriterStyle.BorderWidth] = "1";
            tBoookingMonthView.Rows[2].Style[HtmlTextWriterStyle.BorderColor] = "#507cd1";
        }

        tBoookingMonthView.CellPadding = 2;
        //tblBookingView.BorderStyle = BorderStyle.Outset;
        tBoookingMonthView.Font.Size = 7;
        tBoookingMonthView.Font.Name = "Verdana";
        tBoookingMonthView.GridLines = GridLines.Both;
        return tBoookingMonthView;
    }

    private void AddMonthChartToPanel(Table tBookingMonthView)
    {
        if (pnlBookingMonthView.Controls.Count > 0)
            pnlBookingMonthView.Controls.RemoveAt(0);
        pnlBookingMonthView.Controls.AddAt(0, tBookingMonthView);
        SessionServices.BookingChart_TableMonthChart = tBookingMonthView;
    }
        
    protected void btnFullView_Click(object sender, EventArgs e)
    {
        Response.Redirect("BookingChartView.aspx");
    }
}
