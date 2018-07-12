using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.Common.DataEntities.Masters;

using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using FarHorizon.DataSecurity;
using System.Text.RegularExpressions;
using System.Data.SqlClient;


public partial class MasterUI_AgentMaster : MasterBasePage
{
    BALAgentPayment blAgentpayment = new BALAgentPayment();
    DALAgentPayment dlAgentpayment = new DALAgentPayment();
    int GetQueryResponse = 0;
    DataTable dtGetReturenedData;

    string agenturl = "";

    BALLinks blLinks = new BALLinks();
    DALLinks dlLinks = new DALLinks();
    DataTable dtGetReturnedData;
    DALOpenDates dlOpenDates = new DALOpenDates();
    BALOpenDates blOpenDates = new BALOpenDates();
    SqlConnection con;
    #region ControlsEvents

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                con = new SqlConnection(GetConnectionString());
                string url = Request.Url.ToString();

                int index = url.LastIndexOf("/");
                if (index > 0)
                    url = url.Substring(0, index); // or index + 1 to keep slash
                url = url.Substring(0, url.LastIndexOf("/") + 1);



                SqlDataAdapter adp = new SqlDataAdapter("select max(agentid) from tblAgentMaster", con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    int id = Convert.ToInt32(dt.Rows[0][0]) + 1;
                    txtUrl.Text = url + "Cruise/Booking/searchproperty.aspx?agentid=" + id + "";
                  ViewState["agenturl"] = url + "Cruise/Booking/searchproperty.aspx?agentid=" + id + "";
                }


                Commission.Visible = false;
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?')");

