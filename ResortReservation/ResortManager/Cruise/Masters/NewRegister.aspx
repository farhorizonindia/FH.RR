<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewRegister.aspx.cs" Inherits="Cruise_Masters_NewRegister" EnableEventValidation="false" ValidateRequest="false" %>

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

                        <ul class="nav agileits  navbar-nav">
                            <li class="dropdown">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">LOGIN <span class="caret"></span></a>
                                <ul class="dropdown-menu">
                                    <li><a href="../Booking/agentLogin1.aspx">Login Partner</a></li>
                                    <li><a href="../Masters/NewRegister.aspx">Customer</a></li>
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
                                                <%-- <h4>LOGIN</h4>--%>
                                                <div class="ak-form">

                                                    <ul class="agileits ">
                                                        <li class="agileits">
                                                            <asp:TextBox ID="txtCustMailId" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Email"></asp:TextBox>
                                                            <%--<input required="" id="" runat="server"  type="text" value="Enter Your Email"  onblur="if (this.value == '') {this.value = 'Enter Your Email';}" name="email" />--%></li>
                                                        <li class="agileits">
                                                            <asp:TextBox ID="txtCustPass" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Password" TextMode="Password"></asp:TextBox>
                                                            <%--<input required="" id="" runat="server"  type="password" value="Your Password"  onblur="if (this.value == '') {this.value = 'Your Password';}" name="password" />--%></li>
                                                    </ul>

                                                </div>
                                                <div class="ak-btn-cont ak-btn-booking-cont login-btn">
                                                    <asp:Button runat="server" class="btn btn-primary agileits button2" CssClass="btn btn-info btnWidth100 btnFont" ID="btnCustLogin" Text="Login" OnClick="btnCustLogin_Click" />
                                                    <asp:Button runat="server" class="btn btn-primary agileits button2" CssClass="btn btn-info btnWidth100 btnFont" ID="Button1" Text="Forgot Password" OnClick="btnCustLogin_Click1" />
                                                    <!-- <button type="button" id="register-show" class="btn btn-primary agileits button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span> Register</button> -->
                                                    <%-- <button type="submit" id="login" class="btn btn-primary agileits button2">Login<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                                </div>
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
                                <div id="dvreg" runat="server">
                                    <div class="panel panel-default">

                                        <div class="panel-heading collapsed" role="tab" id="headingOne"
                                            data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="false" aria-controls="collapseOne" style="background-color: #f5f5f5; border-color: #ddd;">
                                            <div class="panel-title">
                                                <label style="margin-bottom: 0;">REGISTER </label>
                                                <img src="../../images/next-arrow.png" style="width: 16px; float: right; margin-top: 4px; transform: rotate(90deg);">
                                            </div>
                                        </div>

                                        <div id="collapseOne" class="panel-collapse collapse " role="tabpanel" aria-labelledby="headingOne">
                                            <div class="top-form panel-body" runat="server" id="custRegis">
                                                <%-- <h4 style="padding-bottom: 19px;">Register</h4>--%>
                                                <div class="ak-form">

                                                    <ul class="agileits ">


                                                        <li class="agileits list-unstyled">

                                                            <%--<select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%;">
                                            <option selected="selected" value="Title">Title</option>
                                            <option value="Mr">Mr</option>
                                            <option value="Mrs">Mrs</option>
                                            <option value="Miss">Miss</option>
                                            <option value="Ms">Ms</option>
                                        </select>--%>
                                                        </li>
                                                    </ul>
                                                    <div class="row agileits ">
                                                        <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                            <div style="width: 100%;">
                                                                <div style="width: 87%; float: right;">
                                                                    <asp:TextBox ID="txtFirstName" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="First Name"></asp:TextBox>
                                                                </div>
                                                                <div style="width: 50%;">
                                                                    <asp:DropDownList ID="ddltitle" runat="server" class="text-box-dark agileits" Style="margin-bottom: 0px; margin-top: 0%; font-size: 12px; width: 24%; border: 1px solid #9e9e9e; height: 53px;">

                                                                        <asp:ListItem>Mr</asp:ListItem>
                                                                        <asp:ListItem>Mrs</asp:ListItem>
                                                                        <asp:ListItem>Miss</asp:ListItem>
                                                                        <asp:ListItem>Ms</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <%-- <input required="" id="" runat="server" type="text" value="First Name" onblur="if (this.value == '') {this.value = 'First Name';}" name="firstname" />--%>
                                                        </div>
                                                        <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                            <asp:TextBox ID="txtLastName" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Last Name"></asp:TextBox>
                                                            <%-- <input required="" id="" runat="server" type="text" value="Last Name" onblur="if (this.value == '') {this.value = 'Last Name';}" name="lastname" />--%>
                                                        </div>
                                                    </div>
                                                    <div class="row agileits ">
                                                        <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                            <asp:TextBox ID="txtMailAddress" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Email"></asp:TextBox>
                                                            <%--<input required="" id="" runat="server" type="text" value="Email Address" onblur="if (this.value == '') {this.value = 'Email Address';}" name="register-email" />--%>
                                                            <%-- <asp:TextBox ID="txtMailAddress" runat="server" PlaceHolder="Email Address" class="form-control"></asp:TextBox>--%>
                                                        </div>
                                                        <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                            <asp:TextBox ID="txtTelephone" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Contact No" MaxLength="10"></asp:TextBox>
                                                            <%--<input required="" id="" runat="server" type="text" value="Telephone" onblur="if (this.value == '') {this.value = 'Telephone';}" name="telephone" />--%>
                                                        </div>
                                                    </div>
                                                    <div class="row agileits ">
                                                        <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                            <asp:DropDownList ID="ddlCountry" class="text-box-dark agileits" runat="server" CssClass="form-control" Style="font-size: 12px; border-radius: 0; padding: 17px 12px !important;"></asp:DropDownList>

                                                            <%--<input required="" runat="server" id="" type="text" value="Address 1" onblur="if (this.value == '') {this.value = 'Address 1';}" name="address1" />--%>
                                                        </div>
                                                        <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                            <asp:TextBox ID="txtState" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="State"></asp:TextBox>

                                                            <%-- <input required="" runat="server" id="" type="text" value="Address 2" onblur="if (this.value == '') {this.value = 'Address 2';}" name="address2" />--%>
                                                        </div>
                                                    </div>
                                                    <div class="row agileits ">
                                                        <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                            <asp:TextBox ID="txtCity" class="text-box-dark agileits " runat="server" onfocus="this.value = '';" Placeholder="City"></asp:TextBox>
                                                            <%--<input required="" id="" runat="server" type="text" value="City" onblur="if (this.value == '') {this.value = 'City';}" name="city" />--%>
                                                        </div>
                                                        <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                            <asp:TextBox ID="txtAddress1" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Address1"></asp:TextBox>
                                                            <%--<input required="" type="text" value="State" id="" runat="server" onblur="if (this.value == '') {this.value = 'State';}" name="state" />--%>
                                                        </div>
                                                    </div>
                                                    <div class="row agileits ">
                                                        <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                            <asp:TextBox ID="txtaddress2" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Address2"></asp:TextBox>

                                                            <%-- <input required="" type="text" value="Postcode" id="" runat="server" onblur="if (this.value == '') {this.value = 'Postcode';}" name="postcode" />--%>
                                                        </div>
                                                        <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                            <asp:TextBox ID="txtPostcode" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Post Code" MaxLength="6"></asp:TextBox>

                                                            <%--<input required="" id="" runat="server" type="password" value="Password" onblur="if (this.value == '') {this.value = 'Password';}" name="register-password" />--%>
                                                        </div>
                                                    </div>
                                                    <div class="row agileits ">
                                                        <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                            <asp:TextBox ID="txtpassword" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Password" TextMode="Password"></asp:TextBox>
                                                            <%--<select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%; margin-top: 1%;">
                                            <option selected="selected" value="Title">Country</option>
                                            <option value="India">India</option>
                                            <option value="USA">USA</option>
                                            <option value="UK">UK</option>
                                            <option value="Norway">Norway</option>
                                        </select>--%>
                                                        </div>
                                                        <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                            <asp:TextBox ID="txtConfirmPassword" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Confirm Password" TextMode="Password"></asp:TextBox>
                                                        </div>
                                                    </div>


                                                    <div class="ak-btn-cont ak-btn-booking-cont">
                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-primary agileits  wow fadeInLeft button1" CssClass="btn btn-info font16" OnClick="btnSubmit_Click" ValidationGroup="Cust" Width="100%" />
                                                        <%-- <asp:Button ID="Button1" runat="server" Text="Button" class="btn btn-primary agileits  wow fadeInLeft button1" OnClick="Button1_Click" />--%>
                                                        <%--<button type="submit" name="register" id="submit-register" class="btn btn-primary agileits  wow fadeInLeft button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span>Submit</button>--%>
                                                        <%--<button type="button" id="register-close" class="btn btn-primary agileits  wow fadeInRight button2">Close<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                    </div>
                                </div>

                            </div>


                        </div>

                    </div>
                    <asp:Panel ID="customerLogin" runat="server">
                        <table id="tableVerify" runat="server" visible="false" width="100%" style="margin-bottom: 2%;">
                            <tr>
                                <td style="text-align: center;">
                                    <div>
                                        <asp:TextBox ID="txtCode" placeholder="Enter Code" runat="server"></asp:TextBox>

                                        <asp:Button Class="btn" ID="btnVerify" runat="server" OnClick="btnVerify_Click" Text="Verify" />
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hfVCode" runat="server" />
                    </asp:Panel>
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
