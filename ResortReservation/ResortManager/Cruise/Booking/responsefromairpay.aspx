<%@ Page Language="C#" AutoEventWireup="true" CodeFile="responsefromairpay.aspx.cs" Inherits="response" %>




<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    protected void Button1_Click(object sender, EventArgs e)
    {

    }
</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
<meta http-equiv="Cache-Control" content="post-check=0, pre-check=0', false" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="Sat, 26 Jul 1997 05:00:00 GMT" />
<meta http-equiv="Last-Modified" content='" + now1( D, d M Y H:i:s ) + "GMT'/>
<title>Airpay</title>

<script type="text/javascript" src="resources/js/jquery.js"></script>



</head>
<body>


    <form id="form1" runat="server">


<% 

    // This is landing page where you will receive response from airpay. 
    // The name of the page should be as per you have configured in airpay system
    // All columns are mandatory    


    // Generating Secure Hash
    // $mercid = 	Merchant Id, $username = username
    // You will find above two keys on the settings page, which we have defined here in config.php
              
    string username = ConfigurationManager.AppSettings.Get("username").ToString();
    string password = ConfigurationManager.AppSettings.Get("password").ToString();
    string secretKey = ConfigurationManager.AppSettings.Get("secret").ToString();
    string MID = ConfigurationManager.AppSettings.Get("mercid").ToString();

    string error = "";
    string TRANSACTIONSTATUS = Request.Params.Get("TRANSACTIONSTATUS").Trim();
    string APTRANSACTIONID = Request.Params.Get("APTRANSACTIONID").Trim();
    string MESSAGE = Request.Params.Get("MESSAGE").Trim();
    string TRANSACTIONID = Request.Params.Get("TRANSACTIONID").Trim();
    string AMOUNT = Request.Params.Get("AMOUNT").Trim();
    string ap_SecureHash = Request.Params.Get("ap_SecureHash").Trim();

    if (TRANSACTIONSTATUS == "" || APTRANSACTIONID == "" || TRANSACTIONID == "" || AMOUNT == "" || ap_SecureHash == "")
    {
    if (TRANSACTIONID == "") { error = "TRANSACTIONID"; }
    if (APTRANSACTIONID == "") { error = "APTRANSACTIONID"; }
    if (AMOUNT == "") { error = "AMOUNT"; }
    if (TRANSACTIONSTATUS == "") { error = "TRANSACTIONSTATUS"; }
    if (ap_SecureHash == "") { error = "ap_SecureHash"; }
       }

    //comparing Secure Hash with Hash sent by Airpay
    string sTemp = TRANSACTIONID + ":" + APTRANSACTIONID + ":" + AMOUNT + ":" + TRANSACTIONSTATUS + ":" + MESSAGE + ":" + MID + ":" + username;
    string strCRC = CRCCode(sTemp, ap_SecureHash);

    if(error == "")
    {
    if (TRANSACTIONSTATUS == "200")
    {
        Literal1.Text = "<table width='100%'><tr width='100%'><td align='left' width='50%'>Transaction Id</td><td align='left' width='50%' style='color:black;'>" + TRANSACTIONID + "</td></tr><tr width='100%'><td align='left' width='50%'>Airpay Transaction Id</td><td align='left' width='50%' style='color:black;'>" + APTRANSACTIONID + "</td></tr><tr width='100%'><td align='left' width='50%'>Amount</td><td align='left' width='50%' style='color:black;'>" + AMOUNT + "</td></tr><tr width='100%'><td align='left' width='50%'>Transaction Status Code</td><td align='left' width='50%' style='color:black;'>" + TRANSACTIONSTATUS + "</td></tr><tr width='100%'><td align='left' width='50%'>Message</td><td align='left' width='50%' style='color:black;'>" + MESSAGE + "</td></tr><tr width='100%'><td align='left' width='50%'>Status</td><td align='left' width='50%' style='color:green;'>Success</td></tr></table>";
    }else{
        Literal1.Text = "<table width='100%'><tr width='100%'><td align='left' width='50%'>Transaction Id</td><td align='left' width='50%' style='color:black;'>" + TRANSACTIONID + "</td></tr><tr width='100%'><td align='left' width='50%'>Airpay Transaction Id</td><td align='left' width='50%' style='color:black;'>" + APTRANSACTIONID + "</td></tr><tr width='100%'><td align='left' width='50%'>Amount</td><td align='left' width='50%' style='color:black;'>" + AMOUNT + "</td></tr><tr width='100%'><td align='left' width='50%'>Transaction Status Code</td><td align='left' width='50%' style='color:black;'>" + TRANSACTIONSTATUS + "</td></tr><tr width='100%'><td align='left' width='50%'>Message</td><td align='left' width='50%' style='color:black;'>" + MESSAGE + "</td></tr><tr width='100%'><td align='left' width='50%'>Status</td><td align='left' width='50%' style='color:green;'>Failed</td></tr></table>";
    }
    }
    else
    {
      Literal1.Text = "<table width='100%'><tr><td align='center'>Variable(s) " + error + " is/are empty.</td></tr></table>";
     }

	%>
    <center>
    

        <div style="width:450px;margin:0px auto;">
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
    </center>
    
    </form>
    
</body>
</html>
