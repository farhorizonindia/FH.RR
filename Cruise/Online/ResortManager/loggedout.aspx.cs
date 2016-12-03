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
using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;

public partial class loggedout : ClientBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string OperationMode;
        OperationMode = Request.QueryString["op"];
        if (string.Compare(OperationMode, "logout") == 0)
        {
            logoutmsg.Text = "You successfully logged out from the application";
        }
        else
        {
            logoutmsg.Text = "Your session is expired.";
        }
    }
}
