<%@ Page Language="C#" AutoEventWireup="true" CodeFile="afterBookingTouristactions.aspx.cs" Inherits="ClientUI_afterBookingTouristactions" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>After Booking Actions</title>
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />    
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />    
</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="Tourist Status and Summary" />
    <div>
    <asp:ScriptManager ID="scmgrafterBookingTouristactions" runat ="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="pnlafterBookingTouristactions" runat="server">
    <ContentTemplate>
        <table id="statussection" class="statussection" style="font-family:Verdana; font-size:small; width: 706px">         
            <tr>
                <td>
                    <asp:Label ID="lblTouristStatus" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTouristDetails" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblComment" runat="server"></asp:Label></td>
            </tr>            
        </table>        
        </ContentTemplate>
    </asp:UpdatePanel>
    
    </div>
    </form>
</body>
</html>
