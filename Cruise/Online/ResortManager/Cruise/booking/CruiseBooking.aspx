<%@ page language="C#" autoeventwireup="true" inherits="Cruise_booking_CruiseBooking" CodeFile="~/Cruise/booking/CruiseBooking.aspx.cs" %>
<%@ Register assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta name="author" content="Pandaw Cruises Ltd" />

    <!--<link href="../../css/style.css" rel="stylesheet" />
    <link href="../../css/newcss.css" rel="stylesheet" />-->

    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../../style.css" />
    <link href="../../css/newcss.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../../js/bootstrap.min.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/jquery-1.11.1.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.0.3/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script src="js/QueryString.js" type="text/javascript"></script>
    <title>Cruise Booking Application</title>
    <meta name="description" content="" />
    <script src="js/expedition.js" type="text/javascript"></script>
    <style>

  .polygonbooked:hover {
background: url("http://www.shortstack.com/wp-content/uploads/2016/04/shortstack-podcast-profile-2.jpg") no-repeat; /* <–copy the URL for your image and paste it here */
}

.button-link {
	padding: 10px 15px;
	background: #4479BA;
	color: #FFF;
	font-size: medium;
}
th {
	text-align: center;
}
.auto-style2 {
	height: 17px;
}
tr {
	text-align: center;
}
h1 {
	font-size: 34px;
}

        .rightalign
        {
            text-align: right;
        }

caption {
	color: black;
	font-weight: bold;
}
        .auto-style3
        {
            height: 31px;
        }
    </style>
