<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BannerMaster.aspx.cs" Inherits="MasterUI_BannerMaster" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <title>Banner Master</title>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Banner Master" />
        <div style="padding-left: 39%; margin-bottom: 4%; margin-top: 2%;">
            <table>
                <tr>
                    <td>Title</td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox></td>

                </tr>
                <tr>
                    <td>Upload Image</td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" /></td>
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
        <div style="width: 50%; padding-left: 36%;">
            <asp:GridView ID="gvBanner" Width="50%" runat="server" DataKeyNames="Id" AutoGenerateColumns="false"
                RowStyle-Height="35px" BackColor="LightGray" HeaderStyle-BackColor="LightSeaGreen"
                HeaderStyle-Font-Italic="true" RowStyle-BorderStyle="Double" Visible="true" AllowPaging="true" OnRowCommand="gvBanner_RowCommand" OnRowDeleting="gvBanner_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                    <asp:TemplateField HeaderText="Images">
                        <ItemTemplate>
                            <img src='../Cruise/Booking/<%#Eval("Image") %>' width="50" height="50" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="lblDelete" CommandName="Delete" OnClientClick="return confrm();"
                                CommandArgument='<%# Eval("Id")%>' runat="server" Text="Delete"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
