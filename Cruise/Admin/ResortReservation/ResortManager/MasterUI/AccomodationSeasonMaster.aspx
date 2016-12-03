<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccomodationSeasonMaster.aspx.cs"
    Inherits="MasterUI_AccomodationSeasonMaster" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Accomodation Season Master</title>
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />

    <script language="javascript" type="text/javascript" src="../js/calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-en.js"></script>

    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-setup.js"></script>

    <script language="javascript" type="text/javascript" src="../js/master/accommaster.js"></script>

    <script language="javascript" type="text/javascript" src="../js/global.js"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../css/calendar-blue2.css"
        title="win2k-cold-1" />
</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Accomodation Season Master" />
    <div>
        <asp:ScriptManager ID="scmgrAccomMaster" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="pnlAccomMaster" runat="server">
            <ContentTemplate>
                <div>
                    <div id="leftCol" style="float: left; margin-right: 10px;">
                        <div id="filterSection">
                            <asp:TreeView ID="tvAccomodations" runat="server" 
                                onselectednodechanged="tvAccomodations_SelectedNodeChanged">
                            </asp:TreeView>
                        </div>
                    </div>
                    <div id="rightCol" style="float: left; margin-right: 10px;">
                        <div id="gridsection" class="gridsection" style="float: right">
                            <asp:DataGrid ID="dgAccomodationSeasons" runat="server" AutoGenerateColumns="False"
                                BorderStyle="Ridge" CellPadding="4" DataKeyField="AccomodationId" ForeColor="#333333"
                                GridLines="None" OnSelectedIndexChanged="dgAccomodationSeasons_SelectedIndexChanged"
                                TabIndex="3" Width="350px">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditItemStyle BackColor="#2461BF" />
                                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <AlternatingItemStyle BackColor="White" />
                                <ItemStyle BackColor="#EFF3FB" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:BoundColumn DataField="AccomodationId" HeaderText="Accomodation Id" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SeasonStartDate" HeaderText="Start Date" Visible="True" DataFormatString="{0:dd-MMM-yyyy}">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SeasonEndDate" HeaderText="End Date" Visible="True" DataFormatString="{0:dd-MMM-yyyy}">
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="Select" HeaderText="[...]" Text="Edit"></asp:ButtonColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                        <table id="inputsection" class="inputsection">
                            <tr>
                                <td class="labelcell">Accomodation Name:</td>
                                <td class="inputcell">
                                <asp:TextBox ID="txtAccomodationName" runat="server" CssClass="input" 
                                        Enabled ="false" Width="218px"></asp:TextBox>
                                    </td>
                            </tr>
                            <tr>
                                <td class="labelcell">
                                    Season Start Date:
                                </td>
                                <td class="inputcell">
                                    <asp:TextBox ID="txtSeasonStartDate" runat="server" CssClass="input" 
                                        TabIndex="6" Width="95px"></asp:TextBox>
                                    <input ID="btnStartDate" class="datebutton" name="btnStartDate" 
                                        onclick="return setupCalendar('txtSeasonStartDate','btnStartDate')" 
                                        onfocus="return setupCalendar('txtSeasonStartDate','btnStartDate')" 
                                        tabindex="7" type="button" value="..." />
                                </td>
                            </tr>
                            <tr>
                                <td class="labelcell">
                                    Season End Date:
                                </td>
                                <td class="inputcell">
                                    <asp:TextBox CssClass="input" ID="txtSeasonEndDate" runat="server" Width="95px" TabIndex="8"></asp:TextBox><input
                                        id="btnEndDate" class="datebutton" name="btnEndDate" onclick="return setupCalendar('txtSeasonEndDate','btnEndDate')"
                                        onfocus="return setupCalendar('txtSeasonEndDate','btnEndDate')" type="button"
                                        value="..." tabindex="9" />
                                </td>
                            </tr>
                        </table>
                        <table id="buttonsection" class="buttonsection">
                            <tr>
                                <td>
                                    <asp:Button CssClass="appbutton" ID="btnEdit" runat="server" Height="24px" OnClick="btnEdit_Click"
                                        Text="Update" Width="65px" TabIndex="10" />
                                </td>
                                <td>
                                    <asp:Button CssClass="appbutton" ID="btnDelete" runat="server" Height="24px" OnClick="btnDelete_Click"
                                        Text="Delete" Width="65px" TabIndex="11" />
                                </td>
                                <td>
                                    <asp:Button CssClass="appbutton" ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                        Text="Cancel" Width="65px" TabIndex="12" />
                                </td>
                            </tr>
                        </table>
                        <table id="statussection" class="statussection">
                            <tr>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table id="hiddensection" class="hiddensection">
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hfId" runat="server" />
                                    <asp:HiddenField ID="hfOldSeasonStartDate" runat="server" />
                                    <asp:HiddenField ID="hfOldSeasonEndDate" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button CssClass="appbutton" ID="btnAddNew" runat="server" Height="24px" OnClick="btnAddNew_Click"
                                        Text="New" Visible="False" Width="65px" />
                                </td>
                                <td>
                                    <asp:Button CssClass="appbutton" ID="btnSave" runat="server" Height="24px" OnClick="btnSave_Click"
                                        Text="Save" Visible="False" Width="65px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="clear: both">
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
