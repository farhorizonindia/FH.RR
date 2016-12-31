<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgentMaster.aspx.cs" Inherits="MasterUI_AgentMaster" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />    
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />    
    <title>Agent Master</title>
    <style type="text/css">
        .auto-style1
        {
            height: 23px;
        }
        .auto-style2
        {
            width: 120px;
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="Agent Master" />
    <div>
    <asp:ScriptManager ID="scmgrAgent" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="pnlAgentMaster" runat="server">
    <ContentTemplate>                                
    <div id="gridsection" class="gridsection">
        <asp:DataGrid ID="dgAgents" runat="server" AutoGenerateColumns="False" CellPadding="4"
        DataKeyField="AgentID" ForeColor="#333333" GridLines="None" OnDeleteCommand="dgAgents_ItemCommand"
        OnSelectedIndexChanged="dgAgents_SelectedIndexChanged" Width="525px" BorderStyle="Ridge" OnItemCommand="dgAgents_ItemCommand">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditItemStyle BackColor="#2461BF" />
        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <AlternatingItemStyle BackColor="White" />
        <ItemStyle BackColor="#EFF3FB" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:BoundColumn DataField="AgentName" HeaderText="Agent Name"></asp:BoundColumn>
            <asp:BoundColumn DataField="AgentCode" HeaderText="Agent Code"></asp:BoundColumn>
            <asp:ButtonColumn CommandName="Select" HeaderText="[...]" Text="Edit"></asp:ButtonColumn>
            <asp:ButtonColumn CommandName="Delete" HeaderText="[...]" Text="Delete" Visible="False"></asp:ButtonColumn>
              <asp:ButtonColumn CommandName="MaptoRate" HeaderText="[...]" Text="Map" ></asp:ButtonColumn>
        </Columns>
        </asp:DataGrid>
    </div>
    <table id="inputsection" class="inputsection">
    <tr>
        <td>
            Agent Name</td>
        <td style="width: 120px">
            <asp:TextBox cssclass="input"  ID="txtAgentName" runat="server"></asp:TextBox></td>        
    </tr>
    <tr>
        <td>
            Agent Code</td>
        <td style="width: 120px">
            <asp:TextBox cssclass="input"  ID="txtAgentCode" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td class="auto-style1">
            Agent Email Id</td>
        <td class="auto-style2">
            <asp:TextBox cssclass="input"  ID="txtAgentEmailId" runat="server" MaxLength="50" Width="200px" AutoCompleteType="Email"></asp:TextBox></td>
    </tr>
        <tr>
            <td class="auto-style1">Phone</td>
            <td class="auto-style2">
                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqftxtBillingAddress0" runat="server" ControlToValidate="txtPhone" ErrorMessage="*" ForeColor="Red" ValidationGroup="valGroupRegister"></asp:RequiredFieldValidator>
            </td>
        </tr>
       <tr>
           <td>Password</td>
           <td><asp:TextBox cssclass="input"  ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
           </td>

       </tr>
    </table>
   <table id="tblDetails">
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
                              
                            </tr>
                        </tr>
                    </table>


    <table id="buttonsection" class="buttonsection">
        <tr>
            <td style="width: 74px; height: 26px">
                <asp:Button cssclass="appbutton" ID="btnEdit" runat="server" Height="24px" OnClick="btnEdit_Click" Text="Update"
                    Width="65px" /></td>
            <td style="width: 74px; height: 26px">
                <asp:Button cssclass="appbutton" ID="btnDelete" runat="server" Height="24px" OnClick="btnDelete_Click"
                    Text="Delete" Width="65px" /></td>
            <td style="width: 74px; height: 26px">
                <asp:Button cssclass="appbutton" ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
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
        <asp:Button cssclass="appbutton" ID="btnAddNew" runat="server" Height="24px" OnClick="btnAddNew_Click"
        Text="New" Width="65px" Visible="False" />
        <asp:Button cssclass="appbutton" ID="btnSave" runat="server" Height="24px" OnClick="btnSave_Click" Text="Save"
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
