<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangeTreeType.aspx.cs" Inherits="MasterUI_ChangeTreeType" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />    
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />    
    <title>Change Tree Type</title>
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
    <form id="form1" runat="server">
    <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="Accomodation Tree Master" />
    <div>
        <div>                            
        <table id="inputsection" class="inputsection">                                
                <tr>
                    <td style="font-size: x-small; font-family: Verdana">
                        Current Tree Type:</td>
                    <td style="font-size: x-small; width: 216px; font-family: Verdana">
                        <asp:Label ID="lblTreeType" runat="server" Text="Label" Width="214px"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-size: x-small; font-family: Verdana">
                        Select Tree Type:</td>
                    <td style="font-size: x-small; font-family: Verdana; width: 216px;">
                        <asp:DropDownList cssclass="select" ID="ddlTreeType" runat="server" Width="214px">
                        </asp:DropDownList></td>
                </tr>
                </table>
        <table id="buttonsection" class="buttonsection">
            <tr>
                <td valign="top">
                    <asp:Button cssclass="appbutton" ID="btnDone" runat="server" OnClick="btnDone_Click" Text="Update" />
                    </td>
                <td><input id="btnClose" onclick="CloseMe()" type="button" value="Close" /></td>                
            </tr>
            </table>
        <table id="statussection" class="statussection">
            <tr>
                <td style="font-size: x-small; font-family: Verdana; height: 14px;">
                    <asp:Label ID="lblErrorMsg" runat="server" Font-Size="X-Small" ForeColor="Red"></asp:Label></td>
            </tr>
        </table>
        </div>    
    </div>
    </form>
</body>
</html>
