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
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.Common.DataEntities.Masters;

using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;

public partial class Rate_MarketMaster : System.Web.UI.Page
{
    BALmarket blmarket = new BALmarket();
    DALMarket dlMarket = new DALMarket();
    DataTable dtGetReturnedData;
    int GetQueryResponse = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            this.BindGridMarkets();
    }

    #region ControlsEvent
    protected void btnSbmit_Click(object sender, EventArgs e)
    {
        try
        {
            blmarket._Action = "AddNewMarket";
            blmarket._marketId = txtmarketName.Text.Trim().Substring(0, 2) + DateTime.Now.ToString("MMddhhmmss");
            blmarket._marketName = txtmarketName.Text.ToString().Trim();
            blmarket._region = txtRegion.Text.ToString().Trim();
            blmarket._specification = txtSpecification.Text.ToString().Trim();
            GetQueryResponse = dlMarket.AddNewMarket(blmarket);
            if (GetQueryResponse > 0)
            {
                string Message = txtmarketName.Text.ToString().Trim() + " Added Successfully.";
                this.ResetControls();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + Message + "')", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Cannot add this market. please see error log.')", true);

        }
        catch (Exception sqe)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Cannot add this market. please see error log.')", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
    #endregion ControlsEvent

    #region user Define Functions
    private void BindGridMarkets()
    {
        try
        {
            blmarket._Action = "GetAllMarkets";
            dtGetReturnedData = dlMarket.GetAllmarkets(blmarket);
            if (dtGetReturnedData != null)
            {
                GridMarkets.DataSource = dtGetReturnedData;
                GridMarkets.DataBind();
            }
            else
            {
                GridMarkets.DataSource = null;
                GridMarkets.DataBind();
            }
        }
        catch(Exception sqe)
        {
            GridMarkets.DataSource = null;
            GridMarkets.DataBind();
        }
    }
    private void ResetControls()
    {
        txtmarketName.Text = string.Empty;
        txtRegion.Text = string.Empty;
        txtSpecification.Text = string.Empty;
    }

    #endregion
   
}