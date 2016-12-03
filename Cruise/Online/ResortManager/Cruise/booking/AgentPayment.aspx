<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgentPayment.aspx.cs" Inherits="Cruise_booking_AgentPayment" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

   <style type="text/css">
.classname {
	-moz-box-shadow:inset 0px 1px 0px 0px #bbdaf7;
	-webkit-box-shadow:inset 0px 1px 0px 0px #bbdaf7;
	box-shadow:inset 0px 1px 0px 0px #bbdaf7;
	background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #79bbff), color-stop(1, #378de5) );
	background:-moz-linear-gradient( center top, #79bbff 5%, #378de5 100% );
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#79bbff', endColorstr='#378de5');
	background-color:#79bbff;
	-webkit-border-top-left-radius:0px;
	-moz-border-radius-topleft:0px;
	border-top-left-radius:0px;
	-webkit-border-top-right-radius:0px;
	-moz-border-radius-topright:0px;
	border-top-right-radius:0px;
	-webkit-border-bottom-right-radius:0px;
	-moz-border-radius-bottomright:0px;
	border-bottom-right-radius:0px;
	-webkit-border-bottom-left-radius:0px;
	-moz-border-radius-bottomleft:0px;
	border-bottom-left-radius:0px;
	text-indent:0;
	border:1px solid #84bbf3;
	display:inline-block;
	color:#ffffff;
	font-family:Arial;
	font-size:15px;
	font-weight:bold;
	font-style:normal;
	height:65px;
	line-height:65px;
	width:131px;
	text-decoration:none;
	text-align:center;
	text-shadow:1px 1px 0px #528ecc;
}
.classname:hover {
	background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #378de5), color-stop(1, #79bbff) );
	background:-moz-linear-gradient( center top, #378de5 5%, #79bbff 100% );
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#378de5', endColorstr='#79bbff');
	background-color:#378de5;
}.classname:active {
	position:relative;
	top:1px;
}</style>

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

                            <td>First Name</td>
                            <td>

                                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="reqftxtFirstName" runat="server" ControlToValidate="txtFirstName" ForeColor="Red" ValidationGroup="valGroupRegister" ErrorMessage="*"></asp:RequiredFieldValidator>

                            </td>
                            <td></td>
                        </tr>
                        <tr>

                            <td>Last Name</td>
                            <td>

                                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqftxtLastName" runat="server" ControlToValidate="txtLastName" ForeColor="Red" ValidationGroup="valGroupRegister" ErrorMessage="*"></asp:RequiredFieldValidator>

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
                            <td>Email Id</td>
                            <td>
                                <asp:TextBox ID="txtMailId" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqftxtMailId" runat="server" ControlToValidate="txtMailId" ErrorMessage="*" ForeColor="Red" ValidationGroup="valGroupRegister"></asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>

                            <td>Password</td>
                            <td>

                                <asp:TextBox ID="txtpassword" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqftxtpassword" runat="server" ControlToValidate="txtpassword" ForeColor="Red" ValidationGroup="valGroupRegister" ErrorMessage="*"></asp:RequiredFieldValidator>

                            </td>
                            <td></td>
                            <tr>

                                <td>Confirm Password</td>
                                <td>

                                    <asp:TextBox ID="txtConfirmPassword" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqftxtConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" ForeColor="Red" ValidationGroup="valGroupRegister" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </td>
                                <td></td>
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
                                                <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Refresh" />
                                            </td>
                                            <td></td>
                                        </tr>
                                    </tr>

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
