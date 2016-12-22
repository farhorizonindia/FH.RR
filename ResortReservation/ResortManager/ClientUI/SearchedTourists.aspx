<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchedTourists.aspx.cs" Inherits="ClientUI_SearchedTourists" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />    
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />    
    <title>Untitled Page</title>    
</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="Tourist List" />
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 432px; height: 35px;">
            <tr>
                <td style="height:10px; width: 400px;">Toursits matching your search criteria are
                </td>
            </tr>
            <tr>
                <td style="font-family:Verdana; font-size:small; width: 400px;">
                    <asp:DataGrid ID="dgTouristDetails" runat="server" DataKeyField="TouristNo" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" Width="430px" AllowPaging="True" OnSelectedIndexChanged="dgTouristDetails_SelectedIndexChanged" PageSize="20">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditItemStyle BackColor="#2461BF" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EFF3FB" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="FirstName" HeaderText="First Name"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LastName" HeaderText="Last Name"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PassportNo" HeaderText="Passport No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Nationality" HeaderText="Nationality"></asp:BoundColumn>
                            <asp:ButtonColumn CommandName="Select" HeaderText="[...]" Text="Choose"></asp:ButtonColumn>
                        </Columns>
                    </asp:DataGrid></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
