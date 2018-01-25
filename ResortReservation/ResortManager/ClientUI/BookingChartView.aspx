<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookingChartView.aspx.cs"
    Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Booking Chart</title>
    <link rel="stylesheet" type="text/css" media="all" href="../css/bookingChart.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />

    <script language="javascript" type="text/javascript" src="../js/calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-en.js"></script>

    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-setup.js"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../css/calendar-blue2.css"
        title="win2k-cold-1" />

    <script language="javascript" type="text/javascript" src="../js/tooltip.js"></script>

    <script language="javascript" type="text/javascript" src="../js/client/bookingchartview.js"></script>

    <script language="javascript" type="text/javascript" src="../js/popups.js"></script>

    <script language="javascript" type="text/javascript" src="../js/global.js"></script>
    
 
 

</head>
<body>

    


    <form id="form1" runat="server">
    <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Booking Chart" />
    <div>
        <asp:ScriptManager ID="scmgrBookingChartView" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="pnlBookingChartView" runat="server">
            <ContentTemplate>



                <table id="MainTable" class="mainTable">
                    <tr>
                        <td class="changeTree">
                            <a href='#' class="applink" onclick="popUpTreeTypeChanger('../MasterUI/ChangeTreeType.aspx','height=200, width=450, left=200, top=200, scrollbars=yes')">
                            Change Tree
                        </td>
                        <td style="width: 350px;">
                            <div style="padding-left:96px;">
                                <asp:Button CssClass="btnnavigator" ID="btnPrevYear" runat="server" Text="<<<" TabIndex="1"
                                    ToolTip="Previous Year" OnClick="btnPrevYear_Click" />
                                <asp:Button CssClass="btnnavigator" ID="btnPrevMonth" runat="server" Text="<<" OnClick="btnPrevMonth_Click"
                                    TabIndex="2" ToolTip="Previous Month" />
                                <asp:Button CssClass="btnnavigator" ID="btnPrevDay" runat="server" Text="<" OnClick="btnPrevDay_Click"
                                    TabIndex="3" ToolTip="Previous Day" />
                                <asp:TextBox CssClass="input" ID="txtFromDate" runat="server" Width="84px" BackColor="Lavender"
                                    BorderStyle="Double" Font-Size="10pt" Height="18px" ReadOnly="True" TabIndex="3"
                                    Wrap="False" Style="margin: 0px 0px 1px 0px;"></asp:TextBox>
                                <asp:Button CssClass="btnnavigator" ID="btnNextDay" runat="server" Text=">" OnClick="btnNextDay_Click"
                                    TabIndex="4" ToolTip="Next Day" />
                                <asp:Button CssClass="btnnavigator" ID="btnNextMonth" runat="server" Text=">>" OnClick="btnNextMonth_Click"
                                    TabIndex="5" ToolTip="Next Month" />
                                <asp:Button CssClass="btnnavigator" ID="btnNextYear" runat="server" Text=">>>" TabIndex="6"
                                    ToolTip="Next Year" OnClick="btnNextYear_Click" />
                            </div>
                        </td>
                        <td align="right" style="text-align: right;">
                            <div style="float:left; padding-left:50px;">
                                <asp:TextBox CssClass="input" ID="txtStartDate" runat="server" Width="91px"></asp:TextBox>
                                <input type="button" class="datebutton" id="btnFromDate" name="btnFromDate" onclick="return setupCalendar('txtStartDate','btnFromDate')"
                                    onfocus="return setupCalendar('txtStartDate','btnFromDate')" value="..." />
                                <asp:Button CssClass="appbutton" Style="vertical-align: top;" ID="btnStartChartFrom"
                                    runat="server" OnClick="btnStartChartFrom_Click" Text="Start From" Width="72px"
                                    Height="23px" />
                        </td>
                        <div>
                        </div>
                    </tr>
                </table>
                <div class="Two_Column">
                    <div class="LeftColumn" style="float: left;">
                        <div id="uppersection" class="uppersection" style="height: 300px">
                            <asp:TreeView ID="tvRegions" CssClass="charttree" runat="server" ExpandDepth="1"
                                ImageSet="Msdn" OnSelectedNodeChanged="tvRegions_SelectedNodeChanged" NodeIndent="10"
                                Style="margin-left: 3px; margin-top: 3px;">
                                <ParentNodeStyle Font-Bold="False" />
                                <HoverNodeStyle Font-Underline="True" BackColor="#CCCCCC" BorderColor="#888888" />
                                <SelectedNodeStyle Font-Underline="False" HorizontalPadding="3px" VerticalPadding="1px"
                                    BackColor="White" BorderColor="#888888" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Bold="True" />
                                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                                    NodeSpacing="1px" VerticalPadding="2px" />
                                <DataBindings>
                                    <asp:TreeNodeBinding DataMember="RegionId" TextField="#Name" />
                                </DataBindings>
                            </asp:TreeView>
                        </div>
                    </div>
                    <div class="RightColumn" style="float: left;">
                        <asp:Panel ID="pnlBookingView" runat="server" Direction="LeftToRight" HorizontalAlign="Center">
                            <div id="mainTT" class="mainTT" style="display: none;">
                            </div>
                        </asp:Panel>
                        <table id="LegendSection">
                            <tr>
                                <td class="proposedCell" style="width: 80px; color: White; padding-left: 4px;">
                                    Proposed
                                </td>
                                <td class="bookedCell" style="width: 80px; padding-left: 4px;">
                                    Booked
                                </td>
                                <td class="waitListedCell" style="width: 80px; padding-left: 4px;">
                                    Wait Listed
                                </td>
                                <td class="confirmedCell" style="width: 80px; padding-left: 4px;">
                                    Confirmed
                                </td>
                                 <td class="maintenanceCell" style="width: 80px; padding-left: 4px;">
                                    Under Maintenance
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="clear: both;">
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
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
