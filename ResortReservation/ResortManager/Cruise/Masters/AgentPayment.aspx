<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgentPayment.aspx.cs" Inherits="Cruise_booking_AgentPayment" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

   

    <title></title>
    <link rel="stylesheet" type="text/css" media="all" href="../../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <link href="../../css/roleRights.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="scmgrLocation" runat="server">
        </asp:ScriptManager>
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Agent paymet Detail" />
        <asp:UpdatePanel ID="updatepanel1" runat="server">
            <ContentTemplate>
                <div>

                    <%-- <asp:DropDownList ID="ddlDestination" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDestination_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlDates" runat="server">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlRiver" runat="server">
        </asp:DropDownList>
    
                    --%>
                    <br />
                    <table id="tblDetails">
                        <tr>

                            <td>Select Agent</td>
                            <td>

                                <asp:DropDownList ID="ddlAgent" runat="server" AutoPostBack="True" >
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="reqftxtFirstName" runat="server" ControlToValidate="ddlAgent" ForeColor="Red" ValidationGroup="valGroupRegister" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>

                            </td>
                            <td></td>
                        </tr>
                        <tr>

                            <td>Billing Address</td>
                            <td>

                                <asp:TextBox ID="txtBillingAddress" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqftxtBillingAddress" runat="server" ControlToValidate="txtBillingAddress" ErrorMessage="*" ForeColor="Red" ValidationGroup="valGroupRegister"></asp:RequiredFieldValidator>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Payment Method</td>
                            <td>
                                <asp:DropDownList ID="ddlpaymentMethod"  runat="server">
                                    <asp:ListItem>-Select-</asp:ListItem>
                                    <asp:ListItem>AirPay - Credit/Debit Card</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqfddlpaymentMethod" runat="server" ControlToValidate="ddlpaymentMethod" InitialValue="-Select-" ErrorMessage="*" ForeColor="Red" ValidationGroup="valGroupRegister"></asp:RequiredFieldValidator>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>

                            <td>On Credit</td>
                            <td>

                                <asp:CheckBox ID="chkOnCredit" runat="server" />

                            </td>
                            <td>&nbsp;</td>
                            <tr>

                                <td>Credit Limit</td>
                                <td>

                                    <asp:TextBox ID="txtCreditLimit" runat="server"></asp:TextBox>

                                </td>
                                <td>&nbsp;</td>
                                <tr>

                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Label ID="lbStatus" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>

                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnRegister" runat="server" CssClass="classname" OnClick="btnRegister_Click" Text="Register" ValidationGroup="valGroupRegister" />
                                        &nbsp;
                                        <asp:Button ID="btnRefresh" CssClass="classname" runat="server" OnClick="btnRefresh_Click" Text="Refresh" />
                                    </td>
                                    <td></td>
                                </tr>
                            </tr>
                        </tr>
                    </table>




                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
