<%@ Page Language="C#" AutoEventWireup="true" CodeFile="afterBookingSeriesactions.aspx.cs"
    Inherits="ClientUI_afterBookingSeriesactions" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>After Booking Series Actions</title>
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <link rel="Stylesheet" type="text/css" media="all" href="../css/seriesbooking.css" />
</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Series Booking Status and Summary" />
    <div>
        <%--<asp:ScriptManager ID="scmgrafterbookingactions" runat ="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="pnlafterbookingactions" runat="server">
    <ContentTemplate>--%>
        <table>
            <tr>
                <td colspan="2" style="width: 550px">
                    <asp:Panel ID="pnlCurrentSeriesBookingDetails" runat="server" Width="550px">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSeriesBookingStatus" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSeriesBookingDetails" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblComment" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    <div class="bookingListContainer">
        <div>
            <asp:Label ID="lblBookingDetails" runat="server"></asp:Label>
        </div>
        <asp:Panel ID="pnlBookingList" runat="server">
        </asp:Panel>
    </div>
    </form>
</body>
</html>
