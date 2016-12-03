<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookingChartMonthView.aspx.cs" Inherits="ClientUI_BookingChartMonthView" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Booking Month View</title>
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />    
</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="Booking" />
     <table style="font-size: small; font-family: Verdana">            
            <tr>
                <td align="center" style="width: 615px; height: 15px">
                    <asp:LinkButton ID="btnFullView" runat="server" Font-Size="9pt" TabIndex="6" OnClick="btnFullView_Click">Full View</asp:LinkButton></td>
                <td style="width: 592px; height: 15px" align="center">
                    <a href="../ClientUI/BookingChartView.aspx">Booking Chart</a>
                    </td>
            </tr>
            </table>
        <table style="width: 955px">
            <tr>
                <td style="width: 325px">
                    Accomodation Type:</td>
                <td style="width: 143px">
                    <asp:DropDownList cssclass="select" ID="ddlAccomType" runat="server" Width="134px" OnSelectedIndexChanged="ddlAccomType_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList></td>
                <td>
                    Accomodation:</td>
                <td>
                    <asp:DropDownList cssclass="select" ID="ddlAccomodations" runat="server" Width="236px">
                    </asp:DropDownList></td>
                <td style="width: 53px">
                    Month:</td>
                <td style="width: 600px">
                    <asp:DropDownList cssclass="select" ID="ddlMonths" runat="server" Width="62px">
                    </asp:DropDownList>
                    <asp:DropDownList cssclass="select" ID="ddlYears" runat="server" Width="87px">
                    </asp:DropDownList></td>
                <td style="width: 320px">
                    <asp:Button cssclass="appbutton" ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" Width="71px" /></td>
            </tr>         
        </table>
        <table style="width: 954px">
            <tr>
                <td align="center" colspan="3">
                    <asp:Panel ID="pnlBookingMonthView" runat="server" Width="201px">
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
