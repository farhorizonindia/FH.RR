using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
public partial class Cruise_booking_ReviewBookingDetails : System.Web.UI.Page
{
    public DataTable dtGetBookedRooms;
    public int LoopCounter = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            if (Session["Usercode"] != null)
            {
                this.LoadBookedRoomDetails();
            }
            else
            {
                Response.Redirect("agentLogin.aspx");
            }
        }
    }
    #region UDF
    private void LoadBookedRoomDetails()
    {

        dtGetBookedRooms = Session["BookedRooms"] as DataTable;
        DataTable dtgroupedData = new DataTable();
        dtgroupedData.Columns.Add("categoryName");
        dtgroupedData.Columns.Add("Pax");
        dtgroupedData.Columns.Add("Price");
        DataTable dtUniqueCategories = dtGetBookedRooms.DefaultView.ToTable(true, "categoryName");

        #region Adding distict Room categories
        foreach (DataRow dr1 in dtUniqueCategories.Rows)
        {
            string categoryName = dr1["categoryName"].ToString();
            DataRow dr2 = dtgroupedData.NewRow();
            dr2["categoryName"] = categoryName;
            dtgroupedData.Rows.Add(dr2);
        }
        #endregion

        #region calculating values
        foreach (DataRow dr1 in dtgroupedData.Rows)
        {
            string category = dr1["categoryName"].ToString();
            DataView dv;
            dv = new DataView(dtGetBookedRooms, "categoryName='" + category + "'", "categoryName", DataViewRowState.CurrentRows);
            DataTable dtFiltered = dv.ToTable();
            int packs = 0;
            decimal price = 0;
            foreach (DataRow dr3 in dtFiltered.Rows)
            {
                packs = packs + Convert.ToInt32(dr3["Pax"].ToString());
                price = price + Convert.ToDecimal(dr3["Price"].ToString());
            }
            dr1["Pax"] = packs.ToString();


            dr1["Price"] = price.ToString();
        }
        dtGetBookedRooms = dtgroupedData;
        #endregion
    }
    #endregion

    protected void btnSbmt_Click(object sender, EventArgs e)
    {

    }
    protected void btncontinue_Click(object sender, EventArgs e)
    {

    }
}