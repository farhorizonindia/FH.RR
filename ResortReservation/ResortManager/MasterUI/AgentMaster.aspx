<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgentMaster.aspx.cs" Inherits="MasterUI_AgentMaster" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <title>Agent Master</title>
    <%--<style type="text/css">
        .auto-style1 {
            height: 23px;
        }

        .auto-style2 {
            width: 120px;
            height: 23px;
        }
    </style>--%>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Agent Master" />
        <div>
            <table>
                <tr>
                    <td>Agent Name:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlagent" runat="server"></asp:DropDownList>
                    </td>
                    <td>Email:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    </td>
                    <td>Country:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCountry" runat="server"></asp:DropDownList>
                    </td>
                    <td>On credits:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOncredits" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>Agent Type:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAgentType" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:ScriptManager ID="scmgrAgent" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="pnlAgentMaster" runat="server">
                <ContentTemplate>
                    <div id="gridsection" class="gridsection">
                        <div>
                            <asp:DataGrid ID="dgAgents" runat="server" CellPadding="4" AutoGenerateColumns="False"
                                DataKeyField="AgentID" ForeColor="#333333" Width="100%" GridLines="None" OnDeleteCommand="dgAgents_ItemCommand"
                                OnSelectedIndexChanged="dgAgents_SelectedIndexChanged" BorderStyle="Ridge" AllowPaging="True" PageSize="30" OnItemCommand="dgAgents_ItemCommand" OnPageIndexChanged="dgAgents_PageIndexChanged"
                                OnItemDataBound="dgAgents_ItemDataBound">

                                <Columns>
                                    <asp:BoundColumn DataField="AgentID" HeaderText="AgentID" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="AgentName" HeaderText="Agent Name"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="AgentCode" HeaderText="Agent Code"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="AgentEmailId" HeaderText="Email"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Category" HeaderText="Category"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Country" HeaderText="Country"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Phone" HeaderText="Phone No"></asp:BoundColumn>
                                    <%--<asp:BoundColumn DataField="BillingAddress" HeaderText="Billing Address"></asp:BoundColumn>--%>
                                    <asp:BoundColumn DataField="PaymentMethod" HeaderText="Payment Method"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="CreditLimit" HeaderText="Credit Limit"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Oncredits" HeaderText="Oncredits"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Commision" HeaderText="Commission"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="localagent" HeaderText="Agent Type"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="localagent" HeaderText="Market"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="RateCategory" HeaderText="Rate Category"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="MarketMappedto" HeaderText="Market Mapped"></asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="Select" HeaderText="[...]" Text="Edit"></asp:ButtonColumn>
                                    <asp:ButtonColumn CommandName="Delete" HeaderText="[...]" Text="Delete" Visible="False"></asp:ButtonColumn>
                                    <asp:ButtonColumn CommandName="MaptoRate" HeaderText="[...]" Text="Map"></asp:ButtonColumn>
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
                    </div>
                    <table id="inputsection" class="inputsection">
                        <tr>
                            <td>Agent Name</td>
                            <td style="width: 120px">
                                <asp:TextBox CssClass="input" ID="txtAgentName" Width="129%" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Local Agent</td>
                            <td style="width: 120px">
                                <asp:CheckBox ID="chklocal" runat="server" Style="margin-left: 3%;" />
                            </td>
                        </tr>
                        <tr>
                            <td>Agent Code</td>
                            <td style="width: 120px">
                                <asp:TextBox CssClass="input" ID="txtAgentCode" Width="129%" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Agent Email Id</td>
                            <td class="auto-style2">
                                <asp:TextBox CssClass="input" ID="txtAgentEmailId" Width="129%" runat="server" MaxLength="50" AutoCompleteType="Email"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Phone</td>
                            <td class="auto-style2">
                                <asp:TextBox ID="txtPhone" Width="129%" runat="server" Style="margin-left: 3%;" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqftxtBillingAddress0" runat="server" ControlToValidate="txtPhone" ErrorMessage="*" ForeColor="Red" ValidationGroup="valGroupRegister"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid" ControlToValidate="txtPhone" ValidationGroup="valGroupRegister" ValidationExpression="^([0-9]{8,15})$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Category</td>
                            <td class="auto-style2">
                                <asp:TextBox ID="txtCategory" Width="127%" Style="margin-left: 3%;" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqstcategory" runat="server" ControlToValidate="txtCategory" ErrorMessage="*" ForeColor="Red" ValidationGroup="valGroupRegister"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Country</td>
                            <td class="auto-style2">
                                <asp:TextBox ID="txtCountry" Width="127%" Style="margin-left: 3%;" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqcountry" runat="server" ControlToValidate="txtCountry" ErrorMessage="*" ForeColor="Red" ValidationGroup="valGroupRegister"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Password</td>
                            <td>
                                <asp:TextBox CssClass="input" Width="127%" ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
                            </td>

                        </tr>


                        <td>Billing Address</td>
                        <td>

                            <asp:TextBox ID="txtBillingAddress" Width="127%" Style="margin-left: 3%;" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqftxtBillingAddress" runat="server" ControlToValidate="txtBillingAddress" ErrorMessage="*" ForeColor="Red" ValidationGroup="valGroupRegister"></asp:RequiredFieldValidator>

                        </td>
                        <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Payment Method</td>
                            <td>
                                <asp:DropDownList ID="ddlpaymentMethod" Style="margin-left: 3%;" runat="server">
                                    <asp:ListItem>-Select-</asp:ListItem>
                                    <asp:ListItem>Online</asp:ListItem>
                                    <asp:ListItem>Offline</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqfddlpaymentMethod" runat="server" ControlToValidate="ddlpaymentMethod" InitialValue="-Select-" ErrorMessage="*" ForeColor="Red" ValidationGroup="valGroupRegister"></asp:RequiredFieldValidator>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>

                            <td>On Credit</td>
                            <td>

                                <asp:CheckBox ID="chkOnCredit" Style="margin-left: 3%;" runat="server" />

                            </td>
                            <td>&nbsp;</td>
                            <tr>

                                <td>Credit Limit</td>
                                <td>

                                    <asp:TextBox Width="127%" Style="margin-left: 3%;" ID="txtCreditLimit" runat="server"></asp:TextBox>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid" ControlToValidate="txtCreditLimit" ValidationGroup="valGroupRegister" ValidationExpression="^[1-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                </td>
                                <td>&nbsp;</td>
                                <tr>

                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Label ID="lbStatus" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>

                                </tr>

                            </tr>
                        </tr>
                        <tr id="Commission" runat="server">
                            <td>Commission
                            </td>
                            <td>
                                <asp:TextBox ID="txtCommission" Style="margin-left: 3%;" Width="127%" runat="server" Text="0" Visible="false"></asp:TextBox>

                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid" ControlToValidate="txtCommission" ValidationGroup="valGroupRegister" ValidationExpression="^[1-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>--%>
                            </td>
                        </tr>
                    </table>


                    <table id="buttonsection" class="buttonsection">
                        <tr>
                            <td style="width: 74px; height: 26px">
                                <asp:Button CssClass="appbutton" ID="btnEdit" runat="server" Height="24px" ValidationGroup="valGroupRegister" OnClick="btnEdit_Click" Text="Update"
                                    Width="65px" /></td>
                            <td style="width: 74px; height: 26px">
                                <asp:Button CssClass="appbutton" ID="btnDelete" runat="server" Height="24px" OnClick="btnDelete_Click"
                                    Text="Delete" Width="65px" /></td>
                            <td style="width: 74px; height: 26px">
                                <asp:Button CssClass="appbutton" ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                    Width="65px" /></td>
                        </tr>
                    </table>
                    <table id="status" class="status">
                        <tr>
                            <td style="height: 18px">
                                <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label></td>
                        </tr>
                    </table>
                    <table id="hiddensection" class="hiddensection">
                        <tr>
                            <td style="width: 106px">
                                <asp:HiddenField ID="hfId" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Button CssClass="appbutton" ID="btnAddNew" runat="server" Height="24px" OnClick="btnAddNew_Click"
                                    Text="New" Width="65px" Visible="False" />
                                <asp:Button CssClass="appbutton" ID="btnSave" runat="server" Height="24px" OnClick="btnSave_Click" Text="Save"
                                    Width="65px" Visible="False" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </form>
</body>
</html>
