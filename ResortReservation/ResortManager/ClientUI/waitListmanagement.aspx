<%@ Page Language="C#" AutoEventWireup="true" CodeFile="waitListmanagement.aspx.cs" Inherits="ClientUI_waitListtedbookingmanagement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Waitlisted Booking Manager</title>
    <link rel="stylesheet" type="text/css" media="all" href="../css/calendar-blue2.css" title="win2k-cold-1" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/Booking.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/waitListManagement.css" />
    
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-en.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-setup.js"></script>    
    <script language="javascript" type="text/javascript" src="../js/client/booking.js"></script>        
    <script language="javascript" type="text/javascript" src="../js/popups.js"></script>
    <script language="javascript" type="text/javascript" src="../js/global.js"></script> 
    <script language="javascript" type="text/javascript" src="../js/client/waitlistmanagement.js"></script> 
</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="Bookings List" />
    <asp:ScriptManager ID="scmgrwaitListmanagement" runat ="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="pnlwaitListmanagement" runat="server">
    <ContentTemplate>     
    <div id="maindiv" class="parentdiv">
    <table id="filtersection" class="filtersection" style="width:100%">
            <tr>
                <td align="right" class="labelcell" style="width:95px;">
                   <div class="label">Check In:</div></td>
                <td align="left" class="inputcell">
                    <asp:TextBox cssclass="input"  ID="txtStartDate" runat="server" 
                    Width="95px"></asp:TextBox><input type="button" class="datebutton" id="btnStartDate" name ="btnStartDate" onfocus="return setupCalendar('txtStartDate','btnStartDate')" onclick="return setupCalendar('txtStartDate','btnStartDate')" value="..."/></td>
                <td align="right" class="labelcell" style="width: 103px;" >
                    <div class="label">Check Out:</div></td>
                <td align="left" class="inputcell">
                        <asp:TextBox cssclass="input"  ID="txtEndDate" runat="server" 
                        Width="95px"></asp:TextBox><input type="button" class="datebutton" id="btnEndDate" name="btnEndDate" onclick="return setupCalendar('txtEndDate','btnEndDate')" onfocus="return setupCalendar('txtEndDate','btnEndDate')" value="..." /></td>
                <td align="right" class="labelcell" style="width: 140px;" >
                    <div class="label">Accomodation:</div></td>
                <td align="left" class="inputcell" >
                        <asp:DropDownList cssclass="select" ID="ddlAccomodation" runat="server" Width="276px">
                        </asp:DropDownList></td>                
                <td align="left" style="width: 100px;">
                    <asp:Button cssclass="appbutton" ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" /></td>
            </tr>
    </table>
    <div id="gridsection" class="gridsection">
    <asp:Panel ID="pnlwaitlistedbookings" runat="Server"></asp:Panel>    
    </div>
    <table id="hiddensection" class="hiddensection">
    <tr><td>
    <asp:HiddenField ID="hfId" runat="server"/>
    </td>
    </tr>
    </table>
    </div>
    </ContentTemplate>    
    </asp:UpdatePanel>    
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="200">
            <ProgressTemplate>
                <iframe id="pgrIFrame" frameborder="0" marginheight="0" marginwidth="0" scrolling="no" 
                            src="javascript:'<html></html>';" style="position:absolute; 
                            top:729px; left:36px; height:68px; width:208px; z-index:19999"></iframe>
                    <asp:Panel ID="Panel1" runat="server" BackColor="white" BorderColor="#C2D3FC" 
                        BorderStyle="solid" BorderWidth="1" Height="100" Style="z-index:20000" Width="300">
                        <div style="position: relative; top:20px; left:70px;">
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
