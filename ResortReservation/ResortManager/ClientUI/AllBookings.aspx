<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllBookings.aspx.cs" Inherits="ClientUI_AllBookings" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>All Bookings</title>
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />    
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />    
</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="All Bookings" />
        <div>
            <table style="font-family: Verdana; font-size: small; width: 700px; height: 1px;" id="tblBookingDet">                
                <tr>
                   <td colspan="2" style="width: 300px">
                       <asp:DataGrid ID="dgBookings" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="575px">
                           <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                           <EditItemStyle BackColor="#2461BF" />
                           <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                           <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                           <AlternatingItemStyle BackColor="White" />
                           <ItemStyle BackColor="#EFF3FB" />
                           <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                           <Columns>
                               <asp:HyperLinkColumn DataTextField="BookingCode" DataNavigateUrlField="BookingID" DataNavigateUrlFormatString="Booking.aspx?bid={0}" HeaderText = "Booking Code"></asp:HyperLinkColumn>
                               <asp:BoundColumn DataField="BookingReference" HeaderText="Booking Reference"></asp:BoundColumn>
                               <asp:BoundColumn DataField="SDate" HeaderText="Check In"></asp:BoundColumn>
                               <asp:BoundColumn DataField="EDate" HeaderText="Check Out"></asp:BoundColumn>
                           </Columns>
                       </asp:DataGrid></td> 
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
