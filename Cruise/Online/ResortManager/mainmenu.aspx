<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mainmenu.aspx.cs" Inherits="mainmenu" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Main Menu</title>
    <link rel="stylesheet" type="text/css" media="all" href="css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="style.css" />
    <link rel="stylesheet" type="text/css" media="all" href="css/mainmenu.css" />
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Main Menu" />
        <div id="masterlinks" class="masterlinks">
            <table id="masterlinkssection" class="linkssection">
                <tr>
                    <td class="headercell" colspan="2">MASTERS</td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="MasterUI/AccomTypeMaster.aspx">Accomodation Type</a></td>
                    <td class="menuCell">
                        <a href="MasterUI/MealPlanMaster.aspx">Meal Plans</a></td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="MasterUI/AccomMaster.aspx">Accomodations</a></td>
                    <td class="menuCell">
                        <a href="MasterUI/NationalityMaster.aspx">Nationality</a></td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="MasterUI/AccomodationContactMaster.aspx">Accomodation Contacts</a></td>
                    <td class="menuCell">&nbsp;<a href="MasterUI/RoomTypeMaster.aspx">Room Type</a></td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="MasterUI/AccomodationSeasonMaster.aspx">Accomodation Seasons</a></td>
                    <td class="menuCell">
                        <a href="MasterUI/RegionMaster.aspx">Regions</a>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="MasterUI/AccomWiseActivitiesMaster.aspx">Accomodation Wise Activities</a></td>
                    <td class="menuCell">
                        <a href="MasterUI/RoomMaster.aspx">Rooms</a></td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="MasterUI/AgentMaster.aspx">Agents</a></td>
                    <td class="menuCell">
                        <a href="MasterUI/RoomCategoryMaster.aspx">Room Category</a></td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="MasterUI/ChangeTreeType.aspx">Change Booking Tree Type</a></td>
                    <td class="menuCell">
                        <a href="MasterUI/TransportMaster.aspx">Transport</a></td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="MasterUI/CityMaster.aspx">Cities</a></td>
                    <td class="menuCell">
                        <a href="MasterUI/Users/UserMaster.aspx">User Master</a></td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="MasterUI/EventMessageMaster.aspx">Email Messages</a></td>
                    <td class="menuCell">
                        <a href="MasterUI/Users/UserRoleMaster.aspx">User Roles</a></td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="MasterUI/floormaster.aspx">Floors</a></td>
                    <td class="menuCell">
                        <a href="MasterUI/Users/UserRoleRightsMaster.aspx">Role Rights</a>

                    </td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="MasterUI/MailServerDetailsMaster.aspx">Mail Server Details</a>

                    </td>
                    <td class="menuCell">
                        <a href="MasterUI/Users/UserAgentMaster.aspx">User Agent Mapping</a>

                    </td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="MasterUI/RoomMaintenanceDates.aspx">Room Maintenance</a>

                    </td>

                    </td>
            <td class="menuCell">
                <a href="MasterUI/RateCategory.aspx">Rate Category
                </a>

            </td>

                </tr>

                <tr>
                    <td class="menuCell">
                        <a href="MasterUI/RoomRateMaster.aspx">Room Rate Master</a></td>

                    </td>
                   
            <td class="menuCell"></td>

                </tr>

            </table>
        </div>

        <div id="operationlinks" class="operationlinks">
            <table id="operationslinksection" class="linkssection" style="margin-left: 5px">
                <tr>
                    <td class="headercell">OPERATIONS</td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="ClientUI/BookingChartView.aspx">Booking Chart</a></td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="ClientUI/Booking.aspx?bookingId=0">New Reservation</a></td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="ClientUI/ViewBookings.aspx">Reservations</a></td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="ClientUI/SeriesBooking.aspx?sid=0">New Series</a></td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="ClientUI/viewSeries.aspx">Series List</a></td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="ClientUI/waitListmanagement.aspx">Wait List Management</a></td>
                </tr>
            </table>
        </div>

        <div id="reportLinks" class="operationlinks">
            <table id="tblReportLinks" class="linkssection" style="margin-left: 5px">
                <tr>
                    <td class="headercell">REPORTS</td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="ClientUI/ViewBookings.aspx?cf=true">C-Forms</a></td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="ClientUI/FHReport.aspx?cf=true">Resort Booking Report</a></td>

                </tr>
                <tr>
                    <td class="menuCell" style="display: none;">
                        <a href="ClientUI/TouristCountReport.aspx">Tourist Count Report</a></td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <a href="ClientUI/TouristReportftr.aspx">Tourist  Report</a></td>
                </tr>
            </table>
        </div>
        <div style="clear: both"></div>
    </form>
</body>
</html>
