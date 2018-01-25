using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.Bases.BasePages;

public partial class Cruise_Masters_CruiseOpenDatesMaster : MasterBasePage
{
    DALOpenDates dlOpenDates = new DALOpenDates();
    BALOpenDates blOpenDates = new BALOpenDates();
    DALPackageRateCard dalp = new DALPackageRateCard();
    DataTable dtGetReturnedData;
    int getQueryResponse = 0;
    BALBooking blsr = new BALBooking();
    DALBooking dlsr = new DALBooking();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfid.Value = "0";
            this.LoadAccom();
            this.LoadCountries();
            this.LoadAllPackages();
            this.BindGridOpenDates();
            ViewState["flag"] = 0;

        }
    }
    #region UDF
    private void LoadAccom()
    {
        try
        {
            blOpenDates._Action = "GetAllAccom";
            dtGetReturnedData = dlOpenDates.BindControls(blOpenDates);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlAccom.DataSource = dtGetReturnedData;
                ddlAccom.DataTextField = "AccomName";
                ddlAccom.DataValueField = "AccomId";
                ddlAccom.DataBind();
                ddlAccom.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlAccom.Items.Clear();
                ddlAccom.DataSource = null;
                ddlAccom.DataBind();
                ddlAccom.Items.Insert(0, "-No Accom-");
            }
        }
        catch (Exception sqe)
        {
            ddlAccom.Items.Clear();
            ddlAccom.DataSource = null;
            ddlAccom.DataBind();
            ddlAccom.Items.Insert(0, "-No Accom-");

        }
    }
    private void LoadCountries()
    {
        try
        {
            blOpenDates._Action = "GetCountry";
            dtGetReturnedData = dlOpenDates.BindControls(blOpenDates);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlCountry.DataSource = dtGetReturnedData;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlCountry.Items.Clear();
                ddlCountry.DataSource = null;
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, "-No Accom-");
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

    private void LoadAllPackages()
    {
        try
        {
            blOpenDates._Action = "GetPackages";
            dtGetReturnedData = dlOpenDates.BindControls(blOpenDates);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlpackage.DataSource = dtGetReturnedData;
                ddlpackage.DataTextField = "PackageName";
                ddlpackage.DataValueField = "PackageId";
                ddlpackage.DataBind();
                ddlpackage.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlpackage.Items.Clear();
                ddlpackage.DataSource = null;
                ddlpackage.DataBind();
                ddlpackage.Items.Insert(0, "-No Package-");
            }
        }
        catch (Exception sqe)
        {
            ddlpackage.Items.Clear();
            ddlpackage.DataSource = null;
            ddlpackage.DataBind();
            ddlpackage.Items.Insert(0, "-No Package-");

        }
    }

    private void BindGridOpenDates()
    {
        try
        {
            blOpenDates._Action = "GetAllOpenDates";
            dtGetReturnedData = dlOpenDates.BindControls(blOpenDates);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                GridOpenDates.DataSource = dtGetReturnedData;
                GridOpenDates.DataBind();
            }
            else
            {
                GridOpenDates.DataSource = null;
                GridOpenDates.DataBind();
            }
        }
        catch (Exception sqe)
        {
            GridOpenDates.DataSource = null;
            GridOpenDates.DataBind();
        }
    }

    #endregion

    #region Control Click Events
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {



            blOpenDates._checkInDate = Convert.ToDateTime(txtBoardingDate.Text.ToString().Trim());
            blOpenDates._checkOutDate = Convert.ToDateTime(txtDeBordingDate.Text.ToString().Trim());
            blOpenDates._RiverId = Convert.ToInt32(ddlRiver.SelectedItem.Value);
            blOpenDates._CountryId = Convert.ToInt32(ddlCountry.SelectedItem.Value);
            blOpenDates._PackageId = ddlpackage.SelectedItem.Value.ToString();
            blOpenDates._AccomId = Convert.ToInt32(ddlAccom.SelectedItem.Value);
            blOpenDates.Status = true;





            if (btnAdd.Text == "Add")
            {
                blOpenDates._Action = "AddNewOpenDate";
                getQueryResponse = dlOpenDates.AddNewOpenDate(blOpenDates);
                if (getQueryResponse > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Cruise OpenDate has been added successfully')", true);
                    BindGridOpenDates();
                }
            }
            else
            {
                blOpenDates._Action = "UpdateOpenDates";
                blOpenDates.Id = Convert.ToInt32(hfid.Value);
                getQueryResponse = dlOpenDates.UpdateOpenDates(blOpenDates);
                if (getQueryResponse > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Cruise OpenDate has been updated successfully')", true);
                    BindGridOpenDates();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Cruise OpenDate could not be updated')", true);
                }

            }





        }
        catch (Exception sqe)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('cannot add this Open date. please see error log.')", true);
        }
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {


        bindrivers(Convert.ToInt32(ddlCountry.SelectedItem.Value));


    }

    public void bindrivers(int Cid)
    {
        #region Bind RiverDD
        try
        {
            blOpenDates._Action = "GetRiver";
            blOpenDates._CountryId = Cid;
            DataTable dt1 = new DataTable();
            dt1 = dlOpenDates.GetRiverLocation(blOpenDates);
            if (dt1.Rows.Count > 0)
            {
                ddlRiver.DataSource = dt1;
                ddlRiver.DataTextField = "RiverName";
                ddlRiver.DataValueField = "RiverId";
                ddlRiver.DataBind();
                ddlRiver.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlRiver.Items.Clear();
                ddlRiver.DataSource = null;
                ddlRiver.DataBind();
                ddlRiver.Items.Insert(0, "-No River-");
            }
        }
        catch (Exception sqe)
        {
            ddlRiver.Items.Clear();
            ddlRiver.DataSource = null;
            ddlRiver.DataBind();
            ddlRiver.Items.Insert(0, "-No River-");

        }
        #endregion
    }
    protected void btnAdd0_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void txtBoardingDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["flag"].ToString() == "0")
            {
                DateTime date = Convert.ToDateTime(txtBoardingDate.Text);
                int NoOfNights = Convert.ToInt32(hdnfNoOfRooms.Value);
                DateTime dtcalculatedChuckOut = date.AddDays(NoOfNights);


                if (ddlAccom.SelectedIndex != 0 && txtBoardingDate.Text != "")
                {
                    blOpenDates._AccomId = Convert.ToInt32(ddlAccom.SelectedValue);
                    blOpenDates._Action = "CheckSeason";
                    blOpenDates._checkInDate = Convert.ToDateTime(txtBoardingDate.Text.Trim());
                    blOpenDates._checkOutDate = dtcalculatedChuckOut;
                    blOpenDates._PackageId = ddlpackage.SelectedValue;
                    blOpenDates.Id = Convert.ToInt32(hfid.Value);
                    DataTable dt1 = new DataTable();
                    dt1 = dlOpenDates.SeasonCheck(blOpenDates);
                    if (dt1 != null)
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            blOpenDates._Action = "checkduplicatedeparture";
                            DataTable dt2 = dlOpenDates.checkDuplicateDepartures(blOpenDates);
                            if (dt2 != null)
                            {
                                if (dt2.Rows.Count > 0)
                                {
                                    if (Convert.ToInt32(dt2.Rows[0][0].ToString() == "" ? "1" : dt2.Rows[0][0]) > 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('another departure with same checkin  and checkout already exists')", true);
                                        txtDeBordingDate.Text = "";
                                    }
                                    else
                                    {
                                        blOpenDates._Action = "CheckChildPackage";

                                        DataTable dt3 = dlOpenDates.masterpackageallowedcheckins(blOpenDates);
                                        if (dt3 != null)
                                        {
                                            if (dt3.Rows.Count > 0)
                                            {
                                                if (Convert.ToInt32(dt3.Rows[0][0].ToString() == "" ? "0" : dt3.Rows[0][0]) > 0)
                                                {
                                                    txtDeBordingDate.Text = dtcalculatedChuckOut.ToShortDateString();
                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('checkin not allowed on this date')", true);
                                                    txtDeBordingDate.Text = "";
                                                }
                                            }
                                        }







                                    }
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('cannot create departure for dates that are out of season ')", true);
                        }
                    }

                }
            }

            ViewState["flag"] = "0";

        }
        catch
        {
            txtDeBordingDate.Text = string.Empty;
            txtBoardingDate.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please select a package first.')", true);

        }
    }
    protected void ddlpackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        sethiddenfieldnights(ddlpackage.SelectedItem.Value);
    }

    public void sethiddenfieldnights(string pckid)
    {
        try
        {

            blOpenDates._Action = "GetNoOfNights";
            blOpenDates._PackageId = pckid;
            DataTable dt1 = new DataTable();
            dt1 = dlOpenDates.GetNoOfNights(blOpenDates);
            if (dt1.Rows.Count > 0)
            {
                hdnfNoOfRooms.Value = dt1.Rows[0]["NoOfNights"].ToString();

            }
            else
            {
                hdnfNoOfRooms.Value = string.Empty;
            }
            txtBoardingDate.Text = string.Empty;
            txtDeBordingDate.Text = string.Empty;

        }
        catch
        {


        }
    }

    #endregion





    protected void GridOpenDates_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        try
        {


            int id = Convert.ToInt32(GridOpenDates.DataKeys[e.RowIndex].Value);
            blOpenDates.Id = id;
            blOpenDates._Action = "DeleteDeparture";
            int res = dlOpenDates.DeleteOpenDates(blOpenDates);
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Departure Deleted')", true);
                BindGridOpenDates();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Departure could not be Deleted')", true);
            }
        }

        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Departure could not be Deleted')", true);
        }
    }
    public DataTable bindroomddl(int departureid, string packageid)
    {
        try
        {
            blsr.action = "GetcruiseRooms";
            blsr.PackageId = packageid;


            blsr.DepartureId = departureid;

            if (Session["UserCode"] != null)
            {
                blsr._iAgentId = Convert.ToInt32(Session["UserCode"].ToString());
            }
            DataTable dt = new DataTable();
            dt = dlsr.GetCruiseRooms(blsr);
            if (dt != null)
            {

                return dt;
            }
            else
            {
                return null;

            }
        }
        catch
        {
            return null;
        }
    }
    protected void GridOpenDates_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                ViewState["flag"] = "1";
                LinkButton lnk = (LinkButton)e.CommandSource;
                GridViewRow grow = (GridViewRow)lnk.NamingContainer;
                int id = Convert.ToInt32(GridOpenDates.DataKeys[grow.RowIndex].Value);

                hfid.Value = id.ToString();

                blOpenDates._Action = "GetOpenDatesbyId";
                blOpenDates.Id = id;
                dtGetReturnedData = new DataTable();
                dtGetReturnedData = dlOpenDates.getOpenDatesbyId(blOpenDates);
                if (dtGetReturnedData != null)
                {
                    bindrivers(Convert.ToInt32(dtGetReturnedData.Rows[0]["CountryId"].ToString()));
                    sethiddenfieldnights(dtGetReturnedData.Rows[0]["packageId"].ToString());
                    txtBoardingDate.Text = Convert.ToDateTime(dtGetReturnedData.Rows[0]["CheckInDate"]).ToString("MM/dd/yyyy");
                    txtDeBordingDate.Text = Convert.ToDateTime(dtGetReturnedData.Rows[0]["CheckOutDate"]).ToString("MM/dd/yyyy");
                    ddlAccom.SelectedValue = dtGetReturnedData.Rows[0]["AccomId"].ToString();
                    ddlCountry.SelectedValue = dtGetReturnedData.Rows[0]["CountryId"].ToString();
                    ddlpackage.SelectedValue = dtGetReturnedData.Rows[0]["packageId"].ToString();
                    ddlRiver.SelectedValue = dtGetReturnedData.Rows[0]["RiverId"].ToString();
                    btnAdd.Text = "Update";
                }
                else
                {
                    txtBoardingDate.Text = "";
                    txtDeBordingDate.Text = "";
                    ddlAccom.SelectedIndex = 0;
                    ddlCountry.SelectedIndex = 0;
                    ddlpackage.SelectedIndex = 0;
                    ddlRiver.SelectedIndex = 0;

                }



            }
            if (e.CommandName == "Open")
            {
                int getid = Convert.ToInt32(e.CommandArgument.ToString());
                blOpenDates.Id = getid;
                blOpenDates._Action = "GetOpenDatesbyId";
                DataTable dt = dlOpenDates.getOpenDatesbyId(blOpenDates);
                DataTable getavialabl = bindroomddl(getid, dt.Rows[0]["packageId"].ToString());
                DataView dv = new DataView();
                dv = new DataView(getavialabl, "BookedStatus='Not Available'", "BookedStatus", DataViewRowState.CurrentRows);
                DataTable dt2 = dv.ToTable();
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('Close can only happen if there is no booking on those departures')", true);
                    return;

                }
                else
                {
                    blsr.action = "getchilid";
                    blsr.PackageId = dt.Rows[0]["packageId"].ToString();
                    DataTable dtchild = dlsr.getchilid(blsr);
                    if (dtchild != null && dtchild.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtchild.Rows.Count; i++)
                        {
                            blsr.action = "getdepartureid";
                            blsr.PackageId = dtchild.Rows[i]["PackageId"].ToString();
                            blsr._dtStartDate = Convert.ToDateTime(dt.Rows[0]["CheckInDate"].ToString());
                            blsr._dtEndDate = Convert.ToDateTime(dt.Rows[0]["CheckOutDate"].ToString());
                            DataTable dtgetdepurtureid = dlsr.getdepartureid(blsr);
                            if (dtgetdepurtureid != null && dtgetdepurtureid.Rows.Count > 0)
                            {
                                blsr.action = "GetcruiseRooms";
                                blsr.DepartureId = Convert.ToInt32(dtgetdepurtureid.Rows[0]["Id"].ToString());
                                DataTable dtgetchild = dlsr.GetCruiseRooms(blsr);
                                DataView dvgetchild = new DataView();
                                dvgetchild = new DataView(dtgetchild, "BookedStatus='Not Available'", "BookedStatus", DataViewRowState.CurrentRows);
                                dtgetchild = dvgetchild.ToTable();
                                if (dtgetchild != null && dtgetchild.Rows.Count > 0)
                                {
                                    Session["set"] = null;
                                }
                                else
                                {
                                    Session["set"] = 1;
                                }
                            }

                        }
                    }
                    else
                    {
                        blsr.action = "getdepartureid";
                        blsr.PackageId = dt.Rows[0]["PackageId"].ToString();
                        blsr._dtStartDate = Convert.ToDateTime(dt.Rows[0]["CheckInDate"].ToString());
                        blsr._dtEndDate = Convert.ToDateTime(dt.Rows[0]["CheckOutDate"].ToString());
                        DataTable dtgetdepurtureid = dlsr.getdepartureid(blsr);
                        if (dtgetdepurtureid != null && dtgetdepurtureid.Rows.Count > 0)
                        {
                            blsr.action = "GetcruiseRooms";
                            blsr.DepartureId = Convert.ToInt32(dtgetdepurtureid.Rows[0]["Id"].ToString());
                            DataTable dtgetchild = dlsr.GetCruiseRooms(blsr);
                            DataView dvgetchild = new DataView();
                            dvgetchild = new DataView(dtgetchild, "BookedStatus='Not Available'", "BookedStatus", DataViewRowState.CurrentRows);
                            dtgetchild = dvgetchild.ToTable();
                            if (dtgetchild != null && dtgetchild.Rows.Count > 0)
                            {
                                Session["set"] = null;
                            }
                            else
                            {
                                Session["set"] = 1;
                            }
                        }
                    }
                    if (Session["set"] != null)
                    {
                        int n = dalp.updateopenclose(getid);
                        if (n == 1)
                        {
                            for (int i = 0; i < dtchild.Rows.Count; i++)
                            {
                                blsr.action = "getdepartureid";
                                blsr.PackageId = dtchild.Rows[i]["PackageId"].ToString();
                                blsr._dtStartDate = Convert.ToDateTime(dt.Rows[0]["CheckInDate"].ToString());
                                blsr._dtEndDate = Convert.ToDateTime(dt.Rows[0]["CheckOutDate"].ToString());
                                DataTable dtgetdepurtureid = dlsr.getdepartureid(blsr);
                                try
                                {
                                    int n1 = dalp.updateopenclosebydate(Convert.ToInt32(dtgetdepurtureid.Rows[0]["Id"].ToString()));
                                    if (n1 == 1)
                                    {

                                    }
                                }
                                catch (Exception ex) { }
                            }
                            Session["set"] = null;
                            this.BindGridOpenDates();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('Update Successfully')", true);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('Please try again')", true);
                            return;
                        }
                    }
                }
            }
            //if (e.CommandName == "Open")
            //{
            //    int getid = Convert.ToInt32(e.CommandArgument.ToString());
            //    blOpenDates.Id = getid;
            //    blOpenDates._Action = "GetOpenDatesbyId";
            //    DataTable dt = dlOpenDates.getOpenDatesbyId(blOpenDates);
            //    DataTable getavialabl = bindroomddl(getid, dt.Rows[0]["packageId"].ToString());
            //    DataView dv = new DataView();
            //    dv = new DataView(getavialabl, "BookedStatus='Not Available'", "BookedStatus", DataViewRowState.CurrentRows);
            //    DataTable dt2 = dv.ToTable();
            //    if (dt2 != null && dt2.Rows.Count > 0)
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('Close can only happen if there is no booking on those departures')", true);
            //        return;

            //    }
            //    else
            //    {
            //        blsr.action = "getchilid";
            //        blsr.PackageId = dt.Rows[0]["packageId"].ToString();
            //        DataTable dtchild = dlsr.getchilid(blsr);
            //        if (dtchild != null && dtchild.Rows.Count > 0)
            //        {
            //            for (int i = 0; i < dtchild.Rows.Count; i++)
            //            {
            //                blsr.action = "getdepartureid";
            //                blsr.PackageId = dt.Rows[0]["PackageId"].ToString();
            //                blsr._dtStartDate = Convert.ToDateTime(dt.Rows[0]["CheckInDate"].ToString());
            //                blsr._dtEndDate = Convert.ToDateTime(dt.Rows[0]["CheckOutDate"].ToString());
            //                DataTable dtgetdepurtureid = dlsr.getdepartureid(blsr);
            //                if (dtgetdepurtureid != null && dtgetdepurtureid.Rows.Count > 0)
            //                {
            //                    blsr.action = "GetcruiseRooms";
            //                    blsr.DepartureId = Convert.ToInt32(dtgetdepurtureid.Rows[0]["Id"].ToString());
            //                    DataTable dtgetchild = dlsr.GetCruiseRooms(blsr);
            //                    DataView dvgetchild = new DataView();
            //                    dvgetchild = new DataView(dtgetchild, "BookedStatus='Not Available'", "BookedStatus", DataViewRowState.CurrentRows);
            //                    dtgetchild = dvgetchild.ToTable();
            //                    if (dtgetchild != null && dtgetchild.Rows.Count > 0)
            //                    {
            //                        Session["set"] = null;
            //                    }
            //                    else
            //                    {
            //                        Session["set"] = 1;
            //                    }
            //                }

            //            }
            //        }

            //        if (Session["set"] != null)
            //        {
            //            int n = dalp.updateopenclose(getid);
            //            if (n == 1)
            //            {
            //                for (int i = 0; i < dt.Rows.Count; i++)
            //                {
            //                    blsr.action = "getdepartureid";
            //                    blsr.PackageId = dt.Rows[i]["PackageId"].ToString();
            //                    blsr._dtStartDate = Convert.ToDateTime(dt.Rows[0]["CheckInDate"].ToString());
            //                    blsr._dtEndDate = Convert.ToDateTime(dt.Rows[0]["CheckOutDate"].ToString());
            //                    DataTable dtgetdepurtureid = dlsr.getdepartureid(blsr);
            //                    try
            //                    {
            //                        int n1 = dalp.updateopenclosebydate(Convert.ToInt32(dtgetdepurtureid.Rows[0]["Id"].ToString()));
            //                        if (n1 == 1)
            //                        {

            //                        }
            //                    }
            //                    catch (Exception ex) { }
            //                }
            //                Session["set"] = null;
            //                this.BindGridOpenDates();
            //                ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('Update Successfully')", true);

            //            }
            //            else
            //            {
            //                ScriptManager.RegisterStartupScript(this, this.GetType(), "QuoteFull", "javascript:alert('Please try again')", true);
            //                return;
            //            }
            //        }
            //    }
            //}
        }

        catch (Exception ex)
        {
        }
    }
}