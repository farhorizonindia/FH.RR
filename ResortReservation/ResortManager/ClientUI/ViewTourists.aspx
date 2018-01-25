<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewTourists.aspx.cs" Inherits="ClientUI_ViewTourists" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/viewbookings.css" />
    <title>Tourist List</title>
</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Tourist List" />
    <div>
        <asp:ScriptManager ID="scmgrviewbookings" runat="server">
        </asp:ScriptManager>
        <asp:Label ID="lblTouristNotFound" runat="server"></asp:Label>
        <asp:UpdatePanel ID="pnlviewbookings" runat="server">
            <ContentTemplate>
                <div id="gridsection" class="gridsection">
                    <asp:DataGrid ID="dgTouristDetails" PageSize="30" runat="server" AutoGenerateColumns="False"
                        Width="936px" AllowPaging="True">
                        <Columns>
                            <asp:BoundColumn DataField="BookingId" HeaderText="Booking Code">
                                <ItemStyle CssClass="column" />
                                <HeaderStyle CssClass="columnHeader" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Touristno" HeaderText="Tourist No">
                                <ItemStyle CssClass="column" />
                                <HeaderStyle CssClass="columnHeader" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FirstName" HeaderText="First Name">
                                <ItemStyle CssClass="column" />
                                <HeaderStyle CssClass="columnHeader" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MiddleName" HeaderText="Middle Name">
                                <ItemStyle CssClass="column" />
                                <HeaderStyle CssClass="columnHeader" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="LastName" HeaderText="Last Name">
                                <ItemStyle CssClass="column" />
                                <HeaderStyle CssClass="columnHeader" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="View/Edit Details">
                                <ItemTemplate>
                                    <a href='<%# "touristDetails.aspx?op=edit&bid=" + DataBinder.Eval(Container.DataItem,"BookingID") + "&tno=" + DataBinder.Eval(Container.DataItem,"Touristno") %>'>
                                        Edit</a>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditItemStyle BackColor="#2461BF" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EFF3FB" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    </asp:DataGrid>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
