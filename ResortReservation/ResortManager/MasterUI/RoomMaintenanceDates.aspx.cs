using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.MasterServices;
using System.Data;

public partial class MasterUI_RoomMaintenanceDates : System.Web.UI.Page
{
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillAccomodations();
            EnableNewButton();
            dgRooms.AllowPaging = true;
           
            refreshgrid(0, "");
            AddAttributesToControls();
           
        }
    }

    private void FillAccomodations()
    {
        AccomodationMaster accomodationMaster = new AccomodationMaster();
        List<AccomodationDTO> accomodationsList = new List<AccomodationDTO>(accomodationMaster.GetData());
        tvAccomodations.Nodes.Clear();

        TreeNode rootNode = new TreeNode("Accomodations");
        TreeNode accomodationNode = null;

        if (accomodationsList != null)
        {
            foreach (AccomodationDTO accomodation in accomodationsList)
            {
                accomodationNode = new TreeNode(accomodation.AccomodationName, accomodation.AccomodationId.ToString());
                rootNode.ChildNodes.Add(accomodationNode);
            }
        }
        tvAccomodations.Nodes.Add(rootNode);
        tvAccomodations.ExpandAll();
    }

    protected void tvAccomodations_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            dgRooms.AllowPaging = false;
            dgRooms.DataSource = null;
            dgRooms.DataBind();
            ClearControls();
            int AccomodationId = 0;

            AccomodationId = Convert.ToInt32(tvAccomodations.SelectedNode.Value);
            hfId.Value = AccomodationId.ToString();
            ddlRoomNo.Items.Clear();
           
            Bindrooms(AccomodationId);
            dgRooms.AllowPaging = true;
            refreshgrid(AccomodationId, "");
          
        }
        catch
        {
        }
    }

    private void Bindrooms(int AccomodationId)
    {
        try
        {
            RoomMaster oRoomMaster = new RoomMaster();
            RoomDTO[] oAccomRoomData = null;
            if (AccomodationId != 0)
                oAccomRoomData = oRoomMaster.GetAllRooms(AccomodationId);
            if (oAccomRoomData != null)
            {
                if (oAccomRoomData.Length > 0)
                {
                    ddlRoomNo.DataSource = oAccomRoomData;
                    ListItem l = new ListItem("Choose Room", "0");
                    ddlRoomNo.Items.Insert(0, l);
                    if (oAccomRoomData != null)
                    {
                        for (int i = 0; i < oAccomRoomData.Length; i++)
                        {
                            l = new ListItem(oAccomRoomData[i].RoomNo.ToString(), oAccomRoomData[i].RoomNo.ToString());
                            ddlRoomNo.Items.Insert(i + 1, l);
                        }
                    }

                }
            }
            else
            {


            }
        }
        catch
        {
        }
    }

    private void EnableNewButton()
    {
        btnDelete.Enabled = false;
        btnCancel.Visible = false;
        btnEdit.Text = "Add";
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (txtSeasonStartDate.Text != "")
        {
           
            ds = new DataSet();
            RoomMaster rm = new RoomMaster();
            roommaintainDTO roommaintain = new roommaintainDTO();
            roommaintain = MapControlsToObject();

            ds = rm.checkifroombooked(roommaintain);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblStatus.Text = "this Room is Booked,please cancel the booking first";
            }
            else
            {
                if (String.IsNullOrEmpty(hfOldSeasonStartDate.Value) || String.IsNullOrEmpty(hfOldSeasonEndDate.Value))
                {
                    lblStatus.Text = "Add action initiated.";
                    if (ddlRoomNo.SelectedIndex < 1)
                    {
                        lblStatus.Text = "Please Select a Room no";
                    }
                    else
                    {
                        Save();
                    }
                }

                else
                {
                    lblStatus.Text = "Update action initiated.";
                    if (ddlRoomNo.SelectedIndex <= 0)
                    {
                        lblStatus.Text = "select a room first";
                    }
                    else
                    {
                        Update();
                    }
                }
            }
        }
        else
        {
            lblStatus.Text = "you must enter a start date";
        }
        EnableNewButton();
    }


    private void AddAttributesToControls()
    {


        txtSeasonStartDate.Attributes.Add("onchange", "return fillEndDate('" + txtSeasonStartDate.ClientID + "','" + txtSeasonEndDate.ClientID + "');");


    }



    private void Save()
    {
        RoomMaster rm = new RoomMaster();

        roommaintainDTO roommaintain = new roommaintainDTO();
        roommaintain = MapControlsToObject();
        bool bActionCompleted = rm.InsertmaintenanceDates(roommaintain);
        if (bActionCompleted == true)
        {
            // base.DisplayAlert("The record has been insert successfully");
            // ClearControls();
            lblStatus.Text = "Saved";
        }
        else
        {
            lblStatus.Text = "Error Occured while saving: Please refer to the error log.";
            return;
        }
    }

    private roommaintainDTO MapControlsToObject()
    {
        DateTime dt;
        roommaintainDTO roommaintain = new roommaintainDTO();
        int id = 0;
        string roomid = ddlRoomNo.SelectedValue;
        string reason=txtreason.Text;
        int.TryParse(hfId.Value, out id);
        roommaintain.AccomodationId = id;
        roommaintain.roomId = roomid;
        roommaintain.Reason = reason;
        DateTime.TryParse(txtSeasonStartDate.Text, out dt);
        roommaintain.StartDate = dt;

        DateTime.TryParse(txtSeasonEndDate.Text, out dt);
        roommaintain.EndDate = dt;


        return roommaintain;
    }


    protected void ddlRoomNo_SelectedIndexChanged(object sender, EventArgs e)
    {

        refreshgrid(Convert.ToInt32(hfId.Value), ddlRoomNo.SelectedValue);
        lblStatus.Text = "";
    }

    public void refreshgrid(int hid, string roomno)
    {
        RoomMaster rm = new RoomMaster();
        List<roommaintainDTO> rmdto = rm.GetroommaintainDates(hid,roomno);

        if (rmdto != null && rmdto.Count > 0)
        {
            
            dgRooms.DataSource = rmdto;
            dgRooms.DataBind();
           
        }
        else
        {
            dgRooms.DataSource = null;
            dgRooms.DataBind();
        }
    }

    protected void dgRooms_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iAccomID = Convert.ToInt32(dgRooms.DataKeys[dgRooms.SelectedIndex]);
        Bindrooms(iAccomID);
        hfId.Value = iAccomID.ToString();
        string roomid = dgRooms.Items[dgRooms.SelectedIndex].Cells[0].Text;
        ddlRoomNo.SelectedValue = roomid;
        hfRoomid.Value = roomid;
        DateTime dt;

        DateTime.TryParse(dgRooms.SelectedItem.Cells[1].Text.ToString().Trim(), out dt);
        hfOldSeasonStartDate.Value = txtSeasonStartDate.Text = GF.GetDD_MMM_YYYY(dt, false);

        DateTime.TryParse(dgRooms.SelectedItem.Cells[2].Text.ToString().Trim(), out dt);
        hfOldSeasonEndDate.Value = txtSeasonEndDate.Text = GF.GetDD_MMM_YYYY(dt, false);
        txtreason.Text = dgRooms.Items[dgRooms.SelectedIndex].Cells[4].Text;

        btnCancel.Visible = true;
        btnDelete.Enabled = true;
        btnEdit.Text = "Update";
        lblStatus.Text = "";
    }


    private roommaintainDTO GetroommaintainOldDates()
    {
        roommaintainDTO rmolddates = new roommaintainDTO();
        int id = 0;
        int.TryParse(hfId.Value, out id);

        rmolddates.AccomodationId = id;
        rmolddates.roomId = hfRoomid.Value.ToString();
        DateTime dt;
        DateTime.TryParse(hfOldSeasonStartDate.Value, out dt);
        rmolddates.StartDate = dt;

        DateTime.TryParse(hfOldSeasonEndDate.Value, out dt);
        rmolddates.EndDate = dt;
        return rmolddates;
    }


    private void Update()
    {
        //  if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Update))
        //     return;

        //   if (!ValidateValues())
        //       return;

        bool bActionCompleted;
        roommaintainDTO oldmaintain = GetroommaintainOldDates();
        roommaintainDTO newmaintain = MapControlsToObject();

        RoomMaster rm = new RoomMaster();
        bActionCompleted = rm.UpdateroomMaintaindates(oldmaintain, newmaintain);
        if (bActionCompleted == true)
        {
            //  base.DisplayAlert("The record has been updated successfully");
            //  ClearControls();
            lblStatus.Text = "Updated Successfully";
        }
        else
            lblStatus.Text = "Error Occured while updating: Please refer to the error log.";

        refreshgrid(Convert.ToInt32(hfId.Value), ddlRoomNo.SelectedValue);
        oldmaintain = null;
        newmaintain = null;
        rm = null;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        EnableNewButton();
        ClearControls();
        lblStatus.Text = "Action Cancelled";
    }


    private void ClearControls()
    {
       txtreason.Text= txtSeasonStartDate.Text = txtSeasonEndDate.Text = String.Empty;
        hfOldSeasonStartDate.Value = hfOldSeasonEndDate.Value = String.Empty;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Delete();
        EnableNewButton();
        refreshgrid(Convert.ToInt32(hfId.Value), ddlRoomNo.SelectedValue);
    }


    private void Delete()
    {
        // if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Delete))
        //     return;

        bool bActionCompleted;
        string sMessage = "";
        roommaintainDTO rmdto = MapControlsToObject();
        RoomMaster rm = new RoomMaster();

        int Id = 0;
        int.TryParse(hfId.Value, out Id);
        rmdto.AccomodationId = Id;
        rmdto.roomId = hfRoomid.Value.ToString();

        if (Id == 0)
        {
            lblStatus.Text = "Please click on edit again.";
            return;
        }

      //  GF.HasRecords(Convert.ToString(Id), "accomodation", out sMessage);
        if (sMessage != "")
        {
            // base.DisplayAlert(sMessage);
            btnDelete.Enabled = true;
        }
        else
        {
            bActionCompleted = rm.DeletemaintainDates(rmdto);
            if (bActionCompleted == true)
            {
                // base.DisplayAlert("The record has been deleted successfully.");
                ClearControls();
            }
            else
            {
                // base.DisplayAlert("Error Occured while deletion: Please refer to the error log.");
            }

        }
        rmdto = null;
        rm = null;
    }
    protected void dgRooms_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgRooms.CurrentPageIndex = e.NewPageIndex;
        refreshgrid(0, "");
    }
}