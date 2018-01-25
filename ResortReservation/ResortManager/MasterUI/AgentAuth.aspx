<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgentAuth.aspx.cs" Inherits="MasterUI_AgentAuth" %>


<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    
    <title>Agent Auth</title>

</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Agent Auth" />
        <div>
            <asp:ScriptManager ID="scmgrAccomMaster" runat="server">
            </asp:ScriptManager>
            <%--  <asp:UpdatePanel ID="pnlAccomMaster" runat="server">--%>
            <%-- <ContentTemplate>--%>
            <table id="filtersection" class="filtersection">
                <tr>
                    <td class="labelcell">Agents:
                    </td>
                    <td>
                        <asp:DropDownList CssClass="select dropdown" ID="ddlAgent" runat="server" Width="215px">
                        </asp:DropDownList>
                    </td>


                </tr>
                <tr>
                    <td style="width: 15px">
                        <asp:Button CssClass="appbutton" ID="btnGenarateToken" runat="server" OnClick="btnGenarateToken_Click"
                            Text="Genarate Token" Width="100px" TabIndex="2" />
                    </td>
                    <td>
                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>


            <%--</ContentTemplate>--%>
            <%--</asp:UpdatePanel>--%>
        </div>
        <div>
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="gvTokenlist" runat="server" AutoGenerateColumns="false" DataKeyNames="" OnPageIndexChanging="gvTokenlist_PageIndexChanging" OnRowDataBound="gvTokenlist_RowDataBound" AllowPaging="true">



                            <Columns>
                                <asp:TemplateField HeaderText="AgentId">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAgentId" Enabled="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.AgentId") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Agent Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAgentName" Enabled="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.AgentName") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TokenNo">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTokenNo" Enabled="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.TokenNo") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div style="width: 300px; color: #FF0000; display: block; height: 20px; text-align: center;">
                                    <b>No record found.</b>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
