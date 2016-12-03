<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewSeries.aspx.cs" Inherits="ClientUI_viewSeries" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Series List</title>

    <script language="javascript" type="text/javascript" src="../js/calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-en.js"></script>

    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-setup.js"></script>

    <script language="javascript" type="text/javascript" src="../js/global.js"></script>

    <script language="javascript" type="text/javascript" src="../js/client/viewSeries.js"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/calendar-blue2.css"
        title="win2k-cold-1" />
</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Series List" />
    <div>
        <asp:ScriptManager ID="scmgrViewSeries" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="pnlViewSeries" runat="server">
            <ContentTemplate>
                <table id="filtersection" class="filtersection" style="width: 600px; margin-left: 10px;">
                    <tr>
                        <td align="left" style="font-size: 8pt; height: 19px">
                            Series Start Date:
                        </td>
                        <td align="left" style="width: 129px; height: 19px">
                            <asp:TextBox CssClass="input" ID="txtStartDate" runat="server" Font-Size="8pt" Width="95px"></asp:TextBox>
                            <input type="button" class="datebutton" id="btnStartDate" name="btnStartDate" onfocus="return setupCalendar('txtStartDate','btnStartDate')"
                                onclick="return setupCalendar('txtStartDate','btnStartDate')" value="..." />
                        </td>
                        <td align="left" style="font-size: 8pt; height: 19px">
                            Accomodation:
                        </td>
                        <td align="left" style="font-size: 8pt; height: 19px">
                            <asp:DropDownList CssClass="select" ID="ddlAccomName" runat="server" Width="220px">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="font-size: 8pt; height: 19px">
                            <asp:Button CssClass="appbutton" ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />
                        </td>
                    </tr>
                </table>
                <div id="gridsection" class="gridsection" style="margin-left: 10px;">
                    <asp:DataGrid ID="dgSeries" DataKeyField="SeriesID" runat="server" AutoGenerateColumns="False"
                        Width="951px" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True"
                        OnPageIndexChanged="dgSeries_PageIndexChanged" OnEditCommand="dgSeries_EditCommand">
                        <Columns>
                            <asp:BoundColumn DataField="SeriesId" HeaderText="Series Id"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Accomodation" HeaderText="Accomodation"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SeriesStartDate" HeaderText="Series Starting On" DataFormatString="{0:dd-MMM-yyyy}">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SeriesName" HeaderText="Series Name"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NoOfdepartures" HeaderText="Departures"></asp:BoundColumn>
                            <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update" HeaderText="[...]">
                            </asp:EditCommandColumn>
                            <asp:ButtonColumn CommandName="Delete" Text="Delete" HeaderText="[...]" Visible="False">
                            </asp:ButtonColumn>
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
        </asp:UpdatePanel>
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
        <cc1:alwaysvisiblecontrolextender id="AlwaysVisibleControlExtender1" runat="server"
            targetcontrolid="Panel1" horizontaloffset="300" verticaloffset="150">
    </cc1:alwaysvisiblecontrolextender>
    </div>
    </form>
</body>
</html>
