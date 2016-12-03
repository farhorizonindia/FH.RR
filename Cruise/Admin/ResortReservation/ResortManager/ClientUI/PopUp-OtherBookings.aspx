<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUp-OtherBookings.aspx.cs" Inherits="ClientUI_PopUp_OtherBookings" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />    
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />    
    <title>Other Bookings</title>    
</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="Booking Chart" />
    <div>
        <asp:Panel ID="pnlOtherBookings" runat="server" Height="50px" Width="125px">
        </asp:Panel>
    </div>
    </form>
</body>
</html>
