<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MapAgentstoMarket.aspx.cs" Inherits="Rate_MapAgentstoMarket" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>Room Category Master</title>
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <script language="javascript" type="text/javascript" src="../js/master/roomcategorymaster.js"></script>
    <style type="text/css">
        .auto-style1
        {
            width: 128px;
            height: 23px;
        }

        .auto-style2
        {
            height: 23px;
        }
    </style>
</head>
<body>
   <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Map Agent To Market" />
        <br />
        <div>
            <asp:ScriptManager ID="scmgrLinkRcategoryToMarket" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="pnlLinkRcategoryToMarket" runat="server">
                <ContentTemplate>
                    <div id="gridsection" class="gridsection">
                        <asp:GridView ID="gridAgents" runat="server" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="Both" Width="485px" AllowPaging="True" OnPageIndexChanging="gridAgents_PageIndexChanging" PageSize="20" OnRowDataBound="gridAgents_RowDataBound" >
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Category">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnfAgentId" Value='<%#Eval("AgentId") %>' runat="server" />
                                        <asp:Label runat="server" ID="lbRateName" Text='<%#Eval("AgentName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbStatus" AutoPostBack="true"    runat="server" OnCheckedChanged="cbStatus_CheckedChanged"  />
                                        <asp:Label runat="server" ID="lbStatus" Text=""></asp:Label>
                                        <asp:HiddenField  ID="hdnfStatus" runat="server"  Value='<%#Eval("cnt") %>'/>
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
                    <asp:Panel runat="server" ID="pnlDd">
                        <table>
                            <tr>
                                <td>Select Market
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMarket" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMarket_SelectedIndexChanged" ></asp:DropDownList>
                                </td>

                            </tr>
                        </table>

                    </asp:Panel>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
