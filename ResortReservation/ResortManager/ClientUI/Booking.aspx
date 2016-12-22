<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Booking.aspx.cs" Inherits="ClientUI_Booking" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<%--ValidateRequest="false" EnableEventValidation="false" --%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Booking</title>
    <link rel="stylesheet" type="text/css" media="all" href="../css/calendar-blue2.css"
        title="win2k-cold-1" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/Booking.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />

    <script type="text/javascript" src="../js/calendar/calendar.js"></script>
    <script type="text/javascript" src="../js/calendar/calendar-en.js"></script>
    <script type="text/javascript" src="../js/calendar/calendar-setup.js"></script>
    <script type="text/javascript" src="../js/popups.js"></script>
    <script type="text/javascript" src="../js/global.js"></script>
    <script type="text/javascript" src="../js/client/booking.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Booking" />
    <div id="tooltip">
        <div id="header" class="header">
            No other bookings of this room.</div>
        <div id="detail" class="detail">
        </div>
    </div>
    <asp:ScriptManager ID="scmgrBooking" runat="server">
        <Scripts>
            <asp:ScriptReference Path="../js/json2.js" />
        </Scripts>
    </asp:ScriptManager>
    <%--<asp:UpdatePanel ID="updpnlBooking" runat="server">
        <ContentTemplate> --%>
    <div id="TwoColumn">
        <asp:UpdatePanel ID="updLeft" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="LeftColumn" class="LeftColumn" style="float: left">
                    <div id="filtersection" class="filtersection">
                        <div class="checkinoutInput">
                            <div class="filterLabel">
                                <asp:Literal runat="server" Text="Check In"></asp:Literal>
                            </div>
                            <div>
                                <asp:TextBox CssClass="input" ID="txtStartDate" runat="server"></asp:TextBox>
                                <input type="button" class="datebutton" id="btnStartDate" name="btnStartDate" onfocus="return setupCalendar('txtStartDate', 'btnStartDate')"
                                    onclick="return setupCalendar('txtStartDate', 'btnStartDate')" value="..." />
                            </div>
                        </div>
                        <div class="checkinoutInput">
                            <div class="filterLabel">
                                <asp:Literal runat="server" Text="Check Out"></asp:Literal>
                            </div>
                            <div>
                                <asp:TextBox CssClass="input" ID="txtEndDate" runat="server"></asp:TextBox>
                                <input type="button" class="datebutton" id="btnEndDate" name="btnEndDate" onclick="return setupCalendar('txtEndDate','btnEndDate')"
                                    onfocus="return setupCalendar('txtEndDate','btnEndDate')" value="..." />
                            </div>
                        </div>
                        <div class="checkinoutInput">
                            <div class="filterLabel">
                                <asp:Literal Text="Accom Type" runat="server"></asp:Literal>
                            </div>
                            <asp:DropDownList CssClass="select filterselect" ID="ddlAccomType" DataTextField="AccomType"
                                DataValueField="AccomTypeID" runat="server" AutoPostBack="True" Width="150px"
                                OnSelectedIndexChanged="ddlAccomType_SelectedIndexChanged">
                                <asp:ListItem Value="0">Choose Type</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="checkinoutInput">
                            <div class="filterLabel">
                                <asp:Literal Text="Accom Name" runat="server"></asp:Literal>
                            </div>
                            <asp:DropDownList CssClass="select filterselect" ID="ddlAccomName" runat="server"
                                Width="150px">
                                <asp:ListItem Value="0">Choose Accomodation</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="btnGetAvailableContainer">
                            <asp:Button CssClass="button btnGetAvailableRooms" ID="btnGetAvailableRooms" runat="server"
                                OnClick="btnGetAvailableRooms_Click" Text="Get Available Rooms" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <%--<Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlAccomType" EventName="SelectedIndexChanged" />
        </Triggers>--%>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="updRight" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="RightColumn" class="RightColumn" style="float: left; width: 765px">
                    <div id="inputsection" class="inputsection">
                        <div id="bookingRef_UIElement" style="float: left; width: 505px;">
                            <div id="bookingRefLabel" class="bookingRefLabel">
                                Booking Ref.:</div>
                            <asp:TextBox ID="txtBookingRef" CssClass="bookingRefInputBox" runat="server" MaxLength="50"     width="400px"></asp:TextBox>
                        </div>
                        <div id="agent_UIElement" style="float: left;">
                            <div id="agentLabel" class="agentLabel">
                                Agent:</div>
                            <asp:DropDownList CssClass="select filterselect" ID="ddlAgent" runat="server" Style="width: 200px;">
                            </asp:DropDownList>
                        </div>
                        <div id="pax_UIElement" style="float: left;">
                            <div id="paxLabel" class="paxLabel">
                                Pax:</div>
                            <asp:TextBox CssClass="paxInputBox input" ID="txtNoOfPersons" Width="30px" runat="server"
                                Style="height: auto; font-size: 11px;"></asp:TextBox>
                        </div>
                        <div id="nights_UIElement" style="float: left;">
                            <div id="nightsLabel" class="nightsLabel">
                                Nights:</div>
                            <asp:TextBox CssClass="input nightsInputBox" ID="txtNoOfNights" Width="30px" Style="height: auto;
                                font-size: 11px;" runat="server"></asp:TextBox>
                        </div>
                        <div id="status_UIElement" style="float: left;">
                            <div id="statusLabel" class="statusLabel">
                                <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
                            </div>
                            <asp:TextBox CssClass="input statusInputBox" ID="txtBookingStatus" runat="server"
                                Width="60px" Style="height: auto; font-size: 11px;" Enabled="false"></asp:TextBox>
                        </div>

                           <div id="amount_UIElement" style="float: left;">
                            <div id="AmountLabel" class="statusLabel">
                                <asp:Label ID="amtLabrl" runat="server" Text="Total Amount:"></asp:Label>
                            </div>
                            <asp:TextBox CssClass="input statusInputBox" ID="txtTotalAmount" runat="server"
                                Width="60px" Style="height: auto; font-size: 11px;" Enabled="false"></asp:TextBox>
                        </div>

                        <div id="proposedBooking_UIElement">
                            <div id="proposedBookingLabel" class="proposedBookingLabel">
                                <%--<asp:Label ID="lblProposedBooking" runat="server" Text="Proposed Booking"></asp:Label>--%>
                            </div>
                            <div class="proposedBookingOptions">
                                <asp:RadioButton runat="server" ID="rdProposedBookingYes" Text="Proposed Booking"
                                    GroupName="grpProposedBooking" CssClass="input statusInputBox" Style="height: auto;
                                    font-size: 11px;" Enabled="true"></asp:RadioButton>
                                <asp:RadioButton runat="server" ID="rdProposedBookingNo" Text="Not a proposed booking"
                                    GroupName="grpProposedBooking" CssClass="input statusInputBox" Style="height: auto;
                                    font-size: 11px;" Enabled="true" Checked="true"></asp:RadioButton>
                               <asp:CheckBox runat="server" ID="chkChartered" Text="Chartered" />
                            </div>
                            <div style="clear: both;" />
                        </div>
                    </div>
                    <div>

                        <asp:GridView ID="gdvRatesHotel" runat="server" AutoGenerateColumns="False">

                               <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <AlternatingRowStyle BackColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="RoomCategory" HeaderText="Room Category" />
                                <asp:BoundField DataField="RoomType" HeaderText="RoomType" />
                                <asp:BoundField DataField="Amtc" HeaderText="Rate" />
                            </Columns>
                        </asp:GridView>
                         <asp:GridView ID="gdvRatesCruise" runat="server" AutoGenerateColumns="False">

                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <AlternatingRowStyle BackColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                             <Columns>

                                   <asp:TemplateField HeaderText="Cabin Category" >
                        <ItemTemplate>
                          <asp:Label runat="server"  ID="RoomId" Text='<%#Eval("Cabin Category") %>'></asp:Label>
                        </ItemTemplate>
                      </asp:TemplateField>
                              <asp:TemplateField HeaderText="Price Per Person on Twin Sharing </br> inclusive of taxes">
                        <ItemTemplate>
                          <asp:Label runat="server"  ID="lbltws" Text='<%#Eval("Price Per Person on Twin Sharing inclusive of taxes") %>'></asp:Label>
                        </ItemTemplate>
                      </asp:TemplateField>
                              <asp:TemplateField HeaderText="Price Per Person on Single Cabin</br> inclusive of taxes">
                        <ItemTemplate>
                          <asp:Label runat="server"  ID="lblsc" Text='<%#Eval("Price Per Person on Single Cabin inclusive of taxes") %>'></asp:Label>
                        </ItemTemplate>
                      </asp:TemplateField>
                             </Columns>


                         </asp:GridView>
                    </div>


                    <asp:Panel ID="pnlShowAvailableRoomNos" runat="server">
                        &nbsp;</asp:Panel>
                    <table id="buttonsection" class="buttonsection">
                        <tr>
                            <td>
                                <asp:Button CssClass="appbutton" ID="btnBookTour" runat="server" OnClick="btnBookTour_Click"
                                    Text="Book" Width="87px" />
                            </td>
                            <td>
                                <asp:Button CssClass="appbutton" ID="btnConfirmBooking" runat="server" Text="Confirm Booking"
                                    Width="105px" OnClick="btnConfirmBooking_Click" />
                            </td>
                            <td>
                                <asp:Button CssClass="appbutton" ID="btnCancel" runat="server" Text="Cancel Booking"
                                    OnClick="btnCancel_Click" Width="105px" />
                            </td>
                            <td>
                                <asp:Button CssClass="appbutton" ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click"
                                    Width="87px" />
                            </td>
                            <td>
                                <asp:Button CssClass="appbutton" ID="btnDeleteBooking" runat="server" Text="Delete"
                                    OnClick="btnDeleteBooking_Click" Width="87px" Visible="False" />
                            </td>
                        </tr>

                    </table>
                   
                    <table id="statussection" class="statussection">
                        <tr>
                            <td>
                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Width="660px" Font-Size="X-Small"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table id="hiddensection" class="hiddensection">
                        <tr>
                            <td>
                                <asp:HiddenField ID="hidStartDate" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="hidEndDate" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="hfBookingId" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="hfRoomOtherBookings" runat="server" />
                            </td>
                            <td>
                                <input type="hidden" name="hiddenNoN" style="width: 5px" />
                            </td>
                            <td>
                                <input type="hidden" name="hiddenNoN" style="width: 5px" />
                            </td>
                            <td>
                                  <asp:HiddenField ID="hdnchartered" runat="server" />

                            </td>
                        </tr>
                    </table>
                    <%--<div id="roomsection" class="roomsection">
                    &nbsp;</div>--%>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlAccomType" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlAccomName" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnBookTour" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnConfirmBooking" EventName="Click" />
                <asp:PostBackTrigger ControlID="btnGetAvailableRooms" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%--  </ContentTemplate>           
        </asp:UpdatePanel>--%>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="200">
        <ProgressTemplate>
            <iframe id="pgrIFrame" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"
                src="javascript:'<html></html>';" style="position: absolute; top: 729px; left: 36px;
                height: 68px; width: 208px; z-index: 19999"></iframe>
            <asp:Panel ID="Panel1" runat="server" BackColor="white" BorderColor="#C2D3FC" BorderStyle="solid"
                BorderWidth="1" Height="100" Style="z-index: 20000" Width="300">
                <div style="position: relative; top: 20px; left: 70px;">
                    <asp:Image ID="image2" runat="server" ImageUrl="~/images/indicator.gif" />
                Please Wait....
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <cc1:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1" runat="server"
        TargetControlID="Panel1" HorizontalOffset="300" VerticalOffset="150">
    </cc1:AlwaysVisibleControlExtender>
    </form>
</body>
</html>
