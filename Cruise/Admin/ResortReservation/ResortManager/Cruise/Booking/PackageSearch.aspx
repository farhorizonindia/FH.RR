<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PackageSearch.aspx.cs" Inherits="Cruise_Booking_PackageSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>  <asp:DropDownList ID="ddlDestination" runat="server" ></asp:DropDownList></td>
                <td>  <asp:DropDownList ID="ddlDates" runat="server" ></asp:DropDownList></td>
                <td>  <asp:DropDownList ID="ddlRivers" runat="server" ></asp:DropDownList></td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /></td>
            </tr>
        </table>
       
    </div>
    </form>
</body>
</html>
