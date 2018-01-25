<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookingConfirmation.aspx.cs"
    Inherits="ClientUI_BookingConfirmation" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Booking Confirmation</title>

    <script language="javascript" type="text/javascript" src="../js/calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-en.js"></script>

    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-setup.js"></script>

    <script language="javascript" type="text/javascript" src="../js/client/bookingconfirmation.js"></script>

    <script language="javascript" type="text/javascript" src="../js/global.js"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../css/calendar-blue2.css"
        title="win2k-cold-1" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/bookingconfirmation.css" />
    <style type="text/css">
        .auto-style1
        {
            font-family: Arial;
            font-size: 8pt;
            padding-left: 3px;
            height: 27px;
        }
        .auto-style2
        {
            padding-right: 3px;
            padding-top: 2px;
            padding-bottom: 2px;
            width: 155px;
            height: 27px;
        }
        .auto-style3
        {
            padding-right: 3px;
            padding-top: 2px;
            padding-bottom: 2px;
            width: 209px;
        }
    </style>
</head>
<%--<body onload="SetFocus()" >--%>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Booking Confirmation" />
    <div>
        <asp:ScriptManager ID="scmgrBooking" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updpnlBooking" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table id="infosection" class="infosection">
                    <tr>
                        <td class="labelcell" style="width: 75px;">
                            Check In:
                        </td>
                        <td class="inputcell">
                            <asp:TextBox CssClass="input" ID="txtStartDate" runat="server" Width="150px" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="labelcell" style="width: 75px;">
                            Check Out:
                        </td>
                        <td class="inputcell">
                            <asp:TextBox CssClass="input" ID="txtEndDate" runat="server" Width="150px" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="labelcell" style="width: 50px;">
                            Nights:
                        </td>
                        <td class="inputcell">
                            <asp:TextBox CssClass="input" ID="txtNoOfNights" runat="server" Width="35px" ReadOnly="True"
                                Enabled="False"></asp:TextBox>
                        </td>
                        <td class="labelcell" style="width: 110px; text-align: right;">
                            No. of Pax:
                        </td>
                        <td class="inputcell" colspan="3">
                            <asp:TextBox CssClass="input" ID="txtNoOfPersons" runat="server" Width="35px" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="labelcell" style="width: 110px;">
                            Accomodation Type:
                        </td>
                        <td class="inputcell">
                            <asp:TextBox CssClass="input" ID="txtAccomType" runat="server" Enabled="False" Width="150px"></asp:TextBox>
                        </td>
                        <td class="labelcell" style="width: 110px;">
                            Accomodation Name:
                        </td>
                        <td class="inputcell">
                            <asp:TextBox CssClass="input" ID="txtAccomodation" runat="server" Enabled="False"
                                Width="180px"></asp:TextBox>
                        </td>
                        <td class="labelcell" style="width: 50px;">
                            Status:
                        </td>
                        <td class="inputcell" colspan="2">
                            <asp:TextBox CssClass="input" ID="txtBookingStatus" runat="server" Enabled="False"
                                MaxLength="50" Width="107px"></asp:TextBox>
                        </td>
                        <td class="inputcell" style="width: 6px">
                        </td>
                    </tr>
                    <tr>
                        <td class="labelcell" style="width: 110px;">
                            Booking Ref.:
                        </td>
                        <td class="inputcell">
                            <asp:TextBox CssClass="input" ID="txtBookingRef" runat="server" MaxLength="50" Width="180px"
                                Enabled="False"></asp:TextBox>
                        </td>
                        <td class="labelcell" style="width: 110px;">
                            Agent:
                        </td>
                        <td class="inputcell">
                            <asp:TextBox CssClass="input" ID="txtAgent" runat="server" Enabled="False" Width="180px"></asp:TextBox>
                        </td>
                        <td align="left" colspan="3">
                        </td>
                        <td class="inputcell" style="width: 6px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <div>
                                <div id="bookedroomssectionheader" class="bookedroomssectionheader">
                                    <img id="imgbookedroomssection" src="../images/icon_summary_minus_On.gif" alt="" onclick="javascript:showhide('bookedroomssection');" />
                                    <span>Show Booked Rooms</span>
                                </div>
                                <div id="bookedroomssection" class="bookedroomssection" style="display: block;">
                                    <asp:Panel ID="pnlBookedRoomDetails" runat="server" GroupingText="Rooms Booked" CssClass="pnlBookedRoomDetails">
                                    </asp:Panel>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                <table id="inputsection1" class="inputsection">
                    <tr>
                        <td class="labelcell">
                            Exchange Order No.:
                        </td>
                        <td class="inputcell">
                            <asp:TextBox CssClass="input" ID="txtExchangeOrderNo" runat="server" Width="163px"></asp:TextBox>
                        </td>
                        <td class="labelcell">
                            Voucher Date:
                        </td>
                        <td class="inputcell">
                            <asp:TextBox CssClass="input" ID="txtVoucherDate" runat="server" Width="99px"></asp:TextBox><input
                                type="button" class="datebutton" id="btnVoucherDate" onclick="return setupCalendar('txtVoucherDate','btnVoucherDate')"
                                onfocus="return setupCalendar('txtVoucherDate','btnVoucherDate')" value="..." />
                        </td>
                        <td class="labelcell">
                            Tour No.:
                        </td>
                        <td class="inputcell">
                            <asp:TextBox CssClass="input" ID="txtTourId" runat="server" MaxLength="50" Width="208px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div id="pnlMealandActivity" style="clear: both;">
                    <table id="inputsection4" class="inputsection">
                        <tr>
                            <td style="width: 126px">
                                <asp:Panel ID="pnlDateWiseSchedule" runat="server">
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="pnlTravelInfo">
                    <div id="divLeft" style="float: left;">
                        <table id="inputsection2" class="inputsection">
                            <tr>
                                <td class="labelcell" style="font-weight: bold; background-color: #95BEDE" colspan="2">
                                    Arrival
                                </td>
                            </tr>
                            <tr>
                                <td class="labelcell">
                                    DateTime:
                                </td>
                                <td class="inputcell" style="width: 155px">
                                    <asp:TextBox CssClass="input" ID="txtArrivalDate" runat="server" Width="125px"></asp:TextBox><input
                                        type="button" id="btnArrivalDate" class="datebutton" onclick="return setupCalendarWithTime('txtArrivalDate','btnArrivalDate')"
                                        onfocus="return setupCalendarWithTime('txtArrivalDate','btnArrivalDate')" value="..." />
                                </td>
                            </tr>
                            <tr>
                                <td class="labelcell">
                                    City:
                                </td>
                                <td class="inputcell" style="width: 155px">
                                    <asp:DropDownList CssClass="select" ID="ddlArrivalCity" DataTextField="CityName"
                                        DataValueField="CityId" runat="server" Width="185px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelcell">
                                    Transport Mode:
                                </td>
                                <td class="inputcell" style="width: 155px">
                                    <asp:DropDownList ID="ddlArrivalTransport" runat="server" Width="185px" CssClass="select">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelcell">
                                    No.
                                </td>
                                <td class="inputcell" style="width: 155px">
                                    <asp:TextBox CssClass="input" ID="txtArrivalVehicleNo" runat="server" Width="140px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelcell">
                                    Vehicle Name/Type:
                                </td>
                                <td class="inputcell" style="width: 155px">
                                    <asp:TextBox ID="txtArrivalVehicleNameType" runat="server" CssClass="input" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelcell">
                                    Transport Company:
                                </td>
                                <td class="inputcell" style="width: 155px">
                                    <asp:TextBox ID="txtArrivalTrasnportCompany" runat="server" CssClass="input" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelcell">
                                    Transport Phone No.:
                                </td>
                                <td class="inputcell" style="width: 155px">
                                    <asp:TextBox ID="txtArrivalTransportCompanyPhoneNo" runat="server" CssClass="input"
                                        Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">
                                    Driver Phone No.:
                                </td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtArrivalDriverPhoneNo" runat="server" CssClass="input" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divRight" style="float: left;">
                        <table id="inputsection3" class="inputsection">
                            <tr>
                                <td class="labelcell" style="font-weight: bold; background-color: #95BEDE" colspan="2">
                                    Departure
                                </td>
                            </tr>
                            <tr>
                                <td class="labelcell">
                                    Date Time:
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox CssClass="input" ID="txtDepartureDate" runat="server" Width="125px"></asp:TextBox><input
                                        type="button" id="btnDepartureDate" class="datebutton" onclick="return setupCalendarWithTime('txtDepartureDate','btnDepartureDate')"
                                        onfocus="return setupCalendarWithTime('txtDepartureDate','btnDepartureDate')"
                                        value="..." />
                                </td>
                            </tr>
                            <tr>
                                <td class="labelcell">
                                    City:
                                </td>
                                <td class="auto-style3">
                                    <asp:DropDownList CssClass="select" ID="ddlDepartureCity" runat="server" Width="185px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelcell">
                                    Transport Mode:
                                </td>
                                <td class="auto-style3">
                                    <asp:DropDownList ID="ddlDepartureTransport" runat="server" Width="185px" CssClass="select">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelcell">
                                    No.
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox CssClass="input" ID="txtDepartureVehicleNo" runat="server" Width="140px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelcell">
                                    Vehicle Name/Type:
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="txtDepartureVehicleNameType" runat="server" CssClass="input" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelcell">
                                    Transport Company:
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="txtDepartureTransportCompany" runat="server" CssClass="input" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelcell">
                                    Transport Phone No.:
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="txtDepartureTransportCompanyPhoneNo" runat="server" CssClass="input"
                                        Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelcell">
                                    Driver Phone No.:
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="txtDepartureDriverPhoneNo" runat="server" CssClass="input" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="clear: both;">
                    </div>
                </div>
                <table id="buttonsection" class="buttonsection">
                    <tr>
                        <td>
                            <asp:Button CssClass="appbutton" ID="btnConfirmBooking" runat="server" Text="Confirm Booking"
                                OnClick="btnConfirmBooking_Click" Width="111px" />
                        </td>
                        <td>
                            <asp:Button CssClass="appbutton" ID="btnCancelConfirmation" runat="server" Text="Cancel Confirmation"
                                Width="112px" OnClick="btnCancelConfirmation_Click" />
                        </td>
                        <td>
                            <asp:Button CssClass="appbutton" ID="btnEditTour" runat="server" OnClick="btnEditTour_Click"
                                Text="View/Edit Booking" Width="94px" />
                        </td>
                        <td>
                            <asp:Button CssClass="appbutton" ID="btnCancelBooking" runat="server" Text="Cancel Booking"
                                Width="94px" OnClick="btnCancelBooking_Click" />
                        </td>
                        <td>
                            <asp:Button CssClass="appbutton" ID="btnAddTouristDetails" runat="server" Text="Add Tourist"
                                OnClick="btnAddTouristDetails_Click" Width="94px" />
                        </td>
                        <td>
                            <asp:Button CssClass="appbutton" ID="btnViewTourist" runat="server" Text="View Tourist"
                                OnClick="btnViewTourist_Click" Width="94px" />
                        </td>
                        <td>
                            <asp:Button CssClass="appbutton" ID="btnReset" runat="server" Text="Reset" Width="94px"
                                OnClick="btnReset_Click" />
                        </td>
                    </tr>
                </table>
                <table id="statussection" class="statussection">
                    <tr>
                        <td>
                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Width="872px" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table id="hiddensection" class="hiddensection">
                    <tr>
                        <td>
                            <asp:HiddenField ID="hfAccomId" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
