<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Packagesearchresult1.aspx.cs" Inherits="Cruise_Booking_Packageserachresult1" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resorts a Hotels and Restaurants </title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link href='http://fonts.googleapis.com/css?family=Source+Sans+Pro:400,700' rel='stylesheet' type='text/css'>

    <link rel="stylesheet" href="css/css/reset.css">
    <!-- CSS reset -->
    <link rel="stylesheet" href="css/css/style.css" />

 
    <!-- Resource style -->
    <script src="js/modernizr.js"></script>
    <!-- Meta-Tags -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
    <!-- //Meta-Tags -->

    <!-- Custom-Stylesheet-Links -->
    <!-- Bootstrap-CSS -->
    <link rel="stylesheet" href="css/Newcss/bootstrap.min.css" type="text/css" media="all">
    <!-- Index-Page-CSS -->
    <link rel="stylesheet" href="css/Newcss/style.css" type="text/css" media="all"/>

     
       
    <!-- Animate.CSS -->
    <link rel="stylesheet" href="css/Newcss/animate.css" type="text/css" media="all">
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

 
    <div class="loaderbody"><div class="loader"><img src="../../images/loading1.gif" alt="Loading..." /> Please Wait</div></div>
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
                        <a class="navbar-brand agileits " href="searchproperty1.aspx">Resorts</a>
                    </div>

                    <div id="navbar" class="navbar-collapse agileits  navbar-right collapse">
                        <asp:Label ID="lblUsername" runat="server" Font-Bold="true" ForeColor="Red" Text=" "></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button-link" OnClick="LinkButton1_Click">Logout</asp:LinkButton>
                        <ul class="nav agileits  navbar-nav" runat="server" id="navlogin">
                            <li class="dropdown">
                                <a id="lblLoginas" runat="server" href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">LOGIN <span class="caret"></span></a>
                                <ul class="dropdown-menu">
                                    <li><a runat="server" id="lnkLogin" href="agentLogin1.aspx">Partner</a></li>
                                    <li class="dropdown-submenu"><a runat="server" id="lnkCustomerRegis" href="../Masters/NewRegister.aspx">Customer </a>
                                        <ul class="dropdown-menu">
                                            <li><a href="#">Sign Up</a></li>
                                            <li><a href="#">Login</a></li>
                                            <%--      </li>
                                    <li><a runat="server" id="lnkCustLogin" href="CustomerLogin.aspx">Customer Login</a>

                                    </li>
                                    <li><a runat="server" id="lnkView" href="ViewAllBookings.aspx">Customer Login</a>

                                    </li>--%>
                                        </ul>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </div>

                </div>
            </nav>
            <!-- //Navbar -->

            <!--$$$$$$$$%%%%%%%%%%%%%%%%%%%%% pakages Details &&&&&&&&&&&&&&&&&&&&&&&&&&&&&& -->


            <div class="details agileits ak-cruise-details-container">
                <div class="container">
                    <div class="row">
                        <div class="col-sm-4">
                            <h2 class="text-left" style="padding-top: 15px; text-align: left !important; font-weight: normal; font-size: 24px; margin-bottom: 0;">Vessel:MV Mahabaahu
                                <%--<asp:Label ID="lblPackagename" runat="server" Text=' '></asp:Label>--%></h2>
                        </div>
                        <div class="col-sm-8">
                            <section style="width: 100%; height: 74px; padding-top: 0; margin-left: 28px; border-bottom: none;">


                                <nav>
                                    <ol class="cd-breadcrumb triangle" style="float: right;">
                                        <li><em>Search</em></li>
                                        <li class="current"><em>Packages</em></li>
                                        <li><em>Choose Date</em></li>
                                        <li><em>Select Cabin</em></li>
                                        <%--<li><a href="#0">Reservation Details & Check Out</a></li>--%>
                                        <li><em>Details & Check Out</em></li>
                                    </ol>
                                </nav>
                            </section>  
                        </div>

                    </div>

                    <%--   <h3>Packages</h3>--%>
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


                    <asp:UpdatePanel runat="server" ID="upd1">
                        <ContentTemplate>
                            <div class="container available ak-cruise">

                                <!-- Cruise item -->
                                <%try %>
                                <%{ %>
                                <% for (int i = 0; i < dtres.Rows.Count; i++)

                                    {
                                %>
                                <div class="row room-detail">
                                    <div class="col-sm-7" style="padding-top: 22px; padding-bottom: 22px;">
                                        <% Response.Write("<img src= /" + dtres.Rows[i]["Img"].ToString() + " class='img-responsive' border='0' alt='' />");%>
                                        <%-- <img src="images/project-4.jpg" class="img-responsive" alt="room-">--%>
                                    </div>
                                    <div class="col-sm-5" style="padding-top: 22px;">
                                        <div class="row">
                                            <div class="col-sm-12 top-div" style="padding-left: 0;">
                                                <div class="details-grid-info ak-cruise-details agileits ">
                                                    <h3 style="border-bottom: 1px solid #000; padding-bottom: 4%; color: #000; font-weight: normal; font-size: 24px; margin-bottom: -23px;"><%Response.Write(dtres.Rows[i]["PackageName"].ToString()); %></h3>
                                                    <p class="text-justify" style="padding-top: 21px;">
                                                        <br>
                                                        <%
                                                            Response.Write(dtres.Rows[i]["BFrom"].ToString() + " to " + dtres.Rows[i]["BTo"].ToString()); %>
                                                        <br />
                                                        <br />
                                                        <b>
                                                            <%
                                                                Response.Write(dtres.Rows[i]["NoOfNights"].ToString() + "  Nights/" + (Convert.ToInt32(dtres.Rows[i]["NoOfNights"].ToString()) + 1).ToString() + " Days");
                                                            %>
                                                        </b>
                                                        <%-- Guwahati (1) to Neamati (7)<br>
                                                7 Nights/8 Days<br>
                                                <br>--%>
                                                        <br />
                                                        <br />
                                                        <%Response.Write(dtres.Rows[i]["PackageDescription"].ToString()); %>
                                                        <% PackageId = dtres.Rows[i]["PackageId"].ToString(); %>
                                                        
                                                    </p>
                                                </div>
                                                <div class="pull-right" style="padding-top: 10px;">
                                                    <%Response.Write("<a href='" + dtres.Rows[i]["ItineraryLink"].ToString() + "' class='btn btn-info btnWidth100 btnFont' >Itinerary</a>");%>
                                                    <%
                                                        if (Session["agentid"] != null)
                                                        {
                                                            Response.Write("<a href='DepartureSearch1.aspx?PackId=" + PackageId + "&CheckinDep=" + CheckinDep + "&CheckoutDep=" + CheckoutDep + "&agentid=" + Session["agentid"] + "'  class='btn btn-info btnWidth100 btnFont' >Book Now</a>");
                                                        }
                                                        else
                                                        {
                                                            Response.Write("<a href='DepartureSearch1.aspx?PackId=" + PackageId + "&CheckinDep=" + CheckinDep + "&CheckoutDep=" + CheckoutDep + " '  class='btn btn-info btnWidth100 btnFont' >Book Now</a>");
                                                        }
                                                         %>
                                                    <%--<%-- <a href="http://www.mahabaahucruiseindia.com/brahmaputra-river-cruise-itinerary/upstream-itinerary/">
                                                <button class="btn btn-primary wow agileits  fadeInLeft">ITINERARY<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>
                                            </a>--%>
                                                    <%--<a href="7night-8day-Downstream-Cruise-travel-Date.html" title="">
                                                <button class="btn btn-primary wow agileits  fadeInLeft">BOOK NOW<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button></a>--%>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                                <%

                                    } %>
                                <%} %>
                                <%catch (Exception sqe) %>
                                <%{ %>
                                <%} %>
                                <!-- Cruise item ends -->

                                <!-- Cruise item -->
                                <%--  <div class="row room-detail">
                            <div class="col-sm-4">
                                <img src="images/project-4.jpg" class="img-responsive" alt="room-">
                            </div>
                            <div class="col-sm-8">
                                <div class="row">
                                    <div class="col-sm-12 top-div">
                                        <div class="details-grid-info ak-cruise-details agileits ">
                                            <h4><b>7 nights 8 days MV Mahabaahu Downstream Cruise</b></h4>
                                            <p>
                                                Vessel:MV Mahabaahu<br>
                                                Neamati (7) to Guwahati (1)<br>
                                                7 Nights/8 Days<br>
                                                <br>
                                                Sibsagar: Ahom kingdom, a meal with a tea planter family - Majauli: Vaishwanite monastery, two cultural dances by monks and folk dancers - Mishing tribal village - Boat safari - Biswanath Ghat: Silk weaver's village, Tea plantation - Silghat: Tea factory with cultural dances - Kaziranga National Park: jeep and elephant safaris, Agrarian Bangladeshi immigrant village, Umananda Temple, Kamakhya Temple
                                            </p>
                                        </div>
                                        <div class="pull-right">
                                            <a href="http://mahabaahucruiseindia.com/itinerary/downstream-itinerary/">
                                                <button class="btn btn-primary wow agileits  fadeInLeft">ITINERARY<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button></a>
                                            <a href="7night-8day-Downstream-Cruise-travel-Date.html" title="">
                                                <button class="btn btn-primary wow agileits  fadeInLeft">BOOK NOW<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button></a>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>
                        <!-- Cruise item ends -->

                        <!-- Cruise item -->
                        <div class="row room-detail">
                            <div class="col-sm-4">
                                <img src="images/project-4.jpg" class="img-responsive" alt="room-">
                            </div>
                            <div class="col-sm-8">
                                <div class="row">
                                    <div class="col-sm-12 top-div">
                                        <div class="details-grid-info ak-cruise-details agileits ">
                                            <h4><b>5 nights 6 days MV Mahabaahu Upstream Cruise</b></h4>
                                            <p>
                                                Vessel:MV Mahabaahu<br>
                                                Silghat (3) to Neamati (7)<br>
                                                5 Nights/6 Days<br>
                                                <br>
                                                Silghat: Tea plantation and factory with cultural dances - Kaziranga National Park: Jeep and elephant safaris - Biswanath Ghat: Silk weaver's village - Boat safari, Mishing tribal village, Majuli: Vaishwanite monastery, two cultural dances by monks and folk dancers - Sibsagar: Ahom kingdom and a meal with a tea planter family.
                                            </p>
                                        </div>
                                        <div class="pull-right">
                                            <a href="http://www.mahabaahucruiseindia.com/brahmaputra-river-cruise-itinerary/upstream-itinerary-5-nights/">
                                                <button class="btn btn-primary wow agileits  fadeInLeft">ITINERARY<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button></a>
                                            <a href="7night-8day-Downstream-Cruise-travel-Date.html" title="">
                                                <button class="btn btn-primary wow agileits  fadeInLeft">BOOK NOW<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button></a>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>
                        <!-- Cruise item ends -->

                        <!-- Cruise item -->
                        <div class="row room-detail">
                            <div class="col-sm-4">
                                <img src="images/project-4.jpg" class="img-responsive" alt="room-">
                            </div>
                            <div class="col-sm-8">
                                <div class="row">
                                    <div class="col-sm-12 top-div">
                                        <div class="details-grid-info ak-cruise-details agileits ">
                                            <h4><b>3 nights 4 days MV Mahabaahu Downstream Cruise</b></h4>
                                            <p>
                                                Vessel:MV Mahabaahu<br>
                                                Silghat (3) to Guwahati (1)<br>
                                                3 Nights/4 Days<br>
                                                <br>
                                                Silghat: Tea plantation and tea factory visit with cultural dances, Kaziranga National Park, Agrarian Bangladeshi immigrant village, the Umananda Temple, Kamakhya Temple.
                                            </p>
                                        </div>
                                        <div class="pull-right">
                                            <a href="http://www.mahabaahucruiseindia.com/itinerary/downstream-itinerary-3-nights/">
                                                <button class="btn btn-primary wow agileits  fadeInLeft">ITINERARY<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button></a>
                                            <a href="7night-8day-Downstream-Cruise-travel-Date.html" title="">
                                                <button class="btn btn-primary wow agileits  fadeInLeft">BOOK NOW<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button></a>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>--%>
                                <!-- Cruise item ends -->

                            </div>
                        </ContentTemplate>
                        <%-- <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn" />
                </Triggers>--%>
                    </asp:UpdatePanel>

                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="200">
                        <ProgressTemplate>
                            <iframe id="pgrIFrame" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"
                                src="javascript:'<html></html>';" style="position: absolute;"></iframe>
                            <asp:Panel ID="Panel1" runat="server" Height="150%" Style="margin-left: 0px; margin-bottom: 700px;" Width="100%">
                                <div style="position: relative; top: 200px; left: 200px; padding-left: 150px;">
                                    <div style="padding-right: 50px;">
                                        <asp:Image ID="image2" runat="server" Height="40px" Width="40px" ImageUrl="~/images/loading1.gif" />
                                        Please Wait.... 
                                    </div>
                                </div>
                            </asp:Panel>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <cc1:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1" runat="server"
                        TargetControlID="Panel1" HorizontalOffset="300" VerticalOffset="150"></cc1:AlwaysVisibleControlExtender>

                    <!-- Tooltip-Content -->


                    <!-- //Tooltip-Content -->

                </div>
            </div>

            <!-- //Details -->

            <!-- Footer -->
            <%--<div class="footer agileits ">
                <div class="container">

                    <div class="col-md-6 col-sm-6 agileits  footer-grids">
                        <div class="col-md-4 col-sm-4 footer-grid agileits  footer-grid-1 wow fadeInUp">
                            <ul class="agileits ">--%>
            <%--<li class="agileits ">5 Star Hotels</li>
                                <li class="agileits ">Beach Resorts</li>
                                <li class="agileits ">Beach Houses</li>
                                <li class="agileits ">Water Houses</li>--%>
            <%-- </ul>
                        </div>
                        <div class="col-md-4 col-sm-4 footer-grid agileits  footer-grid-2 wow fadeInUp">
                            <ul class="agileits ">--%>
            <%-- <li class="agileits "><a href="gallery.html">Bahamas</a></li>
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
                     <%--       <li class="agileits "><a href="#" class="youtube agileits " title="Go to Our Youtube Channel"></a></li>--%>
            <%--</ul>
                    </div>

                    <div class="col-md-6 col-sm-6 footer-grids agileits  copyright wow fadeInUp">
                        <p>&copy; 2017 Resorts. All Rights Reserved | Design by</p>
                    </div>
                    <div class="clearfix"></div>

                </div>
            </div>--%>
            <!-- //Footer -->
            <!-- // $$$$$$$$%%%%%%%%%%%%%%%%%%%%% pakages Details &&&&&&&&&&&&&&&&&&&&&&&&&&&&&& -->
            <!-- // $$$$$$$$%%%%%%%%%%%%%%%%%%%%% pakages Details &&&&&&&&&&&&&&&&&&&&&&&&&&&&&& -->
            <!-- Default-JavaScript -->
            <script type="text/javascript" src="../Booking/js/jquery-2.1.4.min.js"></script>
            <!-- Bootstrap-JavaScript -->
            <script type="text/javascript" src="../Booking/js/bootstrap.min.js"></script>
            <!-- Animate.CSS-JavaScript -->
            <script src="../Booking/js/wow.min.js"></script>
            <script type="../Booking/js/index.js"></script>
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


                $(document).ready(function () {
                    $('.loaderbody').css('display', 'none');
                    $('.btn-info').click(function () {
                        $('.loaderbody').css('display', 'block');
                    });
                });

            </script>
            <!-- //Animate.CSS-JavaScript -->
    </form>
</body>
</html>
