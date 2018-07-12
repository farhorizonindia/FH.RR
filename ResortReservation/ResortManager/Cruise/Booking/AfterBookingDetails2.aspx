<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AfterBookingDetails2.aspx.cs" Inherits="Cruise_Booking_AfterBookingDetails2" EnableEventValidation="false" ValidateRequest="false" %>

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
                                    <li><a runat="server" id="lnkLogin" href="agentLogin1.aspx">Login Partner</a></li>
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
            <div class="yours-details">
                <div class="row" runat="server">
                    <div class="col-sm-8">
                        <div class="panel panel-default" runat="server" id="pnllogin">
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
                                    <img src="../../images/next-arrow.png" style="width: 16px; float: right; margin-top: 4px;" />
                                </div>
                            </div>
                            <div id="collapse" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                                <div class="panel-body">
                                    <div class="ak-main-login payment-online-form-left agileits">
                                        <h4>LOGIN</h4>
                                        <div class="ak-form">

                                            <ul class="agileits ">
                                                <li class="agileits">
                                                    <asp:TextBox ID="txtCustMailId" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Email"></asp:TextBox>
                                                    <%--<input required="" id="" runat="server" type="text" value="Enter Your Email" onblur="if (this.value == '') {this.value = 'Enter Your Email';}" name="email" />--%></li>
                                                <li class="agileits">
                                                    <asp:TextBox ID="txtCustPass" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Password" TextMode="Password"></asp:TextBox>
                                                    <%--<input required="" id="" runat="server" type="password" value="Your Password" onblur="if (this.value == '') {this.value = 'Your Password';}" name="password" />--%></li>
                                            </ul>
                                            <div class="ak-btn-cont ak-btn-booking-cont login-btn">
                                                <asp:Button runat="server" class="btn btn-primary agileits button2" CssClass="btn btn-info btnWidth100 btnFont" ID="btnCustLogin" ValidationGroup="CustLogin" Text="Login" OnClick="btnCustLogin_Click" />
                                                <asp:Button runat="server" class="btn btn-primary agileits button2" CssClass="btn btn-info btnWidth100 btnFont" ID="Button2" Text="Forgot Password" OnClick="btnCustLogin_Click1" />
                                                <!-- <button type="button" id="register-show" class="btn btn-primary agileits button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span> Register</button> -->
                                                <%-- <button type="submit" id="login" class="btn btn-primary agileits button2">Login<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                            </div>
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
                        <div class="panel panel-default">
                            <div id="dvreg" runat="server">
                                <div class="panel-heading" role="tab" id="headingOne"
                                    role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                    <div class="panel-title">
                                        <label style="margin-bottom: 0;">REGISTER IN  TO BOOK FASTER</label>
                                        <img src="../../images/next-arrow.png" style="width: 16px; float: right; margin-top: 4px;">
                                    </div>
                                </div>
                            </div>
                            <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                                <div class="top-form panel-body" runat="server" id="custRegis">
                                    <h4>Register</h4>
                                    <div class="ak-form">

                                        <ul class="agileits ">


                                            <li class="agileits  wow fadeInLeft list-unstyled">
                                                <asp:DropDownList ID="ddltitle" runat="server" class="text-box-dark agileits" Style="margin-bottom: 15px">

                                                    <asp:ListItem>Mr</asp:ListItem>
                                                    <asp:ListItem>Mrs</asp:ListItem>
                                                    <asp:ListItem>Miss</asp:ListItem>
                                                    <asp:ListItem>Ms</asp:ListItem>
                                                </asp:DropDownList>
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
                                            <div class="col-sm-6 agileits  wow fadeInLeft">
                                                <asp:TextBox ID="txtFirstName" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="First Name"></asp:TextBox>
                                                <%-- <input required="" id="" runat="server" type="text" value="First Name" onblur="if (this.value == '') {this.value = 'First Name';}" name="firstname" />--%>
                                            </div>
                                            <div class="col-sm-6 agileits  wow fadeInRight">
                                                <asp:TextBox ID="txtLastName" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Last Name"></asp:TextBox>
                                                <%-- <input required="" id="" runat="server" type="text" value="Last Name" onblur="if (this.value == '') {this.value = 'Last Name';}" name="lastname" />--%>
                                            </div>
                                        </div>
                                        <div class="row agileits ">
                                            <div class="col-sm-6 agileits  wow fadeInLeft">
                                                <asp:TextBox ID="txtMailAddress" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Email"></asp:TextBox>
                                                <%--<input required="" id="" runat="server" type="text" value="Email Address" onblur="if (this.value == '') {this.value = 'Email Address';}" name="register-email" />--%>
                                                <%-- <asp:TextBox ID="txtMailAddress" runat="server" PlaceHolder="Email Address" class="form-control"></asp:TextBox>--%>
                                            </div>
                                            <div class="col-sm-6 agileits  wow fadeInRight">
                                                <asp:TextBox ID="txtTelephone" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Contact No" MaxLength="10"></asp:TextBox>
                                                <%--<input required="" id="" runat="server" type="text" value="Telephone" onblur="if (this.value == '') {this.value = 'Telephone';}" name="telephone" />--%>
                                            </div>
                                        </div>
                                        <div class="row agileits ">
                                            <div class="col-sm-6 agileits  wow fadeInLeft">
                                                <asp:TextBox ID="txtAddress1" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Address1"></asp:TextBox>
                                                <%--<input required="" runat="server" id="" type="text" value="Address 1" onblur="if (this.value == '') {this.value = 'Address 1';}" name="address1" />--%>
                                            </div>
                                            <div class="col-sm-6 agileits  wow fadeInRight">
                                                <asp:TextBox ID="txtaddress2" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Address2"></asp:TextBox>
                                                <%-- <input required="" runat="server" id="" type="text" value="Address 2" onblur="if (this.value == '') {this.value = 'Address 2';}" name="address2" />--%>
                                            </div>
                                        </div>
                                        <div class="row agileits ">
                                            <div class="col-sm-6 agileits  wow fadeInLeft">
                                                <asp:TextBox ID="txtCity" class="text-box-dark agileits " runat="server" onfocus="this.value = '';" Placeholder="City"></asp:TextBox>
                                                <%--<input required="" id="" runat="server" type="text" value="City" onblur="if (this.value == '') {this.value = 'City';}" name="city" />--%>
                                            </div>
                                            <div class="col-sm-6 agileits  wow fadeInRight">
                                                <asp:TextBox ID="txtState" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="State"></asp:TextBox>
                                                <%--<input required="" type="text" value="State" id="" runat="server" onblur="if (this.value == '') {this.value = 'State';}" name="state" />--%>
                                            </div>
                                        </div>
                                        <div class="row agileits ">
                                            <div class="col-sm-6 agileits  wow fadeInLeft">
                                                <asp:TextBox ID="txtPostcode" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Post Code" MaxLength="6"></asp:TextBox>
                                                <%-- <input required="" type="text" value="Postcode" id="" runat="server" onblur="if (this.value == '') {this.value = 'Postcode';}" name="postcode" />--%>
                                            </div>
                                            <div class="col-sm-6 agileits  wow fadeInRight">
                                                <asp:TextBox ID="txtpassword" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Password" TextMode="Password"></asp:TextBox>
                                                <%--<input required="" id="" runat="server" type="password" value="Password" onblur="if (this.value == '') {this.value = 'Password';}" name="register-password" />--%>
                                            </div>
                                        </div>
                                        <div class="agileits " style="margin: 2% 0%;">
                                            <li class="agileits  wow fadeInLeft list-unstyled">
                                                <asp:DropDownList ID="ddlCountry" class="text-box-dark agileits" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <%--<select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%; margin-top: 1%;">
                                            <option selected="selected" value="Title">Country</option>
                                            <option value="India">India</option>
                                            <option value="USA">USA</option>
                                            <option value="UK">UK</option>
                                            <option value="Norway">Norway</option>
                                        </select>--%>
                                            </li>
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

                        <div class="payment-option">
                            <div id="dvpayment" runat="server" visible="false">
                                <label class="text-left">PAYMENT OPTIONS</label>
                                <p style="padding-bottom: 2%;">Your credit card will be used to guarantee your booking for late arrival - it will not be charged.</p>
                                <div>
                                    <label>CARD DETAILS</label>

                                    <ul class="agileits list-unstyled ">
                                        <li class="agileits  wow fadeInLeft">
                                            <asp:TextBox ID="txtNamenCard" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Name On Card"></asp:TextBox>
                                            <%--<input required="" id="" runat="server" type="text" value="Name On Card" onblur="if (this.value == '') {this.value = 'Name On Card';}" name="Name On Card" />--%></li>
                                    </ul>
                                    <div class="row">
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtcardnumber" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Card Number"></asp:TextBox>
                                            <%-- <input required="" id="" runat="server" type="text" value="Card Number" onblur="if (this.value == '') {this.value = 'Card Number';}" name="Card Number" />--%>
                                        </div>
                                        <div class="col-sm-4">

                                            <div class="book-pag-frm2 agileits ">
                                                <asp:TextBox ID="datepicker2" runat="server" class="date agileits " onfocus="this.value = '';" Placeholder="Expiry Date"></asp:TextBox>
                                                <%--<input id="" type="text" runat="server" value=" Expiry Date" onblur="if (this.value == '') {this.value = 'Expiry Date';}" required="" />--%>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                            </div>
                            <!-- ####################### -->


                            <div class="panel panel-default" id="dvBilling" runat="server">
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
                                        <asp:TextBox ID="txtBilingAddress" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Billing Address"></asp:TextBox>
                                        <%--<input required="" id="" runat="server" type="text" value="Email Address" onblur="if (this.value == '') {this.value = 'Email Address';}" name="register-email" />--%>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default" runat="server" id="dvRefrence">
                                <div class="panel-heading collapsed" role="tab" id="headingTwo" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                    <!-- <h4 class="panel-title">
                                  <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                    Collapsible Group Item #2
                                  </a>
                                </h4> -->
                                    <div class="panel-title">
                                        <span></span>
                                        <label style="padding-left: 4%;">
                                            Continue As a Guest
                                        </label>
                                    </div>
                                </div>
                                <div id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                                    <div class="panel-body">
                                        <div class="ak-form">

                                            <ul class="agileits ">
                                                <li class="agileits">
                                                    <asp:DropDownList ID="ddlList1" class="text-box-dark agileits" runat="server" Style="margin-bottom: 15px">

                                                        <asp:ListItem>Mr</asp:ListItem>
                                                        <asp:ListItem>Mrs</asp:ListItem>
                                                        <asp:ListItem>Miss</asp:ListItem>
                                                        <asp:ListItem>Ms</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%-- <select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%;">
                                                        <option selected="selected" value="Title">Title</option>
                                                        <option value="Mr">Mr</option>
                                                        <option value="Mrs">Mrs</option>
                                                        <option value="Miss">Miss</option>
                                                        <option value="Ms">Ms</option>
                                                    </select>--%>
                                                </li>
                                            </ul>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <ul class="agileits ">
                                                        <li class="agileits">
                                                            <asp:TextBox ID="txtFirstname1" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="First Name"></asp:TextBox>
                                                            <%--<input required="" id="" runat="server" type="text" value="First Name" onblur="if (this.value == '') {this.value = 'First Name';}" name="firstname" />--%>
                                                            <%-- <input required="" class="text-box-dark agileits " id="txtFirstname1" runat="server" type="text" value="First Name" onfocus="this.value = '';" name="firstname" />--%></li>
                                                        <li class="agileits">
                                                            <asp:TextBox ID="txtLastanme1" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" placeholder="Last Name"></asp:TextBox>
                                                            <%--<input required="" id="" runat="server" type="text" value="Last Name" onblur="if (this.value == '') {this.value = 'Last Name';}" name="lastname" />--%></li>
                                                    </ul>
                                                    <ul class="agileits ">
                                                        <li class="agileits">
                                                            <%--<asp:TextBox ID="txtEmailid1" runat="server" PlaceHolder="Email Address" class="form-control"></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtEmailid1" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" PlaceHolder="Email"></asp:TextBox>
                                                            <%--<input required="" id="" runat="server" type="text" value="Email Address" onblur="if (this.value == '') {this.value = 'Email Address';}" name="register-email" />--%></li>
                                                        <li class="agileits">
                                                            <asp:TextBox ID="txtMobilephone1" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" PlaceHolder="Contact No" MaxLength="10"></asp:TextBox>
                                                            <%--<input required="" id="" runat="server" type="text" value="Telephone" onblur="if (this.value == '') {this.value = 'Telephone';}" name="telephone" />--%></li>
                                                    </ul>
                                                    <ul class="agileits ">
                                                        <li class="agileits">
                                                            <asp:TextBox ID="txtAddress11" class="text-box-dark agileits " runat="server" onfocus="this.value = '';" Placeholder="Address1"></asp:TextBox>
                                                            <%--<input required="" id="" runat="server" type="text" value="Address 1" onblur="if (this.value == '') {this.value = 'Address 1';}" name="address1" />--%></li>
                                                        <li class="agileits">
                                                            <asp:TextBox ID="txtadress22" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Address2"></asp:TextBox>
                                                            <%--<input required="" id="" runat="server" type="text" value="Address 2" onblur="if (this.value == '') {this.value = 'Address 2';}" name="address2" />--%></li>
                                                    </ul>
                                                </div>
                                                <div class="col-sm-6">
                                                    <ul class="agileits ">
                                                        <li class="agileits">
                                                            <asp:TextBox ID="txtCity1" Placeholder="City" runat="server" class="text-box-dark agileits " onfocus="this.value = '';"></asp:TextBox>
                                                            <%--<input required="" id="" runat="server" type="text" value="City" onblur="if (this.value == '') {this.value = 'City';}" name="city" />--%></li>
                                                        <li class="agileits">
                                                            <asp:TextBox ID="txtState1" Placeholder="State" runat="server" class="text-box-dark agileits " onfocus="this.value = '';"></asp:TextBox>
                                                            <%--<input required="" type="text" id="" runat="server" value="State" onblur="if (this.value == '') {this.value = 'State';}" name="state" />--%></li>
                                                    </ul>
                                                    <ul class="agileits ">
                                                        <li class="agileits">
                                                            <asp:TextBox ID="txtPostCode1" Placeholder="Post Code" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" MaxLength="6"></asp:TextBox>
                                                            <%--<input required="" type="text" id="" runat="server" value="Postcode" onblur="if (this.value == '') {this.value = 'Postcode';}" name="postcode" />--%></li>
                                                        <li class="agileits">
                                                            <asp:TextBox ID="txtPassword1" Visible="false" Placeholder="Password" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" TextMode="Password"></asp:TextBox>
                                                            <%--<input required="" type="password" id="" runat="server" value="Password" onblur="if (this.value == '') {this.value = 'Password';}" name="register-password" placeholder="password" />--%></li>
                                                    </ul>
                                                    <ul class="agileits " style="margin-bottom: 2%;">
                                                        <li class="agileits">
                                                            <asp:DropDownList ID="ddlCountry1" class="text-box-dark agileits" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            <%--  <select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%; margin-top: 1%;">
                                                        <option selected="selected" value="Title">Country</option>
                                                        <option value="India">India</option>
                                                        <option value="USA">USA</option>
                                                        <option value="UK">UK</option>
                                                        <option value="Norway">Norway</option>
                                                    </select>--%>
                                                        </li>
                                                    </ul>
                                                    <%--<div class="ak-btn-cont ak-btn-booking-cont">
                                                <button type="submit" name="register" id="submit-register" class="btn btn-primary agileits  button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span>Submit</button>
                                                <button type="button" id="register-close" class="btn btn-primary agileits button2">Close<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>
                                            </div>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Button ID="Button1" runat="server" Text="Submit" class="btn btn-primary agileits  wow fadeInLeft button1" CssClass="btn btn-info font16" OnClick="Button1_Click" ValidationGroup="Cust" />
                                        <%-- <asp:Button ID="Button1" runat="server" Text="Button" class="btn btn-primary agileits  wow fadeInLeft button1" OnClick="Button1_Click" />--%>
                                        <%--<button type="submit" name="register" id="submit-register" class="btn btn-primary agileits  wow fadeInLeft button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span>Submit</button>--%>
                                        <%--<button type="button" id="register-close" class="btn btn-primary agileits  wow fadeInRight button2">Close<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default" id="divspcl" runat="server">
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
                                        <textarea class="form-control" rows="3" id="specialrequest" runat="server"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="ak-btn-cont ak-btn-booking-cont">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="buttonUpdate" runat="server" Text="Update" Visible="false" class="btn btn-primary agileits  wow fadeInLeft button1" CssClass="btn btn-info font16" OnClick="Button2_Click" /></td>
                                        <td></td>
                                    </tr>
                                </table>


                                <%-- <asp:Button ID="Button1" runat="server" Text="Button" class="btn btn-primary agileits  wow fadeInLeft button1" OnClick="Button1_Click" />--%>
                                <%--<button type="submit" name="register" id="submit-register" class="btn btn-primary agileits  wow fadeInLeft button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span>Submit</button>--%>
                                <%--<button type="button" id="register-close" class="btn btn-primary agileits  wow fadeInRight button2">Close<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                            </div>

                            <asp:Panel ID="customerLogin" runat="server">
                                <table id="tableVerify" runat="server" visible="false">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtCode" placeholder="Enter Code" runat="server"></asp:TextBox>
                                            <asp:Button ID="btnVerify" runat="server" OnClick="btnVerify_Click" Text="Verify" class="btn btn-primary agileits  wow fadeInLeft button1" />

                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hfVCode" runat="server" />
                            </asp:Panel>
                            <div style="width: 100%; margin: 0 auto; float: none; margin-top: 4%;">
                                <asp:Panel ID="pnlFullDetails" class="CSSTableGenerator" Width="100%" runat="server" Style="box-shadow: 2px 2px 8px #cacaca">
                                    <div id="BookRef" runat="server" style="width: 90%; margin: auto; padding-top: 5%;">
                                        <table style="background: #89E7F1 !important; width: 100%;" id="tblBref" runat="server">
                                            <tr>
                                                <td class="auto-style5" style="padding-left: 3%; font-size: 16px;">Enter Booking Reference Name.</td>
                                                <td style="float: right; padding-right: 1%;">
                                                    <asp:TextBox ID="txtBookRef" runat="server" Width="254px"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="ReqBookRef" runat="server" ControlToValidate="txtBookRef" ErrorMessage="*" ValidationGroup="Pay"></asp:RequiredFieldValidator></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <h2 style="font-size: 20px; padding-top: 8px; padding-bottom: 8px;">Payment Details</h2>
                                    <table style="width: 90%; margin: 0 auto;">
                                        <tr style="background-color: #89E7F1 !important; height: 40px;">
                                            <td style="font-weight: bold; width: 22%; text-align: center;">Invoice To</td>
                                            <td style="">
                                                <asp:Label ID="lblAgentName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr style="background-color: #f9f9f9 !important; height: 40px;">
                                            <td style="font-weight: bold">
                                                <asp:Label ID="lblBilling" runat="server" Text="Billing Address : "></asp:Label></td>
                                            <td style="">
                                                <asp:Label ID="lblBillingAddress" runat="server" Text=""></asp:Label></td>

                                        </tr>
                                        <tr style="background-color: #EFF3FB; height: 40px;">
                                            <td style="font-weight: bold">
                                                <asp:Label ID="Label3" runat="server" Text="Special Request : "></asp:Label></td>
                                            <td style="">
                                                <asp:Label ID="lblSpecialRequest" runat="server" Text=""></asp:Label></td>

                                        </tr>
                                        <tr style="background-color: #f9f9f9 !important; height: 40px;">
                                            <td style="font-weight: bold">
                                                <asp:Label ID="lbPayment" runat="server" Text="Payment Method : "></asp:Label></td>
                                            <td style="">
                                                <asp:Label ID="lbPaymentMethod" runat="server" Text=""></asp:Label><asp:HiddenField ID="hdnfPhoneNumber" runat="server" />
                                            </td>

                                        </tr>

                                        <tr style="display: none;">
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:HiddenField ID="hdnfCreditLimit" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                    <div style="height: 40px"></div>
                                    <asp:Panel ID="panelwithoutCreditAgent" Width="100%" runat="server" Font-Size="Medium">
                                        <div>
                                            <table style="width: 90%; margin: 0 auto;">
                                                <tr style="background-color: #89E7F1 !important;">
                                                    <td style="font-weight: bold; width: 22%;" class="auto-style3">Total amount</td>
                                                    <td style="" class="auto-style4">
                                                        <asp:Label ID="lbltotAmt" runat="server" Text="Label"></asp:Label></td>

                                                </tr>

                                                <tr style="background-color: #f9f9f9!important;">
                                                    <td style="font-weight: bold; width: 22%;" class="auto-style3">Tax</td>
                                                    <td style="" class="auto-style4">
                                                        <asp:Label ID="lblTax" runat="server" Text=" "></asp:Label></td>

                                                </tr>
                                                <tr style="background-color: #f9f9f9!important;">
                                                    <td style="font-weight: bold">Booking Amount</td>
                                                    <td style="">
                                                        <asp:Label ID="lblCurrency" runat="server" Text=" "></asp:Label><asp:Label ID="txtPaidAmt" runat="server"></asp:Label>
                                                        (25% of total)</td>

                                                </tr>
                                                <tr style="background-color: #f9f9f9!important;">
                                                    <td style="font-weight: bold">Balance Amount</td>
                                                    <td style="">
                                                        <asp:Label ID="lblBalanceAmt" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>
                                                <tr style="background-color: #f9f9f9!important;">
                                                    <td style="font-weight: bold">Balance Date</td>
                                                    <td style="">
                                                        <asp:Label ID="lblBalancedate" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>
                                            </table>
                                            <br />
                                            <div style="float: left"></div>
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

                            <div id="pnlBookButton" width="70%" runat="server" style="width: 27%; margin: auto;">
                                <asp:Button ID="btnPayProceed" runat="server" Text="Proceed For Payment" CssClass="btn btn-info btnWidth100 btnFont" OnClick="btnPayProceed_Click" ValidationGroup="Pay" Font-Size="Medium" />
                                <asp:Label ID="lblPaymentErr" runat="server" ForeColor="#FF3300"></asp:Label>
                                <asp:HiddenField ID="hftxtpaidamt" runat="server" />
                                <asp:HiddenField ID="hdnfTotalPaybleAmt" runat="server" />
                                <br />
                            </div>


                            <!-- ####################### -->
                        </div>
                    </div>
                    <div class="col-sm-4 left-col" style="box-shadow: 2px 2px 8px #cacaca;">
                        <h4 class="text-left" style="padding-top: 3%; font-size: 22px; font-family: 'Montserrat', sans-serif;">Available Rooms</h4>
                        <div class="row room-detail">
                            <asp:GridView ID="gdvHotelRoomRates" runat="server" ForeColor="#333333" GridLines="None" Font-Size="Medium" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gdvHotelRoomRates_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div class="col-sm-6">
                                                <img src='<%# Session["ImagePathforadd"].ToString() %>' class="img-responsive" alt="room-">
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-12 top-div">
                                                        <div class="row">
                                                            <!-- <div class="col-sm-3">
                                            <label>Room</label>
                                            <p>Pool Facing Dlx Cottage</p>
                                        </div> -->
                                                            <h3 style="font-size: 17px;"><%# Eval("categoryName") %></h3>
                                                            <div class="col-sm-4">
                                                                <label>Type</label>
                                                                <div class="form-group" style="width: 148%;">
                                                                    <%--<img src="images/down.png" class="img-responsive" alt="">--%>
                                                                    <asp:Label class="form-control" ID="Label2" runat="server" Text='<%# Session["GetroomType123"].ToString() %>'></asp:Label>
                                                                    <%-- <select class="form-control" id="sel1">

                                                                        <option>No</option>
                                                                        <option>Yes</option>
                                                                    </select>--%>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <label>Guests</label>
                                                                <div class="form-group">
                                                                    <%--<img src="images/down.png" class="img-responsive" alt="">--%>
                                                                    <asp:Label ID="Label1" class="form-control" runat="server" Text='<%# Eval("Pax") %>'></asp:Label>
                                                                    <%-- <select class="form-control" id="sel1">
                                                                        <option>1</option>
                                                                        <option>2</option>
                                                                        <option>3</option>
                                                                        <option>4</option>
                                                                        <option>5</option>
                                                                        <option>6</option>
                                                                    </select>--%>
                                                                </div>
                                                            </div>
                                                            <%-- <div class="col-sm-4">
                                                                <label>Room</label>
                                                                <p><%# Eval("roomtype") %></p>
                                                            </div>--%>
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
                                                        <p><%# Session["Descriptionforadd"].ToString() %></p>
                                                    </div>
                                                    <div class="col-sm-6 text-center">

                                                        <p style="font-size: 18px;" class="text-right"><strong><%# Eval("Price") %></strong> avg.per night</p>
                                                        <%--<button class="btn btn-primary wow agileits  fadeInUp pull-right" id="book-resort">Add to Cart<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                                    </div>

                                                </div>


                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>
                    <div style="float: right;box-shadow: 2px 2px 8px #cacaca;padding: 10px;font-weight: bold;">
                        <asp:Label ID="lblAllTotal" runat="server" Text=" "></asp:Label>
                    </div>
                    <asp:Button ID="btnaddroom" runat="server" Text="Add Room" class="btn btn-primary agileits  wow fadeInLeft button1" CssClass="btn btn-info font16" OnClick="btnaddroom_Click" />
                </div>
                
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
                            "background": "url(/images/close.png)",
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
