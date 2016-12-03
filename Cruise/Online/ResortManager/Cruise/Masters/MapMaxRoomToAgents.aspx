<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MapMaxRoomToAgents.aspx.cs" Inherits="Cruise_Masters_MapMaxRoomToAgents" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../../style.css" />
    <link href="../../css/roleRights.css" rel="stylesheet" type="text/css" />
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
            <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>
                    <br />
                    <div id="ddSection">
                        <table id="tblDd">
                            <tr>
                                <td>&nbsp;&nbsp; Agent
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlAgents" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlAgents_SelectedIndexChanged"></asp:DropDownList>
                                </td>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>

