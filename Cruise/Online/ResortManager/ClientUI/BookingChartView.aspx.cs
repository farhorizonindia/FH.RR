using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common.DataEntities.Client;

public partial class _Default : ClientBasePage
{
    const int GRIDSTARTCOL = 8;

    private int _totalDaysInChart = 31;
    private int _gridEndCol = 39;  //TOTALDAYSINCHART + 8 COLS FOR STATIC DATA 

    public int TotalDaysInChart
    {
        get { return _totalDaysInChart; }
        set { _totalDaysInChart = value; }
    }

    public int GridEndCol
    {
        get { return TotalDaysInChart + GRIDSTARTCOL; }
    }

    #region Event handlers

    protected void Page_Load(object sender, EventArgs e)
    {
        Table tBookingChart = new Table();
        DateTime today;
        DateTime thisMonth;
        if (!IsPostBack)
        {
            SessionServices.BookingChart_TreeDTO = null;
            SessionServices.BookingChart_TreeType = null;
            SessionServices.BookingChart_TableBookingTable = null;
            FillTree();

            today = GF.GetDate();
            TotalDaysInChart = DateTime.DaysInMonth(today.Year, today.Month);
            thisMonth = DateTime.Parse(today.Year + "/" + today.Month + "/" + "01");
            txtFromDate.Text = GF.GetMonthName(thisMonth.Month) + " " + thisMonth.Year.ToString();

            //txtStartDate.Text = "dd-mmm-yyyy";
            tBookingChart = PrepareBookingChartHeader(thisMonth);
            tBookingChart = FormatTable(tBookingChart);
            AddChartToPanel(tBookingChart);
        }
        if (IsPostBack == true)
        {
            if (SessionServices.BookingChart_TreeDTO == null || SessionServices.BookingChart_TreeType == string.Empty)
                FillTree();

            tBookingChart = (Table)SessionServices.BookingChart_TableBookingTable;
            if (tBookingChart != null)
            {
                tBookingChart = FormatTable(tBookingChart);
                AddChartToPanel(tBookingChart);
            }
        }
        AddAttributes();
        tBookingChart = null;
    }

    private void Page_LoadComplete(object sender, EventArgs e)
    {
        //RegisterToolTip();
    }

