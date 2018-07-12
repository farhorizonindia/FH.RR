<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HotelBooking1.aspx.cs" Inherits="Cruise_Booking_HotelBooking1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resorts a Hotels and Restaurants </title>

    <!-- Meta-Tags -->
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
    <!-- //Meta-Tags -->

    <!-- Custom-Stylesheet-Links -->
    <!-- Bootstrap-CSS -->
    <link rel="stylesheet" href="css/Newcss/bootstrap.min.css" type="text/css" media="all" />
    <!-- Index-Page-CSS -->
    <link rel="stylesheet" href="css/Newcss/style.css" type="text/css" media="all" />
    <!-- Animate.CSS -->
    <link rel="stylesheet" href="css/Newcss/animate.css" type="text/css" media="all" />
    <!-- //Custom-Stylesheet-Links -->
    <link rel="stylesheet" href="css/Newcss/cruise.css" />
    <!-- Fonts -->
    <!-- Body-Font -->
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Open+Sans:400,300,600,700,800" type="text/css" />
    <!-- Logo-Font -->
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Cinzel+Decorative:400,900,700" type="text/css" />
    <!-- Navbar-Font -->
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Montserrat:400,700" type="text/css" />
    <!-- //Fonts -->
</head>
<body>
    <form id="form1" runat="server">
        <div class="header agileits " id="home">

            <!-- Navbar -->
            <nav class="navbar navbar-default  aits wow bounceInUp agileits ">
                <div class="container">

                    <div class="navbar-header agileits ">
                        <button type="button" class="navbar-toggle agileits  collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false">
                            <span class="sr-only agileits ">Toggle navigation</span>
                            <span class="icon-bar "></span>
                            <span class="icon-bar "></span>
                            <span class="icon-bar "></span>
                        </button>
                        <a class="navbar-brand agileits " href="index.html">Resorts</a>
                    </div>

                    <div id="navbar" class="navbar-collapse agileits  navbar-right collapse">
                        <ul class="nav agileits  navbar-nav">
                            <li class="dropdown">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">LOGIN <span class="caret"></span></a>
                                <ul class="dropdown-menu">
                                    <li><a href="#">Agent</a></li>
                                    <li><a href="#">Customer</a></li>
                                </ul>
                            </li>
                        </ul>
                    </div>

                </div>
            </nav>
            <!-- //Navbar -->


            <div class="ak-available-container">

                <div class="container available">

                    <h3 class="ak-available-head">Search Results  </h3>

                    <div class="row room-detail">
                        <h3>Available Rooms</h3>
                        <asp:GridView ID="gdvHotelRoomRates" runat="server" ForeColor="#333333" GridLines="None" Font-Size="Medium" AutoGenerateColumns="False" OnRowDataBound="gdvHotelRoomRates_RowDataBound" Width="100%" OnSelectedIndexChanged="gdvHotelRoomRates_SelectedIndexChanged">
                            <Columns>

                                <asp:ImageField DataImageUrlField="ImagePath" ControlStyle-CssClass="col-sm-4">
                                    
                                    <ControlStyle CssClass="img-responsive" Height="120px" Width="120px"></ControlStyle>
                                </asp:ImageField>
                                <%-- <div class="col-sm-4">
                                    <img src="images/project-4.jpg" class="img-responsive" alt="room-">
                                </div>--%>
                                <asp:TemplateField>
                                    <ItemTemplate>

                                        <asp:HiddenField ID="hfrctId" runat="server" Value='<%#Eval("RoomCategoryId") %>' />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfrtype" runat="server" Value='<%#Eval("RoomTypeId") %>' />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfMaxGuests" runat="server" Value='<%#Eval("MaxGuests") %>' />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="col-sm-8">
                                            <div class="row">
                                                <div class="col-sm-12 top-div">
                                                    <div class="row">
                                                        <div class="col-sm-3">
                                                            <label>Room</label>
                                                            <p><%# Eval("RoomCategory") %></p>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <label>Double</label>
                                                            <div class="form-group">
                                                                <img src="images/down.png" class="img-responsive" alt="">
                                                                <asp:DropDownList ID="ddlConvert" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlConvert_SelectedIndexChanged">
                                                                    <asp:ListItem>Yes</asp:ListItem>
                                                                    <asp:ListItem>No</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<select class="form-control" id="sel1">

                                                                    <option>No</option>
                                                                    <option>Yes</option>
                                                                </select>--%>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <label>Guests</label>
                                                            <div class="form-group">
                                                                <img src="images/down.png" class="img-responsive" alt="">
                                                                <asp:DropDownList ID="ddlGuests" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGuests_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <%--<select class="form-control" id="sel1">
                                                                    <option>1</option>
                                                                    <option>2</option>
                                                                    <option>3</option>
                                                                    <option>4</option>
                                                                    <option>5</option>
                                                                    <option>6</option>
                                                                </select>--%>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <label>Room</label>
                                                            <p><%# Eval("RoomCategory") %></p>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 bottom-div">
                                                    <div class="row">
                                                        <div class="col-sm-3">
                                                            <label>Detail</label>
                                                            <p><%# Eval("RoomDescription") %></p>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <label>Rate Includes</label>
                                                            <p><%# Eval("description") %></p>

                                                        </div>
                                                        <div class="col-sm-3">
                                                            <label>Rate</label>
                                                            <p><%# Eval("Amtc") %></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Button ID="btnBook" runat="server" OnClick="btnBook_Click" Text="Book" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                        <%-- <button class="btn btn-primary wow agileits  fadeInUp" id="book-resort">BOOK<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                    </div>
                </div>

                <div id="ak-booking-select">
                </div>
                <div class="RoomRatesBox" style="display: none">
                    <h2>Meal Plan Rates</h2>
                    <asp:GridView ID="gdvMealplans" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gdvMealplans_SelectedIndexChanged" Font-Size="Medium">
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

                <div class="RoomRatesBox" style="display: none">
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
                    <asp:GridView ID="gdvselectedMealplan" runat="server" Width="50%" CellPadding="4" Font-Size="Medium" ForeColor="#333333" GridLines="None">
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

            <div class="RoomRatesBox White-Box2" runat="server" id="DivRmRate" style="padding: 20px !important;">

                <h2 style="font-family: 'Times New Roman'; font-style: italic; font-size: 20px">Selected Rooms</h2>




                <asp:GridView ID="gdvSelectedRooms" runat="server" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Font-Size="Medium" OnRowDataBound="gdvSelectedRooms_RowDataBound" Style="width: 100%" OnRowCommand="gdvSelectedRooms_RowCommand">
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

                        <asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-HorizontalAlign="Right" />

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
                    <PagerStyle BackColor="#666666" ForeColor="White" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>


                <div style="padding-top: 30px;">
                    <div class=" clear"></div>
                    <div>
                        <div style="float: left">
                            <h2>Total :
                                        <asp:Label runat="server" ID="lblCurrency"></asp:Label>
                                <asp:Label runat="server" ID="lblRmRate"></asp:Label></h2>
                        </div>

                    </div>

                    <div style="float: right">
                        <asp:Button ID="Button2" runat="server" CssClass="btn btn-info rightMargin15 font16 pricePanelBtn" Text="Continue" OnClick="Button2_Click" />
                    </div>
                </div>
                <div class=" clear"></div>


            </div>
        </div>



        <!-- Footer -->
        <div class="footer agileits ">
            <div class="container">

                <div class="col-md-6 col-sm-6 agileits  footer-grids">
                    <div class="col-md-4 col-sm-4 footer-grid agileits  footer-grid-1 wow fadeInUp">
                        <ul class="agileits ">
                            <li class="agileits ">5 Star Hotels</li>
                            <li class="agileits ">Beach Resorts</li>
                            <li class="agileits ">Beach Houses</li>
                            <li class="agileits ">Water Houses</li>
                        </ul>
                    </div>
                    <div class="col-md-4 col-sm-4 footer-grid agileits  footer-grid-2 wow fadeInUp">
                        <ul class="agileits ">
                            <li class="agileits "><a href="gallery.html">Bahamas</a></li>
                            <li class="agileits "><a href="gallery.html">Hawaii</a></li>
                            <li class="agileits "><a href="gallery.html">Miami</a></li>
                            <li class="agileits "><a href="gallery.html">Ibiza</a></li>
                        </ul>
                    </div>
                    <div class="col-md-4 col-sm-4 footer-grid agileits  footer-grid-3 wow fadeInUp">
                        <ul class="agileits ">
                            <li class="agileits "><a href="about.html">About</a></li>
                            <li class="agileits "><a href="cuisines.html">Cuisines</a></li>
                            <li class="agileits "><a href="gallery.html">Gallery</a></li>
                            <li class="agileits "><a href="booking.html">Contact</a></li>
                        </ul>
                    </div>
                    <div class="clearfix"></div>
                </div>

                <div class="col-md-6 col-sm-6 footer-grids agileits  social wow fadeInUp">
                    <ul class="social-icons">
                        <li class="agileits "><a href="#" class="facebook agileits " title="Go to Our Facebook Page"></a></li>
                        <li class="agileits "><a href="#" class="twitter agileits " title="Go to Our Twitter Account"></a></li>
                        <li class="agileits "><a href="#" class="googleplus agileits " title="Go to Our Google Plus Account"></a></li>
                        <li class="agileits "><a href="#" class="instagram agileits " title="Go to Our Instagram Account"></a></li>
                        <li class="agileits "><a href="#" class="youtube agileits " title="Go to Our Youtube Channel"></a></li>
                    </ul>
                </div>

                <div class="col-md-6 col-sm-6 footer-grids agileits  copyright wow fadeInUp">
                    <p>&copy; 2017 Resorts. All Rights Reserved | Design by</p>
                </div>
                <div class="clearfix"></div>

            </div>
        </div>
        <!-- //Footer -->
        <!-- // $$$$$$$$%%%%%%%%%%%%%%%%%%%%% pakages Details &&&&&&&&&&&&&&&&&&&&&&&&&&&&&& -->
        <!-- Default-JavaScript -->
        <script type="text/javascript" src="js/jquery-2.1.4.min.js"></script>
        <!-- Bootstrap-JavaScript -->
        <script type="text/javascript" src="js/bootstrap.min.js"></script>
        <!-- Animate.CSS-JavaScript -->
        <script src="js/wow.min.js"></script>
        <script type="js/index.js"></script>
        <script>
            new WOW().init();
        </script>
        <script type="text/javascript">
            $('#book-resort').click(function (event) {
                $('#ak-booking-select').html('<div class="container select-room"><button id="ak-booking-del" class="btn btn-default btn-lg"aria-label="Left Align"type=button><span aria-hidden=true class="glyphicon first-span glyphicon-remove-sign"></span></button><div class=select><div class="agileits wow location-grids location-grids-1 select-down slideInDown"><div class=table-responsive><table class="table table-bordered table-striped"><h3>Selected Rooms</h3><tr class=first-row><th>Rooms<th>Name<th>Pax<th>Price<th>Nights<th>Total<tr class=second-row><td>1<td>Deluxe Room<td>2<td>INR 15000<td>1<td>INR 15000</table></div></div><div class="agileits wow location-grids location-grids-1 col-md-2 slideInUp total">Total : INR 15000</div><button class="agileits wow btn btn-primary fadeInUp"><a href="booking-details.html">CONTINUE</a><span aria-hidden=true class="agileits glyphicon glyphicon-arrow-right"></span></button></div></div>');
            });
            $('.ak-available-container').on('click', '#ak-booking-del', function () {
                $('#ak-booking-select').html('');
            });
        </script>
        <!-- //Animate.CSS-JavaScript -->
    </form>
</body>
</html>
