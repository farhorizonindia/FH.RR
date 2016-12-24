using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ClientUI_BookingReminder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            sendMail();
        }
    }




    public string GetGridviewData(GridView gv)
    {
        StringBuilder strBuilder = new StringBuilder();
        StringWriter strWriter = new StringWriter(strBuilder);
        HtmlTextWriter htw = new HtmlTextWriter(strWriter);
        gv.RenderControl(htw);
        return strBuilder.ToString();
    }


    public void sendMail()
    {
        try
        {

            string strCon = string.Empty;

            strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;


            SqlConnection cn = new SqlConnection(strCon);
            string query1 = "Select Days from [dbo].[tblBookingReminder] where EmailID=@EmailId";
            SqlCommand cmd1 = new SqlCommand(query1, cn);
            cmd1.Parameters.AddWithValue("@EmailId", "rahul.dd21@gmail.com");
            cmd1.CommandType = CommandType.Text;
            cn.Open();
            SqlDataReader dr1 = cmd1.ExecuteReader();

            DataTable dtReturnData1 = new DataTable();
            dtReturnData1.Load(dr1);
            cn.Close();

            string query = "SELECT    B.BookingCode,am.AccomName ,BSM.BookingStatusType as BookingStatus,"
                            + " CONVERT(Varchar(11), B.StartDate, 106) AS StartDate, "
                            + " CONVERT(Varchar(11), B.enddate, 106) AS EndDate"
                            + " FROM         tblBooking AS B LEFT OUTER JOIN "
                            + " tblBookingStatusMaster AS BSM ON "
                            + "   B.BookingStatusId = BSM.BookingStatusId inner join dbo.tblAccomMaster am on am.AccomId=B.AccomId"
                            + "  WHERE CONVERT(Varchar(11), DATEADD(day,@RemDays, B.BookingDate),106)=CONVERT(Varchar(11),getdate(),106) and   B.proposedbooking=1 ";

            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@RemDays", Convert.ToInt32(dtReturnData1.Rows[0][0]));

            cmd.CommandType = CommandType.Text;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            DataTable dtReturnData = new DataTable();
            dtReturnData.Load(dr);
            cn.Close();

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("adventureresortscruises.in");

            mail.From = new MailAddress("reservations@adventureresortscruises.in");

            mail.To.Add("rahul.dd21@gmail.com");


            mail.Subject = "Booking Reminder";


            StringBuilder sb = new StringBuilder();
            sb.Append("<table style='border-collapse: collapse;border: 1px solid black'>");
            sb.Append("<tr style='1px solid black'><td style='1px solid black'>BookingCode</td><td>Accomodation</td><td>BookingStatus</td><td>CheckinDate</td></tr>");
            if (dtReturnData != null)
            {
                for (int i = 0; i < dtReturnData.Rows.Count; i++)
                {
                    sb.Append("<tr style='1px solid black'><td style='1px solid black'>" + dtReturnData.Rows[i]["BookingCode"].ToString() + "</td>" + "<td style='1px solid black'>" + dtReturnData.Rows[i]["AccomName"].ToString() + "</td>" + "<td style='1px solid black'>" + dtReturnData.Rows[i]["BookingStatus"].ToString() + "</td>" + "<td style='1px solid black'>" + dtReturnData.Rows[i]["StartDate"].ToString() + "</td></tr>");
                }
            }
            sb.Append("</table>");

            //mail.Attachments.Add(new Attachment(new MemoryStream(pdfBytes), "invoice.pdf"));

            mail.IsBodyHtml = true;
            mail.Body = sb.ToString();

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("reservations@adventureresortscruises.in", "Augurs@123");
            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);
            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Payment Successfull, Please Print your Invoice, Booking Details have been sent to your e-mail.')", true);
        }
        catch (Exception ex)
        {
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
        }
    }


}