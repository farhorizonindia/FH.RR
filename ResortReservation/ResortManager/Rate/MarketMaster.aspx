<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MarketMaster.aspx.cs" Inherits="Rate_MarketMaster" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Room Category Master</title>
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <script language="javascript" type="text/javascript" src="../js/master/roomcategorymaster.js"></script>
    <style type="text/css">
        /*.auto-style1
        {
            width: 128px;
            height: 23px;
        }

        .auto-style2
        {
            height: 23px;
        }*/
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Market Master" />
        <br />
        <div>
            <asp:ScriptManager ID="scmgrMarketMaster" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="pnlMarketMaster" runat="server">
                <ContentTemplate>
                    <div id="gridsection" class="gridsection">
                        <asp:GridView ID="GridMarkets" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridMarkets_RowCommand" DataKeyNames="MarketCode" OnRowDeleting="GridMarkets_RowDeleting" HeaderStyle-HorizontalAlign="Right" Width="350px">

                           
                            <AlternatingRowStyle BackColor="White" />
                            <RowStyle HorizontalAlign="Right" />
                           
                            <Columns>
                             
                                <asp:BoundField DataField="MarketCode" HeaderText="MarketCode" />
                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                <asp:BoundField DataField="Region" HeaderText="Region" />
                                <asp:BoundField DataField="specification" HeaderText="Specification" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Select">Edit</asp:LinkButton>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete"  OnClientClick="return confirm('Are you certain you want to delete this Market?') " >Delete</asp:LinkButton>
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
                    </div>
                    <table id="inputsection" class="inputsection">
                        <tr>
                            <td style="width: 128px">Name</td>
                            <td>
                                <asp:TextBox CssClass="input" ID="txtmarketName" runat="server" Width="127px" MaxLength="25"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqftxtmarketName" runat="server" ErrorMessage="*" ControlToValidate="txtmarketName" ValidationGroup="ValMarket"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 128px">Region</td>
                            <td>
                                <asp:TextBox ID="txtRegion" runat="server" CssClass="input"
                                    Width="127px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqftxtRegion" runat="server" ErrorMessage="*" ControlToValidate="txtRegion"  ValidationGroup="ValMarket"></asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Specification</td>
                            <td class="auto-style2">
                                <asp:TextBox ID="txtSpecification" runat="server" CssClass="input" Width="127px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqftxtSpecification" runat="server" ErrorMessage="*" ControlToValidate="txtSpecification"  ValidationGroup="ValMarket"></asp:RequiredFieldValidator>

                            </td>
                        </tr>
                    </table>
                    <table id="buttonsection" class="buttonsection">
                        <tr>
                            <td style="width: 74px; height: 26px">
                                <asp:Button CssClass="appbutton" ID="btnSbmit" runat="server"  ValidationGroup="ValMarket" Height="24px" Text="Submit"
                                    Width="65px" OnClick="btnSbmit_Click" /></td>
                            <td style="width: 74px; height: 26px">
                                <asp:Button CssClass="appbutton" ID="btnCancel" runat="server" Text="Cancel"
                                    Width="65px" OnClick="btnCancel_Click" /></td>
                        </tr>
                    </table>
                    <table id="statussection" class="statussection">
                        <tr>
                            <td>&nbsp;</td>
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
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </ContentTemplate>
              
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
