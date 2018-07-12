using FarHorizon.DataSecurity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for BaseClass
/// </summary>
public class BaseClass: System.Web.UI.Page
{
    string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;
    protected override void OnLoad(EventArgs e)
    {
        // ... add custom logic here ...

        // Be sure to call the base class's OnLoad method!

    

        //Session["CssPath"] = "";

        if ( Convert.ToString( Request.QueryString["agentid"]) != null )
        {
           // string agentid = Request.QueryString["agentid"];
             int agentid = 0;
            agentid = Convert.ToInt32(Request.QueryString["agentid"]);
            SqlDataAdapter adp = new SqlDataAdapter("select CssPath,AgentEmailId,IsPaymentByPass from tblagentmaster where agentid=" + agentid + "", strCon);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string path = dt.Rows[0][0].ToString();
                Session["CssPath"] = path;
                Session["AgentId"] = agentid;

                string email = DataSecurityManager.Decrypt(dt.Rows[0][1].ToString());
                Session["AgentEmailId"] = email;
                string ss= dt.Rows[0][2].ToString();
                if (ss != "")
                {
                    Session["IsPaymentByPass"] = Convert.ToBoolean(dt.Rows[0][2]);
                }
                else
                {
                    Session["IsPaymentByPass"] = false;
                }
                AddCss(path);


                try
                {
                    if (Session["SetCurrency"].ToString() == "")
                    {
                        Session["SetCurrency"] = "INR";
                    }
                }catch { Session["SetCurrency"] = "INR"; }
                //Response.Cookies["agentInfo"]["CssPath"] = path;
                //Response.Cookies["agentInfo"]["AgentId"] = Convert.ToInt32(agentid).ToString();
                //Response.Cookies["agentInfo"]["AgentEmailId"] = email;
                //Response.Cookies["agentInfo"]["IsPaymentByPass"] = Convert.ToBoolean(dt.Rows[0][2]).ToString();
                //Response.Cookies["agentInfo"].Expires = DateTime.Now.AddDays(1);

                HttpCookie aCookie = new HttpCookie("agentInfo");
                aCookie.Values["CssPath"] = path;
                aCookie.Values["AgentId"] = Convert.ToInt32(agentid).ToString();
                aCookie.Values["AgentEmailId"] = email;
                if (ss != "")
                {
                    aCookie.Values["IsPaymentByPass"] = Convert.ToBoolean(dt.Rows[0][2]).ToString();
                }else
                {
                    aCookie.Values["IsPaymentByPass"] = false.ToString();

                }
                aCookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(aCookie);



                //  changeCss.Href = "~/Cruise/Booking/css/agent_css/green.css";
            }
        }

        //else if (Session["AgentId"]!=null)
        else if (Request.Cookies["agentInfo"] != null)
        {
            if (Convert.ToString(Request.QueryString["agentid"]) == null)
            {
                Session["CssPath"] = null;
                Session["AgentId"] = null;
                Session["AgentEmailId"] = null;
                Session["IsPaymentByPass"] = null;
                //Session["SetCurrency"] = "";
            }
            else
            {
                HttpCookie reqCookies = Request.Cookies["agentInfo"];
                if (reqCookies != null)
                {
                    string csspath = reqCookies["CssPath"].ToString();
                    int agentid = Convert.ToInt32(reqCookies["AgentId"]);
                    string email = reqCookies["AgentEmailId"].ToString();
                    bool ss = Convert.ToBoolean(reqCookies["IsPaymentByPass"]);
                    Session["CssPath"] = csspath;
                    Session["AgentId"] = agentid; Session["AgentEmailId"] = email; Session["IsPaymentByPass"] = ss;

                    AddCss(csspath);
                }
            }
           // AddCss(Convert.ToString( Session["CssPath"]));

        }
        else
        {
            //Session["SetCurrency"] = "";
        }
        //else
        //{
        //    Session["CssPath"] = "/Cruise/Booking/css/Newcss/style.css";

        //}



        base.OnLoad(e);
    }
    

    private void AddCss(string path)
    {

        HtmlLink htmlLink = new HtmlLink();
        htmlLink.Href = path;
        htmlLink.Attributes.Add("rel", "stylesheet");
        htmlLink.Attributes.Add("type", "text/css");


        Page.Header.Controls.Add(htmlLink);
    }

    protected override void OnInit(EventArgs e)
    {
        
    }


}