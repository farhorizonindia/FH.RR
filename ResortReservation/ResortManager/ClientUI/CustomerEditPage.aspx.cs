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
public partial class ClientUI_CustomerEditPage : System.Web.UI.Page
{
    DALCustomers dalcustomer = new DALCustomers();
    BALCustomers balcustomer = new BALCustomers();
    DALOpenDates dlOpenDates = new DALOpenDates();
    BALOpenDates blOpenDates = new BALOpenDates();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCountries();
            if (Session["getcustid"] != null)
            {
                getbycustid(Convert.ToInt32(Session["getcustid"].ToString()));
            }
            else
            {
                btnUpdate.Visible = false;
            }
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

            }
            else
            {
                ddlCountry.Items.Clear();
                ddlCountry.DataSource = null;
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Select Country-", "0"));


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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (Session["getcustid"] != null)
        {
            balcustomer.CustId = Convert.ToInt32(Session["getcustid"].ToString());
        }
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
        int n = dalcustomer.Updateforadmin(balcustomer);
        if (n > 0)
        {
            lblmsg.Text = "Update Successfully done";
            lblmsg.ForeColor = System.Drawing.Color.Green;
            Session["getcustid"] = null;
            Response.Redirect("CustomerReport.aspx");
            //load();
            //clear();
        }
        else
        {
            lblmsg.Text = "Please try again";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            return;
        }


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
}