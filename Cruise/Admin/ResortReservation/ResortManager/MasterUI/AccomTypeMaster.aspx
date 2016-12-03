<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccomTypeMaster.aspx.cs" Inherits="MasterUI_AccomTypeMaster" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" /> 
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" /> 
    <title>Accomodation Type Master</title>
</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="Accomodation Type Master" />
    <div>                
        <asp:ScriptManager ID="scmgrAccomTypeMaster" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="pnlAccomTypeMaster" runat="server">
        <ContentTemplate>     
        <div id="gridsection" class="gridsection">
            <asp:DataGrid ID="dgAccomodationType" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="None" Width="346px" DataKeyField="AccomodationTypeID" OnSelectedIndexChanged="dgAccomodationType_SelectedIndexChanged" BorderStyle="Ridge">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditItemStyle BackColor="#2461BF" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle BackColor="#EFF3FB" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundColumn DataField="AccomodationType" HeaderText="Accomodation Type Name">
                    </asp:BoundColumn>
                    <asp:ButtonColumn CommandName="Select" HeaderText="[...]" Text="Edit"></asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <table id="inputsection" class="inputsection">
        <tr>
            <td style="height: 15px; font-size: small;">
                Accomodation Type:</td>
            <td style="width: 62px; height: 15px">
                <asp:TextBox cssclass="input"  ID="txtAccomType" runat="server" MaxLength="50"></asp:TextBox></td>            
        </tr>            
        </table>        
        <table id="buttonsection" class="buttonsection">
        <tr>
                <td>
                    <asp:Button cssclass="appbutton" ID="btnEdit" runat="server" Text="Update" Width="65px" OnClick="btnEdit_Click" /></td>
                <td>
                    <asp:Button cssclass="appbutton" ID="btnDelete" runat="server" Text="Delete" Width="65px" OnClick="btnDelete_Click" /></td>
                <td>
                    <asp:Button cssclass="appbutton" ID="btnCancel" runat="server" Text="Cancel" Width="65px" OnClick="btnCancel_Click" /></td>
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
                </td>
        </tr>
        </table>
        <table>
        <tr>
            <td>
            <asp:Button cssclass="appbutton" ID="btnAddNew" runat="server" Height="24px" Text="New" Width="65px" OnClick="btnAddNew_Click" Visible="False" />
            <asp:Button cssclass="appbutton" ID="btnSave" runat="server" Height="24px" Text="Save" Width="65px" OnClick="btnSave_Click" Visible="False" />
            </td>
        </tr>
        </table>        
   </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
