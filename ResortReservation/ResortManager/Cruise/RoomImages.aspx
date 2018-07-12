<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoomImages.aspx.cs" Inherits="Cruise_Masters_RoomImages" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<%@ Register assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title></title>
      <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <script language="javascript" type="text/javascript" src="../js/master/roomcategorymaster.js"></script>
    <script type="text/javascript">

        function ShowImagePreview() {

            var preview = document.querySelector('#<%=Image1.ClientID %>');
            var file = document.querySelector('#<%=uploadLogo.ClientID %>').files[0];

        
            var reader = new FileReader();
            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }


        }
      </script>
</head>
<body>
    <form id="form1" runat="server">
         <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Package Master" />
    <div>
 <asp:ScriptManager ID="scmgrMarketMaster" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="pnlMarketMaster" runat="server">
                <ContentTemplate>

    <table>
        <tr>
            <td>Accom Type</td>
            <td>
                <asp:DropDownList ID="ddlAccomType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAccomType_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAccomType" ErrorMessage="*" InitialValue="0" ValidationGroup="Image"></asp:RequiredFieldValidator>
            </td>

        </tr>
        <tr>
            <td>Accomodation</td>
            <td>
                <asp:DropDownList ID="ddlAccom" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAccomName_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAccom" ErrorMessage="*" InitialValue="0" ValidationGroup="Image"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Room Category
            </td>
            <td>
                <asp:DropDownList ID="ddlRoomCategory" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRoomCategory" ErrorMessage="*" InitialValue="0" ValidationGroup="Image"></asp:RequiredFieldValidator>
            </td>

        </tr>

        <tr>
            <td>Image</td>
            <td> <input type="file" cssclass="appbutton" runat="server" id="uploadLogo" onchange="ShowImagePreview()"  />
   <asp:Image ID="Image1" runat="server" Width="128" Height="123" /></td>

        </tr>

        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="Image" />
                <asp:Button ID="btnReload" runat="server" Text="Reload" OnClick="btnReload_Click" /> 
                <asp:HiddenField ID="hfId" runat="server" />
            </td>
        </tr>

    </table>




    <asp:GridView ID="gdvRoomImages" runat="server" AutoGenerateColumns="False" OnRowCommand="gdvRoomImages_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="gdvRoomImages_RowDeleting" DataKeyNames="Id">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="Sno">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>

                </ItemTemplate>

            </asp:TemplateField>
            <asp:BoundField DataField="RoomCategory" HeaderText="RoomCategory" />
            <asp:ImageField DataImageUrlField="ImagePath" ControlStyle-Height="120px" ControlStyle-Width="120px">
                <ControlStyle Height="120px" Width="120px" />
            </asp:ImageField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Select">Edit</asp:LinkButton>
                    <asp:LinkButton ID="lnkDelete" runat="server" OnClientClick="return confirm('Are you certain you want to dlete this image?')" CommandName="Delete">Delete</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
                    <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>



                    </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                    <asp:AsyncPostBackTrigger ControlID="btnReload" />

                </Triggers>




                </asp:UpdatePanel>


    </div>

    
    </form>
</body>
</html>
