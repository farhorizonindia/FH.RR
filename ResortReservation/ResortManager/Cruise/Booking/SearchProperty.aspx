<%@ Page Language="C#" AutoEventWireup="true" CodeFile="searchproperty.aspx.cs" Inherits="Cruise_Booking_searchproperty" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resorts a Hotels and Restaurants </title>

    <!-- Meta-Tags -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
    <!-- //Meta-Tags -->
    <%--  --%>
    <!-- Custom-Stylesheet-Links -->
    <!-- Bootstrap-CSS -->
<<<<<<< HEAD
    <link rel="stylesheet" href="css/Newcss/bootstrap.min.css" type="text/css" media="all"/>
    <!-- Index-Page-CSS -->
    <link href="css/Newcss/style.css" rel="stylesheet" />

    <!-- Index-Page-CSS -->
    <link rel="stylesheet" href="css/Newcss/jquery-ui.css" type="text/css" media="all"/>
    <!-- Animate.CSS -->
    <link rel="stylesheet" href="css/Newcss/animate.css" type="text/css" media="all"/>
=======
    <link rel="stylesheet" href="css/Newcss/bootstrap.min.css" type="text/css" media="all">
    <!-- Index-Page-CSS -->
    <link rel="stylesheet" href="css/Newcss/style.css" type="text/css" media="all">
    <!-- Index-Page-CSS -->
    <link rel="stylesheet" href="css/Newcss/jquery-ui.css" type="text/css" media="all">
    <!-- Animate.CSS -->
    <link rel="stylesheet" href="css/Newcss/animate.css" type="text/css" media="all">
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
    <!-- //Custom-Stylesheet-Links -->

    <!-- Fonts -->
    <!-- Body-Font -->
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Open+Sans:400,300,600,700,800" type="text/css">
    <!-- Logo-Font -->
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Cinzel+Decorative:400,900,700" type="text/css">
    <!-- Navbar-Font -->
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Montserrat:400,700" type="text/css">
    <!-- //Fonts -->
</head>
<body>
    <div class="loaderbody">
        <div class="loader">
            <img src="../../images/loading1.gif" alt="Loading..." />
            Please Wait</div>
    </div>
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
<<<<<<< HEAD
                        <a class="navbar-brand agileits " id="logoheading" runat="server" href=" ">Booking System</a>
