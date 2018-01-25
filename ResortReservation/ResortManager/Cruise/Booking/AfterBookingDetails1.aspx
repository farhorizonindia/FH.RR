<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AfterBookingDetails1.aspx.cs" Inherits="Cruise_Booking_AfterBookingDetails1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Booking Details</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <!-- <script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script> -->
    <!-- //Meta-Tags -->

    <!-- Custom-Stylesheet-Links -->
    <!-- Bootstrap-CSS -->
    <link rel="stylesheet" href="css/Newcss/bootstrap.min.css" type="text/css" media="all">
    <!-- Index-Page-CSS -->
    <!-- <link rel="stylesheet" href="css/style.css" 		type="text/css" media="all"> -->
    <!-- Animate.CSS -->
    <link rel="stylesheet" href="css/Newcss/animate.css" type="text/css" media="all">
    <!-- //Custom-Stylesheet-Links -->
    <link rel="stylesheet" href="css/Newcss/style.css">
    <link rel="stylesheet" href="css/Newcss/cruise.css" type="text/css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="header agileits " id="home">

            <!-- Navbar -->
            <nav class="navbar navbar-default inner-pages-navbar agileits  wow bounceInUp">
                <div class="container">

                    <div class="navbar-header agileits ">
                        <button type="button" class="navbar-toggle agileits  collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false">
                            <span class="sr-only agileits ">Toggle navigation</span>
                            <span class="icon-bar agileits "></span>
                            <span class="icon-bar agileits "></span>
                            <span class="icon-bar agileits "></span>
                        </button>
                        <a class="navbar-brand agileits " href="index.html">Resorts</a>
                    </div>

                    <div id="navbar" class="navbar-collapse agileits  navbar-right collapse">
                        <ul class="nav navbar-nav agileits ">
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

            <div class="ak-booking-details">

                <div class="container">
                    <div class="wow slideInDown">
                        <h3 class="ak-booking-details-head">Booking Details  </h3>
                    </div>
                    <div class="booking-details ">
                        <asp:GridView ID="gdvSelectedRooms" runat="server" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Font-Size="Medium">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnRmno" runat="server" Value='<%#Eval("RoomCategoryId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="">

                                            <div class="wow slideInLeft table-responsive ">
                                                <table class="table table-striped">
                                                    <h3>Selected Rooms</h3>

                                                    <div class="wow slideInDown">
                                                        <h4 class="text-danger">Room1: <%=Request.QueryString["AccomName"]%></h4>
                                                    </div>

                                                    <tr>
                                                        <td>Check in:</td>
                                                        <td>Check out:</td>
                                                        <td>Guests:</td>
                                                        <td>Room:</td>
                                                        <td>Total:</td>
                                                    </tr>
                                                    <tr>
                                                        <td><%# Session["Chkin"] %></td>
                                                        <td><%# Session["chkout"] %></td>
                                                        <td><%# Eval("Pax") %></td>
                                                        <td><%# Eval("categoryName") %></td>
                                                        <td><%# Eval("Total") %></td>
                                                    </tr>

                                                </table>
                                            </div>
                                        </div>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <div class="ak-btn-cont">
                            <div id="DivContinue" class=" btn-summerised">
                                <asp:HiddenField ID="hdnfTotalPaybleAmt" runat="server" />
                                <asp:Button runat="server" CssClass="btn btn-info btnWidth100 btnFont" ID="btnBack" Text="Back" OnClick="btnBack_Click" />
                                <asp:Button CssClass="btn btn-info btnWidth100 btnFont" runat="server" ID="btnSmbt" Text="Proceed" OnClick="btnSmbt_Click" />
                            </div>
                            <%--<a href="available.html">
                                <button class="btn btn-primary agileits  wow fadeInLeft button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span>BACK</button></a>
                            <button id="proceed-login" class="btn btn-primary agileits  wow fadeInRight button2">PROCEED<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="pnlLogin" runat="server">
                    <div>
                        <div class="container">
                            <div class=" ">
                                <div class="ak-main-login payment-online-form-left agileits">
                                    <h4>LOGIN</h4>
                                    <form class="ak-form" method="post" action="#">

                                        <ul class="agileits ">
                                            <li class="agileits  wow fadeInLeft">
                                                <input required="" class="text-box-dark agileits " type="text" value="Enter Your Email" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Enter Your Email';}" name="email"></li>
                                            <li class="agileits  wow fadeInRight">
                                                <input required="" class="text-box-dark agileits " type="password" value="Your Password" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Your Password';}" name="password"></li>
                                        </ul>
                                        <div class="ak-btn-cont ak-btn-booking-cont">
                                            <asp:Button ID="btnSbmt" CssClass="btn btn-info btnWidth100 btnFont" runat="server" Height="36px" OnClick="btnSbmt_Click" Text="Login" />
                                            &nbsp;
                                            <asp:Button ID="txtRegNow" CssClass="btn btn-info btnWidth100 btnFont" runat="server" Text="Register Now" OnClick="txtRegNow_Click" />
                                            <%-- <button type="button" id="register-show" class="btn btn-primary agileits  wow fadeInLeft button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span>Register</button></a>
							<button type="submit" id="login" class="btn btn-primary agileits  wow fadeInRight button2">Login<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="customerLogin" runat="server">
                    <table id="tableVerify" runat="server" visible="false">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtCode" placeholder="Enter Code" runat="server"></asp:TextBox>
                                <asp:Button ID="btnVerify" runat="server" OnClick="btnVerify_Click" Text="Verify" />

                            </td>
                            <td></td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hfVCode" runat="server" />
                    <table id="TableCust" runat="server" style="margin: 30px auto; float: none;">
                        <tr>
                            <td>Enter your Email</td>
                            <td>
                                <asp:TextBox ID="txtCustMailId" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Enter password</td>
                            <td>
                                <asp:TextBox ID="txtCustPass" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1ee" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCustPass" ValidationGroup="CustLogin"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="height: 30px;"></div>
                            </td>
                            <td>
                                <div style="height: 30px;"></div>
                            </td>

                        </tr>
                        <tr style="margin-top: 30px;">
                            <td></td>
                            <td style="text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" CssClass="btn btn-info btnWidth100 btnFont" ID="btnCustLogin" ValidationGroup="CustLogin" Text="Login" OnClick="btnCustLogin_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlCustReg" runat="server">
                    <div>
                        <div class="container" id="dvshow" runat="server">

                            <div class="ak-main-login payment-online-form-left agileits">
                                <h4>Register</h4>
                                <form class="ak-form" method="post" action="#">

                                    <ul class="agileits ">
                                        <li class="agileits  wow fadeInLeft">'
                                                <asp:DropDownList ID="ddltitle" class="text-box-dark agileits" runat="server" Style="margin-bottom: 15px">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>Mr</asp:ListItem>
                                                    <asp:ListItem>Mrs</asp:ListItem>
                                                    <asp:ListItem>Miss</asp:ListItem>
                                                    <asp:ListItem>Ms</asp:ListItem>
                                                </asp:DropDownList>
                                            <%-- <select class="text-box-dark agileits" name="title" id="title-name">
                                                    <option selected="selected" value="Title">Title</option>
                                                    <option value="Mr">Mr</option>
                                                    <option value="Mrs">Mrs</option>
                                                    <option value="Miss">Miss</option>
                                                    <option value="Ms">Ms</option>
                                                </select>--%></li>
                                    </ul>
                                    <ul class="agileits ">
                                        <li class="agileits  wow fadeInLeft">
                                            <input required="" class="text-box-dark agileits " id="txtFirstName" runat="server" type="text" value="First Name" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'First Name';}" name="firstname" /></li>
                                        <li class="agileits  wow fadeInRight">
                                            <input required="" class="text-box-dark agileits " id="txtLastName" runat="server" type="text" value="Last Name" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Last Name';}" name="lastname" /></li>
                                    </ul>
                                    <ul class="agileits ">
                                        <li class="agileits  wow fadeInLeft">
                                            <asp:TextBox ID="txtMailAddress" runat="server" OnTextChanged="txtMailAddress_TextChanged" AutoPostBack="True" required="" class="text-box-dark agileits " type="text" value="Email Address" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Email Address';}" name="register-email"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtMailAddress" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label></li>
                                        <%-- <input required="" class="text-box-dark agileits " type="text" value="Email Address" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Email Address';}" name="register-email" /></li>--%>
                                        <li class="agileits  wow fadeInRight">
                                            <input required="" class="text-box-dark agileits " id="txtTelephone" runat="server" type="text" value="Telephone" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Telephone';}" name="telephone" /></li>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtTelephone" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                    </ul>
                                    <ul class="agileits ">
                                        <li class="agileits  wow fadeInLeft">
                                            <input required="" class="text-box-dark agileits " id="txtAddress1" runat="server" type="text" value="Address 1" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Address 1';}" name="address1" /></li>
                                        <li class="agileits  wow fadeInRight">
                                            <input required="" class="text-box-dark agileits " id="txtaddress2" runat="server" type="text" value="Address 2" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Address 2';}" name="address2" /></li>
                                    </ul>
                                    <ul class="agileits ">
                                        <li class="agileits  wow fadeInLeft">
                                            <input required="" class="text-box-dark agileits " type="text" value="City" id="txtCity" runat="server" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'City';}" name="city" /></li>
                                        <li class="agileits  wow fadeInRight">
                                            <input required="" class="text-box-dark agileits " type="text" value="State" id="txtState" runat="server" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'State';}" name="state"></li>
                                    </ul>
                                    <ul class="agileits ">
                                        <li class="agileits  wow fadeInLeft">
                                            <input required="" class="text-box-dark agileits " type="text" value="Postcode" onfocus="this.value = '';" id="txtPostcode" runat="server" onblur="if (this.value == '') {this.value = 'Postcode';}" name="postcode"></li>
                                        <li class="agileits  wow fadeInRight">
                                            <input required="" class="text-box-dark agileits " type="password" value="Password" onfocus="this.value = '';" id="txtPassWord" runat="server" onblur="if (this.value == '') {this.value = 'Password';}" name="register-password"></li>
                                    </ul>
                                    <ul class="agileits ">
                                        <li class="agileits  wow fadeInLeft">
                                            <asp:DropDownList ID="ddlCountry" runat="server" class="text-box-dark agileits" name="title" CssClass="form-control"></asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlCountry" InitialValue="0" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                            <%-- <select class="text-box-dark agileits" name="title" id="title-name">
                                                    <option selected="selected" value="Title">Country</option>
                                                    <option value="India">India</option>
                                                    <option value="USA">USA</option>
                                                    <option value="UK">UK</option>
                                                    <option value="Norway">Norway</option>
                                                </select>--%></li>
                                    </ul>
                                    <ul class="agileits">
                                        <li class="agileits  wow fadeInLeft">
                                            <input required="" class="text-box-dark agileits " type="text" value="Postcode" onfocus="this.value = '';" id="txtPaymentMethod" runat="server" onblur="if (this.value == '') {this.value = 'Payment';}" name="Payment Method">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtPaymentMethod" ValidationGroup="Cust"></asp:RequiredFieldValidator></li>
                                    </ul>
                                    <div class="ak-btn-cont ak-btn-booking-cont">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info font16" OnClick="btnSubmit_Click" ValidationGroup="Cust" />

                                        <asp:Button ID="btnCloseCust" runat="server" CssClass="btn btn-info font16" OnClick="btnCloseCust_Click" Text="Close" />
                                        <%-- <button type="submit" name="register" id="submit-register" class="btn btn-primary agileits  wow fadeInLeft button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span>Submit</button></a>
							<button type="button" id="register-close" class="btn btn-primary agileits  wow fadeInRight button2">Close<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                    </div>

                                </form>
                            </div>

                        </div>
                    </div>
                </asp:Panel>
                <div style="width: 70%; margin: 0 auto; float: none;">
                    <asp:Panel ID="pnlFullDetails" class="CSSTableGenerator" Width="100%" runat="server">
                        <div id="BookRef" runat="server">
                            <table style="border: 1px solid; background: -webkit-gradient( linear, left top, left bottom, color-stop(0.05, #a9cae3), color-stop(1, #a9cae3) );">
                                <tr>
                                    <td class="auto-style5">Enter Booking Reference Name.</td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtBookRef" runat="server" Width="281px"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="ReqBookRef" runat="server" ControlToValidate="txtBookRef" ErrorMessage="*" ValidationGroup="Pay"></asp:RequiredFieldValidator></td>
                                </tr>
                            </table>
                        </div>
                        <h2 style="font-family: 'Times New Roman'; font-style: italic; font-weight: bold; font-size: 17px;">Payment Details</h2>
                        <table style="border: thin solid #000000; width: 80%; margin: 0 auto;">
                            <tr style="background-color: rgba(149, 190, 222, 0.8) !important">
                                <td style="border: thin solid #000000; font-weight: bold; width: 22%;">Invoice To</td>
                                <td style="border: thin solid #000000">
                                    <asp:Label ID="lblAgentName" runat="server"></asp:Label></td>
                            </tr>
                            <tr style="background-color: #EFF3FB;">
                                <td style="border: thin solid #000000; font-weight: bold">
                                    <asp:Label ID="lblBilling" runat="server" Text="Billing Address : "></asp:Label></td>
                                <td style="border: thin solid #000000">
                                    <asp:Label ID="lblBillingAddress" runat="server" Text=""></asp:Label></td>

                            </tr>
                            <tr style="background-color: rgba(149, 190, 222, 0.8) !important">
                                <td style="border: thin solid #000000; font-weight: bold">
                                    <asp:Label ID="lbPayment" runat="server" Text="Payment Method : "></asp:Label></td>
                                <td style="border: thin solid #000000">
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
                                <table style="border: thin solid #000000; width: 80%; margin: 0 auto;">
                                    <tr style="background-color: rgba(149, 190, 222, 0.8) !important">
                                        <td style="border: thin solid #000000; font-weight: bold; width: 22%;" class="auto-style3">Total amount</td>
                                        <td style="border: thin solid #000000" class="auto-style4">
                                            <asp:Label ID="lbltotAmt" runat="server" Text="Label"></asp:Label></td>

                                    </tr>
                                    <tr style="background-color: #EFF3FB;">
                                        <td style="border: thin solid #000000; font-weight: bold">Booking Amount</td>
                                        <td style="border: thin solid #000000">
                                            <asp:Label ID="lblCurrency" runat="server" Text="Label"></asp:Label><asp:Label ID="txtPaidAmt" runat="server"></asp:Label>
                                            (25% of total)</td>

                                    </tr>
                                    <tr style="background-color: rgba(149, 190, 222, 0.8) !important">
                                        <td style="border: thin solid #000000; font-weight: bold">Balance Amount</td>
                                        <td style="border: thin solid #000000">
                                            <asp:Label ID="lblBalanceAmt" runat="server" Text="Label"></asp:Label>
                                            (75% of total) to be paid prior to <%Response.Write(Convert.ToDateTime(System.DateTime.Now).AddDays(-90).ToString("dddd, MMMM d, yyyy")); %></td>

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
                    <div style="height: 40px"></div>
                    <center>
                        <asp:Panel ID="pnlBookButton" Width="70%" runat="server">

                            <asp:Button ID="btnPayProceed" runat="server" Text="Proceed For Payment" CssClass="btn btn-info btnWidth100 btnFont" OnClick="btnPayProceed_Click" ValidationGroup="Pay" Font-Size="Medium" />
                            <asp:Label ID="lblPaymentErr" runat="server" ForeColor="#FF3300"></asp:Label>
                            <asp:HiddenField ID="hftxtpaidamt" runat="server" />
                            <br />
                        </asp:Panel>
                        </center>
                </div>
            </div>

            <!-- Footer -->
            <div class="footer agileits ">
                <div class="container">

                    <div class="col-md-6 col-sm-6 agileits  footer-grids">
                        <div class="col-md-4 col-sm-4 agileits  footer-grid footer-grid-1 wow fadeInUp">
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
                        <div class="col-md-4 col-sm-4 footer-grid agileits  footer-grid-2 wow fadeInUp">
                            <ul class="agileits ">
                                <li class="agileits "><a href="index.html">Home</a></li>
                                <li class="agileits "><a href="about.html">About</a></li>
                                <li class="agileits "><a href="cuisines.html">Cuisines</a></li>
                                <li class="agileits "><a href="gallery.html">Gallery</a></li>
                            </ul>
                        </div>
                        <div class="clearfix"></div>
                    </div>

                    <div class="col-md-6 col-sm-6 footer-grids agileits  social wow fadeInUp">
                        <ul class="social-icons agileits ">
                            <li class="agileits "><a href="#" class="facebook agileits " title="Go to Our Facebook Page"></a></li>
                            <li class="agileits "><a href="#" class="twitter agileits " title="Go to Our Twitter Account"></a></li>
                            <li class="agileits "><a href="#" class="googleplus agileits " title="Go to Our Google Plus Account"></a></li>
                            <li class="agileits "><a href="#" class="instagram agileits " title="Go to Our Instagram Account"></a></li>
                            <li class="agileits "><a href="#" class="youtube agileits " title="Go to Our Youtube Channel"></a></li>
                        </ul>
                    </div>

                    <div class="col-md-6 col-sm-6 footer-grids agileits  copyright wow fadeInUp">
                        <p>&copy; 2017  Resorts. All Rights Reserved | Design by </p>
                    </div>
                    <div class="clearfix"></div>

                </div>
            </div>
            <!-- //Footer -->



            <!-- Necessary-JavaScript-Files-&-Links -->

            <!-- Default-JavaScript -->
            <script type="text/javascript" src="js/jquery-2.1.4.min.js"></script>
            <!-- Bootstrap-JavaScript -->
            <script type="text/javascript" src="js/bootstrap.min.js"></script>

            <!-- Animate.CSS-JavaScript -->
            <script src="js/wow.min.js"></script>
            <script>
                new WOW().init();
            </script>
            <!-- //Animate.CSS-JavaScript -->
            <%-- <script type="text/javascript">
                $('#proceed-login').click(function (event) {
                    $('#login-cont').show('fast');
                });
                $('#register-show').click(function (event) {
                    $('#register-cont').show('fast');
                });
                $('#register-close').click(function (event) {
                    $('#register-cont').hide('fast');
                });
            </script>--%>
    </form>
</body>
</html>
