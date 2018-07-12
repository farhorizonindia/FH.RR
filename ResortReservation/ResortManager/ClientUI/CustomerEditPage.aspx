<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerEditPage.aspx.cs" Inherits="ClientUI_CustomerEditPage" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration Report</title>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-en.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-setup.js"></script>
    <script language="javascript" type="text/javascript" src="../js/global.js"></script>
    <script language="javascript" type="text/javascript" src="../js/client/viewbookings.js"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/viewbookings.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/calendar-blue2.css" title="win2k-cold-1" />
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Customer List" />
        <div style="margin-top: 3%; margin-left: 31%;">
            <table>
                <tr>
                    <td>Title</td>
                    <td>
                        <asp:DropDownList ID="ddlTitle" runat="server">
                            <asp:ListItem>Mr</asp:ListItem>
                            <asp:ListItem>Mrs</asp:ListItem>
                            <asp:ListItem>Miss</asp:ListItem>
                            <asp:ListItem>Ms</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="height: 3px;"></td>
                </tr>
                <tr>
                    <td>First Name</td>
                    <td>
                        <asp:TextBox ID="txtFisrtName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 3px;"></td>
                </tr>
                <tr>
                    <td>Last Name</td>
                    <td>
                        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 3px;"></td>
                </tr>
                <tr>
                    <td>Email</td>
                    <td>
                        <asp:TextBox ID="txtEmail" ReadOnly="true" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 3px;"></td>
                </tr>
                <tr>
                    <td>Contact No</td>
                    <td>
                        <asp:TextBox ID="txtContactNo" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 3px;"></td>
                </tr>
                <tr>
                    <td>Address1</td>
                    <td>
                        <asp:TextBox ID="txtAddress1" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 3px;"></td>
                </tr>
                <tr>
                    <td>Address2</td>
                    <td>
                        <asp:TextBox ID="txtAddress2" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 3px;"></td>
                </tr>
                <tr>
                    <td>City</td>
                    <td>
                        <asp:TextBox ID="txtCity" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 3px;"></td>
                </tr>
                <tr>
                    <td>State</td>
                    <td>
                        <asp:TextBox ID="txtState" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 3px;"></td>
                </tr>
                <tr>
                    <td>Post Code</td>
                    <td>
                        <asp:TextBox ID="txtPost" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 3px;"></td>
                </tr>
                <tr>
                    <td>Country</td>
                    <td>
                        <asp:DropDownList ID="ddlCountry" runat="server" Width="51%"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="height: 3px;"></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" /></td>
                    <td>
                        <%-- <asp:Button ID="btnDelete" runat="server" Text="Delete" />--%></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
