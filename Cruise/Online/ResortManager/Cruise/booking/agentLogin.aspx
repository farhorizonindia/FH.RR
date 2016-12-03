<%@ Page Language="C#" AutoEventWireup="true" CodeFile="agentLogin.aspx.cs" Inherits="Cruise_booking_agentLogin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link rel="stylesheet" type="text/css" media="all" href="../../css/newcss.css" />
    
    <style>
    
    .loginbox2{ 
		width: 97%; 
		height: 132px;
		
		 left:30px;}
	
	.loginbox2 table{ margin:0 auto; float:none; }
    
    </style>
</head>
<body class="bg-img-H">
    <form id="form1" runat="server">    
         
    <div style="font-weight: bold; font-size: 10px; text-transform: capitalize; color: navy; font-family: 'Monotype Corsiva'">
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>        
          <div style="    margin: 144px -4px 0 0;" class="loginbox2" >
  
           
                    <asp:Login ID="Login1" runat="server" BackColor="#EFF3FB" BorderColor="#FF8000" BorderPadding="4"
                        BorderStyle="Solid" BorderWidth="2px" Font-Names="Verdana" Font-Size="0.8em"
                        ForeColor="#333333" Height="166px" OnAuthenticate="Login1_Authenticate" Width="361px" DisplayRememberMe="False" UserNameLabelText="Email Id:">
                        <TitleTextStyle BackColor="#95bede" Font-Bold="True" Font-Size="Small" ForeColor="White" />
                        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                        <TextBoxStyle Font-Bold="False" Font-Italic="False" Font-Size="Small" />
                        <LoginButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                            Font-Bold="True" Font-Names="Verdana" Font-Size="Small" ForeColor="#284E98" />
                        <LabelStyle Font-Size="Small" />
                    </asp:Login>
              </div>
        </ContentTemplate>                   
        </asp:UpdatePanel>    
    </div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="200">
            <ProgressTemplate>
                <iframe id="pgrIFrame" frameborder="0" marginheight="0" marginwidth="0" scrolling="no" 
                            src="javascript:'<html></html>';" style="position:absolute; 
                            top:342px; left:334px; height:45px; width:208px; z-index:19999">

                </iframe>
                    <asp:Panel ID="Panel1" runat="server"   Height="100" Style="z-index:20000" Width="300">
                        <div style="position: relative; top:20px; left:70px;"></div>
                        <asp:Image ID="image2" runat="server" Height="40px" Width="40px" ImageUrl="~/images/preloader.png" />
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
