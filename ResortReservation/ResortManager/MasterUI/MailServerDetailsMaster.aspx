<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MailServerDetailsMaster.aspx.cs" Inherits="MasterUI_MailDetailsMaster" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />    
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />    
    <title>Mail Details Master</title>
</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="MailDetails Master" />
    <div>
    <asp:ScriptManager ID="scmgrMailDetails" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="pnlMailDetailsMaster" runat="server">
    <ContentTemplate>                                
    <div id="gridsection" class="gridsection">
        <asp:DataGrid ID="dgMailServerDetails" runat="server" AutoGenerateColumns="False" CellPadding="4"
        DataKeyField="SMTPId" ForeColor="#333333" OnDeleteCommand="dgMailDetailss_DeleteCommand"
        OnSelectedIndexChanged="dgMailDetailss_SelectedIndexChanged" Width="525px" BorderStyle="Ridge">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditItemStyle BackColor="#2461BF" />
        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <AlternatingItemStyle BackColor="White" />
        <ItemStyle BackColor="#EFF3FB" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:BoundColumn DataField="SMTPServer" HeaderText="SMTP Server"></asp:BoundColumn>
            <asp:BoundColumn DataField="Active" HeaderText="Active"></asp:BoundColumn>            
            <asp:ButtonColumn CommandName="Select" HeaderText="[...]" Text="Edit"></asp:ButtonColumn>
            <asp:ButtonColumn CommandName="Delete" HeaderText="[...]" Text="Delete" Visible="False"></asp:ButtonColumn>
        </Columns>
        </asp:DataGrid>
    </div>
    <table id="inputsection" class="inputsection">
    <tr>
        <td>
            SMTP Server</td>
        <td>
            <asp:TextBox cssclass="input"  ID="txtSMTPServer" runat="server" Width="190px"></asp:TextBox>
        </td>        
    </tr>
    <tr>
        <td>
            From Email Id</td>
        <td>
            <asp:TextBox cssclass="input"  ID="txtFromId" runat="server" Width="190px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            Reply To Email Id</td>
        <td>
            <asp:TextBox cssclass="input"  ID="txtReplyToId" runat="server" Width="190px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            From Display Name</td>
        <td>
            <asp:TextBox cssclass="input"  ID="txtFromDisplayName" runat="server" Width="190px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            User Id</td>
        <td>
            <asp:TextBox cssclass="input"  ID="txtUserId" runat="server" Width="190px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            Password</td>
        <td>
            <asp:TextBox cssclass="input"  ID="txtPassword" runat="server" TextMode="Password" Width="190px"></asp:TextBox></td>
    </tr>
        <tr>
        <td>
            Port</td>
        <td>
            <asp:TextBox cssclass="input"  ID="txtPort" runat="server" Width="190px"></asp:TextBox></td>
    </tr>
        <tr>
        <td>
            Active</td>
        <td>
            <asp:CheckBox cssclass="input"  ID="chkActive" runat="server" Width="106px"></asp:CheckBox></td>
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
                    Width="65px" /></td>            
        </tr>
    </table>   
    <table id="status" class="status">
        <tr>
            <td style="height: 18px">
                <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
    </table>
    <table id="hiddensection" class="hiddensection">
        <tr>
            <td style="width: 106px">
                <asp:HiddenField ID="hfId" runat="server" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
        <td>
        <asp:Button cssclass="appbutton" ID="btnAddNew" runat="server" Height="24px" OnClick="btnAddNew_Click"
        Text="New" Width="65px" Visible="False" />
        <asp:Button cssclass="appbutton" ID="btnSave" runat="server" Height="24px" OnClick="btnSave_Click" Text="Save"
        Width="65px" Visible="False" />
        </td>
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
        
    </div>
    </form>
</body>
</html>