    private void FillTree()
    {
        string treeType = string.Empty;
        if (SessionServices.BookingChart_TreeDTO == null)
            FillTreeView();
        treeType = SessionServices.BookingChart_TreeType;
        switch (treeType)
        {
            case "R_PT_P":
                FillTree_R_PT_P();
                break;
            case "PT_R_P":
                FillTree_PT_R_P();
                break;
            case "R_P":
                FillTree_R_P();
                break;
            case "P":
                FillTree_P();
                break;
            default:
                FillTree_R_PT_P();
                break;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void btnMonthView_Click(object sender, EventArgs e)
    {
        Response.Redirect("BookingChartMonthView.aspx");

    }

    #region TreeRelated

    protected void tvRegions_SelectedNodeChanged(object sender, EventArgs e)
    {
        int RegionId = 0;
        int AccomTypeId = 0;
        int AccomId = 0;
        string sTreeType = "";
        if (SessionServices.BookingChart_TreeType != null && SessionServices.BookingChart_TreeType != string.Empty)
        {
            sTreeType = SessionServices.BookingChart_TreeType;
            if (sTreeType == "R_PT_P")
            {
                #region "R_PT_P"
                if (tvRegions.SelectedNode.Depth == 0)
                {
                    RegionId = Convert.ToInt32(tvRegions.SelectedNode.Value);
                    AccomTypeId = 0;
                    AccomId = 0;
                    //BookingChartView(Convert.ToInt32(tvRegions.SelectedNode.Value), 0, 0);
                }
                if (tvRegions.SelectedNode.Depth == 1)
                {
                    RegionId = Convert.ToInt32(tvRegions.SelectedNode.Parent.Value);
                    AccomTypeId = Convert.ToInt32(tvRegions.SelectedNode.Value);
                    AccomId = 0;
                    //BookingChartView(, , 0);
                }
                if (tvRegions.SelectedNode.Depth == 2)
                {
                    RegionId = Convert.ToInt32(tvRegions.SelectedNode.Parent.Parent.Value);
                    AccomTypeId = Convert.ToInt32(tvRegions.SelectedNode.Parent.Value);
                    AccomId = Convert.ToInt32(tvRegions.SelectedNode.Value);
                    //BookingChartView(, , );
                }
                #endregion "R_PT_P"
            }
            else if (sTreeType == "R_P")
            {
                #region "R_P"
                if (tvRegions.SelectedNode.Depth == 0)
                {
                    RegionId = Convert.ToInt32(tvRegions.SelectedNode.Value);
                    AccomTypeId = 0;
                    AccomId = 0;
                }
                if (tvRegions.SelectedNode.Depth == 1)
                {
                    RegionId = 0;
                    AccomTypeId = 0;
                    AccomId = Convert.ToInt32(tvRegions.SelectedNode.Value);
                }
                #endregion "R_P"
            }
            else if (sTreeType == "P")
            {
                #region "P"
                if (tvRegions.SelectedNode.Depth == 0)
                {
                    RegionId = 0;
                    AccomTypeId = 0;
                    AccomId = Convert.ToInt32(tvRegions.SelectedNode.Value);
                }
                #endregion "P"
            }
            else if (sTreeType == "PT_R_P")
            {
                #region "PT_R_P"
                if (tvRegions.SelectedNode.Depth == 0)
                {
                    RegionId = 0;
                    AccomTypeId = Convert.ToInt32(tvRegions.SelectedNode.Value);
                    AccomId = 0;
                    //BookingChartView(Convert.ToInt32(tvRegions.SelectedNode.Value), 0, 0);
                }
                if (tvRegions.SelectedNode.Depth == 1)
                {
                    RegionId = Convert.ToInt32(tvRegions.SelectedNode.Value);
                    AccomTypeId = Convert.ToInt32(tvRegions.SelectedNode.Parent.Value);
                    AccomId = 0;
                    //BookingChartView(, , 0);
                }
                if (tvRegions.SelectedNode.Depth == 2)
                {
                    RegionId = Convert.ToInt32(tvRegions.SelectedNode.Parent.Value);
                    AccomTypeId = Convert.ToInt32(tvRegions.SelectedNode.Parent.Parent.Value);
                    AccomId = Convert.ToInt32(tvRegions.SelectedNode.Value);
                    //BookingChartView(, , );
                }
                #endregion "PT_R_P"
            }
        }

        SessionServices.BookingChart_RegionId = RegionId;
        SessionServices.BookingChart_AccomodTypeId = AccomTypeId;
        SessionServices.BookingChart_AccomId = AccomId;

        FillBookingChartView();
    }

    //protected void tvRegions_SelectedNodeChanged(object sender, EventArgs e)
    //{
    //    int RegionId = 0;
    //    int AccomTypeId = 0;
    //    int AccomId = 0;
    //    //DateTime SD;
    //    //DateTime ED;
    //    //if (SessionHandler"TreeArrangeBy"] != null)
    //    //  return;
    //    if (SessionHandler"TreeArrangeBy"] != null)
    //    {
    //        if (SessionHandler"TreeArrangeBy"].ToString() == "Region")
    //        {
    //            if (tvRegions.SelectedNode.Depth == 0)
    //            {
    //                RegionId = Convert.ToInt32(tvRegions.SelectedNode.Value);
    //                AccomTypeId = 0;
    //                AccomId = 0;
    //                //BookingChartView(Convert.ToInt32(tvRegions.SelectedNode.Value), 0, 0);
    //            }
    //            if (tvRegions.SelectedNode.Depth == 1)
    //            {
    //                RegionId = Convert.ToInt32(tvRegions.SelectedNode.Parent.Value);
    //                AccomTypeId = Convert.ToInt32(tvRegions.SelectedNode.Value);
    //                AccomId = 0;
    //                //BookingChartView(, , 0);
    //            }
    //            if (tvRegions.SelectedNode.Depth == 2)
    //            {
    //                RegionId = Convert.ToInt32(tvRegions.SelectedNode.Parent.Parent.Value);
    //                AccomTypeId = Convert.ToInt32(tvRegions.SelectedNode.Parent.Value);
    //                AccomId = Convert.ToInt32(tvRegions.SelectedNode.Value);
    //                //BookingChartView(, , );
    //            }

    //        }
    //    }
    //    if (SessionHandler"TreeArrangeBy"].ToString() != null)
    //    {
    //        if (SessionHandler"TreeArrangeBy"].ToString() == "Accom")
    //        {
    //            if (tvRegions.SelectedNode.Depth == 0)
    //            {
    //                RegionId = 0;
    //                AccomTypeId = Convert.ToInt32(tvRegions.SelectedNode.Value);
    //                AccomId = 0;
    //                //   BookingChartView(0, , 0);
    //            }
    //            if (tvRegions.SelectedNode.Depth == 1)
    //            {
    //                RegionId = Convert.ToInt32(tvRegions.SelectedNode.Value);
    //                AccomTypeId = Convert.ToInt32(tvRegions.SelectedNode.Parent.Value);
    //                AccomId = 0;
    //                //BookingChartView(, , 0);
    //            }
    //            if (tvRegions.SelectedNode.Depth == 2)
    //            {
    //                RegionId = Convert.ToInt32(tvRegions.SelectedNode.Parent.Value);
    //                AccomTypeId = Convert.ToInt32(tvRegions.SelectedNode.Parent.Parent.Value);
    //                AccomId = Convert.ToInt32(tvRegions.SelectedNode.Value);
    //            }
    //        }
    //    }
    //    //Response.Redirect("BookingChartView.aspx?regionid=" + RegionId + "&accomodationtypeid=" + AccomTypeId + "&accomodationid=" + AccomId);
    //    SessionHandler"RegionId"] = RegionId;
    //    SessionHandler"AccomodTypeId"] = AccomTypeId;
    //    SessionHandler"AccomId"] = AccomId;

    //    FillBookingChartView();
    //}

    #endregion TreeRelated

    #region ChartRelated
    protected void btnPrevMonth_Click(object sender, EventArgs e)
    {
        StartAfterMonths(-1);
    }
    protected void btnPrevDay_Click(object sender, EventArgs e)
    {
        StartAferDays(-1);
    }
    protected void btnNextDay_Click(object sender, EventArgs e)
    {
        StartAferDays(1);
    }
    protected void btnNextMonth_Click(object sender, EventArgs e)
    {
        StartAfterMonths(1);
    }
    protected void btnNextYear_Click(object sender, EventArgs e)
    {
        StartAfterMonths(12);
    }
    protected void btnPrevYear_Click(object sender, EventArgs e)
    {
        StartAfterMonths(-12);
    }
    protected void btnStartChartFrom_Click(object sender, EventArgs e)
    {
        DateTime result, sd, ed;
        int RegionId = 0;
        int AccomTypeId = 0;
        int AccomId = 0;

        if (txtStartDate.Text.Trim() == "dd-mm-yyyy")
            return;
        if (DateTime.TryParse(txtStartDate.Text, out result) == false)
            return;

        sd = DateTime.MinValue;
        ed = DateTime.MinValue;

        GetIdFromSession(out RegionId, out AccomTypeId, out AccomId);

        sd = result;
        ed = sd.AddMonths(1);
        PrepareBookingChartView(RegionId, AccomTypeId, AccomId, sd, ed);
    }
    #endregion ChartRelated

    #endregion Event handlers

    #region UserDefinedFunctions
    private void AddAttributes()
    {
        btnStartChartFrom.Attributes.Add("onclick", "return validateStartDate();");
    }

    private void StartAfterMonths(int Months)
    {
        Table t;
        DateTime StartDate;
        DateTime EndDate;
        t = (Table)SessionServices.BookingChart_TableBookingTable;
        if (t != null)
        {
            DateTime.TryParse(t.Rows[1].Cells[GRIDSTARTCOL].ID, out StartDate);
            if (StartDate != DateTime.MinValue)
            {
                StartDate = StartDate.AddMonths(Months);
                StartDate = DateTime.Parse(StartDate.Year.ToString() + "/" + StartDate.Month.ToString() + "/" + "01");
                TotalDaysInChart = DateTime.DaysInMonth(StartDate.Year, StartDate.Month);
                EndDate = DateTime.Parse(StartDate.Year.ToString() + "/" + StartDate.Month.ToString() + "/" + TotalDaysInChart.ToString());
                t.Rows[1].Cells[GRIDSTARTCOL].ID = StartDate.Year.ToString() + "-" + StartDate.Month.ToString("0#") + "-" + StartDate.Day.ToString("0#");
                t.Rows[1].Cells[t.Rows[1].Cells.Count - 1].ID = EndDate.Year.ToString() + "-" + EndDate.Month.ToString("0#") + "-" + EndDate.Day.ToString("0#");


                //t.Rows[1].Cells[GridEndCol - 1].ID = EndDate.Year.ToString() + "-" + EndDate.Month.ToString("0#") + "-" + EndDate.Day.ToString("0#");
                SessionServices.BookingChart_TableBookingTable = t;
                FillBookingChartView();
                //PrepareBookingChartView(0, 0, 0, StartingDate, StartingDate.AddMonths(Days));
                txtFromDate.Text = GF.GetMonthName(StartDate.Month) + " " + StartDate.Year.ToString();

            }
        }
    }
    private void StartAferDays(int Days)
    {
        //This Function will move the chart by adding the no. of paramter to the current start date
        //It can also receive -ive value as a paramter to move to previous dates.
        Table t;
        DateTime StartDate;
        DateTime EndDate;
        t = (Table)SessionServices.BookingChart_TableBookingTable;
        if (t != null)
        {
            DateTime.TryParse(t.Rows[1].Cells[GRIDSTARTCOL].ID, out StartDate);
            if (StartDate != DateTime.MinValue)
            {
                StartDate = StartDate.AddDays(Days);
                EndDate = StartDate.AddMonths(1).AddDays(-1);
                t.Rows[1].Cells[GRIDSTARTCOL].ID = StartDate.Year.ToString() + "-" + StartDate.Month.ToString("0#") + "-" + StartDate.Day.ToString("0#");
                int cellCount = t.Rows[1].Cells.Count - 1;
                t.Rows[1].Cells[cellCount].ID = EndDate.Year.ToString() + "-" + EndDate.Month.ToString("0#") + "-" + EndDate.Day.ToString("0#");
                SessionServices.BookingChart_TableBookingTable = t;
                FillBookingChartView();
                //PrepareBookingChartView(0, 0, 0, StartingDate, StartingDate.AddMonths(Days));
            }
        }
    }
    private void GetStartandEndDate(out DateTime StartDate, out DateTime EndDate)
    {
        StartDate = DateTime.MinValue;
        EndDate = DateTime.MinValue;
        Table t;
        try
        {
            t = (Table)SessionServices.BookingChart_TableBookingTable;
            if (t != null)
            {
                DateTime.TryParse(t.Rows[1].Cells[GRIDSTARTCOL].ID, out StartDate);
                int daysInMonth = DateTime.DaysInMonth(StartDate.Year, StartDate.Month);
                EndDate = StartDate.AddDays(daysInMonth);
                //DateTime.TryParse(t.Rows[1].Cells[t.Rows[1].Cells.Count - 1].ID, out EndDate);
            }
        }
        catch (Exception exp)
        {
            GF.LogError("BookingChartView.aspx.GetStartandEndDate", exp.Message);
        }
        finally
        {
            if (StartDate == DateTime.MinValue)
                StartDate = GF.GetDate();
            if (EndDate == DateTime.MinValue)
                EndDate = StartDate.AddDays(31);
        }
    }

    private void FillTreeView()
    {
        string sTreeType = "";
        BookingChartServices bookingChartServices;
        BookingChartTreeDTO[] oBookingChartTreeDTO;
        bookingChartServices = new BookingChartServices();

        oBookingChartTreeDTO = bookingChartServices.GetTreeData(out sTreeType);
        SessionServices.BookingChart_TreeDTO = oBookingChartTreeDTO;
        SessionServices.BookingChart_TreeType = sTreeType;
    }

    private void FillTree_R_PT_P()
    {
        string sPrevRegion = "", sPrevProductType = "", sPath = "";
        int indexRegion = 0, indexProductType = 0, indexProduct = 0;
        BookingChartTreeDTO[] oBCTD = null;
        TreeNode RegionNode = null, ProductTypeNode = null, ProductNode = null, newnode = null; //, ParentNode =null;
        tvRegions.Nodes.Clear();
        if (SessionServices.BookingChart_TreeDTO != null)
            oBCTD = SessionServices.BookingChart_TreeDTO;

        for (int i = 0; i < oBCTD.Length; i++)
        {
            sPath = oBCTD[i].Region.RegionId.ToString();
            RegionNode = tvRegions.FindNode(sPath);
            if (RegionNode == null)
            {
                newnode = new TreeNode();
                newnode.Text = oBCTD[i].Region.RegionName.ToString();
                newnode.Value = oBCTD[i].Region.RegionId.ToString();
                sPrevRegion = oBCTD[i].Region.RegionName;
                //newnode.ValuePath = oBCTD[i].Region.RegionId.ToString();
                RegionNode = newnode;
                tvRegions.Nodes.Add(RegionNode);
            }
            sPath = sPath + "/" + oBCTD[i].AccomodationType.AccomodationTypeId.ToString();
            ProductTypeNode = tvRegions.FindNode(sPath);
            if (ProductTypeNode == null)
            {
                newnode = new TreeNode();
                newnode.Text = oBCTD[i].AccomodationType.AccomodationType.ToString();
                newnode.Value = oBCTD[i].AccomodationType.AccomodationTypeId.ToString();
                //newnode.ValuePath = oBCTD[i].Region.RegionId.ToString() + @"\" + oBCTD[i].AccomodationType.AccomodationTypeId.ToString(); 
                sPrevProductType = oBCTD[i].AccomodationType.AccomodationType;
                ProductTypeNode = newnode;
                RegionNode.ChildNodes.AddAt(indexProductType, ProductTypeNode);
                indexRegion++;
            }
            sPath = sPath + "/" + oBCTD[i].Accomodation.AccomodationId.ToString();
            ProductNode = tvRegions.FindNode(sPath);
            if (ProductNode == null)
            {
                newnode = new TreeNode();
                newnode.Text = oBCTD[i].Accomodation.AccomodationName.ToString();
                newnode.Value = oBCTD[i].Accomodation.AccomodationId.ToString();
                ProductNode = newnode;
                //ProductTypeNode.ChildNodes.AddAt(indexProduct, ProductNode);
                ProductTypeNode.ChildNodes.Add(ProductNode);
                indexProduct++;
            }
        }
    }

    private void FillTree_PT_R_P()
    {
        string sPrevRegion = "", sPrevProductType = "", sPath = "";
        int indexRegion = 0, indexProduct = 0;  //, indexProductType = 0 ;
        BookingChartTreeDTO[] oBCTD = null;
        TreeNode RegionNode = null, ProductTypeNode = null, ProductNode = null, newnode = null; //, ParentNode = null;
        tvRegions.Nodes.Clear();
        if (SessionServices.BookingChart_TreeDTO != null)
            oBCTD = SessionServices.BookingChart_TreeDTO;

        for (int i = 0; i < oBCTD.Length; i++)
        {
            sPath = oBCTD[i].AccomodationType.AccomodationTypeId.ToString();
            ProductTypeNode = tvRegions.FindNode(sPath);
            if (ProductTypeNode == null)
            {
                newnode = new TreeNode();
                newnode.Text = oBCTD[i].AccomodationType.AccomodationType.ToString();
                newnode.Value = oBCTD[i].AccomodationType.AccomodationTypeId.ToString();
                //newnode.ValuePath = oBCTD[i].Region.RegionId.ToString() + @"\" + oBCTD[i].AccomodationType.AccomodationTypeId.ToString(); 
                sPrevProductType = oBCTD[i].AccomodationType.AccomodationType;
                ProductTypeNode = newnode;
                tvRegions.Nodes.Add(ProductTypeNode);

            }

            sPath = sPath + "/" + oBCTD[i].Region.RegionId.ToString();
            RegionNode = tvRegions.FindNode(sPath);
            if (RegionNode == null)
            {
                newnode = new TreeNode();
                newnode.Text = oBCTD[i].Region.RegionName.ToString();
                newnode.Value = oBCTD[i].Region.RegionId.ToString();
                sPrevRegion = oBCTD[i].Region.RegionName;
                //newnode.ValuePath = oBCTD[i].Region.RegionId.ToString();
                RegionNode = newnode;
                //ProductTypeNode.ChildNodes.AddAt(indexRegion, RegionNode);
                ProductTypeNode.ChildNodes.Add(RegionNode);
                indexRegion++;
            }

            sPath = sPath + "/" + oBCTD[i].Accomodation.AccomodationId.ToString();
            ProductNode = tvRegions.FindNode(sPath);
            if (ProductNode == null)
            {
                newnode = new TreeNode();
                newnode.Text = oBCTD[i].Accomodation.AccomodationName.ToString();
                newnode.Value = oBCTD[i].Accomodation.AccomodationId.ToString();
                ProductNode = newnode;
                //ProductTypeNode.ChildNodes.AddAt(indexProduct, ProductNode);
                RegionNode.ChildNodes.AddAt(indexProduct, ProductNode);
            }
        }
    }

    private void FillTree_R_P()
    {
        string sPrevRegion = "", sPath = ""; //sPrevProductType = "", -
        int indexProduct = 0; //indexRegion = 0, indexProductType = 0; 
        BookingChartTreeDTO[] oBCTD = null;
        TreeNode RegionNode = null, ProductNode = null, newnode = null; //, ProductTypeNode = null, ParentNode = null;
        tvRegions.Nodes.Clear();
        if (SessionServices.BookingChart_TreeDTO != null)
            oBCTD = SessionServices.BookingChart_TreeDTO;

        for (int i = 0; i < oBCTD.Length; i++)
        {
            sPath = oBCTD[i].Region.RegionId.ToString();
            RegionNode = tvRegions.FindNode(sPath);
            if (RegionNode == null)
            {
                newnode = new TreeNode();
                newnode.Text = oBCTD[i].Region.RegionName.ToString();
                newnode.Value = oBCTD[i].Region.RegionId.ToString();
                sPrevRegion = oBCTD[i].Region.RegionName;
                //newnode.ValuePath = oBCTD[i].Region.RegionId.ToString();
                RegionNode = newnode;
                tvRegions.Nodes.Add(RegionNode);
            }
            //sPath = sPath + "/" + oBCTD[i].AccomodationType.AccomodationTypeId.ToString();
            //ProductTypeNode = tvRegions.FindNode(sPath);
            //if (ProductTypeNode == null)
            //{
            //    newnode = new TreeNode();
            //    newnode.Text = oBCTD[i].AccomodationType.AccomodationType.ToString();
            //    newnode.Value = oBCTD[i].AccomodationType.AccomodationTypeId.ToString();
            //    //newnode.ValuePath = oBCTD[i].Region.RegionId.ToString() + @"\" + oBCTD[i].AccomodationType.AccomodationTypeId.ToString(); 
            //    sPrevProductType = oBCTD[i].AccomodationType.AccomodationType;
            //    ProductTypeNode = newnode;
            //    RegionNode.ChildNodes.AddAt(indexProductType, ProductTypeNode);
            //    indexRegion++;
            //}
            sPath = sPath + "/" + oBCTD[i].Accomodation.AccomodationId.ToString();
            ProductNode = tvRegions.FindNode(sPath);
            if (ProductNode == null)
            {
                newnode = new TreeNode();
                newnode.Text = oBCTD[i].Accomodation.AccomodationName.ToString();
                newnode.Value = oBCTD[i].Accomodation.AccomodationId.ToString();
                ProductNode = newnode;
                //ProductTypeNode.ChildNodes.AddAt(indexProduct, ProductNode);                
                //RegionNode.ChildNodes.AddAt(indexProduct, ProductNode);
                RegionNode.ChildNodes.Add(ProductNode);
                indexProduct++;
            }
        }
    }

    private void FillTree_P()
    {
        string sPath = ""; //sPrevRegion = "", sPrevProductType = "", 
        int indexProduct = 0; //indexRegion = 0, indexProductType = 0, 
        BookingChartTreeDTO[] oBCTD = null;
        TreeNode ProductNode = null, newnode = null; //, RegionNode = null, ProductTypeNode = null, ParentNode = null;
        tvRegions.Nodes.Clear();
        if (SessionServices.BookingChart_TreeDTO != null)
            oBCTD = SessionServices.BookingChart_TreeDTO;

        for (int i = 0; i < oBCTD.Length; i++)
        {
            sPath = sPath + "/" + oBCTD[i].Accomodation.AccomodationId.ToString();
            ProductNode = tvRegions.FindNode(sPath);
            if (ProductNode == null)
            {
                newnode = new TreeNode();
                newnode.Text = oBCTD[i].Accomodation.AccomodationName.ToString();
                newnode.Value = oBCTD[i].Accomodation.AccomodationId.ToString();
                ProductNode = newnode;
                //ProductTypeNode.ChildNodes.AddAt(indexProduct, ProductNode);                
                //RegionNode.ChildNodes.AddAt(indexProduct, ProductNode);
                //RegionNode.ChildNodes.Add(ProductNode);
                tvRegions.Nodes.Add(ProductNode);
                indexProduct++;
            }
        }
    }

    private void FillBookingChartView()
    {
        int RegionId = 0;
        int AccomTypeId = 0;
        int AccomId = 0;
        DateTime SD, ED;

        GetIdFromSession(out RegionId, out AccomTypeId, out AccomId);

        GetStartandEndDate(out SD, out ED);
        PrepareBookingChartView(RegionId, AccomTypeId, AccomId, SD, ED);
    }

    private static void GetIdFromSession(out int RegionId, out int AccomTypeId, out int AccomId)
    {
        RegionId = 0;
        AccomTypeId = 0;
        AccomId = 0;
        if (SessionServices.BookingChart_RegionId != -1)
            RegionId = SessionServices.BookingChart_RegionId;
        if (SessionServices.BookingChart_AccomodTypeId != -1)
            AccomTypeId = SessionServices.BookingChart_AccomodTypeId;
        if (SessionServices.BookingChart_AccomId != -1)
            AccomId = SessionServices.BookingChart_AccomId;
    }

    private void PrepareBookingChartView(int RegionId, int AccomodationTypeId, int AccomodationId, DateTime StartDate, DateTime EndDate)
    {
        BookingChartServices bookingChartServices;
        BookingChartDTO[] oBookingChartDTO = null;
        BookingChartDTO[] oRoomMaintenance = null;
        RoomBookingDateWiseDTO[] oRoomBookingDateWiseDTO = null;
        Table tblBookingView;

        if (RegionId != 0 || AccomodationTypeId != 0 || AccomodationId != 0)
        {
            bookingChartServices = new BookingChartServices();
            //oAccomTypeData = oBookingChartViewManager.GetBookingChart(RegionId, AccomodationTypeId, AccomodationId, StartDate, EndDate);
            oBookingChartDTO = bookingChartServices.GetRoomDetailsNew(RegionId, AccomodationTypeId, AccomodationId);
            if (oBookingChartDTO != null)
            {
                if (oBookingChartDTO.Length > 0)
                {
                    oRoomBookingDateWiseDTO = bookingChartServices.GetBookingDataForChart(AccomodationTypeId, RegionId, AccomodationId, StartDate, EndDate);
                    oRoomMaintenance = bookingChartServices.GetRoomDetmaintenance(AccomodationTypeId, RegionId, AccomodationId, StartDate, EndDate);
                }
            }
            bookingChartServices = null;
        }
        tblBookingView = PrepareBookingChartHeader(StartDate);

        if (oBookingChartDTO != null)
            tblBookingView = PrepareBookingChartEmptyCells(tblBookingView, oBookingChartDTO);
        if (oRoomBookingDateWiseDTO != null)
        {
            tblBookingView = PrepareBookingChartBookingCells(tblBookingView, oRoomBookingDateWiseDTO);
        }

        if (oRoomMaintenance != null)
        {
            tblBookingView = PrepareBookingChartMaintenanceCells(tblBookingView, oRoomMaintenance);
        }

        tblBookingView = FormatTable(tblBookingView);

        AddChartToPanel(tblBookingView);
    }

    private Table PrepareBookingChartHeader(DateTime StartDate)
    {
        Table tblBookingView;
        TableRow thr;
        TableRow thrMonthRow;

        tblBookingView = new Table();
        thr = GenerateHeaderData(StartDate);
        tblBookingView.Rows.AddAt(0, thr);
        thrMonthRow = GenerateHeaderMonthData(thr);
        tblBookingView.Rows.AddAt(0, thrMonthRow);
        return tblBookingView;
    }

    private Table FormatTable(Table tblBookingView)
    {
        if (tblBookingView.Rows.Count > 1)
        {
            tblBookingView.Rows[1].Style[HtmlTextWriterStyle.BorderWidth] = "1";
            tblBookingView.Rows[1].Style[HtmlTextWriterStyle.BorderColor] = "#507cd1";
        }
        if (tblBookingView.Rows.Count > 2)
        {
            tblBookingView.Rows[2].Style[HtmlTextWriterStyle.BorderWidth] = "1";
            tblBookingView.Rows[2].Style[HtmlTextWriterStyle.BorderColor] = "#507cd1";
        }
        tblBookingView.Attributes.Add("class", "tblBookingView");
        //tblBookingView.Style[HtmlTextWriterStyle.BorderWidth] = "1";
        //tblBookingView.Style[HtmlTextWriterStyle.BorderColor] = "#507cd1";
        tblBookingView.CellPadding = 2;
        //tblBookingView.Style[HtmlTextWriterStyle.FontSize] = "x-small";        
        //tblBookingView.Font.Name = "Verdana";
        tblBookingView.GridLines = GridLines.Both;
        return tblBookingView;
    }

    private TableCell FormatEmptyCells(DateTime CurrentCellDate, int AccomodationTypeId, int AccomodationId)
    {
        TableCell oTC;
        oTC = new TableCell();
        string sURL = "Booking.aspx?bid=0" +
                "&AccomTypeId=" + AccomodationTypeId.ToString() +
                "&AccomId=" + AccomodationId.ToString() +
                "&sdate=" + CurrentCellDate.Year.ToString() + "/" + CurrentCellDate.Month.ToString() + "/" + CurrentCellDate.Day.ToString();
        oTC.Text = "<a href='" + sURL + "' style='text-decoration:none' class='nav' Target='_blank'>A</a>";
        oTC.AccessKey = "N";
        oTC.Style[HtmlTextWriterStyle.BackgroundColor] = "#FFFFFF";
        oTC.Style[HtmlTextWriterStyle.Color] = "#507cd1";
        return oTC;
    }

    private TableCell FormatBookedCells(int BookingId, string BookingCode)
    {
        TableCell oTC;
        oTC = new TableCell();
        string sURL = "Booking.aspx?bid=" + BookingId.ToString();
        oTC.Text = "<a href='" + sURL + "' style='text-decoration:none; color=#FFFFFF' Target='_blank'>" + BookingCode + "</a>";
        oTC.AccessKey = "B";
        oTC.BackColor = System.Drawing.Color.SaddleBrown;
        return oTC;
    }

    private TableRow GenerateHeaderData(DateTime StartDate)
    {
        TableRow thr;
        TableCell thc;
        thr = new TableRow();
        string ColumnCaption;

        #region FixedColumnHeaders
        thc = new TableCell();
        thc.ID = "AccomTypeId";
        thc.Text = "Accom Type Id";
        // thc.AbbreviatedText = "Accom Type Id";
        thc.Visible = false;
        thc.Attributes.Add("class", "dayheader");
        thr.Cells.Add(thc);

        thc = new TableCell();
        thc.ID = "AccomType";
        thc.Text = "Accom Type";
        //thc.AbbreviatedText = "Accom Type";
        thc.Visible = false;
        thc.Attributes.Add("class", "dayheader");
        thr.Cells.Add(thc);

        thc = new TableCell();
        thc.ID = "AccomId";
        thc.Text = "Accom Id";
        //thc.AbbreviatedText = "Accom Id";
        thc.Visible = false;
        thc.Attributes.Add("class", "dayheader");
        thr.Cells.Add(thc);

        thc = new TableCell();
        thc.ID = "Accom";
        thc.Text = "Accom";
        thc.Attributes.Add("class", "dayheader");
        thr.Cells.Add(thc);

        thc = new TableCell();
        thc.ID = "RegionId";
        thc.Text = "Region Id";
        thc.Visible = false;
        thc.Attributes.Add("class", "dayheader");
        thr.Cells.Add(thc);

        thc = new TableCell();
        thc.ID = "RoomCategory";
        thc.Text = "Category";
        thc.Attributes.Add("class", "dayheader");
        thr.Cells.Add(thc);

        thc = new TableCell();
        thc.ID = "RoomNo";
        thc.Text = "Room No";
        thc.Attributes.Add("class", "dayheader");
        thr.Cells.Add(thc);

        thc = new TableCell();
        thc.ID = "Sequence";
        thc.Text = "Sequence";
        thc.Visible = false;
        thc.Attributes.Add("class", "dayheader");
        thr.Cells.Add(thc);
        #endregion FixedColumnHeaders

        for (int i = 0; i < TotalDaysInChart; i++)
        {
            ColumnCaption = StartDate.DayOfWeek.ToString().Substring(0, 2);
            ColumnCaption += Environment.NewLine;
            if (StartDate.Day.ToString().Trim().Length == 1)
                ColumnCaption += "0" + StartDate.Day.ToString();
            else
                ColumnCaption += StartDate.Day.ToString();

            thc = new TableCell();
            thc.Text = ColumnCaption;
            if (StartDate.DayOfWeek == DayOfWeek.Sunday)
                thc.Attributes.Add("class", "dayheadersunday");
            else
                thc.Attributes.Add("class", "dayheader");

            thc.ID = StartDate.Year.ToString() + "-" + StartDate.Month.ToString("0#") + "-" + StartDate.Day.ToString("0#");
            thr.Cells.Add(thc);
            StartDate = StartDate.AddDays(1);
        }
        return thr;
    }

    private TableRow GenerateHeaderMonthData(TableRow thr)
    {
        TableRow thrm;
        TableCell tc;
        TableCell tcm;
        DateTime dt;
        string sVal;
        int daysLeftInThisMonth = 0;
        thrm = new TableRow();

        for (int i = 0; i < thr.Cells.Count; i++)
        {
            tc = thr.Cells[i];
            DateTime.TryParse(tc.ID.ToString(), out dt);
            if (dt != DateTime.MinValue)
            {//7 is the colun from where dates starts
                if (dt.Day == 1 || i == GRIDSTARTCOL)
                {
                    tcm = new TableCell();
                    daysLeftInThisMonth = (DateTime.DaysInMonth(dt.Year, dt.Month) - dt.Day) + 1;
                    i = i + (daysLeftInThisMonth - 1);
                    tcm.ColumnSpan = daysLeftInThisMonth;

                    sVal = GF.GetMonthName(dt.Month);
                    sVal += " " + dt.Year.ToString();

                    tcm.Attributes.Add("class", "monthheader");
                    tcm.Text = sVal;
                    thrm.Cells.Add(tcm);
                }
                else
                {
                    tcm = new TableCell();
                    tcm.Attributes.Add("class", "monthheader");
                    thrm.Cells.Add(tcm);
                }
            }
            else
            {
                tcm = new TableCell();
                tcm.Attributes.Add("class", "monthheader");
                if (tc.Visible == false)
                    tcm.Visible = false;
                thrm.Cells.Add(tcm);
            }
        }
        //thrm.Cells.Add(tcm);
        return thrm;
    }

    private Table PrepareBookingChartEmptyCells(Table tblBookingView, BookingChartDTO[] oBookingChartDTO)
    {
        TableRow DataRow;
        TableCell tc;
        DateTime currCellDT;
        string sAccessKey = "col";
        string cellDate = "";
        int iAccesskeyCounter = 0;
        int startcol = GRIDSTARTCOL;

        if (oBookingChartDTO != null)
        {
            for (int i = 0; i < oBookingChartDTO.Length; i++)
            {
                startcol = GRIDSTARTCOL;
                //                sPrevBookingCode = "";
                #region AccomDetailsCells
                DataRow = new TableRow();
                tc = new TableCell();
                tc.Text = oBookingChartDTO[i].AccomodationTypeId.ToString();
                //tc.AccessKey = sAccessKey + iAccesskeyCounter.ToString();
                tc.Attributes.Add("class", "availablebookingcell");
                tc.ID = sAccessKey + iAccesskeyCounter.ToString();
                tc.Visible = false;
                iAccesskeyCounter++;
                DataRow.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = oBookingChartDTO[i].AccomodationType.ToString();
                //tc.AccessKey = sAccessKey + iAccesskeyCounter.ToString();
                tc.Attributes.Add("class", "availablebookingcell");
                tc.ID = sAccessKey + iAccesskeyCounter.ToString();
                tc.Visible = false;
                iAccesskeyCounter++;
                DataRow.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = oBookingChartDTO[i].AccomodationId.ToString();
                //tc.AccessKey = sAccessKey + iAccesskeyCounter.ToString();
                tc.Attributes.Add("class", "availablebookingcell");
                tc.ID = sAccessKey + iAccesskeyCounter.ToString();
                tc.Visible = false;
                iAccesskeyCounter++;
                DataRow.Cells.Add(tc);

                tc = new TableCell();
                //tc.Text = oBCDN[i].AccomodationName.ToString();
                tc.Text = Convert.ToString(oBookingChartDTO[i].AccomInitial);
                tc.Attributes.Add("class", "availablebookingcell");
                tc.ID = sAccessKey + iAccesskeyCounter.ToString();
                iAccesskeyCounter++;
                DataRow.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = oBookingChartDTO[i].RegionId.ToString();
                tc.Attributes.Add("class", "availablebookingcell");
                tc.ID = sAccessKey + iAccesskeyCounter.ToString();
                tc.Visible = false;
                iAccesskeyCounter++;
                DataRow.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = oBookingChartDTO[i].RoomCategoryAlias;
                tc.Attributes.Add("class", "availablebookingcell");
                tc.ID = sAccessKey + iAccesskeyCounter.ToString();
                iAccesskeyCounter++;
                DataRow.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = oBookingChartDTO[i].RoomNo.ToString();
                tc.Attributes.Add("class", "availablebookingcell");
                tc.ID = sAccessKey + iAccesskeyCounter.ToString();
                iAccesskeyCounter++;
                DataRow.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = oBookingChartDTO[i].RegionName.ToString();
                tc.Attributes.Add("class", "availablebookingcell");
                tc.ID = sAccessKey + iAccesskeyCounter.ToString();
                tc.Visible = false;
                iAccesskeyCounter++;
                DataRow.Cells.Add(tc);
                #endregion AccomDetailsCells

                for (int y = startcol; y < GridEndCol; y++)
                {
                    tc = new TableCell();
                    currCellDT = DateTime.MinValue;
                    currCellDT = Convert.ToDateTime(tblBookingView.Rows[1].Cells[y].ID);
                    tc = FormatEmptyCells(currCellDT, oBookingChartDTO[i].AccomodationTypeId, oBookingChartDTO[i].AccomodationId);
                    //tc.ID = sAccessKey + iAccesskeyCounter.ToString();
                    cellDate = currCellDT.Year.ToString() + currCellDT.Month.ToString("0#") + currCellDT.Day.ToString("0#");
                    tc.ID = Constants.CHART_ROOM_CELL + oBookingChartDTO[i].RoomNo.ToString() + cellDate;
                    tc.Attributes.Add("class", "availablebookingcell");
                    iAccesskeyCounter++;
                    DataRow.Cells.Add(tc);
                }

                tblBookingView.Rows.Add(DataRow);
            }
        }

        return tblBookingView;
    }

    private Table PrepareBookingChartBookingCells(Table tblBookingView, RoomBookingDateWiseDTO[] oRBDDW)
    {
        DateTime startDate, currentDate, endDate;
        string cellId = "", bookingDetailContainingId = "";
        TableCell tc = null;
        Control c = null;
        HtmlGenericControl divRoomMain;
        List<ChartViewBookingDetail> bookingDetailList = new List<ChartViewBookingDetail>();

        //delegate(ChartViewBookingDetail bd) { return bd.key == bookingDetail.key; })
        ArrayList arrList = new ArrayList(oRBDDW);
        arrList.Sort();

        if (oRBDDW != null)
        {
            #region Populate the BookingsDetailList
            for (int i = 0; i < oRBDDW.Length; i++)
            {
                startDate = oRBDDW[i].Startdate;
                endDate = oRBDDW[i].Enddate;

                //This line is being added because on the chart they don't want to show that the room is 
                // booked on the last day of booking, and it is available for booking.
                //ed = ed.AddDays(-1); //This do not change any logic but only change the onscreen display.

                for (currentDate = startDate; currentDate < endDate; currentDate = currentDate.AddDays(1))
                {
                    ChartViewBookingDetail bookingDetail = new ChartViewBookingDetail();
                    bookingDetail.key = oRBDDW[i].RoomNo + GF.GetYYYYMMDD(currentDate);
                    bookingDetail.bookingDetailHtml = PrepareBookingDetailHTML(oRBDDW[i]);

                    bookingDetail.BookingStatusType = (BookingStatusTypes)Enum.Parse(typeof(BookingStatusTypes), oRBDDW[i].BookingStatusType);
                    //bookingDetail.bookingStatusType = oRBDDW[i].BookingStatusType;

                    if (bookingDetailList.Exists(delegate(ChartViewBookingDetail bd) { return bd.key == bookingDetail.key; }))
                    {
                        ChartViewBookingDetail currentBookingDetail;
                        currentBookingDetail = bookingDetailList.Find(delegate(ChartViewBookingDetail bd) { return bd.key == bookingDetail.key; });
                        int index = bookingDetailList.IndexOf(currentBookingDetail);
                        currentBookingDetail.bookingDetailHtml += "</br><span/>" + bookingDetail.bookingDetailHtml;

                        //Override the low level Booking Status with High Level Booking Status.
                        if (bookingDetail.BookingStatusType == BookingStatusTypes.WAITLISTED)
                        {
                            currentBookingDetail.BookingStatusType = bookingDetail.BookingStatusType;
                        }
                        else if (bookingDetail.BookingStatusType == BookingStatusTypes.CONFIRMED &&
                                    currentBookingDetail.BookingStatusType != BookingStatusTypes.WAITLISTED)
                        {
                            currentBookingDetail.BookingStatusType = bookingDetail.BookingStatusType;
                        }
                        else if (bookingDetail.BookingStatusType == BookingStatusTypes.BOOKED &&
                                    currentBookingDetail.BookingStatusType != BookingStatusTypes.CONFIRMED &&
                                    currentBookingDetail.BookingStatusType != BookingStatusTypes.WAITLISTED)
                        {
                            currentBookingDetail.BookingStatusType = bookingDetail.BookingStatusType;
                        }
                        bookingDetailList[index] = currentBookingDetail;
                    }
                    else
                    {
                        bookingDetailList.Add(bookingDetail);
                    }
                }
            }
            #endregion

            #region Prepare The BookingCells
            foreach (ChartViewBookingDetail bookingDetail in bookingDetailList)
            {
                cellId = Constants.CHART_ROOM_CELL + bookingDetail.key;
                c = FindControl(tblBookingView, cellId);
                if (c != null)
                {
                    if (c.GetType().ToString().CompareTo("System.Web.UI.WebControls.TableCell") == 0)
                    {
                        tc = (TableCell)c;
                        tc.ID = cellId;
                        if (tc.Controls.Count == 0) //If this is the first booking details for this date.
                        {
                            divRoomMain = PrepareRoomBookingDiv(bookingDetail.key, bookingDetail,"", out bookingDetailContainingId);

                            if (bookingDetail.bookingDetailHtml.Contains("span")) //This is a hack to find if there is single booking or multibooking
                            {
                                tc.Attributes.Add("class", "multibookingcell");
                            }
                            else if (tc.Attributes["class"] != "multibookingcell")
                            {
                                tc.Attributes.Add("class", "singlebookingcell");
                            }
               
                            divRoomMain.Attributes.Add("onmouseover", "javascript:showRoomBookings('" + tc.ClientID + "', '" + bookingDetailContainingId + "')");
                            divRoomMain.Attributes.Add("onmouseout", "javascript:hideRoomBookings('" + bookingDetailContainingId + "')");

                            //HtmlGenericControl div = (HtmlGenericControl)divRoomMain.Controls[0];
                            //div.Attributes.Add("onmouseover", "javascript:showRoomBookings('" + tc.ClientID + "', '" + bookingDetailContainingId + "')");
                            //div.Attributes.Add("onmouseout", "javascript:hideRoomBookings('" + bookingDetailContainingId + "')");

                            tc.Controls.Add(divRoomMain);
                        }
                    }
                }
            }
            #endregion
        }
        return tblBookingView;
    }

    private Table PrepareBookingChartMaintenanceCells(Table tblBookingView, BookingChartDTO[] oRBDDW)
    {
        DateTime startDate, currentDate, endDate;
        string cellId = "", bookingDetailContainingId = "";
        TableCell tc = null;
        Control c = null;
        HtmlGenericControl divRoomMain;
        List<ChartViewBookingDetail> bookingDetailList = new List<ChartViewBookingDetail>();

        //delegate(ChartViewBookingDetail bd) { return bd.key == bookingDetail.key; })
        ArrayList arrList = new ArrayList(oRBDDW);
      //  arrList.Sort();

        if (oRBDDW != null)
        {
            #region Populate the BookingsDetailList
            for (int i = 0; i < oRBDDW.Length; i++)
            {
                startDate = oRBDDW[i].FromDt;
                endDate = oRBDDW[i].Todt;

                //This line is being added because on the chart they don't want to show that the room is 
                // booked on the last day of booking, and it is available for booking.
                //ed = ed.AddDays(-1); //This do not change any logic but only change the onscreen display.

                for (currentDate = startDate; currentDate < endDate; currentDate = currentDate.AddDays(1))
                {
                    ChartViewBookingDetail bookingDetail = new ChartViewBookingDetail();
                    bookingDetail.key = oRBDDW[i].RoomNo + GF.GetYYYYMMDD(currentDate);
                    bookingDetail.bookingDetailHtml = PrepareMaintenanceDetailsHtml(oRBDDW[i]);

                //    bookingDetail.BookingStatusType = "MAINTENANCE";
                    ////bookingDetail.bookingStatusType = oRBDDW[i].BookingStatusType;

                    //if (bookingDetailList.Exists(delegate(ChartViewBookingDetail bd) { return bd.key == bookingDetail.key; }))
                    //{
                    //    ChartViewBookingDetail currentBookingDetail;
                    //    currentBookingDetail = bookingDetailList.Find(delegate(ChartViewBookingDetail bd) { return bd.key == bookingDetail.key; });
                    //    int index = bookingDetailList.IndexOf(currentBookingDetail);
                    //    currentBookingDetail.bookingDetailHtml += "</br><span/>" + bookingDetail.bookingDetailHtml;

                    //    //Override the low level Booking Status with High Level Booking Status.
                    //    if (bookingDetail.BookingStatusType == BookingStatusTypes.WAITLISTED)
                    //    {
                    //        currentBookingDetail.BookingStatusType = bookingDetail.BookingStatusType;
                    //    }
                    //    else if (bookingDetail.BookingStatusType == BookingStatusTypes.CONFIRMED &&
                    //                currentBookingDetail.BookingStatusType != BookingStatusTypes.WAITLISTED)
                    //    {
                    //        currentBookingDetail.BookingStatusType = bookingDetail.BookingStatusType;
                    //    }
                    //    else if (bookingDetail.BookingStatusType == BookingStatusTypes.BOOKED &&
                    //                currentBookingDetail.BookingStatusType != BookingStatusTypes.CONFIRMED &&
                    //                currentBookingDetail.BookingStatusType != BookingStatusTypes.WAITLISTED)
                    //    {
                    //        currentBookingDetail.BookingStatusType = bookingDetail.BookingStatusType;
                    //    }
                    //    bookingDetailList[index] = currentBookingDetail;
                    //}
                    //else
                    //{
                        bookingDetailList.Add(bookingDetail);
                    //}
                }
            }
            #endregion

            #region Prepare The BookingCells
            foreach (ChartViewBookingDetail bookingDetail in bookingDetailList)
            {
                cellId = Constants.CHART_ROOM_CELL + bookingDetail.key;
                c = FindControl(tblBookingView, cellId);
                if (c != null)
                {
                    if (c.GetType().ToString().CompareTo("System.Web.UI.WebControls.TableCell") == 0)
                    {
                        tc = (TableCell)c;
                        tc.ID = cellId;
                        if (tc.Controls.Count == 0) //If this is the first booking details for this date.
                        {
                            divRoomMain = PrepareRoomBookingDiv(bookingDetail.key, bookingDetail,"M", out bookingDetailContainingId);

                            //if (bookingDetail.bookingDetailHtml.Contains("span")) //This is a hack to find if there is single booking or multibooking
                            //{
                            //    tc.Attributes.Add("class", "multibookingcell");
                            //}
                            //else if (tc.Attributes["class"] != "multibookingcell")
                            //{
                            //    tc.Attributes.Add("class", "singlebookingcell");
                            //}

                            divRoomMain.Attributes.Add("onmouseover", "javascript:showRoomBookingsm('" + tc.ClientID + "', '" + bookingDetailContainingId + "')");
                            divRoomMain.Attributes.Add("onmouseout", "javascript:hideRoomBookings('" + bookingDetailContainingId + "')");

                            //HtmlGenericControl div = (HtmlGenericControl)divRoomMain.Controls[0];
                            //div.Attributes.Add("onmouseover", "javascript:showRoomBookings('" + tc.ClientID + "', '" + bookingDetailContainingId + "')");
                            //div.Attributes.Add("onmouseout", "javascript:hideRoomBookings('" + bookingDetailContainingId + "')");

                            tc.Controls.Add(divRoomMain);
                        }
                    }
                }
            }
            #endregion
        }
        return tblBookingView;
    }



    private static string PrepareBookingDetailHTML(RoomBookingDateWiseDTO oRoomBookingDateWiseData)
    {
        string sBookingDetailsHTML = "";
        //sBookingDetailsHTML += "<b>Booking Code: </b><a href='Booking.aspx?bid= " + oRoomBookingDateWiseData.BookingId.ToString() + "target='_blank'>" + oRoomBookingDateWiseData.BookingCode.ToString() + "</a><br/>";
        sBookingDetailsHTML +=  oRoomBookingDateWiseData.BookingCode.ToString() + "<br/>";
        sBookingDetailsHTML += "<b>Booking Reference: </b>" + oRoomBookingDateWiseData.BookingReference + "<br/>";
        sBookingDetailsHTML += "<b>Booking Status: </b>" + oRoomBookingDateWiseData.BookingStatusType + "<br/>";
        sBookingDetailsHTML += "<b>From:</b>" +  oRoomBookingDateWiseData.StartDateFormatted + " <b>To: </b>" +oRoomBookingDateWiseData.EndDateFormatted+ "<br/>";
       
        sBookingDetailsHTML += "<b>Agent: </b> " + oRoomBookingDateWiseData.AgentName + "<br/>";
        if (oRoomBookingDateWiseData.Chartered == true)
        {
            sBookingDetailsHTML += "<b>Booking Type:</b><font color=\"red\">Chartered</font><br/>";
        }
        sBookingDetailsHTML += "<b>No of Rooms: </b> " + oRoomBookingDateWiseData.NoofRooms + "<br/>";
        sBookingDetailsHTML += "<b>Pax: </b> " + oRoomBookingDateWiseData.paxStaying + "<br/>";


        sBookingDetailsHTML += "<hr/>";
        return sBookingDetailsHTML;
    }

    private static string PrepareMaintenanceDetailsHtml(BookingChartDTO oRoomMaintenance)
    {
        string maintenanceDetailsHTML = "";
        //sBookingDetailsHTML += "<b>Booking Code: </b><a href='Booking.aspx?bid= " + oRoomBookingDateWiseData.BookingId.ToString() + "target='_blank'>" + oRoomBookingDateWiseData.BookingCode.ToString() + "</a><br/>";
      
     
        maintenanceDetailsHTML += "<b>Status: </b> Under Maintenance<br/>";
        maintenanceDetailsHTML += "<b>Reason: </b> " + oRoomMaintenance.Reason + "<br/>";
        maintenanceDetailsHTML += "<b>From:</b>" + oRoomMaintenance.StartdtFormatted + " <b>To: </b>" + oRoomMaintenance.EndDtFormatted + "<br/>";

        //maintenanceDetailsHTML += "<b>Agent: </b> " + oRoomMaintenance.AgentName + "<br/>";
        //if (oRoomMaintenance.Chartered == true)
        //{
        //    maintenanceDetailsHTML += "<b>Booking Type:</b><font color=\"red\">Chartered</font><br/>";
        //}
        //maintenanceDetailsHTML += "<b>No of Rooms: </b> " + oRoomMaintenance.NoofRooms + "<br/>";
        //maintenanceDetailsHTML += "<b>Pax: </b> " + oRoomMaintenance.paxStaying + "<br/>";


        maintenanceDetailsHTML += "<hr/>";
        return maintenanceDetailsHTML;
    }

    private HtmlGenericControl PrepareRoomBookingDiv(string idSuffix, ChartViewBookingDetail bookingDetail,string flag, out string bookingSummaryContainingId)
    {
        HtmlGenericControl divRoomMain, divRoomHeader, divRoomDetail;
        string id = "";

        divRoomMain = new HtmlGenericControl("div");
        divRoomMain.ID = Constants.CHART_ROOM_CELL_DIV_MAIN + idSuffix;

        divRoomHeader = new HtmlGenericControl("div");
        divRoomHeader.ID = Constants.CHART_ROOM_CELL_DIV_HEADER + idSuffix;

        divRoomDetail = new HtmlGenericControl("div");
        divRoomDetail.ID = Constants.CHART_ROOM_CELL_DIV_DETAIL + idSuffix;
        id = Constants.CHART_ROOM_CELL_DIV_DETAIL + idSuffix;
        divRoomDetail.Style.Add(HtmlTextWriterStyle.Display, "none");
        if (flag != "M")
        {
            divRoomHeader.InnerText = Enum.GetName(typeof(BookingStatusTypes), bookingDetail.BookingStatusType).Substring(0, 1);
        }
        else
        {
            divRoomHeader.InnerText = "M";
            divRoomHeader.Attributes.Add("class", "maintainCell"); 
        }
        divRoomDetail.InnerHtml = bookingDetail.bookingDetailHtml;

        switch (bookingDetail.BookingStatusType)
        {
            case BookingStatusTypes.BOOKED:
                divRoomHeader.Attributes.Add("class", "bookedCell");
                break;
            case BookingStatusTypes.CONFIRMED:
                divRoomHeader.Attributes.Add("class", "confirmedCell");
               
                break;
            case BookingStatusTypes.WAITLISTED:
                divRoomHeader.Attributes.Add("class", "waitListedCell");
                break;
            case BookingStatusTypes.PROPOSED:
                divRoomHeader.Attributes.Add("class", "proposedCell");
                break;
            case BookingStatusTypes.CANCELLED:
                divRoomHeader.Attributes.Add("class", "cancelledCell");
                break;
            default:
                break;
        }

        bookingSummaryContainingId = id;

        divRoomMain.Controls.Add(divRoomHeader);
        divRoomMain.Controls.Add(divRoomDetail);

        /*if (bookingDetail.bookingDetailHtml.Contains("span")) //This is a hack to find if there is single booking or multibooking
            //if (BookingsLink.Contains("</br>"))
            divRoomHeader.Attributes.Add("class", "multibookingcell");
        else
            divRoomHeader.Attributes.Add("class", "singlebookingcell");*/

        return divRoomMain;
    }

    protected override Control FindControl(Control c, string ID)
    {
        Control cntrl = null;
        if (c != null)
        {
            if (c.HasControls() == false)
            {
                if (c.ID != null)
                {
                    if (c.ID.Replace("ABchkRoomNo", "chkRoomNo") == ID)
                        return c;
                    else
                        return null;
                }
            }
            else if (c.Controls.Count > 0)
            {
                for (int i = 0; i < c.Controls.Count; i++)
                {
                    cntrl = FindControl(c.Controls[i], ID);
                    if (cntrl != null)
                    {
                        break;
                    }
                }
            }
        }
        return cntrl;
    }

    private void AddChartToPanel(Table tBookingChart)
    {
        if (pnlBookingView.Controls.Count > 0)
        {
            for (int i = 0; i < pnlBookingView.Controls.Count; i++)
            {
                if (pnlBookingView.Controls[i].GetType().ToString() == "System.Web.UI.WebControls.Table")
                {
                    pnlBookingView.Controls.RemoveAt(i);
                }
            }
        }
        pnlBookingView.Controls.AddAt(0, tBookingChart);
        SessionServices.BookingChart_TableBookingTable = tBookingChart;
    }

    #endregion UserDefinedFunctions
}