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
          .auto-style7
          {
              width: 140px;
          }
          .auto-style8
          {
              width: 177px;
          }
          .auto-style9
          {
              height: 23px;
              width: 177px;
          }
          .auto-style10
          {
              width: 128px;
              height: 27px;
          }
          .auto-style11
          {
              width: 177px;
              height: 27px;
          }
          .auto-style12
          {
              height: 27px;
          }
          .auto-style13
          {
              width: 8px;
              height: 27px;
          }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Hotel Rate Card" />
        <br />
       


        <div>
            <asp:ScriptManager ID="scmgrRateCateMaster" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="pnlMarketMaster" runat="server">
                <ContentTemplate>
                     <fieldset id="Fieldset3" style="width:80%" >
                        <legend>Rate Cards</legend>
                    <asp:GridView ID="gdvRateCards" style="width:inherit" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" HeaderStyle-HorizontalAlign="Left" Width="60%" DataKeyNames="RateCardId" OnRowCommand="gdvRateCards_RowCommand" OnRowDeleting="gdvRateCards_RowDeleting" CellSpacing="10" >

                        <AlternatingRowStyle BackColor="White" />
                        <RowStyle HorizontalAlign="Left" />
                       

                        <Columns>
                            <asp:BoundField DataField="RateCardId" HeaderText="RateCardId" />
                            <asp:BoundField DataField="RatecatId" HeaderText="RatecatId" />
                            <asp:BoundField DataField="AccomType" HeaderText="AccomType" />
                            <asp:BoundField DataField="AccomName" HeaderText="Accom" />
                            <asp:BoundField DataField="RoomCategory" HeaderText="RoomCategory" />
                            <asp:BoundField DataField="ValFrom" HeaderText="Valid From" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="ValTo" HeaderText="Valid To" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="Currency" HeaderText="Currency" />
                            <asp:BoundField DataField="GITPaxFrom" HeaderText="GIT Range From" />
                            <asp:BoundField DataField="TaxPct" HeaderText="Tax(%)" />
                            <asp:BoundField DataField="Description" HeaderText="Description" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                  <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Select">Edit</asp:LinkButton>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete"  OnClientClick="return confirm('Are you certain you want to delete this Rate Card?') " >Delete</asp:LinkButton>

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



                    <div id="gridsection" class="gridsection">
                    </div>
                    <table id="inputsection">
                        <tr>
                            <td style="width: 128px">Select Rate category</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="ddlRatecategory" CssClass="ddlStyling" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRatecategory_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqfddlRatecategory" runat="server" ControlToValidate="ddlRatecategory" ErrorMessage="*" ForeColor="Red" InitialValue="-Select-" ValidationGroup="ValRate"></asp:RequiredFieldValidator>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td class="auto-style5">&nbsp;</td>
                        </tr>
                         <tr>
                            <td style="width: 128px">Select Agent</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="ddlagent" CssClass="ddlStyling" runat="server" OnSelectedIndexChanged="ddlRatecategory_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRatecategory" ErrorMessage="*" ForeColor="Red" InitialValue="-Select-" ValidationGroup="ValRate"></asp:RequiredFieldValidator>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td class="auto-style5">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style10">Accom. Type</td>
                            <td class="auto-style11">
                                <asp:DropDownList ID="ddlAccomType" CssClass="ddlStyling" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAccomType_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqfddlAccomType" runat="server" ControlToValidate="ddlAccomType" ErrorMessage="*" ForeColor="Red" InitialValue="-Select-" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                            </td>
                            <td class="auto-style12"></td>
                            <td class="auto-style12"></td>
                            <td class="auto-style13"></td>
                        </tr>
                        <tr>
                            <td style="width: 128px">Accom. Name</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="ddlAccom" CssClass="ddlStyling" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAccom_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqfddlAccom" runat="server" ControlToValidate="ddlAccom" ErrorMessage="*" ForeColor="Red" InitialValue="-Select-" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td class="auto-style5">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 128px">Room Category</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="ddlRoomCategory" CssClass="ddlStyling" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRoomCategory_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqfddlRoomCategory" runat="server" ControlToValidate="ddlRoomCategory" ErrorMessage="*" ForeColor="Red" InitialValue="-Select-" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td class="auto-style5">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 128px">Valid From </td>
                            <td class="auto-style8">
                                <asp:TextBox ID="txtvalFrom" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="txtvalFrom_CalendarExtender" runat="server" TargetControlID="txtvalFrom">
                                </asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="reqftxtvalFrom" runat="server" ControlToValidate="txtvalFrom" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>
                                <br />
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

                            </td>
                            <td class="auto-style5">
                                <asp:CompareValidator ID="cmprValtxtvalto" runat="server" ControlToCompare="txtvalFrom" ControlToValidate="txtvalto" ErrorMessage="*" ForeColor="Red" Operator="GreaterThan" Type="Date" ValidationGroup="ValRate" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Season</td>
                            <td class="auto-style9">
                                <asp:TextBox ID="txtSeason" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqftxtSeason" runat="server" ControlToValidate="txtSeason" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                            </td>
                            <td class="auto-style2"></td>
                            <td class="auto-style2"></td>
                            <td class="auto-style6"></td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Min. Nights</td>
                            <td class="auto-style9">
                                <asp:TextBox ID="txtMinNights" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqftxtMinNights" runat="server" ControlToValidate="txtMinNights" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                            </td>
                            <td class="auto-style2">&nbsp;</td>
                            <td class="auto-style2">&nbsp;</td>
                            <td class="auto-style6">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Operating Days</td>
                            <td class="auto-style9"></td>
                            <td class="auto-style2"></td>
                            <td class="auto-style2"></td>
                            <td class="auto-style6"></td>
                        </tr>
                    </table>
                    <div id="DivOperRatingDays">
                        <asp:GridView ID="GridOperatingDays" runat="server" Width="169px" CellPadding="4" ForeColor="#333333" GridLines="None">

                            <AlternatingRowStyle BackColor="White" />

                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="cbCheck" />
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
                    <table id="tblDetailsBed" class="inputsection">
                        <tr style="display:none">
                            <td class="auto-style4">Allow Extra BedWeb Enabled</td>
                            <td class="auto-style7">
                                <asp:CheckBox ID="cbAllowExtraBed" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="auto-style4">Web EnabledAllow Extra Bed</td>
                            <td class="auto-style7">
                                <asp:CheckBox ID="cbWebEnabled" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="auto-style4">Tax Inclusive</td>
                            <td class="auto-style7">
                                <asp:CheckBox ID="cbTaxInclusive" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="auto-style4">Comissionable</td>
                            <td class="auto-style7">
                                <asp:CheckBox ID="cbCommisssionable" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="auto-style4">Rate Type</td>
                            <td class="auto-style7">
                                <asp:TextBox ID="txtRateType" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqftxtRateType" runat="server" ControlToValidate="txtRateType" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">Currency</td>
                            <td class="auto-style7">
                                <asp:DropDownList ID="ddlCurrency" runat="server" Enabled="False">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                    <asp:ListItem>INR</asp:ListItem>
                                    <asp:ListItem>USD</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqftxtCurrency" runat="server" ControlToValidate="ddlCurrency" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate" InitialValue="0"></asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">Remark</td>
                            <td class="auto-style7">
                                <asp:TextBox ID="txtRemark" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>GIT Range From(Pax)</td>
                            <td class="auto-style7">
                                <asp:TextBox ID="txtGITPAXRange" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RqGITPaxRange" runat="server" ControlToValidate="txtGITPAXRange" ErrorMessage="*" ForeColor="Red" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                            </td>

                        </tr>

                        <tr>
                            <td>Tax(%)</td>
                            <td class="auto-style7">
                                <asp:TextBox ID="txtTaxPer" runat="server">0</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Description</td>
                              <td class="auto-style7"> <asp:TextBox ID="txtRateDesc" runat="server" TextMode="MultiLine"></asp:TextBox> </td>
                           
                        </tr>

                    </table>
                    <table id="statussection" class="statussection">
                        <tr>
                            <td></td>
                          
                        </tr>
                    </table>
                    <table id="hiddensection" class="hiddensection">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <div style="width:100%">
                        <div style="width:48%">
                    <fieldset id="fdFIT" style="width:250px" >
                        <legend>F.I.T.</legend>
                        <br />
                        <center>
                            
                                <fieldset id="fieldSet1" style="width: 50%">
                                    <legend>Room</legend>
                                    <div>
                                        <asp:GridView runat="server" ID="GridFITRooms" CssClass="GridStyle" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Room Type">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnfRoomTypeId" runat="server" Value='<%#Eval("RoomTypeId") %>' />
                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("RoomType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtFitRate" runat="server" Width="80px" CssClass="GridTxt"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqftxtFitRate" runat="server" ErrorMessage="*" ControlToValidate="txtFitRate" ValidationGroup="ValRate"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFitRate" ErrorMessage="*" ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="ValRate"></asp:RegularExpressionValidator>
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
                                    <div id="DivFitQuad">
                                        <fieldset id="FdSetFITQuad">
                                            <legend>Quad/Extra Bed</legend>
                                            <table id="tblFitQuad" class="tblQuad">
                                                <tr>
                                                    <td>Quad</td>
                                                    <td>
                                                        <asp:TextBox ID="txtFITQuad" CssClass="txtStyle" Width="80px" Text="0" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqftxtFITQuad" runat="server" ErrorMessage="*" ControlToValidate="txtFITQuad" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                                                    </td>
                                                    <td>Extra Bed</td>
                                                    <td>
                                                        <asp:TextBox ID="txtFItExtraBed" CssClass="txtStyle" Text="0" Width="80px" runat="server"></asp:TextBox>
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
                            runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" 
                            >
                                             <AlternatingRowStyle BackColor="White" />
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
                                </fieldset>
                        
                        </center>
                        <br />
                    </fieldset>
                        </div>
                       <div style="float:left;width:48%">
                        
                    <fieldset id="fdGIT" >
                        <legend>G.I.T.</legend>
                        <br />
                        <center>
                            <div id="div1">
                                <fieldset id="fieldSet4" style="width: 50%">

                                    <legend>Room</legend>
                                    
                                    <div>
                                        <asp:GridView runat="server" ID="GridGITRooms" CssClass="GridStyle" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Room Type">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnfRoomTypeId" runat="server" Value='<%#Eval("RoomTypeId") %>' />
                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("RoomType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" Width="80px" ID="txtGitRate" CssClass="GridTxt"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqftxtGitRate" runat="server" ErrorMessage="*" ControlToValidate="txtGitRate" ValidationGroup="ValRate"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="regextxtGitRate" runat="server" ControlToValidate="txtGitRate" ErrorMessage="*" ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="ValRate"></asp:RegularExpressionValidator>

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
                                        <div id="DivGitQuad">
                                            <fieldset id="FdGidQuad">
                                                <legend>Quad/Extra Bed</legend>
                                                <table id="tblGitQuad" class="tblQuad">
                                                    <tr>
                                                        <td>Quad</td>
                                                        <td>
                                                            <asp:TextBox ID="txtGitQuad" Width="80px" CssClass="txtStyle" Text="0" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="reqftxtGitQuad" runat="server" ErrorMessage="*" ControlToValidate="txtGitQuad" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                                                        </td>
                                                        <td>Extra Bed</td>
                                                        <td>
                                                            <asp:TextBox ID="txtGitExtraBed" Width="80px" CssClass="txtStyle" Text="0" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="reqftxtGitExtraBed" runat="server" ErrorMessage="*" ControlToValidate="txtGitExtraBed" ValidationGroup="ValRate"></asp:RequiredFieldValidator>

                                                        </td>
                                                    </tr>

                                                </table>
                                            </fieldset>
                                        </div>
                                    </div>
                                </fieldset></div>
                                <fieldset id="fieldSet5" style="width: 50%">
                                    <legend>Services</legend>
                                   
                                    <div>
                                        <asp:GridView   ID="gdvMealRatesGIT"
                            runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" 
                            >
                                            <AlternatingRowStyle BackColor="White" />
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
                                </fieldset>
                          
                        </center>
                        <br />
                    </fieldset>
                            </div>
                    </div>
                    <br />
                    <div id="DivSbmtSection" class="DivSbmt" style="float:left;width:100%;margin-top: 20px; " >
                        <asp:Button ID="btnSbmit" CssClass="appbutton" runat="server" Text="Submit" OnClick="btnSbmit_Click" ValidationGroup="ValRate" Width="60px" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" CssClass="appbutton" CausesValidation="false" runat="server" Text="Reload" OnClick="btnCancel_Click" />

                        <asp:HiddenField ID="hdnRateCardId" runat="server" />

                    </div>
                    <br />
                     
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlAccom" />
                     <asp:AsyncPostBackTrigger ControlID="ddlRoomCategory" />

                </Triggers>

            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
