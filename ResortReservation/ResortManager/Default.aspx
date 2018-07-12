<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reservation Manager</title>
    <style type="text/css">
        /*.auto-style1
        {
            width: 321px;
            height: 208px;
        }*/
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-weight: bold; font-size: 10px; text-transform: capitalize; color: navy; font-family: 'Monotype Corsiva'">
            <br />
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 789px; height: 132px;">
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
                    src="javascript:'<html></html>';" style="position: absolute; top: 342px; left: 334px; height: 45px; width: 208px; z-index: 19999"></iframe>
                <asp:Panel ID="Panel1" runat="server" BackColor="white" BorderColor="#C2D3FC"
                    BorderStyle="solid" BorderWidth="1" Height="100" Style="z-index: 20000" Width="300">
                    <div style="position: relative; top: 20px; left: 70px;">
                        <asp:Image ID="image2" runat="server" ImageUrl="~/images/indicator.gif" />
                    Please Wait....                     
                </asp:Panel>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <cc1:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1" runat="server"
            TargetControlID="Panel1" HorizontalOffset="300" VerticalOffset="150">
        </cc1:AlwaysVisibleControlExtender>
    </form>
</body>
</html>
