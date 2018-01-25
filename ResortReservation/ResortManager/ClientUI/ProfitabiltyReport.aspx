<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProfitabiltyReport.aspx.cs" Inherits="ClientUI_ProfitabiltyReport" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <title>Profitability Report </title>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Profitability Report" />
        <div style="padding-left: 40%;">
            <table>
                <tr>
                    <td>Revenue Amount:</td>
                    <td>
                        <asp:Label ID="lblRevenue" runat="server" Text=" "></asp:Label></td>
                </tr>
                <tr>
                    <td>Commission Amount:</td>
                    <td>
                        <asp:Label ID="lblCommissionamount" runat="server" Text=" "></asp:Label></td>
                </tr>
                <tr>
                    <td>Profitability Amount:</td>
                    <td>
                        <asp:Label ID="lblProfitability" runat="server" Text=" "></asp:Label></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
