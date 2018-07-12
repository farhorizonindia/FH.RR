<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Packagewiseavailability.aspx.cs" Inherits="ClientUI_Packagewiseavailability" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Booking Summery Report</title>
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
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="MV Mahabaahu Departure Availability Status" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <img id="imgid" runat="server" src="~/Cruise/Booking/ARC_Logo.jpg.png" style="padding-left: 35%; padding-bottom: 2%;" />
        </div>
        <div>
            <div style="padding-left: 10%;">
                <table>
                    <tr>
                        <td>Package:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCm" runat="server">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Master Package" Value="Master Package"></asp:ListItem>
                                <asp:ListItem Text="Child Package" Value="Child Package"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>Pacakge Name:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPackagename" runat="server"></asp:DropDownList>
                        </td>

                        <td>From:
                        </td>
                        <td>
                            <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFrom" runat="server" />

                        </td>
                        <td>To:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTo" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Departure Status </td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" runat="server">
                                <asp:ListItem Text="All" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Open" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Close" Value="1"></asp:ListItem>
                            </asp:DropDownList>

                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server" Text="Clear" OnClick="Button2_Click" />
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
            <div id="div1" runat="server" style="padding-left: 23%; padding-top: 2%;">
                <asp:DataGrid CellPadding="4" ForeColor="#333333"
                    ID="dgBookings" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnPageIndexChanged="dgBookings_PageIndexChanged">
                    <Columns>
                        <asp:TemplateColumn HeaderText="Sno">
                            <ItemTemplate>
                                <asp:Label ID="lblserial" Text='<%# Container.ItemIndex+1 %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="NamePack" HeaderText="Package Name">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CheckInDate" DataFormatString="{0:dd MMM yyyy}" HeaderText="Boarding Date">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CheckOutDate" DataFormatString="{0:dd MMM yyyy}" HeaderText="DeBoarding Date">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Suite" HeaderText="Suite">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Swb" HeaderText="Swb">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />

                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Swob" HeaderText="Swob">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>

                                
                        <asp:BoundColumn DataField="Total" HeaderText="Total">
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

            <div id="div2" runat="server" visible="false" style="padding-left: 23%; padding-top: 2%;">
                <asp:DataGrid CellPadding="4" ForeColor="#333333"
                    ID="dgBookings1" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnPageIndexChanged="dgBookings1_PageIndexChanged">
                    <Columns>
                        <asp:TemplateColumn HeaderText="Sno">
                            <ItemTemplate>
                                <asp:Label ID="lblserial" Text='<%# Container.ItemIndex+1 %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="NamePack" HeaderText="Package Name">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CheckInDate" DataFormatString="{0:dd MMM yyyy}" HeaderText="Boarding Date">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CheckOutDate" DataFormatString="{0:dd MMM yyyy}" HeaderText="DeBoarding Date">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Suite" HeaderText="Suite">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Swb" HeaderText="Swb">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />

                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Swob" HeaderText="Swob">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>

                                  <asp:BoundColumn DataField="Lcwb" HeaderText="Lcwb">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Total" HeaderText="Total">
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
            <div style="float: left; padding-left: 23%;">
                <table>
                    <tr>
                        <strong>
                            <b>
                                <td>Notes:
                                </td>
                            </b>
                        </strong>
                    </tr>
                    <tr>
                        <td>
                            Upstream: Guwahati to Neamati, Downstream: Neamati to Guwahati
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Suite: Suite with Balcony, SWB: Superior with Balcony, SWoB: Superior without Balcony
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
