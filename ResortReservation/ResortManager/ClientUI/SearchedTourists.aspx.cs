using System;
using System.Text;
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

public partial class ClientUI_SearchedTourists : ClientBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RefreshGrid();
        }
    }

    private void RefreshGrid()
    {
        string sFirstName = Request.QueryString["FN"];
        string sLastName = Request.QueryString["LN"];
        string sPassportno = Request.QueryString["PN"];
        int iNationalityid = Convert.ToInt32(Request.QueryString["NID"]);
        BookingTouristDTO[] oBookingTouristDTO = null;
        oBookingTouristDTO = GetTouristData(sFirstName, sLastName, sPassportno, iNationalityid);
        if (oBookingTouristDTO != null)
        {
            dgTouristDetails.DataSource = oBookingTouristDTO;
            dgTouristDetails.DataBind();
        }
    }
    private BookingTouristDTO[] GetTouristData(string FirstName, string LastName, string PassportNo, int NationalityID)
    {
        //BookingTouristHandler oBookingTouristHandler = new BookingTouristHandler();
        TouristServices touristServices = new TouristServices();
        BookingTouristDTO[] oBookingTouristDTO = null;

        Cache.Remove("TouristData");

        oBookingTouristDTO = touristServices.GetTourists(FirstName, LastName, PassportNo, NationalityID);
        if (oBookingTouristDTO != null)
        {
            Cache.Insert("TouristData", oBookingTouristDTO, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
        }
        return oBookingTouristDTO;
    }
    protected void dgTouristDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iTouristNo = Convert.ToInt32(dgTouristDetails.DataKeys[dgTouristDetails.SelectedIndex]);
        SessionServices.TouristDetails_TouristNo = iTouristNo;
        StringBuilder sb = new StringBuilder();
        sb.Append("window.opener.document.forms[0].submit();");
        sb.Append("window.close();");
        ClientScript.RegisterClientScriptBlock(this.GetType(), "CloseWindowScript", sb.ToString(), true);
    }
}
