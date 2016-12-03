<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccomWiseActivitiesMaster.aspx.cs" Inherits="MasterUI_AccomWiseActivitiesMaster" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
<link rel="stylesheet" type="text/css" media="all" href="../style.css" />        
<title>Accomodation Wise Activities</title>
<script type="text/JavaScript" language="JavaScript">
    
    
    function ValidateForm()
{
    var theForm = document.forms['form1'];
    if(!theForm)
        theForm=document.form1;
    if(theForm.txtActivityName.value == "")
    {    
        alert("Please Fill In Activity Name");
        theForm.txtActivityName.focus();
        return false;
    }
    if(theForm.txtActivityDesc.value == "")
    {
        alert("Please Fill In Activity Description");
        theForm.txtActivityName.focus();
        return false;
    }    
    if(theForm.txtActivityDesc.value == "")
    {
        alert("Please Fill In Activity Description");
        theForm.txtActivityName.focus();
        return false;
    }    
    if(theForm.ddlAccomodations.selectedIndex == 0 || theForm.ddlAccomodations.selectedIndex == 1)
    {
       alert("Please select the Accomodation.");
        theForm.ddlAccomodations.focus();
        return false;
    }
    return true;
}

</script>
</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="Accomodation Wise Activity Master" />
    <div>
    <asp:ScriptManager ID="scmgrAccomWiseActivitiesMaster" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="pnlAccomWiseActivitiesMaster" runat="server">
    <ContentTemplate>        
        <table class="filtersection" id="filtersection">   
        <tr>
            <td>
                Accomodation:</td>
            <td>
                <asp:DropDownList cssclass="select" ID="ddlAccomodations" runat="server" Width="215px">
                </asp:DropDownList></td>
            <td>
                <asp:Button cssclass="appbutton" ID="btnSearch" runat="server" Text="Show" Width="65px" OnClick="btnSearch_Click" /></td>            
        </tr>
        </table>
        <div id="gridsection" class="gridsection">
        <asp:DataGrid ID="dgAccomWiseActivities" runat="server" AutoGenerateColumns="False" DataKeyField="ActivityId"
            OnSelectedIndexChanged="dgAccomWiseActivities_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None" Width="650px" BorderStyle="Ridge" Height="133px">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditItemStyle BackColor="#2461BF" />
            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#EFF3FB" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><Columns>
                <asp:BoundColumn DataField="AccomodationId" HeaderText="Accomodation Id" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="AccomodationName" HeaderText="Accomodation"></asp:BoundColumn>
                <asp:BoundColumn DataField="ActivityId" HeaderText="Activity Id" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ActivityName" HeaderText="Activity"></asp:BoundColumn>
                <asp:BoundColumn DataField="ActivityDesc" HeaderText="Activity Desc" Visible="False">
                </asp:BoundColumn>
                <asp:ButtonColumn CommandName="Select" Text="Edit" HeaderText="[...]"></asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        </div>
        <table id="inputsection" class="inputsection">
        <tr>
            <td>Activity Name:</td>            
            <td>
            <asp:TextBox cssclass="input"  ID="txtActivityName" runat="server" Width="260px" MaxLength="50"></asp:TextBox></td>        
        </tr>
        <tr>
            <td>Activity Description:</td>        
            <td>
            <asp:TextBox cssclass="input"  ID="txtActivityDesc" runat="server" MaxLength="1000" TabIndex="1" Width="260px" Height="81px"></asp:TextBox></td>            
        </tr>
        <tr>
        </table>
        <table id="buttonsection" class="buttonsection">
            <tr>
                <td style="width: 74px; height: 26px;">
                    <asp:Button cssclass="appbutton" ID="btnEdit" runat="server" Height="24px" Text="Update" Width="65px" OnClick="btnEdit_Click" /></td>
                <td style="width: 74px; height: 26px;">
                    <asp:Button cssclass="appbutton" ID="btnDelete" runat="server" Height="24px" Text="Delete" Width="65px" OnClick="btnDelete_Click" /></td>
                <td style="width: 74px; height: 26px;">
                    <asp:Button cssclass="appbutton" ID="btnCancel" runat="server" Text="Cancel" Width="65px" OnClick="btnCancel_Click" /></td>                
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
        <td style="height: 24px; width: 139px;">             
            <asp:HiddenField ID="hfId" runat="server" /><asp:HiddenField ID="hfAccomId" runat="server" />
        </td>
        </tr></table>
        <table>
        <tr>
        <td style="width: 74px; height: 26px;">
            <asp:Button cssclass="appbutton" ID="btnAddNew" runat="server" Height="24px" Text="New" Width="65px" OnClick="btnAddNew_Click" Visible="False" /></td>
        <td style="width: 74px; height: 26px;">
            <asp:Button cssclass="appbutton" ID="btnSave" runat="server" Height="24px" Text="Save" Width="65px" OnClick="btnSave_Click" Visible="False" /></td>
        </tr>
        </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
