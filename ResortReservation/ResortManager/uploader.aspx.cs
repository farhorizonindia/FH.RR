using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;

public partial class uploader : ClientBasePage
{
    private int BookingId;
    private string uploadType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["bid"] != null)
            int.TryParse(Request.QueryString["bid"], out BookingId);

        if (Request.QueryString["upload"] != null)
            uploadType = Request.QueryString["upload"];
    }

    protected void Submit1_ServerClick1(object sender, EventArgs e)
    {
        string msg = string.Empty;

        #region Validating Input
        if ((fileUploader.PostedFile != null) && (fileUploader.PostedFile.ContentLength > 0))
        {
            string fn = System.IO.Path.GetFileName(fileUploader.PostedFile.FileName);
            if (!fn.ToUpper().StartsWith("FH_TOURISTDETAILS") || !fn.EndsWith(".csv"))
            {
                msg = "You can only select FH_TOURISTDETAILS_XXXXXXX.CSV to upload.";
                System.IO.StringWriter sw = new System.IO.StringWriter();
                sw.Write(msg);
                HtmlTextWriter tw = new HtmlTextWriter(sw);
                //Response.Write(msg);
                base.DisplayAlert(msg);
                return;
            }
        }
        #endregion
       
        try
        {
            #region Loading the Excel/CSV File
            ArrayList RecordList = new ArrayList();
            using (System.IO.StreamReader sr = new System.IO.StreamReader(fileUploader.PostedFile.InputStream))
            {
                while (sr.Peek() >= 0)
                {
                    string tempString = sr.ReadLine();
                    //recordCollection = tempString.Split(';');
                    RecordList.Add(tempString);
                }
                sr.Close();
            }
            #endregion
          
            #region Upload File To the Database

            ENums.UploadXMLType uploadXMLType = (ENums.UploadXMLType)Enum.Parse(typeof(ENums.UploadXMLType), uploadType);
            UploadServices uploadServices = new UploadServices();
            //Response.Write("abc");
            bool uploaded = uploadServices.HandleUploadedFile(BookingId, uploadXMLType, RecordList);
           
            #endregion

            #region Validating Response
            if (uploaded)
            {
              
                msg = "File uploaded and processed successfully.";
                base.DisplayAlert(msg);
                Response.Redirect("~\\ClientUI\\ViewTourists.aspx?bid=" + BookingId.ToString());
            }
            else
            {
                msg = "File is not processed successfully. Please try again.";
                base.DisplayAlert(msg);
            }
            msg = "The file has been uploaded.";
            base.DisplayAlert(msg);
            #endregion
        }
        catch (Exception ex)
        {

            Response.Write(ex.Message.ToString());
            msg = "File is not uploaded successfully, please try again, or contact your system administrator.";
            base.DisplayAlert(msg);
            GF.LogError("uploader.Submit", ex.Message);
            System.IO.StringWriter sw = new System.IO.StringWriter();
            sw.Write(msg);
            HtmlTextWriter tw = new HtmlTextWriter(sw);

            tw.Write(msg + " " + ex.Message);
            tw.Write(ex.StackTrace);

            //Note: Exception.Message returns detailed message that describes the current exception. 
            //For security reasons, we do not recommend you return Exception.Message to end users in 
            //production environments. It would be better just to put a generic error message. 
        }
    }
}
