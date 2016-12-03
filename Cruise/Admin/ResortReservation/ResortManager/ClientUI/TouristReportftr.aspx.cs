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
using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.MasterServices;
using System.IO;

public partial class ClientUI_TouristReportftr : ClientBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetAccomodationTypeDetails();
            FillAccomodationTypes();
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        RefreshGrid();
        
    }


    private void FillAccomodationTypes()
    {

        ddlAccomName.Items.Clear();
        SortedList slAccomTypes = new SortedList();
        slAccomTypes.Add("0", "Choose");

        AccomTypeDTO[] oAccomTypeData = SessionServices.Booking_AccomodationData;
        if (oAccomTypeData == null)
        {
           // SetAccomodationTypeDetails();
            oAccomTypeData = SessionServices.Booking_AccomodationData;
        }

        if (oAccomTypeData != null)
        {
            for (int i = 0; i < oAccomTypeData.Length; i++)
            {
                slAccomTypes.Add(Convert.ToString(oAccomTypeData[i].AccomodationTypeId), Convert.ToString(oAccomTypeData[i].AccomodationType));
            }
        }
        ddlAccomType.DataSource = slAccomTypes;
        ddlAccomType.DataTextField = "value";
        ddlAccomType.DataValueField = "key";
        ddlAccomType.DataBind();
    }

    private void SetAccomodationTypeDetails()
    {
        //Filling the Object which contains the Accomodation type and also all the respective accomodations
        //This is saved in the session and then the Drop downs are filled.
        AccomTypeDTO[] oAccomTypeData;
        AccomodationTypeMaster objATM;
        objATM = new AccomodationTypeMaster();
        oAccomTypeData = objATM.GetAccomTypeWithAccomDetails(0);
        SessionServices.Booking_AccomodationData = oAccomTypeData;
        objATM = null;
    }

    private void FillAccomodations(int AccomodationTypeId)
    {

        AccomTypeDTO[] oAccomTypeData;
        oAccomTypeData = SessionServices.Booking_AccomodationData;
        ddlAccomName.Items.Clear();
        SortedList slAccomData = new SortedList();
        slAccomData.Add("0", "Choose");
        if (oAccomTypeData != null)
        {
            for (int i = 0; i < oAccomTypeData.Length; i++)
            {
                if (oAccomTypeData[i].AccomodationTypeId.CompareTo(AccomodationTypeId) == 0)
                {
                    if (oAccomTypeData[i].Accomodations != null)
                    {
                        for (int j = 0; j < oAccomTypeData[i].Accomodations.Length; j++)
                        {
                            if (!slAccomData.ContainsKey(Convert.ToString(oAccomTypeData[i].Accomodations[j].AccomodationId)))
                            {
                                slAccomData.Add(Convert.ToString(oAccomTypeData[i].Accomodations[j].AccomodationId), Convert.ToString(oAccomTypeData[i].Accomodations[j].AccomodationName));
                            }
                        }
                    }
                }
            }
            ddlAccomName.DataSource = slAccomData;
            ddlAccomName.DataTextField = "value";
            ddlAccomName.DataValueField = "key";
            ddlAccomName.DataBind();
        }
    }


    private void RefreshGrid()
    {

        DateTime checkInDate, checkOutDate;
        DateTime.TryParse(txtStartDate.Text, out checkInDate);
        DateTime.TryParse(txtEndDate.Text, out checkOutDate);
        TouristServices touristServices = new TouristServices();
        if ((checkInDate != DateTime.MinValue && checkOutDate != DateTime.MinValue))
        {
            BookingTouristDTO[] oBTData = touristServices.GetBookingTouristsftr(txtbkCode.Text.Trim(), txtmailId.Text.Trim(), txtPassportNo.Text.Trim(), txtName.Text.Trim(), checkInDate, checkOutDate,Convert.ToInt32(ddlAccomName.SelectedIndex>0? ddlAccomName.SelectedValue:"0"));
            lblTouristNotFound.Text = String.Empty;

            if (oBTData == null)
            {
                lblTouristNotFound.Text = "No tourist found for this booking.";
                dgTouristDetails.DataSource = null;
                dgTouristDetails.DataBind();
            }
            else
            {


                dgTouristDetails.CurrentPageIndex = 0;
                dgTouristDetails.DataSource = oBTData;

                dgTouristDetails.DataBind();

            }
        }
    }

    private void RefreshGrid1()
    {

        DateTime checkInDate, checkOutDate;
        DateTime.TryParse(txtStartDate.Text, out checkInDate);
        DateTime.TryParse(txtEndDate.Text, out checkOutDate);
        TouristServices touristServices = new TouristServices();
        if ((checkInDate != DateTime.MinValue && checkOutDate != DateTime.MinValue))
        {
            BookingTouristDTO[] oBTData = touristServices.GetBookingTouristsftr(txtbkCode.Text.Trim(), txtmailId.Text.Trim(), txtPassportNo.Text.Trim(), txtName.Text.Trim(), checkInDate, checkOutDate,Convert.ToInt32(ddlAccomName.SelectedValue));
            lblTouristNotFound.Text = String.Empty;

            if (oBTData == null)
            {
                lblTouristNotFound.Text = "No tourist found for this booking.";
                dgTouristDetails.DataSource = null;
                dgTouristDetails.DataBind();
            }
            else
            {


            
                dgTouristDetails.DataSource = oBTData;

                dgTouristDetails.DataBind();

            }
        }
    }


    protected void dgTouristDetails_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
       
        dgTouristDetails.CurrentPageIndex = e.NewPageIndex;
        RefreshGrid1();


    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportToExcel();
    }

    protected void ExportToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=TouristReport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);


            dgTouristDetails.AllowPaging = false;
            RefreshGrid1();




            dgTouristDetails.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode {mso-number-format:General } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void ddlAccomType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillAccomodations(Convert.ToInt32(ddlAccomType.SelectedValue));
    }
}