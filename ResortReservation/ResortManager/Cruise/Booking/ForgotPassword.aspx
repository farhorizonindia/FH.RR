<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="Cruise_Booking_ForgotPassword" EnableEventValidation="false" %>

<!DOCTYPE html>
<script runat="server">
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resorts a Hotels and Restaurants </title>

    <!-- Meta-Tags -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
    <!-- //Meta-Tags -->
    <!-- Custom-Stylesheet-Links -->
    <!-- Bootstrap-CSS -->
    <link rel="stylesheet" href="../Booking/css/Newcss/bootstrap.min.css" type="text/css" media="all">
    <!-- Index-Page-CSS -->
    <link rel="stylesheet" href="../Booking/css/Newcss/style.css" type="text/css" media="all">
    <!-- Animate.CSS -->
    <link rel="stylesheet" href="../Booking/css/animate.css" type="text/css" media="all">
    <link rel="stylesheet" href="../Booking/css/Newcss/jquery-ui.css" type="text/css" media="all">
    <!-- //Custom-Stylesheet-Links -->
    <link rel="stylesheet" href="../Booking/css/Newcss/cruise.css">
    <!-- Fonts -->
    <!-- Body-Font -->
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Open+Sans:400,300,600,700,800" type="text/css">
    <!-- Logo-Font -->
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Cinzel+Decorative:400,900,700" type="text/css">
    <!-- Navbar-Font -->
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Montserrat:400,700" type="text/css">
    <!-- //Fonts -->
</head>
<body class="bg-img1" style="font-family: 'Open Sans', sans-serif;">
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
                        <a class="navbar-brand agileits " href="../Booking/searchproperty1.aspx">Resorts</a>
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
                                    <li><a runat="server" id="lnkCustomerRegis" href="../Masters/NewRegister.aspx">Customer </a>


                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </div>

                </div>
            </nav>
            <!-- //Navbar -->
            <br />
            <br />
            <br />
            <br />
            <br />
            <div>
                <div class="container-fluid">

                    <div class="yours-details">
                        <div class="row" runat="server" style="width: 80%; margin: auto;">
                            <div class="col-sm-12">
                                <div class="panel panel-default" runat="server" id="pnllogin">
                                    <div class="panel-heading " role="tab" id="headingTwo" data-toggle="collapse" data-parent="#accordion" href="#collapse" aria-expanded="true" aria-controls="collapseTwo">
                                        <!-- <h4 class="panel-title">
                              <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                Collapsible Group Item #2
                              </a>
                            </h4> -->
                                        <div class="panel-title">

                                            <span></span>
                                            <label style="margin: 0;">
                                                Password Recovery 
                                            </label>
                                            <img src="../../images/next-arrow.png" style="width: 16px; float: right; margin-top: 4px; transform: rotate(90deg);" />
                                        </div>
                                    </div>
                                    <div id="collapse" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingTwo">
                                        <div class="panel-body">
                                            <div class="ak-main-login payment-online-form-left agileits">
                                               <%-- <h4>Enter Your Email Id</h4>--%>
                                                <asp:RadioButton ID="rdbAgent" runat="server" Text="Partner" GroupName="chk" /><asp:RadioButton ID="rdbCustomer" GroupName="chk" Text="Customer" runat="server" />


                                                <form class="ak-form" method="post" action="#">

                                                    <ul class="agileits ">
                                                        <li class="agileits">
                                                            <input required="" id="txtCustMailId" runat="server" class="text-box-dark agileits " type="text" value="Enter Your Email" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Enter Your Email';}" name="email" /></li>

                                                    </ul>

                                                </form>
                                                <div class="ak-btn-cont ak-btn-booking-cont login-btn">

                                                    <asp:Button runat="server" class="btn btn-primary agileits button2" CssClass="btn btn-info btnWidth100 btnFont" ID="btnCustLogin" Text="Click Here" OnClick="btnCustLogin_Click" />
                                                    <asp:Button runat="server" class="btn btn-primary agileits button2" CssClass="btn btn-info btnWidth100 btnFont" ID="Button1" Text="Back" OnClick="Button1_Click" />
                                                    <!-- <button type="button" id="register-show" class="btn btn-primary agileits button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span> Register</button> -->
                                                    <%-- <button type="submit" id="login" class="btn btn-primary agileits button2">Login<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>





                            </div>


                        </div>

                    </div>

                </div>
            </div>
        </div>
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
        </script>
        <!-- //Animate.CSS-JavaScript -->
    </form>
</body>
</html>