<script>
function blockArea() {
    $('area[alt="Not Available"]').css('cursor', 'not-allowed');
}
</script>
    </head>
    <body class="bg-img1" >
    <form id="form1" runat="server">
          

      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

         
      <asp:UpdatePanel runat="server" ID="upd1">
        <ContentTemplate>
          <asp:TextBox ID="txtBookingRef" runat="server" style="display:none"  ></asp:TextBox>
          
           <div class="container">
          <div class="row">
            <div id="headings1" runat="server" class="col-md-12 text-center White-Box">
              <h2 >Select Cabin Category 
              <span class=" pull-right" >
                <asp:LinkButton ID="lnkLogout" runat="server" CssClass="button-link" BackColor="#4479BA" OnClick="lnkLogout_Click">Logout</asp:LinkButton>
                </span>
                </h2>
              <div class=" clearfix"> </div>
            </div>
            <div class="clearfix"></div>
          </div>
          </div>
          
          <div class="m2">
            <div class="booking-online-box White-Box2 padding2">
              <div class=" col-md-8" style=" margin:0 auto; float:none;">
                <div class="booking-online-left1">
                  <div id="CruisesInformation" class="booking-online">
                    <table >
                      <tr>
                        <td style="border: thin solid #fff;font-family: 'Times New Roman'; font-style: italic;font-size:16px">Please Enter the no of guests to fetch the applicable tariff </td>
                        <td><asp:TextBox ID="txtPassengers" runat="server" placeholder="No of Guests" OnTextChanged="txtPassengers_TextChanged"></asp:TextBox>
                          <%--<asp:DropDownList ID="ddlpax" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpax_SelectedIndexChanged">
                                                <asp:ListItem Value="0">-Select no of passengers-</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                            </asp:DropDownList>--%></td>
                        <td><asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" /></td>
                      </tr>
                    </table>
                    <br />

                  </div>
                  <div id="div1" runat="server" class="booking-online">
                    <table id="tblSelectSection" style=" ">
                      <tr>
                        <td colspan="4" style="font-weight:bold; font-size:16px; font-family:'Times New Roman', Times, serif;" class="auto-style2">Cabin Configuration</td>
                      </tr>
                        <tr>
                            <td colspan="4" style="font-family: 'Times New Roman'; font-style: italic;font-size: 13px;">Please enter the cabin configuration and then select the cabin no on the deck plan to book</td>

                        </tr>
                      <tr style="background-color:#EFF3FB">
                        <td>Bed Configuration</td>
                        <td><asp:DropDownList ID="ddlConvert" runat="server">
                            <asp:ListItem Value="1">Double</asp:ListItem>
                            <asp:ListItem Value="0">Twin</asp:ListItem>
                          </asp:DropDownList></td>
                        <td>Pax </td>
                        <td><asp:DropDownList ID="ddlpax1rm" runat="server">
                            <asp:ListItem>-Occupancy-</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                             <asp:ListItem>3</asp:ListItem>
                          </asp:DropDownList></td>
                      </tr>
                    </table>
                    <%-- <asp:CheckBoxList ID="ddlrooms" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList>--%>
                  </div>
                  <br />
                   

                  <div style="padding-left:8%">
                    <asp:ImageMap ID="ImageMap1"  runat="server" ImageUrl="~/images/aspnet_imagemap.png" 
            OnClick="ImageMap1_Click">
           <%--           <asp:PolygonHotSpot Coordinates="346,15,374,16,375,59,347,56" AlternateText="210"
                HotSpotMode="PostBack" PostBackValue="210" />
                      <asp:PolygonHotSpot Coordinates="373,13,413,14,410,58,371,58" AlternateText="208"
                HotSpotMode="PostBack" PostBackValue="208" />
                      <asp:PolygonHotSpot Coordinates="416,14,450,15,450,58,426,57,412,55" AlternateText="206"
                HotSpotMode="PostBack" PostBackValue="206" />
                      <asp:PolygonHotSpot Coordinates="455,15,491,15,489,55,455,57" AlternateText="204"
                HotSpotMode="PostBack" PostBackValue="204" />
                      <asp:PolygonHotSpot Coordinates="495,15,527,14,532,60,494,56" AlternateText="202"
                HotSpotMode="PostBack" PostBackValue="202" />
                      <asp:PolygonHotSpot Coordinates="202,145,236,145,237,188,204,188" AlternateText="114"
                HotSpotMode="PostBack" PostBackValue="114" />
                      <asp:PolygonHotSpot Coordinates="239,146,270,147,270,187,240,188" AlternateText="112"
                HotSpotMode="PostBack" PostBackValue="112" />
                      <asp:PolygonHotSpot Coordinates="274,154,327,156,325,176,316,177,314,188,273,188"
                AlternateText="110" HotSpotMode="PostBack" PostBackValue="110" />
                      <asp:PolygonHotSpot Coordinates="346,152,395,154,395,187,348,186" AlternateText="108"
                HotSpotMode="PostBack" PostBackValue="108" />
                      <asp:PolygonHotSpot Coordinates="400,154,450,154,449,187,398,187" AlternateText="106"
                HotSpotMode="PostBack" PostBackValue="106" />
                      <asp:PolygonHotSpot Coordinates="451,154,502,152,500,187,450,189" AlternateText="104"
                HotSpotMode="PostBack" PostBackValue="104" />
                      <asp:PolygonHotSpot Coordinates="501,153,519,153,545,153,548,146,567,152,579,161,587,171,590,178,543,178,542,188,503,187"
                AlternateText="102" HotSpotMode="PostBack" PostBackValue="102" />
                      <asp:PolygonHotSpot Coordinates="224,204,269,204,269,239,226,235" AlternateText="111"
                HotSpotMode="PostBack" PostBackValue="111" />
                      <asp:PolygonHotSpot Coordinates="272,201,317,205,316,216,324,216,330,238,272,240"
                AlternateText="109" HotSpotMode="PostBack" PostBackValue="109" />
                      <asp:PolygonHotSpot Coordinates="348,202,398,201,398,237,352,237" AlternateText="107"
                HotSpotMode="PostBack" PostBackValue="107" />
                      <asp:PolygonHotSpot Coordinates="399,203,425,205,445,205,448,236,419,239,400,237"
                AlternateText="105" HotSpotMode="PostBack" PostBackValue="105" />
                      <asp:PolygonHotSpot Coordinates="451,204,501,204,500,239,453,236" AlternateText="103"
                HotSpotMode="PostBack" PostBackValue="103" />
                      <asp:PolygonHotSpot Coordinates="502,202,542,202,542,211,590,212,588,223,579,234,562,243,549,244,540,239,524,237,508,239,502,239"
                AlternateText="101" HotSpotMode="PostBack" PostBackValue="101" />
                      <asp:PolygonHotSpot Coordinates="345,81,352,81,353,74,372,74,372,116,344,113" AlternateText="209"
                HotSpotMode="PostBack" PostBackValue="209" />
                      <asp:PolygonHotSpot Coordinates="375,73,410,72,413,115,375,115" AlternateText="207"
                HotSpotMode="PostBack" PostBackValue="207" />
                      <asp:PolygonHotSpot Coordinates="414,73,451,72,452,115,416,115" AlternateText="205"
                HotSpotMode="PostBack" PostBackValue="205" />
                      <asp:PolygonHotSpot Coordinates="453,71,491,71,493,115,452,115" AlternateText="203"
                HotSpotMode="PostBack" PostBackValue="203" />
                      <asp:PolygonHotSpot Coordinates="493,72,529,73,532,116,494,115" AlternateText="201"
                HotSpotMode="PostBack" PostBackValue="201" />--%>

                    </asp:ImageMap>
                  </div>

                  <br />
                  <div style="padding-left: 6%; " class="tbl_CB">
                    <asp:GridView ID="gdvRoomCategories"   runat="server" style="width:inherit"
                                    CellPadding="4" ForeColor="#333333" GridLines="Both" Font-Size="12px" AutoGenerateColumns="false" >
                        
                          <Columns>

                      <asp:TemplateField HeaderText="Cabin Category" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                        <ItemTemplate>
                          <asp:Label runat="server"  ID="RoomId" Text='<%#Eval("Cabin Category") %>'></asp:Label>
                        </ItemTemplate>
                      </asp:TemplateField>
                              <asp:TemplateField HeaderText="Price Per Person on Twin Sharing </br> inclusive of taxes">
                        <ItemTemplate>
                          <asp:Label runat="server"  ID="lbltws" Text='<%#Eval("Price Per Person on Twin Sharing inclusive of taxes") %>'></asp:Label>
                        </ItemTemplate>
                      </asp:TemplateField>
                              <asp:TemplateField HeaderText="Price Per Person on Single Cabin</br> inclusive of taxes">
                        <ItemTemplate>
                          <asp:Label runat="server"  ID="lblsc" Text='<%#Eval("Price Per Person on Single Cabin inclusive of taxes") %>'></asp:Label>
                        </ItemTemplate>
                      </asp:TemplateField>
                              </Columns>
                      <AlternatingRowStyle BackColor="White" />
                      <EditRowStyle BackColor="#2461BF" />
                      <FooterStyle BackColor="#5bc0de" Font-Bold="True" ForeColor="Black" />
                      <HeaderStyle CssClass="GridHeader" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                      <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                      <RowStyle BackColor="#EFF3FB" />
                      <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                      <SortedAscendingCellStyle BackColor="#F5F7FB" />
                      <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                      <SortedDescendingCellStyle BackColor="#E9EBEF" />
                      <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                  </div>
                  <br />
                  <div id="Div1" class="box box-primary booking-online">
                    <asp:GridView ID="GridRoomPaxDetail" ShowFooter="true" AutoGenerateColumns="False" runat="server" Caption="Cabin Details" OnRowDeleting="GridRoomPaxDetail_RowDeleting" OnRowCommand="GridRoomPaxDetail_RowCommand" OnRowDataBound="GridRoomPaxDetail_RowDataBound" Font-Size="12px">
                      <HeaderStyle ForeColor="Black" />
                      <Columns>
                      <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                          <asp:Label runat="server"  ID="RoomId" Text='<%#Eval("RoomNumber") %>'></asp:Label>
                        </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Category">
                        <ItemTemplate>
                          <asp:Label runat="server" ID="RoomCategoryId" Visible="false" Text='<%#Eval("RoomCategoryId") %>'></asp:Label>
                          <asp:Label runat="server" ID="lbcategoryName" Text='<%#Eval("categoryName") %>'></asp:Label>
                        </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Bed Configuration"  >
                        <ItemTemplate>
                          <asp:Label runat="server" ID="BedConfig" Text='<%#Eval("Bed Configuration") %>'></asp:Label>
                        </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="No of Guests">
                        <ItemTemplate>
                          <asp:Label runat="server" ID="Pax" Text='<%#Eval("Pax") %>'></asp:Label>
                        </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField Visible="false" >
                        <ItemTemplate>
                          <asp:Label runat="server" ID="lblCurr" Text='<%#Eval("Currency") %>'></asp:Label>
                        </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField   HeaderText="Price" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                          <asp:Label runat="server" ID="Price" Text='<%#Eval("CRPrice") %>'></asp:Label>
                        </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Tax" Visible="false">
                        <ItemTemplate>
                          <asp:Label runat="server" ID="Tax" Text='<%#Eval("Tax") %>'></asp:Label>
                        </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                          <asp:ImageButton  Width="25px" ImageUrl="~/images/delete-icon.png" ID="imgbtnDelete" CommandName="Remove" runat="server" />
                        </ItemTemplate>
                      </asp:TemplateField>
                      </Columns>
                      <HeaderStyle CssClass="GridHeader" Font-Bold="True" />
                      <AlternatingRowStyle BackColor="White" />
                      <RowStyle BackColor="#EFF3FB" />
                        <FooterStyle CssClass="rightalign"  />
                    </asp:GridView>
                    <p id="pMessages" runat="server" style="margin-left: 71px;">
                    <table style="width:53%;color:white; margin:0 119px 0 0; float:right;" >
                      <tr>
                        <td><asp:Label ID="TotalCabins" runat="server" Font-Bold="True" Visible="false" Font-Size="Medium"></asp:Label></td>
                        <td><asp:Label ID="lblTotalCabins" runat="server" Font-Bold="True" Visible="false" Font-Size="Medium"></asp:Label></td>
                        <td><asp:Label ID="lblTotal" runat="server" Font-Bold="True" Visible="false" Font-Size="Large"></asp:Label></td>
                        <td><asp:Label ID="lblCurr" runat="server" Font-Bold="True" Visible="false" Font-Size="Medium"></asp:Label>
                          <asp:Label ID="lblTotAmt" runat="server" Font-Bold="True" Visible="false"  Font-Size="Medium"></asp:Label></td>
                      </tr>
                    </table>
                    
                    <div class=" clearfix"></div>
                    </p>
                  </div>
                  
                  <!-- Cruise Prices - END --> 
                  
                  <!-- Selected Cabins - START -->
                  
                  <div class="text-right btncox" id="ButtonsDiv" runat="server">
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-info rightMargin15 font16 pricePanelBtn" Text="Back" OnClick="Button1_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" CssClass="btn btn-info rightMargin15 font16 pricePanelBtn" Text="Next" OnClick="Button2_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="Button3" runat="server" CssClass="btn btn-info rightMargin15 font16 pricePanelBtn" Text="Reload" OnClick="Button3_Click" />
                  </div>
                  <asp:GridView ID="GridView1" Visible="false" runat="server"
                                OnRowDataBound="GridView1_RowDataBound" ShowFooter="True" CellPadding="4" ForeColor="#333333" GridLines="Both">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
                  <asp:HiddenField ID="hfRoomId" runat="server" />
                </div>
                <div class="clear"></div>
              </div>
              <div class="clear"></div>
            </div>
          </div>
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="ImageMap1"  />
        </Triggers>
      </asp:UpdatePanel>

          <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="200">
            <ProgressTemplate>
                <iframe id="pgrIFrame" frameborder="0" marginheight="0" marginwidth="0" scrolling="no" 
                            src="javascript:'<html></html>';" style="position:absolute; 
                            top:342px; left:334px; height:45px; width:208px; z-index:19999">

                </iframe>
                    <asp:Panel ID="Panel1" runat="server"   Height="100" Style="z-index:20000;    margin-left: 295px;margin-top: 90px;" Width="300"  >
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
