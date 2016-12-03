<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EventMessageMaster.aspx.cs" Inherits="MasterUI_EventMessageMaster" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" /> 
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" /> 
    <title>Event Message Master</title>    
</head>
<body>
    <form id="form1" runat="server">    
    <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="Event Message Master" />
        <div>
            <asp:ScriptManager ID="scmgrEventMessageMaster" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="pnlEventMessageMaster" runat="server">
                <ContentTemplate>                
        <div id="gridsection" class="gridsection">
           <asp:DataGrid ID="dgEventMessages" runat="server" AutoGenerateColumns="False" BorderStyle="Ridge"
                CellPadding="4" DataKeyField="MessageId" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="dgEventMessages_SelectedIndexChanged"
                Width="650px" TabIndex="3">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditItemStyle BackColor="#2461BF" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle BackColor="#EFF3FB" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundColumn DataField="MessageId" HeaderText="Message Id" Visible="False">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="EventName" HeaderText="Event Name"></asp:BoundColumn>
                    <asp:BoundColumn DataField="EventMessage" HeaderText="Event Message" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="EventSubject" HeaderText="Event Subject" Visible="false"></asp:BoundColumn>                    
                    <asp:ButtonColumn CommandName="Select" HeaderText="[...]" Text="Edit"></asp:ButtonColumn>
                </Columns>
            </asp:DataGrid> 
        </div>
        <table id="inputsection" class="inputsection">            
        <tr>
            <td class="labelcell">
                Event Name:</td>
            <td class="inputcell">
                <asp:TextBox cssclass="input"  ID="txtEventName" runat="server" MaxLength="50" Width="260px" TabIndex="4"></asp:TextBox></td>                
        </tr>
        <tr>
            <td class="labelcell">
                Event Message:</td>
            <td class="inputcell">
                <asp:TextBox cssclass="input"  ID="txtEventMesssage" runat="server" MaxLength="4000" TabIndex="5" Width="551px" Height="140px" TextMode="MultiLine"></asp:TextBox></td>                
        </tr>
        <tr>
            <td class="labelcell">
                Event Subject:</td>
            <td class="inputcell">
                <asp:TextBox cssclass="input"  ID="txtEventSubject" runat="server" Width="553px" TabIndex="6" MaxLength="100"></asp:TextBox></td>                                  
        </tr>
        <tr>
            <td class="labelcell" style="height: 20px">
                Allow Mails:</td>
            <td class="inputcell" style="height: 20px">
                <asp:CheckBox ID="chkAllowMails" runat="server" /></td> 
        </tr>
        </table>
        <table id="buttonsection" class="buttonsection">
            <tr>
                <td>
                    <asp:Button cssclass="appbutton" ID="btnEdit" runat="server" Height="24px" OnClick="btnEdit_Click" Text="Update"
                        Width="65px" TabIndex="10" /></td>
                <td>
                    <asp:Button cssclass="appbutton" ID="btnDelete" runat="server" Height="24px" OnClick="btnDelete_Click"
                        Text="Delete" Width="65px" TabIndex="11" Visible="False" /></td>
                <td>
                    <asp:Button cssclass="appbutton" ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                        Width="65px" TabIndex="12" /></td>
            </tr>
        </table>
        <table id="statussection" class="statussection">
        <tr>
        <td>
        <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
        </table>
        <table id="hiddensection" class="hiddensection">
        <tr>
        <td>
        <asp:HiddenField ID="hfId" runat="server" />
        </td></tr></table>
        <table>
        <tr>
            <td>
                <asp:Button cssclass="appbutton" ID="btnAddNew" runat="server" Height="24px" OnClick="btnAddNew_Click"
                    Text="New" Visible="False" Width="65px" /></td>
            <td>
                <asp:Button cssclass="appbutton" ID="btnSave" runat="server" Height="24px" OnClick="btnSave_Click" Text="Save"
                    Visible="False" Width="65px" /></td>
        </tr>
        </table>        
        </ContentTemplate>
        </asp:UpdatePanel>
        </div>        
    </form>
</body>
</html>
