<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FHReport.aspx.cs" Inherits="FHReport" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Booking List</title>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-en.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-setup.js"></script>
    <script language="javascript" type="text/javascript" src="../js/global.js"></script>
    <script language="javascript" type="text/javascript" src="../js/client/viewbookings.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/viewbookings.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/calendar-blue2.css"
        title="win2k-cold-1" />
    <style type="text/css">
        .auto-style1
        {
            width: 80px;
            height: 19px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Bookings List" />
        <div>
            <asp:ScriptManager ID="scmgrViewBookings" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="pnlViewBookings" runat="server">
                <ContentTemplate>
                    <table id="filtersection" class="filtersection">
                        <tr>
                            <td class="filtersectionCell">
                                Check-In:</td>
                            <td class="filtersectionCell">
                                <asp:TextBox CssClass="input" ID="txtStartDate" runat="server" Font-Size="8pt" Width="95px"></asp:TextBox>
                                <input type="button" class="datebutton" id="btnStartDate" name="btnStartDate" onfocus="return setupCalendar('txtStartDate','btnStartDate')"
                                    onclick="return setupCalendar('txtStartDate', 'btnStartDate')" value="..." /></td>
                            <td class="filtersectionCell">
                                Check-out:</td>
                            <td class="filtersectionCell">
                                <asp:TextBox CssClass="input" ID="txtEndDate" runat="server" Font-Size="8pt" Width="95px"></asp:TextBox>
                                <input type="button" class="datebutton" id="btnEndDate" name="btnEndDate" onclick="return setupCalendar('txtEndDate', 'btnEndDate')"
                                    onfocus="return setupCalendar('txtEndDate','btnEndDate')" value="..." /></td>
                            <td class="filtersectionCell">
                                Booking Code:</td>
                            <td class="filtersectionCell">
                                <asp:TextBox CssClass="input" ID="txtBookingCode" runat="server" Font-Size="8pt" Width="213px"></asp:TextBox></td>
                            <td class="filtersectionCell">
                                </td>
                            <td class="filtersectionCell">
                                </td>                            
                        </tr>
                        <tr>
                            <td class="filtersectionCell">
                                Accom Type:</td>
                            <td class="filtersectionCell">
                                <asp:DropDownList CssClass="select" ID="ddlAccomType" runat="server" 
                                    Width="150px">
                                </asp:DropDownList></td>
                            <td class="filtersectionCell">
                                Booking State:</td>
                            <td class="filtersectionCell">
                                <asp:DropDownList CssClass="select" ID="ddlBookingStatusTypes" runat="server" Width="125px">
                                </asp:DropDownList></td>
                            <td class="filtersectionCell">
                                Agent:</td>
                            <td class="filtersectionCell">
                                <asp:DropDownList CssClass="select" ID="ddlAgent" runat="server" Width="220px">
                                </asp:DropDownList></td>
                            <td class="filtersectionCell">
                                </td>
                            <td class="filtersectionCell">
                                <asp:Button CssClass="appbutton" ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />

                                <asp:Button CssClass="appbutton"  ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Download" />
                            </td>                           
                        </tr>
                    </table>
                   
                    <div id="gridsection">
                        <asp:DataGrid CellPadding="4" ForeColor="#333333"
                             ID="dgBookings" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnItemDataBound="dgBookings_ItemDataBound" OnPageIndexChanged="dgBookings_PageIndexChanged" PageSize="25" >
                            <Columns>
                               <asp:BoundColumn DataField="unit" HeaderText="Unit">
                                    <ItemStyle CssClass="column" />
                                    <HeaderStyle CssClass="columnHeader" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SDate" HeaderText="From" DataFormatString="{0:dd-MMM-yyyy}">
                                    <ItemStyle CssClass="column" />
                                    <HeaderStyle CssClass="columnHeader" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="EDate" HeaderText="To" DataFormatString="{0:dd-MMM-yyyy}">
                                    <ItemStyle CssClass="column" />
                                    <HeaderStyle CssClass="columnHeader" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BookingCode" HeaderText="Code">
                                    <ItemStyle CssClass="column" />
                                    <HeaderStyle CssClass="columnHeader" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BookingReference" HeaderText="Name">
                                    <ItemStyle CssClass="column" />
                                    <HeaderStyle CssClass="columnHeader" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="agentname" HeaderText="Agent" >
                                    <ItemStyle CssClass="column" />
                                    <HeaderStyle CssClass="columnHeader" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="refagentname" HeaderText="RefAgent" >
                                    <ItemStyle CssClass="column" />
                                    <HeaderStyle CssClass="columnHeader" />
                                </asp:BoundColumn>
                                   <asp:BoundColumn DataField="noofnights" HeaderText="Nights" >
                                    <ItemStyle CssClass="column" />
                                    <HeaderStyle CssClass="columnHeader" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SGL" HeaderText="SGL" >
                                    <ItemStyle CssClass="column" />
                                    <HeaderStyle CssClass="columnHeader" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="TWN" HeaderText="TWN" >
                                    <ItemStyle CssClass="column" />
                                    <HeaderStyle CssClass="columnHeader" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="DBL" HeaderText="DBL" >
                                    <ItemStyle CssClass="column" />
                                    <HeaderStyle CssClass="columnHeader" />
                                </asp:BoundColumn>

                                  <asp:BoundColumn DataField="TRP" HeaderText="TRP" >
                                    <ItemStyle CssClass="column" />
                                    <HeaderStyle CssClass="columnHeader" />
                                </asp:BoundColumn>


                                  <asp:BoundColumn DataField="Total" HeaderText="Total" >
                                    <ItemStyle CssClass="column" />
                                    <HeaderStyle CssClass="columnHeader" />
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="PAX" HeaderText="PAX" >
                                    <ItemStyle CssClass="column" />
                                    <HeaderStyle CssClass="columnHeader" />
                                </asp:BoundColumn>
                                 <asp:BoundColumn DataField="BookingStatus" HeaderText="Booking Status" >
                                    <ItemStyle CssClass="column" />
                                    <HeaderStyle CssClass="columnHeader" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="ProposedBooking" HeaderText="ProposedBooking" Visible="false">
                                    <ItemStyle CssClass="column" />
                                    <HeaderStyle CssClass="columnHeader" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CharteredBooking" HeaderText="CharteredBooking" Visible="false">
                                    <ItemStyle CssClass="column" />
                                    <HeaderStyle CssClass="columnHeader" />
                                </asp:BoundColumn>
                                
                                <asp:BoundColumn DataField="BookingAmt" HeaderText="Booking Amount" >
                                    <ItemStyle CssClass="column" />
                                    <HeaderStyle CssClass="columnHeader" />
                                </asp:BoundColumn>

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
                    <table id="LegendSection">
                        <tr>
                            <td style="background-color: blue; color: White; padding-left: 4px;" class="auto-style1">
                                Proposed</td>
                            <td style="background-color: Aqua; padding-left: 4px;" class="auto-style1">
                                Booked</td>
                            <td style="background-color: orange; padding-left: 4px;" class="auto-style1">
                                Wait Listed</td>
                            <td style="background-color: Lime; padding-left: 4px;" class="auto-style1">
                                Confirmed</td>
                            <td style="background-color: Red; padding-left: 4px;" class="auto-style1">
                                Cancelled</td>
                             <td style="background-color: teal; padding-left: 4px;" class="auto-style1">
                                Chartered</td>
                        </tr>
                    </table>
                   



                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnExport" />

                </Triggers>
            </asp:UpdatePanel>

        </div>
        
    </form>
</body>
</html>

