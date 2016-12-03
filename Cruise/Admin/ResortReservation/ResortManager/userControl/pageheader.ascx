<%@ Control Language="C#" AutoEventWireup="true" CodeFile="pageheader.ascx.cs" Inherits="userControl_pageheader" %>
<div id="pageheader" class="pageheader"> 
    <div class= "titleParent">
        <div id="companyname" class="companyname">Far Horizon</div>
        <div id="pagetitle" class="pagetitle">
            <asp:Label ID="lblpageTitle" cssclass="labelpagetitle" runat="server" >Page Title</asp:Label>
       </div>
        <div id="buttonlogout" class="buttonlogout">
           <asp:LinkButton ID="btnLogout" runat="server" CssClass="pageLink" OnClick="btnLogout_Click">Log Out</asp:LinkButton>
        </div>    
    </div>   
    <div class="linksParent">
        <div class="blankdiv">
            <asp:Label ID="lblUserInfo" runat="server" CssClass="lblUserInfo">User Info</asp:Label>
        </div>            
        <div class="currentreservationlink">  
            <asp:HyperLink ID="lnkViewBookings" runat="server" CssClass="applink" Text="Reservations"></asp:HyperLink>            
        </div>    
        <div class="newreservationlink">            
            <asp:HyperLink ID="lnkBooking" runat="server" CssClass="applink" Text="New Reservation"></asp:HyperLink>
        </div>    
        <div class="chartlink">            
            <asp:HyperLink ID="lnkBookingChartView" runat="server" CssClass="applink" Text="Booking Chart"></asp:HyperLink>
        </div>
        <div class="masterlink">            
            <asp:HyperLink ID="lnkMainmenu" runat="server" CssClass="applink" Text="Main Menu"></asp:HyperLink>
        </div>
    </div>    
</div>