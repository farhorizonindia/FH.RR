<%@ page language="C#" autoeventwireup="true" CodeFile="RoomMaster.aspx.cs" Inherits="MasterUI_RoomMaster"%>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Room Master</title>    
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />    
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />    
    <script language="javascript" type="text/javascript" src="../js/master/roommaster.js"></script>    
    <script language="javascript" type="text/javascript" src="../js/global.js"></script>    
    <style type="text/css">
        .style1
        {
            width: 24px;
        }
        .auto-style1
        {
            height: 20px;
            width: 228px;
        }
        .auto-style2
        {
            height: 11px;
            width: 228px;
        }
        .auto-style3
        {
            height: 18px;
            width: 228px;
        }
        .auto-style4
        {
            width: 228px;
        }
    </style>
  

</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="Room Master" />
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <asp:UpdatePanel ID="pnlRoomMaster" runat="server">
   <ContentTemplate>
    <div>       
    <table id="filtersection" class="filtersection" style="font-size: small; font-family: Verdana; ">
        <tr>
            <td align="left" style="height: 18px; font-size: 8pt; width: 120px; text-align: left;">
                Accomodation Type:</td>
            <td style="width: 104px; height: 18px; font-size: 8pt;">
                <asp:DropDownList cssclass="select" ID="ddlAccomType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAccomType_SelectedIndexChanged" Width="196px" >
                </asp:DropDownList></td>
            <td style="height: 18px; font-size: 8pt; width: 91px; text-align: left;">
                Accomodation:</td>
            <td style="height: 18px; font-size: 8pt;">
                <asp:DropDownList cssclass="select" ID="ddlAccomodation" runat="server" TabIndex="1">
                    <asp:ListItem Value="0">Choose Accomodation</asp:ListItem>
                </asp:DropDownList></td>
            <td style="height: 18px; font-size: 8pt; width: 48px;">
                <asp:Button cssclass="appbutton" ID="btnShow" runat="server" OnClick="btnSearch_Click" Text="Show"
                                TabIndex="2" /></td>
        </tr>
    </table>            
    <div id="gridsection" class="gridsection">
        <asp:DataGrid ID="dgRooms" runat="server" AutoGenerateColumns="False"
            BorderStyle="Ridge" CellPadding="4" DataKeyField="RoomNo" ForeColor="#333333"
            GridLines="None" OnSelectedIndexChanged="dgRooms_SelectedIndexChanged"
            Width="907px" TabIndex="3" AllowPaging="True" OnPageIndexChanged="dgRooms_PageIndexChanged" Height="231px" Font-Size="Small">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditItemStyle BackColor="#2461BF" />
            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#EFF3FB" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:BoundColumn DataField="RoomNo" HeaderText="Room No">
                    <HeaderStyle Width="50px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AccomodationId" HeaderText="AccomodationId" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="RoomCategory" HeaderText="Category">
                    <HeaderStyle Width="250px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="RoomType" HeaderText="Type">
                    <HeaderStyle Width="100px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="No_of_Beds" HeaderText="Total Beds">
                    <HeaderStyle Width="70px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ExtraBeds" HeaderText="Extra Beds">
                    <HeaderStyle Width="70px" />
                </asp:BoundColumn>
                <asp:ButtonColumn CommandName="Select" HeaderText="[...]" Text="Edit"></asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    <table id="inputsection" class="inputsection">
        <tr>
            <td style="height: 18px; font-size: 8pt; width: 91px; text-align: left;">
                &nbsp;</td>
            <td class="auto-style1">
                <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
            </td>            
        </tr>
        <tr>
            <td style="height: 18px; font-size: 8pt; width: 91px; text-align: left;">
                Room Category:</td>
            <td class="auto-style1">
                <asp:DropDownList ID="ddlRoomCategory" runat="server" cssclass="select" 
                    Font-Size="8pt" OnSelectedIndexChanged="ddlRoomCategory_SelectedIndexChanged" 
                    TabIndex="4" Width="221px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="height: 18px; font-size: 8pt; width: 91px; text-align: left;">
                Room Type:</td>
            <td class="auto-style2">
                <asp:DropDownList cssclass="select" ID="ddlRoomType" runat="server" Width="221px" TabIndex="5" AutoPostBack="True" OnSelectedIndexChanged="ddlRoomType_SelectedIndexChanged" Font-Size="8pt">
            </asp:DropDownList></td>
            <td style="height: 18px; font-size: 8pt; width: 91px; text-align: left;">
                Default Beds:
            </td>
            <td style="height: 11px;">
                <asp:TextBox cssclass="input"  ID="txtDefaultNoOfBeds"  ReadOnly="true" runat="server" TabIndex="8" Width="40px" Font-Size="8pt"></asp:TextBox>
            </td>           
        </tr>
        <tr>
            <td style="height: 18px; font-size: 8pt; width: 91px; text-align: left;">
                Floor:</td>
            <td class="auto-style3">
                <asp:DropDownList cssclass="select" ID="ddlFloors" runat="server" Width="155px" TabIndex="6" Font-Size="8pt">
                </asp:DropDownList></td>                
        </tr>
        <tr>
            <td style="height: 18px; font-size: 8pt; width: 91px; text-align: left;">
                Room No:</td>
            <td class="auto-style3">
                <asp:TextBox cssclass="input"  ID="txtRoomNo" runat="server" MaxLength="15" TabIndex="7" Font-Size="8pt"></asp:TextBox></td>            
        </tr>
        <tr>
            <td style="font-size: 8pt; width: 91px; height: 18px; text-align: left">
                Extra Beds:</td>
            <td class="auto-style4"><asp:DropDownList cssclass="select" ID="ddlExtraBeds" runat="server" Width="95px" TabIndex="8" AutoPostBack="True" OnSelectedIndexChanged="ddlExtraBeds_SelectedIndexChanged" Font-Size="8pt">
            </asp:DropDownList></td>
            <td style="font-size: 8pt; width: 91px; height: 18px; text-align: left">
                Extra Bed Rate:</td>
            <td>
                <asp:TextBox cssclass="input"  ID="txtExtraBedRate" runat="server" Enabled="False" TabIndex="9" Font-Size="8pt"></asp:TextBox></td>            
        </tr>
        <tr>
            <td style="height: 18px; font-size: 8pt; width: 91px; text-align: left;">
                Total
                No Of Beds:</td>
            <td class="auto-style3">
                <asp:TextBox cssclass="input"  ID="txtNoOfBeds" runat="server" Enabled="False" TabIndex="10" Width="89px" Font-Size="8pt"></asp:TextBox></td>
            <td style="height: 18px;">
                <asp:Label ID="lblConvert" runat="server" Text="Convert" Font-Size="8pt" Width="73px"></asp:Label></td>
            <td style="height: 18px;"><asp:DropDownList cssclass="select" ID="ddlConvert" runat="server" Width="95px" TabIndex="11" AutoPostBack="True" OnSelectedIndexChanged="ddlExtraBeds_SelectedIndexChanged" Font-Size="8pt">
            </asp:DropDownList></td>            
        </tr>
        <tr>
            <td style="vertical-align:top; font-size: 8pt; width: 91px; height: 18px; text-align: left;">
                Description:</td>
            <td style="width: 228px" colspan="3">
                <asp:TextBox cssclass="input"  ID="txtDescription" runat="server" Rows="5" TextMode="MultiLine" Width="445px" TabIndex="12" Font-Size="8pt" Height="30px"></asp:TextBox>&nbsp;
            </td>            
        </tr>
     
        <tr>
            <td></td>
            <td class="auto-style4">
                <asp:CheckBox ID="chkMainten" runat="server" Text="Closed For Maintenance" Visible="false" />

            </td>

        </tr>
    </table>        
    <table id="buttonsection" class="buttonsection">
    <tr>
        <td style="width: 74px; height: 26px;">
            <asp:Button cssclass="appbutton" ID="btnEdit" runat="server" Height="24px" OnClick="btnEdit_Click" Text="Update"
                Width="65px" TabIndex="13" /></td>
        <td style="width: 74px; height: 26px;">
            <asp:Button cssclass="appbutton" ID="btnDelete" runat="server" Height="24px" OnClick="btnDelete_Click"
                Text="Delete" Width="65px" TabIndex="14" /></td>
        <td style="width: 74px; height: 26px;">
            <asp:Button cssclass="appbutton" ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                Width="65px" TabIndex="15" /></td>            
    </tr>
    </table>
    <table id="hiddensection" class="hiddensection">
    <tr>
        <td><asp:HiddenField ID="hfAccomId" runat="server" />
            </td>
            <td><asp:HiddenField ID="hfRoomNo" runat="server" /></td>
     </tr>
    </table>
    <table>
        <tr>
        <td>            
            <asp:Button cssclass="appbutton" ID="btnAddNew" runat="server" Height="24px" OnClick="btnAddNew_Click"
            Text="New" Width="65px" TabIndex="13" Visible="False" />
            <asp:Button cssclass="appbutton" ID="btnSave" runat="server" Height="24px" OnClick="btnSave_Click" Text="Save"
            Width="65px" TabIndex="15" Visible="False" />
            </td>
        </tr>          
    </table>
    </div>
    </ContentTemplate>
       <Triggers>

           <asp:PostBackTrigger ControlID="btnEdit" />
       </Triggers>
    </asp:UpdatePanel>
    </form>
           
</body>
</html>
