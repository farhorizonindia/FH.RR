<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SummarizedDetails2.aspx.cs" Inherits="Cruise_Booking_SummarizedDetails2"  EnableEventValidation="false" %>

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
        <div class="container-fluid">
            <div class="yours-details">
                <div class="row">
                    <div class="col-sm-8">
                        <div class="panel panel-default">
                            <div class="panel-heading collapsed" role="tab" id="headingTwo" data-toggle="collapse" data-parent="#accordion" href="#collapse" aria-expanded="false" aria-controls="collapseTwo">
                                <!-- <h4 class="panel-title">
                              <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                Collapsible Group Item #2
                              </a>
                            </h4> -->
                                <div class="panel-title">

                                    <span></span>
                                    <label style="margin: 0;">
                                        SIGN IN TO BOOK FASTER
                                    </label>
                                </div>
                            </div>
                            <div id="collapse" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                                <div class="panel-body">
                                    <div class="ak-main-login payment-online-form-left agileits">
                                        <h4>LOGIN</h4>
                                        <form class="ak-form" method="post" action="#">

                                            <ul class="agileits ">
                                                <li class="agileits">
                                                    <input required="" class="text-box-dark agileits " type="text" value="Enter Your Email" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Enter Your Email';}" name="email"></li>
                                                <li class="agileits">
                                                    <input required="" class="text-box-dark agileits " type="password" value="Your Password" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Your Password';}" name="password"></li>
                                            </ul>
                                            <div class="ak-btn-cont ak-btn-booking-cont login-btn">
                                                <!-- <button type="button" id="register-show" class="btn btn-primary agileits button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span> Register</button> -->
                                                <button type="submit" id="login" class="btn btn-primary agileits button2">Login<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>
                                            </div>
                                        </form>
                                        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- 	 <div class="panel panel-default">
                                       <div class="panel-heading" role="tab" id="headingOne" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                         <h4 class="panel-title text-left">
                                           <a role="button" >
                                           SIGN IN TO BOOK FASTER
                                           </a>
                                         </h4>
                                       </div>
                       <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                         <div class="panel-body">
                          <div class=" ">
                               <div class="ak-main-login payment-online-form-left agileits">
                               <h4>LOGIN</h4>
                                   <form class="ak-form" method="post" action="#">

                                       <ul class="agileits ">
                                           <li class="agileits  wow fadeInDown"><input required="" class="text-box-dark agileits " type="text" value="Enter Your Email" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Enter Your Email';}" name="email"></li>
                                           <li class="agileits  wow fadeInDown"><input required="" class="text-box-dark agileits " type="password" value="Your Password" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Your Password';}" name="password"></li>
                                       </ul>
                                       <div class="ak-btn-cont ak-btn-booking-cont">
                                           <button type="button" id="register-show" class="btn btn-primary agileits  wow fadeInDown button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span> Register</button>
                                           <button type="submit" id="login" class="btn btn-primary agileits  wow fadeInDown button2" >Login<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>
                                       </div>
                                   </form>
                               </div>
                           </div>
                         </div>
                       </div>
                    </div>  -->
                       
                        <div class="top-form">
                            <h4>Register</h4>
                            <form class="ak-form" method="post" action="#">

                                <ul class="agileits ">
                                    <li class="agileits  wow fadeInLeft list-unstyled">
                                        <select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%;">
                                            <option selected="selected" value="Title">Title</option>
                                            <option value="Mr">Mr</option>
                                            <option value="Mrs">Mrs</option>
                                            <option value="Miss">Miss</option>
                                            <option value="Ms">Ms</option>
                                        </select>
                                    </li>
                                </ul>
                                <div class="row agileits ">
                                    <div class="col-sm-6 agileits  wow fadeInLeft">
                                        <input required="" class="text-box-dark agileits " type="text" value="First Name" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'First Name';}" name="firstname">
                                    </div>
                                    <div class="col-sm-6 agileits  wow fadeInRight">
                                        <input required="" class="text-box-dark agileits " type="text" value="Last Name" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Last Name';}" name="lastname">
                                    </div>
                                </div>
                                <div class="row agileits ">
                                    <div class="col-sm-6 agileits  wow fadeInLeft">
                                        <input required="" class="text-box-dark agileits " type="text" value="Email Address" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Email Address';}" name="register-email">

                                    </div>
                                    <div class="col-sm-6 agileits  wow fadeInRight">
                                        <input required="" class="text-box-dark agileits " type="text" value="Telephone" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Telephone';}" name="telephone">
                                    </div>
                                </div>
                                <div class="row agileits ">
                                    <div class="col-sm-6 agileits  wow fadeInLeft">
                                        <input required="" class="text-box-dark agileits " type="text" value="Address 1" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Address 1';}" name="address1">
                                    </div>
                                    <div class="col-sm-6 agileits  wow fadeInRight">
                                        <input required="" class="text-box-dark agileits " type="text" value="Address 2" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Address 2';}" name="address2">
                                    </div>
                                </div>
                                <div class="row agileits ">
                                    <div class="col-sm-6 agileits  wow fadeInLeft">
                                        <input required="" class="text-box-dark agileits " type="text" value="City" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'City';}" name="city">
                                    </div>
                                    <div class="col-sm-6 agileits  wow fadeInRight">
                                        <input required="" class="text-box-dark agileits " type="text" value="State" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'State';}" name="state">
                                    </div>
                                </div>
                                <div class="row agileits ">
                                    <div class="col-sm-6 agileits  wow fadeInLeft">
                                        <input required="" class="text-box-dark agileits " type="text" value="Postcode" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Postcode';}" name="postcode">
                                    </div>
                                    <div class="col-sm-6 agileits  wow fadeInRight">
                                        <input required="" class="text-box-dark agileits " type="password" value="Password" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Password';}" name="register-password">
                                    </div>
                                </div>
                                <div class="agileits ">
                                    <li class="agileits  wow fadeInLeft list-unstyled">
                                        <select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%; margin-top: 1%;">
                                            <option selected="selected" value="Title">Country</option>
                                            <option value="India">India</option>
                                            <option value="USA">USA</option>
                                            <option value="UK">UK</option>
                                            <option value="Norway">Norway</option>
                                        </select>
                                    </li>
                                </div>
                                <div class="ak-btn-cont ak-btn-booking-cont">
                                    <button type="submit" name="register" id="submit-register" class="btn btn-primary agileits  wow fadeInLeft button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span>Submit</button>
                                    <button type="button" id="register-close" class="btn btn-primary agileits  wow fadeInRight button2">Close<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>
                                </div>


                            </form>
                            <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
                        </div>

                        <div class="payment-option">
                            <label class="text-left">PAYMENT OPTIONS</label>
                            <p style="padding-bottom: 2%;">Your credit card will be used to guarantee your booking for late arrival - it will not be charged.</p>
                            <form>
                                <label>CARD DETAILS</label>

                                <ul class="agileits list-unstyled ">
                                    <li class="agileits  wow fadeInLeft">
                                        <input required="" class="text-box-dark agileits " type="text" value="Name On Card" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Name On Card';}" name="Name On Card"></li>
                                </ul>
                                <div class="row">
                                    <div class="col-sm-8">

                                        <input required="" class="text-box-dark agileits " type="text" value="Card Number" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Card Number';}" name="Card Number">
                                    </div>
                                    <div class="col-sm-4">

                                        <div class="book-pag-frm2 agileits ">

                                            <input class="date agileits " id="datepicker2" type="text" value=" Expiry Date" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Expiry Date';}" required="">
                                        </div>

                                    </div>
                                </div>

                            </form>

                            <!-- ####################### -->


                            <div class="panel panel-default">
                                <div class="panel-heading collapsed" role="tab" id="headingTwo" data-toggle="collapse" data-parent="#accordion" href="#collapse1" aria-expanded="false" aria-controls="collapseTwo">
                                    <!-- <h4 class="panel-title">
                                  <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                    Collapsible Group Item #2
                                  </a>
                                </h4> -->
                                    <div class="panel-title">

                                        <span></span>
                                        <label style="padding-left: 4%;">
                                            I want to enter a billing address.
                                        </label>
                                    </div>
                                </div>
                                <div id="collapse1" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                                    <div class="panel-body">
                                        <input required="" class="text-box-dark agileits " type="text" value="Email Address" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Email Address';}" name="register-email">
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading collapsed" role="tab" id="headingTwo" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                    <!-- <h4 class="panel-title">
                                  <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                    Collapsible Group Item #2
                                  </a>
                                </h4> -->
                                    <div class="panel-title">
                                        <span></span>
                                        <label style="padding-left: 4%;">
                                            I am booking on behalf of someone else.
                                        </label>
                                    </div>
                                </div>
                                <div id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                                    <div class="panel-body">
                                        <form class="ak-form" method="post" action="#">

                                            <ul class="agileits ">
                                                <li class="agileits">
                                                    <select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%;">
                                                        <option selected="selected" value="Title">Title</option>
                                                        <option value="Mr">Mr</option>
                                                        <option value="Mrs">Mrs</option>
                                                        <option value="Miss">Miss</option>
                                                        <option value="Ms">Ms</option>
                                                    </select>
                                                </li>
                                            </ul>
                                            <ul class="agileits ">
                                                <li class="agileits">
                                                    <input required="" class="text-box-dark agileits " type="text" value="First Name" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'First Name';}" name="firstname"></li>
                                                <li class="agileits">
                                                    <input required="" class="text-box-dark agileits " type="text" value="Last Name" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Last Name';}" name="lastname"></li>
                                            </ul>
                                            <ul class="agileits ">
                                                <li class="agileits">
                                                    <input required="" class="text-box-dark agileits " type="text" value="Email Address" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Email Address';}" name="register-email"></li>
                                                <li class="agileits">
                                                    <input required="" class="text-box-dark agileits " type="text" value="Telephone" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Telephone';}" name="telephone"></li>
                                            </ul>
                                            <ul class="agileits ">
                                                <li class="agileits">
                                                    <input required="" class="text-box-dark agileits " type="text" value="Address 1" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Address 1';}" name="address1"></li>
                                                <li class="agileits">
                                                    <input required="" class="text-box-dark agileits " type="text" value="Address 2" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Address 2';}" name="address2"></li>
                                            </ul>
                                            <ul class="agileits ">
                                                <li class="agileits">
                                                    <input required="" class="text-box-dark agileits " type="text" value="City" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'City';}" name="city"></li>
                                                <li class="agileits">
                                                    <input required="" class="text-box-dark agileits " type="text" value="State" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'State';}" name="state"></li>
                                            </ul>
                                            <ul class="agileits ">
                                                <li class="agileits">
                                                    <input required="" class="text-box-dark agileits " type="text" value="Postcode" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Postcode';}" name="postcode"></li>
                                                <li class="agileits">
                                                    <input required="" class="text-box-dark agileits " style="padding: 1%;" type="password" value="Password" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Password';}" name="register-password"></li>
                                            </ul>
                                            <ul class="agileits ">
                                                <li class="agileits">
                                                    <select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%; margin-top: 1%;">
                                                        <option selected="selected" value="Title">Country</option>
                                                        <option value="India">India</option>
                                                        <option value="USA">USA</option>
                                                        <option value="UK">UK</option>
                                                        <option value="Norway">Norway</option>
                                                    </select>
                                                </li>
                                            </ul>
                                            <div class="ak-btn-cont ak-btn-booking-cont">
                                                <button type="submit" name="register" id="submit-register" class="btn btn-primary agileits  button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span>Submit</button>
                                                <button type="button" id="register-close" class="btn btn-primary agileits button2">Close<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>
                                            </div>


                                        </form>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading collapsed" role="tab" id="headingTwo" data-toggle="collapse" data-parent="#accordion" href="#collapseThree" aria-expanded="false" aria-controls="collapseTwo">
                                    <!-- <h4 class="panel-title">
                                  <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                    Collapsible Group Item #2
                                  </a>
                                </h4> -->
                                    <div class="panel-title">
                                        <span></span>
                                        <label style="padding-left: 4%;">
                                            I have special requests.
                                        </label>
                                    </div>
                                </div>
                                <div id="collapseThree" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                                    <div class="panel-body">
                                        <p style="padding: 2%;">Let us know if there is anything else we can help you with, we are always happy to help where ever we can.</p>
                                        <textarea class="form-control" rows="3"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="ak-btn-cont ak-btn-booking-cont check-room">
                                <button type="submit" name="register" id="submit-register" class="btn btn-primary agileits  wow fadeInLeft button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span>Add another room</button>
                                <button type="button" id="register-close" class="btn btn-primary agileits  wow fadeInRight button2 pull-right">Reveiw<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>
                            </div>


                            <!-- ####################### -->
                        </div>




                    </div>
                    <div class="col-sm-4 left-col" style="box-shadow: 1px 1px 8px 0px;">
                        <h4 class="text-left" style="padding-top: 3%; font-size: 14px;">Available Rooms</h4>
                        <div class="row room-detail">

                            <div class="col-sm-6">
                                <img src="images/project-4.jpg" class="img-responsive" alt="room-">
                            </div>
                            <div class="col-sm-6">
                                <div class="row">
                                    <div class="col-sm-12 top-div">
                                        <div class="row">
                                            <!-- <div class="col-sm-3">
                                            <label>Room</label>
                                            <p>Pool Facing Dlx Cottage</p>
                                        </div> -->
                                            <h3 style="font-size: 17px;">Pool Facing Dlx Cottage</h3>
                                            <div class="col-sm-4">
                                                <label>Double</label>
                                                <div class="form-group">
                                                    <img src="images/down.png" class="img-responsive" alt="">
                                                    <select class="form-control" id="sel1">

                                                        <option>No</option>
                                                        <option>Yes</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <label>Guests</label>
                                                <div class="form-group">
                                                    <img src="images/down.png" class="img-responsive" alt="">
                                                    <select class="form-control" id="sel1">
                                                        <option>1</option>
                                                        <option>2</option>
                                                        <option>3</option>
                                                        <option>4</option>
                                                        <option>5</option>
                                                        <option>6</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <label>Room</label>
                                                <p>Twin</p>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <!-- middle-div -->
                                <!-- <div class="row">
                                <div class="col-sm-12 middle-div">
                                    <p><strong>Detail:</strong>Stone Cottages (201 - 208)</p>
                                </div>

                            </div> -->
                                <!-- middle-div -->

                            </div>
                            <div class="col-sm-12 bottom-div">
                                <div class="row">
                                    <!-- <div class="col-sm-3">
                                    <label>Detail</label>
                                    <p>Stone Cottages (201 - 208)</p>
                                </div> -->
                                    <div class="col-sm-6">
                                        <label style="font-size: 18px;">Rate Includes</label>
                                        <p>1. Accommodation inclusive of all meals (Full board) 2. One activity included on per night stay. 3. Free usage of the Pool</p>

                                    </div>
                                    <div class="col-sm-6 text-center">

                                        <p style="font-size: 18px;" class="text-right"><strong>15000</strong> avg.per night</p>
                                        <button class="btn btn-primary wow agileits  fadeInUp pull-right" id="book-resort">Add to Cart<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>
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
                $("#datepicker,#datepicker1,#datepicker2").datepicker({ dateFormat: 'MM-yy' });


                $('.panel-default').click(function () {
                    var spanChng = $(this).children('.panel-heading').children('.panel-title').children('span');
                    if ($(this).children('.panel-heading').hasClass('collapsed')) {
                        spanChng.css({
                            "background": "url(images/check.png)",
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
        </script>
    </form>
</body>
</html>
