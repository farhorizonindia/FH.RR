using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Common.DataEntities.Masters;

public partial class MasterUI_ChangeTreeType : MasterBasePage
{
    #region Event handlers
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillTreeType();
        }
        else if (IsPostBack)
        {
        }
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Update))
            return;

        if (string.Compare(ddlTreeType.SelectedItem.Text, "Choose Tree Type", true) == 0)
            return;

        SetTreeTypeasDefault(GetSelectedTreeType());
        SessionServices.DeleteSession(Constants._BookingChart_TreeDTO);
        SessionServices.DeleteSession(Constants._BookingChart_TreeType);
        CloseWindow();
    }
    #endregion Event handlers

    #region Helper Methods
    private void FillTreeType()
    {
        //tblMaster = new Table();
        BookingChartServices bookingChartServices = new BookingChartServices();
        ListItem l = null;

        ddlTreeType.Items.Insert(0, "Choose Tree Type");
        TreeTypeDTO[] oTreeTypeData = null;
        oTreeTypeData = bookingChartServices.GetTreeTypes();

        if (oTreeTypeData != null)
        {
            for (int i = 0; i < oTreeTypeData.Length; i++)
            {
                l = new ListItem();
                l.Value = oTreeTypeData[i].TreeTypeId.ToString();
                l.Text = oTreeTypeData[i].Description;
                ddlTreeType.Items.Insert(i + 1, l);
                if (oTreeTypeData[i].Selected == true)
                    lblTreeType.Text = oTreeTypeData[i].Description;
            }
        }
        ddlTreeType.SelectedIndex = 0;
    }

    private void SetTreeTypeasDefault(int TreeTypeId)
    {
        BookingChartServices bookingChartServices = null;
        bookingChartServices = new BookingChartServices();
        bookingChartServices.SetDefaultTreeType(TreeTypeId);
    }

    private int GetSelectedTreeType()
    {
        int SelItemVal = 0;
        int.TryParse(ddlTreeType.SelectedItem.Value, out SelItemVal);
        return SelItemVal;
    }

    private void CloseWindow()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("window.opener.document.forms[0].submit();");
        sb.Append("window.close();");
        ClientScript.RegisterClientScriptBlock(this.GetType(), "CloseWindowScript", sb.ToString(), true);
    }
    #endregion Helper Methods
}
