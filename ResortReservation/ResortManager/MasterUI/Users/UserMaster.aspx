<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserMaster.aspx.cs" Inherits="MasterUI_Users_UserMaster" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../../style.css" />
    <title>User Master</title>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="User Master" />
        <div>
            <asp:ScriptManager ID="scmgrUser" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="pnlUserMaster" runat="server">
                <ContentTemplate>
                    <div id="gridsection" class="gridsection">
                        <asp:DataGrid ID="dgUsers" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            DataKeyField="UserId" ForeColor="#333333" GridLines="None" OnDeleteCommand="dgUsers_DeleteCommand"
                            OnSelectedIndexChanged="dgUsers_SelectedIndexChanged" Width="587px" BorderStyle="Ridge">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditItemStyle BackColor="#2461BF" />
                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#EFF3FB" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundColumn DataField="UserId" HeaderText="User Id"></asp:BoundColumn>
                                <asp:BoundColumn DataField="UserName" HeaderText="User Name"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Active" HeaderText="Active"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RoleIdForDisplay" HeaderText="User Role Id" Visible="false">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RoleNameForDisplay" HeaderText="User Role Name"></asp:BoundColumn>
                                <asp:ButtonColumn CommandName="Select" HeaderText="[...]" Text="Edit"></asp:ButtonColumn>
                                <asp:ButtonColumn CommandName="Delete" HeaderText="[...]" Text="Delete" Visible="False">
                                </asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                    <div id="leftCol" style="float: left;">
                        <table id="inputsection" class="inputsection">
                            <tr>
                                <td>
                                    User Id</td>
                                <td style="width: 106px">
                                    <asp:TextBox ID="txtUserId" runat="server" CssClass="input" MaxLength="10" Width="156px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    User Name</td>
                                <td style="width: 106px">
                                    <asp:TextBox CssClass="input" ID="txtUserName" runat="server" MaxLength="50" Width="156px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    Password</td>
                                <td style="width: 106px">
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="input" MaxLength="10" Width="156px"
                                        TextMode="Password"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    Active</td>
                                <td style="width: 106px">
                                    <asp:CheckBox ID="chkActive" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>
                                    User Role</td>
                                <td style="width: 106px">
                                    <asp:DropDownList ID="ddlUserRoleList" runat="server" Width="167px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>
                                    User Email Id</td>
                                <td style="width: 106px">
                                    <asp:TextBox CssClass="input" ID="txtUserEmailId" runat="server" MaxLength="50" Width="156px" AutoCompleteType="Email"></asp:TextBox></td>
                            </tr>
                        </table>
                        <table id="buttonsection" class="buttonsection">
                            <tr>
                                <td style="width: 74px; height: 26px">
                                    <asp:Button CssClass="appbutton" ID="btnEdit" runat="server" Height="24px" OnClick="btnEdit_Click"
                                        Text="Update" Width="65px" /></td>
                                <td style="width: 74px; height: 26px">
                                    <asp:Button CssClass="appbutton" ID="btnDelete" runat="server" Height="24px" OnClick="btnDelete_Click"
                                        Text="Delete" Width="65px" /></td>
                                <td style="width: 74px; height: 26px">
                                    <asp:Button CssClass="appbutton" ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                        Text="Cancel" Width="65px" Visible="False" /></td>
                            </tr>
                        </table>
                        <table id="statussection" class="statussection">
                            <tr>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label></td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button CssClass="appbutton" ID="btnAddNew" runat="server" Height="24px" OnClick="btnAddNew_Click"
                                        Text="New" Width="65px" Visible="False" />
                                    <asp:Button CssClass="appbutton" ID="btnSave" runat="server" Height="24px" OnClick="btnSave_Click"
                                        Text="Save" Width="65px" Visible="False" />
                                </td>
                            </tr>
                        </table>
                        <table id="hiddensection" class="hiddensection">
                            <tr>
                                <td style="width: 106px">
                                    <asp:HiddenField ID="hfId" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                    </div>
                    <div style="clear: both;" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
