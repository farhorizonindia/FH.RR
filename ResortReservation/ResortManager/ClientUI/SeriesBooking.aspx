<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SeriesBooking.aspx.cs" Inherits="ClientUI_SeriesBooking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Series Booking</title>
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/seriesbooking.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/calendar-blue2.css"
        title="win2k-cold-1" />

    <script language="javascript" type="text/javascript" src="../js/global.js"></script>

    <script language="javascript" type="text/javascript" src="../js/calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-en.js"></script>

    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-setup.js"></script>

    <script language="javascript" type="text/javascript" src="../js/client/seriesbooking.js"></script>

    <script language="javascript" type="text/javascript" src="../js/global.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Series Booking" />
        <asp:ScriptManager ID="scmgrSeriesBooking" runat="server"  EnablePageMethods="true"/>
        <asp:UpdatePanel ID="updpnlSeriesBooking" runat="server">
            <ContentTemplate>
                <div>
                    <table id="filtersection" class="filtersection">
                        <tr>
                            <td class="filterLabel" style="height: 34px">Accom Type
                            </td>
                            <td style="height: 34px">
                                <asp:DropDownList CssClass="select" ID="ddlAccomType" runat="server" Width="165px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlAccomType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="filterLabel" style="height: 34px">Accom
                            </td>
                            <td style="height: 34px">
                                <asp:DropDownList CssClass="select" ID="ddlAccomName" runat="server" Width="252px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlAccomName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="filterLabel" style="height: 34px;">Series Name
                            </td>
                            <td style="height: 34px; width: 194px;">
                                <asp:TextBox CssClass="input" ID="txtSeriesName" runat="Server" Width="185px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table id="inputsection" class="inputsection">
                        <tr>
                            <td class="filterLabel" style="width: 95px;">Series Start Date
                            </td>
                            <td>
                                <asp:TextBox CssClass="input" ID="txtFirstCheckInDate" runat="server" Width="65px"></asp:TextBox><input
                                    type="button" class="datebutton" id="btnFirstCheckInDate" name="btnFirstDepDate"
                                    onclick="return setupCalendar('txtFirstCheckInDate','btnFirstCheckInDate')" onfocus="return setupCalendar('txtFirstCheckInDate','btnFirstCheckInDate')"
                                    value="..." />
                            </td>
                            <td class="filterLabel" style="padding-right: 0px;">Agents
                            </td>
                            <td style="width: 170px;">
                                <asp:DropDownList CssClass="select" ID="ddlAgent" runat="Server" Width="192px">
                                </asp:DropDownList>
                            </td>
                            <td class="filterLabel" style="width: 40px">Nights
                            </td>
                            <td style="width: 49px">
                                <asp:DropDownList CssClass="select" ID="ddlNoOfNights" runat="Server" Width="70px">
                                </asp:DropDownList>
                            </td>
                            <td class="filterLabel" style="width: 22px">Gap
                            </td>
                            <td style="width: 57px">
                                <asp:DropDownList CssClass="select" ID="ddlGap" runat="Server" Width="70px">
                                </asp:DropDownList>
                            </td>
                            <td class="filterLabel" style="width: 58px">Departures
                            </td>
                            <td style="width: 68px">
                                <asp:DropDownList CssClass="select" ID="ddlNoOfDeps" runat="Server" Width="70px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <div id="gridsection" class="gridsection" style="width: 123%">
                        <div id="leftsection" class="leftsection">
                            <asp:Panel ID="pnlTotalRoomCount" runat="server">
                            </asp:Panel>
                        </div>
                        <div id="rightsection" class="rightsection">
                            <asp:Panel ID="pnlSeries" runat="server">
                            </asp:Panel>
                            <asp:Panel ID="pnlRegenSeries" runat="server" Style="display: none;">
                                <asp:Label ID="lblRegenerateSeries" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Button ID="btnRegenerateSeries" runat="server" Text="Re-generate Series" OnClick="btnRegenerateSeries_Click" />
                            </asp:Panel>
                        </div>
                        <div style="clear: both;">
                        </div>
                    </div>
                    <table id="buttonsection" class="buttonsection">
                        <tr>
                            <td>
                                <asp:Button CssClass="appbutton" ID="btnGenerateSeries" Text="Generate Series" runat="Server"
                                    OnClick="btnGenerateSeries_Click" Width="95px" />
                            </td>
                            <td>
                                <asp:Button CssClass="appbutton" ID="btnSaveSeries" Text="Save Series" runat="server"
                                    OnClick="btnSaveSeries_Click" Width="75px" />
                            </td>
                        </tr>
                    </table>
                    <div id="hiddensection" class="hiddensection">
                        <asp:HiddenField ID="hfAccomTypeId" runat="server" />
                        <asp:HiddenField ID="hfAccomId" runat="server" />
                        <asp:HiddenField ID="hfFirstCheckInDate" runat="server" />
                        <asp:HiddenField ID="hfNoOfNights" runat="server" />
                        <asp:HiddenField ID="hfGap" runat="server" />
                        <asp:HiddenField ID="hfDepartures" runat="server" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="200">
            <ProgressTemplate>
                <iframe id="pgrIFrame" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"
                    src="javascript:'<html></html>';" style="position: absolute; top: 729px; left: 36px; height: 68px; width: 208px; z-index: 19999"></iframe>
                <asp:Panel ID="Panel1" runat="server" BackColor="white" BorderColor="#C2D3FC" BorderStyle="solid"
                    BorderWidth="1" Height="100" Style="z-index: 20000" Width="300">
                    <div style="position: relative; top: 20px; left: 70px;">
                        <asp:Image ID="image2" runat="server" ImageUrl="~/images/indicator.gif" />
                    Please Wait....
                </asp:Panel>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <cc1:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1" runat="server"
            TargetControlID="Panel1" HorizontalOffset="300" VerticalOffset="150"></cc1:AlwaysVisibleControlExtender>
    </form>
</body>
</html>
