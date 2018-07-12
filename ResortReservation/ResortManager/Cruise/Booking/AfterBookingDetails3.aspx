<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AfterBookingDetails3.aspx.cs" Inherits="Cruise_Booking_AfterBookingDetails3" %>

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

                          <asp:DropDownList id="ddlCurrency"
                AppendDataBoundItems="True"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged"  >
             
                             <asp:ListItem  Value="USD">INR</asp:ListItem>
                             <asp:ListItem  Value="INR">USD</asp:ListItem>
           </asp:DropDownList>
                    </div>

                    <div  id="navbar" class="navbar-collapse agileits  navbar-right collapse">
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
            <%--  <div class="col-sm-12" style="margin-top: 10px; margin-bottom: 10px; padding-left: 0; padding-right: 29px; position: fixed; width: 100%; z-index: 1;">
                <asp:Button ID="Button3" runat="server" Text="Add Room" Style="float: right;" class="btn btn-primary agileits  wow fadeInLeft button1" CssClass="btn btn-info font16" OnClick="btnaddroom_Click" />
            </div>--%>
            <section style="width: 100%; height: 70px; padding: 0;">


                <nav>
                    <ol class="cd-breadcrumb triangle">
                        <li><em>Search</em></li>
                        <li><em>Available Rooms</em></li>
                        <li class="current"><em>Book and Pay</em></li>
                        <%--<li><a href="#">Project</a></li>--%>
                    </ol>
                </nav>
            </section>
            <div class="yours-details">
                <div class="row" runat="server">
                    <div class="col-sm-7">
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
                                    <img src="../../images/next-arrow.png" style="width: 16px; float: right; margin-top: 4px; transform: rotate(90deg);" />
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
                                            <div class="text-center">
                                                <asp:Label ID="lblLoginMsg" runat="server" Text=" "></asp:Label>
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
                        <div runat="server" id="regdv">
                            <div class="panel panel-default" runat="server" id="dvpaneldefault">
                                <div id="dvreg" runat="server">
                                    <div class="panel-heading collapsed" role="tab" id="headingOne"
                                        data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="false" aria-controls="collapseOne" style="background-color: #f5f5f5; border-color: #ddd;">
                                        <div class="panel-title">
                                            <label style="margin-bottom: 0;">REGISTER </label>
                                            <img src="../../images/next-arrow.png" style="width: 16px; float: right; margin-top: 4px; transform: rotate(90deg);">
                                        </div>
                                    </div>
                                </div>
                                <div id="collapseOne" class="panel-collapse collapse " role="tabpanel" aria-labelledby="headingOne">
                                    <div class="top-form panel-body" runat="server" id="custRegis">
                                        <%--  <h4 style="padding-bottom: 19px;">Register</h4>--%>
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
                                                        <div style="width: 85%; float: right;">
                                                            <asp:TextBox ID="txtFirstName" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="First Name"></asp:TextBox>
                                                        </div>
                                                        <div style="width: 50%;">
                                                            <asp:DropDownList ID="ddltitle" runat="server" class="text-box-dark agileits" Style="margin-bottom: 0px; margin-top: 0%; font-size: 12px; width: 28%; border: 1px solid #9e9e9e; height: 53px;">

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
                                            <%--<strong>Terms & Conditions</strong>
                                            <div style="width: 100%; height: 100px; overflow: auto; margin-bottom: 1%; margin-top: 2%;" runat="server" id="dvkalakho">
                                                <strong>Booking & Deposit:</strong>
                                                <br />
                                                At the time of booking: 25% Advance deposit
                                                <br />
                                                45 Days prior to CHECK IN: 75% Balance payment
                                                <br />
                                                <br />
                                                <strong>Cancellation:</strong>
                                                <br />


                                                Between 60 to 46 days prior to CHECK IN: 10% cancellation charges
                                                <br />
                                                Between 45 to 30 days prior to CHECK IN: 25% cancellation charges
                                                <br />
                                                Less than 29 days prior to CHECK IN and NO SHOW: 100% cancellation charges
                                                <br />
                                                ** All reservations are subject to cancellation if payments are not received by the due date.
                                                <br />
                                                <br />
                                                <strong>CHILDREN/MINORS:</strong>
                                                <br />
                                                <p>
                                                    Children are welcome! Under 12 years can share the room with their parents or guardians. In case an extra mattress is requested, it shall be on chargeable basis.
                                                </p>

                                                <br />
                                                12 years and above will require an extra mattress and only 1 extra mattress is permitted per cottage.
                                                <br />
                                                <br />
                                                Children will be the sole responsibility of their parents/guardians and must never be left unattended.<br />
                                                <br />
                                                <strong>DIFFERENTLY ABLED GUESTS:</strong>
                                                <br />

                                                <p>
                                                    You must report any special attention required at the time the reservation is made. Far Horizon India will make reasonable attempts to accommodate the special needs of differently abled travelers, but is not responsible in the event it is unable to do so.
                                                </p>
                                                <br />
                                                <br />
                                                <strong>TIPS:</strong>
                                                <br />


                                                <p>
                                                    It is customary to give gratuities, subject to your satisfaction of services rendered. Gratuities are not included in the tariff. Individual tipping is not recommended. We request you to put the tips in the common 'Tip Box'.
                                                </p>
                                                <br />
                                                <br />
                                                <strong>PETS:
                                                </strong>
                                                <br />


                                                Pets are not allowed in the retreat.
                                                <br />
                                                <br />
                                                Dera Village Retreat shall not be required to refund any amount paid by any guest who must leave the resort prematurely for any reason, nor shall Dera Village Retreat identified herein be responsible for the lodging, meals, return transportation or other expenses incurred by such guest. However, facilitation of all services to the said affect would be made on request and paid for by the guest.

                                            </div>--%>
                                           <%-- <div style="width: 100%; height: 100px; overflow: auto; margin-bottom: 1%; margin-top: 2%;" runat="server" id="dvvikundam">
                                                <strong>Booking & Deposit:</strong>
                                                <br />
                                                At the time of booking: 25% Advance deposit
                                                <br />
                                                45 Days prior to CHECK IN: 75% Balance payment
                                                <br />
                                                <br />
                                                <strong>Cancellation:</strong>
                                                <br />


                                                Between 60 to 46 days prior to CHECK IN: 10% cancellation charges
                                                <br />
                                                Between 45 to 30 days prior to CHECK IN: 25% cancellation charges
                                                <br />
                                                Less than 29 days prior to CHECK IN and NO SHOW: 100% cancellation charges
                                                <br />
                                                ** All reservations are subject to cancellation if payments are not received by the due date.
                                                <br />
                                                <br />
                                                <strong>CHILDREN/MINORS:</strong>
                                                <br />
                                                <p>
                                                    Children are welcome! Under 12 years can share the room with their parents or guardians. Above 12 years will require another room. No extra beds or mattress are available on board.
                                                </p>

                                                
                                                <br />
                                                Children will be the sole responsibility of their parents/guardians and must never be left unattended.<br />
                                                <br />
                                                <strong>DIFFERENTLY ABLED GUESTS:</strong>
                                                <br />

                                                <p>
                                                    You must report any special attention required at the time the reservation is made. Far Horizon India will make reasonable attempts to accommodate the special needs of differently abled travelers, but is not responsible in the event it is unable to do so.
                                                </p>
                                                <br />
                                                <br />
                                                <strong>TIPS:</strong>
                                                <br />

                                                <p>
                                                    It is customary to give gratuities, subject to your satisfaction of services rendered. Gratuities are not included in the tariff. Individual tipping is not recommended. We request you to put the tips in the common 'Tip Box'.
                                                </p>
                                                <br />
                                                <br />
                                                <strong>PETS:
                                                </strong>
                                                <br />


                                                Pets are not allowed in the retreat.
                                                <br />
                                                <br />
                                                <p>
                                                    Vaikundam & Sauver Nigam shall not be required to refund any amount paid by any guest who must leave the resort prematurely for any reason, nor shall Vaikundam & Sauver Nigam identified herein be responsible for the lodging, meals, return transportation or other expenses incurred by such guest. However, facilitation of all services to the said affect would be made on request and paid for by the guest.
                                                </p>

                                            </div>
                                            <div style="width: 100%; height: 100px; overflow: auto; margin-bottom: 1%; margin-top: 2%;" runat="server" id="dvJamba">
                                                <strong>Booking & Deposit:</strong>
                                                <br />
                                                At the time of booking: 25% Advance deposit
                                                <br />
                                                45 Days prior to CHECK IN: 75% Balance payment
                                                <br />
                                                <br />
                                                <strong>Cancellation:</strong>
                                                <br />


                                                Between 60 to 46 days prior to CHECK IN: 10% cancellation charges
                                                <br />
                                                Between 45 to 30 days prior to CHECK IN: 25% cancellation charges
                                                <br />
                                                Less than 29 days prior to CHECK IN and NO SHOW: 100% cancellation charges
                                                <br />
                                                ** All reservations are subject to cancellation if payments are not received by the due date.
                                                <br />
                                                <br />
                                                <strong>CHILDREN/MINORS:</strong>
                                                <br />
                                                <p>
                                                    Children are welcome! Under 12 years can share the room with their parents or guardians. In case an extra mattress is requested, it shall be on chargeable basis.
                                                </p>

                                                <br />
                                                12 years and above will require an extra mattress and only 1 extra mattress is permitted per cottage.
                                                <br />
                                                <br />
                                                Children will be sole the responsibility of their parents/guardians and must never be left unattended.<br />
                                                <br />
                                                <strong>DIFFERENTLY ABLED GUESTS:</strong>

                                                <p>
                                                    You must report any special attention required at the time the reservation is made. Far Horizon India will make reasonable attempts to accommodate the special needs of differently abled travelers, but is not responsible in the event it is unable to do so.
                                                </p>
                                                <br />
                                                <br />
                                                <strong>TIPS:</strong>
                                                <br />


                                                <p>
                                                    It is customary to give gratuities, subject to your satisfaction of services rendered. Gratuities are not included in the tariff. Individual tipping is not recommended. We request you to put the tips in the common 'Tip Box'.
                                                </p>
                                                <br />
                                                <br />
                                                <strong>PETS:
                                                </strong>
                                                <br />


                                                Pets are not allowed in the retreat.
                                                <br />
                                                <br />
                                                <p>Far Horizon shall not be required to refund any amount paid by any guest who must leave the resort prematurely for any reason, nor shall FAR HORIZON identified herein be responsible for the lodging, meals, return transportation or other expenses incurred by such guest. However, facilitation of all services to the said affect would be made on request and paid for by the guest.</p>


                                            </div>--%>
                                            <table style="margin-left: 33%; margin-top: 2%; margin-bottom: 2%;">
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkRegTerm" runat="server" /></td>
                                                    <td>I agree to the 
                                                       <%-- <asp:LinkButton ID="LinkButton2"  Font-Underline="True"  runat="server" Text="Terms & Conditions"  OnClick="lbtnTandC_Click" OnClientClick="window.document.forms[0].target = '_blank';"></asp:LinkButton>--%>
                                                        <a id="linkTandCReg" runat="server" target="_blank">terms and conditions</a>
                                                    </td>
                                                    
                                                </tr>
                                            </table>

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


                            <div class="panel panel-default" id="dvBilling" runat="server" visible="false">
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
                                            Continue as Guest
                                        </label>
                                    </div>
                                </div>
                                <div id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                                    <div class="panel-body">
                                        <div class="ak-form">

                                            <ul class="agileits ">
                                                <li class="agileits">

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
                                                            <div style="width: 100%;">
                                                                <div style="width: 85%; float: right;">
                                                                    <asp:TextBox ID="txtFirstname1" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="First Name" TabIndex="1"></asp:TextBox>
                                                                </div>
                                                                <div style="width: 50%;">
                                                                    <asp:DropDownList ID="ddlList1" class="text-box-dark agileits" runat="server" Style="margin-bottom: 9px; margin-top: 5%; font-size: 12px; width: 28%; border: 1px solid #9e9e9e; height: 51px;" TabIndex="1">

                                                                        <asp:ListItem>Mr</asp:ListItem>
                                                                        <asp:ListItem>Mrs</asp:ListItem>
                                                                        <asp:ListItem>Miss</asp:ListItem>
                                                                        <asp:ListItem>Ms</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <%--<input required="" id="" runat="server" type="text" value="First Name" onblur="if (this.value == '') {this.value = 'First Name';}" name="firstname" />--%>
                                                            <%-- <input required="" class="text-box-dark agileits " id="txtFirstname1" runat="server" type="text" value="First Name" onfocus="this.value = '';" name="firstname" />--%></li>
                                                        <li class="agileits">
                                                            <asp:TextBox ID="txtEmailid1" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" PlaceHolder="Email" TabIndex="3"></asp:TextBox>


                                                            <%--<input required="" id="" runat="server" type="text" value="Last Name" onblur="if (this.value == '') {this.value = 'Last Name';}" name="lastname" />--%></li>
                                                    </ul>
                                                    <ul class="agileits ">
                                                        <li class="agileits">
                                                            <asp:DropDownList ID="ddlCountry1" class="text-box-dark agileits" runat="server" CssClass="form-control" Style="font-size: 12px; border-radius: 0; padding: 17px 12px !important; margin-top: 2%; margin-bottom: 2%;" TabIndex="5"></asp:DropDownList>

                                                            <%--<asp:TextBox ID="txtEmailid1" runat="server" PlaceHolder="Email Address" class="form-control"></asp:TextBox>--%>

                                                            <%--<input required="" id="" runat="server" type="text" value="Email Address" onblur="if (this.value == '') {this.value = 'Email Address';}" name="register-email" />--%></li>
                                                        <li class="agileits">

                                                            <%--<input required="" id="" runat="server" type="text" value="Telephone" onblur="if (this.value == '') {this.value = 'Telephone';}" name="telephone" />--%></li>
                                                    </ul>
                                                    <ul class="agileits ">
                                                        <li class="agileits">
                                                            <asp:TextBox ID="txtCity1" Placeholder="City" TabIndex="7" runat="server" class="text-box-dark agileits " onfocus="this.value = '';"></asp:TextBox>


                                                            <%--<input required="" id="" runat="server" type="text" value="Address 1" onblur="if (this.value == '') {this.value = 'Address 1';}" name="address1" />--%></li>
                                                        <li class="agileits">
                                                            <asp:TextBox ID="txtadress22" TabIndex="9" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" Placeholder="Address2"></asp:TextBox>


                                                            <%--<input required="" id="" runat="server" type="text" value="Address 2" onblur="if (this.value == '') {this.value = 'Address 2';}" name="address2" />--%></li>
                                                    </ul>
                                                </div>
                                                <div class="col-sm-6">
                                                    <ul class="agileits ">
                                                        <li class="agileits">
                                                            <asp:TextBox ID="txtLastanme1" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" placeholder="Last Name" TabIndex="2"></asp:TextBox>

                                                            <%--<input required="" id="" runat="server" type="text" value="City" onblur="if (this.value == '') {this.value = 'City';}" name="city" />--%></li>
                                                        <li class="agileits">
                                                            <asp:TextBox ID="txtMobilephone1" runat="server" TabIndex="4" class="text-box-dark agileits " onfocus="this.value = '';" PlaceHolder="Contact No" MaxLength="10"></asp:TextBox>


                                                            <%--<input required="" type="text" id="" runat="server" value="State" onblur="if (this.value == '') {this.value = 'State';}" name="state" />--%></li>
                                                    </ul>
                                                    <ul class="agileits ">
                                                        <li class="agileits">
                                                            <asp:TextBox ID="txtState1" TabIndex="6" Placeholder="State" runat="server" class="text-box-dark agileits " onfocus="this.value = '';"></asp:TextBox>



                                                            <%--<input required="" type="text" id="" runat="server" value="Postcode" onblur="if (this.value == '') {this.value = 'Postcode';}" name="postcode" />--%></li>
                                                        <li class="agileits">


                                                            <asp:TextBox ID="txtAddress11" TabIndex="8" class="text-box-dark agileits " runat="server" onfocus="this.value = '';" Placeholder="Address1"></asp:TextBox>
                                                            <%--<input required="" type="password" id="" runat="server" value="Password" onblur="if (this.value == '') {this.value = 'Password';}" name="register-password" placeholder="password" />--%></li>
                                                    </ul>
                                                    <ul class="agileits " style="margin-bottom: 2%;">
                                                        <li class="agileits">
                                                            <asp:TextBox ID="txtPostCode1" TabIndex="10" Placeholder="Post Code" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" MaxLength="6"></asp:TextBox>
                                                            <asp:TextBox ID="txtPassword1" Visible="false" Placeholder="Password" runat="server" class="text-box-dark agileits " onfocus="this.value = '';" TextMode="Password"></asp:TextBox>
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
                                            <%--<strong>Terms & Conditions</strong>
                                            <div style="width: 100%; height: 100px; overflow: auto; margin-bottom: 1%; margin-top: 2%;" runat="server" id="dvkalakho1">
                                                <strong>Booking & Deposit:</strong>
                                                <br />
                                                At the time of booking: 25% Advance deposit
                                                <br />
                                                45 Days prior to CHECK IN: 75% Balance payment
                                                <br />
                                                <br />
                                                <strong>Cancellation:</strong>
                                                <br />


                                                Between 60 to 46 days prior to CHECK IN: 10% cancellation charges
                                                <br />
                                                Between 45 to 30 days prior to CHECK IN: 25% cancellation charges
                                                <br />
                                                Less than 29 days prior to CHECK IN and NO SHOW: 100% cancellation charges
                                                <br />
                                                ** All reservations are subject to cancellation if payments are not received by the due date.
                                                <br />
                                                <br />
                                                <strong>CHILDREN/MINORS:</strong>
                                                <br />
                                                <p>
                                                    Children are welcome! Under 12 years can share the room with their parents or guardians. In case an extra mattress is requested, it shall be on chargeable basis.
                                                </p>

                                                <br />
                                                12 years and above will require an extra mattress and only 1 extra mattress is permitted per cottage.
                                                <br />
                                                <br />
                                                Children will be the sole responsibility of their parents/guardians and must never be left unattended.<br />
                                                <br />
                                                <br />
                                                <strong>DIFFERENTLY ABLED GUESTS:</strong>
                                                <br />

                                                <p>
                                                    You must report any special attention required at the time the reservation is made. Far Horizon India will make reasonable attempts to accommodate the special needs of differently abled travelers, but is not responsible in the event it is unable to do so.
                                                </p>
                                                <br />
                                                <br />
                                                <strong>TIPS:</strong>
                                                <br />


                                                <p>
                                                    It is customary to give gratuities, subject to your satisfaction of services rendered. Gratuities are not included in the tariff. Individual tipping is not recommended. We request you to put the tips in the common 'Tip Box'.
                                                </p>
                                                <br />
                                                <br />
                                                <strong>PETS:
                                                </strong>
                                                <br />


                                                Pets are not allowed in the retreat.
                                                <br />
                                                <br />
                                                Dera Village Retreat shall not be required to refund any amount paid by any guest who must leave the resort prematurely for any reason, nor shall Dera Village Retreat identified herein be responsible for the lodging, meals, return transportation or other expenses incurred by such guest. However, facilitation of all services to the said affect would be made on request and paid for by the guest.

                                            </div>
                                            <div style="width: 100%; height: 100px; overflow: auto; margin-bottom: 1%; margin-top: 2%;" runat="server" id="dvvikundam2">
                                                <strong>Booking & Deposit:</strong>
                                                <br />
                                                At the time of booking: 25% Advance deposit
                                                <br />
                                                45 Days prior to CHECK IN: 75% Balance payment
                                                <br />
                                                <br />
                                                <strong>Cancellation:</strong>
                                                <br />


                                                Between 60 to 46 days prior to CHECK IN: 10% cancellation charges
                                                <br />
                                                Between 45 to 30 days prior to CHECK IN: 25% cancellation charges
                                                <br />
                                                Less than 29 days prior to CHECK IN and NO SHOW: 100% cancellation charges
                                                <br />
                                                ** All reservations are subject to cancellation if payments are not received by the due date.
                                                <br />
                                                <br />
                                                <strong>CHILDREN/MINORS:</strong>
                                                <br />
                                                <p>
                                                    Children are welcome! Under 12 years can share the room with their parents or guardians. Above 12 years will require another room. No extra beds or mattress are available on board.
                                                </p>

                                               
                                                <br />
                                                Children will be the sole responsibility of their parents/guardians and must never be left unattended.<br />
                                                <br />
                                                <strong>DIFFERENTLY ABLED GUESTS:</strong>
                                                <br />

                                                <p>
                                                    You must report any special attention required at the time the reservation is made. Far Horizon India will make reasonable attempts to accommodate the special needs of differently abled travelers, but is not responsible in the event it is unable to do so.
                                                </p>
                                                <br />
                                                <br />
                                                <strong>TIPS:</strong>
                                                <br />

                                                <p>
                                                    It is customary to give gratuities, subject to your satisfaction of services rendered. Gratuities are not included in the tariff. Individual tipping is not recommended. We request you to put the tips in the common 'Tip Box'.
                                                </p>
                                                <br />
                                                <br />
                                                <strong>PETS:
                                                </strong>
                                                <br />


                                                Pets are not allowed in the retreat.
                                                <br />
                                                <br />
                                                <p>
                                                    Vaikundam & Sauver Nigam shall not be required to refund any amount paid by any guest who must leave the resort prematurely for any reason, nor shall Vaikundam & Sauver Nigam identified herein be responsible for the lodging, meals, return transportation or other expenses incurred by such guest. However, facilitation of all services to the said affect would be made on request and paid for by the guest.
                                                </p>

                                            </div>
                                            <div style="width: 100%; height: 100px; overflow: auto; margin-bottom: 1%; margin-top: 2%;" runat="server" id="dvJamba3">
                                                <strong>Booking & Deposit:</strong>
                                                <br />
                                                At the time of booking: 25% Advance deposit
                                                <br />
                                                45 Days prior to CHECK IN: 75% Balance payment
                                                <br />
                                                <br />
                                                <strong>Cancellation:</strong>
                                                <br />


                                                Between 60 to 46 days prior to CHECK IN: 10% cancellation charges
                                                <br />
                                                Between 45 to 30 days prior to CHECK IN: 25% cancellation charges
                                                <br />
                                                Less than 29 days prior to CHECK IN and NO SHOW: 100% cancellation charges
                                                <br />
                                                ** All reservations are subject to cancellation if payments are not received by the due date.
                                                <br />
                                                <br />
                                                <strong>CHILDREN/MINORS:</strong>
                                                <br />
                                                <p>
                                                    Children are welcome! Under 12 years can share the room with their parents or guardians. In case an extra mattress is requested, it shall be on chargeable basis.
                                                </p>

                                                <br />
                                                12 years and above will require an extra mattress and only 1 extra mattress is permitted per cottage.
                                                <br />
                                                <br />
                                                Children will be sole the responsibility of their parents/guardians and must never be left unattended.<br />
                                                <br />
                                                <strong>DIFFERENTLY ABLED GUESTS:</strong>
                                                <br />

                                                <p>
                                                    You must report any special attention required at the time the reservation is made. Far Horizon India will make reasonable attempts to accommodate the special needs of differently abled travelers, but is not responsible in the event it is unable to do so.
                                                </p>
                                                <br />
                                                <br />
                                                <strong>TIPS:</strong>
                                                <br />


                                                <p>
                                                    It is customary to give gratuities, subject to your satisfaction of services rendered. Gratuities are not included in the tariff. Individual tipping is not recommended. We request you to put the tips in the common 'Tip Box'.
                                                </p>
                                                <br />
                                                <br />
                                                <strong>PETS:
                                                </strong>
                                                <br />


                                                Pets are not allowed in the retreat.
                                                <br />
                                                <br />
                                                <p>Far Horizon shall not be required to refund any amount paid by any guest who must leave the resort prematurely for any reason, nor shall FAR HORIZON identified herein be responsible for the lodging, meals, return transportation or other expenses incurred by such guest. However, facilitation of all services to the said affect would be made on request and paid for by the guest.</p>


                                            </div>--%>
                                            <table style="margin-left: 33%; margin-top: 2%; margin-bottom: 2%;">
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkTerm" runat="server" /></td>
                                                    <td>I agree to the 
                                                        <%--<asp:LinkButton ID="LinkButton3"  Font-Underline="True"  runat="server" Text="Terms & Conditions"  OnClick="lbtnTandC_Click"></asp:LinkButton>--%>
                                                        <a id="linkTandCGuest" runat="server" target="_blank" >terms and conditions</a>

                                                    </td>
                                                    
                                                </tr>
                                            </table>
                                            <div style="width: 16rem !important; margin: 0 auto;">
                                                <asp:Button ID="Button1" runat="server" TabIndex="11" Text="Submit" class="btn btn-primary agileits  wow fadeInLeft button1" Style="width: 100%" CssClass="btn btn-info font16" OnClick="Button1_Click" ValidationGroup="Cust" />

                                            </div>
                                        </div>

                                        <%-- <asp:Button ID="Button1" runat="server" Text="Button" class="btn btn-primary agileits  wow fadeInLeft button1" OnClick="Button1_Click" />--%>
                                        <%--<button type="submit" name="register" id="submit-register" class="btn btn-primary agileits  wow fadeInLeft button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span>Submit</button>--%>
                                        <%--<button type="button" id="register-close" class="btn btn-primary agileits  wow fadeInRight button2">Close<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default" id="divspcl" runat="server" visible="false">
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
                            <div class="ak-btn-cont ak-btn-booking-cont" runat="server" id="dvbtnid" visible="false">
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
                            <div style="width: 100%; margin: 0 auto; float: none;">
                                <asp:Panel ID="pnlFullDetails" class="CSSTableGenerator" Width="100%" runat="server" Style="box-shadow: 3px 3px 9px 2px rgba(0,0,0,0.4); border: 1px solid rgba(0,0,0,0.5) !important; border-radius: 4px;">
                                    <div id="BookRef" runat="server" style="width: 90%; margin: auto; padding-top: 5%;">
                                        <table style="background: #ECECEC !important; width: 100%;" id="tblBref" runat="server">
                                            <tr>
                                                <td class="auto-style5" style="padding-left: 3%; font-size: 16px;">Enter Booking Reference Name.</td>
                                                <td style="float: right; padding-right: 1%;">
                                                    <asp:TextBox ID="txtBookRef" runat="server" Width="254px"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="ReqBookRef" runat="server" ControlToValidate="txtBookRef" ErrorMessage="*" ValidationGroup="Pay"></asp:RequiredFieldValidator></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <h4 style="font-size: 20px; padding-top: 14px; padding-bottom: 14px; font-size: 17px; font-family: 'Montserrat', sans-serif;">Payment Details</h4>

                                    <table class="table table-bordered" style="width: 90%; margin: 0 auto; border: 1px solid #ddd;">
                                        <tr style="background-color: #ECECEC !important; height: 40px;">
                                            <td style="font-weight: bold; padding-top: 10px; padding-right: 12%;">Invoice To</td>
                                            <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                <asp:Label ID="lblAgentName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr style="background-color: #f9f9f9 !important; height: 40px;">
                                            <td style="font-weight: bold; padding-right: 8%; padding-top: 10px;">
                                                <asp:Label ID="lblBilling" runat="server" Text="Billing Address  "></asp:Label></td>
                                            <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                <asp:Label ID="lblBillingAddress" runat="server" Text=""></asp:Label></td>

                                        </tr>
                                        <%-- <tr style="background-color: #ECECEC !important; height: 40px;">
                                            <td style="font-weight: bold; padding-right: 7%; padding-top: 10px;">
                                                <asp:Label ID="Label3" runat="server" Text="Special Request  "></asp:Label></td>
                                            <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                <asp:Label ID="lblSpecialRequest" runat="server" Text=""></asp:Label></td>

                                        </tr>--%>
                                        <tr style="background-color: #ECECEC !important; height: 40px;">
                                            <td style="font-weight: bold; width: 27%; padding-right: 6%; padding-top: 10px;">
                                                <asp:Label ID="lbPayment" runat="server" Text="Payment Method  "></asp:Label></td>
                                            <td style="padding-left: 30px !important; padding-right: 30px !important;">
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

                                    <%--<br />
                                    <br />
                                    <div style="height: 40px"></div>--%>
                                    <asp:Panel ID="panelwithoutCreditAgent" Width="100%" Style="padding-top: 15px" runat="server" Font-Size="Medium">
                                        <div>

                                            <table class="table table-bordered" style="width: 90%; margin: 0 auto; border: 1px solid #ddd;">
                                                <tr style="background-color: #ECECEC !important;">
                                                    <td style="font-weight: bold; width: 27%; padding-right: 9%; padding-top: 10px;"
                                                        class="auto-style3">Total amount</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;"
                                                        class="auto-style4">
                                                        <asp:Label ID="lbltotAmt" runat="server" Text="Label"></asp:Label></td>

                                                </tr>

                                                <tr style="background-color: #f9f9f9 !important;">
                                                    <td style="font-weight: bold; padding-right: 20%; padding-top: 10px;" class="auto-style3">Tax(<asp:Label ID="lbltextIn" runat="server" Text=" "></asp:Label>)</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;"
                                                        class="auto-style4">
                                                        <asp:Label ID="lblTax" runat="server" Text=" "></asp:Label></td>

                                                </tr>
                                                <tr style="background-color: #ECECEC !important;">
                                                    <td style="font-weight: bold; padding-right: 18%; padding-top: 10px;" class="auto-style3">Gross</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;"
                                                        class="auto-style4">
                                                        <asp:Label ID="lblGross" runat="server" Text=" "></asp:Label></td>

                                                </tr>
                                                <tr style="background-color: #f9f9f9!important;">
                                                    <td style="font-weight: bold; padding-right: 5%; padding-top: 10px;">Advance Amount</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lblCurrency" runat="server" Text=" "></asp:Label><asp:Label ID="txtPaidAmt" runat="server"></asp:Label>
                                                        <asp:Label ID="lblpertext" runat="server" Text=" "></asp:Label></td>

                                                </tr>
                                                <tr style="background-color: #ECECEC !important;" id="trbalanceamount" runat="server">
                                                    <td style="font-weight: bold; padding-right: 5%; padding-top: 10px;">Balance Amount</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lblBalanceAmt" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>
                                                <tr style="background-color: #f9f9f9!important;" id="trbalancedate" runat="server">
                                                    <td style="font-weight: bold; padding-right: 9%; padding-top: 10px;">Balance Payment Date</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lblBalancedate" runat="server" Text=" "></asp:Label><asp:Label ID="Priorto" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>
                                            </table>

                                            <br />
                                            <div style="padding-left: 30%;">
                                                <%--<asp:CheckBox ID="chkterms" runat="server" AutoPostBack="true" OnCheckedChanged="chkterms_CheckedChanged"  Visible="false"/><asp:Label ID="Label3" Style="padding-left: 2%;" runat="server" Text="By clicking I accept terms and condition"></asp:Label>--%>
                                                <asp:CheckBox ID="chkterms" runat="server" AutoPostBack="true" OnCheckedChanged="chkterms_CheckedChanged"  Visible="false"/>
                                                <asp:Label ID="lblT" runat="server" Text="By proceeding I accept the"></asp:Label>
                                                <%--<asp:LinkButton ID="lbtnTandC" Style="padding-left: 2%;" Font-Underline="True"  runat="server" Text="terms and conditions"  OnClick="lbtnTandC_Click"></asp:LinkButton>--%>
                                                <a id="linkTandCSign" runat="server" target="_blank" Font-Underline="True">terms and conditions</a>
                                                <%--<asp:HyperLink ID="hplTandC" runat="server" Text="By Proceeding I Accept The Terms & Conditions"></asp:HyperLink>--%>
                                            </div>
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
                            <br />


                            <br />
                            <div id="pnlBookButton" style="padding-bottom: 5px;" runat="server" class="text-center">
                                <asp:Button ID="btnPayProceed" runat="server" Text="Proceed For Payment" CssClass="btn btn-info btnWidth100 btnFont" OnClick="btnPayProceed_Click" ValidationGroup="Pay" Font-Size="Medium" />
                                <asp:Label ID="lblPaymentErr" runat="server" ForeColor="#FF3300"></asp:Label>
                                <asp:HiddenField ID="hftxtpaidamt" runat="server" />
                                <asp:HiddenField ID="hdnfTotalPaybleAmt" runat="server" />
                                <br />
                            </div>


                            <!-- ####################### -->
                        </div>
                    </div>
                    <div class="col-sm-5 left-col" style="box-shadow: 3px 3px 9px 2px rgba(0,0,0,0.4);">
                        <h4 class="text-left" style="padding-top: 3%; font-size: 17px; font-family: 'Montserrat', sans-serif; border-bottom: 1px solid #ccc; padding-bottom: 13px; border-bottom: 1px solid rgba(193, 193, 193, 0.44); text-align: center;">Your Reservation</h4>
                        <div class="row room-detail">
                            <asp:GridView ID="gdvHotelRoomRates" runat="server" ForeColor="#333333" GridLines="None" Font-Size="Medium" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gdvHotelRoomRates_RowDataBound" OnRowCommand="gdvHotelRoomRates_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <h3 style="font-size: 22px; padding-top: 16px; padding-bottom: 10px;"><%# Eval("categoryName") %></h3>

                                            <%--    <div class="col-sm-12  first">
                                                <label style="font-size: 18px;"></label>
                                                <p><%#  Session["RoomDescription"].ToString() %></p>
                                            </div>--%>
                                            <div class="col-sm-12 second" style="padding-left: 0; padding-right: 0;">
                                                <img src='<%# Eval("ImagePath") %>' class="img-responsive" alt="room-">
                                            </div>
                                            <div class="col-sm-12 third">
                                                <div class="row">
                                                    <div class="col-sm-12 top-div">
                                                        <div class="row" style="padding-top: 25px;">
                                                            <!-- <div class="col-sm-3">
                                            <label>Room</label>
                                            <p>Pool Facing Dlx Cottage</p>
                                        </div> -->

                                                            <div class="col-sm-6">
                                                                <label>Room Type:</label>

                                                                <%--<img src="images/down.png" class="img-responsive" alt="">--%>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("RoomType") %>'></asp:Label>
                                                                <%-- <select class="form-control" id="sel1">

                                                                        <option>No</option>
                                                                        <option>Yes</option>
                                                                    </select>--%>
                                                            </div>
                                                            <div class="col-sm-6" style="width: 30%;">
                                                                <label>Guests:</label>

                                                                <%--<img src="images/down.png" class="img-responsive" alt="">--%>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Pax") %>'></asp:Label>
                                                                <%-- <select class="form-control" id="sel1">
                                                                        <option>1</option>
                                                                        <option>2</option>
                                                                        <option>3</option>
                                                                        <option>4</option>
                                                                        <option>5</option>
                                                                        <option>6</option>
                                                                    </select>--%>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <label>Check In:</label>

                                                                <%--<img src="images/down.png" class="img-responsive" alt="">--%>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Session["getcheckin"].ToString() %>'></asp:Label>
                                                                <%-- <select class="form-control" id="sel1">
                                                                        <option>1</option>
                                                                        <option>2</option>
                                                                        <option>3</option>
                                                                        <option>4</option>
                                                                        <option>5</option>
                                                                        <option>6</option>
                                                                    </select>--%>
                                                            </div>

                                                            <div class="col-sm-6">
                                                                <label>Check Out:</label>

                                                                <%--<img src="images/down.png" class="img-responsive" alt="">--%>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Session["getcheckout"].ToString() %>'></asp:Label>
                                                                <%-- <select class="form-control" id="sel1">
                                                                        <option>1</option>
                                                                        <option>2</option>
                                                                        <option>3</option>
                                                                        <option>4</option>
                                                                        <option>5</option>
                                                                        <option>6</option>
                                                                    </select>--%>
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
                                            <div class="col-sm-12 bottom-div fourth" style="border-bottom: 1px solid rgba(0,0,0,0.5);">
                                                <div class="row">
                                                    <!-- <div class="col-sm-3">
                                    <label>Detail</label>
                                    <p>Stone Cottages (201 - 208)</p>
                                </div> -->
                                                    <div class="col-sm-12 top">
                                                        <label style="font-size: 18px;">Rate Includes</label>
                                                        <p style="padding-top: 5px; text-align: justify; padding-bottom: 5px;">
                                                            <%# Session["Descriptionforadd"].ToString() %>
                                                        </p>
                                                    </div>
                                                    <div class="row text-center bottom">
                                                        <div class="col-sm-6">
                                                            <p style="font-size: 18px;" class="text-left">Night/s: </p>
                                                            <p style="font-size: 18px;" class="text-left">Room Price: </p>

                                                            <p style="font-size: 18px;" class="text-left">Gross: </p>

                                                            <p style="font-size: 18px;" class="text-left">Tax(<%# Session["Taxpax"].ToString() %>%) : </p>

                                                            <%--<button class="btn btn-primary wow agileits  fadeInUp pull-right" id="book-resort">Add to Cart<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                                        </div>

                                                            <div style="display:none">
                                                                        <%# Session["Nights"] = DataBinder.Eval(Container.DataItem, "Nights")%>
                                                                        <%#  Session["Price"] = DataBinder.Eval(Container.DataItem, "Price")%>
                                                                        <%# Session["Total"] = DataBinder.Eval(Container.DataItem, "Total")%>
                                                                        <%# Session["Tax"] = DataBinder.Eval(Container.DataItem, "Tax")%>
                                                                 <%# Session["Inclusivetax"] = DataBinder.Eval(Container.DataItem, "Inclusivetax")%>
                                                                        
                                                                        </div>
                                                        <div class="col-sm-6">

                                                            <% if(ddlCurrency.Text != "USD")
                                                                {
                                                                      Response.Write(" <p style='font-size: 18px;'  class='text-right'><strong> " + Session["Nights"] + " </strong></p>");

                                                                      currency(Session["Price"].ToString(), "INR");
                                                                    Response.Write(" <p style='font-size: 18px;'  class='text-right'><strong>USD " +  ViewState["Comman"] + " </strong></p>");

                                                                      currency(Session["Total"].ToString(), "INR");
                                                                    Response.Write(" <p style='font-size: 18px;'  class='text-right'><strong>USD " +  ViewState["Comman"] + " </strong></p>");

                                                                      currency(Session["Tax"].ToString(), "INR");
                                                                    Response.Write(" <p style='font-size: 18px;'  class='text-right'><strong>USD " +  ViewState["Comman"] + " </strong></p>");

                                                                }else
                                                                {
                                                                     
                                                                    Response.Write(" <p style='font-size: 18px;'  class='text-right'><strong> " +  Session["Nights"] + " </strong></p>");                                                                    
                                                                    Response.Write(" <p style='font-size: 18px;'  class='text-right'><strong> " +  Session["Price"] + " </strong></p>");                                                                    
                                                                    Response.Write(" <p style='font-size: 18px;'  class='text-right'><strong> " +  Session["Total"] + " </strong></p>");                                                                   
                                                                    Response.Write(" <p style='font-size: 18px;'  class='text-right'><strong> " +  Session["Tax"] + " </strong></p>");
                                                                }


                                                                         %>
                                                           <%-- <p style="font-size: 18px;" class="text-right"><strong><%# Eval("Nights") %></strong> </p>
                                                            <p style="font-size: 18px;" class="text-right"><strong><%# Eval("Price") %></strong> </p>

                                                            <p style="font-size: 18px;" class="text-right"><strong><%# Eval("Total") %></strong> </p>

                                                            <p style="font-size: 18px;" class="text-right"><strong><%# Eval("Tax") %></strong> </p>--%>

                                                            <%--<button class="btn btn-primary wow agileits  fadeInUp pull-right" id="book-resort">Add to Cart<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                                        </div>
                                                        <div class="col-sm-12 gross">
                                                            <div class="col-sm-6">

                                                                <p style="font-size: 18px;" class="text-left">Total: </p>
                                                                <%--<button class="btn btn-primary wow agileits  fadeInUp pull-right" id="book-resort">Add to Cart<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%
                                                                    if(ddlCurrency.Text != "USD")
                                                                    {
                                                                         currency(Session["Inclusivetax"].ToString(), "INR");
                                                                    Response.Write(" <p style='font-size: 18px;'  class='text-right'><strong>USD " +  ViewState["Comman"] + " </strong></p>");
                                                                    }
                                                                    else
                                                                    {
                                                                       
                                                                    Response.Write(" <p style='font-size: 18px;'  class='text-right'><strong>" +  Session["Inclusivetax"] + " </strong></p>");
                                                                    }

                                                                     %>

                                                              <%--  <p style="font-size: 18px;" class="text-right"><strong><%# Eval("Inclusivetax") %></strong> </p>--%>
                                                                <%--<button class="btn btn-primary wow agileits  fadeInUp pull-right" id="book-resort">Add to Cart<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12" style="padding-bottom: 5px;">
                                                            <asp:ImageButton Height="25px" Width="25px" padding="0" ImageUrl="~/Cruise/Booking/images/closetrash (2).png" ID="imgbtnDelete" CommandName="Remove" runat="server" />
                                                        </div>
                                                    </div>

                                                </div>


                                            </div>

                                        </ItemTemplate>
                                    </asp:TemplateField>




                                </Columns>
                            </asp:GridView>

                        </div>
                        <div class="col-sm-12 bottom-btn" style="padding-left: 0; padding-right: 0;">
                            <div class="row">
                                <div>
                                    <div class="total-value col-sm-12">
                                        <asp:Label ID="Label6" runat="server" Style="float: left; padding-left: 2%;" Text="Total"></asp:Label>

                                        <asp:Label ID="lblAllTotal" runat="server" Text=" "></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12" style="margin-top: 10px; margin-bottom: 10px; padding-left: 0; padding-right: 4px;">
                                    <asp:Button ID="btnaddroom" runat="server" Text="Add Room" Style="float: right;" class="btn btn-primary agileits  wow fadeInLeft button1" CssClass="btn btn-info font16" OnClick="btnaddroom_Click" />
                                </div>


                            </div>
                        </div>
                    </div>

                </div>

            </div>

        </div>
        <!-- Footer -->
        <%--   <div class="footer agileits ">
            <div class="container">

                <div class="col-md-6 col-sm-6 agileits  footer-grids">
                    <div class="col-md-4 col-sm-4 footer-grid agileits  footer-grid-1 wow fadeInUp">
                        <ul class="agileits ">--%>
        <%-- <li class="agileits ">5 Star Hotels</li>
                            <li class="agileits ">Beach Resorts</li>
                            <li class="agileits ">Beach Houses</li>
                            <li class="agileits ">Water Houses</li>--%>
        <%--</ul>
                    </div>
                    <div class="col-md-4 col-sm-4 footer-grid agileits  footer-grid-2 wow fadeInUp">
                        <ul class="agileits ">--%>
        <%-- <li class="agileits "><a href="gallery.html">Bahamas</a></li>
                            <li class="agileits "><a href="gallery.html">Hawaii</a></li>
                            <li class="agileits "><a href="gallery.html">Miami</a></li>
                            <li class="agileits "><a href="gallery.html">Ibiza</a></li>--%>
        <%--   </ul>
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
        <%--<li class="agileits "><a href="#" class="facebook agileits " title="Go to Our Facebook Page"></a></li>
                        <li class="agileits "><a href="#" class="twitter agileits " title="Go to Our Twitter Account"></a></li>
                        <li class="agileits "><a href="#" class="googleplus agileits " title="Go to Our Google Plus Account"></a></li>
                        <li class="agileits "><a href="#" class="instagram agileits " title="Go to Our Instagram Account"></a></li>
                        <li class="agileits "><a href="#" class="youtube agileits " title="Go to Our Youtube Channel"></a></li>--%>
        <%-- </ul>
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
                $("#datepicker,#datepicker1,#datepicker2").datepicker({ dateFormat: 'MM-yy' });


                $('.panel-default').click(function () {
                    var spanChng = $(this).children('.panel-heading').children('.panel-title').children('span');
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
        </script>
    </form>
</body>
</html>
