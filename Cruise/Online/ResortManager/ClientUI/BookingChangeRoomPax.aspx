<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookingChangeRoomPax.aspx.cs" Inherits="ChangeRoomPax" EnableEventValidation="false" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Change Rooms/Pax</title>
    <link rel="Stylesheet" type="text/css" media="all" href="../css/bookingChangeRoomPax.css" />    
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />    
    <script language="javascript" type="text/javascript" src="../js/client/bookingChangeRoomPax.js"></script>        
    <script language="javascript" type="text/javascript" src="../js/global.js"></script>        
    <script language= "javascript" type="text/javascript">
        function CloseMe()
        {       
            window.close();
        }
        function DoPostBack()
        {
            __doPostBack('btnDone','This Finishes Room Selection');
        }
    </script>
</head>
<body>
    <form id="frmChotu" runat="server">
    <div>
    <asp:ScriptManager ID="scmgrBooking" runat="server">
        <Scripts>
        <asp:ScriptReference Path="../js/json2.js" />
        </Scripts>
        </asp:ScriptManager>  
        <table id="header" class="header">
        <tr>
            <td class="headertitle" valign="middle" align="left">
             Far Horizon</td>
        </tr>        
        </table>
        
        <table id="changeroompax" class="changeroompax">
        <tr>
        <td colspan="1">
            <asp:Panel ID="pnlChotu" runat="server"></asp:Panel>
        </td>
        </tr>
        <tr>
            <td class="errormsgcell">
            <asp:Label ID="lblErrorMsg" runat="server" CssClass="errormsglabel"></asp:Label></td>
        </tr>
            <tr>
                <td valign="top" style="width: 414px">
                <asp:Button cssclass="appbutton" ID="btnDone" runat="server" Text="Done" OnClick="btnDone_Click" />
                <asp:Button cssclass="appbutton" id="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" /></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
