<%@ page language="C#" autoeventwireup="true" CodeFile="~/Hotel/HotelBooking.aspx.cs"  inherits="Hotel_HotelBooking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">


    <title>Hotel Booking</title>
    <link rel="icon" type="image/png" href="/favicon.ico" />

    <script src="js/QueryString.js" type="text/javascript"></script>
   <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" type="text/css" media="all" href="../css/calendar-blue2.css"
        title="win2k-cold-1" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/Booking.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
     <link rel="stylesheet" type="text/css" media="all" href="../css/newcss.css" />

    <script language="javascript" type="text/javascript" src="../js/calendar/calendar.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-en.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-setup.js"></script>
    <script language="javascript" type="text/javascript" src="../js/popups.js"></script>
    <script language="javascript" type="text/javascript" src="../js/global.js"></script>
    <script language="javascript" type="text/javascript" src="../js/client/booking.js"></script>
    <style type="text/css">
        .auto-style1
        {
            width: 33%;
        }
        .button-link {
    padding: 10px 15px;
    background: #4479BA;
    color: #FFF;
    font-size:medium;
}

          th {
   color:Black;font-weight:bold;
   font-family: 'Times New Roman';
    text-align:left;
     padding: 6px;
   
}

          table{
 border: 1px solid #ddd;


          }

          table tr td{

              text-align: left;
          }
        
    </style>



</head>
<body class="bg-img-H">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scrptmngr" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="upd1" runat="server">
            <ContentTemplate>



        <div>
<div class="RoomRatesBox" style="margin-bottom:25px;">



      <div class="White-Box text-center col-md-12">
            <h2>Search Results<span  class=" pull-right"> <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button-link" OnClick="LinkButton1_Click">Logout</asp:LinkButton></span></h2>
        <div class=" clearfix"> </div>
        </div>

           <div class=" clearfix"> </div>    
        
            
  <div class="RoomRatesBox2 White-Box2" style="padding:20px;">
    


      <h2 style="font-family:'Times New Roman';font-style:italic;font-size:20px"> <span id="avl" runat="server" >Available Rooms</span> </h2>
            <asp:GridView ID="gdvHotelRoomRates" runat="server"  ForeColor="#333333" GridLines="None" Font-Size="Medium" AutoGenerateColumns="False" OnRowDataBound="gdvHotelRoomRates_RowDataBound" Width="99%" >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                
                    <asp:ImageField DataImageUrlField="ImagePath" ControlStyle-Width="120px" ControlStyle-Height="120">
<ControlStyle Height="120px" Width="120px"></ControlStyle>
                    </asp:ImageField>
                
                        <asp:TemplateField   >
                        <ItemTemplate>
                            <asp:HiddenField ID="hfrctId" runat="server" Value='<%#Eval("RoomCategoryId") %>' />

                        </ItemTemplate>

                    </asp:TemplateField>

                      <asp:TemplateField  >
                        <ItemTemplate>
                            <asp:HiddenField ID="hfrtype" runat="server" Value='<%#Eval("RoomTypeId") %>' />

                        </ItemTemplate>

                    </asp:TemplateField>

                     <asp:TemplateField   >
                        <ItemTemplate>
                            <asp:HiddenField ID="hfMaxGuests" runat="server" Value='<%#Eval("MaxGuests") %>' />

                        </ItemTemplate>

                    </asp:TemplateField>

                
                    <asp:BoundField DataField="RoomCategory" HeaderText="Room"  />
                    <asp:TemplateField HeaderText="Double" >


                        <ItemTemplate >
                            <asp:DropDownList ID="ddlConvert" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlConvert_SelectedIndexChanged">
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>


                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Guests"  >
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlGuests" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGuests_SelectedIndexChanged">
                             
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="RoomType" HeaderText="Room" />

                

                    <asp:BoundField DataField="RoomDescription" HeaderText="Details"  />
                    <asp:BoundField DataField="description" HeaderText="Rate Includes" />
                    <%--  <asp:BoundField DataField="Currency" HeaderText=""  />--%>
                    <asp:BoundField DataField="Amtc"  HeaderText="Rate" DataFormatString="{0:#.##}"  />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnBook" runat="server" OnClick="btnBook_Click" Text="Book" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle CssClass="GridHeader" Font-Bold="True" ForeColor="White"  />
                <PagerStyle BackColor="#284775" ForeColor="White" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

            </asp:GridView>


    </div>
 <div class="RoomRatesBox" style="display:none">
          <h2>Meal Plan Rates</h2>
            <asp:GridView ID="gdvMealplans" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gdvMealplans_SelectedIndexChanged" Font-Size="Medium" >
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5bc0de" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>

        </div>

             <div class="RoomRatesBox" style="display:none">
            <asp:Label ID="lblMealTotal" runat="server" Text=""></asp:Label>
            <asp:GridView ID="gdvHotelServiceRates" runat="server" Caption="Service Rates" Width="50%" CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="Medium">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5bc0de" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />

            </asp:GridView>


                 <div class="Hotel-Booking-Tbl-2"> 
            <table>
                <tr>
                   
                    <td>Date
                        <asp:DropDownList ID="ddlDates" runat="server"></asp:DropDownList></td>
                   
                    <td>Plan
                        <asp:DropDownList ID="ddlMealPlan" runat="server"></asp:DropDownList></td>
                    <td>
                        <asp:Button ID="btnAddmealplan" runat="server" Text="Add" OnClick="btnAddmealplan_Click" /></td>

                </tr>

            </table>
                      </div>
                 </div>

              <div class="RoomRatesBox">
