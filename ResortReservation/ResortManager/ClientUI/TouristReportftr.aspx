<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TouristReportftr.aspx.cs" Inherits="ClientUI_TouristReportftr" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/viewbookings.css" />
     <script language="javascript" type="text/javascript" src="../js/calendar/calendar.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-en.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-setup.js"></script>
    <script language="javascript" type="text/javascript" src="../js/global.js"></script>
    <script language="javascript" type="text/javascript" src="../js/client/viewbookings.js"></script>


    <link rel="stylesheet" type="text/css" media="all" href="../css/calendar-blue2.css" />

    <title>Tourist List</title>
    <style>
        .singlebookingcell
        {
            border: solid 2px !important;
        }

        .auto-style1
        {
            align: left;
            font-size: 8pt;
            height: 19px;
            padding-left: 3px;
            width: 39px;
        }

        .auto-style2
        {
            align: left;
            font-size: 8pt;
            height: 19px;
            padding-left: 3px;
            width: 104px;
        }
        .auto-style3
        {
            align: left;
            font-size: 8pt;
            height: 19px;
            padding-left: 3px;
            width: 25px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Tourist List" />
    <div>
        <asp:ScriptManager ID="scmgrviewbookings" runat="server">
        </asp:ScriptManager>
     
        <asp:UpdatePanel ID="pnlviewbookings" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table id="filtersection" class="filtersection">
                        <tr>
                            <td class="filtersectionCell">
                                Check-In:</td>
                            <td class="filtersectionCell">
                                <asp:TextBox CssClass="input" ID="txtStartDate" runat="server" Font-Size="8pt" Width="95px"></asp:TextBox>
                                <input type="button" class="datebutton" id="btnStartDate" name="btnStartDate" onfocus="return setupCalendar('txtStartDate','btnStartDate')"
                                    onclick="return setupCalendar('txtStartDate', 'btnStartDate')" value="..." /></td>
                            <td class="filtersectionCell">
                                Check-out:</td>
                            <td class="auto-style2" style="width:140px" >
                                <asp:TextBox CssClass="input" ID="txtEndDate" runat="server" Font-Size="8pt" Width="95px"></asp:TextBox>
                                <input type="button" class="datebutton" id="btnEndDate" name="btnEndDate" onclick="return setupCalendar('txtEndDate', 'btnEndDate')"
                                    onfocus="return setupCalendar('txtEndDate','btnEndDate')" value="..." /></td>
                            <td class="filtersectionCell">Name:</td>
                            <td class="filtersectionCell">
                                <asp:TextBox CssClass="input" ID="txtName" runat="server" Font-Size="8pt" Width="95px"></asp:TextBox>
                                </td>
                            <td class="auto-style1">
                                Booking Code:</td>
                            <td class="filtersectionCell">
                                <asp:TextBox CssClass="input" ID="txtbkCode" runat="server" Font-Size="8pt" Width="95px"></asp:TextBox>
                              </td>
                            <td class="auto-style3">
                               Email Id:</td>
                            <td class="filtersectionCell">
                                <asp:TextBox CssClass="input" ID="txtmailId" runat="server" Font-Size="8pt" Width="213px"></asp:TextBox></td>
                            <td class="filtersectionCell">
                                Passport No:</td>


                            <td class="filtersectionCell">
                                <asp:TextBox ID="txtPassportNo" runat="server" CssClass="input" Font-Size="8pt" Width="95px"></asp:TextBox>
                                </td>  
                            
                             <td class="filtersectionCell">
                                </td>


                            <td class="filtersectionCell">
                                 
                                </td>  

                                                      
                            <td class="filtersectionCell">
                              
                            </td>
                            <td class="filtersectionCell"></td>
                        </tr>
                        <tr>
                            <td class="filtersectionCell">  Accom type</td>
                            <td class="filtersectionCell"><asp:DropDownList CssClass="select filterselect" ID="ddlAccomType" DataTextField="AccomType"
                                DataValueField="AccomTypeID" runat="server" AutoPostBack="True" Width="150px" OnSelectedIndexChanged="ddlAccomType_SelectedIndexChanged"
                               >
                                <asp:ListItem Value="0">Choose Type</asp:ListItem>
                            </asp:DropDownList></td>
                            <td class="filtersectionCell">Accomodation no:</td>
                            <td class="auto-style2"><asp:DropDownList CssClass="select filterselect" ID="ddlAccomName" runat="server"
                                Width="150px"></asp:DropDownList></td>
                            <td class="filtersectionCell"><asp:Button ID="btnShow"   runat="server" CssClass="appbutton" OnClick="btnShow_Click" Text="Show" />
                                <asp:Button ID="btnExport" runat="server" CssClass="appbutton" OnClick="btnExport_Click" Text="Download" /></td>
                            <td class="filtersectionCell">&nbsp;</td>
                            <td class="auto-style1">&nbsp;</td>
                            <td class="filtersectionCell">&nbsp;</td>
                            <td class="auto-style3">&nbsp;</td>
                            <td class="filtersectionCell">&nbsp;</td>
                            <td class="filtersectionCell">&nbsp;</td>
                            <td class="filtersectionCell">&nbsp;</td>
                            <td class="filtersectionCell">&nbsp;</td>
                            <td class="filtersectionCell">&nbsp;</td>
                        </tr>
                    </table>
                   <asp:Label ID="lblTouristNotFound" runat="server"></asp:Label>
                <div id="gridsection" class="gridsection">
                    <asp:DataGrid ID="dgTouristDetails" PageSize="30" runat="server" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanged="dgTouristDetails_PageIndexChanged" Width="100%" >
                        <Columns>
                            <asp:BoundColumn DataField="BookingCode" HeaderText="Booking Ref. No.">
                                <ItemStyle CssClass="column" />
                                <HeaderStyle CssClass="columnHeader" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BookingRef" HeaderText="Booking Reference Name">
                                <ItemStyle CssClass="column" />
                                <HeaderStyle CssClass="columnHeader"  />
                            </asp:BoundColumn>



                              <asp:BoundColumn DataField="AccomName" HeaderText="Accomodation Name">
                                <ItemStyle CssClass="column" />
                                <HeaderStyle CssClass="columnHeader" />
                            </asp:BoundColumn>
                              <asp:BoundColumn DataField="CheckinDate" HeaderText="Check in" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle CssClass="column" />
                                <HeaderStyle CssClass="columnHeader" />
                            </asp:BoundColumn>
                              <asp:BoundColumn DataField="CheckoutDate" HeaderText="Check Out" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle CssClass="column" />
                                <HeaderStyle CssClass="columnHeader" />
                            </asp:BoundColumn>





                            <asp:BoundColumn DataField="AgentName" HeaderText="FTO Name">
                                <ItemStyle CssClass="column" />
                                <HeaderStyle CssClass="columnHeader" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ClientName" HeaderText="Client Name">
                                <ItemStyle CssClass="column" />
                                <HeaderStyle CssClass="columnHeader" />
                            </asp:BoundColumn>
                             <asp:BoundColumn DataField="EmailId" HeaderText="E-mail">
                                <ItemStyle CssClass="column" />
                                <HeaderStyle CssClass="columnHeader" />
                            </asp:BoundColumn>

                            <asp:BoundColumn DataField="Gender" HeaderText="Gender">
                                <ItemStyle CssClass="column" />
                                <HeaderStyle CssClass="columnHeader" />
                            </asp:BoundColumn>

                               <asp:BoundColumn DataField="DateOfBirth" HeaderText="DOB" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle CssClass="column" />
                                <HeaderStyle CssClass="columnHeader" />
                            </asp:BoundColumn>

                             <asp:BoundColumn DataField="Nationality" HeaderText="Citizen of">
                                <ItemStyle CssClass="column" />
                                <HeaderStyle CssClass="columnHeader" />
                            </asp:BoundColumn>
                             <asp:BoundColumn DataField="PassportNo" HeaderText="PassportNo">
                                <ItemStyle CssClass="column" />
                                <HeaderStyle CssClass="columnHeader" />
                            </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="View/Edit Details">
                                <ItemTemplate>
                                    <a href='<%# "touristDetails.aspx?op=edit&bid=" + DataBinder.Eval(Container.DataItem,"BookingID") + "&tno=" + DataBinder.Eval(Container.DataItem,"Touristno") %>'>
                                        Edit</a>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                          
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditItemStyle BackColor="#2461BF" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EFF3FB" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        
                    </asp:DataGrid>
                </div>
            </ContentTemplate>
               <Triggers>
                    <asp:PostBackTrigger ControlID="btnExport" />
                    <asp:AsyncPostBackTrigger ControlID="dgTouristDetails" EventName="PageIndexChanged" />
                </Triggers>
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
