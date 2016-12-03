<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TouristCountReport.aspx.cs" Inherits="ClientUI_TouristCountReport" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Tourist Count</title>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-en.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-setup.js"></script>
    <script language="javascript" type="text/javascript" src="../js/global.js"></script>
    <script language="javascript" type="text/javascript" src="../js/client/viewbookings.js"></script>
    
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />    
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />  
    <link rel="stylesheet" type="text/css" media="all" href="../css/viewbookings.css" />  
    <link rel="stylesheet" type="text/css" media="all" href="../css/calendar-blue2.css" title="win2k-cold-1" />    
</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="Bookings List" />
    <div>
    <asp:ScriptManager ID="scmgrTouristCount" runat ="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="pnlTouristCount" runat="server">
    <ContentTemplate>        
        <table id="filtersection" class="filtersection" style="width: 948px">
            <tr>
                <td align="left" style="font-size: 8pt; height: 19px; padding-left:3px;">
                    Check-In:</td>
                <td align="left" style="width: 129px; height: 19px">
                    <asp:TextBox cssclass="input"  ID="txtStartDate" runat="server" Font-Size="8pt" Width="95px"></asp:TextBox>
                    <input type="button" class="datebutton" id="btnStartDate" name ="btnStartDate" onfocus="return setupCalendar('txtStartDate','btnStartDate')" onclick="return setupCalendar('txtStartDate','btnStartDate')" value="..."/></td>
                <td align="left" style="font-size: 8pt; height: 19px">
                    Check-out:</td>
                <td align="left" style="font-size: 8pt; width: 132px; height: 19px">
                        <asp:TextBox cssclass="input"  ID="txtEndDate" runat="server" Font-Size="8pt" Width="95px"></asp:TextBox>
                        <input type="button" class="datebutton" id="btnEndDate" name="btnEndDate" onclick="return setupCalendar('txtEndDate','btnEndDate')" onfocus="return setupCalendar('txtEndDate','btnEndDate')" value="..." /></td>
                <td align="left" style="font-size: 8pt; height: 19px">
                    Accom Type:</td>
                <td align="left" style="font-size: 8pt; height: 19px">
                        <asp:DropDownList cssclass="select" ID="ddlAccomType" runat="server" OnSelectedIndexChanged="ddlAccomType_SelectedIndexChanged" Width="220px">
                        </asp:DropDownList></td>
                <td align="left" style="font-size: 8pt; height: 19px">
                    <asp:Button cssclass="appbutton" ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" /></td>
            </tr>
        </table>
        <div id="gridsection">
            <asp:DataGrid ID="dgTouristCount" DataKeyField ="BookingId" runat="server" AutoGenerateColumns="False" Width="989px" CellPadding="4" ForeColor="#333333" GridLines="None" OnItemCommand="dgBookings_ItemCommand" AllowPaging="True" OnPageIndexChanged="dgBookings_PageIndexChanged" PageSize="25">
            <Columns>            
                <asp:BoundColumn DataField="AccomodationTypeId" HeaderText="Accomodation Type Id" Visible="false">
                    <ItemStyle CssClass="column" />
                    <HeaderStyle CssClass="columnHeader" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AccomodationType" HeaderText="Accomodation Type">
                    <ItemStyle CssClass="column" />
                    <HeaderStyle CssClass="columnHeader" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AccomodationId" HeaderText="Accomodation Id" Visible="false">
                    <ItemStyle CssClass="column" />
                    <HeaderStyle CssClass="columnHeader" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AccomodationName" HeaderText="Accomodation Name">                    
                    <ItemStyle CssClass="column" />
                    <HeaderStyle CssClass="columnHeader" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="BookingId" HeaderText="BookingId">
                    <ItemStyle CssClass="column" />
                    <HeaderStyle CssClass="columnHeader" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="BookingReference" HeaderText="Booking Reference">
                    <ItemStyle CssClass="column" />
                    <HeaderStyle CssClass="columnHeader" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="BookingStartDate" HeaderText="Start Date">
                    <ItemStyle CssClass="column" />
                    <HeaderStyle CssClass="columnHeader" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="BookingEndDate" HeaderText="End Date">
                    <ItemStyle CssClass="column" />
                    <HeaderStyle CssClass="columnHeader" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TotalTourist" HeaderText="Total Tourist">
                    <ItemStyle CssClass="column" />
                    <HeaderStyle CssClass="columnHeader" />
                </asp:BoundColumn>
                <asp:ButtonColumn CommandName="viewBooking" HeaderText="[...]" Text="View Booking">
                    <ItemStyle CssClass="column" />
                    <HeaderStyle CssClass="columnHeader" />
                </asp:ButtonColumn>                
                <asp:ButtonColumn CommandName="viewtourist" HeaderText="[...]" Text="View Tourist">
                    <ItemStyle CssClass="column" />
                    <HeaderStyle CssClass="columnHeader" />
                </asp:ButtonColumn>                
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditItemStyle BackColor="#2461BF" />
            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#EFF3FB" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        </asp:DataGrid>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>    
    </div>
        
    </form>
</body>
</html>

