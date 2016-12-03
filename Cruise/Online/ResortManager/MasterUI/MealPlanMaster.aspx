<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MealPlanMaster.aspx.cs" Inherits="MasterUI_MealPlanMaster" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />    
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />    
    <title>Meal Plan Master</title>
</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="Meal Plan Master" />
    <div>
    <asp:ScriptManager ID="scmgrMealPlan" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="pnlMealPlanMaster" runat="server">
    <ContentTemplate>        
        <div id="gridsection" class="gridsection">
            <asp:DataGrid ID="dgMealPlans" runat="server" AutoGenerateColumns="False" CellPadding="4"
                DataKeyField="MealPlanID" ForeColor="#333333" GridLines="None" OnDeleteCommand="dgMealPlans_DeleteCommand"
                OnSelectedIndexChanged="dgMealPlans_SelectedIndexChanged" Width="705px" BorderStyle="Ridge">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditItemStyle BackColor="#2461BF" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle BackColor="#EFF3FB" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundColumn DataField="MealPlanCode" HeaderText="Meal Plan Code"></asp:BoundColumn>
                    <asp:BoundColumn DataField="MealPlan" HeaderText="Meal Plan"></asp:BoundColumn>                        
                    <asp:BoundColumn DataField="MealPlanDesc" HeaderText="Meal Plan Desc" Visible="False"></asp:BoundColumn>                        
                    <asp:ButtonColumn CommandName="Select" HeaderText="[...]" Text="Edit"></asp:ButtonColumn>
                    <asp:ButtonColumn CommandName="Delete" HeaderText="[...]" Text="Delete" Visible="False"></asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <table id="inputsection" class="inputsection">
            <tr>
                <td style="width: 107px">
                    Meal Plan:</td>
                <td colspan="2" style="width: 418px">
                    <asp:TextBox cssclass="input"  ID="txtMealPlanName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 107px">
                    Meal Plan Code:</td>
                <td colspan="2" style="width: 418px">
                    <asp:TextBox cssclass="input"  ID="txtMealPlanCode" runat="server" Width="148px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 107px; height: 10px;">
                    Meal Includes:</td>
                <td>
                    <table style="font-size: x-small; font-family: Verdana;">
                        <tr>
                            <td style="height: 18px">
                                <asp:CheckBox ID="chkWelcomeDrink" runat="server" Text="Welcome Drink" Width="121px" /></td>
                            <td style="height: 18px">
                                <asp:CheckBox ID="chkBreakfast" runat="server" Text="Breakfast" TabIndex="1" Width="90px" /></td>
                            <td style="height: 18px">
                                <asp:CheckBox ID="chkLunch" runat="server" Text="Lunch" TabIndex="2" Width="65px" /></td>
                            <td style="height: 18px; width: 105px;">
                                <asp:CheckBox ID="chkEveSnacks" runat="server" Text="Eve Snacks" TabIndex="3" Width="100px" /></td>
                            <td style="height: 18px">
                                <asp:CheckBox ID="chkDinner" runat="server" Text="Dinner" TabIndex="4" Width="70px" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 107px">
                    Meal Plan Desc:</td>
                <td style="width: 418px">
                    <asp:TextBox cssclass="input"  ID="txtMealPlanDesc" runat="server" TextMode="MultiLine" Width="449px" Height="45px"></asp:TextBox></td>
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
        <table id="statussection" class="statussection">
        <tr>
            <td>
                <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label></td>
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
