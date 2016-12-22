<%@ Page Language="C#" AutoEventWireup="true" CodeFile="loggedout.aspx.cs" Inherits="loggedout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Logged Out</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left:5px;">
    <asp:Label ID="logoutmsg" runat="server"></asp:Label>
    </div>
    <div style="margin-left:5px;">Please click on <a href="Default.aspx">login</a> to go to the login page.</div>
    </form>
</body>
</html>
