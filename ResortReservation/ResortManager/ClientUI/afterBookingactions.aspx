<%@ Page Language="C#" AutoEventWireup="true" CodeFile="afterBookingactions.aspx.cs" Inherits="ClientUI_afterBookingactions" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>After Booking Actions</title>
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/afterBookingactions.css" />
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Booking Status and Summary" />
        <div>
            <%--<asp:ScriptManager ID="scmgrafterbookingactions" runat ="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="pnlafterbookingactions" runat="server">
    <ContentTemplate>--%>
            <table>
                <tr>
                    <td colspan="2" style="width: 550px">
                        <asp:Panel ID="pnlCurrentBookingDetails" runat="server" Width="550px">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblBookingStatus" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblBookingDetails" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblComment" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblpropsedbook" runat="server"></asp:Label></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pnlReleasedRooms" runat="server" Width="500px"></asp:Panel>
                        <asp:Panel ID="pnlBlockedBookings" runat="server" Width="500px"></asp:Panel>
                        <asp:Panel ID="pnlBookingDetails" runat="server" Visible="False"></asp:Panel>

                        <hr />
                        <div>
                            <div style="margin-bottom: 10px; font: bold;">
                                Sub :<asp:Label ID="lblSubject" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                            <div style="margin-bottom: 10px">
                                <asp:Label ID="Label1" runat="server" Text="Dear Sir/Madam," Style="margin-bottom: 10px"></asp:Label>
                            </div>

                            <div style="margin-bottom: 10px">

                                <asp:Label ID="Label2" runat="server" Text="We are truly pleased to update your booking as per the below details: "></asp:Label>
                            </div>
                        </div>

                        <asp:Panel ID="PnlMailformat" runat="server">
                        </asp:Panel>
                        <asp:Panel ID="pnlmailbookedrms" runat="server">
                        </asp:Panel>
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Cabin/Room number detail :"></asp:Label></td>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text=" "></asp:Label></td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text=" "></asp:Label></td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text=" "></asp:Label></td>

                            </tr>
                        </table>
                        <br />
                        <div style="float: left; clear: left">
                            <asp:Label ID="lblOtherMsg" runat="server" Text="Other Bookings Within The Same Date Are:" Font-Bold="true"></asp:Label>
                            <br />
                            <br />
                            <asp:Panel ID="pnlOtherBookings" runat="server"></asp:Panel>
                            <hr />
                        </div>
                    </td>
                </tr>
            </table>

            <br />
            <br />
        </div>
    </form>
</body>
</html>