<%--          <h2>Meal Plan Rates</h2>--%>
            <asp:GridView ID="gdvselectedMealplan" runat="server" Width="50%" CellPadding="4" Font-Size="Medium" ForeColor="#333333" GridLines="None" >
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5bc0de" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            </div>
   

          <div class="RoomRatesBox">
            <%--<asp:GridView ID="gdvHotelServiceRates" runat="server" Caption="Service Rates" Width="50%" CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="Medium">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5bc0de" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />

            </asp:GridView>--%>


        <%--    <asp:Label ID="lblMealTotal" runat="server" Text=""></asp:Label>--%>


              <%--<div class="Hotel-Booking-Tbl-2">        
               


                  
            <table>
                <tr>
                   
                    <td>Date
                        <asp:DropDownList ID="ddlDates" runat="server"></asp:DropDownList></td>
                   
                    <td>Plan
                        <asp:DropDownList ID="ddlMealPlan" runat="server"></asp:DropDownList></td>
                    <td>
                        <asp:Button ID="btnAddmealplan" runat="server" Text="Add" OnClick="btnAddmealplan_Click" /></td>

                </tr>

            </table>
               


                  
                  </div>--%>     
</div>


        </div>
        
        <div class="RoomRatesBox White-Box2" runat="server" id="DivRmRate" style="padding:20px !important;">
            
            <h2 style="font-family:'Times New Roman';font-style:italic;font-size:20px">Selected Rooms</h2>

        
                
        
        <asp:GridView ID="gdvSelectedRooms" runat="server"   ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Font-Size="Medium" OnRowDataBound="gdvSelectedRooms_RowDataBound" style="width:100%" OnRowCommand="gdvSelectedRooms_RowCommand">
            <AlternatingRowStyle BackColor="White" />
            
            <Columns>
               <asp:BoundField DataField="Rooms" HeaderText="Rooms" ItemStyle-HorizontalAlign="Right" />
               
                   <asp:TemplateField>
                    <ItemTemplate>

                        <asp:HiddenField ID="hdnRooCatId" runat="server" Value='<%#Eval("RoomCategoryId") %>' />
                    </ItemTemplate>

                </asp:TemplateField>

                <asp:BoundField DataField="categoryName" HeaderText="Name" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="Pax" HeaderText="Pax" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="Price" HeaderText="Price" ItemStyle-HorizontalAlign="Right" />
             
                <asp:BoundField DataField="Nights" HeaderText="Nights" ItemStyle-HorizontalAlign="Right" />
            
                <asp:BoundField DataField="Total" HeaderText="Total"  ItemStyle-HorizontalAlign="Right"/>

                <asp:TemplateField>
                    <ItemTemplate>

                        <asp:HiddenField ID="hdnRmno" runat="server" Value='<%#Eval("RoomNo") %>' />
                    </ItemTemplate>

                </asp:TemplateField>
                   <asp:TemplateField>
                    <ItemTemplate>

                        <asp:HiddenField ID="hdnCurrency" runat="server" Value='<%#Eval("Currency") %>' />
                    </ItemTemplate>

                </asp:TemplateField>
<%--                       <asp:BoundField DataField="Currency"  ItemStyle-HorizontalAlign="Right" />--%>
                 



                     <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:ImageButton Height="25px" Width="25px" ImageUrl="~/images/delete-icon.png" ID="imgbtnDelete" CommandName="Remove" runat="server" />
                                        </ItemTemplate>


                                    </asp:TemplateField>


                  <asp:TemplateField>
                    <ItemTemplate>

                        <asp:HiddenField ID="hdnConv" runat="server" Value='<%#Eval("ConvDouble") %>' />
                    </ItemTemplate>

                </asp:TemplateField>

            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle CssClass="GridHeader" Font-Bold="True" ForeColor="White" HorizontalAlign="Right" />
            <PagerStyle BackColor="#666666" ForeColor="White"  />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>


            <div style=" padding-top:30px;" >  <div class=" clear"></div>
<div >
    <div style="float:left">
    <h2>Total : <asp:Label runat="server" ID="lblCurrency"></asp:Label>  <asp:Label runat="server" ID="lblRmRate"></asp:Label></h2> 
        </div>
   
</div>

  <div  style=" float:right">
        <asp:Button ID="Button2" runat="server" CssClass="btn btn-info rightMargin15 font16 pricePanelBtn" Text="Continue" OnClick="Button2_Click" />
        </div>
           </div> 
            <div class=" clear"></div>

           
        </div>

        <div style=" height:80px;"></div>
            
            </ContentTemplate>

        </asp:UpdatePanel>
    </form>
</body>
</html>
