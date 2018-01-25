<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgentContractingmaster.aspx.cs" Inherits="MasterUI_AgentContractingmaster" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agent Contracting Master</title>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-en.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-setup.js"></script>
    <script language="javascript" type="text/javascript" src="../js/global.js"></script>
    <script language="javascript" type="text/javascript" src="../js/client/viewbookings.js"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/viewbookings.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/calendar-blue2.css" title="win2k-cold-1" />
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Agent Contratcting Master" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="width: 75%;" align="center">

            <asp:UpdatePanel ID="pnlTouristCount" runat="server">
                <ContentTemplate>
                    <fieldset id="Fieldset3" style="width: 80%">
                        <legend>Agent Contracting List</legend>
                        <asp:GridView ID="dgTouristCount" AllowPaging="True" PageSize="15" Style="width: inherit" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" HeaderStyle-HorizontalAlign="Left" Width="60%" CellSpacing="10">

                            <AlternatingRowStyle BackColor="White" />
                            <RowStyle HorizontalAlign="Left" />


                            <Columns>
                                <asp:BoundField DataField="CustId" HeaderText="Customer Id" Visible="false" />
                                <asp:BoundField DataField="Title" HeaderText="Title" />
                                <asp:BoundField DataField="FirstName" HeaderText="FirstName" />
                                <asp:BoundField DataField="LastName" HeaderText="LastName" />
                                <asp:BoundField DataField="Email" HeaderText="Email" />
                                <asp:BoundField DataField="Telephone" HeaderText="Contact Number" />
                                <asp:BoundField DataField="Address1" HeaderText="Address 1" />
                                <asp:BoundField DataField="Address2" HeaderText="Address 2" />
                                <asp:BoundField DataField="City" HeaderText="City" />
                                <asp:BoundField DataField="State" HeaderText="State" />
                                <asp:BoundField DataField="PostalCode" HeaderText="Postal Code" />
                                <asp:BoundField DataField="CountryName" HeaderText="Country Name" />
                                <asp:BoundField DataField="Password" HeaderText="Password" Visible="false" />
                                <asp:BoundField DataField="PaymentMethod" HeaderText="PaymentMethod" />
                                <asp:BoundField DataField="PostalCode" HeaderText="Postal Code" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Select">Edit</asp:LinkButton>
                                        <%--<asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you certain you want to delete this Rate Card?') ">Delete</asp:LinkButton>--%>
                                    </ItemTemplate>

                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />

                        </asp:GridView>

                    </fieldset>
                    <br />

                    <br />
                    <div style="margin-top: 1%; margin-left: 2%;">
                        <table>
                            <tr>
                                <td>Select Agent</td>
                                <td>
                                    <asp:DropDownList ID="ddlAgent" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAgent_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="height: 3px;"></td>
                            </tr>
                            <tr>
                                <td>Cancellation Policy</td>
                                <td>
                                    <asp:TextBox ID="txtCancel" Width=" 98%"
                                        Height=" 80px" TextMode="MultiLine" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="height: 3px;"></td>
                            </tr>
                            <tr>
                                <td>booking payment policy </td>
                                <td>
                                    <asp:TextBox ID="txtBookingpaymentPolicy" Width=" 98%"
                                        Height=" 80px" TextMode="MultiLine" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="height: 3px;"></td>
                            </tr>

                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" /></td>
                                <td>
                                    <%-- <asp:Button ID="btnDelete" runat="server" Text="Delete" />--%></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
