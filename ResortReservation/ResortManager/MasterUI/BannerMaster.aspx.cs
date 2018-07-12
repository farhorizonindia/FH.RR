using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using System.Data;
using FarHorizon.Reservations.Bases.BasePages;
public partial class MasterUI_BannerMaster : MasterBasePage
{
    DALAgentPayment dalagent = new DALAgentPayment();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            selectall();
        }
    }
    private void selectall()
    {
        DataTable dt = dalagent.selectforbanner();
        if (dt != null && dt.Rows.Count > 0)
        {
            gvBanner.DataSource = dt;
            gvBanner.DataBind();
        }
        else
        {
            gvBanner.DataSource = null;
            gvBanner.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtTitle.Text != "")
        {
            string imageone = string.Empty;
            string Pic1url = "";
            if (FileUpload1.HasFile)
            {
                string Fname;
                Fname = Path.GetFileName(FileUpload1.FileName);
                string ex = Path.GetExtension(Fname);
                ex = ex.ToLower();
                if (ex == ".jpg" || ex == ".bmp" || ex == ".png" || ex == ".jpeg")
                {
                    try
                    {
                        string STR = Fname;
                        string[] STR1 = STR.Split('.');
                        string fileName = "";
                        string extention = "";
                        string datetime = DateTime.Now.ToString();
                        datetime = datetime.Replace(' ', '_');
                        datetime = datetime.Replace(':', '_');
                        datetime = datetime.Replace('-', '_');
                        datetime = datetime.Replace('/', '_');
                        datetime = datetime.Replace('\\', '_');
                        fileName = STR1[0];
                        extention = STR1[1];
                        //Random rnd = new Random();
                        //string random = rnd.Next(LowerBoundary, UpperBoundary).ToString();
                        // imageone = "inv/" + fileName + datetime + '.' + extention;
                        imageone = "images/" + fileName + datetime + '.' + extention;
                        //uploadProductPic = "admin/ProjectsPic/" + fileName + datetime + '.' + extention;
                       // Pic1url = "../Cruise/Booking/inv/" + fileName + datetime + "." + extention;

                        Pic1url = "../Cruise/Booking/images/" + fileName + datetime + "." + extention;
                        FileUpload1.SaveAs(Server.MapPath(Pic1url));
                        //createimage
                    }
                    catch { }
                }
                else
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Invalid image format!')</script>", false);
                    return;
                }
            }
            else
            {

                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Browse an image first!')</script>", false);
                    return;
                }
            }
            int n = dalagent.savebanner(imageone, txtTitle.Text);
            if (n == 1)
            {
                lblMsg.Text = "Save Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtTitle.Text = "";
                selectall();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter Title!')</script>", false);
            return;
        }

    }

    protected void gvBanner_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            int n = dalagent.deletebanner(id);
            if (n == 1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Deleted Successfully')</script>", false);
                selectall();

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Try again')</script>", false);
            }
        }
    }

    protected void gvBanner_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}