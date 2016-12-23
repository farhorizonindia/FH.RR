<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MapMaxRoomToAgents.aspx.cs" Inherits="Cruise_Masters_MapMaxRoomToAgents" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../../style.css" />
    <link href="../../css/roleRights.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" media="all" href="../../css/calendar-blue2.css"
        title="win2k-cold-1" />
    <link rel="stylesheet" type="text/css" media="all" href="../../css/Booking.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../../style.css" />
    
    <script language="javascript" type="text/javascript" src="../../js/calendar/calendar.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/calendar/calendar-en.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/calendar/calendar-setup.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/popups.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/global.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/client/booking.js"></script>

    <title>Open Dates Master</title>
    <style>
        .controlsCss
        {
            Width: 161px;
        }

        .txtAlign
        {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Map Accom. Rooms To Agents" />
        <div>
            <asp:ScriptManager ID="scmgrLocation" runat="server">
            </asp:ScriptManager>
           <asp:UpdatePanel ID="Upd1" runat="server">
               <ContentTemplate>


      
                    <br />
                    <div id="ddSection">
                        <table id="tblDd">
                            <tr>
                                <td>&nbsp;&nbsp; Agent
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlAgents" runat="server" OnSelectedIndexChanged="ddlAgents_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                </td>
                                <td>Select Date</td>
                                <td>
                                    <asp:TextBox ID="txtCalender" runat="server"  AutoPostBack="True" ></asp:TextBox>
                                     <input type="button" class="datebutton" id="btnStartDate" name="btnStartDate" onfocus="return setupCalendar('txtCalender', 'btnStartDate')"
                                    onclick="return setupCalendar('txtCalender', 'btnStartDate')" value="..." />
                                </td>
                                <td> <asp:TextBox ID="txtCalenderto" runat="server" AutoPostBack="True" OnTextChanged="txtCalenderto_TextChanged" ></asp:TextBox>
                                     <input type="button" class="datebutton" id="btnEndDate" name="btnEndDate" onfocus="return setupCalendar('txtCalenderto', 'btnEndDate')"
                                    onclick="return setupCalendar('txtCalenderto', 'btnEndDate')" value="..." /></td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div id="gridsection" class="gridsection">
                        <asp:GridView ID="GridAccomodations" runat="server" AutoGenerateColumns="false" EmptyDataText="No Accomodation Found" GridLines="Both" Width="687px">

                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            <Columns>
                                <asp:TemplateField HeaderText="Accom. Id">
                                    <ItemTemplate>
                                        <center><asp:Label runat="server" ID="lbAccomId" Text='<%#Eval("AccomId") %>'></asp:Label></center>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accom. Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbAccomName" Text='<%#Eval("AccomName") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MaxRooms">

                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtMaxRoom" Width="60px"></asp:TextBox>
                                        <asp:Label ID="lbStatus" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />
                    <div id="divsbmt">
                        &nbsp;&nbsp;<asp:Button CssClass="appbutton" runat="server" Text="Submit" OnClick="Unnamed1_Click"></asp:Button>&nbsp;&nbsp;<asp:Button ID="Button1" CssClass="appbutton" runat="server" Text="Reload" OnClick="Button1_Click"></asp:Button>


                    </div>
           
                   <asp:GridView ID="gdvMaxBookableDetails" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Id" OnRowDeleting="gdvMaxBookableDetails_RowDeleting" CellSpacing="5">
                       <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                       <Columns>
                           <asp:TemplateField HeaderText="Sno.">
                               <ItemTemplate>
                                   <%#Container.DataItemIndex+1 %>

                               </ItemTemplate>

                           </asp:TemplateField>
                           <asp:BoundField DataField="AgentName" HeaderText="AgentName" />
                           <asp:BoundField DataField="AccomName" HeaderText="Accomodation" />
                           <asp:BoundField DataField="MaxRooms" HeaderText="MaxRooms" />
                           <asp:BoundField DataField="date" HeaderText="FromDate" DataFormatString="{0:dd-MMM-yyyy}" />
                           <asp:BoundField DataField="ToDate" HeaderText="ToDate" DataFormatString="{0:dd-MMM-yyyy}" />
                           <asp:TemplateField>
                               <ItemTemplate>
                                   <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete"  OnClientClick="return confirm('Are you certain you want to delete this Record?')" >Delete</asp:LinkButton>
                               </ItemTemplate>
                           </asp:TemplateField>
                       </Columns>
                       <EditRowStyle BackColor="#999999" />
                       <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                       <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                       <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                       <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                       <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                       <SortedAscendingCellStyle BackColor="#E9E7E2" />
                       <SortedAscendingHeaderStyle BackColor="#506C8C" />
                       <SortedDescendingCellStyle BackColor="#FFFDF8" />
                       <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
        </div>
                            </ContentTemplate>
           </asp:UpdatePanel>
    </form>
</body>
</html>

