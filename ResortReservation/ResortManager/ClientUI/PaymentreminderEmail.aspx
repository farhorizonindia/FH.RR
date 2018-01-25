<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentreminderEmail.aspx.cs" Inherits="ClientUI_PaymentreminderEmail" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Reminder</title>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-en.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-setup.js"></script>
    <script language="javascript" type="text/javascript" src="../js/global.js"></script>
    <script language="javascript" type="text/javascript" src="../js/client/viewbookings.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/viewbookings.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/calendar-blue2.css"
        title="win2k-cold-1" />
    <style type="text/css">
        .auto-style1 {
            width: 80px;
            height: 19px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Revenue Report" />
        <div>
            <table>
                <tr>
                    <td>Send Payment Reminder Email</td>
                    <td>
                        <asp:Button ID="btnSend" runat="server" Text="Send Reminder Email" OnClick="btnSend_Click" Style="height: 25px" /></td>
                    <td></td>

                    <td>
                        <asp:Button ID="btnFinalreminderemail" runat="server" Text="Send Final Reminder Email" OnClick="btnFinalreminderemail_Click" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" Text=" "></asp:Label></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
