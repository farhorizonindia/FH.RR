<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Far Horizon India Resort Reservation Manager</title>
    <style type="text/css">
        .auto-style1 {
            width: 321px;
            height: 208px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <%--position: fixed;
   top: 50%;
   left: 50%;
   transform: translate(-50%, -50%);--%>
        <div style="position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); background-color: white; font-weight: bold; font-size: 10px; text-transform: capitalize; color: navy; font-family: 'Monotype Corsiva'">
            <%--<div style="font-weight: bold; font-size: 10px; text-transform: capitalize; color: navy; font-family: 'Monotype Corsiva'">--%>
            <br />
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td rowspan="2" class="auto-style1">
                                <asp:Login ID="Login1" runat="server" BackColor="#EFF3FB" BorderColor="#FF8000" BorderPadding="4"
                                    BorderStyle="Solid" BorderWidth="2px" Font-Names="Verdana" Font-Size="0.8em"
                                    ForeColor="#333333" Height="166px" OnAuthenticate="Login1_Authenticate" Width="361px" DisplayRememberMe="False" OnLoggedIn="Login1_LoggedIn">
                                    <TitleTextStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Small" ForeColor="White" />
                                    <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                                    <TextBoxStyle Font-Bold="False" Font-Italic="False" Font-Size="Small" />
                                    <LoginButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Bold="True" Font-Names="Verdana" Font-Size="Small" ForeColor="#284E98" />
                                    <LabelStyle Font-Size="Small" />
                                </asp:Login>
                                <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="TextBox2" runat="server" Visible="false"></asp:TextBox>
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Visible="false" Text="Button" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="200">
            <ProgressTemplate>
                <iframe id="pgrIFrame" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"
                    src="javascript:'<html></html>';" style="position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); height: 100px; width: 250px; z-index: 19999; background-color: white; border-color: #C2D3FC; border-style: solid; border-width: 1px;"></iframe>
                <asp:Panel ID="Panel1" runat="server" BackColor="white" Style="z-index: 20000">
                    <%--<div style="position: relative; top: 20px; left: 70px;">--%>
                    <div style="position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
                        <asp:Image ID="image2" runat="server" ImageUrl="~/images/indicator.gif" />
                        Please Wait....      
                    </div>
                </asp:Panel>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <cc1:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1" runat="server"
            TargetControlID="Panel1" HorizontalOffset="300" VerticalOffset="150"></cc1:AlwaysVisibleControlExtender>
    </form>
</body>
</html>