=======
                        <a class="navbar-brand agileits " href="searchproperty.aspx">Booking System</a>
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
                    </div>

                    <div id="navbar" class="navbar-collapse agileits  navbar-right collapse">
                        <asp:Label ID="lblUsername" runat="server" Font-Bold="true" ForeColor="Red" Text=" "></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button-link" OnClick="LinkButton1_Click">Logout</asp:LinkButton>
                        <ul class="nav agileits  navbar-nav" runat="server" id="navlogin">
                            <li class="dropdown">
                                <a id="lblLoginas" runat="server" href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">LOGIN <span class="caret"></span></a>
                                <ul class="dropdown-menu">
                                    <li><a runat="server" id="lnkLogin" href="agentLogin.aspx">Partner</a></li>
                                    <li><a runat="server" id="lnkCustomerRegis" href="../Masters/NewRegister.aspx">Customer </a>


                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </div>

                </div>
            </nav>
            <!-- //Navbar -->


            <div class="ak-banner-booking">
                <!-- Slider1 -->
                <div class="slider agileits ">

                    <div class="slider-1 agileits ">

                        <ul class="rslides agileits " id="slider1">
                            <asp:Repeater ID="rpt1" runat="server">
                                <ItemTemplate>
                                    <li>
                                      <%--  <img style="width: 1700px; height: 700px;" src='<%#Eval("Image") %>' alt="Agileits ">--%>
                                          <img style="width: 100%;" src='<%#Eval("Image") %>' alt="Agileits ">
                                        <div class="layer agileits "></div>
                                        <div class="caption agileits ">
                                            <!-- <h3>Welcome To <span>TROPICAL RESORTS</span></h3> -->
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                            <%--   <li>
                                <img src="images/slide-1.jpg" alt="Agileits ">
                                <div class="layer agileits "></div>
                                <div class="caption agileits ">
                                    <!-- <h3>Welcome To <span>TROPICAL RESORTS</span></h3> -->
                                </div>
                            </li>
                            <li>
                                <img src="images/slide-2.jpg" alt="Agileits ">
                                <div class="caption agileits ">
                                    <!-- <h3>Choose The Best Resort For You</h3> -->
                                </div>
                            </li>
                            <li>
                                <img src="images/slide-3.jpg" alt="Agileits ">
                                <div class="caption agileits ">
                                    <!-- <h3>Stay Right Next To The Exotic Beaches</h3> -->
                                </div>
                            </li>
                            <li>
                                <img src="images/slide-4.jpg" alt="Agileits ">
                                <div class="caption agileits ">
                                    <!-- <h3>Spend The Best Moments In Our Resorts</h3> -->
                                </div>
                            </li>
                            <li>
                                <img src="images/slide-5.jpg" alt="Agileits ">
                                <div class="layer agileits "></div>
                                <div class="caption agileits ">
                                    <!-- <h3>Experience The Best Luxury & Hospitality</h3> -->
                                </div>
                            </li>--%>
                        </ul>
                    </div>
                </div>
                <!-- //Slider1 -->
                <div class="clearfix"></div>
                <!-- ##############SEARCH ACCOMODATION############# -->

                <div class=" avilable">
                    <h1>Search</h1>
                    <div>
                        <div>

                            <!-- Nav tabs -->
                            <%--<asp:RadioButtonList class="nav ak-nav nav-tabs" OnSelectedIndexChanged="rbtnSelectAccomtype_SelectedIndexChanged" role="tablist" ID="rbtnSelectAccomtype" runat="server"  RepeatDirection="Horizontal" AutoPostBack="True">
                                        <asp:ListItem>Cruise</asp:ListItem>
                                        <asp:ListItem>Other</asp:ListItem>
                                    </asp:RadioButtonList>--%>
                            <ul class="nav ak-nav nav-tabs" role="tablist">

                                <li id="cruise" runat="server" class="active current " data-tab="nav-1">Cruise</li>
                                <li id="othr" runat="server" data-tab="nav-2">Other</li>
                            </ul>


                            <!-- Tab panes -->
                        </div>
                    </div>
                    <br />

                    <div class="row cruise-trip nav-content current" id="nav-1">

                        <div class="col-sm-12 booking-div">
                            <div class="dropdown">
                                <!-- <button class="btn btn-default dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
						    Dropdown
						    <span class="caret"></span>
						  </button>
						  <ul class="dropdown-menu" aria-labelledby="dropdownMenu1">
						    <li><a href="#">7 night 8 day MV Mahabaahu Upstream Cruise</a></li>
						    <li><a href="#">7 night 8 day MV Mahabaahu Downstream Cruise</a></li>
						    <li><a href="#">5 night 6 day MV Mahabaahu Upstream Cruise</a></li>
						    <li><a href="#">3 night 4 day MV Mahabaahu Downstream Cruise</a></li>
						     <li><a href="#">2 night 3 day MV Mahabaahu Upstream Cruise</a></li>
						     <li><a href="#">4 night 5 day MV Mahabaahu Downstream Cruise</a></li>
						  </ul> -->
                                <asp:DropDownList Enabled="false" ID="ddlDestination" Visible="false" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:DropDownList class="form-control" ID="ddlPackege" runat="server">
                                </asp:DropDownList>
                                <%-- <select class="form-control" id="sel1">
                                    <option>7 night 8 day MV Mahabaahu Upstream Cruise</option>
                                    <option>7 night 8 day MV Mahabaahu Downstream Cruise</option>
                                    <option>5 night 6 day MV Mahabaahu Upstream Cruise</option>
                                    <option>3 night 4 day MV Mahabaahu Downstream Cruise</option>
                                    <option>2 night 3 day MV Mahabaahu Upstream Cruise</option>
                                    <option>4 night 5 day MV Mahabaahu Downstream Cruise</option>
                                </select>--%>
                            </div>
                            <div class="dropdown">
                                <asp:DropDownList class="form-control" ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                </asp:DropDownList>
                                <%--<select class="form-control" id="sel1">
                                    <option>--Year--</option>
                                    <option>2017</option>
                                    <option>2018</option>
                                    <option>2019</option>
                                </select>--%>
                            </div>
                            <div class="dropdown">
                                <asp:DropDownList class="form-control" ID="ddlDates" runat="server">
                                    <%-- <asp:ListItem Text="Month"></asp:ListItem>
                                    <asp:ListItem Text="Jan"></asp:ListItem>
                                    <asp:ListItem Text="Feb"></asp:ListItem>
                                    <asp:ListItem Text="Mar"></asp:ListItem>
                                    <asp:ListItem Text="Apr"></asp:ListItem>
                                    <asp:ListItem Text="May"></asp:ListItem>
                                    <asp:ListItem Text="Jun"></asp:ListItem>
                                    <asp:ListItem Text="Jul"></asp:ListItem>
                                    <asp:ListItem Text="Aug"></asp:ListItem>
                                    <asp:ListItem Text="Sept"></asp:ListItem>
                                    <asp:ListItem Text="Oct"></asp:ListItem>
                                    <asp:ListItem Text="Nov"></asp:ListItem>
                                    <asp:ListItem Text="Dec"></asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlRiver" Visible="false" runat="server">
                                </asp:DropDownList>
                                <%--<select class="form-control" id="sel1">
                                    <option>Month</option>
                                    <option>Jan</option>
                                    <option>Feb</option>
                                    <option>Mar</option>
                                    <option>Apr</option>
                                    <option>May</option>
                                    <option>Jun</option>
                                    <option>Jul</option>
                                    <option>Aug</option>
                                    <option>Sep</option>
                                    <option>Oct</option>
                                    <option>Nov</option>
                                    <option>Dec</option>
                                </select>--%>
                            </div>

                            <asp:Button ID="btnSearch" type="button" runat="server" class="btn btn-primary" hover="Orange" OnClick="btnSearch_Click" Text="Search" />
                        </div>
                        <%-- <div class="col-sm-3 search-btn">
                            
                            <%-- <a href="cruise.html" title="">
                                        <button type="button" class="btn btn-primary">Search</button></a>--%>
                        <%--</div>--%>
                    </div>
                    <!--############# OTHER TAB SECTION START ###############  -->

                    <div class="row other-tab nav-content " id="nav-2" style="margin-left: 0; margin-right: 0;">
                        <div class="col-sm-12" style="display: inline-flex;">
                            <%-- <li>
                                <div class="dropdown">
                                    <asp:DropDownList class="form-control" ID="ddlAccomType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAccomType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <%-- <select class="form-control" id="sel1">
					  				<option>--Select--</option>
							    	<option>Houseboats</option>
							    	<option>Resort</option>
							    	<option>Retreat</option>
							  	</select>--%>
                            <%-- </div>
                            </li>--%>

                            <div class="dropdown" style="width: 69%; margin-right: 1rem;">
                                <asp:DropDownList class="form-control" ID="ddlAccomodationName" runat="server">
                                </asp:DropDownList>
                                <%-- <select class="form-control" id="sel1">
                                        <option>--Select--</option>
                                        <option>Vaikundam</option>
                                        <option>Sauvernigam</option>

                                    </select>--%>
                            </div>




                            <div class="book-pag-frm1 agileits ">
                                <!-- <label>Check In</label> -->
                                <asp:TextBox class="date agileits " runat="server" ID="datepicker1" type="text" value="Date" required=""></asp:TextBox>
                                <%-- <input class="date agileits " id="datepicker1" type="text" value="Date" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = '';}" required="">--%>
                            </div>

                            <div class="clearfix"></div>


                            <div class="book-pag-frm2 agileits ">
                                <!-- <label>Check Out</label> -->
                                <asp:TextBox class="date agileits " runat="server" ID="datepicker2" type="text" value="Date" required=""></asp:TextBox>
                                <%-- <input class="date agileits " id="datepicker2" type="text" value="Date" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = '';}" required="">--%>
                            </div>
                            <div class="clearfix"></div>

                            <%-- <li>
                                <!-- <label>No of Rooms:</label> -->
                                <div class="dropdown">
                                    <asp:DropDownList ID="ddlNoofrooms" class="form-control" runat="server" Width="95%" AutoPostBack="True" OnSelectedIndexChanged="ddlNoofrooms_SelectedIndexChanged">
                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" ValidationGroup="Search" ControlToValidate="ddlNoofrooms" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <%-- <select class="form-control" id="sel1">
                                        <option>--Select Room--</option>
                                        <option>1</option>
                                        <option>2</option>
                                        <option>3</option>
                                        <option>4</option>
                                        <option>5</option>

                                    </select>--%>
                            <%--</div>
                            </li>--%>


                            <a href="available.html" title="">
                                <asp:Button class="btn btn-primary agileits  " data-toggle="modal" data-target="#myModal3" ID="btnSearchOthAccom" runat="server" ValidationGroup="Search" Text="Check Availability" OnClick="btnSearchOthAccom_Click" />
                                <%--<button class="btn  agileits  " id="btnSearchOthAccom" runat="server" data-toggle="modal"  data-target="#myModal3">Check Availability--%></a>


                        </div>
                        <div class="grid-room" style="width: 50%;">
                            <asp:GridView ID="gdvRooms" runat="server" AutoGenerateColumns="False" Width="55%" CellPadding="4" BackColor="#ffffff" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Rooms">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Guests">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlGuests" runat="server">
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#8fdde4" Font-Bold="True" ForeColor="#222" />
                                <%-- <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />--%>
                            </asp:GridView>
                        </div>

                    </div>

                </div>

            </div>

            <%--<Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            </Triggers>--%>

            <!-- ##############OTHER TAB SECTION  END ############# -->
        </div>


        <!-- //Header -->



        <!-- Projects -->
        <%--<div class="projects agileits ">
            <div class="container">

                <div class="col-md-8 col-sm-8 projects-grid agileits  projects-grid1 wow slideInLeft">
                    <!-- Slider2 -->
                    <div class="slider-2 agileits ">
                        <ul class="rslides agileits " id="slider2">
                            <li>
                                <img src="images/project-1.jpg" alt="Agileits ">
                            </li>
                            <li>
                                <img src="images/project-2.jpg" alt="Agileits ">
                            </li>
                            <li>
                                <img src="images/project-3.jpg" alt="Agileits ">
                            </li>
                            <li>
                                <img src="images/project-4.jpg" alt="Agileits ">
                            </li>
                            <li>
                                <img src="images/project-5.jpg" alt="Agileits ">
                            </li>
                        </ul>
                    </div>
                    <!-- //Slider2 -->

                    <!-- Slider3 -->
                    <div class="slider-3 agileits ">
                        <ul class="rslides agileits " id="slider3">
                            <li>
                                <img src="images/project-6.jpg" alt="Agileits ">
                            </li>
                            <li>
                                <img src="images/project-7.jpg" alt="Agileits ">
                            </li>
                            <li>
                                <img src="images/project-8.jpg" alt="Agileits ">
                            </li>
                            <li>
                                <img src="images/project-9.jpg" alt="Agileits ">
                            </li>
                            <li>
                                <img src="images/project-10.jpg" alt="Agileits ">
                            </li>
                        </ul>
                    </div>
                    <!-- //Slider3 -->
                </div>

                <div class="col-md-4 col-sm-4 projects-grid agileits  projects-grid2 wow slideInRight">
                    <h1>Featured Resorts</h1>
                    <h4>BEST BEACH RESORTS</h4>
                    <div class="h4-underline agileits  wow slideInLeft"></div>
                    <p>Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.</p>
                    <a class="agileits  slideInLeft" href="gallery.html">Read More <span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></a>
                </div>

            </div>
        </div>--%>
        <!-- //Projects -->











        <!-- Footer -->
        <%--<div class="footer agileits ">
            <div class="container">

                <div class="col-md-6 col-sm-6 agileits  footer-grids">
                    <div class="col-md-4 col-sm-4 footer-grid agileits  footer-grid-1 wow fadeInUp">
                        <ul class="agileits ">
                            <%--<li class="agileits ">5 Star Hotels</li>
                            <li class="agileits ">Beach Resorts</li>
                            <li class="agileits ">Beach Houses</li>
                            <li class="agileits ">Water Houses</li>--%>
        <%--    </ul>
                    </div>
                    <div class="col-md-4 col-sm-4 footer-grid agileits  footer-grid-2 wow fadeInUp">
                        <ul class="agileits ">--%>
        <%--  <li class="agileits "><a href="gallery.html">Bahamas</a></li>
                            <li class="agileits "><a href="gallery.html">Hawaii</a></li>
                            <li class="agileits "><a href="gallery.html">Miami</a></li>
                            <li class="agileits "><a href="gallery.html">Ibiza</a></li>--%>
        <%-- </ul>
                    </div>
                    <div class="col-md-4 col-sm-4 footer-grid agileits  footer-grid-3 wow fadeInUp">
                        <ul class="agileits ">--%>
        <%-- <li class="agileits "><a href="about.html">About</a></li>
                            <li class="agileits "><a href="cuisines.html">Cuisines</a></li>
                            <li class="agileits "><a href="gallery.html">Gallery</a></li>
                            <li class="agileits "><a href="booking.html">Contact</a></li>--%>
        <%-- </ul>
                    </div>
                    <div class="clearfix"></div>
                </div>

                <div class="col-md-6 col-sm-6 footer-grids agileits  social wow fadeInUp">
                    <ul class="social-icons">--%>
        <%--  <li class="agileits "><a href="#" class="facebook agileits " title="Go to Our Facebook Page"></a></li>
                        <li class="agileits "><a href="#" class="twitter agileits " title="Go to Our Twitter Account"></a></li>
                        <li class="agileits "><a href="#" class="googleplus agileits " title="Go to Our Google Plus Account"></a></li>
                        <li class="agileits "><a href="#" class="instagram agileits " title="Go to Our Instagram Account"></a></li>
                   <%--     <li class="agileits "><a href="#" class="youtube agileits " title="Go to Our Youtube Channel"></a></li>--%>
        <%--</ul>
                </div>

                <div class="col-md-6 col-sm-6 footer-grids agileits  copyright wow fadeInUp">
                    <p>&copy; 2017 Resorts. All Rights Reserved | Design by</p>
                </div>
                <div class="clearfix"></div>

            </div>
        </div>--%>
        <!-- //Footer -->



        <!-- Custom-JavaScript-File-Links -->

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
        <!-- //Animate.CSS-JavaScript -->

        <!-- Slider-JavaScript -->
        <script src="js/responsiveslides.min.js"></script>
        <script>

            $(function () {
                $("#slider1, #slider2, #slider3, #slider4").responsiveSlides({
                    auto: true,
                    nav: true,
                    speed: 1500,
                    namespace: "callbacks",
                    pager: true,
                });
            });
        </script>
        <!-- //Slider-JavaScript -->



        <script type="text/javascript">
            $(document).ready(function () {

                $('ul.nav.ak-nav li').click(function () {
                    var nav_id = $(this).attr('data-tab');

                    $('ul.nav li').removeClass('current');
                    $('.nav-content').removeClass('current');

                    $(this).addClass('current');
                    $("#" + nav_id).addClass('current');
                    localStorage.setItem('activeTab', $(this).text());
                })


                var activeTab = localStorage.getItem('activeTab');

                if (activeTab == 'Cruise') {
                    $('#cruise').trigger('click');
                } else if (activeTab == 'Other') {
                    $('#othr').trigger('click');
                }
                var getUrlParameter = function getUrlParameter(sParam) {
                    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
                        sURLVariables = sPageURL.split('&'),
                        sParameterName,
                        i;

                    for (i = 0; i < sURLVariables.length; i++) {
                        sParameterName = sURLVariables[i].split('=');

                        if (sParameterName[0] === sParam) {
                            return sParameterName[1] === undefined ? true : sParameterName[1];
                        }
                    }
                };
                var prop = getUrlParameter('Prop');
                console.log(prop);
                if (prop) {
                    $('#othr').trigger('click');
                }
            })
        </script>

        <!-- Date-Picker-JavaScript -->
        <script src="js/jquery-ui.js">


            var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};
            var prop = getUrlParameter('Prop');
            if(prop!="")
            {
            $('#othr').trigger('click');
            }
        </script>

        <%--<script>
            $(function () {
                $("#datepicker,#datepicker1,#datepicker2").datepicker();
            });
        </script>--%>
        <script>
            localStorage.clear("get");
            localStorage.clear('activeTab12');
            $(function () {
                //$("#datepicker1").datepicker({ dateFormat: "dd-mm-yy" }).val()
                //$("#datepicker2").datepicker({ dateFormat: "dd-mm-yy" }).val()
                //var dates = $('#datepicker2, #datepicker1').datepicker({
                //    onSelect: function (selectedDate) {
                //        var option = this.id == "datepicker1" ? "minDate" : "maxDate";
                //        dates.not(this).datepicker("option", option, $(this).datepicker('getDate'));
                //    }
                //});
                $('#datepicker2').datepicker({
                    dateFormat: "dd MM yy"
                });

                $("#datepicker1").datepicker({
                    dateFormat: "dd MM yy",
                    minDate: 0,
                    onSelect: function (date) {
                        var date1 = $('#datepicker1').datepicker('getDate');
                        var date = new Date(Date.parse(date1));
                        date.setDate(date.getDate() + 1);
                        var newDate = date.toDateString();
                        newDate = new Date(Date.parse(newDate));
                        $('#datepicker2').datepicker("option", "minDate", newDate);
                    }


                });
                $('#datepicker1').datepicker("setDate", 1);
                $('#datepicker2').datepicker("setDate", 2);

                $('#datepicker1').datepicker("option", "minDate", 1);
                $('#datepicker2').datepicker("option", "minDate", 2);

            });

            $(document).ready(function () {
                $('.loaderbody').css('display', 'none');
                $('#ddlYear').change(function () {
                    $('.loaderbody').css('display', 'block');
                });
                $('#btnSearch').click(function () {
                    $('.loaderbody').css('display', 'block');
                });
            });

        </script>
    </form>
</body>
</html>
