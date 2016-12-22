using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using FarHorizon.Reservations.Bases.BasePages;

public partial class Rate_RateCategoryMaster : MasterBasePage
{
    BALRateCategory blCategory = new BALRateCategory();
    DALRateCategory dlCategory = new DALRateCategory();
    int GetQueryResponse = 0;
    DataTable dtGetReturnedData;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            this.BindAllCategories();
    }
    #region User Define Function
    private void BindAllCategories()
    {
        try
        {
            blCategory._Action = "SelectAllCategory";
            dtGetReturnedData = dlCategory.GetAllCategories(blCategory);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                GridRateCategories.DataSource = dtGetReturnedData;
                GridRateCategories.DataBind();
            }
            else
            {
                GridRateCategories.DataSource = null;
                GridRateCategories.DataBind();
            }
        }
        catch
        {
 
        }
    }
    private void ResetControls()
    {
        txtAltName.Text = string.Empty;
        txtCategoryName.Text = string.Empty;
        txtRemark.Text = string.Empty;

    }
    #endregion



    #region Control Events

    protected void btnSbmit_Click(object sender, EventArgs e)
    {
        try
        {

            blCategory._categoryId = txtCategoryName.Text.Trim().Substring(0, 2) + DateTime.Now.ToString("MMddhhmmss");
            blCategory._CategoryName = txtCategoryName.Text.ToString().Trim();
            blCategory._AltName = txtAltName.Text.ToString().Trim();
            blCategory._Remark = txtRemark.Text.ToString().Trim();
            blCategory._Status = true;

            if (btnSbmit.Text == "Submit")
            {
                blCategory._Action = "AddNewCategory";
               
                GetQueryResponse = dlCategory.AddNewMarket(blCategory);
                if (GetQueryResponse > 0)
                {
                    string Message = "Rate Category " + txtCategoryName.Text.ToString().Trim() + " Added Successfully.";
                    this.ResetControls();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + Message + "')", true);
                    this.BindAllCategories();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Cannot add this rate category.')", true);
                }
            }
            else
            {
                blCategory._categoryId = hfId.Value;
                blCategory._Action = "UpdateCategory";
             
                GetQueryResponse = dlCategory.updateRateCategory(blCategory);
                if (GetQueryResponse > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Rate Category Updated ')", true);
                    BindAllCategories();
                    txtAltName.Text = "";
                    txtCategoryName.Text = "";
                    txtRemark.Text = "";
                    btnSbmit.Text = "Submit";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Rate Category could not be Updated ')", true);
                }




            }
        }
        catch (Exception sqe)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('cannot Add ... please see error log.')", true);

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    #endregion
    protected void GridRateCategories_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                LinkButton lnk=(LinkButton)e.CommandSource;
                GridViewRow grow=(GridViewRow)lnk.NamingContainer;
                string ratid=GridRateCategories.DataKeys[grow.RowIndex].Value.ToString();
                hfId.Value = ratid;
                blCategory._Action = "Getcategoriesbyid";
                blCategory._categoryId = ratid;
                dtGetReturnedData = new DataTable();
                dtGetReturnedData = dlCategory.getCategoriesbyId(blCategory);
                if (dtGetReturnedData != null)
                {
                    txtAltName.Text = dtGetReturnedData.Rows[0]["AltName"].ToString();
                    txtCategoryName.Text = dtGetReturnedData.Rows[0]["RateName"].ToString();
                    txtRemark.Text = dtGetReturnedData.Rows[0]["Remark"].ToString();
                    btnSbmit.Text = "Update";
                }
                else
                {
                    txtAltName.Text = "";
                    txtCategoryName.Text = "";
                    txtRemark.Text = "";
                }

               

            }
        }
        catch
        {
        }
    }
    protected void GridRateCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string ratecatid = GridRateCategories.DataKeys[e.RowIndex].Value.ToString();
            blCategory._categoryId = ratecatid;
            blCategory._Action = "DeleteRateCategory";
            int res = dlCategory.DeleteRateCategory(blCategory);
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Rate Category Deleted')", true);
                BindAllCategories();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Rate Category Could not be deleted')", true);
            }

        }

        catch
        {
        }
    }
}