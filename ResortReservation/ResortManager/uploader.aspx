<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uploader.aspx.cs" Inherits="uploader" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Upload</title>
    <link rel="stylesheet" type="text/css" media="all" href="css/pageheader.css"/>    
    <link rel="stylesheet" type="text/css" media="all" href="style.css"/>    
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="Upload Data" />
    <div style="padding-left:5px;">
        <asp:Label ID="Label1" runat="server" Text="Select File"></asp:Label>
        <input id="fileUploader" style="width: 197px" type="file" runat="server"/>
        <input type="submit" id="Submit1" value="Upload" runat="server" onserverclick="Submit1_ServerClick1" />    
    </div>
    </form>
</body>
</html>
