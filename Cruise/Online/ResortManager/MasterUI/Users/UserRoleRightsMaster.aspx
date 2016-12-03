<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserRoleRightsMaster.aspx.cs"
    Inherits="MasterUI_Users_UserRoleRightsMaster" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../../style.css" />
    <link href="../../css/roleRights.css" rel="stylesheet" type="text/css" />
    <title>User Role Wise Rights</title>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="User Role Wise Rights" />
        <div>
            <asp:ScriptManager ID="scmgrRole" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="pnlRoleRightsAssignment" runat="server">
                <ContentTemplate>
                    <div runat="server" id="pnlWorkingArea">
                        <div id="pnlLeftCol" class="roleListParent">
                            <div class="headerLabel">
                                <asp:Label ID="lblRoleList" runat="server" Text="Role List"></asp:Label>
                            </div>
                            <asp:ListBox runat="server" ID="lstUserRoles" CssClass="roleList" OnSelectedIndexChanged="lstUserRoles_SelectedIndexChanged" AutoPostBack="True">
                            </asp:ListBox>
                        </div>
                        <div id="pnlRightCol" class="rightListParent">
                            <div class="headerLabel">
                                <asp:Label ID="lblRightsList" runat="server" Text="Rights List"></asp:Label>
                            </div>
                            <asp:TreeView ID="tvRights" CssClass="rightList" runat="server" ExpandDepth="2" ShowCheckBoxes="Leaf"
                                ImageSet="Msdn" NodeIndent="30">
                                <ParentNodeStyle />
                                <HoverNodeStyle Font-Underline="True" BackColor="#CCCCCC" BorderColor="#888888" />
                                <SelectedNodeStyle Font-Underline="False" HorizontalPadding="3px" VerticalPadding="1px"
                                    BackColor="White" BorderColor="#888888" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Bold="True" />
                                <NodeStyle HorizontalPadding="5px" NodeSpacing="1px" VerticalPadding="2px" />
                            </asp:TreeView>
                            <table id="Table1" class="buttonsection2">
                                <tr>
                                    <td style="width: 74px; height: 26px">
                                        <asp:Button CssClass="appbutton" ID="btnSelectAll" runat="server" Height="24px" Text="Select All"
                                            Width="65px" OnClick="btnSelectAll_Click" /></td>
                                    <td style="width: 74px; height: 26px">
                                        <asp:Button CssClass="appbutton" ID="btnDeSelectAll" runat="server" Text="Un-Select All"
                                            Width="65px" OnClick="btnDeSelectAll_Click" /></td>
                                </tr>
                            </table>
                        </div>
                        <%--DataKeyField="UserId" --%>
                        <div id="pnlRightColExtreme" class="extremeRightListParent">
                            <div class="headerLabel">
                                <asp:Label ID="lblCurrentRights" runat="server" Text="Current Rights"></asp:Label>
                            </div>
                            <div id="gridsection" class="gridsection2">
                                <asp:DataGrid ID="dgRoleRights" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="None" Width="450px" BorderStyle="solid" BorderColor="gray"
                                    BorderWidth="1px">
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <EditItemStyle BackColor="#2461BF" />
                                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <ItemStyle BackColor="#EFF3FB" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="RoleId" HeaderText="Role Id" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RoleName" HeaderText="Role Name"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ScreenName" HeaderText="Screen Name"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RightName" HeaderText="Right Name"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RightKey" HeaderText="Right Key" Visible="false"></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </div>
                        <div style="clear: both;" />
                    </div>
                    <table id="buttonsection" class="buttonsection2">
                        <tr>
                            <td style="width: 74px; height: 26px">
                                <asp:Button CssClass="appbutton" ID="btnRoleRightsAssignment" runat="server" Height="24px"
                                    Text="Save" Width="65px" OnClick="btnRoleRightsAssignment_Click" /></td>
                            <td style="width: 74px; height: 26px">
                                <asp:Button CssClass="appbutton" ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                    Text="Cancel" Width="65px" /></td>
                        </tr>
                    </table>
                    <table id="statussection" class="statussection">
                        <tr>
                            <td>
                                <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
