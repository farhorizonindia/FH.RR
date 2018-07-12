<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookingDetailreportCruise.aspx.cs" Inherits="ClientUI_BookingDetailreportCruise" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Booking Details Report</title>
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
        .auto-style1 {
            width: 79px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Booking Details Report(Cruise)" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <div>
                <table>
                    <tr>
                        <td>Accomdation:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAccom" runat="server"></asp:DropDownList>
                        </td>
                        <td>Operation Period From:
                        </td>
                        <td>
                            <asp:TextBox ID="txtfrom" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtfrom" runat="server" />
                        </td>
                        <td>Operation Period To:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTo" runat="server" />
                        </td>
                        <td>Main agent:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFagent" runat="server"></asp:DropDownList>
                        </td>
                        <%--<td>Lagent:
                        </td>--%>
                        <td>
                            <asp:DropDownList ID="ddlLocalAgent" runat="server" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Invoice Period From </td>
                        <td>
                            <asp:TextBox ID="txtinfrom" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtinfrom" runat="server" />
                        </td>
                        <td>Invoice Period To </td>
                        <td>
                            <asp:TextBox ID="txtinto" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" TargetControlID="txtinto" runat="server" />
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server" Text="Clear All" OnClick="Button2_Click" />
                        </td>
                    </tr>

                </table>
            </div>
            <div style="float: right; padding-right: 17%;">
                <table>
                    <tr>
                        <td>as on:
                        </td>
                        <td>
                            <asp:Label ID="lblCurrentDateTime" runat="server"></asp:Label>
                            <asp:Label ID="lblTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:DataGrid CellPadding="4" ForeColor="#333333"
                    ID="dgBookings" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnPageIndexChanged="dgBookings_PageIndexChanged">
                    <Columns>
                        <asp:BoundColumn DataField="AccomName" HeaderText="Accom Name">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="StartDate"  DataFormatString="{0:dd MMM yyyy}"  HeaderText="Start Date">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="enddate" DataFormatString="{0:dd MMM yyyy}" HeaderText="End Date">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NoOFNights" HeaderText="Nights">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BookingCode" HeaderText="Booking Code">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />

                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BookingRef" HeaderText="Booking Ref">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PackageName" HeaderText="Package Name">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Fagent" HeaderText="Main Agent">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Lagent" HeaderText="Local Agent">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RoomCategory" HeaderText="Room Category">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                      <%--  <asp:BoundColumn DataField="PaxStaying" HeaderText="Bed Config" Visible="false">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RoomNo" HeaderText="Cabin NO" Visible="false">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>--%>
                        <asp:BoundColumn DataField="TotalRoom" HeaderText="Rooms">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NoOfPersons" HeaderText="Pax">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PricePerPerson" DataFormatString="{0:0}" HeaderText="Rate Per Person">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Invoiceno" HeaderText="Invoice no">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="InvoiceDate" DataFormatString="{0:dd MMM yyyy}" HeaderText="Invoice Date">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Gross" DataFormatString="{0:0}" HeaderText="Gross">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DiscountPercent" DataFormatString="{0:0}" HeaderText="Discount%">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DiscountAmount" DataFormatString="{0:0}" HeaderText="Discount Amount">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="taxableamount" DataFormatString="{0:0}" HeaderText="Taxable Amount">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Taxpercentage" DataFormatString="{0:0}" HeaderText="Tax %">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="taxamount" DataFormatString="{0:0}" HeaderText="Tax Amount">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Invoiceamount" DataFormatString="{0:0}" HeaderText="Invoice Amount">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <%--  <asp:BoundColumn DataField="Paidamount" HeaderText="Paid Aamount">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>--%>
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
            <div style="width: 100%;">
                <div style="width: 50%; float: left;">
                    <strong>Total:
                    </strong>
                </div>
                <div style="width: 50%; float: right;">
                    <strong>
                        <asp:Label ID="lblTotal" runat="server" Text=" " Style="float: right;"></asp:Label>
                    </strong>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
