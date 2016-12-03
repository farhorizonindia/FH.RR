<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserAgentMaster.aspx.cs" Inherits="MasterUI_Users_UserAgentMaster" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../../style.css" />
    <link href="../../css/roleRights.css" rel="stylesheet" type="text/css" />
    <title>User Agent Mapper</title>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="User Agent Mapper" />
        <div>
            <asp:ScriptManager ID="scmgrUserAgent" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="pnlUserAgentMapper" runat="server">
                <ContentTemplate>
                    <div runat="server" id="pnlWorkingArea">
                        <div id="pnlLeftCol" class="roleListParent">
                            <div class="headerLabel">
                                <asp:Label ID="lblUserList" runat="server" Text="User List"></asp:Label>
                            </div>
                            <asp:ListBox runat="server" ID="lstUsers" CssClass="roleList" OnSelectedIndexChanged="lstUsers_SelectedIndexChanged" AutoPostBack="True">
                            </asp:ListBox>
                        </div>
                        <div id="pnlRightCol" class="rightListParent">
                            <div class="headerLabel">
                                <asp:Label ID="lblUnHookedAgentList" runat="server" Text="UnAssigned Agent List"></asp:Label>
                                <asp:Label ID="Label1" runat="server" Font-Size="Smaller" Text="(Hold ctrl to select multiple items)"></asp:Label></div>
                            <asp:ListBox ID="lstUnHookedAgents" CssClass="rightList" runat="server" SelectionMode="Multiple"/>                            
                            <table id="Table1" class="buttonsection2">
                                <tr>
                                    <td style="width: 74px; height: 26px">
                                        <asp:Button CssClass="appbutton" ID="btnAssignAgents" runat="server" Height="24px" Text="Assign Agent"
                                            Width="96px" OnClick="btnAssignAgents_Click" /></td>
                                </tr>
                            </table>
                        </div>
                        <%--DataKeyField="UserId" --%>
                        <div id="pnlRightColExtreme" class="extremeRightListParent">
                            <div class="headerLabel">
                                <asp:Label ID="lblHookedAgents" runat="server" Text="Assigned Agent List"></asp:Label>
                                <asp:Label ID="Label2" runat="server" Font-Size="Smaller" Text="(Hold ctrl to select multiple items)"></asp:Label></div>
                            <asp:ListBox ID="lstHookedAgents" CssClass="rightList" runat="server" SelectionMode="Multiple"/>                            
                            <table id="Table2" class="buttonsection2">
                                <tr>
                                    <td style="width: 74px; height: 26px">
                                        <asp:Button CssClass="appbutton" ID="btnUnAssignAgent" runat="server" Height="24px" Text="Un Assign Agent"
                                            Width="96px" OnClick="btnUnAssignAgent_Click" /></td>
                                </tr>
                            </table>
                        </div>
                        <div style="clear: both;" />
                    </div>
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
