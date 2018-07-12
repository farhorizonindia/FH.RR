<%@ Page Language="C#" AutoEventWireup="true" CodeFile="agentLogin1.aspx.cs" Inherits="Cruise_Booking_agentLogin1" EnableEventValidation="false" %>

<!DOCTYPE html>

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
                                    <div class="panel-heading" role="tab" id="headingTwo" data-toggle="collapse" data-parent="#accordion" href="#collapse" aria-expanded="true" aria-controls="collapseTwo">
                                        <!-- <h4 class="panel-title">
                              <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                Collapsible Group Item #2
                              </a>
                            </h4> -->
                                        <div class="panel-title">

                                            <span></span>
                                            <label style="margin: 0;">
                                                SIGN IN
                                            </label>
                                            <img src="../../images/next-arrow.png" style="width: 16px; float: right; margin-top: 4px; transform: rotate(270deg);" />
                                        </div>
                                    </div>
                                    <div id="collapse" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingTwo">
                                        <div class="panel-body">
                                            <div class="ak-main-login payment-online-form-left agileits">
                                                <%--   <h4>LOGIN</h4>--%>
                                                <form class="ak-form" method="post" action="#">

                                                    <ul class="agileits ">
                                                        <li class="agileits">
                                                            <input  id="txtCustMailId" runat="server" class="text-box-dark agileits " type="text" placeholder="Enter Email" name="email" /></li>
                                                        <li class="agileits">
                                                            <input  id="txtCustPass" runat="server" class="text-box-dark agileits " type="password" placeholder="Enter Password" value="Your Password" name="password" /></li>
                                                    </ul>

                                                </form>
                                                <div class="ak-btn-cont ak-btn-booking-cont login-btn">
                                                    <asp:Button runat="server" class="btn btn-primary agileits button2" CssClass="btn btn-info btnWidth100 btnFont" ID="btnCustLogin" Text="Login" OnClick="btnCustLogin_Click" />
                                                    <!-- <button type="button" id="register-show" class="btn btn-primary agileits button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span> Register</button> -->
                                                    <%-- <button type="submit" id="login" class="btn btn-primary agileits button2">Login<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                                    <asp:Button runat="server" class="btn btn-primary agileits button2" CssClass="btn btn-info btnWidth100 btnFont" ID="Button1" Text="Forgot Password" OnClick="btnCustLogin_Click1" />
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
        <script>
            $(function () {

                $('.panel-default').click(function () {
                    var spanChng1 = $(this).children('.panel-heading').children('.panel-title').children('img');
                    console.log(spanChng1);

                    if ($(this).children('.panel-heading').hasClass('collapsed')) {
                        spanChng1.css({
                            "background": "url(../../images/next-arrow.png)",
                            "transform": "rotate(270deg)"
                        });
                    } else {
                        spanChng1.css({
                            "background": "url(../../images/next-arrow.png)",
                            "transform": "rotate(90deg)"
                        });
                    }
                });
            });
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
