using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.Common.DataEntities.InputOutput;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.MasterServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using FarHorizon.DataSecurity;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;
using System.Activities.Statements;

public partial class ViewBookings : ClientBasePage
{
    private bool _cForm = false;
    DatabaseManager oDB;
    DALBooking dlbooking = new DALBooking();
    BALBooking blbooking = new BALBooking();
    public bool CForm
    {
        get { return _cForm; }
        set { _cForm = value; }
    }

    #region Event Handler
    string CompanyName = ConfigurationManager.AppSettings["cName"];
    string CompanyEmail = ConfigurationManager.AppSettings["cEmail"];
    string CompanyAddress = ConfigurationManager.AppSettings["cAddress"];
    string CompanyPhoneNo = ConfigurationManager.AppSettings["cPhoneNo"];
    string CompanyMobile = ConfigurationManager.AppSettings["cMobile"];
    string CompanyLogo = ConfigurationManager.AppSettings["cLogo"];
    string ccEmail = ConfigurationManager.AppSettings["ccEmail"];
    string SmtpUserId = ConfigurationManager.AppSettings["SMTPUserId"];
    string SmtpPassword = ConfigurationManager.AppSettings["SMTPPwd"];
    string SmtpHost = ConfigurationManager.AppSettings["SMTPServer"];
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection(GetConnectionString());
        if (Request.QueryString["cf"] != null)
        {
            CForm = Boolean.Parse(Request.QueryString["cf"]);
        }
        AddAttributes();

