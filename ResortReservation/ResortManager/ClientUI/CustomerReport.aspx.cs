using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using System.Data;
using FarHorizon.DataSecurity;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Bases.BasePages;

using iTextSharp.tool.xml;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using FarHorizon.Reservations.MasterServices;
using System.Net.Mail;

public partial class ClientUI_CustomerReport : MasterBasePage
{
    private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
    DALCustomers dalcustomer = new DALCustomers();
    BALCustomers balcustomer = new BALCustomers();
    DALOpenDates dlOpenDates = new DALOpenDates();
    BALOpenDates blOpenDates = new BALOpenDates();
    EventMessageMaster emsg = new EventMessageMaster();
    string CompanyName = ConfigurationManager.AppSettings["cName"];
    string CompanyEmail = ConfigurationManager.AppSettings["cEmail"];
    string CompanyAddress = ConfigurationManager.AppSettings["cAddress"];
    string CompanyPhoneNo = ConfigurationManager.AppSettings["cPhoneNo"];
    string CompanyMobile = ConfigurationManager.AppSettings["cMobile"];
    string CompanyLogo = ConfigurationManager.AppSettings["cLogo"];
    string SmtpUserId = ConfigurationManager.AppSettings["SMTPUserId"];
    string SmtpPassword = ConfigurationManager.AppSettings["SMTPPwd"];
    string SmtpHost = ConfigurationManager.AppSettings["SMTPServer"];
    string strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["iLoginId"] != null)
        //{

        //}
        //else
        //{
        //    Response.Redirect("~/Default.aspx");
        //}
        if (!IsPostBack)
        {
            load();
            LoadCountries();
        }
    }
    private void load()
    {
        balcustomer.action = "Selectall";

        DataTable dt = dalcustomer.selectall(balcustomer);
        DataTable dt1 = new DataTable();
        dt1.Clear();
        dt1.Columns.Add("CustId");
        dt1.Columns.Add("Title");
        dt1.Columns.Add("FirstName");
        dt1.Columns.Add("LastName");
        dt1.Columns.Add("Email");
        dt1.Columns.Add("Telephone");
        dt1.Columns.Add("Address1");
        dt1.Columns.Add("Address2");
        dt1.Columns.Add("City");
        dt1.Columns.Add("State");
        dt1.Columns.Add("PostalCode");
        dt1.Columns.Add("CountryName");
        dt1.Columns.Add("Password");
        dt1.Columns.Add("PaymentMethod");

        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt1.NewRow();
                dr["CustId"] = DataSecurityManager.Decrypt(dt.Rows[i]["CustId"].ToString());
                dr["Title"] = DataSecurityManager.Decrypt(dt.Rows[i]["Title"].ToString());
                dr["FirstName"] = DataSecurityManager.Decrypt(dt.Rows[i]["FirstName"].ToString());
                dr["LastName"] = DataSecurityManager.Decrypt(dt.Rows[i]["LastName"].ToString());
                dr["Email"] = DataSecurityManager.Decrypt(dt.Rows[i]["Email"].ToString());
                dr["Telephone"] = DataSecurityManager.Decrypt(dt.Rows[i]["Telephone"].ToString());
                dr["Address1"] = DataSecurityManager.Decrypt(dt.Rows[i]["Address1"].ToString());
                dr["Address2"] = DataSecurityManager.Decrypt(dt.Rows[i]["Address2"].ToString());
                dr["City"] = DataSecurityManager.Decrypt(dt.Rows[i]["City"].ToString());
                dr["State"] = DataSecurityManager.Decrypt(dt.Rows[i]["State"].ToString());
                dr["PostalCode"] = DataSecurityManager.Decrypt(dt.Rows[i]["PostalCode"].ToString());
                dr["CountryName"] = DataSecurityManager.Decrypt(dt.Rows[i]["CountryName"].ToString());
                dr["Password"] = DataSecurityManager.Decrypt(dt.Rows[i]["Password"].ToString());
                dr["PaymentMethod"] = DataSecurityManager.Decrypt(dt.Rows[i]["PaymentMethod"].ToString());

                dt1.Rows.Add(dr);
            }
        }
        if (dt1 != null && dt1.Rows.Count > 0)
        {
            Session["getcustomer"] = dt1;
            dgTouristCount.DataSource = dt1;
            dgTouristCount.DataBind();

        }
    }

    protected void dgTouristCount_EditCommand(object source, DataGridCommandEventArgs e)
    {

    }

    protected void dgTouristCount_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    private void getbycustid(int custid)
    {
        balcustomer.action = "selectbyCustid";
        balcustomer.CustId = custid;
        DataTable dt = dalcustomer.selectbyCustid(balcustomer);
        if (dt != null && dt.Rows.Count > 0)
        {
            txtFisrtName.Text = DataSecurityManager.Decrypt(dt.Rows[0]["FirstName"].ToString());
            txtLastName.Text = DataSecurityManager.Decrypt(dt.Rows[0]["LastName"].ToString());
            txtEmail.Text = DataSecurityManager.Decrypt(dt.Rows[0]["Email"].ToString());
            txtContactNo.Text = DataSecurityManager.Decrypt(dt.Rows[0]["Telephone"].ToString());
            txtAddress1.Text = DataSecurityManager.Decrypt(dt.Rows[0]["Address1"].ToString());
            txtAddress2.Text = DataSecurityManager.Decrypt(dt.Rows[0]["Address2"].ToString());
            txtPost.Text = DataSecurityManager.Decrypt(dt.Rows[0]["City"].ToString());
            txtState.Text = DataSecurityManager.Decrypt(dt.Rows[0]["State"].ToString());
            txtCity.Text = DataSecurityManager.Decrypt(dt.Rows[0]["City"].ToString());
            ddlCountry.SelectedValue = DataSecurityManager.Decrypt(dt.Rows[0]["CountryId"].ToString());
            ddlTitle.SelectedValue = DataSecurityManager.Decrypt(dt.Rows[0]["Title"].ToString());

            //ddlTitle.Items.FindByText(dt.Rows[0]["Title"].ToString()).Selected = true;
            //ddlCountry.Items.FindByValue(dt.Rows[0]["CountryId"].ToString()).Selected = true;


        }
    }

    private void LoadCountries()
    {
        try
        {
            blOpenDates._Action = "GetCountry";
            DataTable dtGetReturnedData = dlOpenDates.BindControls(blOpenDates);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlCountry.DataSource = dtGetReturnedData;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Select Country-", "0"));

                ddlcountries.DataSource = dtGetReturnedData;
                ddlcountries.DataTextField = "CountryName";
                ddlcountries.DataValueField = "CountryId";
                ddlcountries.DataBind();
                ddlcountries.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Select Country-", "0"));

            }
            else
            {
                ddlCountry.Items.Clear();
                ddlCountry.DataSource = null;
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Select Country-", "0"));

                ddlcountries.Items.Clear();
                ddlcountries.DataSource = null;
                ddlcountries.DataBind();
                ddlcountries.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Select Country-", "0"));


            }
        }
        catch (Exception sqe)
        {
            ddlCountry.Items.Clear();
            ddlCountry.DataSource = null;
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "-No Accom-");


        }
    }
    public DataTable selectbyCustid(BALCustomers obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_customers]", cn);
            da.SelectCommand.Parameters.Clear();
            da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
            da.SelectCommand.Parameters.AddWithValue("@CustId", obj.CustId);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            cn.Open();
            da.SelectCommand.ExecuteReader();
            DataTable dtReturnData = new DataTable();
            cn.Close();
            da.Fill(dtReturnData);
            if (dtReturnData != null)
                return dtReturnData;
            else
                return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
    public DataTable searchCustomer(BALCustomers obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_SearchCustomers]", cn);
            da.SelectCommand.Parameters.Clear();
            da.SelectCommand.Parameters.AddWithValue("@SearchFor", DataSecurityManager.Encrypt(txtSearch.Text.Trim().ToString()));
            da.SelectCommand.Parameters.AddWithValue("@Country", ddlcountries.SelectedValue);
            da.SelectCommand.Parameters.AddWithValue("@IsActive", drpStatus.SelectedValue);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            cn.Open();
            da.SelectCommand.ExecuteReader();
            DataTable dtReturnData = new DataTable();
            cn.Close();
            da.Fill(dtReturnData);
            if (dtReturnData != null)
                return dtReturnData;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
            return null;
        }
    }
    protected void dgTouristCount_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Select")
        {
            int custid = Convert.ToInt32(e.CommandArgument.ToString());
            hfId.Value = custid.ToString();
            //hdnRateCardId.Value = RatecardId;
            balcustomer.action = "selectbyCustid";
            balcustomer.CustId = custid;
            DataTable dt = selectbyCustid(balcustomer);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtFisrtName.Text = DataSecurityManager.Decrypt(dt.Rows[0]["FirstName"].ToString());
                txtLastName.Text = DataSecurityManager.Decrypt(dt.Rows[0]["LastName"].ToString());
                txtEmail.Text = DataSecurityManager.Decrypt(dt.Rows[0]["Email"].ToString());
                txtContactNo.Text = DataSecurityManager.Decrypt(dt.Rows[0]["Telephone"].ToString());
                txtAddress1.Text = DataSecurityManager.Decrypt(dt.Rows[0]["Address1"].ToString());
                txtAddress2.Text = DataSecurityManager.Decrypt(dt.Rows[0]["Address2"].ToString());
                txtPost.Text = DataSecurityManager.Decrypt(dt.Rows[0]["PostalCode"].ToString());
                txtState.Text = DataSecurityManager.Decrypt(dt.Rows[0]["State"].ToString());
                txtCity.Text = DataSecurityManager.Decrypt(dt.Rows[0]["City"].ToString());
                ddlCountry.SelectedValue = DataSecurityManager.Decrypt(dt.Rows[0]["CountryId"].ToString());
                ddlTitle.SelectedValue = DataSecurityManager.Decrypt(dt.Rows[0]["Title"].ToString());
                chkStatus.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"] == DBNull.Value ? false : dt.Rows[0]["IsActive"]);

                //ddlTitle.Items.FindByText(dt.Rows[0]["Title"].ToString()).Selected = true;
                //ddlCountry.Items.FindByValue(dt.Rows[0]["CountryId"].ToString()).Selected = true;


            }
        }

        if (e.CommandName == "Sendmail")
        {
            int custid = Convert.ToInt32(e.CommandArgument.ToString());
            hfId.Value = custid.ToString();
            balcustomer.action = "selectbyCustid";
            balcustomer.CustId = custid;
            DataTable dt = selectbyCustid(balcustomer);
            if (dt != null && dt.Rows.Count > 0)
            {
                string name = DataSecurityManager.Decrypt(dt.Rows[0]["FirstName"].ToString());
                string email = DataSecurityManager.Decrypt(dt.Rows[0]["Email"].ToString());
                var chars = "ABCDEF!GHIJKLMNOP@QRSTUVWXYZabc#defghijklm$nopqr&stuvwx*yz0123456789";
                var stringChars = new char[6];
                var random = new Random();
                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }
                var pass = new String(stringChars);
                string password = DataSecurityManager.Encrypt(pass);


                balcustomer.CustId = custid;
              
                balcustomer.Password = password;
                int n = dalcustomer.UpdateforadminPassword(pass, custid);
           

                sendMail(name, email, pass);

            }
        }



    }


    public void sendMail(string name, string email, string password)
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(SmtpHost);
            mail.From = new MailAddress(CompanyEmail);
            mail.To.Add(email);
            mail.Subject = "Change Password";
            string msgsubject = "";
            Random rnd = new Random();
            string Code = rnd.Next(10000, 99999).ToString();
            DataTable dt = emsg.getmessgaeforpassword();
            if (dt != null && dt.Rows.Count > 0)
            {
                msgsubject = dt.Rows[0]["EventMessage"].ToString();
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<div>");
            sb.Append(msgsubject);
            sb.Append(" <div>Dear User : " + name + ", </div> <br/>");
            sb.Append(" <div>Your password is: " + password + "  </div> <div><br/> </div><div><br/></div><div>Thanking you,</div><div><br/></div><div>Reservations Office</div> ");
            sb.Append("</div>");
            CompanyLogo = "http://adventureresortscruises.in/Cruise/booking/ARC_Logo.jpg.png";
            //sb.Append("<img src='http://adventureresortscruises.in/Cruise/booking/ARC_Logo.jpg.png' alt='Image'/><br /><div> Adventure Resorts & Cruises Pvt. Ltd.</div><div> B209, CR Park, New Delhi 110019 </div> <div> Phone: +91 - 011 - 41057370 / 1 / 2 </div><div> Mobile: +91 - 9599755353 </div><div><br/> </div> ");
            sb.Append("<img src='" + CompanyLogo + "' alt='Image'/><br /><div> " + CompanyName + "</div><div> " + CompanyAddress + " </div> <div> Phone: " + CompanyPhoneNo + " </div><div> Mobile: " + CompanyMobile + " </div><div><br/> </div> ");
            mail.IsBodyHtml = true;
            mail.Body = sb.ToString();
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(SmtpUserId, SmtpPassword);
            SmtpServer.EnableSsl = false;
            SmtpServer.Send(mail);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Password has been sent to your email id " + email + "')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
        }
    }





    protected void dgTouristCount_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    private void clear()
    {
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtCity.Text = "";
        txtContactNo.Text = "";
        txtEmail.Text = "";
        txtFisrtName.Text = "";
        txtLastName.Text = "";
        txtPost.Text = "";
        txtState.Text = "";
        chkStatus.Checked = false;
        LoadCountries();
    }
    public int Updateforadmin(BALCustomers obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.InsertCommand = new SqlCommand("[dbo].[sp_customers]", cn);
            da.InsertCommand.Parameters.AddWithValue("@action", obj.action);
            da.InsertCommand.Parameters.AddWithValue("@CustId", DataSecurityManager.Encrypt(obj.CustId.ToString()));
            da.InsertCommand.Parameters.AddWithValue("@Title", DataSecurityManager.Encrypt(obj.Title));
            da.InsertCommand.Parameters.AddWithValue("@FirstName", DataSecurityManager.Encrypt(obj.FirstName));
            da.InsertCommand.Parameters.AddWithValue("@LastName", DataSecurityManager.Encrypt(obj.LastName));
            da.InsertCommand.Parameters.AddWithValue("@Email", DataSecurityManager.Encrypt(obj.Email));
            da.InsertCommand.Parameters.AddWithValue("@Telephone", DataSecurityManager.Encrypt(obj.Telephone));
            da.InsertCommand.Parameters.AddWithValue("@Address1", DataSecurityManager.Encrypt(obj.Address1));
            da.InsertCommand.Parameters.AddWithValue("@Address2", DataSecurityManager.Encrypt(obj.Address2));
            da.InsertCommand.Parameters.AddWithValue("@City", DataSecurityManager.Encrypt(obj.City));
            da.InsertCommand.Parameters.AddWithValue("@State", DataSecurityManager.Encrypt(obj.State));
            da.InsertCommand.Parameters.AddWithValue("@PostalCode", DataSecurityManager.Encrypt(obj.PostalCode));
            da.InsertCommand.Parameters.AddWithValue("@CountryId", DataSecurityManager.Encrypt(obj.CountryId.ToString()));


            da.InsertCommand.CommandType = CommandType.StoredProcedure;
            cn.Open();
            int Status = da.InsertCommand.ExecuteNonQuery();
            cn.Close();
            if (Status > 0)
                return Status;
            else
                return 0;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public void checkpostpostcode(string postcode)
    {
        if (postcode != null && postcode != "")
        {
            if (postcode.Length < 6)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid post code')", true);
                return;
            }
            else
            {
                try
                {
                    long post = Convert.ToInt64(postcode);
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid post code')", true);
                    return;
                }
            }
        }
    }
    private void checkphone(string phone)
    {
        if (phone != null || phone != "")
        {
            try
            {
                long value = Convert.ToInt64(phone);
                if (value < 10)
                {
                    Session["Phonecheck"] = 1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid phone no')", true);
                    return;
                }
            }
            catch
            {
                Session["Phonecheck"] = 1;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid phone no')", true);
                return;
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Plaese Choose Country')", true);
            return;
        }
        if (txtFisrtName.Text == "" || txtLastName.Text == "" || txtContactNo.Text == "" || txtAddress1.Text == "" || txtCity.Text == "" || txtState.Text == "" || txtPost.Text == "" || ddlCountry.SelectedIndex == 0)
        {
            //Session["Phonecheck"] = 1;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Plaese enter valid data')", true);
            return;
        }
        if (txtContactNo.Text != null || txtContactNo.Text != "")
        {
            try
            {
                long value = Convert.ToInt64(txtContactNo.Text);
                if (txtContactNo.Text.Length < 10)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid phone no')", true);
                    return;
                }
            }
            catch
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid phone no')", true);
                return;
            }
        }
        //if (txtPost.Text != null && txtPost.Text != "")
        //{
        //    if (txtPost.Text.Length < 6)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid post code')", true);
        //        return;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            long post = Convert.ToInt64(txtPost.Text);
        //        }
        //        catch
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid post code')", true);
        //            return;
        //        }
        //    }
        //}
        balcustomer.CustId = Convert.ToInt32(hfId.Value);

        balcustomer.action = "updateinadmin";
        balcustomer.Title = ddlTitle.SelectedItem.ToString();
        balcustomer.FirstName = txtFisrtName.Text;
        balcustomer.LastName = txtLastName.Text;
        balcustomer.Email = txtEmail.Text;
        balcustomer.Telephone = txtContactNo.Text;
        balcustomer.Address1 = txtAddress1.Text;
        balcustomer.Address2 = txtAddress2.Text;
        balcustomer.City = txtCity.Text;
        balcustomer.State = txtState.Text;
        balcustomer.PostalCode = txtPost.Text;
        balcustomer.CountryId = Convert.ToInt32(ddlCountry.SelectedValue.ToString());
        balcustomer.IsActive = chkStatus.Checked;
        int n = dalcustomer.Updateforadmin(balcustomer);
        if (n > 0)
        {
            lblmsg.Text = "Update Successfully done";
            lblmsg.ForeColor = System.Drawing.Color.Green;
            hfId.Value = "";

            btnSearch_Click(this, e);
            clear();
        }
        else
        {
            lblmsg.Text = "Please try again";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            return;
        }


    }



    protected void dgTouristCount_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    public void ExportToPdf(DataTable dt)
    {
        Document document = new Document();
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("C:\\Users\\sample.pdf", FileMode.Create));
        document.Open();
        iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);

        PdfPTable table = new PdfPTable(dt.Columns.Count);
        PdfPRow row = null;
        float[] widths = new float[] { 4f, 4f, 4f, 4f };

        table.SetWidths(widths);

        table.WidthPercentage = 100;
        int iCol = 0;
        string colname = "";
        PdfPCell cell = new PdfPCell(new Phrase("Products"));

        cell.Colspan = dt.Columns.Count;

        foreach (DataColumn c in dt.Columns)
        {

            table.AddCell(new Phrase(c.ColumnName, font5));
        }

        foreach (DataRow r in dt.Rows)
        {
            if (dt.Rows.Count > 0)
            {
                table.AddCell(new Phrase(r[0].ToString(), font5));
                table.AddCell(new Phrase(r[1].ToString(), font5));
                table.AddCell(new Phrase(r[2].ToString(), font5));
                table.AddCell(new Phrase(r[3].ToString(), font5));
            }
        }
        document.Add(table);
        document.Close();
    }
    protected void dgTouristCount_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgTouristCount.PageIndex = e.NewPageIndex;

        DataTable dt = Session["getcustomer"] as DataTable;
        dgTouristCount.DataSource = dt;
        dgTouristCount.DataBind();
    }

    protected void Unnamed1_Click(object sender, EventArgs e)
    {



    }


    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void dgTouristCount_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void dgTouristCount_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {





    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = searchCustomer(balcustomer);
        DataTable dt1 = new DataTable();
        dt1.Clear();
        dt1.Columns.Add("CustId");
        dt1.Columns.Add("Title");
        dt1.Columns.Add("FirstName");
        dt1.Columns.Add("LastName");
        dt1.Columns.Add("Email");
        dt1.Columns.Add("Telephone");
        dt1.Columns.Add("Address1");
        dt1.Columns.Add("Address2");
        dt1.Columns.Add("City");
        dt1.Columns.Add("State");
        dt1.Columns.Add("PostalCode");
        dt1.Columns.Add("CountryName");
        dt1.Columns.Add("Password");
        dt1.Columns.Add("PaymentMethod");
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt1.NewRow();
                dr["CustId"] = DataSecurityManager.Decrypt(dt.Rows[i]["CustId"].ToString());
                dr["Title"] = DataSecurityManager.Decrypt(dt.Rows[i]["Title"].ToString());
                dr["FirstName"] = DataSecurityManager.Decrypt(dt.Rows[i]["FirstName"].ToString());
                dr["LastName"] = DataSecurityManager.Decrypt(dt.Rows[i]["LastName"].ToString());
                dr["Email"] = DataSecurityManager.Decrypt(dt.Rows[i]["Email"].ToString());
                dr["Telephone"] = DataSecurityManager.Decrypt(dt.Rows[i]["Telephone"].ToString());
                dr["Address1"] = DataSecurityManager.Decrypt(dt.Rows[i]["Address1"].ToString());
                dr["Address2"] = DataSecurityManager.Decrypt(dt.Rows[i]["Address2"].ToString());
                dr["City"] = DataSecurityManager.Decrypt(dt.Rows[i]["City"].ToString());
                dr["State"] = DataSecurityManager.Decrypt(dt.Rows[i]["State"].ToString());
                dr["PostalCode"] = DataSecurityManager.Decrypt(dt.Rows[i]["PostalCode"].ToString());
                dr["CountryName"] = DataSecurityManager.Decrypt(dt.Rows[i]["CountryName"].ToString());
                dr["Password"] = DataSecurityManager.Decrypt(dt.Rows[i]["Password"].ToString());
                dr["PaymentMethod"] = DataSecurityManager.Decrypt(dt.Rows[i]["PaymentMethod"].ToString());

                dt1.Rows.Add(dr);
            }
        }
        if (dt1 != null && dt1.Rows.Count > 0)
        {
             Session["getcustomer"] = dt1;
            dgTouristCount.DataSource = dt1;
            dgTouristCount.DataBind();

        }
        else
        {

            dgTouristCount.DataSource = null;
            dgTouristCount.DataBind();

        }
    }
}
