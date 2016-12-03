<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserRoleMaster.aspx.cs" Inherits="MasterUI_Users_UserRoleMaster" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../../css/pageheader.css" />    
    <link rel="stylesheet" type="text/css" media="all" href="../../style.css" />    
    <title>User Role Master</title>
</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="User Role Master" />
    <div>
    <asp:ScriptManager ID="scmgrUserRole" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="pnlUserRoleMaster" runat="server">
    <ContentTemplate>                        
    <div id="gridsection" class="gridsection">
            <asp:DataGrid ID="dgUserRoles" runat="server" AutoGenerateColumns="False" CellPadding="4"
                DataKeyField="UserRoleID" ForeColor="#333333" GridLines="None" OnDeleteCommand="dgUserRoles_DeleteCommand"
                OnSelectedIndexChanged="dgUserRoles_SelectedIndexChanged" Width="587px" BorderStyle="Ridge">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditItemStyle BackColor="#2461BF" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle BackColor="#EFF3FB" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundColumn DataField="UserRoleName" HeaderText="UserRole Name"></asp:BoundColumn>                    
                    <asp:ButtonColumn CommandName="Select" HeaderText="[...]" Text="Edit"></asp:ButtonColumn>
                    <asp:ButtonColumn CommandName="Delete" HeaderText="[...]" Text="Delete" Visible="False"></asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>
        </div>
    <table id="inputsection" class="inputsection">
        <tr>
            <td>
                User Role Name</td>
            <td style="width: 106px">
                <asp:TextBox cssclass="input"  ID="txtUserRoleName" runat="server" Width="100px"></asp:TextBox></td>            
        </tr>
        </table>        
    <table id="buttonsection" class="buttonsection">
        <tr>
            <td style="width: 74px; height: 26px">
                <asp:Button cssclass="appbutton" ID="btnEdit" runat="server" Height="24px" OnClick="btnEdit_Click" Text="Update"
                    Width="65px" /></td>
            <td style="width: 74px; height: 26px">
                <asp:Button cssclass="appbutton" ID="btnDelete" runat="server" Height="24px" OnClick="btnDelete_Click"
                    Text="Delete" Width="65px" /></td>
            <td style="width: 74px; height: 26px">
                <asp:Button cssclass="appbutton" ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                    Width="65px" Visible="False" /></td>            
        </tr>
    </table>    
    <table id="statussection" class="statussection">
    <tr>
        <td>
            <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>
    </table>
    <table>
    <tr><td>
                            <asp:Button cssclass="appbutton" ID="btnAddNew" runat="server" Height="24px" OnClick="btnAddNew_Click"
                                Text="New" Width="65px" Visible="False" />
                            <asp:Button cssclass="appbutton" ID="btnSave" runat="server" Height="24px" OnClick="btnSave_Click" Text="Save"
                                Width="65px" Visible="False" />
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
    </ContentTemplate>
    </asp:UpdatePanel>        
    </div>
    </form>
</body>
</html>
