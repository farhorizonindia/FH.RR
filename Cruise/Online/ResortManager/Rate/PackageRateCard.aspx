<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PackageRateCard.aspx.cs" Inherits="Rate_PackageRateCard" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Room Category Master</title>
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <script language="javascript" type="text/javascript" src="../js/master/roomcategorymaster.js"></script>
    <style type="text/css">
        .auto-style2
        {
            width: 116px;
        }

        .auto-style3
        {
            width: 118px;
        }

        .auto-style4
        {
            width: 77px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Package Master" />
        <br />
        <div>
            <asp:ScriptManager ID="scmgrMarketMaster" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="pnlMarketMaster" runat="server">
                <ContentTemplate>

                    <div id="gridsection" class="gridsection">

                        <table class="titleParent">
                            <tr>
                                <td class="auto-style3">RateCardId</td>
                                <td class="auto-style2">
                                    <asp:DropDownList ID="ddlRateCardId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRateCardId_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="auto-style4">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style3">Package</td>
                                <td class="auto-style2">
                                    <asp:DropDownList ID="ddlpackage" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="auto-style4">
                                    <asp:RequiredFieldValidator ID="reqfddlpackage" runat="server" ControlToValidate="ddlpackage" ForeColor="Red" InitialValue="-Select-" ValidationGroup="ValRate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style3">Rate Category</td>
                                <td class="auto-style2">
                                    <asp:DropDownList ID="ddlRatecategory" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="auto-style4">
                                    <asp:RequiredFieldValidator ID="reqfddlRatecategory" runat="server" ControlToValidate="ddlRatecategory" ErrorMessage="*" InitialValue="-Select-" ValidationGroup="ValRate" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style3">Supplier</td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtSupplier" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style4">
                                    <asp:RequiredFieldValidator ID="reqftxtSupplier" runat="server" ControlToValidate="txtSupplier" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style3">Location</td>
                                <td class="auto-style2">
                                    <asp:DropDownList ID="ddlLocation" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="auto-style4">
                                    <asp:RequiredFieldValidator ID="reqfddlLocation" runat="server" ControlToValidate="ddlLocation" ErrorMessage="*" ForeColor="Red" InitialValue="-Select-" ValidationGroup="ValRate"></asp:RequiredFieldValidator>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style3">Valid From</td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtValFrom" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqftxtValFrom" runat="server" ControlToValidate="txtValFrom" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>
                                </td>
                                <td class="auto-style4">Valid To</td>
                                <td>
                                    <asp:TextBox ID="txtValto" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqftxtValto" runat="server" ControlToValidate="txtValto" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style3">Tax</td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtTaxPercent" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqftxtTaxPercent" runat="server" ControlToValidate="txtTaxPercent" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                                </td>
                                <td class="auto-style4">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style3">Currency</td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtcarruency" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqftxtcarruency" runat="server" ControlToValidate="txtcarruency" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                                </td>
                                <td class="auto-style4">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style3">&nbsp;</td>
                                <td class="auto-style2">&nbsp;</td>
                                <td class="auto-style4">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>

                    </div>
                    <div>

                        <asp:GridView ID="GridRateSheet" runat="server" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="Both" Width="90%" OnRowDataBound="GridRateSheetRowDataBound">
                            <RowStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:BoundField DataField="Sn" />
                                <asp:TemplateField HeaderStyle-BackColor="Green" HeaderStyle-ForeColor="White" HeaderText="Valid<br/> From" >

                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtvalfrom" Width="70px"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtvalFrom_CalendarExtender" runat="server" TargetControlID="txtvalfrom">
                                        </asp:CalendarExtender>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="reqftxtvalFrom" runat="server" ControlToValidate="txtvalfrom" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>
                                        &nbsp;
                                        <asp:RegularExpressionValidator ID="regExValtxtvalFrom"
                                            ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"
                                            ControlToValidate="txtvalfrom" ErrorMessage="*" runat="server"
                                            CssClass="colorred" ValidationGroup="ValRate">
                                        </asp:RegularExpressionValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-BackColor="Green" HeaderStyle-ForeColor="White" HeaderText="Valid<br/>To">

                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtValTo" Width="70px"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtValTo_CalendarExtender" runat="server" TargetControlID="txtValTo">
                                        </asp:CalendarExtender>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="reqftxtValTo" runat="server" ControlToValidate="txtValTo" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                                        &nbsp;
                                        <asp:RegularExpressionValidator ID="regExtxtValTo"
                                            ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"
                                            ControlToValidate="txtValTo" ErrorMessage="*" runat="server"
                                            CssClass="colorred" ValidationGroup="ValRate">
                                        </asp:RegularExpressionValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-BackColor="Purple" HeaderStyle-ForeColor="White" HeaderText="Room<br/> Category">

                                    <ItemTemplate>
                                        <asp:DropDownList runat="server" Width="180px" ID="ddlRoomCat"></asp:DropDownList>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="reqfddlRoomCat" runat="server" ControlToValidate="ddlRoomCat" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate" InitialValue="-Select-"></asp:RequiredFieldValidator>

                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-BackColor="Orange" HeaderStyle-ForeColor="White" HeaderText="From <br/> Pax">

                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtFromPax" Width="40px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regextxtFromPax" runat="server" ControlToValidate="txtFromPax" ErrorMessage="*" ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="ValRate"></asp:RegularExpressionValidator>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-BackColor="Orange" HeaderStyle-ForeColor="White" HeaderText="To <br/> Pax">

                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtToPax" Width="40px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regextxtToPax" runat="server" ControlToValidate="txtToPax" ErrorMessage="*" ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="ValRate"></asp:RegularExpressionValidator>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-BackColor="Yellow" HeaderStyle-ForeColor="Black" HeaderText="BC<br/> PP">

                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtBcPP" Width="40px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regextxtBcPP" runat="server" ControlToValidate="txtBcPP" ErrorMessage="*" ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="ValRate"></asp:RegularExpressionValidator>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-BackColor="Yellow" HeaderStyle-ForeColor="Black" HeaderText="BC<br/>SRS">

                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtBcSrs" Width="40px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regextxtBcSrs" runat="server" ControlToValidate="txtBcSrs" ErrorMessage="*" ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="ValRate"></asp:RegularExpressionValidator>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-BackColor="#ff66ff" HeaderStyle-ForeColor="Black" HeaderText="BC<br/>Tax Inc.">

                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chktaxInc" Width="40px"></asp:CheckBox>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-BackColor="#ff66ff" HeaderStyle-ForeColor="Black" HeaderText="BC<br/>Tax Value">

                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtBcTaxValue" Width="40px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regextxtBcTaxValue" runat="server" ControlToValidate="txtBcTaxValue" ErrorMessage="*" ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="ValRate"></asp:RegularExpressionValidator>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-BackColor="Yellow" HeaderStyle-ForeColor="Black" HeaderText="NC<br/> PP">

                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtNcPP" Width="40px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regextxtNcPP" runat="server" ControlToValidate="txtNcPP" ErrorMessage="*" ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="ValRate"></asp:RegularExpressionValidator>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-BackColor="Yellow" HeaderStyle-ForeColor="Black" HeaderText="NC<br/>SRS">

                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtNcSrs" Width="40px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regextxtNcSrs" runat="server" ControlToValidate="txtNcSrs" ErrorMessage="*" ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="ValRate"></asp:RegularExpressionValidator>
                                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <table id="buttonsection" class="buttonsection">
                        <tr>
                            <td style="width: 74px; height: 26px">
                                <asp:Button CssClass="appbutton" ID="btnSbmit" runat="server"  ValidationGroup="ValRate" Height="24px" Text="Submit"
                                    Width="65px" OnClick="btnSbmit_Click" /></td>
                            <td style="width: 74px; height: 26px">
                                <asp:Button CssClass="appbutton" ID="btnSbmit0" runat="server" Text="Cancel"
                                     OnClick="btnSbmit_Click" Height="24px"  Width="65px" /></td>
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
                            <td>
                                
                            </td>
                            <td>
                                <asp:HiddenField ID="hdnRatecardid" runat="server" />
                                <asp:HiddenField ID="hdnRoomCtid" runat="server" />
                                <asp:HiddenField ID="hdnFrmPax" runat="server" />
                                <asp:HiddenField ID="hdnToPax" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>

