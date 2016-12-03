<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewRateCard.aspx.cs" Inherits="Rate_NewRateCard" %>

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
        .auto-style1
        {
            width: 128px;
            height: 23px;
        }

        .auto-style2
        {
            height: 23px;
        }

        .auto-style4
        {
            width: 121px;
        }

        #divfit
        {
            width: 512px;
        }

        .ddlStyling
        {
            width: 149px;
        }

        .GridTxt
        {
            background-color: gray;
            width: 30px;
        }

        .GridStyle
        {
            Width: 227px;
        }

        .GridItemTempleteStyle
        {
            text-align: center;
            width: 60px;
            background-color: gray;
        }

        .txtStyle
        {
            background-color: none;
            width: 30px;
        }

        .tblQuad
        {
            width: 200px;
        }

        .DivSbmt
        {
            text-align: center;
        }
          .auto-style5
          {
              width: 8px;
          }
          .auto-style6
          {
              height: 23px;
              width: 8px;
          }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Add New Rate Card" />
        <br />
        <div>
            <asp:ScriptManager ID="scmgrRateCateMaster" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="pnlMarketMaster" runat="server">
                <ContentTemplate>
                    <div id="gridsection" class="gridsection">
                    </div>
                    <table id="inputsection">
                        <tr>
                            <td style="width: 128px">Select Rate category</td>
                            <td>
                                <asp:DropDownList ID="ddlRatecategory" CssClass="ddlStyling" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqfddlRatecategory" runat="server" ControlToValidate="ddlRatecategory" ErrorMessage="*" ForeColor="Red" InitialValue="-Select-" ValidationGroup="ValRate"></asp:RequiredFieldValidator>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td class="auto-style5">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 128px">Accom. Type</td>
                            <td>
                                <asp:DropDownList ID="ddlAccomType" CssClass="ddlStyling" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAccomType_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqfddlAccomType" runat="server" ControlToValidate="ddlAccomType" ErrorMessage="*" ForeColor="Red" InitialValue="-Select-" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td class="auto-style5">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 128px">Accom. Name</td>
                            <td>
                                <asp:DropDownList ID="ddlAccom" CssClass="ddlStyling" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqfddlAccom" runat="server" ControlToValidate="ddlAccom" ErrorMessage="*" ForeColor="Red" InitialValue="-Select-" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td class="auto-style5">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 128px">Room Category</td>
                            <td>
                                <asp:DropDownList ID="ddlRoomCategory" CssClass="ddlStyling" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqfddlRoomCategory" runat="server" ControlToValidate="ddlRoomCategory" ErrorMessage="*" ForeColor="Red" InitialValue="-Select-" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td class="auto-style5">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 128px">Valid From </td>
                            <td>
                                <asp:TextBox ID="txtvalFrom" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="txtvalFrom_CalendarExtender" runat="server" TargetControlID="txtvalFrom">
                                </asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="reqftxtvalFrom" runat="server" ControlToValidate="txtvalFrom" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>
                                <br />
                                <asp:RegularExpressionValidator ID="regExValtxtvalFrom"
                                    ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"
                                    ControlToValidate="txtvalFrom" ErrorMessage="Invalid Format. Use MM/DD/YYYY" runat="server"
                                    CssClass="colorred" ValidationGroup="ValRate">
                                </asp:RegularExpressionValidator>
                            </td>
                            <td>To</td>
                            <td>
                                <asp:TextBox ID="txtvalto" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="txtvalto_CalendarExtender" runat="server" TargetControlID="txtvalto">
                                </asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="reqftxtvalto" runat="server" ControlToValidate="txtvalto" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regExtxtvalto"
                                    ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"
                                    ControlToValidate="txtvalto" ErrorMessage="Invalid Format. Use MM/DD/YYYY" runat="server"
                                    CssClass="colorred" ValidationGroup="ValRate">
                                </asp:RegularExpressionValidator>
                                <br />
                                <asp:CompareValidator ID="cmprValtxtvalto" runat="server" ControlToCompare="txtvalFrom" ControlToValidate="txtvalto" ForeColor="Red" ErrorMessage="*Invalid Date Range(End Date should be greater)" Operator="GreaterThan" Type="Date" ValidationGroup="ValRate" />

                            </td>
                            <td class="auto-style5"></td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Season</td>
                            <td class="auto-style2">
                                <asp:TextBox ID="txtSeason" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqftxtSeason" runat="server" ControlToValidate="txtSeason" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                            </td>
                            <td class="auto-style2">&nbsp;</td>
                            <td class="auto-style2">&nbsp;</td>
                            <td class="auto-style6">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Min. Nights</td>
                            <td class="auto-style2">
                                <asp:TextBox ID="txtMinNights" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqftxtMinNights" runat="server" ControlToValidate="txtMinNights" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                            </td>
                            <td class="auto-style2">&nbsp;</td>
                            <td class="auto-style2">&nbsp;</td>
                            <td class="auto-style6">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Operating Days</td>
                            <td class="auto-style2"></td>
                            <td class="auto-style2"></td>
                            <td class="auto-style2"></td>
                            <td class="auto-style6"></td>
                        </tr>
                    </table>
                    <div id="DivOperRatingDays">
                        <asp:GridView ID="GridOperatingDays" runat="server" Width="169px">

                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="cbCheck" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                    <table id="tblDetailsBed" class="inputsection">
                        <tr style="display:none">
                            <td class="auto-style4">Allow Extra Bed</td>
                            <td>
                                <asp:CheckBox ID="cbAllowExtraBed" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="auto-style4">Web Enabled</td>
                            <td>
                                <asp:CheckBox ID="cbWebEnabled" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="auto-style4">Tax Inclusive</td>
                            <td>
                                <asp:CheckBox ID="cbTaxInclusive" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="auto-style4">Comissionable</td>
                            <td>
                                <asp:CheckBox ID="cbCommisssionable" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="auto-style4">Rate Type</td>
                            <td>
                                <asp:TextBox ID="txtRateType" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqftxtRateType" runat="server" ControlToValidate="txtRateType" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">Currency</td>
                            <td>
                                <asp:TextBox ID="txtCurrency" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqftxtCurrency" runat="server" ControlToValidate="txtCurrency" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">Remark</td>
                            <td>
                                <asp:TextBox ID="txtRemark" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>GIT Range From(Pax)</td>
                            <td>
                                <asp:TextBox ID="txtGITPAXRange" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RqGITPaxRange" runat="server" ControlToValidate="txtGITPAXRange" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

                            </td>

                        </tr>

                        <tr>
                            <td>Tax(%)</td>
                            <td>
                                <asp:TextBox ID="txtTaxPer" runat="server">0</asp:TextBox>
                            </td>
                        </tr>

                    </table>
                    <table id="statussection" class="statussection">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <table id="hiddensection" class="hiddensection">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <fieldset id="fdFIT" style="width: 597px">
                        <legend>F.I.T.</legend>
                        <br />
                        <center>
                            <div id="divfit">
                                <fieldset id="fieldSet1" style="width: 50%">
                                    <legend>Room</legend>
                                    <div>
                                        <asp:GridView runat="server" ID="GridFITRooms" CssClass="GridStyle" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Room Type">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnfRoomTypeId" runat="server" Value='<%#Eval("RoomTypeId") %>' />
                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("RoomType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtFitRate" runat="server" CssClass="GridTxt"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqftxtFitRate" runat="server" ErrorMessage="*" ControlToValidate="txtFitRate" ValidationGroup="ValRate"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFitRate" ErrorMessage="*" ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="ValRate"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                    <div id="DivFitQuad">
                                        <fieldset id="FdSetFITQuad">
                                            <legend>Quad/Extra Bed</legend>
                                            <table id="tblFitQuad" class="tblQuad">
                                                <tr>
                                                    <td>Quad</td>
                                                    <td>
                                                        <asp:TextBox ID="txtFITQuad" CssClass="txtStyle" Text="0" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqftxtFITQuad" runat="server" ErrorMessage="*" ControlToValidate="txtFITQuad" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                                                    </td>
                                                    <td>Extra Bed</td>
                                                    <td>
                                                        <asp:TextBox ID="txtFItExtraBed" CssClass="txtStyle" Text="0" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqftxtFItExtraBed" runat="server" ErrorMessage="*" ControlToValidate="txtFItExtraBed" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                                                    </td>
                                                </tr>

                                            </table>
                                        </fieldset>
                                    </div>
                                </fieldset>
                                <fieldset id="fieldSet2" style="width: 50%">
                                    
                                    <legend>Services</legend>
                                    <div>
                                         <asp:GridView  ID="gdvMealRatesFIT"
                            runat="server" AutoGenerateColumns="False" 
                            >
                            <Columns>
                                
                                <asp:TemplateField HeaderText="WelcomeDrink">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtWelcomeDrinkFIT" runat="server" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="Breakfast">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtBreakfastFIT" runat="server" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                              <asp:TemplateField HeaderText="Lunch">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtLunchFIT" runat="server" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            
                                <asp:TemplateField HeaderText="Dinner">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDinnerFIT" runat="server" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="EveSnacks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtEveSnacksFIT" runat="server" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                
                                
                                
                            </Columns>
                        </asp:GridView>
                                    </div>
                                </fieldset>
                            </div>
                        </center>
                        <br />
                    </fieldset>


                    <fieldset id="fdGIT" style="width: 597px">
                        <legend>G.I.T.</legend>
                        <br />
                        <center>
                            <div id="div1">
                                <fieldset id="fieldSet4" style="width: 50%">

                                    <legend>Room</legend>
                                    
                                    <div>
                                        <asp:GridView runat="server" ID="GridGITRooms" CssClass="GridStyle" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Room Type">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnfRoomTypeId" runat="server" Value='<%#Eval("RoomTypeId") %>' />
                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("RoomType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtGitRate" CssClass="GridTxt"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqftxtGitRate" runat="server" ErrorMessage="*" ControlToValidate="txtGitRate" ValidationGroup="ValRate"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="regextxtGitRate" runat="server" ControlToValidate="txtGitRate" ErrorMessage="*" ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="ValRate"></asp:RegularExpressionValidator>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div id="DivGitQuad">
                                            <fieldset id="FdGidQuad">
                                                <legend>Quad/Extra Bed</legend>
                                                <table id="tblGitQuad" class="tblQuad">
                                                    <tr>
                                                        <td>Quad</td>
                                                        <td>
                                                            <asp:TextBox ID="txtGitQuad" CssClass="txtStyle" Text="0" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="reqftxtGitQuad" runat="server" ErrorMessage="*" ControlToValidate="txtGitQuad" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                                                        </td>
                                                        <td>Extra Bed</td>
                                                        <td>
                                                            <asp:TextBox ID="txtGitExtraBed" CssClass="txtStyle" Text="0" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="reqftxtGitExtraBed" runat="server" ErrorMessage="*" ControlToValidate="txtGitExtraBed" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                                                        </td>
                                                    </tr>

                                                </table>
                                            </fieldset>
                                        </div>
                                    </div>
                                </fieldset>
                                <fieldset id="fieldSet5" style="width: 50%">
                                    <legend>Services</legend>
                                   
                                    <div>
                                        <asp:GridView   ID="gdvMealRatesGIT"
                            runat="server" AutoGenerateColumns="False" 
                            >
                            <Columns>
                                
                                <asp:TemplateField HeaderText="WelcomeDrink">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtWelcomeDrinkGIT" runat="server" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="Breakfast">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtBreakfastGIT" runat="server" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                              <asp:TemplateField HeaderText="Lunch">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtLunchGIT" runat="server" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            
                                <asp:TemplateField HeaderText="Dinner">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDinnerGIT" runat="server" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="EveSnacks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtEveSnacksGIT" runat="server" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                
                                
                                
                            </Columns>
                        </asp:GridView>
                                    </div>
                                </fieldset>
                            </div>
                        </center>
                        <br />
                    </fieldset>

                    <br />
                    <div id="DivSbmtSection" class="DivSbmt">
                        <asp:Button ID="btnSbmit" CssClass="appbutton" runat="server" Text="Submit" OnClick="btnSbmit_Click" ValidationGroup="ValRate" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" CssClass="appbutton" CausesValidation="false" runat="server" Text="Reload" OnClick="btnCancel_Click" />

                    </div>
                    <br />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
