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
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.BusinessServices;

using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;

public partial class ClientUI_ViewTourists : ClientBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int iBookingId;
            iBookingId = Convert.ToInt32(Request.QueryString["bid"]);
            RefreshGrid(iBookingId);
        }
    }

    private void RefreshGrid(int iBookingId)
    {
        TouristServices touristServices = new TouristServices();
        BookingTouristDTO[] oBTData = touristServices.GetBookingTourists(iBookingId);
        lblTouristNotFound.Text = String.Empty;

        if (oBTData == null)
        {
            lblTouristNotFound.Text = "No tourist found for this booking.";
        }
        else
        {
            dgTouristDetails.DataSource = oBTData;
            dgTouristDetails.DataBind();
        }
    }
    //private void ShowTourists(clsBookingTouristData[] oBTData)
    //{
    //    if (oBTData == null)
    //        return;
    //    Table tblMain = new Table();
    //    for (int iCols = 0; iCols < oBTData.Length; iCols++)
    //    {
    //        TableRow trMain = new TableRow();
    //        TableCell tcBC = new TableCell();
    //        tcBC.Text = Convert.ToString(oBTData[iCols].BookingCode);
    //        trBC.Cells.Add(tcBC);

    //        TableRow trFN = new TableRow();
    //        TableCell tcFN = new TableCell();
    //        tcFN.Text = oBTData[iCols].FirstName == "" ? "NA" : oBTData[iCols].FirstName;
    //        trFN.Cells.Add(tcFN);

    //        TableRow trMN = new TableRow();
    //        TableCell tcMN = new TableCell();
    //        tcMN.Text = oBTData[iCols].MiddleName;
    //        trMN.Cells.Add(tcMN);

    //        TableRow trLN = new TableRow();
    //        TableCell tcLN = new TableCell();
    //        tcLN.Text = oBTData[iCols].LastName;
    //        trLN.Cells.Add(tcLN);

    //        TableRow trPPN = new TableRow();
    //        TableCell tcPPN = new TableCell();
    //        tcPPN.Text = Convert.ToString(oBTData[iCols].PassportNo);
    //        trPPN.Cells.Add(tcPPN);

    //        TableRow trVN = new TableRow();
    //        TableCell tcVN = new TableCell();
    //        tcVN.Text = Convert.ToString(oBTData[iCols].VisaNo);
    //        trVN.Cells.Add(tcVN);

    //        tblMain.Rows.Add(trBC);
    //        tblMain.Rows.Add(trFN);
    //        tblMain.Rows.Add(trMN);
    //        tblMain.Rows.Add(trLN);
    //        tblMain.Rows.Add(trPPN);
    //        tblMain.Rows.Add(trVN);
    //    }
    //    this.Controls.Add(tblMain);
    //}

}
