using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;

public partial class Cruise_booking_Itinerary : System.Web.UI.Page
{
    BALPackageMaster blpackage = new BALPackageMaster();
    DALPackageMaster dlpackage = new DALPackageMaster();
    public  DataTable dtreturn;
   
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {

          

            ItineraryDetails(Request.QueryString["PackId"]);

          
        }
    }

    public void ItineraryDetails(string PckId)
    {
        try
        {
            blpackage._packageId = PckId;
            blpackage._Action = "GetItinerary";
            dtreturn = new DataTable();
            dtreturn = dlpackage.getPackageItinerary(blpackage);
            if (dtreturn != null)
            {
                

            }

        }
        catch
        {

        }
    }
}