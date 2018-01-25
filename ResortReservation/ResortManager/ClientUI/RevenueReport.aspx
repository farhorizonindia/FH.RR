<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RevenueReport.aspx.cs" Inherits="ClientUI_RevenueReport" %>

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
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Revenue Report" />
        <div>
            <table style="margin-left: 19%; padding-top: 5%;" id="tbl123" runat="server" visible="false">
                <tr>
                    <td>From Date:</td>
                    <td>
                        <asp:TextBox CssClass="input" ID="txtStartDate" runat="server"></asp:TextBox>
                        <input type="button" class="datebutton" id="btnStartDate" name="btnStartDate" onfocus="return setupCalendar('txtStartDate', 'btnStartDate')"
                            onclick="return setupCalendar('txtStartDate', 'btnStartDate')" value="..." /></td>
                    <td>To Date:</td>
                    <td>
                        <asp:TextBox CssClass="input" ID="txtEndDate" runat="server"></asp:TextBox>
                        <input type="button" class="datebutton" id="btnEndDate" name="btnEndDate" onclick="return setupCalendar('txtEndDate', 'btnEndDate')"
                            onfocus="return setupCalendar('txtEndDate','btnEndDate')" value="..." /></td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" Text=" "></asp:Label></td>
                </tr>
            </table>
            <br />
            <div>
                <table style="padding-left: 27%; padding-top: 1%; color: red;">
                    <tr>
                        <td style="font-size: x-large;">Total Revenue: </td>
                        <td style="font-size: x-large;">
                            <asp:Label ID="lblRevenue" runat="server" Text=" "></asp:Label></td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
