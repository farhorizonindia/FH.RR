<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CommissionMaster.aspx.cs" Inherits="MasterUI_CommissionMaster" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <title>Commision Master</title>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Commision Master" />
        <div style="padding-left: 23%;">
            <table>
                <tr style="height: 2px;">
                    <td></td>
                </tr>
                <tr>
                    <td>Accom Type</td>
                    <td>
                        <asp:DropDownList ID="ddlAccomtype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAccomtype_SelectedIndexChanged"></asp:DropDownList></td>
                </tr>
                <tr style="height: 2px;">
                    <td></td>
                </tr>
                <tr>
                    <td>Accom Name</td>
                    <td>
                        <asp:DropDownList ID="ddlAccomname" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlAccomname_SelectedIndexChanged"></asp:DropDownList></td>
                </tr>
                <tr style="height: 2px;">
                    <td></td>
                </tr>
                <tr>
                    <td>Commision %</td>
                    <td>
                        <asp:TextBox Width="64%" ID="txtCommision" runat="server"></asp:TextBox></td>
                </tr>
                <tr style="height: 2px;">
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" Text=" "></asp:Label></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
