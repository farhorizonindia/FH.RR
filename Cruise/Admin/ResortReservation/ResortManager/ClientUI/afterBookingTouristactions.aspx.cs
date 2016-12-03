using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;

public partial class ClientUI_afterBookingTouristactions : ClientBasePage
{
    int iBookingId;
    int iTouristNo;
    //string sBookingCode;

    protected void Page_Load(object sender, EventArgs e)
    {        
        StringBuilder strComment = new StringBuilder();

        iBookingId = Convert.ToInt32(Request.QueryString["bid"]);
        iTouristNo = Convert.ToInt32(Request.QueryString["tno"]);
        //sBookingCode = Convert.ToString(Request.QueryString["bcode"]);
        if (iBookingId > 0)
        {
            lblTouristDetails.Text = " The Booking Id is " + iBookingId.ToString();
            lblTouristDetails.Text = lblTouristDetails.Text + Environment.NewLine + " The Tourist No is " + iTouristNo.ToString();
            if (Request.QueryString["tstatus"] == "ins")
            {
                lblTouristStatus.Text = "The tourist has been added successfully";
                lblTouristStatus.ForeColor = System.Drawing.Color.Orange;                
                strComment.Append("You can perform following operations");
                strComment.Append("<br> <br><a href='touristDetails.aspx?bid=" + Convert.ToString(iBookingId) + "&tno=" + Convert.ToString(iTouristNo) + "&op=edit'>View/Edit Tourist</a><br>");                
                //strComment.Append("<br> <a href='touristDetails.aspx?bid=" + Convert.ToString(_iBookingId) + "'>Add Tourists</a><br>");
                //strComment.Append("<br> <a href='ViewTourists.aspx?bid=" + Convert.ToString(_iBookingId) + "'>View Tourists</a><br>");
                strComment.Append("<br> <a href='Booking.aspx'>Add New Booking</a><br>");

            }
            else if (Request.QueryString["tstatus"] == "upd")
            {
                lblTouristStatus.Text = "The tourist has been updated successfully";
                lblTouristStatus.ForeColor = System.Drawing.Color.Green;
                strComment.Append("You can perform following operations");
                strComment.Append("<br> <br><a href='touristDetails.aspx?bid=" + Convert.ToString(iBookingId) + "&tno=" + Convert.ToString(iTouristNo) + "&op=edit'>View/Edit Tourist</a><br>");                
                strComment.Append("<br> <a href='touristDetails.aspx?bid=" + Convert.ToString(iBookingId) + "'>Add Tourists</a><br>");
                strComment.Append("<br> <a href='ViewTourists.aspx?bid=" + Convert.ToString(iBookingId) + "'>View Tourists</a><br>");
                strComment.Append("<br> <a href='Booking.aspx'>Add New Booking</a><br>");
            }
            else if (Request.QueryString["tstatus"] == "del")
            {
                lblTouristStatus.Text = "The tourist has been deleted successfully";
                lblTouristStatus.ForeColor = System.Drawing.Color.Green;
                strComment.Append("You can perform following operations");                
                strComment.Append("<br> <a href='touristDetails.aspx?bid=" + Convert.ToString(iBookingId) + "'>Add Tourists</a><br>");
                strComment.Append("<br> <a href='ViewTourists.aspx?bid=" + Convert.ToString(iBookingId) + "'>View Tourists</a><br>");
                strComment.Append("<br> <a href='Booking.aspx'>Add New Booking</a><br>");
            }

            lblComment.Text = Convert.ToString(strComment);

        }
        else
        {
            lblTouristStatus.Text = "The tourist has not been updated successfully";
            lblTouristStatus.ForeColor = System.Drawing.Color.Green;
        }
    }
    
}
