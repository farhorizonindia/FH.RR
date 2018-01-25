using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data;
using System.Globalization;
using System.Web;
using System.Net;
using System.Text;
using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;

public partial class Cruise_Masters_RoomImages : MasterBasePage
{
    DataTable dtGetReturnedData;
    BALAgentRooms blar = new BALAgentRooms();
    DALAgentRooms dlar = new DALAgentRooms();
    BALRateCard blCard = new BALRateCard();
    DALRateCard dlcard = new DALRateCard();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAccomType();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubmit.Text == "Submit")
        {
            InsertRoomImage();
        }
        else
        {
            UpdateRoomImage();

        }
    }

    public void UpdateRoomImage()
    {
        try
        {

            blar._Action = "UpdateImages";
            blar.RoomCategoryId = Convert.ToInt32(ddlRoomCategory.SelectedValue);
            blar._AccomId = Convert.ToInt32(ddlAccom.SelectedValue);
            blar._id = Convert.ToInt32(hfId.Value);
            string filename = uploadLogo.PostedFile.FileName;
            if (uploadLogo.PostedFile.ContentLength > 0)
            {
                string uploadPath = "/Cruise/Booking/inv/";
                string rootedpath = HttpContext.Current.Server.MapPath(uploadPath);
                string savepath = rootedpath + filename;

                string newpath = rename(savepath);

                uploadLogo.PostedFile.SaveAs(newpath);
                UploadFileToFTP(newpath);
                string newfilename = Path.GetFileName(newpath);
                blar.ImagePath = uploadPath + newfilename;
            }
            else
            {
                blar.ImagePath = null;
            }
            int res = dlar.UpdateRoomImages(blar);
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Image has been updated successfully')", true);
                BindRoomImages(Convert.ToInt32(ddlAccom.SelectedValue));
                btnSubmit.Text = "Submit";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Image could not be updated ')", true);
            }
        }

        catch
        {

        }
    }


    private void BindAccomType()
    {
        try
        {
            blCard._Action = "GetAllAcoomTypes";
            dtGetReturnedData = dlcard.BindControls(blCard);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlAccomType.Items.Clear();
                ddlAccomType.DataSource = dtGetReturnedData;
                ddlAccomType.DataTextField = "AccomType";
                ddlAccomType.DataValueField = "AccomTypeId";
                ddlAccomType.DataBind();
                ddlAccomType.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            else
            {
                ddlAccomType.Items.Clear();
                ddlAccomType.Items.Insert(0, new ListItem("-Select-", "0"));
            }
        }
        catch
        {
        }
    }



    public void InsertRoomImage()
    {
        try
        {
            blar._Action = "InsertImages";
            blar.RoomCategoryId = Convert.ToInt32(ddlRoomCategory.SelectedValue);
            blar._AccomId = Convert.ToInt32(ddlAccom.SelectedValue);
            string filename = uploadLogo.PostedFile.FileName;
            if (uploadLogo.PostedFile.ContentLength > 0)
            {
                string uploadPath = "/Cruise/Booking/inv/";
                string rootedpath = HttpContext.Current.Server.MapPath(uploadPath);
                string savepath = rootedpath + filename;

                string newpath = rename(savepath);
              
                uploadLogo.PostedFile.SaveAs(newpath);
                UploadFileToFTP(newpath);
                string newfilename = Path.GetFileName(newpath);
                blar.ImagePath = uploadPath + newfilename;


                //using (System.Net.WebClient client = new System.Net.WebClient())
                //{
                //    client.Credentials = new System.Net.NetworkCredential("UploadImage","Augurs@123");
                //    client.UploadFile("ftp.hrpws.com" + "/" + new FileInfo(uploadPath + newfilename).Name, "STOR", newfilename);
                //}
               
            }
            else
            {
                blar.ImagePath = null;
            }
            int res = dlar.AddRoomImages(blar);
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Image has been added successfully')", true);
                BindRoomImages(Convert.ToInt32(ddlAccom.SelectedValue));
                ddlAccom.ClearSelection();
                ddlAccomType.ClearSelection();
                ddlRoomCategory.ClearSelection();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Image Could Not be added ')", true);
            }


    }
        catch(Exception ex)
        {
        }
    }

    private void UploadFileToFTP(string filename)
    {
        FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create("ftp://163.172.206.126/Cruise/Booking/inv/" + Path.GetFileName(filename) + "");

        ftpReq.UseBinary = true;
        ftpReq.Method = WebRequestMethods.Ftp.UploadFile;
        ftpReq.Credentials = new NetworkCredential("advmain", "dAsd133DS@");

        byte[] b = File.ReadAllBytes(filename);
        ftpReq.ContentLength = b.Length;
        using (Stream s = ftpReq.GetRequestStream())
        {
            s.Write(b, 0, b.Length);
        }

        FtpWebResponse ftpResp = (FtpWebResponse)ftpReq.GetResponse();

        if (ftpResp != null)
        {
            if (ftpResp.StatusDescription.StartsWith("226"))
            {
                Console.WriteLine("File Uploaded.");
            }
        }
    }

    public string rename(string fullpath)
    {
        try
        {
            int count = 1;

            string fileNameOnly = Path.GetFileNameWithoutExtension(fullpath);
            string extension = Path.GetExtension(fullpath);
            string path = Path.GetDirectoryName(fullpath);
            string newFullPath = fullpath;

            while (File.Exists(newFullPath))
            {
                string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                newFullPath = Path.Combine(path, tempFileName + extension);
            }
            return newFullPath;
        }
        catch
        {
            return null;
        }

    }

    protected void ddlAccomType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            blCard._Action = "GetAccom";
            blCard._AccomTypeId = Convert.ToInt32(ddlAccomType.SelectedItem.Value);
            dtGetReturnedData = dlcard.GetAccom(blCard);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlAccom.Items.Clear();
                ddlAccom.DataSource = dtGetReturnedData;
                ddlAccom.DataTextField = "AccomName";
                ddlAccom.DataValueField = "AccomId";
                ddlAccom.DataBind();
                ddlAccom.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            else
            {
                ddlAccom.Items.Clear();
                ddlAccom.DataSource = null;
                ddlAccom.DataBind();
                ddlAccom.Items.Insert(0, new ListItem("-Select-", "0"));
            }
        }
        catch (Exception sqe)
        {
            ddlAccom.Items.Clear();
            ddlAccom.DataSource = null;
            ddlAccom.DataBind();
            ddlAccom.Items.Insert(0, new ListItem("-Select-", "0"));
        }
    }
    protected void ddlAccomName_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindroomcat(Convert.ToInt32(ddlAccom.SelectedValue));
        BindRoomImages(Convert.ToInt32(ddlAccom.SelectedValue));
    }

    public void BindRoomImages(int acmid)
    {
        try
        {
            blar._AccomId = acmid;
            blar._Action = "GetImages";
            dtGetReturnedData = new DataTable();
            dtGetReturnedData = dlar.getallRoomImages(blar);
            if (dtGetReturnedData != null)
            {
                gdvRoomImages.DataSource = dtGetReturnedData;
                gdvRoomImages.DataBind();
            }
            else
            {
                gdvRoomImages.DataSource = null;
                gdvRoomImages.DataBind();
            }

        }
        catch
        {

            gdvRoomImages.DataSource = null;
            gdvRoomImages.DataBind();
        }
    }


    public void bindroomcat(int acmid)
    {
        try
        {
            blCard._Action = "getAllRoomCategory";
            blCard._AccomId = acmid;
            DataTable dtGetReturnedData1 = new DataTable();
            dtGetReturnedData1 = dlcard.GetRoomCatbyAccomid(blCard);
            if (dtGetReturnedData1.Rows.Count > 0)
            {
                ddlRoomCategory.DataSource = dtGetReturnedData1;
                ddlRoomCategory.DataTextField = "RoomCategory";
                ddlRoomCategory.DataValueField = "RoomCategoryId";
                ddlRoomCategory.DataBind();
                ddlRoomCategory.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            else
            {
                ddlRoomCategory.Items.Clear();
                ddlRoomCategory.DataSource = null;
                ddlRoomCategory.DataBind();
                ddlRoomCategory.Items.Insert(0, new ListItem("-Select-", "0"));
            }
        }
        catch (Exception sqe)
        {
            ddlRoomCategory.DataSource = null;
            ddlRoomCategory.DataBind();
            ddlRoomCategory.Items.Insert(0, new ListItem("-Select-", "0"));
        }

    }
    protected void gdvRoomImages_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                LinkButton lnk = (LinkButton)e.CommandSource;
                GridViewRow grow = (GridViewRow)lnk.NamingContainer;
                int Id = Convert.ToInt32(gdvRoomImages.DataKeys[grow.RowIndex].Value);
                hfId.Value = Id.ToString();
                blar._id = Id;
                blar._Action = "getImagebyId";
                dtGetReturnedData = new DataTable();
                dtGetReturnedData = dlar.getImageById(blar);
                if (dtGetReturnedData != null)
                {
                    ddlAccomType.SelectedValue = dtGetReturnedData.Rows[0]["AccomTypeId"].ToString();
                    ddlAccom.SelectedValue = dtGetReturnedData.Rows[0]["AccomId"].ToString();
                    ddlRoomCategory.SelectedValue = dtGetReturnedData.Rows[0]["RoomCategoryId"].ToString();
                    Image1.ImageUrl = dtGetReturnedData.Rows[0]["ImagePath"].ToString();


                    btnSubmit.Text = "Update";


                }
                else
                {
                    ddlAccomType.ClearSelection();
                    ddlAccomType.SelectedIndex = 0;
                    ddlAccom.ClearSelection();
                    ddlAccom.SelectedIndex = 0;
                    ddlRoomCategory.ClearSelection();
                    ddlRoomCategory.SelectedIndex = 0;
                    Image1.ImageUrl = "";
                }
            }
        }
        catch
        {
        }
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
    protected void gdvRoomImages_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int Id = Convert.ToInt32(gdvRoomImages.DataKeys[e.RowIndex].Value);
            blar._Action = "DeleteRoomImages";
            blar._id = Id;
            int res = dlar.DeleteRoomImages(blar);
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Image Deleted ')", true);
                BindRoomImages(Convert.ToInt32(ddlAccom.SelectedValue));
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Image Could Not be Deleted ')", true);
            }
        }
        catch
        {
        }
    }
}