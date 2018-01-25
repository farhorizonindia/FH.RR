<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinalpaymentLinkPage.aspx.cs" Inherits="Cruise_Booking_FinalpaymentLinkPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resorts a Hotels and Restaurants </title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link href='http://fonts.googleapis.com/css?family=Source+Sans+Pro:400,700' rel='stylesheet' type='text/css'>

    <link rel="stylesheet" href="css/css/reset.css">
    <!-- CSS reset -->
    <link rel="stylesheet" href="css/css/style.css">
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
    <link rel="stylesheet" href="css/Newcss/style.css" type="text/css" media="all">
    <!-- Animate.CSS -->
    <link rel="stylesheet" href="css/animate.css" type="text/css" media="all">
    <link rel="stylesheet" href="css/Newcss/jquery-ui.css" type="text/css" media="all">
    <!-- //Custom-Stylesheet-Links -->
    <link rel="stylesheet" href="css/Newcss/cruise.css">
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
        <%-- ///header Start///--%>
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



            </div>
        </nav>
        <%-- ///header End///--%>
        <br />
        <br />
        <br />
        <br />
        <br />

        <div class="container-fluid">
            <div class="yours-details">
                <div class="row" runat="server">
                    <div class="col-sm-7" style="margin-left: 19%;">



                        <div class="payment-option">

                            <!-- ####################### -->







                            <div style="width: 100%; float: none;">
                                <asp:Panel ID="pnlFullDetails" class="CSSTableGenerator" Width="100%" runat="server" Style="box-shadow: 3px 3px 9px 2px rgba(0,0,0,0.4); padding-top: 1%; border: 1px solid rgba(0,0,0,0.5) !important; border-radius: 4px;">

                                    <h4 style="font-size: 17px; padding-top: 14px; padding-bottom: 14px; font-family: 'Montserrat', sans-serif;">Payment Details</h4>

                                    <table class="table table-bordered" id="tbl-full-detail" style="width: 90%; margin: 0 auto; border: 1px solid #ddd;">
                                        <tr style="background-color: #ECECEC !important;">
                                            <td style="font-weight: bold; width: 27%; padding-right: 12%; padding-top: 10px;">Invoice To</td>
                                            <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                <asp:Label ID="lblAgentName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr style="background-color: #f9f9f9 !important;">
                                            <td style="font-weight: bold; padding-right: 7%; padding-top: 10px;">
                                                <asp:Label ID="lblBilling" runat="server" Text="Billing Address "></asp:Label></td>
                                            <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                <asp:Label ID="lblBillingAddress" runat="server" Text=""></asp:Label></td>

                                        </tr>
                                        <%-- <tr style="background-color: #ECECEC !important; height: 40px;">
                                            <td style="font-weight: bold; padding-right: 6%; padding-top: 10px;">
                                                <asp:Label ID="Label3" runat="server" Text="Special Request  "></asp:Label></td>
                                            <td style="">
                                                <asp:Label ID="lblSpecialRequest" runat="server" Text=""></asp:Label></td>

                                        </tr>--%>
                                        <tr style="background-color: #ECECEC !important;">
                                            <td style="font-weight: bold; padding-right: 5%; padding-top: 10px;">
                                                <asp:Label ID="lbPayment" runat="server" Text="Payment Method "></asp:Label></td>
                                            <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                <asp:Label ID="lbPaymentMethod" runat="server" Text="Online"></asp:Label><asp:HiddenField ID="hdnfPhoneNumber" runat="server" />
                                            </td>

                                        </tr>

                                        <tr style="display: none;">
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:HiddenField ID="hdnfCreditLimit" runat="server" />
                                            </td>
                                        </tr>
                                    </table>



                                    <asp:Panel ID="panelwithoutCreditAgent" Style="padding-top: 15px" Width="100%" runat="server" Font-Size="Medium">
                                        <div>

                                            <table class="table table-bordered" style="width: 90%; margin: 0 auto; border: 1px solid #ddd;">
                                                <tr style="background-color: #ECECEC !important;">
                                                    <td style="font-weight: bold; width: 27%; padding-right: 8%; padding-top: 10px;" class="auto-style3">Total amount</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;" class="auto-style4">
                                                        <asp:Label ID="lbltotAmt" runat="server" Text="Label"></asp:Label></td>

                                                </tr>






                                                <tr style="background: #f9f9f9 !important;">
                                                    <td style="font-weight: bold; padding-right: 4%; padding-top: 10px;">Advance Amount</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lblCurrency" runat="server" Text=" "></asp:Label><asp:Label ID="txtPaidAmt" runat="server"></asp:Label>
                                                        <asp:Label ID="lbl25" runat="server" Text=" "></asp:Label></td>

                                                </tr>
                                                <tr style="background-color: #ECECEC !important;" id="trbalanceamount" runat="server">
                                                    <td style="font-weight: bold; padding-right: 4%; padding-top: 10px;">Balance Amount</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lblBalanceAmt" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>
                                              <%--  <tr style="background: #f9f9f9 !important;" id="trbalancedate" runat="server">
                                                    <td style="font-weight: bold; padding-right: 8%; padding-top: 10px;">Balance Payment Date</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lblBalancedate" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>--%>
                                            </table>

                                            <br />

                                        </div>
                                        <br />
                                    </asp:Panel>
                                </asp:Panel>
                                <div style="height: 40px"></div>
                                <%--    <asp:Panel ID="fhtfr"  Width="70%" runat="server">
            <div>
                  <p> Amount</p>
                     <p><asp:TextBox ID="txtPaidAmt" runat="server"></asp:TextBox>
                         <asp:Label ID="lblCurrency" runat="server" Text="Label"></asp:Label>
                     </p>
                            </div>

                 
                    <br />
    </asp:Panel>--%>
                                <%--    <div style="height: 40px"></div>--%>
                                <center>
                      
                        </center>
                            </div>
                            <%--  <div class="ak-btn-cont ak-btn-booking-cont check-room">
                                <button type="submit" name="register" id="submit-register" class="btn btn-primary agileits  wow fadeInLeft button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span>Add another room</button>
                                <button type="button" id="register-close" class="btn btn-primary agileits  wow fadeInRight button2 pull-right">Reveiw<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>
                            </div>--%>

                            <div id="pnlBookButton" width="70%" runat="server" class="text-center">
                                <asp:Button ID="btnPayProceed" runat="server" Text="Proceed For Payment" CssClass="btn btn-info btnWidth100 btnFont" Font-Size="Medium" OnClick="btnPayProceed_Click" />
                                <asp:Label ID="lblPaymentErr" runat="server" ForeColor="#FF3300"></asp:Label>
                                <asp:HiddenField ID="hftxtpaidamt" runat="server" />
                                <asp:HiddenField ID="hdnfTotalPaybleAmt" runat="server" />
                                <br />
                                <asp:Label ID="lblBookingLockFound" runat="server" ForeColor="#FF3300" Visible="false"></asp:Label>
                                <br />
                                <asp:HyperLink ID="lnkBackToCruiseBooking" runat="server" CssClass="applink" Text="Back To Rooms Selection" Visible="false"></asp:HyperLink>
                            </div>

                            <!-- ####################### -->
                        </div>
                    </div>


                </div>

            </div>

        </div>

        <!-- Footer -->
        <%--  <div class="footer agileits ">
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
        <%--  <li class="agileits "><a href="gallery.html">Bahamas</a></li>
                            <li class="agileits "><a href="gallery.html">Hawaii</a></li>
                            <li class="agileits "><a href="gallery.html">Miami</a></li>
                            <li class="agileits "><a href="gallery.html">Ibiza</a></li>--%>
        <%--  </ul>
                    </div>
                    <div class="col-md-4 col-sm-4 footer-grid agileits  footer-grid-3 wow fadeInUp">
                        <ul class="agileits ">--%>
        <%--  <li class="agileits "><a href="about.html">About</a></li>
                            <li class="agileits "><a href="cuisines.html">Cuisines</a></li>
                            <li class="agileits "><a href="gallery.html">Gallery</a></li>
                            <li class="agileits "><a href="booking.html">Contact</a></li>--%>
        <%--  </ul>
                    </div>
                    <div class="clearfix"></div>
                </div>

                <div class="col-md-6 col-sm-6 footer-grids agileits  social wow fadeInUp">
                    <ul class="social-icons">--%>
        <%--  <li class="agileits "><a href="#" class="facebook agileits " title="Go to Our Facebook Page"></a></li>
                        <li class="agileits "><a href="#" class="twitter agileits " title="Go to Our Twitter Account"></a></li>
                        <li class="agileits "><a href="#" class="googleplus agileits " title="Go to Our Google Plus Account"></a></li>
                        <li class="agileits "><a href="#" class="instagram agileits " title="Go to Our Instagram Account"></a></li>
                        <li class="agileits "><a href="#" class="youtube agileits " title="Go to Our Youtube Channel"></a></li>--%>
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
        <!-- Default-JavaScript -->
        <script type="text/javascript" src="js/jquery-2.1.4.min.js"></script>
        <!-- Bootstrap-JavaScript -->
        <script type="text/javascript" src="js/bootstrap.min.js"></script>
        <!-- Animate.CSS-JavaScript -->
        <script src="js/wow.min.js"></script>
        <script type="js/index.js">
        </script>
        <script>
            new WOW().init();
        </script>
        <!-- Date-Picker-JavaScript -->
        <script src="js/jquery-ui.js"></script>

        <script>
            $(function () {
                //localStorage.setItem('activeTab12', 'null');

                $('#btnSubmit,#Button1').click(function () {
                    if (this.id == 'btnSubmit') {
                        localStorage.setItem('activeTab12', 'btnSubmit');
                    }
                    else if (this.id == 'Button1') {
                        //alert('Submit 2 clicked');
                        localStorage.setItem('activeTab12', 'Button1');
                    }
                });
                var activeTab = localStorage.getItem('activeTab12');
                if (activeTab == 'btnSubmit') {
                    localStorage.clear('activeTab12');
                    localStorage.clear("get");
                    $('#collapseOne').removeClass('panel-collapse collapse');
                }
                if (activeTab == 'Button1') {
                    localStorage.clear('activeTab12');
                    localStorage.clear("get");
                    $('#collapseTwo').removeClass('panel-collapse collapse');
                }
                $('.panel-heading').click(function () {

                    var spanChng1 = $(this).children('.panel-title').children('img');
                    console.log(spanChng1);

                    //if (localStorage.getItem("get") == null) {
                    if ($(this).hasClass('collapsed')) {
                        localStorage.setItem("get", "get1");
                        spanChng1.css({
                            "background": "url(../../images/next-arrow.png)",
                            "transform": "rotate(270deg)"
                        });
                    } else {
                        localStorage.setItem("get", "get1");
                        spanChng1.css({
                            "background": "url(../../images/next-arrow.png)",
                            "transform": "rotate(90deg)"
                        });
                    }
                    //}
                });
            });
        </script>

        <%--   <script>
            $(function () {
                $("#datepicker,#datepicker1,#datepicker2").datepicker({ dateFormat: 'MM-yy' });


                $('.panel-default').click(function () {
                    var spanChng = $(this).children('.panel-heading').children('.panel-title').children('span');
                    console.log(spanChng);
                    if ($(this).children('.panel-heading').hasClass('collapsed')) {
                        spanChng.css({
                            "background": "url(images/remove.png)",
                            "background-size": "3.6rem",
                            "background-repeat": "no-repeat",
                            "background-position": "53%"
                        });
                    } else {
                        spanChng.css({
                            "background": "",
                            "background-size": "3.6rem",
                            "background-repeat": "no-repeat",
                            "background-position": "53%"
                        });
                    }
                });

            });
        </script>--%>
    </form>
</body>
</html>
