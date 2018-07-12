using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Rate_PackageMaster : MasterBasePage
{
    BALPackageMaster blPackage = new BALPackageMaster();
    DALPackageMaster dlPakage = new DALPackageMaster();
    DataTable dtGetReturnedData;
    int getQueryResponse = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BindGridPackages();
            this.LoadHotels();
            this.LoadDestinations();
        }
    }

    #region UDF
    private void AddPackageNights(string PackageId)
    {
        for (int LoopCounter = 0; LoopCounter < GridCityEachNight.Rows.Count; LoopCounter++)
        {
            try
            {
                Label Night = (Label)GridCityEachNight.Rows[LoopCounter].Cells[1].FindControl("lbNights");
                DropDownList ddlCity = (DropDownList)GridCityEachNight.Rows[LoopCounter].Cells[2].FindControl("ddlcity");
                RadioButton rbCheckInYes = (RadioButton)GridCityEachNight.Rows[LoopCounter].Cells[3].FindControl("rbCheckInYes");
                RadioButton rbcheckInNo = (RadioButton)GridCityEachNight.Rows[LoopCounter].Cells[3].FindControl("rbcheckInNo");
                RadioButton rbCheckOutYes = (RadioButton)GridCityEachNight.Rows[LoopCounter].Cells[4].FindControl("rbCheckOutYes");
                RadioButton rbcheckOutNo = (RadioButton)GridCityEachNight.Rows[LoopCounter].Cells[4].FindControl("rbcheckOutNo");
                int PackCityId = Convert.ToInt32(ddlCity.SelectedItem.Value);
                int packHotelId = 7;
                blPackage._Action = "AddPackageNights";
                blPackage._packageId = PackageId.ToString();
                blPackage._night = Night.Text.ToString();
                blPackage._cityId = PackCityId;
                if (rbCheckInYes.Checked == true)
                    blPackage._AllowCheckIn = true;
                else
                    blPackage._AllowCheckIn = false;

                if (rbCheckOutYes.Checked == true)
                    blPackage._AllowCheckOut = true;
                else
                    blPackage._AllowCheckOut = false;

                getQueryResponse = dlPakage.AddPackageNights(blPackage);
                if (getQueryResponse > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Package Created');", true);
                }
            }
            catch (Exception sqe)
            {

            }
        }

    }
    private void BindGridPackages()
    {
        try
        {
            blPackage._Action = "BindGridPackages";
            dtGetReturnedData = dlPakage.BindControls(blPackage);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                GridPackages.DataSource = dtGetReturnedData;
                GridPackages.DataBind();
            }
            else
            {
                GridPackages.DataSource = null;
                GridPackages.DataBind();
            }
        }
        catch
        {
            GridPackages.DataSource = null;
            GridPackages.DataBind();
        }
    }
    private void LoadHotels()
    {
        try
        {

            blPackage._Action = "GetAllHotels";
            dtGetReturnedData = dlPakage.BindControls(blPackage);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlHotel.DataSource = dtGetReturnedData;
                ddlHotel.DataTextField = "AccomName";
                ddlHotel.DataValueField = "AccomId";
                ddlHotel.DataBind();
                ddlHotel.Items.Insert(0, "-Select Hotel-");
            }
            else
            {
                ddlHotel.Items.Clear();
                ddlHotel.Items.Insert(0, "-No Hotel-");
            }

        }
        catch
        {

        }
    }
    private void LoadDestinations()
    {
        try
        {

            blPackage._Action = "GetAllDestinations";
            dtGetReturnedData = dlPakage.BindControls(blPackage);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlBoardingFrom.DataSource = dtGetReturnedData;
                ddlBoardingFrom.DataTextField = "LocationName";
                ddlBoardingFrom.DataValueField = "LocationId";
                ddlBoardingFrom.DataBind();
                ddlBoardingFrom.Items.Insert(0, "-Select-");

                ddlBoardingTo.DataSource = dtGetReturnedData;
                ddlBoardingTo.DataTextField = "LocationName";
                ddlBoardingTo.DataValueField = "LocationId";
                ddlBoardingTo.DataBind();
                ddlBoardingTo.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlBoardingFrom.Items.Clear();
                ddlBoardingFrom.Items.Insert(0, "-No City-");
                ddlBoardingTo.Items.Clear();
                ddlBoardingTo.Items.Insert(0, "-No City-");
            }
        }
        catch
        {

        }
    }
    private void ClearControls()
    {
        //ddlMasterPackage.SelectedItem.Text = "-Select-";
        //GridCityEachNight.DataSource = null;
        //GridCityEachNight.DataBind();
        //ddlPackageType.DataSource = null;
        //ddlPackageType.DataBind();
        //txtPackageName.Text = string.Empty;
        //ddlnights.SelectedIndex = -1;
        //ddlBoardingFrom.SelectedIndex = -1;
        //ddlBoardingTo.SelectedIndex = -1;
        //ddlHotel.SelectedIndex = -1;


    }
    #endregion

    #region Click events
    protected void btnSbmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSbmit.Text == "Submit")
            {

                blPackage._Action = "GetCountPackages";
                dtGetReturnedData = dlPakage.BindControls(blPackage);
                int CountId = Convert.ToInt32(dtGetReturnedData.Rows[0]["count"]);
                string NewPackageId = "Pack" + (CountId + 1).ToString();
                blPackage._packageId = NewPackageId.ToString();
                blPackage._Action = "AddNewPackage";
                blPackage._creationDate = System.DateTime.Now;
                blPackage._packageName = txtPackageName.Text.ToString();
                blPackage._NoOfNights = Convert.ToInt32(ddlnights.SelectedItem.Text);
                blPackage._pakageType = ddlPackageType.SelectedItem.Text.ToString();
                blPackage.PackageDescription = txtPackageDesc.Text;
                blPackage.ItineraryLink = txtItineraryLink.Text.Trim();

               
                if(chkActive.Checked==true)
                {
                    blPackage.IsActive = true;
                }
                if (chkActive.Checked == false)
                {
                    blPackage.IsActive = false;
                }

                blPackage.ImagePath = UploadImage();

                //  blPackage.Direction = ddlDirection.SelectedValue;
                if (ddlPackageType.SelectedItem.Text == "Child Package")
                    blPackage._MasterPackageId = ddlMasterPackage.SelectedItem.Value;
                else
                    blPackage._MasterPackageId = null;
                blPackage._HotelId = 7;
                blPackage._BoardingFrom = Convert.ToInt32(ddlBoardingFrom.SelectedItem.Value);
                blPackage._BoardingTo = Convert.ToInt32(ddlBoardingTo.SelectedItem.Value);



                getQueryResponse = dlPakage.AddNewPackage(blPackage);
                if (getQueryResponse > 0)
                {
                    this.AddPackageNights(NewPackageId);
                    try
                    {
                        this.BindGridPackages();
                    }
                    catch
                    {
                    }
                    this.ClearControls();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Package Added Successfully');", true);
                    txtPackageName.Text = "";
                    ddlBoardingFrom.ClearSelection();
                    ddlBoardingTo.ClearSelection();
                    ddlMasterPackage.ClearSelection();
                    ddlnights.ClearSelection();
                    ddlPackageType.ClearSelection();
                    txtPackageDesc.Text = "";
                    Image1.ImageUrl = "";
                    txtItineraryLink.Text = ""; chkActive.Checked = false;

                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Not  Added');", true);
            }
            else
            {
                blPackage._packageName = txtPackageName.Text.ToString();
                blPackage._Action = "UpdatePackage";
                blPackage._packageId = hfId.Value.ToString();
                blPackage.PackageDescription = txtPackageDesc.Text;
                blPackage.ItineraryLink = txtItineraryLink.Text.Trim();

                if (chkActive.Checked == true)
                {
                    blPackage.IsActive = true;
                }
                if (chkActive.Checked == false)
                {
                    blPackage.IsActive = false;
                }

                blPackage.ImagePath = UploadImage();

                int res = dlPakage.UpdatePackage(blPackage);
                if (res > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Package Info Updated');", true);
                    btnSbmit.Text = "Submit";
                    txtPackageName.Text = "";
                    ddlBoardingFrom.SelectedIndex = 0;
                    ddlBoardingTo.SelectedIndex = 0;
                    ddlMasterPackage.SelectedIndex = 0;
                    ddlnights.SelectedIndex = 0;
                    ddlPackageType.SelectedIndex = 0; chkActive.Checked = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Package info could  not be updated');", true);
                }
            }
        }
        catch (Exception sqe)
        {
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please check entries')", true);

        }
    }

    private string UploadImage()
    {
        string imagePath = null;
        try
        {
            string filename = Path.GetFileName(uploadLogo.PostedFile.FileName);
            if (uploadLogo.PostedFile.ContentLength > 0)
            {
                string uploadPath = "../RoomImages/";
                string rootedpath = HttpContext.Current.Server.MapPath(uploadPath);

                string savepath = rootedpath + filename;
                string newpath = rename(savepath);
                uploadLogo.PostedFile.SaveAs(newpath);

                string newFileName = Path.GetFileName(newpath);
                imagePath = uploadPath + newFileName;

                byte[] fileBytes = new BinaryReader(uploadLogo.PostedFile.InputStream).ReadBytes(uploadLogo.PostedFile.ContentLength);
                string fn = Path.GetFileName(newpath);
               // UploadFileToSites(fn, fileBytes);
            }
        }
        catch (Exception exp)
        {
            string msg = exp.Message.Replace(@"\", "_");
            msg = msg.Replace("'", "");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "javascript:alert(' error: " + msg + "');", true);
        }
        return imagePath;
    }

    private void UploadFileToSites(string filename, byte[] fileBytes)
    {
        string imageFolder = "RoomImages";

        //string rootedpath = HttpContext.Current.Server.MapPath("..");
        //var siteDirectory = new DirectoryInfo(rootedpath).Name;

        #region Upload To Admin Section
        var siteDirectory = "admin.adventureresortcruises.in";
        string savepath = string.Format(@"{0}\{1}\{2}", siteDirectory, imageFolder, filename);
        string newpath = rename(savepath);
        FTPImage(fileBytes, newpath);
        #endregion

        #region Upload To Admin Section
        siteDirectory = "httpdocs";
        savepath = string.Format(@"{0}\{1}\{2}", siteDirectory, imageFolder, filename);
        newpath = rename(savepath);
        FTPImage(fileBytes, newpath);
        #endregion

        #region Upload To Admin Section
        siteDirectory = "test.adventureresortscruises.in";
        savepath = string.Format(@"{0}\{1}\{2}", siteDirectory, imageFolder, filename);
        newpath = rename(savepath);
        FTPImage(fileBytes, newpath);
        #endregion
    }

    private static void FTPImage(byte[] fileBytes, string newpath)
    {
        string ftpUri = "ftp://ftp.adventureresortscruises.in/" + newpath + "";

        string uId = ConfigurationManager.AppSettings.Get("FtpUid");
        string pwd = ConfigurationManager.AppSettings.Get("FtpPwd");

        FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(ftpUri);
        ftpReq.UseBinary = true;
        ftpReq.Method = WebRequestMethods.Ftp.UploadFile;
        ftpReq.Credentials = new NetworkCredential(uId, pwd);

        ftpReq.ContentLength = fileBytes.Length;
        using (Stream s = ftpReq.GetRequestStream())
        {
            s.Write(fileBytes, 0, fileBytes.Length);
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
    protected void ddlNights_changeEvent(object sender, EventArgs e)
    {
        try
        {
            if (ddlnights.SelectedItem.Text == "-Select-")
            {
                GridCityEachNight.DataSource = null;
                GridCityEachNight.DataBind();
            }
            else
            {
                DataTable dtNights = new DataTable();
                dtNights.Columns.Add("Sn");
                dtNights.Columns.Add("Nights");
                for (int LoopCount = 0; LoopCount < Convert.ToInt32(ddlnights.SelectedItem.Text); LoopCount++)
                {
                    DataRow dr = dtNights.NewRow();
                    dr["Sn"] = (LoopCount + 1).ToString();
                    dr["Nights"] = "Night " + (LoopCount + 1).ToString();
                    dtNights.Rows.Add(dr);
                }
                GridCityEachNight.DataSource = dtNights;
                GridCityEachNight.DataBind();

            }


        }
        catch (Exception sqe)
        {
        }
    }
    protected void ddlpackageType_selectChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPackageType.SelectedItem.Text == "Child Package")
            {
                ddlMasterPackage.Items.Clear();
                ddlMasterPackage.Enabled = true;
                reqfddlMasterPackage.Enabled = true;
                blPackage._Action = "GetAllPackages";
                dtGetReturnedData = dlPakage.BindControls(blPackage);
                if (dtGetReturnedData.Rows.Count > 0)
                {
                    ddlMasterPackage.DataSource = dtGetReturnedData;
                    ddlMasterPackage.DataTextField = "PackageName";
                    ddlMasterPackage.DataValueField = "PackageId";
                    ddlMasterPackage.DataBind();
                    ddlMasterPackage.Items.Insert(0, "-Master Package-");
                }
                else
                {
                    ddlMasterPackage.Items.Clear();
                    ddlMasterPackage.Items.Insert(0, "-No package-");
                }
                ddlnights.Items.Clear();
            }
            else if (ddlPackageType.SelectedItem.Text == "-Select-" || ddlPackageType.SelectedItem.Text == "Master Package")
            {

                ddlMasterPackage.Items.Clear();
                ddlMasterPackage.Enabled = false;
                reqfddlMasterPackage.Enabled = false;
                #region Adding Default no. of nights
                ddlnights.Items.Clear();
                for (int LoopCounter = 0; LoopCounter < 10; LoopCounter++)
                {
                    ddlnights.Items.Add((LoopCounter + 1).ToString());
                    if (LoopCounter == 9)
                        ddlnights.Items.Insert(0, "-Select-");
                }
                #endregion

            }

        }
        catch (Exception sqe)
        {

        }
    }
    protected void GridNights_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlcity = (DropDownList)e.Row.Cells[2].FindControl("ddlcity");
            blPackage._Action = "GetAllDestinations";
            dtGetReturnedData = dlPakage.BindControls(blPackage);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlcity.DataSource = dtGetReturnedData;
                ddlcity.DataTextField = "LocationName";
                ddlcity.DataValueField = "LocationId";
                ddlcity.DataBind();
                ddlcity.Items.Insert(0, "-Select City-");
            }
            else
            {
                ddlcity.Items.Clear();
                ddlcity.Items.Insert(0, "-No City-");
            }




        }
        else
        {

        }
    }
    protected void ddlMasterPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlnights.Items.Clear();

            blPackage._Action = "GetNightsDetail";
            blPackage._packageId = ddlMasterPackage.SelectedItem.Value.ToString();
            dtGetReturnedData = dlPakage.GetNightsDetail(blPackage);

            ddlnights.Items.Insert(0, "-Select-");

            int startPoint = 0;
            int endPoint = 0;
            List<string> nights = new List<string>();
            int i = 0;
            for (i = 0; i < dtGetReturnedData.Rows.Count; i++)
            {
                bool IsAllowCheckIn = Convert.ToBoolean(dtGetReturnedData.Rows[i]["CheckIn"].ToString());

                if (IsAllowCheckIn)
                {
                    startPoint = i;

                    for (int j = i + 1; j < dtGetReturnedData.Rows.Count; j++)
                    {
                        bool IsAllowCheckOut = Convert.ToBoolean(dtGetReturnedData.Rows[j]["CheckOut"].ToString());

                        if (IsAllowCheckOut)
                        {
                            endPoint = j;
                            if (j == dtGetReturnedData.Rows.Count - 1) //If it is last row.
                            {
                                endPoint++;
                            }
                            break;
                        }
                    }
                    nights.Add((endPoint - startPoint).ToString());
                }
            }

            for (int cntr = 0; cntr < nights.Count; cntr++)
            {
                ddlnights.Items.Insert((cntr + 1), nights[cntr]);
            }
        }
        catch (Exception sqe)
        {

        }
    }
    #endregion



    protected void GridPackages_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int res = 0;
            string packageid = GridPackages.DataKeys[e.RowIndex].Value.ToString();

            blPackage._packageId = packageid;
            dtGetReturnedData = new DataTable();
            blPackage._Action = "checkChild";
            dtGetReturnedData = dlPakage.checkChild(blPackage);
            if (dtGetReturnedData != null)
            {
                if (Convert.ToInt32(dtGetReturnedData.Rows[0][0]) > 0)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Child package(s) exist for this package, delete them first');", true);
                }
                else
                {
                    blPackage._Action = "DeletePackage";
                    res = dlPakage.DeletePackage(blPackage);
                }
            }
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Package  Deleted');", true);
                BindGridPackages();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Package could not be  Deleted');", true);
            }

        }

        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Package could not be  Deleted');", true);
        }
    }
    protected void GridPackages_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                LinkButton lnk = (LinkButton)e.CommandSource;
                GridViewRow grow = (GridViewRow)lnk.NamingContainer;
                string packgid = GridPackages.DataKeys[grow.RowIndex].Value.ToString();
                hfId.Value = packgid;
                blPackage._Action = "GetPackagebyPackageId";
                blPackage._packageId = packgid;
                dtGetReturnedData = new DataTable();
                dtGetReturnedData = dlPakage.getPackagebyid(blPackage);
                if (dtGetReturnedData != null)
                {
                    if (dtGetReturnedData.Rows.Count > 0)
                    {
                        txtPackageName.Text = dtGetReturnedData.Rows[0]["PackageName"].ToString();
                        ddlBoardingFrom.SelectedValue = dtGetReturnedData.Rows[0]["BordingFrom"].ToString();
                        ddlBoardingTo.SelectedValue = dtGetReturnedData.Rows[0]["BoadingTo"].ToString();
                        ddlMasterPackage.SelectedValue = dtGetReturnedData.Rows[0]["MasterPackageId"].ToString();
                        ddlnights.SelectedValue = dtGetReturnedData.Rows[0]["NoOfNights"].ToString();
                        ddlPackageType.SelectedValue = dtGetReturnedData.Rows[0]["PackageType"].ToString();
                        txtPackageDesc.Text = dtGetReturnedData.Rows[0]["PackageDescription"].ToString();
                        Image1.ImageUrl = dtGetReturnedData.Rows[0]["PackageImage"].ToString();
                        txtItineraryLink.Text = dtGetReturnedData.Rows[0]["ItineraryLink"].ToString();
                        chkActive.Checked= Convert.ToBoolean(dtGetReturnedData.Rows[0]["IsActive"].ToString());
                        btnSbmit.Text = "Update";
                        BindNights(packgid);
                    }
                    else
                    {
                        txtPackageName.Text = "";
                        ddlBoardingFrom.ClearSelection();
                        ddlBoardingTo.ClearSelection();
                        ddlMasterPackage.ClearSelection();
                        ddlnights.ClearSelection();
                        ddlPackageType.ClearSelection();
                        txtPackageDesc.Text = "";
                        Image1.ImageUrl = "";
                        txtItineraryLink.Text = "";
                        chkActive.Checked = false;

                    }
                }
                else
                {
                    txtPackageName.Text = "";
                    ddlBoardingFrom.ClearSelection();
                    ddlBoardingTo.ClearSelection();
                    ddlMasterPackage.ClearSelection();
                    ddlnights.ClearSelection();
                    ddlPackageType.ClearSelection();
                    txtPackageDesc.Text = "";
                    Image1.ImageUrl = "";
                    txtItineraryLink.Text = ""; chkActive.Checked = false;
                }

            }
        }
        catch
        {

            txtPackageName.Text = "";
            ddlBoardingFrom.ClearSelection();
            ddlBoardingTo.ClearSelection();
            ddlMasterPackage.ClearSelection();
            ddlnights.ClearSelection();
            ddlPackageType.ClearSelection();
            txtPackageDesc.Text = "";
            Image1.ImageUrl = "";
            txtItineraryLink.Text = ""; chkActive.Checked = false;
        }
    }

    public void BindNights(string packid)
    {
        try
        {
            blPackage._Action = "GetPackagencities";
            blPackage._packageId = packid;
            DataTable dtp = new DataTable();
            dtp = dlPakage.getPackagebyid(blPackage);
            if (dtp != null)
            {
                if (dtp.Rows.Count > 0)
                {
                    GridView1.DataSource = dtp;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }

        }

        catch
        {
        }

    }

}