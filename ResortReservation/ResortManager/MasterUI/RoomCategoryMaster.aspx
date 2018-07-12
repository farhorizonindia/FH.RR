<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoomCategoryMaster.aspx.cs" Inherits="MasterUI_RoomCategoryMaster" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <title>Room Category Master</title>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Room Categoary Master" />
        <div>
            <asp:ScriptManager ID="scmgrNationality" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="pnlNationality" runat="server">
                <ContentTemplate>
                    <div id="gridsection" class="gridsection">
                        <asp:DataGrid ID="dgNationality" runat="server" AutoGenerateColumns="False" BorderStyle="Ridge"
                            CellPadding="4" DataKeyField="RoomCategoryId" ForeColor="#333333" GridLines="None"
                            OnSelectedIndexChanged="dgNationality_SelectedIndexChanged1" Width="391px" AllowPaging="True" OnPageIndexChanged="dgNationality_PageIndexChanged">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditItemStyle BackColor="#2461BF" />
                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#EFF3FB" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundColumn DataField="RoomCategory" HeaderText="Room Category"></asp:BoundColumn>
                                <asp:BoundColumn DataField="categoryAlias" HeaderText="Category Alias"></asp:BoundColumn>
                                <asp:ButtonColumn CommandName="Select" HeaderText="[...]" Text="Edit"></asp:ButtonColumn>
                                <asp:ButtonColumn CommandName="Delete" HeaderText="[...]" Text="Delete" Visible="False"></asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                    <table id="inputsection" class="inputsection">
                        <tr>
                            <td style="width: 32px">Room Category:</td>
                            <td style="width: 68px">
                                <asp:TextBox CssClass="input" ID="txtRoomcategory" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 32px">Category Alias:</td>
                            <td style="width: 68px">
                                <asp:TextBox CssClass="input" ID="txtCategoryAlias" runat="server"></asp:TextBox></td>
                        </tr>
                    </table>
                    <table id="buttonsection" class="buttonsection">
                        <tr>
                            <td style="width: 74px; height: 26px">
                                <asp:Button CssClass="appbutton" ID="btnEdit" runat="server" Height="24px" OnClick="btnEdit_Click" Text="Update"
                                    Width="65px" /></td>
                            <td style="width: 74px; height: 26px">
                                <asp:Button CssClass="appbutton" ID="btnDelete" runat="server" Height="24px" OnClick="btnDelete_Click"
                                    Text="Delete" Width="65px" /></td>
                            <td style="width: 74px; height: 26px">
                                <asp:Button CssClass="appbutton" ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                    Width="65px" Visible="False" /></td>
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
                                <asp:Button CssClass="appbutton" ID="btnAddNew" runat="server" Height="24px" OnClick="btnAddNew_Click"
                                    Text="New" Width="65px" Visible="False" /></td>
                            <td>
                                <asp:Button CssClass="appbutton" ID="btnSave" runat="server" Height="24px" OnClick="btnSave_Click" Text="Save"
                                    Width="65px" Visible="False" /></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
