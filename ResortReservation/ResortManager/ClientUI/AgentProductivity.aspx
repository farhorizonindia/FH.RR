<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgentProductivity.aspx.cs" Inherits="ClientUI_AgentProductivity" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Revenue Report</title>
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
            width: 80px;
            height: 19px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Agent Productivity Report" />
        <div>
            <div id="gridsection">
                <asp:DataGrid CellPadding="4" ForeColor="#333333"
                    ID="dgBookings" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnPageIndexChanged="dgBookings_PageIndexChanged" OnSelectedIndexChanged="dgBookings_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundColumn DataField="AgentId" HeaderText="AgentId">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="AgentName" HeaderText="AgentName">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Amount" HeaderText="Total Paid Amount">
                            <ItemStyle CssClass="column" />
                            <HeaderStyle CssClass="columnHeader" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="total Refrence" HeaderText="Total Refrence">
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
    </form>
</body>
</html>
