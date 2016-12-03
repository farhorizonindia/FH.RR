using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Rate_RateCategoryMaster : System.Web.UI.Page
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
            blCategory._Action = "AddNewCategory";
            blCategory._categoryId = txtCategoryName.Text.Trim().Substring(0, 2) + DateTime.Now.ToString("MMddhhmmss");
            blCategory._CategoryName = txtCategoryName.Text.ToString().Trim();
            blCategory._AltName = txtAltName.Text.ToString().Trim();
            blCategory._Remark = txtRemark.Text.ToString().Trim();
            blCategory._Status = true;
            GetQueryResponse = dlCategory.AddNewMarket(blCategory);
            if (GetQueryResponse > 0)
            {
                string Message ="Rate Category "+ txtCategoryName.Text.ToString().Trim() + " Added Successfully.";
                this.ResetControls();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + Message + "')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Cannot add this rate category.')", true);
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
}