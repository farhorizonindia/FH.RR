<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentRegister.aspx.cs" Inherits="ClientUI_PaymentRegister" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Register Report</title>
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
    </head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Payment Register Report" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <img id="img1" runat="server" src="~/Cruise/Booking/ARC_Logo.jpg.png" style="padding-left: 35%; padding-top: 0%; padding-bottom: 2%;" />
        </div>
        <div>
            <div>
                <table>
                    <tr>
                        <td>Accomodation:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlaccom" runat="server"></asp:DropDownList>
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
                        <td>Main Agent:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAgent" runat="server">
                                <asp:ListItem Text="Choose Agent" Value="0"></asp:ListItem>
                                <asp:ListItem Text="CustCruise" Value="247"></asp:ListItem>
                                <asp:ListItem Text="CustHotel" Value="248"></asp:ListItem>
                            </asp:DropDownList>

                        </td>

                    </tr>
                    <tr>
                        <td>Payment Period From </td>
                        <td>
                            <asp:TextBox ID="txtofrom" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtofrom" runat="server" />
                        </td>
                        <td>Payment Period To </td>
                        <td>
                            <asp:TextBox ID="txtoto" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" TargetControlID="txtoto" runat="server" />
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" /></td>
                        <td>
                            <asp:Button ID="Button2" runat="server" Text="Clear" OnClick="Button2_Click" /></td>
                    </tr>

                </table>
            </div>
            <div style="padding-left: 18%; padding-top: 2%;">
                <asp:DataGrid CellPadding="4" ForeColor="#333333"
                    ID="dgBookings" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnPageIndexChanged="dgBookings_PageIndexChanged" OnSelectedIndexChanged="dgBookings_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundColumn DataField="PaymentDate" DataFormatString="{0:dd MMM yyyy}" HeaderText="Payment Date">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="StartDate" DataFormatString="{0:dd MMM yyyy}" HeaderText="Start Date">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="EndDate" DataFormatString="{0:dd MMM yyyy}" HeaderText="End Date">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BookingCode" HeaderText="Booking Code">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BookingRef" HeaderText="BookingRef">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="AgentName" HeaderText="Main Agent">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="LocalAgent" HeaderText="Ref Agent">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PaymentMethod" HeaderText="Payment Method">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />

                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PaidAmt" HeaderText="Paid Amount">
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
        </div>
        <div style="width: 73%;">
            <div style="width: 50%; float: left;">

                <strong style="padding-left: 49%;">Total:
                </strong>
            </div>
            <div style="width: 50%; float: right;">
                <strong>
                    <asp:Label ID="lblTotal" runat="server" Text=" " Style="float: right;"></asp:Label>
                </strong>
            </div>
        </div>
    </form>
</body>
</html>
