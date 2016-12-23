﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LocationMaster.aspx.cs" Inherits="Cruise_Masters_LocationMaster" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../../style.css" />
    <link href="../../css/roleRights.css" rel="stylesheet" type="text/css" />
    <title>Locations Master</title>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Locations Master" />

        <div>
            <asp:ScriptManager ID="scmgrLocation" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>
                    <div id="gridsection" class="gridsection">
                        <asp:GridView ID="gdvlocations" ForeColor="#333333" BorderStyle="Ridge" GridLines="None" runat="server" Width="524px" AutoGenerateColumns="False" CellSpacing="5" OnRowCommand="gdvlocations_RowCommand" OnRowDeleting="gdvlocations_RowDeleting" DataKeyNames="LocationId">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="White" />
                            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate><%#Container.DataItemIndex+1 %> </ItemTemplate>

                                </asp:TemplateField>
                                <asp:BoundField DataField="LocationName" HeaderText="LocationName" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Select">Edit</asp:LinkButton>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you certain you want to delete this Location?') ">Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </div>

                    <table>
                        <tr>
                            <td>Country</td>
                            <td>
                                <asp:DropDownList ID="ddlCountry" runat="server">
                                </asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td>Location Name

                            </td>
                            <td>
                                <asp:TextBox ID="txtLocation" runat="server"></asp:TextBox>

                            </td>

                        </tr>
                          <tr>
            <td>Description</td>
            <td style="width: 106px">
                <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>

                        <tr>
                            <td>
                                <asp:HiddenField ID="hfid" runat="server" />
                            </td>
                            <td>
                                <asp:Button CssClass="appbutton" ID="btnAdd" runat="server" Height="24px" Text="Add"
                                    Width="65px" OnClick="btnAdd_Click" /></td>

                        </tr>
                    </table>


                </ContentTemplate>

            </asp:UpdatePanel>

        </div>

    </form>
</body>
</html>
