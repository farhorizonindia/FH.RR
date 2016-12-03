using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.SessionManager;
using System.Web.UI;
using FarHorizon.Reservations.WebHelper;

namespace FarHorizon.Reservations.Bases.BasePages
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            IsStillLoggedIn();
            if (String.IsNullOrEmpty(Page.Title))
            {
                Page.Title = "Reservations: " + Page.Form.Name;
            }
            else
            {
                Page.Title = "Reservations: " + Page.Title;
            }

            if (!IsPostBack)
            {
                ValidateViewAllowed(Request.Url.ToString());
            }
            // Be sure to call the base class's OnLoad method!
            base.OnLoad(e);
        }

        private void IsStillLoggedIn()
        {
            string requestUrl = Request.Url.ToString();

            if (!requestUrl.ToUpper().Contains("DEFAULT.ASPX") && !requestUrl.ToUpper().Contains("LOGGEDOUT.ASPX"))
            {
                if (SessionHelper.LoginId == 0)
                {
                    Response.Redirect("~/loggedout.aspx");
                }
                else
                {
                    Session.Timeout = 30;
                }
            }
        }

        private void ValidateViewAllowed(string screenPath)
        {
            if (screenPath.Contains("mainmenu.aspx"))
            {
                return;
            }

            bool commandAllowed = IsCommandAllowed(screenPath, FarHorizon.Reservations.Common.ENums.PageCommand.View);
            if (!commandAllowed)
            {
                DisplayAlert("You don't have view rights for this screen. Redirecting you to main menu.");
                Response.Redirect(@"~\mainmenu.aspx", true);
            }
        }

        protected bool ValidateIfCommandAllowed(string screenPath, FarHorizon.Reservations.Common.ENums.PageCommand pageCommand)
        {
            bool commandAllowed = IsCommandAllowed(screenPath, pageCommand);
            if (!commandAllowed)
            {
                DisplayAlert("You don't have rights to " + pageCommand.ToString() + " for this screen.");
                return false;
            }
            return true;
        }

        private bool IsCommandAllowed(string screenPath, FarHorizon.Reservations.Common.ENums.PageCommand pageCommand)
        {
            string screenName = screenPath;
            if (screenPath.StartsWith(@"http://") || screenPath.StartsWith(@"https://"))
            {
                screenName = Path.GetFileNameWithoutExtension(screenPath.ToString());

                //This is to handle the Booking Change Room Pax, which is a modal Pop up for Booking screen, 
                //So allowing user to get onto this screen, if he has rights for the booking screen.
                if (screenName.ToUpper() == "BOOKINGCHANGEROOMPAX")
                {
                    screenName = "Booking";
                }

                switch (screenName.ToUpper())
                {
                    case "DEFAULT":
                    case "LOGGEDOUT":
                        return true;                        
                    default:
                        break;
                }
            }

            List<RoleRightsDTO> roleRightsList = null;

            if (SessionHelper.LoggedInUser != null)
            {
                if (SessionHelper.LoggedInUser.User.UserId.Trim().ToUpper() == "ADMIN")
                {
                    return true;
                }
                roleRightsList = SessionHelper.LoggedInUser.RoleRigthsList;
            }

            if (roleRightsList != null)
            {
                //RoleRightsDTO right = roleRightsList.Find(delegate(RoleRightsDTO _right) { return _right.ScreenName.ToUpper() == screenName.ToUpper() && _right.RightKey.ToUpper().StartsWith(pageCommand.ToString().ToUpper()); });
                return roleRightsList.Exists(_right => _right.ScreenName.ToUpper() == screenName.ToUpper() && _right.RightKey.ToUpper().StartsWith(pageCommand.ToString().ToUpper()));

                //return roleRightsList.Exists(_right =>string.Compare(_right.ScreenName, screenName, StringComparison.OrdinalIgnoreCase) == 0 && _right.RightKey.ToUpper().StartsWith(pageCommand.ToString().ToUpper()));                
            }
            //}
            return false;
        }

        protected virtual void DisplayAlert(string message)
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(),
            //  string.Format("alert('{0}');", message.Replace("'", @"\'")), true);
        }

        protected string ConvertObjetToJSON(object objectTobeJSON)
        {
            WebSiteHelper webSiteHelper = new WebSiteHelper();
            return webSiteHelper.ConvertObjetToJSON(objectTobeJSON);
        }

        protected string GetControlId(string htmlId)
        {
            string[] splitter = htmlId.Split('$');
            string id = string.Empty;
            if (splitter != null && splitter.Length > 0)
            {
                id = splitter[splitter.Length - 1];
            }
            return id;
        }

        protected string GetPostBackControlID()
        {
            string ctrl = Request.Params["__EVENTTARGET"];
            if (ctrl == null)
                ctrl = string.Empty;
            return ctrl;
        }

        protected virtual Control FindControl(Control c, string ID)
        {
            Control cntrl = null;
            if (c != null)
            {
                if (c.HasControls() == false)
                {
                    if (c.ID != null)
                    {
                        if (c.ID == ID)
                            return c;
                        else
                            return null;
                    }
                }
                else if (c.Controls.Count > 0)
                {
                    for (int i = 0; i < c.Controls.Count; i++)
                    {
                        cntrl = FindControl(c.Controls[i], ID);
                        if (cntrl != null)
                        {
                            break;
                        }
                    }
                }
            }
            return cntrl;
        }
    }
}
