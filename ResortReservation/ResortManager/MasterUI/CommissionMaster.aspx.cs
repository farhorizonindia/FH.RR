using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.MasterServices;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using System.Text;
using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine;

public partial class MasterUI_CommissionMaster : MasterBasePage
{
    BALHotelBooking blht = new BALHotelBooking();
    DALHotelBooking dlht = new DALHotelBooking();
    DALOpenDates opndal = new DALOpenDates();
    BALBooking blsr = new BALBooking();
    DALBooking dlsr = new DALBooking();
    BALPackageMaster bpm = new BALPackageMaster();
    DALPackageMaster dpm = new DALPackageMaster();
    DataTable Returndt;
    DataView dv;
    DALAgentPayment dlapayment = new DALAgentPayment();
    double total = 0;

    AccomTypeDTO[] oAccomTypeData;
    int iBookingId = 0;
    Table tblMaster = null;

    int totalEventHandlersAdded = 0;
    int eventCounter = 0;


    DatabaseManager oDB;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillAccomodationTypes();

        }
    }
    private void SetAccomodationTypeDetails()
    {
        //Filling the Object which contains the Accomodation type and also all the respective accomodations
        //This is saved in the session and then the Drop downs are filled.
        AccomTypeDTO[] oAccomTypeData;
        AccomodationTypeMaster objATM;
        objATM = new AccomodationTypeMaster();
        oAccomTypeData = objATM.GetAccomTypeWithAccomDetails(0);
        SessionServices.Booking_AccomodationData = oAccomTypeData;
        objATM = null;
    }
    private void FillAccomodationTypes()
    {

        ddlAccomtype.Items.Clear();
        SortedList slAccomTypes = new SortedList();
        slAccomTypes.Add("0", "Choose");

        AccomTypeDTO[] oAccomTypeData = SessionServices.Booking_AccomodationData;
        if (oAccomTypeData == null)
        {
            SetAccomodationTypeDetails();
            oAccomTypeData = SessionServices.Booking_AccomodationData;
        }

        if (oAccomTypeData != null)
        {
            for (int i = 0; i < oAccomTypeData.Length; i++)
            {
                slAccomTypes.Add(Convert.ToString(oAccomTypeData[i].AccomodationTypeId), Convert.ToString(oAccomTypeData[i].AccomodationType));
            }
        }
        ddlAccomtype.DataSource = slAccomTypes;
        ddlAccomtype.DataTextField = "value";
        ddlAccomtype.DataValueField = "key";
        ddlAccomtype.DataBind();
    }

    private void FillAccomodations(int AccomodationTypeId)
    {
        oAccomTypeData = SessionServices.Booking_AccomodationData;
        ddlAccomname.Items.Clear();
        SortedList slAccomData = new SortedList();
        slAccomData.Add("0", "Choose");
        if (oAccomTypeData != null)
        {
            for (int i = 0; i < oAccomTypeData.Length; i++)
            {
                if (oAccomTypeData[i].AccomodationTypeId.CompareTo(AccomodationTypeId) == 0)
                {
                    if (oAccomTypeData[i].Accomodations != null)
                    {
                        for (int j = 0; j < oAccomTypeData[i].Accomodations.Length; j++)
                        {
                            if (!slAccomData.ContainsKey(Convert.ToString(oAccomTypeData[i].Accomodations[j].AccomodationId)))
                            {
                                slAccomData.Add(Convert.ToString(oAccomTypeData[i].Accomodations[j].AccomodationId), Convert.ToString(oAccomTypeData[i].Accomodations[j].AccomodationName));
                            }
                        }
                    }
                }
            }
            ddlAccomname.DataSource = slAccomData;
            ddlAccomname.DataTextField = "value";
            ddlAccomname.DataValueField = "key";
            ddlAccomname.DataBind();
        }
    }

    protected void ddlAccomtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillAccomodations(Convert.ToInt32(ddlAccomtype.SelectedValue.ToString()));
    }
    private void selectbyaccomname(int accotype, int accomname)
    {
        DataTable dt = dlapayment.selectbyaccom(accotype, accomname);
        if (dt != null && dt.Rows.Count > 0)
        {
            txtCommision.Text = dt.Rows[0]["Commision"].ToString();
        }
        else
        {
            txtCommision.Text = "";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlAccomtype.SelectedIndex > 0)
        {
            if (ddlAccomname.SelectedIndex > 0)
            {
                if (txtCommision.Text != "")
                {
                    try
                    {
                        decimal amount = Convert.ToDecimal(txtCommision.Text);
                        int n = dlapayment.saveCommission(Convert.ToInt32(ddlAccomtype.SelectedValue.ToString()), Convert.ToInt32(ddlAccomname.SelectedValue.ToString()), Convert.ToDecimal(txtCommision.Text));
                        if (n > 0)
                        {
                            selectbyaccomname(Convert.ToInt32(ddlAccomtype.SelectedValue.ToString()), Convert.ToInt32(ddlAccomname.SelectedValue.ToString()));
                            lblMsg.Text = "Save Successfully";
                            lblMsg.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblMsg.Text = "Please try again";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    catch
                    {
                        lblMsg.Text = "Please enter valid  commission";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMsg.Text = "Commission can not be blank";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblMsg.Text = "Please select accomdation name";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lblMsg.Text = "Please select accomdation type";
            lblMsg.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void ddlAccomname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccomtype.SelectedIndex > 0)
        {
            if (ddlAccomname.SelectedIndex > 0)
            {
                selectbyaccomname(Convert.ToInt32(ddlAccomtype.SelectedValue.ToString()), Convert.ToInt32(ddlAccomname.SelectedValue.ToString()));
            }
        }
    }
}