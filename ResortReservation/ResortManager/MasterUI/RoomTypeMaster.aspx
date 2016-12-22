<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoomTypeMaster.aspx.cs" Inherits="MasterUI_RoomTypeMaster" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Room Type Master</title>
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />    
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />    
    <script language="javascript" type="text/javascript" src="../js/master/roomtypemaster.js"></script>    
</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="Region Master" />
    <div>
    <asp:ScriptManager ID="scmgrRoomType" runat="server" />
    <asp:UpdatePanel ID="pnlRoomType" runat="server">
        <ContentTemplate>        
        <div id="gridsection" class="gridsection">
            <asp:DataGrid ID="dgRoomType" runat="server" AutoGenerateColumns="False" CellPadding="4"
                DataKeyField="RoomTypeId" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="dgRoomType_SelectedIndexChanged" Width="571px" BorderStyle="Ridge">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditItemStyle BackColor="#2461BF" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle BackColor="#EFF3FB" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundColumn DataField="RoomType" HeaderText="Room Type"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DefaultNoOfBeds" HeaderText="Default No Of Beds">
                    </asp:BoundColumn>
                    <asp:ButtonColumn CommandName="Select" HeaderText="[...]" Text="Edit"></asp:ButtonColumn>
                    <asp:ButtonColumn CommandName="Delete" HeaderText="[...]" Text="Delete" Visible="False"></asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <table id="inputsection" class="inputsection">
            <tr>
            <td style="width: 135px"> Room Type :</td>
                <td style="width: 73px">
                    <asp:TextBox cssclass="input"  ID="txtRoomType" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 135px">
                    Default No. of Beds:</td>
                <td style="width: 73px">
                    <asp:DropDownList cssclass="select" ID="ddlDefaultNoOfBeds" runat="server" Width="114px">
                    </asp:DropDownList></td>
            </tr>
        </table>
        <table id="buttonsection" class="buttonsection">
            <tr>
                <td style="width: 74px; height: 26px;">
                    <asp:Button cssclass="appbutton" ID="btnEdit" runat="server" Height="24px" OnClick="btnEdit_Click" Text="Update"
                        Width="65px" /></td>
                <td style="width: 74px; height: 26px;">
                    <asp:Button cssclass="appbutton" ID="btnDelete" runat="server" Height="24px" OnClick="btnDelete_Click"
                        Text="Delete" Width="65px" /></td>
                <td style="width: 74px; height: 26px;">
                    <asp:Button cssclass="appbutton" ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                        Width="65px" /></td>
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
                    <td><asp:HiddenField ID="hfId" runat="server" /></td>
            </tr>
        </table>
        <table><tr><td>
            <asp:Button cssclass="appbutton" ID="btnAddNew" runat="server" Height="24px" OnClick="btnAddNew_Click"
                Text="New" Width="65px" Visible="False" /></td>
                <td>
            <asp:Button cssclass="appbutton" ID="btnSave" runat="server" Height="24px" OnClick="btnSave_Click" Text="Save"
                Width="65px" Visible="False" />
                </td></tr>
        </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    </div>
    </form>
</body>
</html>
