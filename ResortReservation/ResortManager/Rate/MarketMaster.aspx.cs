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

public partial class Rate_MarketMaster : MasterBasePage
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
            blmarket._marketName = txtmarketName.Text.ToString().Trim();
            blmarket._region = txtRegion.Text.ToString().Trim();
            blmarket._specification = txtSpecification.Text.ToString().Trim();


            if (btnSbmit.Text == "Submit")
            {

                blmarket._Action = "AddNewMarket";
                blmarket._marketId = txtmarketName.Text.Trim().Substring(0, 2) + DateTime.Now.ToString("MMddhhmmss");

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

            else
            {
                blmarket._Action = "UpdateMarket";
                blmarket._marketId = hfId.Value;
                GetQueryResponse = dlMarket.UpdateMarket(blmarket);

                if (GetQueryResponse > 0)
                {
                    string Message = txtmarketName.Text.ToString().Trim() + " updated Successfully.";
                    btnSbmit.Text = "Submit";
                    this.ResetControls();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + Message + "')", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('could not  update  market')", true);

            }
        }
        catch (Exception sqe)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Could not Process market ')", true);
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

    protected void GridMarkets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                LinkButton lnk = (LinkButton)e.CommandSource;
                GridViewRow grow = (GridViewRow)lnk.NamingContainer;
                int index = grow.RowIndex;
                string marketid = GridMarkets.DataKeys[index].Value.ToString();
                blmarket._marketId = marketid;
                blmarket._Action = "GetMarket";
                dtGetReturnedData = new DataTable();
                dtGetReturnedData = dlMarket.getmarketbyid(blmarket);
                if (dtGetReturnedData != null)
                {
                    txtmarketName.Text = dtGetReturnedData.Rows[0]["Name"].ToString();
                    txtRegion.Text = dtGetReturnedData.Rows[0]["Region"].ToString();
                    txtSpecification.Text = dtGetReturnedData.Rows[0]["specification"].ToString();
                    hfId.Value = dtGetReturnedData.Rows[0]["MarketCode"].ToString();
                    btnSbmit.Text = "Update";

                }


                

            }

    

          

          

          
        }
        catch
        {
        }
    }
    protected void GridMarkets_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
          
            int index = e.RowIndex;
            string marketid = GridMarkets.DataKeys[index].ToString();
            blmarket._Action = "Deletemarket";
            blmarket._marketId = marketid;
            int res = dlMarket.DeleteMarket(blmarket);
            if (res > 0)
            {
                Response.Write("Market Deleted");
                BindGridMarkets();

            }
            else
            {
                Response.Write("Market could not be deleted");
            }

        }
        catch
        {
        }
    }
}