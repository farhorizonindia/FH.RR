<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sendtoairpay.aspx.cs" Inherits="posttoairpay" %>

  
    
    <%    
        //BookingPayId=Ai0427124119166&EmailId=Ashu@farhorizonindia.com&PhoneNumber=&FirstName=Geographic&LastName=Expeditions%20(USA)&PaidAmt=20000
        string username = ConfigurationManager.AppSettings.Get("username").ToString();
        string password = ConfigurationManager.AppSettings.Get("password").ToString();
        string secretKey = ConfigurationManager.AppSettings.Get("secret").ToString();
        string allParamValue = null;

        string sEmail = Request.QueryString["EmailId"].Trim();
        string sPhone = Request.QueryString["PhoneNumber"].Trim();
        string sFName = Request.QueryString["FirstName"].Trim();
        string sLName = Request.QueryString["LastName"].Trim();
        string sAddress = Request.QueryString["BillingAddress"].Trim();
        string sCity = "Lucknow";
        string sState = "UP";
        string sCountry = "INDIA";
        string sPincode = "226005" ;
        string sAmount = Request.QueryString["PaidAmt"].Trim();
        string sOrderId = Request.QueryString["BookingPayId"].Trim();

        // server side validation
        validatepost(sEmail, sPhone, sFName, sLName, sAddress, sCity, sState, sCountry, sPincode, sAmount, sOrderId);

        allParamValue = sEmail + sFName + sLName + sAddress + sCity + sState + sCountry + sAmount + sOrderId;
        DateTime now1 = DateTime.Today; // As DateTime
        string now = now1.ToString("yyyy-MM-dd"); // As String
        string allParamValue1 = allParamValue + now;
        string sTemp = secretKey + "@" + username + ":|:" + password;
        string str256Key = EncryptSHA256Managed(sTemp);
        string allParamValue12 = allParamValue1 + str256Key;
        string checksum1 = MD5Hash(allParamValue12);
        checksum.Text = checksum1;
        privatekey.Text = str256Key;
            
    %>
       
    
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
        <script type="text/javascript">
            function submitForm() {
                var form = document.forms[0];
                form.submit();
            }
</script>
   
   </head>
   
   <body onload="javascript:submitForm()">
<center>
<table width="500px;">
	<tr>
		<td align="center" valign="middle">Do Not Refresh or Press Back <br/> Redirecting to Airpay</td>
	</tr>
	<tr>
		<td align="center" valign="middle">
			<form action="https://payments.airpay.co.in/pay/index.php" method="post" runat="server">
           
            <input type="hidden" name="currency" value="356">
		    <input type="hidden" name="isocurrency" value="INR">

		    <input type="hidden" name="orderid" value="<%=Request.QueryString["BookingPayId"]%>"/>
			<input type="hidden" name="buyerEmail" value="<%=Request.QueryString["EmailId"]%>"/>
			<input type="hidden" name="buyerPhone" value="<%=Request.QueryString["PhoneNumber"]%>"/>
			<input type="hidden" name="buyerFirstName" value="<%=Request.QueryString["FirstName"]%>"/>
			<input type="hidden" name="buyerLastName" value="<%=Request.QueryString["LastName"]%>"/>
			<input type="hidden" name="buyerAddress" value="<%=Request.QueryString["BillingAddress"]%>"/>
			<input type="hidden" name="buyerCity" value="Lucknow"/>
			<input type="hidden" name="buyerState" value="UP"/>
			<input type="hidden" name="buyerCountry" value="INDIA"/>
			<input type="hidden" name="buyerPinCode" value="226005"/>
			<input type="hidden" name="amount" value="<%=Request.QueryString["PaidAmt"]%>"/>
            <input type="hidden" name="chmod" value="">

            <asp:TextBox ID="checksum" runat="server" style="display:none;" ></asp:TextBox>
            <asp:TextBox ID="privatekey" runat="server" style="display:none;" ></asp:TextBox>
            <input type="hidden" name="mercid" value="<%= ConfigurationManager.AppSettings["mercid"].ToString() %>"/>
			            
			</form>
		</td>

	</tr>

</table>

</center>	
</body>
</html>