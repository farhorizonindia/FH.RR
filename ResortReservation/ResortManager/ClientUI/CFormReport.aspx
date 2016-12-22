<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CFormReport.aspx.cs" Inherits="ClientUI_CFormReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../css/cForm.css" rel="stylesheet" type="text/css" />
    <title>C Form Report</title>
</head>
<body>
    <form id="form1" runat="server">    
    <asp:Panel runat="server" ID="pnlCForm" CssClass="cFormContainer">        
        <asp:Panel runat="server" ID="pnlHeaderContainer">
            <div class="header1 label1">
                <asp:Label runat="server" ID="lblHeader1" CssClass="headerRow1"></asp:Label>    
            </div>
            <div class="header1 label2">
                <asp:Label runat="server" ID="lblHeader2" CssClass="headerRow1"></asp:Label>
            </div>
            <div class="header1 label2">
                <asp:Label runat="server" ID="lblBookingRefNo" CssClass="headerRow1"></asp:Label>
            </div>            
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlHeaderContainer2">
            <div class="header2 label1">
                <asp:Label runat="server" ID="lblForeignDetails"></asp:Label>    
            </div>
            <div class="header2 label3">
                <asp:Label runat="server" ID="lblAddress"></asp:Label>    
            </div>             
        </asp:Panel>    
        <asp:Panel runat="server" ID="pnlDataContainer1" style="page-break-after:always; clear:both;">
            <asp:Panel runat="server" ID="pnlTouristDetails">
            </asp:Panel>            
        </asp:Panel>        
        <asp:Panel runat="server" ID="pnlDataContainer2" style="clear:both;">
            <asp:Panel runat="server" ID="pnlGroupDetails">
        </asp:Panel>            
        </asp:Panel>
        <div style="clear:both;" />        
    </asp:Panel>    
    </form>
</body>
</html>