        if (!IsPostBack)
        {
            FillBookingStatusTypes();
            FillAccomodationTypes();
            FillAgents();
            FillRefAgents();
            if (SessionServices.ViewBooking_SelectedCheckInDate != null)
                txtStartDate.Text = SessionServices.ViewBooking_SelectedCheckInDate;
            if (SessionServices.ViewBooking_SelectedCheckOutDate != null)
                txtEndDate.Text = SessionServices.ViewBooking_SelectedCheckOutDate;
            if (SessionServices.ViewBooking_SelectedBookingStatus != null)
                ddlBookingStatusTypes.SelectedValue = SessionServices.ViewBooking_SelectedBookingStatus;
            if (SessionServices.ViewBooking_SelectedAccomodationType != null)
                ddlAccomType.SelectedValue = SessionServices.ViewBooking_SelectedAccomodationType;

            btnShow_Click(sender, e);

        }
    }

    protected void dgBookings_EditCommand(object source, DataGridCommandEventArgs e)
    {
        int iBookingID = Convert.ToInt32(dgBookings.DataKeys[e.Item.ItemIndex].ToString());
        Response.Redirect("Booking.aspx?bid=" + iBookingID);
    }

    protected void dgBookings_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        int iBookingID = Convert.ToInt32(dgBookings.DataKeys[e.Item.ItemIndex].ToString());
        BookingServices oBookingManager;
        //clsBookingHandler oBookingHandler;
        oBookingManager = new BookingServices();
        oBookingManager.DeleteBooking(iBookingID);
        RefreshGrid();
    }
    private string GetConnectionString()
    {

        return Convert.ToString(System.Configuration.ConfigurationManager.ConnectionStrings["ReservationConnectionString"]);


    }
    private void PopulateModule(string AccomName)
    {

        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("select AccomPolicyUrl from tblAccomMaster where  AccomName='" + AccomName + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);


        ViewState["ModuleDt"] = dt.Rows[0]["AccomPolicyUrl"].ToString();
        con.Close();
        // chkModule.Items.Insert(0, new ListItem("All", "0"));


    }
    public void sendMail(string email, string name, string lastname, int bookingid, double amount, double paidamount, string stratdate, string enddate, string bookingcode, DateTime BookingDate, string packagename, string accomname, string regionname, string title,string password)
    {

        try
        {
           // email = "randeep@xportsoft.com";

            //if (Session["AccomId"] != "" && Session["AccomId"] != null)
            //{
            //    PopulateModule(Convert.ToInt32(Session["AccomId"]));
            //    string AccomPolicyUrl = ViewState["ModuleDt"].ToString();
            //    //    //linkTandCSign.Attributes["href"] = AccomPolicyUrl;
            //    //    //linkTandCReg.Attributes["href"] = AccomPolicyUrl;
            //    //    //linkTandCGuest.Attributes["href"] = AccomPolicyUrl;
            //    //}
            //}
            Double dueamount = amount - paidamount;

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(SmtpHost);
            //  SmtpClient SmtpServer = new SmtpClient("adventureresortscruises.in");
            // mail.From = new MailAddress("reservations@adventureresortscruises.in", "ARC Reservations");
            mail.From = new MailAddress(SmtpUserId, "Reservations");
            mail.To.Add(email);
           // mail.CC.Add(ccEmail);

            mail.Subject = "Balance Due Payment for Reservation -" + title + " " + name + " " + lastname + " – " + bookingcode + "";

            //  Random rnd = new Random();
            string Code = password;

            string dateInString = "01.10.2009";

            DateTime startDate = Convert.ToDateTime(stratdate);
            DateTime expiryDate = startDate.AddDays(-30);
            //due date
            DateTime dueDate = startDate.AddDays(-90);
            if (DateTime.Now > expiryDate)
            {
                //... trial expired
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<div>");
            sb.Append("<div> Booking No: " + bookingcode + "</div><div><br/></div><div>Date of Booking: " + Convert.ToDateTime(BookingDate).ToString("d MMMM, yyyy") + "</div>");
            sb.Append("<div><br/></div>");

            if (!string.IsNullOrEmpty(password))
            {
                sb.Append("<div> You Password is:" + password + " </div>");
                sb.Append("<div><br/></div>");
            }
            if (lastname == "XYZ")
            {
                sb.Append("<div> Dear MR. " + name + ",</div><div><br/></div><div>Namaskar! Greetings from " + CompanyName + "</ div><div><br/><div><div>Thank you for booking " + accomname + ", " + regionname + ". </div><div><br/></div><div> Your balance due is Rs. " + (amount - paidamount) + ". </div><div><br/></div><div><a href=http://test1.adventureresortscruises.in/Cruise/Booking/FinalpaymentLinkPage.aspx?bid=" + bookingid + ">Click To Pay</a></div><div><br/></div><div>Please pay by " + dueDate.ToString("d MMMM, yyyy") + ". Please ignore if paid </ div><div><br/><div><div><br/></div><div><br/></div><div><br/></div><div>Best wishes, </div><div><br/></div><div>The " + accomname + " Team!</ div>");
            }
            else
            {                
                sb.Append("<div> Dear " + title + ". " + lastname + ",</div><div><br/></div><div>Namaskar! Greetings from " + CompanyName + "</ div><div><br/><div><div>Thank you for booking " + accomname + ", " + regionname + ". </div><div><br/></div><div> Your balance due is Rs. " + (amount - paidamount) + ". </div><div><br/></div><div><a href=http://test1.adventureresortscruises.in/Cruise/Booking/FinalpaymentLinkPage.aspx?bid=" + bookingid + ">Click To Pay</a></div><div><br/></div><div>Please pay by " + dueDate.ToString("d MMMM, yyyy") + ". Please ignore if paid </ div><div><br/><div><div><br/></div><div><br/></div><div><br/></div><div>Best wishes, </div><div><br/></div><div>The " + accomname + " Team!</ div>");
            }
            sb.Append("</div>");
            sb.Append("<img src='" + CompanyLogo + "' alt='Image'/><br /><div> " + CompanyName + "</div><div> " + CompanyAddress + " </div> <div> Phone: " + CompanyPhoneNo + " </div><div> Mobile: " + CompanyMobile + " </div><div><br/> </div> ");
            //sb.Append("<div>The booking policy of the cruise can be referred to at <a href='http://www.mahabaahucruiseindia.com/cruise-policy' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.mahabaahucruiseindia.com/cruise-policy&amp;source=gmail&amp;ust=1470139247045000&amp;usg=AFQjCNH3vyzjL507K4FspRY6TihAfogUug'>http://www.mahabaahucruiseindia.com/cruise-policy </a>.</div><div><br/></div>");
            if (Session["AccomName"] != "" && Session["AccomName"] != null)
            {
                //PopulateModule(Convert.ToInt32(Session["AccomId"]));
                PopulateModule(Session["AccomName"].ToString());
                string AccomPolicyUrl = ViewState["ModuleDt"].ToString();
                sb.Append("<div>The booking policy of the cruise can be referred to at <a href='" + AccomPolicyUrl + "' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=" + AccomPolicyUrl + "&amp;source=gmail&amp;ust=1470139247045000&amp;usg=AFQjCNH3vyzjL507K4FspRY6TihAfogUug'>" + AccomPolicyUrl + " </a>.</div><div><br/></div>");
            }
            else
            {
                sb.Append("<div>The booking policy of the cruise can be referred to at <a href='http://www.mahabaahucruiseindia.com/cruise-policy' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.mahabaahucruiseindia.com/cruise-policy&amp;source=gmail&amp;ust=1470139247045000&amp;usg=AFQjCNH3vyzjL507K4FspRY6TihAfogUug'>http://www.mahabaahucruiseindia.com/cruise-policy </a>.</div><div><br/></div>");
            }
            mail.IsBodyHtml = true;
            mail.Body = sb.ToString();

            SmtpServer.Port = 587;
            // SmtpServer.Credentials = new System.Net.NetworkCredential("reservations@adventureresortscruises.in", "Augurs@123");
            SmtpServer.Credentials = new System.Net.NetworkCredential(SmtpUserId, SmtpPassword);
            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);
            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Reminder mail has been sent')", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:Sent()", true);

            // ScriptManager.RegisterClientScriptBlock(this, GetType(), "Sc", "alert('Reminder mail has been sent');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
            return;
        }

    }
    protected void dgBookings_ItemCommand(object source, DataGridCommandEventArgs e)
    {

        string cFormUrl = string.Empty;
        try
        {
            if (e.Item.ItemIndex >= 0)
            {
                int iBookingID = Convert.ToInt32(dgBookings.DataKeys[e.Item.ItemIndex].ToString());
                switch (e.CommandName.ToString().ToUpper())
                {
                    case "EDIT":
                        Response.Redirect("Booking.aspx?bid=" + iBookingID);
                        break;
                    case "VIEW":
                        if (string.Compare(e.Item.Cells[5].Text, "CONFIRMED", true) == 0)
                            Response.Redirect("Bookingconfirmation.aspx?bid=" + iBookingID.ToString());
                        else
                            Response.Redirect("Booking.aspx?bid=" + iBookingID.ToString() + "&mode=view");
                        break;
                    case "CONFIRMATION":
                        Response.Redirect("Bookingconfirmation.aspx?bid=" + iBookingID.ToString());
                        break;
                    case "VIEWTOURIST":
                        Response.Redirect("ViewTourists.aspx?bid=" + iBookingID.ToString());
                        break;
                    case "ADDTOURIST":
                        Response.Redirect("touristdetails.aspx?bid=" + iBookingID.ToString());
                        break;
                    case "CFORMFOREIGNNATIONAL":
                        Response.Redirect("CFormReport.aspx?bid=" + iBookingID.ToString() + "&cftype=fn");
                        break;
                    case "CFORMINDIANNATIONAL":
                        Response.Redirect("CFormReport.aspx?bid=" + iBookingID.ToString() + "&cftype=in");
                        break;
                    case "UPLOADTOURIST":
                        Response.Redirect("~\\uploader.aspx?bid=" + iBookingID.ToString() + "&upload=" + ENums.UploadXMLType.Tourist.ToString());
                        break;
                    case "REMINDER":
                        //Reminder Email
                        //DialogResult result = MessageBox.Show("Are you sure you want to send reminder to the guest for the payment at" + DataSecurityManager.Decrypt(Session["Email"].ToString())+ "", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                        //if (result == DialogResult.Yes)
                        //{

                        blbooking._iBookingId = iBookingID;
                        DataTable dt = dlbooking.paymentreminder(blbooking);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Session["Email"] = dt.Rows[0]["Email"].ToString();
                            Session["AccomName"] = dt.Rows[0]["AccomName"].ToString();
                            double amt = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                amt = amt + Convert.ToDouble(dt.Rows[i]["Amount"].ToString());
                            }
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:if(confirm('Are you sure you want to send reminder to the guest for the payment at " + DataSecurityManager.Decrypt(Session["Email"].ToString()) + "?');", true);
                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:if(confirm('Are you sure you want to send reminder to the guest for the payment at " + DataSecurityManager.Decrypt(Session["Email"].ToString()) + "?')==false)return false;", true);

                            //RegisterClientScriptBlock
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:confirm('Are you sure you want to send reminder to the guest for the payment at" + DataSecurityManager.Decrypt(Session["Email"].ToString()) + "?')", true);

                            ScriptManager.RegisterStartupScript(this, GetType(), "Sc", "confirmation('" + DataSecurityManager.Decrypt(Session["Email"].ToString()) + "');", true);
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:confirmation('" + DataSecurityManager.Decrypt(Session["Email"].ToString()) + "')", true);

                            //DialogResult result = MessageBox.Show("Are you sure you want to send reminder to the guest for the payment at  " + DataSecurityManager.Decrypt(Session["Email"].ToString()) + "", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                            //if (result == DialogResult.Yes)
                            //{
                            string L = dt.Rows[0]["LastName"].ToString();
                            if (L == " ")
                            {
                                dt.Rows[0]["LastName"] = "XYZ";
                            }

                            sendMail(DataSecurityManager.Decrypt(dt.Rows[0]["Email"].ToString()), DataSecurityManager.Decrypt(dt.Rows[0]["Name"].ToString()), DataSecurityManager.Decrypt(dt.Rows[0]["LastName"].ToString()), iBookingID, amt, Convert.ToDouble(dt.Rows[0]["PaidAmt"].ToString()), dt.Rows[0]["StartDate"].ToString(), dt.Rows[0]["enddate"].ToString(), dt.Rows[0]["BookingCode"].ToString(), Convert.ToDateTime(dt.Rows[0]["BookingDate"].ToString()), dt.Rows[0]["Packagename"].ToString(), dt.Rows[0]["AccomName"].ToString(), dt.Rows[0]["RegionName"].ToString(), DataSecurityManager.Decrypt(dt.Rows[0]["Title"].ToString()), DataSecurityManager.Decrypt(dt.Rows[0]["Password"].ToString()));

                            // }

                            RefreshGrid();

                        }
                        else
                        {
                            RefreshGrid();
                        }
                        //}
                        //else if (result == DialogResult.No)
                        //{
                        //    RefreshGrid();
                        //}


                        break;

                    default:
                        break;
                }




            }
        }
        catch (Exception exp)
        {
            GF.LogError("ViewBookings.dgBookings_ItemCommand", exp.Message);
            return;
        }

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        GridRefresh();

    }
    protected void ReminderButton_Click(object sender, EventArgs e)
    {
        //DataTable dt = dlbooking.paymentreminder(blbooking);
        //if (dt != null && dt.Rows.Count > 0)
        //{
        //    Session["Email"] = dt.Rows[0]["Email"].ToString();
        //}
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Are you sure you want to send reminder to the guest for the payment at" + Session["Email"].ToString() + "')", true);
    }
    private void GridRefresh()
    {
        SessionServices.DeleteSession(Constants._ViewBooking_BookingData);
        if (txtStartDate.Text != "")
            SessionServices.ViewBooking_SelectedCheckInDate = txtStartDate.Text.ToString();
        if (txtEndDate.Text != "")
            SessionServices.ViewBooking_SelectedCheckOutDate = txtEndDate.Text.ToString();
        if (ddlAccomType.SelectedIndex != 0)
            SessionServices.ViewBooking_SelectedAccomodationType = ddlAccomType.SelectedValue;
        if (ddlBookingStatusTypes.SelectedIndex != 0)
            SessionServices.ViewBooking_SelectedBookingStatus = ddlBookingStatusTypes.SelectedValue;
        RefreshGrid();
    }
    protected void dgBookings_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgBookings.CurrentPageIndex = e.NewPageIndex;
        RefreshGrid();
    }
    protected void dgBookings_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            #region
            if (Boolean.Parse(e.Item.Cells[18].Text) == true)
            {
                e.Item.Cells[4].Text = e.Item.Cells[4].Text + " " + "*";

            }

            #endregion

            #region Booking & Proposed
            if (string.Compare(e.Item.Cells[5].Text, "BOOKED", true) == 0)
            {
                e.Item.Cells[0].BackColor = System.Drawing.Color.Aqua;
                e.Item.Cells[10].Text = "";
                e.Item.Cells[11].Text = "";
                e.Item.Cells[14].Text = "";
                //  e.Item.Cells[15].Text = "";
                //e.Item.Cells[10].Visible = true;
                //e.Item.Cells[11].Text = "";
                //e.Item.Cells[12].Text = "";
                if (e.Item.Cells[22].Text == e.Item.Cells[21].Text)
                {
                    e.Item.Cells[23].Text = "";
                }
                if (string.Compare(e.Item.Cells[15].Text.ToUpper(), "TRUE", true) == 0)
                {
                    e.Item.Cells[0].BackColor = System.Drawing.Color.Blue;
                    e.Item.Cells[0].ForeColor = System.Drawing.Color.White;
                    e.Item.Cells[8].Text = "";
                    e.Item.Cells[9].Text = "";
                    e.Item.Cells[10].Text = "";
                    e.Item.Cells[11].Text = "";
                    //e.Item.Cells[12].Text = "";
                    //e.Item.Cells[13].Text = "";
                    e.Item.Cells[14].Text = "";
                    e.Item.Cells[15].Text = "";
                    e.Item.Cells[23].Text = "";
                }
            }
            #endregion

            #region Confirmed


            else if (string.Compare(e.Item.Cells[5].Text, "CONFIRMED", true) == 0)
            {
                e.Item.Cells[0].BackColor = System.Drawing.Color.Lime;
                e.Item.Cells[6].Text = "";

                LinkButton bc;
                bc = (LinkButton)(e.Item.Cells[9].Controls[0]);
                if (bc != null)
                    bc.Text = "Edit Confirmation";

                // e.Item.Cells[10].Text = "";
                //e.Item.Cells[11].Visible = true;
                //e.Item.Cells[12].Visible = true;
                //e.Item.Cells[13].Visible = true;
                //e.Item.Cells[14].Visible = true;

                e.Item.Cells[10].Visible = true;
                e.Item.Cells[11].Visible = true;
                e.Item.Cells[14].Visible = true;
                e.Item.Cells[23].Text = "";
            }
            #endregion
            #region Cancelled
            else if (string.Compare(e.Item.Cells[5].Text, "CANCELLED", true) == 0)
            {
                e.Item.Cells[0].BackColor = System.Drawing.Color.Red;
                e.Item.Cells[6].Text = "";
                e.Item.Cells[7].Text = "";
                e.Item.Cells[9].Text = "";
                e.Item.Cells[10].Text = "";
                e.Item.Cells[11].Text = "";
                //e.Item.Cells[12].Text = "";
               // e.Item.Cells[13].Text = "";
                e.Item.Cells[14].Text = "";
                e.Item.Cells[15].Text = "";
                e.Item.Cells[23].Text = "";


            }
            #endregion
            #region Waitlisted
            else if (string.Compare(e.Item.Cells[5].Text, "WAITLISTED", true) == 0)
            {
                e.Item.Cells[0].BackColor = System.Drawing.Color.Orange;
                e.Item.Cells[8].Text = "";
                e.Item.Cells[9].Text = "";
                e.Item.Cells[10].Text = "";
                e.Item.Cells[11].Text = "";
                //e.Item.Cells[12].Text = "";
                //e.Item.Cells[13].Text = "";
                e.Item.Cells[14].Text = "";
                e.Item.Cells[15].Text = "";
                e.Item.Cells[23].Text = "";


            }
            #endregion

            //#region Proposed            
            //else if (Boolean.Parse(e.Item.Cells[15].Text) == true || string.Compare(e.Item.Cells[5].Text, "PROPOSED", true) == 0)
            //{
            //    e.Item.Cells[0].BackColor = System.Drawing.Color.Blue;
            //    e.Item.Cells[8].Text = "";
            //    e.Item.Cells[9].Text = "";
            //    e.Item.Cells[10].Text = "";
            //    e.Item.Cells[11].Text = "";
            //    //e.Item.Cells[12].Text = "";
            //    //e.Item.Cells[13].Text = "";
            //    e.Item.Cells[14].Text = "";
            //}
            //#endregion

            #region CForm
            if (CForm)
            {
                e.Item.Cells[6].Text = "";
                e.Item.Cells[7].Text = "";
                e.Item.Cells[8].Text = "";
                e.Item.Cells[9].Text = "";
                e.Item.Cells[10].Text = "";
                e.Item.Cells[11].Text = "";
                e.Item.Cells[14].Text = "";

                //e.Item.Cells[20].Text = "";
                //e.Item.Cells[21].Text = "";
                //e.Item.Cells[22].Text = "";

                dgBookings.Columns[6].Visible = false;
                dgBookings.Columns[7].Visible = false;
                dgBookings.Columns[8].Visible = false;
                dgBookings.Columns[9].Visible = false;
                dgBookings.Columns[10].Visible = false;
                dgBookings.Columns[11].Visible = false;
                dgBookings.Columns[14].Visible = false;

                dgBookings.Columns[20].Visible = false;
                dgBookings.Columns[21].Visible = false;
                dgBookings.Columns[22].Visible = false;

                //If Booking has tourists then show the links for C-Forms
                e.Item.Cells[12].Visible = false;
                e.Item.Cells[13].Visible = false;

                if ((string.Compare(e.Item.Cells[16].Text.ToUpper(), "FALSE", true) == 0) &&
                    (string.Compare(e.Item.Cells[17].Text.ToUpper(), "FALSE", true) == 0))
                {
                    e.Item.Cells[12].Visible = true;
                    e.Item.Cells[12].Text = "No Tourists";
                    e.Item.Cells[13].Visible = false;
                }

                e.Item.Cells[12].Visible = e.Item.Cells[16].Text.ToUpper() == Boolean.TrueString.ToUpper() ? true : false;
                e.Item.Cells[13].Visible = e.Item.Cells[17].Text.ToUpper() == Boolean.TrueString.ToUpper() ? true : false;
               // e.Item.Cells[13].Visible = true;
            }
            else
            {
                e.Item.Cells[12].Text = String.Empty;
                e.Item.Cells[13].Text = String.Empty;
                dgBookings.Columns[12].Visible = false;
                dgBookings.Columns[13].Visible = false;
            }

            System.Web.UI.WebControls.Label lblpst = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblpStatus");
            System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)e.Item.FindControl("chkPayStatus");
            chk.Checked = Convert.ToBoolean(lblpst.Text.Trim() == "" ? "false" : lblpst.Text.ToString().ToLower().Trim());
            System.Web.UI.WebControls.LinkButton reminderButton = (System.Web.UI.WebControls.LinkButton)e.Item.FindControl("ReminderButton");
            if (chk.Checked == true)
            {
                // reminderButton.Visible = false;
                e.Item.Cells[23].Text = "";
            }
            
            #endregion
        }
    }
    protected void ddlAccomType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillAccomodations(Convert.ToInt32(ddlAccomType.SelectedValue));
    }
    #endregion


    #region Helper Method(s)
    private void AddAttributes()
    {
        //txtStartDate.Attributes.Add("onchange", "fillEndDate()");
        txtStartDate.Attributes.Add("onchange", "return fillEndDate('" + txtStartDate.ClientID + "','" + txtEndDate.ClientID + "');");
        btnShow.Attributes.Add("onclick", "return validateBeforeGettingBookings()");
    }

    private void FillAccomodationTypes()
    {
        AccomTypeDTO[] oAccomTypeData = GetAccomodationTypeDetails();
        ddlAccomType.Items.Clear();
        //ddlAccomSubtype.Items.Clear();
        SortedList slAccomTypes = new SortedList();
        slAccomTypes.Add(-1, "Choose Accom Type");
        slAccomTypes.Add(0, "All");
        if (oAccomTypeData != null)
        {
            for (int i = 0; i < oAccomTypeData.Length; i++)
            {
                slAccomTypes.Add(Convert.ToInt32(oAccomTypeData[i].AccomodationTypeId), Convert.ToString(oAccomTypeData[i].AccomodationType));
            }
        }
        ddlAccomType.DataSource = slAccomTypes;
        ddlAccomType.DataTextField = "value";
        ddlAccomType.DataValueField = "key";
        ddlAccomType.DataBind();
    }
    private void FillRefAgents()
    {
        try
        {
            AgentMaster oAgentMaster = new AgentMaster();
            AgentDTO[] oAgentData = oAgentMaster.GetRefAgentData();

            ListItemCollection li = new ListItemCollection();
            ListItem l = new ListItem("Choose Ref Agent", "0");
            ddlRefAgent.Items.Insert(0, l);
            if (oAgentData != null)
            {
                for (int i = 0; i < oAgentData.Length; i++)
                {
                    l = new ListItem(oAgentData[i].AgentName.ToString(), oAgentData[i].AgentId.ToString());
                    ddlRefAgent.Items.Insert(i + 1, l);
                    string Email = oAgentData[i].EmailId.ToString();
                    //Session["Email"] = Email;
                }
            }
        }
        catch { }
    }

    private void FillAgents()
    {
        try
        {
            AgentMaster oAgentMaster = new AgentMaster();
            AgentDTO[] oAgentData = oAgentMaster.GetData();
            
            ListItemCollection li = new ListItemCollection();
            ListItem l = new ListItem("Choose Agent", "0");
            ddlAgent.Items.Insert(0, l);
            if (oAgentData != null)
            {
                for (int i = 0; i < oAgentData.Length; i++)
                {
                    l = new ListItem(oAgentData[i].AgentName.ToString(), oAgentData[i].AgentId.ToString());
                    ddlAgent.Items.Insert(i + 1, l);
                    string Email = oAgentData[i].EmailId.ToString();
                    //Session["Email"] = Email;
                }
            }
        }
        catch { }
    }

    private AccomTypeDTO[] GetAccomodationTypeDetails()
    {
        //Filling the Object which contains the Accomodation type and also all the respective accomodations
        //This is saved in the session and then the Drop downs are filled.
        AccomodationTypeMaster accomodationTypeMaster;
        accomodationTypeMaster = new AccomodationTypeMaster();
        AccomTypeDTO[] oAccomTypeData = null;
        //following line has been commented by vijay to get the acomodations user wise
        //oAccomTypeData = objATM.GetAccomTypeWithAccomDetails(0); 

        oAccomTypeData = accomodationTypeMaster.GetAccomTypeWithAccomDetails();
        return oAccomTypeData;
    }

    private void FillBookingStatusTypes()
    {
        BookingStatusMaster oBookingStatusMaster;
        BookingStatusDTO[] oBookingStatusData = null;
        ListItem l = null;
        oBookingStatusMaster = new BookingStatusMaster();
        oBookingStatusData = oBookingStatusMaster.GetData();

        ddlBookingStatusTypes.Items.Clear();

        l = new ListItem();
        l.Text = "Choose";
        l.Value = "-1";
        ddlBookingStatusTypes.Items.Insert(0, l);

        l = new ListItem();
        l.Text = "All";
        l.Value = "0";
        ddlBookingStatusTypes.Items.Insert(1, l);

        if (oBookingStatusData != null)
        {
            for (int i = 0; i < oBookingStatusData.Length; i++)
            {
                l = new ListItem();
                l.Text = oBookingStatusData[i].BookingStatusType;
                l.Value = Convert.ToString(oBookingStatusData[i].BookingStatusId);
                ddlBookingStatusTypes.Items.Insert(i + 2, l);
            }
        }
    }
    public List<ViewBookingDTO> GetBookings(cdtGetBookingsInput getBookingsInput)
    {
        List<ViewBookingDTO> bookingList;
        ViewBookingDTO booking;
        DataRow dr;
        DataSet dsBookingData;
        string sProcName;
        dsBookingData = null;
        bookingList = null;

        if (getBookingsInput.FromDate == DateTime.MinValue || getBookingsInput.FromDate == DateTime.MaxValue)
            getBookingsInput.FromDate = GF.GetDate().AddYears(-10);
        if (getBookingsInput.ToDate == DateTime.MinValue || getBookingsInput.ToDate == DateTime.MaxValue)
            getBookingsInput.ToDate = GF.GetDate().AddYears(20);
        try
        {
            oDB = new DatabaseManager();
            sProcName = "up_Get_Bookings";
            oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@FromDate", DbType.Date, getBookingsInput.FromDate);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ToDate", DbType.Date, getBookingsInput.ToDate);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingStatusTypeId", DbType.Int32, getBookingsInput.BookingStatusType);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomTypeId", DbType.Int32, getBookingsInput.AccomTypeId);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, getBookingsInput.AccomodationId);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AgentId", DbType.Int32, getBookingsInput.AgentId);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingCode", DbType.String, getBookingsInput.BookingCode);

            dsBookingData = oDB.ExecuteDataSet(oDB.DbCmd);
        }
        catch (Exception exp)
        {
            oDB = null;
            dsBookingData = null;
            GF.LogError("clsBookingHandler.GetBookings", exp.Message);
        }

        if (dsBookingData != null)
        {
            //oBookingData[dsBookingData.Tables[0].Rows.Count] = new clsBookingData();
            bookingList = new List<ViewBookingDTO>();
            for (int i = 0; i < dsBookingData.Tables[0].Rows.Count; i++)
            {
                booking = new ViewBookingDTO();
                dr = dsBookingData.Tables[0].Rows[i];
                booking.BookingId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                booking.BookingCode = Convert.ToString(dr.ItemArray.GetValue(1));
                booking.BookingReference = Convert.ToString(dr.ItemArray.GetValue(2));
                booking.SDate = Convert.ToString(dr.ItemArray.GetValue(3));
                booking.EDate = Convert.ToString(dr.ItemArray.GetValue(4));
                booking.StartDate = Convert.ToDateTime(dr.ItemArray.GetValue(3).ToString());
                booking.EndDate = Convert.ToDateTime(dr.ItemArray.GetValue(4).ToString());
                booking.BookingStatus = Convert.ToString(dr.ItemArray.GetValue(5));
                booking.AccomodationType = Convert.ToString(dr.ItemArray.GetValue(6));
                if (dr.ItemArray.GetValue(7) != DBNull.Value)
                    booking.ProposedBooking = Convert.ToBoolean(dr.ItemArray.GetValue(7));
                if (dr.ItemArray.GetValue(8) != DBNull.Value)
                {
                    if (Convert.ToInt32(dr.ItemArray.GetValue(8)) > 0)
                    {
                        booking.HasForeignTourists = true;
                    }
                }
                if (dr.ItemArray.GetValue(9) != DBNull.Value)
                {
                    if (Convert.ToInt32(dr.ItemArray.GetValue(9)) > 0)
                    {
                        booking.HasIndianTourists = true;
                    }
                }

                if (dr.ItemArray.GetValue(10) != DBNull.Value)
                    booking.CharteredBooking = Convert.ToBoolean(dr.ItemArray.GetValue(10));
                if (dr.ItemArray.GetValue(11) != DBNull.Value)
                    booking.PaymentStatus = Convert.ToBoolean(dr.ItemArray.GetValue(11));

                if (dr.ItemArray.GetValue(12) != DBNull.Value)
                    booking.PaidAmt = Convert.ToDouble(dr.ItemArray.GetValue(12));
                if (dr.ItemArray.GetValue(14) != DBNull.Value)
                    booking.InvoiceAmount = Convert.ToDouble(dr.ItemArray.GetValue(14));

                bookingList.Add(booking);
            }
        }
        return bookingList;
    }
    private void RefreshGrid()
    {
        ENums.BookingStatusTypes bookingStatusType = ENums.BookingStatusTypes.NONE;
        ENums.BookingStatusTypes newBookingStatusType = ENums.BookingStatusTypes.NONE;
        BookingServices oBookingManager = new BookingServices();
        List<ViewBookingDTO> oBookingData = null;
        DateTime checkInDate, checkOutDate;
        DateTime.TryParse(txtStartDate.Text, out checkInDate);
        DateTime.TryParse(txtEndDate.Text, out checkOutDate);

        bookingStatusType = GetBookingStatusType(ddlBookingStatusTypes.SelectedItem.Text);
        int AccomTypeId = 0;

        Int32.TryParse(ddlAccomType.SelectedValue.ToString(), out AccomTypeId);

        if (AccomTypeId <= 0) AccomTypeId = 0; //To handle the -1 value of Choose option.

        if (SessionServices.ViewBooking_BookingData == null)
        {
            if (!String.IsNullOrEmpty(txtBookingCode.Text.Trim()) || (checkInDate != DateTime.MinValue && checkOutDate != DateTime.MinValue))
            {
                newBookingStatusType = bookingStatusType;
                if (bookingStatusType == ENums.BookingStatusTypes.PROPOSED)
                {
                    newBookingStatusType = ENums.BookingStatusTypes.BOOKED;
                }

                cdtGetBookingsInput getBookingsInput = new cdtGetBookingsInput();
                getBookingsInput.AccomTypeId = AccomTypeId;
                getBookingsInput.FromDate = checkInDate;
                getBookingsInput.ToDate = checkOutDate;
                getBookingsInput.BookingStatusType = newBookingStatusType;


                getBookingsInput.BookingCode = txtBookingCode.Text.Trim();

                getBookingsInput.AgentId = Convert.ToInt32(ddlAgent.SelectedValue.ToString());
                getBookingsInput.RefAgentId = Convert.ToInt32(ddlRefAgent.SelectedValue.ToString());
                oBookingData = oBookingManager.GetBookings(getBookingsInput);
                if (bookingStatusType == ENums.BookingStatusTypes.PROPOSED)
                {
                    oBookingData = oBookingData.FindAll(delegate (ViewBookingDTO booking) { return booking.ProposedBooking == true; });
                }
                SessionServices.ViewBooking_BookingData = oBookingData;
            }
        }
        else
        {
            oBookingData = SessionServices.ViewBooking_BookingData;

        }

        dgBookings.DataSource = null;
        dgBookings.DataBind();
        if (oBookingData != null && oBookingData.Count > 0)
        {
            dgBookings.DataSource = oBookingData;
            if (dgBookings.PageCount > 0)
            {
                dgBookings.CurrentPageIndex = dgBookings.CurrentPageIndex > dgBookings.PageCount ? dgBookings.PageCount : dgBookings.CurrentPageIndex;
            }
            dgBookings.DataBind();
        }
        else
        {
            if (IsPostBack)
            {
                base.DisplayAlert("Bookings are not found for the mentioned criteria.");
            }
        }
        oBookingManager = null;
        oBookingData = null;
    }

    private ENums.BookingStatusTypes GetBookingStatusType(string BookingStatus)
    {
        ENums.BookingStatusTypes BST = ENums.BookingStatusTypes.NONE;
        switch (BookingStatus.ToUpper())
        {
            case "CHOOSE":
            case "ALL":
                BST = ENums.BookingStatusTypes.NONE;
                break;
            case "BOOKED":
                BST = ENums.BookingStatusTypes.BOOKED;
                break;
            case "CONFIRMED":
                BST = ENums.BookingStatusTypes.CONFIRMED;
                break;
            case "WAITLISTED":
                BST = ENums.BookingStatusTypes.WAITLISTED;
                break;
            case "CANCELLED":
                BST = ENums.BookingStatusTypes.CANCELLED;
                break;
            case "PROPOSED":
                BST = ENums.BookingStatusTypes.PROPOSED;
                break;
            default:
                break;
        }
        return BST;
    }
    #endregion
    protected void chkPayStatus_CheckedChanged(object sender, EventArgs e)
    {

        string strCon;
        System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)sender;

        DataGridItem grow = (DataGridItem)chk.NamingContainer;

        strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;

        int Bookid = Convert.ToInt32(dgBookings.DataKeys[grow.ItemIndex]);

        try
        {
            SqlConnection cn = new SqlConnection(strCon);

            string query = "update tblbooking set PaymentStatus=@Pstatus where BookingId=@BookingId";
            SqlCommand cmd = new SqlCommand(query, cn);




            cmd.Parameters.AddWithValue("@Pstatus", chk.Checked);
            cmd.Parameters.AddWithValue("@BookingId", Bookid);
            cmd.CommandType = CommandType.Text;
            cn.Open();
            int Status = cmd.ExecuteNonQuery();
            cn.Close();

            GridRefresh();


        }
        catch (Exception)
        {

        }

    }
}
