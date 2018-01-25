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
        .auto-style2 {
            width: 116px;
        }

        .auto-style3 {
            width: 118px;
        }

        .auto-style4 {
            width: 77px;
        }

        .auto-style5 {
            width: 118px;
            height: 24px;
        }

        .auto-style6 {
            width: 116px;
            height: 24px;
        }

        .auto-style7 {
            width: 77px;
            height: 24px;
        }

        .auto-style8 {
            height: 24px;
        }

        .auto-style9 {
            width: 118px;
            height: 19px;
        }

        .auto-style10 {
            width: 116px;
            height: 19px;
        }

        .auto-style11 {
            width: 77px;
            height: 19px;
        }

        .auto-style12 {
            height: 19px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Package Rate Card Master" />
        <br />
        <div>
            <asp:ScriptManager ID="scmgrMarketMaster" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="pnlMarketMaster" runat="server">
                <ContentTemplate>

                    <div id="gridsection" class="gridsection">

                        <table class="titleParent">
                            <tr style="display: none">
                                <td class="auto-style3">RateCardId</td>
                                <td class="auto-style2">
                                    <asp:DropDownList ID="ddlRateCardId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRateCardId_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="auto-style4">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style5">Package</td>
                                <td class="auto-style6">
                                    <asp:DropDownList ID="ddlpackage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpackage_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="auto-style7">
                                    <asp:RequiredFieldValidator ID="reqfddlpackage" runat="server" ControlToValidate="ddlpackage" ForeColor="Red" InitialValue="-Select-" ValidationGroup="ValRate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                                <td class="auto-style8"></td>
                            </tr>
                            <tr>
                                <td >Main Agent</td>
                                <td >
                                    <asp:DropDownList ID="ddlagent" Width="44%" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlagent" ForeColor="Red" InitialValue="-Select-" ValidationGroup="ValRate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                                <td >Ref Agent</td>
                                <td >
                                    <asp:DropDownList ID="ddlAgentRef" Width="44%" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlagentref" ForeColor="Red" InitialValue="-Select-" ValidationGroup="ValRate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                                <td >Commission Agent</td>
                                <td >
                                    <asp:DropDownList ID="ddlAgentCommission" Width="44%" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlAgentCommission" ForeColor="Red" InitialValue="-Select-" ValidationGroup="ValRate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                                <td >Commission for Agent(in %)</td>
                                <td >
                                    <asp:TextBox ID="txtCommission" Width="44%" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCommission" ForeColor="Red" InitialValue="-Select-" ValidationGroup="ValRate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
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
                                <td class="auto-style3">Valid From</td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtValFrom" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtValFrom_CalendarExtender" runat="server" TargetControlID="txtValFrom"></asp:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="reqftxtValFrom" runat="server" ControlToValidate="txtValFrom" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>
                                </td>
                                <td class="auto-style4">Valid To</td>
                                <td>
                                    <asp:TextBox ID="txtValto" runat="server" AutoPostBack="True" OnTextChanged="txtValto_TextChanged"></asp:TextBox>
                                    <asp:CalendarExtender ID="caltxtValto" runat="server" TargetControlID="txtValto"></asp:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="reqftxtValto" runat="server" ControlToValidate="txtValto" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5">Tax(%)</td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtTaxPercent" runat="server" AutoPostBack="True" OnTextChanged="txtTaxPercent_TextChanged">0</asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqftxtTaxPercent" runat="server" ControlToValidate="txtTaxPercent" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                                </td>
                                <td class="auto-style7"></td>
                                <td class="auto-style8"></td>
                            </tr>
                            <tr>
                                <td class="auto-style3">Currency</td>
                                <td class="auto-style2">
                                    <asp:DropDownList ID="ddlCurrency" runat="server">
                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        <asp:ListItem>INR</asp:ListItem>
                                        <asp:ListItem>USD</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqftxtcarruency" runat="server" ControlToValidate="ddlCurrency" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate" InitialValue="0"></asp:RequiredFieldValidator>

                                </td>
                                <td class="auto-style4">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style9">From Pax:</td>
                                <td class="auto-style10">
                                    <asp:TextBox ID="txtFrmPax" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFrmPax" ErrorMessage="*" ValidationGroup="ValRate"></asp:RequiredFieldValidator>
                                </td>
                                <td class="auto-style11"></td>
                                <td class="auto-style12"></td>
                            </tr>
                            <tr>
                                <td class="auto-style9">To Pax:</td>
                                <td class="auto-style10">
                                    <asp:TextBox ID="txtToPax" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToPax" ErrorMessage="*" ValidationGroup="ValRate"></asp:RequiredFieldValidator>
                                </td>
                                <td class="auto-style11">&nbsp;</td>
                                <td class="auto-style12">&nbsp;</td>
                            </tr>
                        </table>
                        <table id="statussection" class="statussection">
                            <tr>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>

                        <asp:GridView ID="GridRateSheet" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" Width="90%" OnRowDataBound="GridRateSheetRowDataBound" GridLines="None">
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle HorizontalAlign="Center" BackColor="#EFF3FB" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="Sn" />
                                <asp:TemplateField>

                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtvalfrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtvalFrom_CalendarExtender" runat="server" TargetControlID="txtvalfrom"></asp:CalendarExtender>
                                        &nbsp;
                                      <%--  <asp:RequiredFieldValidator ID="reqftxtvalFrom" runat="server" ControlToValidate="txtvalfrom" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>
                                        &nbsp;
                                        <asp:RegularExpressionValidator ID="regExValtxtvalFrom"
                                            ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"
                                            ControlToValidate="txtvalfrom" ErrorMessage="*" runat="server"
                                            CssClass="colorred" ValidationGroup="ValRate">
                                        </asp:RegularExpressionValidator>--%>
                                    </ItemTemplate>
                                    <ControlStyle Width="0px" />
                                    <HeaderStyle ForeColor="White" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="" ControlStyle-Width="0">

                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtValTo"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtValTo_CalendarExtender" runat="server" TargetControlID="txtValTo"></asp:CalendarExtender>
                                        &nbsp;
                                     <%--   <asp:RequiredFieldValidator ID="reqftxtValTo" runat="server" ControlToValidate="txtValTo" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                                        &nbsp;
                                        <asp:RegularExpressionValidator ID="regExtxtValTo"
                                            ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"
                                            ControlToValidate="txtValTo" ErrorMessage="*" runat="server"
                                            CssClass="colorred" ValidationGroup="ValRate">
                                        </asp:RegularExpressionValidator>--%>
                                    </ItemTemplate>
                                    <ControlStyle Width="0px" />
                                    <HeaderStyle ForeColor="White" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Room<br/> Category">

                                    <ItemTemplate>
                                        <asp:DropDownList runat="server" Width="180px" ID="ddlRoomCat"></asp:DropDownList>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="reqfddlRoomCat" runat="server" ControlToValidate="ddlRoomCat" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate" InitialValue="-Select-"></asp:RequiredFieldValidator>

                                    </ItemTemplate>

                                    <HeaderStyle ForeColor="White" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="From <br/> Pax">

                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtFromPax" Width="40px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regextxtFromPax" runat="server" ControlToValidate="txtFromPax" ErrorMessage="*" ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="ValRate"></asp:RegularExpressionValidator>

                                    </ItemTemplate>
                                    <HeaderStyle ForeColor="White" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="To <br/> Pax">

                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtToPax" Width="40px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regextxtToPax" runat="server" ControlToValidate="txtToPax" ErrorMessage="*" ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="ValRate"></asp:RegularExpressionValidator>

                                    </ItemTemplate>
                                    <HeaderStyle ForeColor="White" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-ForeColor="Black" HeaderText="BC<br/> PP">

                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtBcPP" Width="40px" AutoPostBack="True" OnTextChanged="txtBcPP_TextChanged"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regextxtBcPP" runat="server" ControlToValidate="txtBcPP" ErrorMessage="*" ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="ValRate"></asp:RegularExpressionValidator>

                                    </ItemTemplate>
                                    <HeaderStyle ForeColor="Black" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-ForeColor="Black" HeaderText="BC<br/>SRS">

                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtBcSrs" Width="40px" AutoPostBack="True" OnTextChanged="txtBcSrs_TextChanged"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regextxtBcSrs" runat="server" ControlToValidate="txtBcSrs" ErrorMessage="*" ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="ValRate"></asp:RegularExpressionValidator>

                                    </ItemTemplate>
                                    <HeaderStyle ForeColor="Black" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-ForeColor="Black" HeaderText="BC<br/>Tax Inc.">

                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chktaxInc" Width="40px" OnCheckedChanged="chktaxInc_CheckedChanged"></asp:CheckBox>

                                    </ItemTemplate>
                                    <HeaderStyle ForeColor="Black" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-ForeColor="Black" HeaderText="BC<br/>Tax(%)">

                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtBcTaxValue" Width="40px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regextxtBcTaxValue" runat="server" ControlToValidate="txtBcTaxValue" ErrorMessage="*" ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="ValRate"></asp:RegularExpressionValidator>

                                    </ItemTemplate>
                                    <HeaderStyle ForeColor="Black" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-ForeColor="Black" HeaderText="NC<br/> PP">

                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtNcPP" Width="40px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regextxtNcPP" runat="server" ControlToValidate="txtNcPP" ErrorMessage="*" ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="ValRate"></asp:RegularExpressionValidator>

                                    </ItemTemplate>
                                    <HeaderStyle ForeColor="Black" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-ForeColor="Black" HeaderText="NC<br/>SRS">

                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtNcSrs" Width="40px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regextxtNcSrs" runat="server" ControlToValidate="txtNcSrs" ErrorMessage="*" ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="ValRate"></asp:RegularExpressionValidator>

                                    </ItemTemplate>
                                    <HeaderStyle ForeColor="Black" />
                                </asp:TemplateField>


                                <asp:TemplateField ControlStyle-Width="0" HeaderStyle-ForeColor="White" HeaderText="">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnfrompaxold" runat="server" />
                                        <asp:HiddenField ID="hdntopaxold" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="0px" />
                                    <HeaderStyle ForeColor="White" />
                                </asp:TemplateField>


                            </Columns>
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </div>
                    <table id="buttonsection" class="buttonsection">
                        <tr>
                            <td style="width: 74px; height: 26px">
                                <asp:Button CssClass="appbutton" ID="btnSbmit" runat="server" ValidationGroup="ValRate" Height="24px" Text="Submit"
                                    Width="65px" OnClick="btnSbmit_Click" /></td>
                            <td style="width: 74px; height: 26px">
                                <asp:Button CssClass="appbutton" ID="btncancel" runat="server" Text="Cancel"
                                    Height="24px" Width="65px" OnClick="btncancel_Click" /></td>
                        </tr>
                    </table>

                    <asp:GridView ID="gdvRateCards" runat="server" AutoGenerateColumns="False" CellPadding="4" CellSpacing="10" ForeColor="#333333" GridLines="None" DataKeyNames="RateCardId" OnRowCommand="gdv_RowCommand" OnRowDeleting="gdv_RowDeleting">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="RateCardId" HeaderText="RateCardId" />

                            <asp:BoundField DataField="PackName" HeaderText="Package Name" />
                            <asp:BoundField DataField="RateCategory" HeaderText="RateCategory" />
                            <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" />
                            <asp:BoundField DataField="AgentName" HeaderText="AgentName" />
                            <asp:BoundField DataField="RefAgentName" HeaderText="Ref AgentName" />
                            <asp:BoundField DataField="VaildFrom" HeaderText="ValidFrom" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="ValidTo" HeaderText="ValidTo" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="tax" HeaderText="tax" />
                            <asp:BoundField DataField="Currency" HeaderText="Currency" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Select">Edit</asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" OnClientClick="return confirm('Are you certain you want to delete this rate card?')" CommandName="Delete">Delete</asp:LinkButton>
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



                    <table id="hiddensection" class="hiddensection">
                        <tr>
                            <td>
                                <asp:HiddenField ID="hfId" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td></td>
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

