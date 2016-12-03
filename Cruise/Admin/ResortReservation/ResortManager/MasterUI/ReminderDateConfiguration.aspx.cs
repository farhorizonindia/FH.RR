using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.Common.DataEntities.Masters;

using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;
using System.Data;

using FarHorizon.Reservations.Common;


using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;
using System.Configuration;

public partial class MasterUI_ReminderDateConfiguration : MasterBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
         
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
          
            string strCon;
            strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();

            if (btnAdd.Text == "Add")
            {
                da.InsertCommand = new SqlCommand("if not exists(Select * from [dbo].[tblBookingReminder] where EmailId=@EmailId)  Insert into [dbo].[tblBookingReminder] values(@EmailId,@Days)", cn);
                da.InsertCommand.Parameters.AddWithValue("@EmailId", txtEmailId.Text.Trim());
                da.InsertCommand.Parameters.AddWithValue("@Days", Convert.ToInt32(txtRemAfter.Text.Trim()));
                da.InsertCommand.CommandType = CommandType.Text;
                cn.Open();
                int Status = da.InsertCommand.ExecuteNonQuery();
                cn.Close();
                if (Status > 0)
                {
                    base.DisplayAlert("The record has been inserted successfully");
                    BindGrid();
                }
                else
                {
                    base.DisplayAlert("The record could not be inserted");
                }
            }
            else
            {
                da.UpdateCommand = new SqlCommand("update [dbo].[tblBookingReminder] set EmailId=@EmailId,Days=@Days  where Id=@Id", cn);
                da.UpdateCommand.Parameters.AddWithValue("@EmailId", txtEmailId.Text.Trim());
                da.UpdateCommand.Parameters.AddWithValue("@Days", Convert.ToInt32(txtRemAfter.Text.Trim()));
                da.UpdateCommand.Parameters.AddWithValue("@Id", Convert.ToInt32(hfId.Value));
                da.UpdateCommand.CommandType = CommandType.Text;
                cn.Open();
                int Status = da.UpdateCommand.ExecuteNonQuery();
                cn.Close();
                if (Status > 0)
                {
                    base.DisplayAlert("The record has been Updated successfully");
                    BindGrid();
                }
                else
                {
                    base.DisplayAlert("The record could not be Updated");
                }
            }

            
        
               
        }
        catch (Exception ex)
        {
            base.DisplayAlert(ex.Message.ToString());
        }
    }

    public void BindGrid()
    {
        try
        {
            string strCon;
            strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Select * from [dbo].[tblBookingReminder]", cn);
       
   

            da.SelectCommand.CommandType = CommandType.Text;
            cn.Open();
            da.SelectCommand.ExecuteReader();
            DataTable dtReturnData = new DataTable();
            cn.Close();
            da.Fill(dtReturnData);
            if (dtReturnData != null)
            {
                gdvReminderDays.DataSource = dtReturnData;
                gdvReminderDays.DataBind();
            }
            else
            {
                gdvReminderDays.DataSource = null;
                gdvReminderDays.DataBind();
            }
            
         
        }
        catch (Exception)
        {
            gdvReminderDays.DataSource = null;
            gdvReminderDays.DataBind();
        }
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {

            string strCon;
            strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;
            LinkButton lnk = (LinkButton)sender;
            GridViewRow grow = (GridViewRow)lnk.NamingContainer;
            int Id = Convert.ToInt32(gdvReminderDays.DataKeys[grow.RowIndex].Value);
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("Select * from [dbo].[tblBookingReminder] where Id=@Id", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@Id",Id);
             

                da.SelectCommand.CommandType = CommandType.Text;
                cn.Open();
                da.SelectCommand.ExecuteReader();
                DataTable dtReturnData = new DataTable();
                cn.Close();
                da.Fill(dtReturnData);
                if (dtReturnData != null)
                {
                    txtEmailId.Text = dtReturnData.Rows[0]["EmailId"].ToString();
                    txtRemAfter.Text = dtReturnData.Rows[0]["Days"].ToString();
                    btnAdd.Text = "Update";
                    hfId.Value = Id.ToString();

                }
                else
                {
                    txtEmailId.Text = "";
                    txtRemAfter.Text = "";
                }
            
            }
            catch (Exception)
            {
               
            }

        }

        catch
        {
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtRemAfter.Text = "";
        txtEmailId.Text = "";
        hfId.Value = "";
        btnAdd.Text = "Add";
    }
}