                RefreshGrid();
                EnableNewButton();
                FillAgents();
                LoadCountries();
                loadoncredits();
                loadagenttype();
                Commission.Visible = false;
            }
        }
        catch { }
    }

    private string GetConnectionString()
    {

        return Convert.ToString(System.Configuration.ConfigurationManager.ConnectionStrings["ReservationConnectionString"]);


    }
    private void LoadCountries()
    {
        try
        {
            blOpenDates._Action = "GetCountry";
            DataTable dtCountries = dlOpenDates.BindControls(blOpenDates);
            if (dtCountries.Rows.Count > 0)
            {
                ddlCountry.DataSource = dtCountries;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("-Select Country-", "0"));


            }
            else
            {
                ddlCountry.Items.Clear();
                ddlCountry.DataSource = null;
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("-Select Country-", "0"));

            }
        }
        catch (Exception sqe)
        {
            ddlCountry.Items.Clear();
            ddlCountry.DataSource = null;
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "-No Accom-");


        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ClearControls();
        btnAddNew.Enabled = false;
        btnCancel.Enabled = true;
        btnDelete.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = true;
        lblStatus.Text = "Add New action initiated";
        txtAgentName.Focus();
    }

    private void ClearControls()
    {
        txtAgentName.Text = String.Empty;
        txtAgentCode.Text = String.Empty;
        txtAgentEmailId.Text = String.Empty;
        txtPassword.Text = String.Empty;
        txtPhone.Text = String.Empty;
        txtCategory.Text = String.Empty;
        txtCountry.Text = String.Empty;
        txtCreditLimit.Text = String.Empty;
        txtBillingAddress.Text = String.Empty;
        txtAgntUrl.Text = String.Empty;
        chkPmntbypass.Checked = false;
       // txtUrl.Text= String.Empty;



    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int Id = 0;
        if (ValidateValues() == false)
            return;
        int.TryParse(hfId.Value, out Id);
        if (Id == 0)
        {
            lblStatus.Text = "Add action initiated.";
            Save();
        }
        else
        {
            lblStatus.Text = "Update action initiated.";
            Update();
        }
        hfId.Value = "";
        RefreshGrid();
        EnableNewButton();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "Save action initiated";
        if (ValidateValues() == false)
            return;
        Save();
        RefreshGrid();
        EnableNewButton();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //lblStatus.Text = "Delete Action initiated";
        Delete();
        EnableNewButton();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        EnableNewButton();
        ClearControls();
        hfId.Value = "";
        //SessionHandler"AgentID"] = null;
        lblStatus.Text = "Action Cancelled";
    }

   
    protected void dgAgents_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iAgentID = Convert.ToInt32(dgAgents.DataKeys[dgAgents.SelectedIndex].ToString());

        hfId.Value = iAgentID.ToString();
        //SessionHandler"AgentID"] = iAgentID;
        AgentMaster oAgentMaster = new AgentMaster();
        AgentDTO[] oAgentData = oAgentMaster.GetData(iAgentID);
        if (oAgentData.Length > 0)
        {
            txtAgentCode.Text = oAgentData[0].AgentCode.ToString();
            txtAgentName.Text = oAgentData[0].AgentName.ToString();
            txtAgentEmailId.Text = oAgentData[0].EmailId.ToString();
            //  txtPassword.TextMode =TextBoxMode.SingleLine ;
            txtPassword.Text = oAgentData[0].Password.ToString();
            txtCountry.Text = oAgentData[0].country.ToString();
            txtCategory.Text = oAgentData[0].category.ToString();
            txtUrl.Text = oAgentData[0].RedirectURL.ToString();
            if (txtUrl.Text == "")
            {
                string url = Request.Url.ToString();

                int index = url.LastIndexOf("/");
                if (index > 0)
                    url = url.Substring(0, index);
                url = url.Substring(0, url.LastIndexOf("/") + 1);
                txtUrl.Text = url + "Cruise/Booking/searchproperty.aspx?agentid=" + iAgentID + "";
            }
            if (oAgentData[0].IsPaymentBypass == 1)
            {
                chkPmntbypass.Checked = true;
                chkPmntbypass_CheckedChanged(this, e);
                txtAgntUrl.Text = oAgentData[0].AgentURL.ToString();
            }
            else
            {
                chkPmntbypass.Checked = false;
                chkPmntbypass_CheckedChanged(this, e);
            }
            if (oAgentData[0].localagent == 1)
            {
                chklocal.Checked = true;
            }
            HiddenField1.Value= oAgentData[0].CssPath.ToString();
            //   txtPassword.TextMode = TextBoxMode.Password;
        }
        oAgentMaster = null;
        oAgentData = null;

        #region fillingPaymentInfo
        dtGetReturenedData = new DataTable();
        blAgentpayment.agentid = iAgentID;
        blAgentpayment._Action = "getPaymentInfo";
        dtGetReturenedData = dlAgentpayment.GetAgentPaymentInfo(blAgentpayment);
        if (dtGetReturenedData != null)
        {
            if (dtGetReturenedData.Rows.Count > 0)
            {
                txtBillingAddress.Text = dtGetReturenedData.Rows[0]["BillingAddress"].ToString();
                txtCreditLimit.Text = dtGetReturenedData.Rows[0]["CreditLimit"].ToString();
                chkOnCredit.Checked = Convert.ToBoolean(dtGetReturenedData.Rows[0]["OnCredit"].ToString() == "" ? "false" : dtGetReturenedData.Rows[0]["OnCredit"].ToString());
                ddlpaymentMethod.SelectedValue = dtGetReturenedData.Rows[0]["PaymentMethod"].ToString();
                txtPhone.Text = dtGetReturenedData.Rows[0]["phone"].ToString();
                txtCommission.Text = dtGetReturenedData.Rows[0]["Commision"].ToString();
            }
            else
            {
                txtBillingAddress.Text = "";
                txtCreditLimit.Text = "";
                chkOnCredit.Checked = false;
                ddlpaymentMethod.ClearSelection();
            }

        }
        #endregion
        //btnAddNew.Enabled = false;
        //btnCancel.Enabled = true;
        btnDelete.Enabled = true;
        btnCancel.Visible = true;
        btnEdit.Text = "Update";
        //txtAgentEmailId.ReadOnly = true;
        //btnSave.Enabled = false;
        lblStatus.Text = "";



    }
    protected void dgAgents_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        int iAgentID = Convert.ToInt32(dgAgents.DataKeys[e.Item.ItemIndex].ToString());
        AgentMaster oAgentMaster = new AgentMaster();
        AgentDTO oAgentData = new AgentDTO();
        oAgentData.AgentId = iAgentID;
        oAgentMaster.Delete(oAgentData);
        ClearControls();
        RefreshGrid();
        oAgentData = null;
        oAgentMaster = null;
    }

    #endregion ControlsEvents

    #region UserDefinedFunctions
    private void Save()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        int agentId;
        AgentDTO oAgentData = new AgentDTO();
        oAgentData.AgentCode = Convert.ToString(txtAgentCode.Text.Trim());
        oAgentData.AgentName = Convert.ToString(txtAgentName.Text.Trim());
        oAgentData.EmailId = txtAgentEmailId.Text.Trim();
        oAgentData.Password = txtPassword.Text.Trim();
        oAgentData.category = txtCategory.Text.Trim();
        oAgentData.country = txtCountry.Text.Trim();
        oAgentData.category = txtCategory.Text.Trim();
        //oAgentData.RedirectURL = txtUrl.Text.Trim();
        oAgentData.RedirectURL = ViewState["agenturl"].ToString();
        oAgentData.AgentURL = txtAgntUrl.Text.Trim();
        if (chkPmntbypass.Checked)
        {
            oAgentData.IsPaymentBypass = 1;
        }
        else
        {
            oAgentData.IsPaymentBypass = 0;
        }
        if (chklocal.Checked)
        {
            oAgentData.localagent = 1;
        }
        else
        {
            oAgentData.localagent = 0;
        }

        var guid = Guid.NewGuid().ToString();
        string filename = uploadLogo.PostedFile.FileName;
       

        if (uploadLogo.PostedFile.ContentLength > 0)
        {
            filename = guid + filename;
            string uploadPath = "/Cruise/Booking/css/agent_css/";
            string rootedpath = HttpContext.Current.Server.MapPath(uploadPath);
            string savepath = rootedpath + filename;
           
            string newpath = rename(savepath);
            uploadLogo.PostedFile.SaveAs(newpath);         
            string newfilename = Path.GetFileName(newpath);
            oAgentData.CssPath = uploadPath + newfilename;
        }
        else
        {
            oAgentData.CssPath = null;
        }


        AgentMaster oAgentMaster = new AgentMaster();

        agentId = oAgentMaster.Insert(oAgentData);

        if (agentId > -1)
        {
            base.DisplayAlert("The record has been inserted successfully");
            PaymentInfo(agentId);
            ClearControls();
        }
        else
            lblStatus.Text = "Error Occured while insertion: Please refer to the error log.";
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

    private void PaymentInfo(int agentId)
    {
        try
        {
            //dtGetReturenedData = new DataTable();
            //dtGetReturenedData = dlAgentpayment.getagentmasterinfo(Convert.ToInt32(ddlAgent.SelectedValue));

            blAgentpayment._Action = "AddDetails";
            blAgentpayment._FirstName = txtAgentName.Text;
            blAgentpayment._LastName = "";
            blAgentpayment._AgentCode = agentId;
            blAgentpayment._PaymentMethod = ddlpaymentMethod.SelectedItem.Text;
            blAgentpayment._EmailId = txtAgentEmailId.Text.Trim();
            //   blAgentpayment._AgentCode = Convert.ToInt32(dtGetReturenedData.Rows[0]["AgentId"].ToString());
            blAgentpayment._Password = txtPassword.Text.Trim();
            blAgentpayment.Phone = txtPhone.Text.Trim();
            blAgentpayment._country = txtCountry.Text.Trim();
            blAgentpayment._category = txtCategory.Text.Trim();
            blAgentpayment._BillingAddress = txtBillingAddress.Text.Trim().ToString();
            blAgentpayment.OnCredit = chkOnCredit.Checked;
            blAgentpayment.CreditLimit = Convert.ToDecimal(txtCreditLimit.Text.Trim() == "" ? "0" : txtCreditLimit.Text.Trim());
            decimal commission = 0;
            if (txtCommission.Text == "")
            {
                blAgentpayment.comission = 0;
            }
            else
            {
                blAgentpayment.comission = Convert.ToDecimal(txtCommission.Text);
            }


            GetQueryResponse = dlAgentpayment.AddpaymentDetails(blAgentpayment);
            if (GetQueryResponse > 0)
            {
                //  ClearAllControls();
                lbStatus.Text = "Payment details saved successfully.";
                lbStatus.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                lbStatus.Text = "Not Saved.. Please check ur entries once";
                lbStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception sqe)
        {
            lbStatus.Text = "Unexpected error found . please check ur entries.";
            lbStatus.ForeColor = System.Drawing.Color.Red;
        }

    }

    private void UpdatePaymentInfo()
    {
        try
        {
            //dtGetReturenedData = new DataTable();
            //dtGetReturenedData = dlAgentpayment.getagentmasterinfo(Convert.ToInt32(ddlAgent.SelectedValue));
            int Id;
            int.TryParse(hfId.Value, out Id);

            blAgentpayment._Action = "updatepaymenInfo";
            blAgentpayment._FirstName = txtAgentName.Text;
            blAgentpayment._LastName = "";
            blAgentpayment._PaymentMethod = ddlpaymentMethod.SelectedItem.Text;
            blAgentpayment._EmailId = txtAgentEmailId.Text.Trim();
            blAgentpayment._AgentCode = Id;
            blAgentpayment.Phone = txtPhone.Text.Trim();
            if (txtPassword.Text != "")
            {
                blAgentpayment._Password = txtPassword.Text.Trim();
            }
            else
            {
                blAgentpayment._Password = null;
            }

            blAgentpayment._BillingAddress = txtBillingAddress.Text.Trim().ToString();
            blAgentpayment.OnCredit = chkOnCredit.Checked;
            blAgentpayment.CreditLimit = Convert.ToDecimal(txtCreditLimit.Text.Trim() == "" ? "0" : txtCreditLimit.Text.Trim());
            blAgentpayment._category = txtCategory.Text.Trim().ToString();
            blAgentpayment._country = txtCountry.Text.Trim().ToString();
            blAgentpayment.comission = Convert.ToDecimal(txtCommission.Text);
            //Make update changes
            GetQueryResponse = dlAgentpayment.UpdatepaymentDetails(blAgentpayment);
            //show changes response
            if (GetQueryResponse > 0)
            {
                //  ClearAllControls();
                lbStatus.Text = "Payment details saved successfully.";
                lbStatus.ForeColor = System.Drawing.Color.Green;
                txtAgentEmailId.ReadOnly = false;
            }
            else
            {
                lbStatus.Text = "Not Saved.. Please check ur entries once";
                lbStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception sqe)
        {
            lbStatus.Text = "Unexpected error found . please check ur entries.";
            lbStatus.ForeColor = System.Drawing.Color.Red;
        }

    }


    private void Update()
    {
        int Id = 0;
        bool bActionCompleted;

        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        if (ValidateValues() == false)
            return;

        int.TryParse(hfId.Value, out Id);
        if (Id == 0)
        {
            lblStatus.Text = "Please click on edit again.";
            return;
        }

        AgentDTO oAgentData = new AgentDTO();
        oAgentData.AgentId = Id;
        oAgentData.AgentName = txtAgentName.Text.Trim();
        oAgentData.AgentCode = txtAgentCode.Text.Trim();
        oAgentData.EmailId = txtAgentEmailId.Text.Trim();
        oAgentData.category = txtCategory.Text.Trim();
        oAgentData.country = txtCountry.Text.Trim();
        oAgentData.RedirectURL = txtUrl.Text.Trim();

        oAgentData.AgentURL = txtAgntUrl.Text.Trim();

        if (chkPmntbypass.Checked)
        {
            oAgentData.IsPaymentBypass = 1;
        }
        else
        {
            oAgentData.IsPaymentBypass = 0;
        }
        if (txtPassword.Text != "")
        {
            oAgentData.Password = txtPassword.Text.Trim();
        }
        else
        {
            oAgentData.Password = null;
        }
        if (chklocal.Checked)
        {
            oAgentData.localagent = 1;
        }
        else
        {
            oAgentData.localagent = 0;
        }

        var guid = Guid.NewGuid().ToString();
        string filename = uploadLogo.PostedFile.FileName;


        if (uploadLogo.PostedFile.ContentLength > 0)
        {
            filename = guid + filename;
            string uploadPath = "/Cruise/Booking/css/agent_css/";
            string rootedpath = HttpContext.Current.Server.MapPath(uploadPath);
            string savepath = rootedpath + filename;

            string newpath = rename(savepath);
            uploadLogo.PostedFile.SaveAs(newpath);
            string newfilename = Path.GetFileName(newpath);
            oAgentData.CssPath = uploadPath + newfilename;
        }
        else
        {
            oAgentData.CssPath = HiddenField1.Value;
        }
       

        AgentMaster oAgentMaster = new AgentMaster();
        bActionCompleted = oAgentMaster.Update(oAgentData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully");
            UpdatePaymentInfo();
            ClearControls();

        }
        else
            lblStatus.Text = "Error Occured while updation: Please refer to the error log.";
    }
    private void Delete()
    {
        int Id = 0;
        bool bActionCompleted;

        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        int.TryParse(hfId.Value, out Id);
        if (Id == 0)
        {
            lblStatus.Text = "Please click on edit again.";
            return;
        }
        if (txtAgentName.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter the Agent.";
            return;
        }

        if (txtAgentCode.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter the Agent.";
            return;
        }
        AgentMaster oAgentMaster = new AgentMaster();
        AgentDTO oAgentData = new AgentDTO();
        oAgentData.AgentId = Id;

        /*
         * 
         * CHECK IF THE AGENT WHICH IS TO BE DELETED HAS ANY ASSOCIATED RECORDS...IF YES, MOVE OUT OF THE FUNCTION ELSE PROCEED
         * IF THE OUTPUT OF sMessage IS "", THEN RECORD CAN BE DELETED, ELSE NOT
         * 
         */
        string sMessage = "";
        GF.HasRecords(Convert.ToString(Id), "agent", out sMessage);
        if (sMessage != "")
        {
            base.DisplayAlert(sMessage);
            btnDelete.Enabled = true;
        }
        else
        {
            bActionCompleted = oAgentMaster.Delete(oAgentData);
            if (bActionCompleted == true)
            {
                ClearControls();
                RefreshGrid();
                oAgentData = null;
                oAgentMaster = null;
                //lblStatus.Text = "Deleted";
                base.DisplayAlert("The record has been deleted successfully");
            }
            else
            {
                base.DisplayAlert("Error Occured while deletion: Please refer to the error log.");
            }
            //lblStatus.Text = "Error Occured while deletion: Please refer to the error log.";
        }
    }
    private void RefreshGrid()
    {
        DataTable dt = new DataTable();
        AgentMaster oAgentMaster = new AgentMaster();
        try
        {
            AgentDTO[] oAgentData = oAgentMaster.GetData();
            dt = dlAgentpayment.getPaymentInfoall(blAgentpayment);
            //if (oAgentData != null)
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //dt.Rows[i]["AgentEmailId"] = DataSecurityManager.Decrypt(dt.Rows[i]["AgentEmailId"].ToString());
                    dt.Rows[i]["AgentName"] = DataSecurityManager.Decrypt(dt.Rows[i]["AgentName"].ToString());
                    dt.Rows[i]["AgentEmailId"] = DataSecurityManager.Decrypt(dt.Rows[i]["AgentEmailId"].ToString());
                    dt.Rows[i]["Category"] = DataSecurityManager.Decrypt(dt.Rows[i]["Category"].ToString());
                    dt.Rows[i]["Country"] = DataSecurityManager.Decrypt(dt.Rows[i]["Country"].ToString());
                }

                //if (oAgentData.Length > 0)
                {
                    Session["Getagentdetils"] = dt;
                    dgAgents.DataSource = dt;
                    dgAgents.DataBind();
                }
            }
            else
            {
                dgAgents.DataSource = null;
                dgAgents.DataBind();
            }
            ClearControls();
            oAgentData = null;
            oAgentMaster = null;
        }
        catch { }
    }
    private void EnableNewButton()
    {
        //btnAddNew.Enabled = true;
        //btnCancel.Enabled = false;
        btnDelete.Enabled = false;
        btnCancel.Visible = false;
        btnEdit.Text = "Add";
        //btnEdit.Enabled = false;
        //btnSave.Enabled = false;
    }
    private bool ValidateValues()
    {
        if (txtAgentCode.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter Agent Code.";
            return false;
        }
        if (txtAgentName.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter Agent Name.";
            return false;
        }
        if (!String.IsNullOrEmpty(txtAgentEmailId.Text.Trim()))
        {
            if (!GF.ValidateEmailId(txtAgentEmailId.Text.Trim()))
            {
                lblStatus.Text = "Please enter correct email id.";
                return false;
            }
        }

        return true;
    }
    #endregion UserDefinedFunctions
    protected void dgAgents_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "MaptoRate")
        {
            try
            {

                LinkButton lnk = (LinkButton)e.CommandSource;
                DataGridItem gitem = (DataGridItem)lnk.NamingContainer;
                int agentid = Convert.ToInt32(dgAgents.DataKeys[gitem.ItemIndex].ToString());

                string url = "../Rate/MapAgentsToRate.aspx?agentId=" + agentid;
                Response.Redirect(url);
                //Response.Write("<script> window.open('" + url + "','_blank'); </script>");
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "popup", "<script language=javascript>window.open('" + url + "','','width=300px,height=200px').focus();</script>");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "<script language=javascript> window.open('" + url + "','','width=300px,height=200px').focus();</script>", true);
            }
            catch
            {
            }

        }
    }

    protected void dgAgents_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgAgents.CurrentPageIndex = e.NewPageIndex;
        RefreshGrid();
    }
    private void FillAgents()
    {
        try
        {
            AgentMaster oAgentMaster = new AgentMaster();
            AgentDTO[] oAgentData = oAgentMaster.GetData();

            ListItemCollection li = new ListItemCollection();
            ListItem l = new ListItem("Choose Agent", "0");
            ddlagent.Items.Insert(0, l);
            if (oAgentData != null)
            {
                for (int i = 0; i < oAgentData.Length; i++)
                {
                    l = new ListItem(oAgentData[i].AgentName.ToString(), oAgentData[i].AgentId.ToString());
                    ddlagent.Items.Insert(i + 1, l);
                }
            }
        }
        catch { }
    }
    private void loadoncredits()
    {
        ddlOncredits.Items.Clear();
        ListItem[] items = new ListItem[3];
        items[0] = new ListItem("Select", "0");
        items[1] = new ListItem("Yes", "1");
        items[2] = new ListItem("No", "2");

        ddlOncredits.Items.AddRange(items);
        ddlOncredits.DataBind();


    }
    private void loadagenttype()
    {
        ddlAgentType.Items.Clear();
        ListItem[] items = new ListItem[3];
        items[0] = new ListItem("Select", "0");
        items[1] = new ListItem("Local", "1");
        items[2] = new ListItem("Foregin", "2");

        ddlAgentType.Items.AddRange(items);
        ddlAgentType.DataBind();


    }
    protected void dgAgents_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

    }
    private void ValidateEmail(string mail)
    {
        string email = mail;
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(email);
        if (match.Success)
        {

        }

        else
        {
            Session["Phonecheck"] = 1;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid email')", true);
            return;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Session["Getagentdetils"] != null)
        {
            DataTable dt = Session["Getagentdetils"] as DataTable;
            DataView dv = new DataView();
            DataSet ds = new DataSet();
            if (ddlagent.SelectedItem.ToString() != "Choose Agent" && txtEmail.Text == "" && ddlCountry.SelectedIndex == 0 && ddlOncredits.SelectedIndex == 0 && ddlAgentType.SelectedIndex == 0)
            {
                dv = new DataView(dt, "AgentName = '" + ddlagent.SelectedItem.ToString() + "'", "AgentName", DataViewRowState.CurrentRows);
                dt = dv.ToTable();
                dgAgents.DataSource = dt;
                dgAgents.DataBind();

                //dt.DefaultView.RowFilter = "AccomName='" + txtAccomdation.Text.Trim() + "'";
            }
            else if (ddlagent.SelectedItem.ToString() == "Choose Agent" && txtEmail.Text != "" && ddlCountry.SelectedIndex == 0 && ddlOncredits.SelectedIndex == 0 && ddlAgentType.SelectedIndex == 0)
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(txtEmail.Text);
                if (match.Success)
                {
                    dv = new DataView(dt, "AgentEmailId = '" + txtEmail.Text + "'", "AgentEmailId", DataViewRowState.CurrentRows);
                    dt = dv.ToTable();
                    dgAgents.DataSource = dt;
                    dgAgents.DataBind();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid email')", true);
                    return;
                }

                //dt.DefaultView.RowFilter = "AccomName='" + txtAccomdation.Text.Trim() + "'";
            }
            else if (ddlagent.SelectedItem.ToString() == "Choose Agent" && txtEmail.Text == "" && ddlCountry.SelectedIndex > 0 && ddlOncredits.SelectedIndex == 0 && ddlAgentType.SelectedIndex == 0)
            {
                dv = new DataView(dt, "Country = '" + ddlCountry.SelectedItem.ToString() + "'", "Country", DataViewRowState.CurrentRows);
                dt = dv.ToTable();
                dgAgents.DataSource = dt;
                dgAgents.DataBind();

                //dt.DefaultView.RowFilter = "AccomName='" + txtAccomdation.Text.Trim() + "'";
            }
            else if (ddlagent.SelectedItem.ToString() == "Choose Agent" && txtEmail.Text == "" && ddlCountry.SelectedIndex == 0 && ddlOncredits.SelectedIndex > 0 && ddlAgentType.SelectedIndex == 0)
            {
                dv = new DataView(dt, "Oncredits = '" + ddlOncredits.SelectedItem.ToString() + "'", "Oncredits", DataViewRowState.CurrentRows);
                dt = dv.ToTable();
                dgAgents.DataSource = dt;
                dgAgents.DataBind();

                //dt.DefaultView.RowFilter = "AccomName='" + txtAccomdation.Text.Trim() + "'";
            }
            else if (ddlagent.SelectedItem.ToString() == "Choose Agent" && txtEmail.Text == "" && ddlCountry.SelectedIndex == 0 && ddlOncredits.SelectedIndex == 0 && ddlAgentType.SelectedIndex > 0)
            {
                dv = new DataView(dt, "localagent = '" + ddlAgentType.SelectedItem.ToString() + "'", "localagent", DataViewRowState.CurrentRows);
                dt = dv.ToTable();
                dgAgents.DataSource = dt;
                dgAgents.DataBind();

                //dt.DefaultView.RowFilter = "AccomName='" + txtAccomdation.Text.Trim() + "'";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please search with one field')", true);
                return;
            }
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        RefreshGrid();
        txtEmail.Text = "";
        loadagenttype();
        FillAgents();
        LoadCountries();
        loadoncredits();
        loadagenttype();
    }



    protected void chkPmntbypass_CheckedChanged(object sender, EventArgs e)
    {
        if(chkPmntbypass.Checked==true)
        {
            txtAgntUrl.Visible = true;
            agnt.Visible = true;
        }
        if(chkPmntbypass.Checked==false)
        {
            txtAgntUrl.Visible = false;
            agnt.Visible = false;
        }
    }
}
